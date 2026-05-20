using Microsoft.EntityFrameworkCore;

namespace asm5.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Cuộc sống" },
                new Category { CategoryId = 2, CategoryName = "Lập trình" },
                new Category { CategoryId = 3, CategoryName = "Sức khỏe" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Cho tôi xin một vé đi tuổi thơ",
                    Author = "Nguyễn Nhật Ánh",
                    Price = 61600,
                    Description = "Cho tôi xin một vé đi tuổi thơ là một trong những tác phẩm thành công nhất của nhà văn Nguyễn Nhật Ánh. Cuốn sách mở ra một thế giới đầy trong trẻo, hồn nhiên của tuổi thơ và đồng thời cũng gợi lên những suy ngẫm sâu sắc cho người lớn về cách giáo dục và hiểu tâm lý con trẻ.",
                    Image = "cho-toi-xin-mot-ve-di-tuoi-tho.png",
                    CategoryId = 1
                },
                new Book
                {
                    BookId = 2,
                    Title = "Cuộc sống rất giống cuộc đời",
                    Author = "Hải Dở",
                    Price = 61000,
                    Description = "Đàn ông tuổi 18 mơ ước thành đàn ông tuổi 20, đàn ông tuổi 20 mơ ước thành đàn ông tuổi 30, đàn ông tuổi 30 mơ ước trở thành đàn ông tuổi 40 và đàn ông tuổi 40 lại mơ ước dứt chân lên cỗ máy thời gian để quay lại tuổi 20 với toàn bộ tài sản của mình! Vậy đấy!",
                    Image = "cuoc-song-rat-giong-cuoc-doi.png",
                    CategoryId = 1
                },
                new Book
                {
                    BookId = 3,
                    Title = "Core Java Fundamentals, Volume 1",
                    Author = "Cay S. Horstmann",
                    Price = 120000,
                    Description = "Cuốn sách cung cấp kiến thức nền tảng vững chắc về ngôn ngữ lập trình Java, bao gồm cú pháp cơ bản, hướng đối tượng, xử lý ngoại lệ, và các tính năng cốt lõi của JDK.",
                    Image = "core-java.png",
                    CategoryId = 2
                }
            );
        }
    }
}
