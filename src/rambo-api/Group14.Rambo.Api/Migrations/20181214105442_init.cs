using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group14.Rambo.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NodeDevice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HardwareAddress = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantFamily",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    SoilMoisture = table.Column<float>(nullable: false),
                    LightIntensity = table.Column<float>(nullable: false),
                    AirHumidity = table.Column<float>(nullable: false),
                    AirTemperature = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantFamily", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActorDevice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HardwareAddress = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Capabilities = table.Column<int>(nullable: false),
                    RouterId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActorDevice_NodeDevice_RouterId",
                        column: x => x.RouterId,
                        principalTable: "NodeDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorDevice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HardwareAddress = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Capabilities = table.Column<int>(nullable: false),
                    RouterId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorDevice_NodeDevice_RouterId",
                        column: x => x.RouterId,
                        principalTable: "NodeDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantCluster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    NumberOfPlants = table.Column<int>(nullable: false),
                    FamilyId = table.Column<long>(nullable: true),
                    SensorUnitId = table.Column<long>(nullable: true),
                    ActorUnitId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCluster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantCluster_ActorDevice_ActorUnitId",
                        column: x => x.ActorUnitId,
                        principalTable: "ActorDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantCluster_PlantFamily_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "PlantFamily",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantCluster_SensorDevice_SensorUnitId",
                        column: x => x.SensorUnitId,
                        principalTable: "SensorDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "NodeDevice",
                columns: new[] { "Id", "HardwareAddress", "Name" },
                values: new object[] { 1L, "AE45FBC654", "Company Node" });

            migrationBuilder.InsertData(
                table: "PlantFamily",
                columns: new[] { "Id", "AirHumidity", "AirTemperature", "LightIntensity", "Name", "SoilMoisture" },
                values: new object[] { 1L, 0.55f, 20f, 250f, "Heat intolerant - Shade", 0.3f });

            migrationBuilder.InsertData(
                table: "PlantFamily",
                columns: new[] { "Id", "AirHumidity", "AirTemperature", "LightIntensity", "Name", "SoilMoisture" },
                values: new object[] { 2L, 0.7f, 25f, 40000f, "Thirsty - Sunlight", 0.5f });

            migrationBuilder.InsertData(
                table: "ActorDevice",
                columns: new[] { "Id", "Capabilities", "HardwareAddress", "Name", "RouterId" },
                values: new object[,]
                {
                    { 1L, 7, "4673EFC", "Office group 1 Actor", 1L },
                    { 2L, 7, "8763EFC", "Office group 2 Actor", 1L },
                    { 3L, 7, "9872AED", "Reception flowerbed 1 Actor", 1L }
                });

            migrationBuilder.InsertData(
                table: "SensorDevice",
                columns: new[] { "Id", "Capabilities", "HardwareAddress", "Name", "RouterId" },
                values: new object[,]
                {
                    { 1L, 15, "4673EFC", "Office group 1 Sensors", 1L },
                    { 2L, 15, "8763EFC", "Office group 2 Sensors", 1L },
                    { 3L, 15, "9872AED", "Reception flowerbed 1 Sensors", 1L }
                });

            migrationBuilder.InsertData(
                table: "PlantCluster",
                columns: new[] { "Id", "ActorUnitId", "FamilyId", "Name", "NumberOfPlants", "SensorUnitId" },
                values: new object[] { 1L, 1L, 1L, "Office group 1", 2, 1L });

            migrationBuilder.InsertData(
                table: "PlantCluster",
                columns: new[] { "Id", "ActorUnitId", "FamilyId", "Name", "NumberOfPlants", "SensorUnitId" },
                values: new object[] { 2L, 2L, 2L, "Office group 2", 1, 2L });

            migrationBuilder.InsertData(
                table: "PlantCluster",
                columns: new[] { "Id", "ActorUnitId", "FamilyId", "Name", "NumberOfPlants", "SensorUnitId" },
                values: new object[] { 3L, 3L, 1L, "Reception flowerbed 1", 6, 3L });

            migrationBuilder.CreateIndex(
                name: "IX_ActorDevice_RouterId",
                table: "ActorDevice",
                column: "RouterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCluster_ActorUnitId",
                table: "PlantCluster",
                column: "ActorUnitId",
                unique: true,
                filter: "[ActorUnitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCluster_FamilyId",
                table: "PlantCluster",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCluster_SensorUnitId",
                table: "PlantCluster",
                column: "SensorUnitId",
                unique: true,
                filter: "[SensorUnitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDevice_RouterId",
                table: "SensorDevice",
                column: "RouterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantCluster");

            migrationBuilder.DropTable(
                name: "ActorDevice");

            migrationBuilder.DropTable(
                name: "PlantFamily");

            migrationBuilder.DropTable(
                name: "SensorDevice");

            migrationBuilder.DropTable(
                name: "NodeDevice");
        }
    }
}
