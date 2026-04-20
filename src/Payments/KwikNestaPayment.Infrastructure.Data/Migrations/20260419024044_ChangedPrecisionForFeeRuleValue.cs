using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaPayment.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPrecisionForFeeRuleValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                schema: "kn-payment-svc",
                table: "FeeRules",
                type: "numeric(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxCap",
                schema: "kn-payment-svc",
                table: "FeeRules",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,3)",
                oldPrecision: 18,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 40, 43, 286, DateTimeKind.Utc).AddTicks(8924));

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 40, 43, 286, DateTimeKind.Utc).AddTicks(8957));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                schema: "kn-payment-svc",
                table: "FeeRules",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxCap",
                schema: "kn-payment-svc",
                table: "FeeRules",
                type: "numeric(18,3)",
                precision: 18,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 39, 32, 212, DateTimeKind.Utc).AddTicks(7709));

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 39, 32, 212, DateTimeKind.Utc).AddTicks(7743));
        }
    }
}
