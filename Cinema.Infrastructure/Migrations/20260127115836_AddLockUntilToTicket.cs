using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace onlineCinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLockUntilToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LockUntil",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockUntil",
                table: "Tickets");
        }
    }
}
