using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace asm5.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Cuộc sống" },
                    { 2, "Lập trình" },
                    { 3, "Sức khỏe" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CategoryId", "Description", "Image", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Nguyễn Nhật Ánh", 1, "Cho tôi xin một vé đi tuổi thơ là một trong những tác phẩm thành công nhất của nhà văn Nguyễn Nhật Ánh. Cuốn sách mở ra một thế giới đầy trong trẻo, hồn nhiên của tuổi thơ và đồng thời cũng gợi lên những suy ngẫm sâu sắc cho người lớn về cách giáo dục và hiểu tâm lý con trẻ.", "cho-toi-xin-mot-ve-di-tuoi-tho.png", 61600m, "Cho tôi xin một vé đi tuổi thơ" },
                    { 2, "Hải Dở", 1, "Đàn ông tuổi 18 mơ ước thành đàn ông tuổi 20, đàn ông tuổi 20 mơ ước thành đàn ông tuổi 30, đàn ông tuổi 30 mơ ước trở thành đàn ông tuổi 40 và đàn ông tuổi 40 lại mơ ước dứt chân lên cỗ máy thời gian để quay lại tuổi 20 với toàn bộ tài sản của mình! Vậy đấy!", "cuoc-song-rat-giong-cuoc-doi.png", 61000m, "Cuộc sống rất giống cuộc đời" },
                    { 3, "Cay S. Horstmann", 2, "Cuốn sách cung cấp kiến thức nền tảng vững chắc về ngôn ngữ lập trình Java, bao gồm cú pháp cơ bản, hướng đối tượng, xử lý ngoại lệ, và các tính năng cốt lõi của JDK.", "core-java.png", 120000m, "Core Java Fundamentals, Volume 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
