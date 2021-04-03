using HomeLibraryData.Models.Base;

namespace HomeLibraryApplication.Validators.Base
{
    public interface IEntityValidator<T> where T: Entity
    {
        bool Validate(T entity);
    }
}
