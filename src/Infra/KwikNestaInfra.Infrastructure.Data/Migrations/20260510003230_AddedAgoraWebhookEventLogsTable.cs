using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaInfra.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAgoraWebhookEventLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgoraWebhookEvents",
                schema: "kn-infra-svc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Channel = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Module = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UId = table.Column<long>(type: "bigint", nullable: true),
                    Platform = table.Column<string>(type: "text", nullable: false, defaultValue: "Other"),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    Reason = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgoraWebhookEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgoraWebhookEvents_Channel",
                schema: "kn-infra-svc",
                table: "AgoraWebhookEvents",
                column: "Channel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgoraWebhookEvents",
                schema: "kn-infra-svc");
        }
    }
}
