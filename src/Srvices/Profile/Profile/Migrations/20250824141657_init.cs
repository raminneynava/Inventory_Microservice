using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Profiles");

            migrationBuilder.CreateSequence(
                name: "ProfileSequence",
                schema: "Profiles");

            migrationBuilder.CreateTable(
                name: "CorporateProfiles",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Profiles].[ProfileSequence]"),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    TimeZoneId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NationalId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualProfiles",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Profiles].[ProfileSequence]"),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    TimeZoneId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: false),
                    LastName = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: false),
                    NationalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Profiles].[ProfileSequence]"),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    TimeZoneId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeFormat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProfiles_NationalId",
                schema: "Profiles",
                table: "CorporateProfiles",
                column: "NationalId",
                unique: true,
                filter: "[NationalId] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProfiles_PhoneNumber",
                schema: "Profiles",
                table: "CorporateProfiles",
                column: "PhoneNumber",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualProfiles_NationalCode",
                schema: "Profiles",
                table: "IndividualProfiles",
                column: "NationalCode",
                unique: true,
                filter: "[NationalCode] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualProfiles_PhoneNumber",
                schema: "Profiles",
                table: "IndividualProfiles",
                column: "PhoneNumber",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PhoneNumber",
                schema: "Profiles",
                table: "Profiles",
                column: "PhoneNumber",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorporateProfiles",
                schema: "Profiles");

            migrationBuilder.DropTable(
                name: "IndividualProfiles",
                schema: "Profiles");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "Profiles");

            migrationBuilder.DropSequence(
                name: "ProfileSequence",
                schema: "Profiles");
        }
    }
}
