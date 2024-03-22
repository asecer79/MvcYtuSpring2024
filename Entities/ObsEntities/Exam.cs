using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ObsEntities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CourseId")]
        [Required(ErrorMessage = "This is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "This is required")]
        public DateTime ExamDate { get; set; }
    }
}
