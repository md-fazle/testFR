using System.ComponentModel.DataAnnotations;

namespace testFR.Models
{
    public class StudentMarks
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Student ID must be between 1 and 20 characters")]
        public string S_id { get; set; } = null!;

        [Required(ErrorMessage = "Subject ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Subject ID must be positive")]
        public int Sub_id { get; set; }

        [Required(ErrorMessage = "Marks are required")]
         
        public string S_Mark { get; set; } = null!;
    }
}
