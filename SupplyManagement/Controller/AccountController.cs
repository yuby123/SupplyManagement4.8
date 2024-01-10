using SupplyManagement.DTOs.Accounts;
using SupplyManagement.Services;
using System.Web.Mvc;

namespace SupplyManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = _accountService.Login(model);

                if (loginResult is dynamic)
                {
                    dynamic dynamicResult = loginResult;

                    if (dynamicResult.Error == null)
                    {
                        // Set session information
                        Session["UserId"] = dynamicResult.LoginInfo.UserId.ToString();
                        Session["UserRole"] = dynamicResult.LoginInfo.UserRole.ToString();
                        Session["statusAccount"] = dynamicResult.LoginInfo.StatusAccount.ToString();
                        Session["statusVendor"] = dynamicResult.LoginInfo.StatusVendor.ToString();

                        if (!string.IsNullOrEmpty(dynamicResult.RedirectAction) && !string.IsNullOrEmpty(dynamicResult.RedirectController))
                        {
                            if (!string.IsNullOrEmpty(dynamicResult.Notification))
                            {
                                TempData["Notification"] = dynamicResult.Notification;
                            }
                            return RedirectToAction(dynamicResult.RedirectAction, dynamicResult.RedirectController);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid redirect configuration.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", dynamicResult.Error);
                    }
                }
            }

            return View(model);
        }
    }
}
