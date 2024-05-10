using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Business.AuthorizationServices.Abstract;
using Business.AuthorizationServices.Concrete;
using Business.CommonServices.Abstract;
using Business.CommonServices.Concrete;
using Business.Services.Obs.Abstract;
using Business.Services.Obs.Concrete;
using Caching.Abstract;
using Caching.Concrete;
using DataAccess.Dal.Abstract;
using DataAccess.Dal.Concrete;

namespace Business.Services.Obs.DependencyResolver
{
    public class ObsDependencyModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FacultyDal>().As<IFacultyDal>();
            builder.RegisterType<DepartmentDal>().As<IDepartmentDal>();
            builder.RegisterType<FacultyService>().As<IFacultyService>();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>();
            builder.RegisterType<UserDal>().As<IUserDal>();
            builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>();
            builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<OperationClaimService>().As<IOperationClaimService>();
            builder.RegisterType<UserOperationClaimService>().As<IUserOperationClaimService>();
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<MemoryCacheProvider>().As<ICacheProvider>();
            //builder.RegisterType<RedisCacheProvider>().As<ICacheProvider>();

        }
    }
}
