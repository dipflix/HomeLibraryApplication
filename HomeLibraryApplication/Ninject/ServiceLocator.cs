using HomeLibraryApplication.ViewModels;
using HomeLibraryApplication.ViewModels.Base;
using Ninject;

namespace HomeLibraryApplication.Ninject
{
    public class ServiceLocator
    {
        private IKernel _kernel;
        public ServiceLocator()
        {
            _kernel = new StandardKernel(new ServiceRegistrator());
        }

        public ApplicationVM AppVM
        {
            get => _kernel.Get<ApplicationVM>();
        }

        public FormContextBaseVM FormContextVM
        {
            get => _kernel.Get<FormContextBaseVM>();
                }

    }
}
