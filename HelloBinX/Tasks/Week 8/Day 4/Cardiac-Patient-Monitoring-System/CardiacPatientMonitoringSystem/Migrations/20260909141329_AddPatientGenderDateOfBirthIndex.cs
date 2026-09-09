using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardiacPatientMonitoringSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientGenderDateOfBirthIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_Gender_DateOfBirth",
                table: "Patients",
                columns: new[] { "Gender", "DateOfBirth" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Gender_DateOfBirth",
                table: "Patients");
        }
    }
}
