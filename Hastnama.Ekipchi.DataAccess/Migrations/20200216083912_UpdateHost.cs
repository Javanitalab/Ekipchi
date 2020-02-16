using Microsoft.EntityFrameworkCore.Migrations;

namespace Hastnama.Ekipchi.DataAccess.Migrations
{
    public partial class UpdateHost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventGalleries_EventGallery_EventId",
                table: "EventGalleries");

            migrationBuilder.AddColumn<string>(
                name: "CoverPhoto",
                table: "Hosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventGalleries_EventGallery_EventId",
                table: "EventGalleries",
                column: "EventGallery_EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventGalleries_EventGallery_EventId",
                table: "EventGalleries");

            migrationBuilder.DropColumn(
                name: "CoverPhoto",
                table: "Hosts");

            migrationBuilder.CreateIndex(
                name: "IX_EventGalleries_EventGallery_EventId",
                table: "EventGalleries",
                column: "EventGallery_EventId",
                unique: true);
        }
    }
}
