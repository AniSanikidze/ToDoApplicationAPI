using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Persistence.Migrations
{
    public partial class AddedSubtasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtask_ToDos_ToDoItemId",
                table: "Subtask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtask",
                table: "Subtask");

            migrationBuilder.RenameTable(
                name: "Subtask",
                newName: "Subtasks");

            migrationBuilder.RenameIndex(
                name: "IX_Subtask_ToDoItemId",
                table: "Subtasks",
                newName: "IX_Subtasks_ToDoItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtasks",
                table: "Subtasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtasks_ToDos_ToDoItemId",
                table: "Subtasks",
                column: "ToDoItemId",
                principalTable: "ToDos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtasks_ToDos_ToDoItemId",
                table: "Subtasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtasks",
                table: "Subtasks");

            migrationBuilder.RenameTable(
                name: "Subtasks",
                newName: "Subtask");

            migrationBuilder.RenameIndex(
                name: "IX_Subtasks_ToDoItemId",
                table: "Subtask",
                newName: "IX_Subtask_ToDoItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtask",
                table: "Subtask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtask_ToDos_ToDoItemId",
                table: "Subtask",
                column: "ToDoItemId",
                principalTable: "ToDos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
