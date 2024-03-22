using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ObsEntities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("DepartmentId")]
        [Required(ErrorMessage = "This is required")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "This is required")]
        public string? Name { get; set; }
    }
}
