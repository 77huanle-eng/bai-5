using System.Collections.Generic;

namespace bai5.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }

        // Khởi tạo sẵn một List trống để tránh cảnh báo và lỗi null
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}