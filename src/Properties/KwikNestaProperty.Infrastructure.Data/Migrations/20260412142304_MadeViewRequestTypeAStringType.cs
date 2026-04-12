using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaProperty.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MadeViewRequestTypeAStringType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("00ed23d9-8f16-4444-83d3-9a114a8e4202"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2996));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01ed23d9-8f16-4444-83d3-9a114a8e4203"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(3000));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("02ed23d9-8f16-4444-83d3-9a114a8e4204"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(3131));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0bed23d9-8f16-4444-83d3-9a114a8e42f2"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2918));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1bed23d9-8f16-4444-83d3-9a114a8e42f1"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2914));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2bed23d9-8f16-4444-83d3-9a114a8e42f0"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2911));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3bed23d9-8f16-4444-83d3-9a114a8e42f9"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2907));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4bed23d9-8f16-4444-83d3-9a114a8e42f8"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2889));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5bed23d9-8f16-4444-83d3-9a114a8e42f7"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2885));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6bed23d9-8f16-4444-83d3-9a114a8e42f6"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2881));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7bed23d9-8f16-4444-83d3-9a114a8e42f5"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2877));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8bed23d9-8f16-4444-83d3-9a114a8e42f4"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2872));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9bed23d9-8f16-4444-83d3-9a114a8e42f3"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2825));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("abed23d9-8f16-4444-83d3-9a114a8e42fa"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2921));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2925));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2929));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("dbed23d9-8f16-4444-83d3-9a114a8e42fc"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2932));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e0ed23d9-8f16-4444-83d3-9a114a8e422e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2955));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e1ed23d9-8f16-4444-83d3-9a114a8e423e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2958));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e2ed23d9-8f16-4444-83d3-9a114a8e424e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2961));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e3ed23d9-8f16-4444-83d3-9a114a8e425e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2965));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e4ed23d9-8f16-4444-83d3-9a114a8e426e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2974));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e5ed23d9-8f16-4444-83d3-9a114a8e427e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2978));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e6ed23d9-8f16-4444-83d3-9a114a8e428e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2982));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e7ed23d9-8f16-4444-83d3-9a114a8e429e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2985));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e8ed23d9-8f16-4444-83d3-9a114a8e4200"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2989));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e9ed23d9-8f16-4444-83d3-9a114a8e4201"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2992));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ebed23d9-8f16-4444-83d3-9a114a8e42fd"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2940));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eced23d9-8f16-4444-83d3-9a114a8e42fe"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2944));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eded23d9-8f16-4444-83d3-9a114a8e420e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2948));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eeed23d9-8f16-4444-83d3-9a114a8e421e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 23, 3, 820, DateTimeKind.Utc).AddTicks(2951));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("00ed23d9-8f16-4444-83d3-9a114a8e4202"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9346));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01ed23d9-8f16-4444-83d3-9a114a8e4203"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9349));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("02ed23d9-8f16-4444-83d3-9a114a8e4204"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9357));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0bed23d9-8f16-4444-83d3-9a114a8e42f2"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9248));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1bed23d9-8f16-4444-83d3-9a114a8e42f1"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9242));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2bed23d9-8f16-4444-83d3-9a114a8e42f0"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9232));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3bed23d9-8f16-4444-83d3-9a114a8e42f9"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9225));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4bed23d9-8f16-4444-83d3-9a114a8e42f8"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5bed23d9-8f16-4444-83d3-9a114a8e42f7"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9194));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6bed23d9-8f16-4444-83d3-9a114a8e42f6"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9187));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7bed23d9-8f16-4444-83d3-9a114a8e42f5"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9179));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8bed23d9-8f16-4444-83d3-9a114a8e42f4"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9163));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9bed23d9-8f16-4444-83d3-9a114a8e42f3"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9074));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("abed23d9-8f16-4444-83d3-9a114a8e42fa"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9255));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9263));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9268));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("dbed23d9-8f16-4444-83d3-9a114a8e42fc"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9274));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e0ed23d9-8f16-4444-83d3-9a114a8e422e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9306));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e1ed23d9-8f16-4444-83d3-9a114a8e423e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9309));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e2ed23d9-8f16-4444-83d3-9a114a8e424e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9313));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e3ed23d9-8f16-4444-83d3-9a114a8e425e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9317));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e4ed23d9-8f16-4444-83d3-9a114a8e426e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9324));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e5ed23d9-8f16-4444-83d3-9a114a8e427e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9328));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e6ed23d9-8f16-4444-83d3-9a114a8e428e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9332));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e7ed23d9-8f16-4444-83d3-9a114a8e429e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9335));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e8ed23d9-8f16-4444-83d3-9a114a8e4200"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9339));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e9ed23d9-8f16-4444-83d3-9a114a8e4201"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9342));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ebed23d9-8f16-4444-83d3-9a114a8e42fd"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9288));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eced23d9-8f16-4444-83d3-9a114a8e42fe"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9294));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eded23d9-8f16-4444-83d3-9a114a8e420e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9298));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eeed23d9-8f16-4444-83d3-9a114a8e421e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 12, 14, 16, 37, 92, DateTimeKind.Utc).AddTicks(9302));
        }
    }
}
