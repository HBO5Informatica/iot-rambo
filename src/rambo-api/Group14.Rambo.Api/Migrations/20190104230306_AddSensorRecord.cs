using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group14.Rambo.Api.Migrations
{
    public partial class AddSensorRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCluster_SensorDevice_SensorUnitId",
                table: "PlantCluster");

            migrationBuilder.DropForeignKey(
                name: "FK_SensorDevice_NodeDevice_RouterId",
                table: "SensorDevice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SensorDevice",
                table: "SensorDevice");

            migrationBuilder.RenameTable(
                name: "SensorDevice",
                newName: "SensorDevices");

            migrationBuilder.RenameIndex(
                name: "IX_SensorDevice_RouterId",
                table: "SensorDevices",
                newName: "IX_SensorDevices_RouterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SensorDevices",
                table: "SensorDevices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SensorRecords",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SensorDeviceId = table.Column<long>(nullable: true),
                    MessageId = table.Column<int>(nullable: false),
                    SoilMoisture = table.Column<int>(nullable: false),
                    Humidity = table.Column<float>(nullable: false),
                    Temperature = table.Column<float>(nullable: false),
                    LightLevel = table.Column<float>(nullable: false),
                    RegisteredDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorRecords_SensorDevices_SensorDeviceId",
                        column: x => x.SensorDeviceId,
                        principalTable: "SensorDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "NodeDevice",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "HardwareAddress", "Name" },
                values: new object[] { "030405060708", "Rambo Node" });

            migrationBuilder.CreateIndex(
                name: "IX_SensorRecords_SensorDeviceId",
                table: "SensorRecords",
                column: "SensorDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCluster_SensorDevices_SensorUnitId",
                table: "PlantCluster",
                column: "SensorUnitId",
                principalTable: "SensorDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorDevices_NodeDevice_RouterId",
                table: "SensorDevices",
                column: "RouterId",
                principalTable: "NodeDevice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCluster_SensorDevices_SensorUnitId",
                table: "PlantCluster");

            migrationBuilder.DropForeignKey(
                name: "FK_SensorDevices_NodeDevice_RouterId",
                table: "SensorDevices");

            migrationBuilder.DropTable(
                name: "SensorRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SensorDevices",
                table: "SensorDevices");

            migrationBuilder.RenameTable(
                name: "SensorDevices",
                newName: "SensorDevice");

            migrationBuilder.RenameIndex(
                name: "IX_SensorDevices_RouterId",
                table: "SensorDevice",
                newName: "IX_SensorDevice_RouterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SensorDevice",
                table: "SensorDevice",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "NodeDevice",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "HardwareAddress", "Name" },
                values: new object[] { "AE45FBC654", "Company Node" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCluster_SensorDevice_SensorUnitId",
                table: "PlantCluster",
                column: "SensorUnitId",
                principalTable: "SensorDevice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorDevice_NodeDevice_RouterId",
                table: "SensorDevice",
                column: "RouterId",
                principalTable: "NodeDevice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
