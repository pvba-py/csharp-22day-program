using CareBridge.EFCoreDemo.Models.Generated;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core DbContext.
// ASP.NET Core will automatically create and inject it when needed.
builder.Services.AddDbContext<CareBridgeScaffoldContext>();

// Add Swagger support.
// Swagger gives us a testing screen for APIs.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow Vue.js running on another port
// to call this API from the browser.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS.
app.UseCors();

// Simple health-check endpoint.
app.MapGet("/", () =>
{
    return "CareBridge API is running";
});

// Return first 20 patients.
// EF Core converts this LINQ query into SQL.
app.MapGet("/api/patients",
    (CareBridgeScaffoldContext db) =>
    {
        return db.Patients

                 // Select only columns we need.
                 .Select(p => new
                 {
                     p.PatientId,
                     p.FullName,
                     p.City,
                     p.IsActive
                 })

                 // Return only first 20 rows.
                 .Take(20)

                 // Execute query.
                 .ToList();
    });

app.MapGet("/api/patients/search",
    (CareBridgeScaffoldContext db, string? city, string? name) =>
    {
        var query = db.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(p => p.City == city);
        }
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p => p.FullName.Contains(name));
        }

        return query
            .OrderBy(p => p.FullName)
            .Select(p => new
            {
                p.PatientId,
                p.FullName,
                p.City,
                IsActive = true
            })
            .ToList();
    });

app.MapGet("/api/analytics/department-load",
    (CareBridgeScaffoldContext db, DateTime? fromDate) =>
    {
        var encounters = db.Encounters.AsQueryable();

        if (fromDate.HasValue)
        {
            encounters = encounters.Where(e => e.AdmitDate >= fromDate.Value);
        }

        var results =
            (
                from e in encounters

                join d in db.Departments
                    on e.DepartmentId equals d.DepartmentId

                group e by d.Name into g

                select new
                {
                    departmentName = g.Key,
                    inpatient = g.Count(x => x.EncounterType == "Inpatient"),
                    outpatient = g.Count(x => x.EncounterType == "Outpatient"),
                    ed = g.Count(x => x.EncounterType == "ED"),
                    total = g.Count()
                }
            )
            .OrderByDescending(x => x.total)
            .ToList();

        return results;
    });

app.Run();
