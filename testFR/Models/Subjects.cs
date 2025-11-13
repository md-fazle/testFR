using System.ComponentModel.DataAnnotations;

namespace testFR.Models
{
    public class Subjects
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Subject ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Subject ID must be positive")]
        [Display(Name = "Subject ID")]
        public int Sub_id { get; set; }

        [Required(ErrorMessage = "Subject name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Subject name must be between 2 and 100 characters")]
        [Display(Name = "Subject Name")]
        public string Sub_Name { get; set; } = null!;

    }
}
