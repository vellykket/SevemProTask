using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SevenProTask.Models;

namespace SevenProTask.ViewModels
{
    public class CourseViewModel : IValidatableObject
    {
        public IEnumerable<Course> Courses { get; set; }
        [Required]
        [Display(Name = "Название курса")]
        public string CourseName { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "День")]
        public string Day { get; set; }
        [Required]
        [Display(Name = "Время начала")]
        public DateTime TimeBegin { get; set; }
        [Required]
        [Display(Name = "Время конца")]
        public DateTime TimeEnd { get; set; }
        public bool IsCourses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (TimeBegin >= TimeEnd)
                errors.Add(new ValidationResult("Не допустимое значение времени"));
            return errors;
        }
    }
}