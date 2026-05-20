namespace bai5.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Khóa ngoại liên kết tới bảng Grade
        public int GradeId { get; set; }
        
        // Thêm dấu ? và = default! để xóa cảnh báo màu vàng
        public Grade? Grade { get; set; } = default!;
    }
}