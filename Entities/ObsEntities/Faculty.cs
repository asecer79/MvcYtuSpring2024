using Core.Repositories.CommonInterfaces;
using System.ComponentModel.DataAnnotations;

namespace Entities.ObsEntities
{
    public class Faculty : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This is required")]
        public string? Name { get; set; }

        public string? DeanName { get; set; }

    }
}
