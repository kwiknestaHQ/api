using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikNestaProperty.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeesionAndParticipantsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeatureLink_Properties_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeatureLink_PropertyFeatures_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyFeatureLink",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink");

            migrationBuilder.DropColumn(
                name: "MeetingLink",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.RenameTable(
                name: "PropertyFeatureLink",
                schema: "kn-property-svc",
                newName: "PropertyFeatureLinks",
                newSchema: "kn-property-svc");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLink_PropertyId_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                newName: "IX_PropertyFeatureLinks_PropertyId_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLink_PropertyId_CustomFeatureNormalized",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                newName: "IX_PropertyFeatureLinks_PropertyId_CustomFeatureNormalized");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLink_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                newName: "IX_PropertyFeatureLinks_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLink_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                newName: "IX_PropertyFeatureLinks_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLink_CustomFeatureNormalized",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                newName: "IX_PropertyFeatureLinks_CustomFeatureNormalized");

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ViewingSessionId",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyFeatureLinks",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ViewingSessions",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewingRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ScheduledStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DurationSeconds = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewingSessions_ViewingRequests_ViewingRequestId",
                        column: x => x.ViewingRequestId,
                        principalSchema: "kn-property-svc",
                        principalTable: "ViewingRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionParticipants",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewingSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LeftAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DurationSeconds = table.Column<int>(type: "integer", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionParticipants_ViewingSessions_ViewingSessionId",
                        column: x => x.ViewingSessionId,
                        principalSchema: "kn-property-svc",
                        principalTable: "ViewingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("00ed23d9-8f16-4444-83d3-9a114a8e4202"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(838));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01ed23d9-8f16-4444-83d3-9a114a8e4203"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(841));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("02ed23d9-8f16-4444-83d3-9a114a8e4204"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0bed23d9-8f16-4444-83d3-9a114a8e42f2"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(760));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1bed23d9-8f16-4444-83d3-9a114a8e42f1"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(756));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2bed23d9-8f16-4444-83d3-9a114a8e42f0"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(753));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3bed23d9-8f16-4444-83d3-9a114a8e42f9"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(748));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4bed23d9-8f16-4444-83d3-9a114a8e42f8"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(725));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5bed23d9-8f16-4444-83d3-9a114a8e42f7"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(722));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6bed23d9-8f16-4444-83d3-9a114a8e42f6"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(718));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7bed23d9-8f16-4444-83d3-9a114a8e42f5"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(713));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8bed23d9-8f16-4444-83d3-9a114a8e42f4"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(709));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9bed23d9-8f16-4444-83d3-9a114a8e42f3"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(664));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("abed23d9-8f16-4444-83d3-9a114a8e42fa"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(763));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(767));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cbed23d9-8f16-4444-83d3-9a114a8e42fb"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(771));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("dbed23d9-8f16-4444-83d3-9a114a8e42fc"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(774));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e0ed23d9-8f16-4444-83d3-9a114a8e422e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(796));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e1ed23d9-8f16-4444-83d3-9a114a8e423e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(799));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e2ed23d9-8f16-4444-83d3-9a114a8e424e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(803));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e3ed23d9-8f16-4444-83d3-9a114a8e425e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(806));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e4ed23d9-8f16-4444-83d3-9a114a8e426e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(814));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e5ed23d9-8f16-4444-83d3-9a114a8e427e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(818));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e6ed23d9-8f16-4444-83d3-9a114a8e428e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(821));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e7ed23d9-8f16-4444-83d3-9a114a8e429e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(825));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e8ed23d9-8f16-4444-83d3-9a114a8e4200"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(831));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e9ed23d9-8f16-4444-83d3-9a114a8e4201"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(834));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ebed23d9-8f16-4444-83d3-9a114a8e42fd"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(782));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eced23d9-8f16-4444-83d3-9a114a8e42fe"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(786));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eded23d9-8f16-4444-83d3-9a114a8e420e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(789));

            migrationBuilder.UpdateData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                keyColumn: "Id",
                keyValue: new Guid("eeed23d9-8f16-4444-83d3-9a114a8e421e"),
                column: "CreatedOn",
                value: new DateTime(2026, 4, 19, 2, 30, 48, 915, DateTimeKind.Utc).AddTicks(793));

            migrationBuilder.CreateIndex(
                name: "IX_SessionParticipants_ViewingSessionId",
                schema: "kn-property-svc",
                table: "SessionParticipants",
                column: "ViewingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewingSessions_ViewingRequestId",
                schema: "kn-property-svc",
                table: "ViewingSessions",
                column: "ViewingRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeatureLinks_Properties_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                column: "PropertyId",
                principalSchema: "kn-property-svc",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeatureLinks_PropertyFeatures_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks",
                column: "FeatureId",
                principalSchema: "kn-property-svc",
                principalTable: "PropertyFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeatureLinks_Properties_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeatureLinks_PropertyFeatures_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks");

            migrationBuilder.DropTable(
                name: "SessionParticipants",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "ViewingSessions",
                schema: "kn-property-svc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyFeatureLinks",
                schema: "kn-property-svc",
                table: "PropertyFeatureLinks");

            migrationBuilder.DropColumn(
                name: "Fee",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.DropColumn(
                name: "ViewingSessionId",
                schema: "kn-property-svc",
                table: "ViewingRequests");

            migrationBuilder.RenameTable(
                name: "PropertyFeatureLinks",
                schema: "kn-property-svc",
                newName: "PropertyFeatureLink",
                newSchema: "kn-property-svc");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLinks_PropertyId_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                newName: "IX_PropertyFeatureLink_PropertyId_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLinks_PropertyId_CustomFeatureNormalized",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                newName: "IX_PropertyFeatureLink_PropertyId_CustomFeatureNormalized");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLinks_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                newName: "IX_PropertyFeatureLink_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLinks_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                newName: "IX_PropertyFeatureLink_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyFeatureLinks_CustomFeatureNormalized",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                newName: "IX_PropertyFeatureLink_CustomFeatureNormalized");

            migrationBuilder.AddColumn<string>(
                name: "MeetingLink",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyFeatureLink",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeatureLink_Properties_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                column: "PropertyId",
                principalSchema: "kn-property-svc",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeatureLink_PropertyFeatures_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                column: "FeatureId",
                principalSchema: "kn-property-svc",
                principalTable: "PropertyFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
