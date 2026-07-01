using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;

namespace BankingApp.Controllers
{
    public class TransferController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        public TransferController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(decimal amount, string fromAccount, string toAccount)
        {
            _telemetryClient.TrackEvent("FundsTransferAttempt", new Dictionary<string, string>
            {
                { "FromAccount", fromAccount },
                { "ToAccount", toAccount },
                { "Amount", amount.ToString() }
            });

            if (amount > 10000)
            {
                _telemetryClient.TrackEvent("FundsTransferFailed", new Dictionary<string, string>
                {
                    { "Reason", "Amount exceeds limit" }
                });
                ViewBag.Message = "❌ Transfer failed: Amount exceeds limit of 10,000.";
                return View();
            }

            _telemetryClient.TrackEvent("FundsTransferSuccess", new Dictionary<string, string>
            {
                { "FromAccount", fromAccount },
                { "ToAccount", toAccount },
                { "Amount", amount.ToString() }
            });

            ViewBag.Message = $"✅ Successfully transferred {amount} from {fromAccount} to {toAccount}.";
            return View();
        }
    }
}
