using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibraryData.Models.Base
{
    public class Entity: IEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public virtual string ToLiteText()
        {
            return Id.ToString();
        }
    }
}
