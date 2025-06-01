using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class General_updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastMessageId",
                table: "Conversations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_LastMessageId",
                table: "Conversations",
                column: "LastMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Messages_LastMessageId",
                table: "Conversations",
                column: "LastMessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Messages_LastMessageId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_LastMessageId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "LastMessageId",
                table: "Conversations");
        }
    }
}
