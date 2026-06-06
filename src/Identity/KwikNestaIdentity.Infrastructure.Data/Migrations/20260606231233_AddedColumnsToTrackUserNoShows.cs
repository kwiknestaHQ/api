using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaIdentity.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsToTrackUserNoShows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastNoShowAt",
                schema: "kn-identity-svc",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoShowCount",
                schema: "kn-identity-svc",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastNoShowAt",
                schema: "kn-identity-svc",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NoShowCount",
                schema: "kn-identity-svc",
                table: "AspNetUsers");
        }
    }
}
