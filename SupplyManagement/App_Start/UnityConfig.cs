using Unity;
using SupplyManagement.Repositories;
using SupplyManagement.Contracts;
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
        container.RegisterType<IVendorRepository, VendorRepository>();


        DependencyResolver.SetResolver(new UnityDependencyResolver(container));
    }
}
