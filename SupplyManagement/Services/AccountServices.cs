using SupplyManagement.Contracts;
using SupplyManagement.DTOs.Accounts;
using SupplyManagement.Models;
using SupplyManagement.Utilities.Enums;
using SupplyManagement.Utilities.Handler;
using System;

namespace SupplyManagement.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountService(IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public object Login(LoginDto model)
        {
            try
            {
                var user = _accountRepository.GetByCompanyEmail(model.Email);

                if (user != null && HashHandler.VerifyPassword(model.Password, user.Password))
                {
                    var userId = user.Guid;
                    var roleName = _roleRepository.GetRoleNameByGuid(user.RoleGuid);
                    var statusAccount = user.Status;

                    var loginInfo = new
                    {
                        UserId = userId,
                        UserRole = roleName,
                        StatusAccount = statusAccount,
                    };

                    return GetRedirectInfo(roleName, statusAccount, loginInfo);
                }
                else
                {
                    return new
                    {
                        Error = "Email, password, or account status is not valid.",
                        RedirectAction = (string)null,
                        RedirectController = (string)null,
                        Notification = (string)null,
                        LoginInfo = (object)null,
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return new
                {
                    Error = "An error occurred during login.",
                    RedirectAction = (string)null,
                    RedirectController = (string)null,
                    Notification = (string)null,
                    LoginInfo = (object)null,
                };
            }

            return new
            {
                Error = "An unexpected error occurred during login.",
                RedirectAction = (string)null,
                RedirectController = (string)null,
                Notification = (string)null,
                LoginInfo = (object)null,
            };

            return new
            {
                Error = "An unexpected error occurred during login."
            };
        }

        private object GetRedirectInfo(string roleName, StatusAccount statusAccount, dynamic loginInfo)
        {
            if (roleName == "admin")
            {
                return new
                {
                    RedirectAction = "CompanyRequested",
                    RedirectController = "Company",
                    LoginInfo = loginInfo
                };
            }
            else if (roleName == "manager")
            {
                return new
                {
                    RedirectAction = "DashboardManager",
                    RedirectController = "Dashboard",
                    LoginInfo = loginInfo
                };
            }
            else if (roleName == "company" && statusAccount == StatusAccount.Approved)
            {
                return new
                {
                    RedirectAction = "VendorList",
                    RedirectController = "Vendor",
                    LoginInfo = loginInfo
                };
            }
            else if (roleName == "company" && statusAccount == StatusAccount.Requested)
            {
                return new
                {
                    Notification = "Account not approved yet. Please wait or contact the administrator!",
                    RedirectAction = "Login",
                    RedirectController = "Account"
                };
            }
            else if (roleName == "company" && statusAccount == StatusAccount.Rejected)
            {
                return new
                {
                    Notification = "Account Rejected. Please wait or contact the administrator!",
                    RedirectAction = "Login",
                    RedirectController = "Account"
                };
            }
            else if (roleName == "company" && statusAccount == StatusAccount.Canceled)
            {
                return new
                {
                    Notification = "Account Canceled. Please wait or contact the administrator!",
                    RedirectAction = "Login",
                    RedirectController = "Account"
                };
            }

            return new
            {
                Error = "Invalid role for redirection."
            };
        }
    }
}
