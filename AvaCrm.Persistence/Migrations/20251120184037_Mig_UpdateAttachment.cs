using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaCrm.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_UpdateAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownloadedFileName",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadedFileName",
                table: "Attachments");
        }
    }
}
