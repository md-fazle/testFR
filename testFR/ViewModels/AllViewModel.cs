using testFR.Models;

namespace testFR.ViewModels
{
    public class AllViewModel
    {
        public IEnumerable<Subjects> Subjects { get; set; } = new List<Subjects>();
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
    }
}
