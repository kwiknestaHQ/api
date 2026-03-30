using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KwikNestaProperty.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kn-property-svc");

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ListingType = table.Column<string>(type: "text", nullable: false),
                    PriceFrequency = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Bedrooms = table.Column<int>(type: "integer", nullable: false),
                    Bathrooms = table.Column<int>(type: "integer", nullable: false),
                    AreaSize = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    AreaUnit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ParkingSpaces = table.Column<int>(type: "integer", nullable: true),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFeatures",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameNormalized = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    VerificationStatus = table.Column<string>(type: "text", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnershipVerificationRequests",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    RejectionReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipVerificationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnershipVerificationRequests_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyInquiries",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OfferedPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyInquiries_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMedias",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyMedias_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyPriceHistories",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    OldPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NewPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyPriceHistories_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewingRequests",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewingRequests_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFeatureLink",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomFeature = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CustomFeatureNormalized = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeatureLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyFeatureLink_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "kn-property-svc",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyFeatureLink_PropertyFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "kn-property-svc",
                        principalTable: "PropertyFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnershipDocuments",
                schema: "kn-property-svc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VerificationRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    OtherDocumentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnershipDocuments_OwnershipVerificationRequests_Verificati~",
                        column: x => x.VerificationRequestId,
                        principalSchema: "kn-property-svc",
                        principalTable: "OwnershipVerificationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "kn-property-svc",
                table: "PropertyFeatures",
                columns: new[] { "Id", "Category", "CreatedOn", "IsDeprecated", "LastUpdatedOn", "Name", "NameNormalized" },
                values: new object[,]
                {
                    { new Guid("00ed23d9-8f16-4444-83d3-9a114a8e4202"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7303), false, null, "Jacuzzi", "jacuzzi" },
                    { new Guid("01ed23d9-8f16-4444-83d3-9a114a8e4203"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7307), false, null, "Garage", "garage" },
                    { new Guid("02ed23d9-8f16-4444-83d3-9a114a8e4204"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7314), false, null, "Elevator", "elevator" },
                    { new Guid("0bed23d9-8f16-4444-83d3-9a114a8e42f2"), "Exterior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7228), false, null, "Fence", "fence" },
                    { new Guid("1bed23d9-8f16-4444-83d3-9a114a8e42f1"), "Exterior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7223), false, null, "Garden", "garden" },
                    { new Guid("2bed23d9-8f16-4444-83d3-9a114a8e42f0"), "Exterior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7220), false, null, "Terrace", "terrace" },
                    { new Guid("3bed23d9-8f16-4444-83d3-9a114a8e42f9"), "Exterior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7216), false, null, "Balcony", "balcony" },
                    { new Guid("4bed23d9-8f16-4444-83d3-9a114a8e42f8"), "Interior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7198), false, null, "Laundry Room", "laundry room" },
                    { new Guid("5bed23d9-8f16-4444-83d3-9a114a8e42f7"), "Interior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7194), false, null, "Smart Home System", "smart home system" },
                    { new Guid("6bed23d9-8f16-4444-83d3-9a114a8e42f6"), "Interior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7191), false, null, "Built-in Wardrobes", "built-in wardrobes" },
                    { new Guid("7bed23d9-8f16-4444-83d3-9a114a8e42f5"), "Interior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7187), false, null, "Semi-Furnished", "semi-furnished" },
                    { new Guid("8bed23d9-8f16-4444-83d3-9a114a8e42f4"), "Interior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7182), false, null, "Furnished", "furnished" },
                    { new Guid("9bed23d9-8f16-4444-83d3-9a114a8e42f3"), "Interior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7139), false, null, "Air Conditioning", "air conditioning" },
                    { new Guid("abed23d9-8f16-4444-83d3-9a114a8e42fa"), "Exterior", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7232), false, null, "Gated Compound", "gated compound" },
                    { new Guid("bbed23d9-8f16-4444-83d3-9a114a8e42fb"), "Security", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7235), false, null, "CCTV", "cctv" },
                    { new Guid("cbed23d9-8f16-4444-83d3-9a114a8e42fb"), "Security", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7238), false, null, "Security Doors", "security doors" },
                    { new Guid("dbed23d9-8f16-4444-83d3-9a114a8e42fc"), "Security", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7241), false, null, "Burglar Alarm", "burglar alarm" },
                    { new Guid("e0ed23d9-8f16-4444-83d3-9a114a8e422e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7265), false, null, "Boys Quarters", "boys quarters" },
                    { new Guid("e1ed23d9-8f16-4444-83d3-9a114a8e423e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7269), false, null, "Generator", "generator" },
                    { new Guid("e2ed23d9-8f16-4444-83d3-9a114a8e424e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7272), false, null, "Inverter", "inverter" },
                    { new Guid("e3ed23d9-8f16-4444-83d3-9a114a8e425e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7276), false, null, "Borehole", "borehole" },
                    { new Guid("e4ed23d9-8f16-4444-83d3-9a114a8e426e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7283), false, null, "Water Suply", "water suply" },
                    { new Guid("e5ed23d9-8f16-4444-83d3-9a114a8e427e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7287), false, null, "Cable TV", "cable tv" },
                    { new Guid("e6ed23d9-8f16-4444-83d3-9a114a8e428e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7290), false, null, "Internet", "internet" },
                    { new Guid("e7ed23d9-8f16-4444-83d3-9a114a8e429e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7293), false, null, "Parking Space", "parking space" },
                    { new Guid("e8ed23d9-8f16-4444-83d3-9a114a8e4200"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7297), false, null, "Swimming Pool", "swimming pool" },
                    { new Guid("e9ed23d9-8f16-4444-83d3-9a114a8e4201"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7300), false, null, "Gym", "gym" },
                    { new Guid("ebed23d9-8f16-4444-83d3-9a114a8e42fd"), "Security", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7250), false, null, "Gated Estate", "gated estate" },
                    { new Guid("eced23d9-8f16-4444-83d3-9a114a8e42fe"), "Security", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7253), false, null, "Security", "security" },
                    { new Guid("eded23d9-8f16-4444-83d3-9a114a8e420e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7257), false, null, "Electricity", "electricity" },
                    { new Guid("eeed23d9-8f16-4444-83d3-9a114a8e421e"), "Utilities", new DateTime(2026, 3, 30, 15, 1, 18, 484, DateTimeKind.Utc).AddTicks(7261), false, null, "Prepaid Meter", "prepaid meter" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_PropertyId",
                schema: "kn-property-svc",
                table: "Locations",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyLocation_Lat_Lng_VerifiedOnly",
                schema: "kn-property-svc",
                table: "Locations",
                columns: new[] { "Latitude", "Longitude" },
                filter: "\"VerificationStatus\" = 'Verified'");

            migrationBuilder.CreateIndex(
                name: "IX_OwnershipDocuments_VerificationRequestId",
                schema: "kn-property-svc",
                table: "OwnershipDocuments",
                column: "VerificationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnershipVerificationRequests_PropertyId",
                schema: "kn-property-svc",
                table: "OwnershipVerificationRequests",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_Price",
                schema: "kn-property-svc",
                table: "Properties",
                column: "Price",
                filter: "\"Status\" = 'Available'");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_Type_Bedrooms_Bathrooms",
                schema: "kn-property-svc",
                table: "Properties",
                columns: new[] { "Type", "Bedrooms", "Bathrooms" },
                filter: "\"Status\" = 'Available'");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_Type_Price",
                schema: "kn-property-svc",
                table: "Properties",
                columns: new[] { "Type", "Price" },
                filter: "\"Status\" = 'Available'");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatureLink_CustomFeatureNormalized",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                column: "CustomFeatureNormalized");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatureLink_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatureLink_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatureLink_PropertyId_CustomFeatureNormalized",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                columns: new[] { "PropertyId", "CustomFeatureNormalized" },
                unique: true,
                filter: "\"CustomFeatureNormalized\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatureLink_PropertyId_FeatureId",
                schema: "kn-property-svc",
                table: "PropertyFeatureLink",
                columns: new[] { "PropertyId", "FeatureId" },
                unique: true,
                filter: "\"FeatureId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInquiries_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyInquiries",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMedias_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyMedias",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPriceHistories_PropertyId",
                schema: "kn-property-svc",
                table: "PropertyPriceHistories",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewingRequests_PropertyId",
                schema: "kn-property-svc",
                table: "ViewingRequests",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "OwnershipDocuments",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "PropertyFeatureLink",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "PropertyInquiries",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "PropertyMedias",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "PropertyPriceHistories",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "ViewingRequests",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "OwnershipVerificationRequests",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "PropertyFeatures",
                schema: "kn-property-svc");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "kn-property-svc");
        }
    }
}
