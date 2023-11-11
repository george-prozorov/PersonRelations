using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonReplations.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceTypeId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedAd = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 11, 13, 31, 53, 873, DateTimeKind.Local).AddTicks(1763))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PersonalId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedAd = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 11, 13, 31, 53, 872, DateTimeKind.Local).AddTicks(7953))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_References_CityId",
                        column: x => x.CityId,
                        principalTable: "References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedAd = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 11, 13, 31, 53, 873, DateTimeKind.Local).AddTicks(2553))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relations_References_RelationTypeId",
                        column: x => x.RelationTypeId,
                        principalTable: "References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedAd = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 11, 13, 31, 53, 872, DateTimeKind.Local).AddTicks(6856))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_References_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    RelationId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedAd = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 11, 13, 31, 53, 872, DateTimeKind.Local).AddTicks(9542))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonRelations_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRelations_Relations_RelationId",
                        column: x => x.RelationId,
                        principalTable: "Relations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "References",
                columns: new[] { "Id", "DisplayName", "ReferenceTypeId" },
                values: new object[,]
                {
                    { 1, "ქალი", 1 },
                    { 2, "კაცი", 1 },
                    { 3, "მობილური", 3 },
                    { 4, "ოფისი", 3 },
                    { 5, "სახლი", 3 },
                    { 6, "კოლეგა", 4 },
                    { 7, "ნაცნობი", 4 },
                    { 8, "ნათესავი", 4 },
                    { 9, "სხვა", 4 },
                    { 10, "თბილისი", 2 },
                    { 11, "პრაღა", 2 },
                    { 12, "პარიზი", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonId",
                table: "PersonRelations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_RelationId",
                table: "PersonRelations",
                column: "RelationId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FirstName",
                table: "Persons",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_LastName",
                table: "Persons",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonalId",
                table: "Persons",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_RelationTypeId",
                table: "Relations",
                column: "RelationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "PersonRelations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "References");
        }
    }
}
