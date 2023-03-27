using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Migrations
{
    public partial class ContactOwnerEmailUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_OwnerId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_OwnerId",
                table: "Contact");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_OwnerId_Email",
                table: "Contact",
                columns: new[] { "OwnerId", "Email" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_OwnerId",
                table: "Contact",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_OwnerId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_OwnerId_Email",
                table: "Contact");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_OwnerId",
                table: "Contact",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_OwnerId",
                table: "Contact",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
