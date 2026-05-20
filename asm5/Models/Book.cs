using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asm5.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống.")]
        [StringLength(150, ErrorMessage = "Tên sách không được vượt quá 150 ký tự.")]
        [Display(Name = "Tên sách")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tác giả không được để trống.")]
        [StringLength(150, ErrorMessage = "Tác giả không được vượt quá 150 ký tự.")]
        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá bán không được để trống.")]
        [Range(0, 1000000000, ErrorMessage = "Giá bán phải là số dương lớn hơn hoặc bằng 0.")]
        [Column(TypeName = "decimal(18,0)")]
        [Display(Name = "Giá bán")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        [Display(Name = "Mô tả chi tiết")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn hình ảnh.")]
        [StringLength(100, ErrorMessage = "Tên tệp hình ảnh không được vượt quá 100 ký tự.")]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn chủ đề.")]
        [Display(Name = "Chủ đề")]
        public int CategoryId { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        [Display(Name = "Chủ đề")]
        public virtual Category? Category { get; set; }
    }
}
