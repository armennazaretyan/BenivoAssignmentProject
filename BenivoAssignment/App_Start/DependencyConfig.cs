using BusinessLayer.InfoServices;
using BusinessLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BenivoAssignment.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Bind<IStoryService>().To<StoryService>().WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Bind<IGroupService>().To<GroupService>().WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Inject(Membership.Provider);
        }
    }
}