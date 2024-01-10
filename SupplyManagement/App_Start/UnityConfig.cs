using Unity;
using SupplyManagement.Repositories; // Ganti dengan namespace yang sesuai
using SupplyManagement.Contracts; // Ganti dengan namespace yang sesuai
using System.Web.Mvc;
using Unity.AspNet.Mvc;
using SupplyManagement.Services;

public static class UnityConfig
{
    public static void RegisterComponents()
    {
        var container = new UnityContainer();

        container.RegisterType<IAccountRepository, AccountRepository>();
        container.RegisterType<AccountService>();
        container.RegisterType<ICompanyRepository, CompanyRepository>();
        container.RegisterType<CompanyService>();
        container.RegisterType<IRoleRepository, RoleRepository>();
        DependencyResolver.SetResolver(new UnityDependencyResolver(container));
    }
}
