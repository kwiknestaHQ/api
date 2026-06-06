using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaInfra.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAgoraTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Duration",
                schema: "kn-infra-svc",
                table: "AgoraWebhookEvents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AgoraTokens",
                schema: "kn-infra-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelName = table.Column<string>(type: "text", nullable: false),
                    Account = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgoraTokens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgoraTokens_ChannelName",
                schema: "kn-infra-svc",
                table: "AgoraTokens",
                column: "ChannelName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgoraTokens",
                schema: "kn-infra-svc");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                schema: "kn-infra-svc",
                table: "AgoraWebhookEvents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
