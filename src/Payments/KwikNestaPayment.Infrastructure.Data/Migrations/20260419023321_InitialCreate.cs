using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KwikNestaPayment.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kn-payment-svc");

            migrationBuilder.CreateTable(
                name: "FeeRules",
                schema: "kn-payment-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CalculationType = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AppliesTo = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MinCap = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    MaxCap = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "kn-payment-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Reference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PlatformFee = table.Column<decimal>(type: "numeric", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Purpose = table.Column<string>(type: "text", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                schema: "kn-payment-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeneficiaryId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Purpose = table.Column<string>(type: "text", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SettledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                columns: new[] { "Id", "AppliesTo", "CalculationType", "CreatedOn", "EndsAt", "IsActive", "IsDeprecated", "LastUpdatedOn", "MaxCap", "MinCap", "Name", "Priority", "StartsAt", "Type", "Value" },
                values: new object[,]
                {
                    { new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"), "Viewing", "Percentage", new DateTime(2026, 4, 19, 2, 33, 20, 73, DateTimeKind.Utc).AddTicks(6930), null, true, false, null, null, 2000m, "Viewing Fee", 1, null, "Charge", 0.007m },
                    { new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"), "Rent", "Percentage", new DateTime(2026, 4, 19, 2, 33, 20, 73, DateTimeKind.Utc).AddTicks(6976), null, true, false, null, null, null, "Rent Commission", 1, null, "PlatformFee", 0.05m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeeRules_IsActive_AppliesTo_StartsAt_EndsAt_Priority",
                schema: "kn-payment-svc",
                table: "FeeRules",
                columns: new[] { "IsActive", "AppliesTo", "StartsAt", "EndsAt", "Priority" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeeRules",
                schema: "kn-payment-svc");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "kn-payment-svc");

            migrationBuilder.DropTable(
                name: "Settlements",
                schema: "kn-payment-svc");
        }
    }
}
