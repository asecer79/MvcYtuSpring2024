using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObsWebUI.Models.Entities
{
    public class InstructorCourse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("InstructorId")]
        [Required(ErrorMessage = "This is required")]
        public int InstructorId { get; set; }

        [ForeignKey("CourseId")]
        [Required(ErrorMessage = "This is required")]
        public int CourseId { get; set; }

    }
}
