using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppAuthors_AuthorId",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_AuthorId",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "AppBooks");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_Name",
                table: "AppBooks",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppAuthors_Id",
                table: "AppBooks",
                column: "Id",
                principalTable: "AppAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppAuthors_Id",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_Name",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppBooks");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "AppBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_AuthorId",
                table: "AppBooks",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppAuthors_AuthorId",
                table: "AppBooks",
                column: "AuthorId",
                principalTable: "AppAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
