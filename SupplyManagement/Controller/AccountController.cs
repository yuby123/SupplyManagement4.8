using SupplyManagement.Contracts;
using SupplyManagement.DTOs.Accounts;
using SupplyManagement.Services;
using SupplyManagement.Utilities.Enums;
using System;
using System.Web.Mvc;

namespace SupplyManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly IAccountRepository _accountRepository;

        public AccountController(AccountService accountService, IAccountRepository accountRepository)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _accountRepository = accountRepository;
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

                if (loginResult is object result && !(result is null))
                {
                    dynamic dynamicResult = result;

                    if (GetProperty(dynamicResult, "Error") == null)
                    {
                        SetSessionInformation(dynamicResult.LoginInfo);
                        

                        if (!string.IsNullOrEmpty(dynamicResult.RedirectAction) && !string.IsNullOrEmpty(dynamicResult.RedirectController))
                        {
                            if (GetProperty(dynamicResult, "Notification") != null && !string.IsNullOrEmpty(dynamicResult.Notification))
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
                        ModelState.AddModelError("", (GetProperty(dynamicResult, "Error") as string) ?? "An unexpected error occurred during login.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "An unexpected error occurred during login.");
                }
            }

            return View(model);
        }

        private object GetProperty(dynamic obj, string propertyName)
        {
            try
            {
                return obj.GetType().GetProperty(propertyName)?.GetValue(obj);
            }
            catch
            {
                return null;
            }
        }


        private void SetSessionInformation(dynamic loginInfo)
        {
            Session["UserId"] = loginInfo.UserId.ToString();
            Session["UserRole"] = loginInfo.UserRole.ToString();
            Session["StatusAccount"] = loginInfo.StatusAccount.ToString();
        }

        [HttpPost]
        public ActionResult UpdateAccountStatus(Guid accountGuid, StatusAccount newStatus)
        {
            try
            {
                var accountToUpdate = _accountRepository.GetAccountByGuid(accountGuid);

                if (accountToUpdate != null)
                {
                    accountToUpdate.Status = newStatus;
                    _accountRepository.UpdateAccountStatus(accountToUpdate);
                    TempData["SuccessMessage"] = "Account status updated successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Account not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction("CompanyRequested", "Company");
        }
    }
}
