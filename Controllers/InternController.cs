    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Last_Try.Controllers
    {
        [Authorize(Roles = "Intern")]
        public class InternController : Controller
        {
            public IActionResult Dashboard()
            {
                return View();
            }
        }
    }


