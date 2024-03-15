﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObsWebUI.Models.Entities
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        public int StudentId { get; set; }

        [ForeignKey("CourserId")]
        public int CourserId { get; set; }

        [Range(1999, 2030)]
        [Required(ErrorMessage = "This is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "This is required")]
        public string? Semester { get; set; }

    }
}
