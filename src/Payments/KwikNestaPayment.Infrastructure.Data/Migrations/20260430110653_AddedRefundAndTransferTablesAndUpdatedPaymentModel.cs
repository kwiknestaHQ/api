using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaPayment.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefundAndTransferTablesAndUpdatedPaymentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Provider",
                schema: "kn-payment-svc",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "Paystack");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "kn-payment-svc",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PayoutAccounts",
                schema: "kn-payment-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RecipientCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccountNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    BankCode = table.Column<string>(type: "text", nullable: false),
                    BankName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccountName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayoutAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Refunds",
                schema: "kn-payment-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ProviderReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExpectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Reference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                schema: "kn-payment-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TransferCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Recipient = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Reference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 30, 11, 6, 51, 653, DateTimeKind.Utc).AddTicks(8645));

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 30, 11, 6, 51, 653, DateTimeKind.Utc).AddTicks(8687));

            migrationBuilder.CreateIndex(
                name: "IX_PayoutAccounts_BankCode_AccountNumber",
                schema: "kn-payment-svc",
                table: "PayoutAccounts",
                columns: new[] { "BankCode", "AccountNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_Reference",
                schema: "kn-payment-svc",
                table: "Refunds",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_Reference",
                schema: "kn-payment-svc",
                table: "Transfers",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayoutAccounts",
                schema: "kn-payment-svc");

            migrationBuilder.DropTable(
                name: "Refunds",
                schema: "kn-payment-svc");

            migrationBuilder.DropTable(
                name: "Transfers",
                schema: "kn-payment-svc");

            migrationBuilder.DropColumn(
                name: "Provider",
                schema: "kn-payment-svc",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "kn-payment-svc",
                table: "Payments");

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 28, 1, 510, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 28, 1, 510, DateTimeKind.Utc).AddTicks(452));
        }
    }
}
