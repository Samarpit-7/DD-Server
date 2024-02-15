using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DD_Server.Migrations
{
    /// <inheritdoc />
    public partial class create1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DId = table.Column<Guid>(type: "uuid", nullable: false),
                    Container = table.Column<string>(type: "text", nullable: true),
                    DataPoint = table.Column<string>(type: "text", nullable: true),
                    DbColumnName = table.Column<string>(type: "text", nullable: true),
                    FieldType = table.Column<string>(type: "text", nullable: true),
                    DbDataType = table.Column<string>(type: "text", nullable: true),
                    Definition = table.Column<string>(type: "text", nullable: true),
                    PossibleValues = table.Column<string[]>(type: "text[]", nullable: true),
                    Synonyms = table.Column<string[]>(type: "text[]", nullable: true),
                    CalculatedInfo = table.Column<string>(type: "text", nullable: true),
                    UId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Container = table.Column<string>(type: "text", nullable: true),
                    DataPoint = table.Column<string>(type: "text", nullable: true),
                    DbColumnName = table.Column<string>(type: "text", nullable: true),
                    FieldType = table.Column<string>(type: "text", nullable: true),
                    DbDataType = table.Column<string>(type: "text", nullable: true),
                    Definition = table.Column<string>(type: "text", nullable: true),
                    PossibleValues = table.Column<string[]>(type: "text[]", nullable: true),
                    Synonyms = table.Column<string[]>(type: "text[]", nullable: true),
                    CalculatedInfo = table.Column<string>(type: "text", nullable: true),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Container = table.Column<string>(type: "text", nullable: true),
                    DataPoint = table.Column<string>(type: "text", nullable: true),
                    DbColumnName = table.Column<string>(type: "text", nullable: true),
                    FieldType = table.Column<string>(type: "text", nullable: true),
                    DbDataType = table.Column<string>(type: "text", nullable: true),
                    Definition = table.Column<string>(type: "text", nullable: true),
                    PossibleValues = table.Column<string[]>(type: "text[]", nullable: true),
                    Synonyms = table.Column<string[]>(type: "text[]", nullable: true),
                    CalculatedInfo = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DId = table.Column<Guid>(type: "uuid", nullable: false),
                    UId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Users_UId",
                        column: x => x.UId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UId",
                table: "Requests",
                column: "UId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "Dictionary");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
