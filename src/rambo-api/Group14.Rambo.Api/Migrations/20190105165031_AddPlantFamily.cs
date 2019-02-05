using Microsoft.EntityFrameworkCore.Migrations;

namespace Group14.Rambo.Api.Migrations
{
    public partial class AddPlantFamily : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCluster_PlantFamily_FamilyId",
                table: "PlantCluster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantFamily",
                table: "PlantFamily");

            migrationBuilder.RenameTable(
                name: "PlantFamily",
                newName: "PlantFamilies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantFamilies",
                table: "PlantFamilies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCluster_PlantFamilies_FamilyId",
                table: "PlantCluster",
                column: "FamilyId",
                principalTable: "PlantFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCluster_PlantFamilies_FamilyId",
                table: "PlantCluster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantFamilies",
                table: "PlantFamilies");

            migrationBuilder.RenameTable(
                name: "PlantFamilies",
                newName: "PlantFamily");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantFamily",
                table: "PlantFamily",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCluster_PlantFamily_FamilyId",
                table: "PlantCluster",
                column: "FamilyId",
                principalTable: "PlantFamily",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
