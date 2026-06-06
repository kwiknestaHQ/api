using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaProperty.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUidToParticipantAndSettlementStatusToViewSessionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionParticipants_ViewingSessionId",
                schema: "kn-property-svc",
                table: "SessionParticipants");

            migrationBuilder.AddColumn<string>(
                name: "SetlementStatus",
                schema: "kn-property-svc",
                table: "ViewingSessions",
                type: "text",
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.AlterColumn<long>(
                name: "DurationSeconds",
                schema: "kn-property-svc",
                table: "SessionParticipants",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "UId",
                schema: "kn-property-svc",
                table: "SessionParticipants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("00ed23d9-8f16-4444-83d3-9a114a8e4202"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01ed23d9-8f16-4444-83d3-9a114a8e4203"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7599));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("02ed23d9-8f16-4444-83d3-9a114a8e4204"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7603));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0bed23d9-8f16-4444-83d3-9a114a8e42f2"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7554));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1bed23d9-8f16-4444-83d3-9a114a8e42f1"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7553));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2bed23d9-8f16-4444-83d3-9a114a8e42f0"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7550));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3bed23d9-8f16-4444-83d3-9a114a8e42f9"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7548));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4bed23d9-8f16-4444-83d3-9a114a8e42f8"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7536));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5bed23d9-8f16-4444-83d3-9a114a8e42f7"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7535));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6bed23d9-8f16-4444-83d3-9a114a8e42f6"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7533));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7bed23d9-8f16-4444-83d3-9a114a8e42f5"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7531));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8bed23d9-8f16-4444-83d3-9a114a8e42f4"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7528));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9bed23d9-8f16-4444-83d3-9a114a8e42f3"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7500));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("abed23d9-8f16-4444-83d3-9a114a8e42fa"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7556));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7558));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7560));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("dbed23d9-8f16-4444-83d3-9a114a8e42fc"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7562));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e0ed23d9-8f16-4444-83d3-9a114a8e422e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7578));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e1ed23d9-8f16-4444-83d3-9a114a8e423e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7580));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e2ed23d9-8f16-4444-83d3-9a114a8e424e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7582));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e3ed23d9-8f16-4444-83d3-9a114a8e425e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e4ed23d9-8f16-4444-83d3-9a114a8e426e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7587));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e5ed23d9-8f16-4444-83d3-9a114a8e427e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7589));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e6ed23d9-8f16-4444-83d3-9a114a8e428e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7590));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e7ed23d9-8f16-4444-83d3-9a114a8e429e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7592));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e8ed23d9-8f16-4444-83d3-9a114a8e4200"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e9ed23d9-8f16-4444-83d3-9a114a8e4201"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7596));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ebed23d9-8f16-4444-83d3-9a114a8e42fd"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7568));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eced23d9-8f16-4444-83d3-9a114a8e42fe"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7570));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eded23d9-8f16-4444-83d3-9a114a8e420e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7571));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eeed23d9-8f16-4444-83d3-9a114a8e421e"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 6, 23, 22, 49, 201, DateTimeKind.Utc).AddTicks(7573));

            migrationBuilder.CreateIndex(
                name: "IX_ViewingSessions_ChannelName",
                schema: "kn-property-svc",
                table: "ViewingSessions",
                column: "ChannelName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionParticipants_ViewingSessionId_UId",
                schema: "kn-property-svc",
                table: "SessionParticipants",
                columns: new[] { "ViewingSessionId", "UId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ViewingSessions_ChannelName",
                schema: "kn-property-svc",
                table: "ViewingSessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionParticipants_ViewingSessionId_UId",
                schema: "kn-property-svc",
                table: "SessionParticipants");

            migrationBuilder.DropColumn(
                name: "SetlementStatus",
                schema: "kn-property-svc",
                table: "ViewingSessions");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "kn-property-svc",
                table: "SessionParticipants");

            migrationBuilder.AlterColumn<int>(
                name: "DurationSeconds",
                schema: "kn-property-svc",
                table: "SessionParticipants",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("00ed23d9-8f16-4444-83d3-9a114a8e4202"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(114));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01ed23d9-8f16-4444-83d3-9a114a8e4203"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(118));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("02ed23d9-8f16-4444-83d3-9a114a8e4204"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(123));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0bed23d9-8f16-4444-83d3-9a114a8e42f2"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(27));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1bed23d9-8f16-4444-83d3-9a114a8e42f1"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(24));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2bed23d9-8f16-4444-83d3-9a114a8e42f0"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(18));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3bed23d9-8f16-4444-83d3-9a114a8e42f9"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 690, DateTimeKind.Utc).AddTicks(9997));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4bed23d9-8f16-4444-83d3-9a114a8e42f8"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 690, DateTimeKind.Utc).AddTicks(9993));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5bed23d9-8f16-4444-83d3-9a114a8e42f7"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 690, DateTimeKind.Utc).AddTicks(9989));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6bed23d9-8f16-4444-83d3-9a114a8e42f6"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 690, DateTimeKind.Utc).AddTicks(9984));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7bed23d9-8f16-4444-83d3-9a114a8e42f5"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 690, DateTimeKind.Utc).AddTicks(9979));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8bed23d9-8f16-4444-83d3-9a114a8e42f4"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 690, DateTimeKind.Utc).AddTicks(9974));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9bed23d9-8f16-4444-83d3-9a114a8e42f3"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 690, DateTimeKind.Utc).AddTicks(9927));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("abed23d9-8f16-4444-83d3-9a114a8e42fa"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(32));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(36));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(40));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("dbed23d9-8f16-4444-83d3-9a114a8e42fc"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(44));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e0ed23d9-8f16-4444-83d3-9a114a8e422e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(69));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e1ed23d9-8f16-4444-83d3-9a114a8e423e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(73));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e2ed23d9-8f16-4444-83d3-9a114a8e424e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(77));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e3ed23d9-8f16-4444-83d3-9a114a8e425e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(81));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e4ed23d9-8f16-4444-83d3-9a114a8e426e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(85));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e5ed23d9-8f16-4444-83d3-9a114a8e427e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(93));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e6ed23d9-8f16-4444-83d3-9a114a8e428e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(97));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e7ed23d9-8f16-4444-83d3-9a114a8e429e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(101));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e8ed23d9-8f16-4444-83d3-9a114a8e4200"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(105));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e9ed23d9-8f16-4444-83d3-9a114a8e4201"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(110));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ebed23d9-8f16-4444-83d3-9a114a8e42fd"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(47));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eced23d9-8f16-4444-83d3-9a114a8e42fe"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(56));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eded23d9-8f16-4444-83d3-9a114a8e420e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(60));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eeed23d9-8f16-4444-83d3-9a114a8e421e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 20, 21, 54, 11, 691, DateTimeKind.Utc).AddTicks(65));

            migrationBuilder.CreateIndex(
                name: "IX_SessionParticipants_ViewingSessionId",
                schema: "kn-property-svc",
                table: "SessionParticipants",
                column: "ViewingSessionId");
        }
    }
}
