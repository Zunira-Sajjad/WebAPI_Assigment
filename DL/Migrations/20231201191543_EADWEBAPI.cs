using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DL.Migrations
{
    public partial class EADWEBAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "studentDbDto",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RollNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentDbDto", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "subjectDbDto",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectDbDto", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "studentSubjectDbDto",
                columns: table => new
                {
                    StudentSubjecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPA = table.Column<double>(type: "float", nullable: false),
                    studentID = table.Column<int>(type: "int", nullable: false),
                    subjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentSubjectDbDto", x => x.StudentSubjecId);
                    table.ForeignKey(
                        name: "FK_studentSubjectDbDto_studentDbDto_studentID",
                        column: x => x.studentID,
                        principalTable: "studentDbDto",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentSubjectDbDto_subjectDbDto_subjectId",
                        column: x => x.subjectId,
                        principalTable: "subjectDbDto",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_studentSubjectDbDto_studentID",
                table: "studentSubjectDbDto",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_studentSubjectDbDto_subjectId",
                table: "studentSubjectDbDto",
                column: "subjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studentSubjectDbDto");

            migrationBuilder.DropTable(
                name: "studentDbDto");

            migrationBuilder.DropTable(
                name: "subjectDbDto");
        }
    }
}
