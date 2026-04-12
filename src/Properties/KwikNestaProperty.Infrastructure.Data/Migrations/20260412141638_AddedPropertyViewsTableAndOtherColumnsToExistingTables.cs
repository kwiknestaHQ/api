using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaProperty.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyViewsTableAndOtherColumnsToExistingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeetingLink",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                schema: "kn-property-svc",
                table: "PropertyInquiries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PropertyViews",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    ViewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyViews_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PropertyViews_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyViews",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyViews",
                schema: "kn-property-svc");

            migrationBuilder.DropColumn(
                name: "MeetingLink",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.DropColumn(
                name: "Note",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.DropColumn(
                name: "Message",
                schema: "kn-property-svc",
                table: "PropertyInquiries");

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("00ed23d9-8f16-4444-83d3-9a114a8e4202"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7303));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01ed23d9-8f16-4444-83d3-9a114a8e4203"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7307));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("02ed23d9-8f16-4444-83d3-9a114a8e4204"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7314));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0bed23d9-8f16-4444-83d3-9a114a8e42f2"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7228));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1bed23d9-8f16-4444-83d3-9a114a8e42f1"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7223));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2bed23d9-8f16-4444-83d3-9a114a8e42f0"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3bed23d9-8f16-4444-83d3-9a114a8e42f9"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7216));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4bed23d9-8f16-4444-83d3-9a114a8e42f8"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7198));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5bed23d9-8f16-4444-83d3-9a114a8e42f7"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7194));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6bed23d9-8f16-4444-83d3-9a114a8e42f6"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7191));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7bed23d9-8f16-4444-83d3-9a114a8e42f5"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7187));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8bed23d9-8f16-4444-83d3-9a114a8e42f4"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7182));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9bed23d9-8f16-4444-83d3-9a114a8e42f3"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7139));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("abed23d9-8f16-4444-83d3-9a114a8e42fa"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7232));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7235));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7238));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("dbed23d9-8f16-4444-83d3-9a114a8e42fc"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7241));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e0ed23d9-8f16-4444-83d3-9a114a8e422e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e1ed23d9-8f16-4444-83d3-9a114a8e423e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e2ed23d9-8f16-4444-83d3-9a114a8e424e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e3ed23d9-8f16-4444-83d3-9a114a8e425e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7276));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e4ed23d9-8f16-4444-83d3-9a114a8e426e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7283));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e5ed23d9-8f16-4444-83d3-9a114a8e427e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7287));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e6ed23d9-8f16-4444-83d3-9a114a8e428e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7290));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e7ed23d9-8f16-4444-83d3-9a114a8e429e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7293));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e8ed23d9-8f16-4444-83d3-9a114a8e4200"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7297));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e9ed23d9-8f16-4444-83d3-9a114a8e4201"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7300));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ebed23d9-8f16-4444-83d3-9a114a8e42fd"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7250));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eced23d9-8f16-4444-83d3-9a114a8e42fe"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7253));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eded23d9-8f16-4444-83d3-9a114a8e420e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7257));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eeed23d9-8f16-4444-83d3-9a114a8e421e"),
                column: "CreatedOn",
                value: new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7261));
        }
    }
}
