using Microsoft.AspNetCore.Mvc;

namespace Bank_Application_Project.Controllers
{
    public class BankController : Controller
    {
        IDictionary<string,dynamic> AccountDetails = new Dictionary<string,dynamic>();
        public BankController()
        {
            AccountDetails.Add("accountNumber", 1001);
            AccountDetails.Add("accountHolderName", "Example Name");
            AccountDetails.Add("currentBalance", 5000);
        }


        [Route("/")]
        public IActionResult Home()
        {
            return Content("Welcome to the Best Bank", "text/plain");
        }


        [Route("account-details")]
        public IActionResult Index()
        {
            return Json(AccountDetails);
        }


        [Route("account-statement")]
        public IActionResult Index2()
        {
            return File("/7150-sdoula528.pdf", "application/pdf");
        }


        [Route("get-current-balance/{id?}")]
        public IActionResult Index3(int id)
        {
            var queryId = id;

            if (queryId == 1001)
            {
                return Content(Convert.ToString(AccountDetails["currentBalance"]), "text/plain");
            }

            if (queryId != 0 && queryId != 1001)
            {
                return Content("Account Number Should be 1001", "text/plain");
            }

            return Content("Account Number Should be Supplied", "text/plain");
        }
    }
}