using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Street" },
                values: new object[] { 1, "lmao" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Street" },
                values: new object[] { 2, "bruder" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "Name" },
                values: new object[] { 1, 1, "Hamlet" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "Name" },
                values: new object[] { 2, 1, "King Lear" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "Name" },
                values: new object[] { 3, 2, "Othello" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
