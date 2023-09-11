using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_do_List_application.Migrations
{
    /// <inheritdoc />
    public partial class Ininial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    ToDoListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.ToDoListID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ToDoItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    ToDoListID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ToDoItemID);
                    table.ForeignKey(
                        name: "FK_Item_List_ToDoListID",
                        column: x => x.ToDoListID,
                        principalTable: "List",
                        principalColumn: "ToDoListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ToDoListID",
                table: "Item",
                column: "ToDoListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "List");
        }
    }
}
