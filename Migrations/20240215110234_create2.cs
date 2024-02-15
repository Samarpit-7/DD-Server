using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DD_Server.Migrations
{
    /// <inheritdoc />
    public partial class create2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Audits_UId",
                table: "Audits",
                column: "UId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audits_Users_UId",
                table: "Audits",
                column: "UId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audits_Users_UId",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audits_UId",
                table: "Audits");
        }
    }
}
