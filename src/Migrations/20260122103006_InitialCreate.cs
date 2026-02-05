using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstNETWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    targetMuscle1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    targetMuscle2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    targetMuscle3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exercise__3213E83F5F5DCFD7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    membershipTier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Basic")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3213E83FE8E97ABC", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BodyTracks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    weight = table.Column<double>(type: "float", nullable: false),
                    height = table.Column<double>(type: "float", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BodyTrac__3213E83F9376C573", x => x.id);
                    table.ForeignKey(
                        name: "FK_BodyTracks_Users",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    membershipTier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plans__3213E83FDE6669F9", x => x.id);
                    table.ForeignKey(
                        name: "FK_Plans_Users",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    planId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Session__3213E83F0BBAD9DE", x => x.id);
                    table.ForeignKey(
                        name: "FK_Session_Plans",
                        column: x => x.planId,
                        principalTable: "Plans",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sessionId = table.Column<int>(type: "int", nullable: false),
                    exerciseId = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: false),
                    reps = table.Column<int>(type: "int", nullable: false),
                    restTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sets__3213E83F473FF2E9", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sets_Exercises",
                        column: x => x.exerciseId,
                        principalTable: "Exercises",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Sets_Session",
                        column: x => x.sessionId,
                        principalTable: "Session",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "id", "imageUrl", "name", "targetMuscle1", "targetMuscle2", "targetMuscle3" },
                values: new object[,]
                {
                    { 1, "https://example.com/bench.png", "bench press", "Chest", "Triceps", "Shoulders" },
                    { 2, "https://example.com/squat.png", "squat", "Quads", "Glutes", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "username" },
                values: new object[,]
                {
                    { 1, "dummyemail1@gmail.com", "akmfsapmfpkmfdsapmfwe", "dummyUsername1" },
                    { 2, "dummyemail2@gmail.com", "yiueoipkwemrkwerewerw", "dummyUsername2" },
                    { 3, "dummyemail3@gmail.com", "svfsdhkjyuiyuiertetrr", "dummyUsername3" }
                });

            migrationBuilder.InsertData(
                table: "BodyTracks",
                columns: new[] { "id", "date", "height", "userId", "weight" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 1, 1), 186.0, 1, 71.099999999999994 },
                    { 2, new DateOnly(2025, 1, 1), 175.0, 2, 68.5 }
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "id", "membershipTier", "name", "userId" },
                values: new object[,]
                {
                    { 1, "basic", "beginner strength", 1 },
                    { 2, "basic", "upper body focus", 2 }
                });

            migrationBuilder.InsertData(
                table: "Session",
                columns: new[] { "id", "date", "planId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 1, 10), 1 },
                    { 2, new DateOnly(2025, 1, 12), 2 }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "id", "exerciseId", "reps", "restTime", "sessionId", "weight" },
                values: new object[,]
                {
                    { 1, 1, 10, 90, 1, 60 },
                    { 2, 1, 8, 120, 1, 65 },
                    { 3, 2, 12, 120, 2, 80 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyTracks_userId",
                table: "BodyTracks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_userId",
                table: "Plans",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_planId",
                table: "Session",
                column: "planId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_exerciseId",
                table: "Sets",
                column: "exerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_sessionId",
                table: "Sets",
                column: "sessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyTracks");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
