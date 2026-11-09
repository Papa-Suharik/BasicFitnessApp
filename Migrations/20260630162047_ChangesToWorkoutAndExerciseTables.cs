using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicFitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToWorkoutAndExerciseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameOfExcercise",
                table: "Exercises",
                newName: "NameOfExercise");

            migrationBuilder.AddColumn<int>(
                name: "TargetMuscles",
                table: "Workouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Goal",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetMuscles",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "NameOfExercise",
                table: "Exercises",
                newName: "NameOfExcercise");
        }
    }
}
