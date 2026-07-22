using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainModel.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameTableSupportTicketToUpperS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_supportTickets_AspNetUsers_UserID",
                table: "supportTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_supportTickets",
                table: "supportTickets");

            migrationBuilder.RenameTable(
                name: "supportTickets",
                newName: "SupportTickets");

            migrationBuilder.RenameIndex(
                name: "IX_supportTickets_UserID",
                table: "SupportTickets",
                newName: "IX_SupportTickets_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportTickets",
                table: "SupportTickets",
                column: "SupportTicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTickets_AspNetUsers_UserID",
                table: "SupportTickets",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportTickets_AspNetUsers_UserID",
                table: "SupportTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportTickets",
                table: "SupportTickets");

            migrationBuilder.RenameTable(
                name: "SupportTickets",
                newName: "supportTickets");

            migrationBuilder.RenameIndex(
                name: "IX_SupportTickets_UserID",
                table: "supportTickets",
                newName: "IX_supportTickets_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_supportTickets",
                table: "supportTickets",
                column: "SupportTicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_supportTickets_AspNetUsers_UserID",
                table: "supportTickets",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
