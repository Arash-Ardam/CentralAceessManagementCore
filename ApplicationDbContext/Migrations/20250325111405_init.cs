using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationDbContext.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataCenters",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCenters", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseEngine",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCenterName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseEngine", x => x.Name);
                    table.ForeignKey(
                        name: "FK_DatabaseEngine_DataCenters_DataCenterName",
                        column: x => x.DataCenterName,
                        principalTable: "DataCenters",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Database",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataBaseEngineName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Database_DatabaseEngine_DataBaseEngineName",
                        column: x => x.DataBaseEngineName,
                        principalTable: "DatabaseEngine",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Database_DataBaseEngineName",
                table: "Database",
                column: "DataBaseEngineName");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseEngine_DataCenterName",
                table: "DatabaseEngine",
                column: "DataCenterName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Database");

            migrationBuilder.DropTable(
                name: "DatabaseEngine");

            migrationBuilder.DropTable(
                name: "DataCenters");
        }
    }
}
