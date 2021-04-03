using HomeLibraryApplication.ViewModels;
using HomeLibraryApplication.ViewModels.Base;
using Ninject;

namespace HomeLibraryApplication.Ninject
{
    public class ServiceLocator
    {
        private static IKernel _kernel;

        public ServiceLocator()
        {
            _kernel = new StandardKernel(new ServiceRegistrator());
        }

        public ApplicationVM AppVM
        {
            get => _kernel.Get<ApplicationVM>();
        }


    }
}
