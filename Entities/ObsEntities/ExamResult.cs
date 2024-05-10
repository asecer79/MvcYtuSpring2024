using Core.Repositories.CommonInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ObsEntities
{
    public class ExamResult : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ExamId")]
        [Required(ErrorMessage = "This is required")]
        public int ExamId { get; set; }

        [ForeignKey("StudentId")]
        [Required(ErrorMessage = "This is required")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "This is required")]
        [Range(0,100, ErrorMessage = "The value should be between 0 and 100.")]
        public double Grade { get; set; }

    }
}
