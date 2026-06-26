using CareBridge.AppointmentAPI.Models;
using CareBridge.AppointmentAPI.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace CareBridge.AppointmentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _service;
    private readonly ILogger<AppointmentController> _logger;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;

    public AppointmentController(
        AppointmentService service,
        ILogger<AppointmentController> logger,
        IMemoryCache cache,
        IConfiguration configuration)
    {
        _service = service;
        _logger = logger;
        _cache = cache;
        _configuration = configuration;
    }

    // ─────────────────────────────────────────────────────────────────────
    // GET: api/appointment/pending
    // Returns all appointments that are waiting for confirmation.
    // This endpoint is typically called by the Reception Dashboard.
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetPending()
    {
        var appointments = await _service.GetPendingAsync();
        return Ok(appointments);
    }

    // ─────────────────────────────────────────────────────────────────────
    // POST: api/appointment/confirm
    //
    // Sample Request
    // {
    //     "appointmentId": 101,
    //     "confirmedBy": "Reception Desk 1"
    // }
    //
    // Updates the appointment status in SQL Server and publishes an
    // AppointmentConfirmed event to Azure Service Bus.
    // ─────────────────────────────────────────────────────────────────────
    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm(
        [FromBody] ConfirmRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return BadRequest("Request body is required.");
        }

        if (request.AppointmentId <= 0)
        {
            return BadRequest("AppointmentId must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(request.ConfirmedBy))
        {
            return BadRequest("ConfirmedBy is required.");
        }

        _logger.LogInformation(
            "Appointment confirmation request received. AppointmentId={AppointmentId}, ConfirmedBy={ConfirmedBy}",
            request.AppointmentId,
            request.ConfirmedBy);

        try
        {
            var appointment = await _service.ConfirmAppointmentAsync(
                request,
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Appointment confirmed successfully. AppointmentConfirmed event published to Azure Service Bus.",
                Appointment = appointment
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(
                ex,
                "Unable to confirm AppointmentId={AppointmentId}",
                request.AppointmentId);

            return BadRequest(new
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unexpected error while confirming AppointmentId={AppointmentId}",
                request.AppointmentId);

            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Success = false,
                Message = "An unexpected error occurred while confirming the appointment."
            });
        }
    }

    // ─────────────────────────────────────────────────────────────────────
    // GET: api/appointment/analytics
    //
    // Returns today's appointment confirmation statistics.
    //
    // Demonstrates the Cache-Aside Pattern:
    // 1. Check IMemoryCache.
    // 2. If found, return cached data.
    // 3. Otherwise query SQL Server and cache the results.
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet("analytics")]
    public async Task<IActionResult> GetAnalytics()
    {
        var cacheKey = $"analytics-{DateTime.UtcNow:yyyy-MM-dd}";

        // Check cache first
        if (_cache.TryGetValue(cacheKey, out object? cachedData))
        {
            return Ok(new
            {
                source = "cache",
                data = cachedData
            });
        }

        using var connection = new SqlConnection(
            _configuration.GetConnectionString("CareBridgeDB"));

        const string sql = @"
SELECT
    DepartmentName,
    ProviderName,
    TotalConfirmed,
    LastUpdated
FROM Analytics
WHERE RecordDate = @RecordDate
ORDER BY TotalConfirmed DESC;";

        var rows = await connection.QueryAsync(sql, new
        {
            RecordDate = DateTime.UtcNow.Date
        });

        // Convert DapperRow into strongly typed objects
        var result = rows.Select(r => new
        {
            departmentName = (string?)r.DepartmentName,
            providerName = (string?)r.ProviderName,
            totalConfirmed = (int)r.TotalConfirmed,
            lastUpdated = (DateTime)r.LastUpdated
        }).ToList();

        // Cache for five minutes
        _cache.Set(
            cacheKey,
            result,
            TimeSpan.FromMinutes(5));

        return Ok(new
        {
            source = "database",
            data = result
        });
    }

    // ─────────────────────────────────────────────────────────────────────
    // GET: api/appointment/timeline/{patientId}
    //
    // Returns the complete event history for a patient.
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet("timeline/{patientId:int}")]
    public async Task<IActionResult> GetTimeline(int patientId)
    {
        using var connection = new SqlConnection(
            _configuration.GetConnectionString("CareBridgeDB"));

        const string sql = @"
SELECT
    pt.TimelineId,
    pt.EventType,
    pt.EventDate,
    pt.ProviderName,
    pt.DepartmentName,
    pt.Notes,
    p.FullName AS PatientName
FROM PatientTimeline pt
INNER JOIN Patient p
    ON p.PatientId = pt.PatientId
WHERE pt.PatientId = @PatientId
ORDER BY pt.EventDate DESC;";

        var timeline = await connection.QueryAsync(sql, new
        {
            PatientId = patientId
        });

        return Ok(timeline);
    }
}
