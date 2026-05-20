using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bai5.Models;
using System.Threading.Tasks;

namespace bai5.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly AppDbContext _context;

        // Nhận DbContext thông qua Dependency Injection từ hệ thống
        public SinhVienController(AppDbContext context)
        {
            _context = context;
        }

        // Hàm xử lý khi người dùng truy cập vào đường dẫn /SinhVien
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách sinh viên đồng thời nạp kèm (Include) thông tin lớp học của sinh viên đó
            var danhSach = await _context.Students
                                         .Include(s => s.Grade)
                                         .ToListAsync();
            
            return View(danhSach);
        }
    }
}