using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardiacPatientMonitoringSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicationSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Medications",
                columns: new[] { "MedicationId", "Description", "DosageForm", "GenericName", "Manufacturer", "Name", "Strength" },
                values: new object[,]
                {
                    { 101, "Antiplatelet medication commonly used in cardiovascular care.", "Tablet", "Acetylsalicylic Acid", "Bayer", "Aspirin", "81 mg" },
                    { 102, "Statin medication used to lower cholesterol.", "Tablet", "Atorvastatin", "Pfizer", "Atorvastatin", "20 mg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medications",
                keyColumn: "MedicationId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Medications",
                keyColumn: "MedicationId",
                keyValue: 102);
        }
    }
}
