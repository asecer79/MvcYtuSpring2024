using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ObsEntities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This is required")]
        [ForeignKey("FacultyId")]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "This is required")]
        public string? Name { get; set; }
    }
}
