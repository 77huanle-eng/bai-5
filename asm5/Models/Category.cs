using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asm5.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên chủ đề không được để trống.")]
        [StringLength(150, ErrorMessage = "Tên chủ đề không được vượt quá 150 ký tự.")]
        [Display(Name = "Tên chủ đề")]
        public string CategoryName { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
