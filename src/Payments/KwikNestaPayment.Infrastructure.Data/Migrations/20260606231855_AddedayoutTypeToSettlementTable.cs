using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaPayment.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedayoutTypeToSettlementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartAfter",
                schema: "kn-payment-svc",
                table: "Settlements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "kn-payment-svc",
                table: "Settlements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BankCode",
                schema: "kn-payment-svc",
                table: "PayoutAccounts",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 18, 54, 246, DateTimeKind.Utc).AddTicks(4450));

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 18, 54, 246, DateTimeKind.Utc).AddTicks(4482));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartAfter",
                schema: "kn-payment-svc",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "kn-payment-svc",
                table: "Settlements");

            migrationBuilder.AlterColumn<string>(
                name: "BankCode",
                schema: "kn-payment-svc",
                table: "PayoutAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

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
        }
    }
}
