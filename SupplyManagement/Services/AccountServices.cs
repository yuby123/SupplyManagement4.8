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
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            
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

                    // Create an anonymous object to hold the login information
                    var loginInfo = new
                    {
                        UserId = userId,
                        UserRole = roleName,
                        StatusAccount = statusAccount,
                        
                    };

                    // Additional logic for role-specific redirects can be added here

                    if (roleName == "Admin")
                    {
                        return new
                        {
                            RedirectAction = "DashboardAdmin",
                            RedirectController = "Dashboard",
                            LoginInfo = loginInfo
                        };
                    }
                    else if (roleName == "Manager")
                    {
                        return new
                        {
                            RedirectAction = "DashboardManager",
                            RedirectController = "Dashboard",
                            LoginInfo = loginInfo
                        };
                    }
                    else if (roleName == "Vendor" && user.Status == StatusAccount.Requested)
                    {
                        return new
                        {
                            Notification = "Akun belum diapproved, silahkan menunggu! atau Silakan hubungi administrator!",
                            RedirectAction = "Login",
                            RedirectController = "Account"
                        };
                    }
                    // Add more role-specific conditions here
                }
                else
                {
                    return new
                    {
                        Error = "Email, password, atau status account tidak valid."
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return new
                {
                    Error = "An error occurred during login."
                };
            }

            // Add a default return statement in case none of the conditions are met
            return new
            {
                Error = "An unexpected error occurred during login."
            };
        }

    }
}
