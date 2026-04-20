using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaPayment.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexToReferenceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"),
                columns: new[] { "CreatedOn", "MaxCap" },
                values: new object[] { new DateTime(2026, 4, 20, 21, 28, 1, 510, DateTimeKind.Utc).AddTicks(418), 8500m });

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 28, 1, 510, DateTimeKind.Utc).AddTicks(452));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Reference",
                schema: "kn-payment-svc",
                table: "Payments",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_Reference",
                schema: "kn-payment-svc",
                table: "Payments");

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704a"),
                columns: new[] { "CreatedOn", "MaxCap" },
                values: new object[] { new DateTime(2026, 4, 19, 2, 40, 43, 286, DateTimeKind.Utc).AddTicks(8924), null });

            migrationBuilder.UpdateData(
                schema: "kn-payment-svc",
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: new Guid("86724d52-6968-4b42-ba1d-897d14bd704b"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 40, 43, 286, DateTimeKind.Utc).AddTicks(8957));
        }
    }
}
