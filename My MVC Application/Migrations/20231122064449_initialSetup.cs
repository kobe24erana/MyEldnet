using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_MVC_Application.Migrations
{
    /// <inheritdoc />
    public partial class initialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrollmentDetails",
                columns: table => new
                {
                    ENRDSTUDID = table.Column<long>(type: "bigint", nullable: false),
                    ENRDSTUDEDPCODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ENRDSTUDSUBJCODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENRDSTUDSTATUS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentDetails", x => new { x.ENRDSTUDID, x.ENRDSTUDEDPCODE });
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentHeaderFiles",
                columns: table => new
                {
                    ENRHSTUDID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ENRHSTUDDATEENROLL = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENRHSTUDSCHLYR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENRHSTUDENCODER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENRHSTUDTOTALUNITS = table.Column<double>(type: "float", nullable: false),
                    ENRHSTUDSTATUS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentHeaderFiles", x => x.ENRHSTUDID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    STUDID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    STUDLNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STUDFNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STUDMNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STUDCOURSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STUDYEAR = table.Column<int>(type: "int", nullable: true),
                    STUDREMARKS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STUDSTATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.STUDID);
                });

            migrationBuilder.CreateTable(
                name: "SubjectAndSubjectPreqs",
                columns: table => new
                {
                    SUBJCODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SUBJCOURSECODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SUBJDESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUBJUNITS = table.Column<int>(type: "int", nullable: true),
                    SUBJREGOFRNG = table.Column<int>(type: "int", nullable: true),
                    SFSUBJCATEGORY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUBJSTATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUBJCURRCODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUBJPRECODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUBJCATEGORY = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAndSubjectPreqs", x => new { x.SUBJCODE, x.SUBJCOURSECODE });
                });

            migrationBuilder.CreateTable(
                name: "SubjectScheds",
                columns: table => new
                {
                    EDPCODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SUBJCODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STARTTIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDTIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DAYS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROOM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAXSIZE = table.Column<int>(type: "int", nullable: false),
                    CLASSSIZE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FXM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SECTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCHOOLYEAR = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectScheds", x => x.EDPCODE);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentDetails");

            migrationBuilder.DropTable(
                name: "EnrollmentHeaderFiles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SubjectAndSubjectPreqs");

            migrationBuilder.DropTable(
                name: "SubjectScheds");
        }
    }
}
