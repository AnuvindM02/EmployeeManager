using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTasks",
                columns: table => new
                {
                    TaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskDetails = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTasks", x => x.TaskID);
                });

            migrationBuilder.InsertData(
                table: "EmployeeTasks",
                columns: new[] { "TaskID", "EmployeeID", "IsCompleted", "TaskDetails" },
                values: new object[,]
                {
                    { new Guid("0b625c4c-92e7-489f-86a4-8b0ef537e1e1"), new Guid("58085ad6-77bf-4e12-a253-f5f7102c3e48"), false, "Get something for yourselves" },
                    { new Guid("189e8d61-3552-44bb-a454-47486a85699f"), new Guid("93f9d8e5-a28e-4f4c-af1e-291c3d2ca3f1"), false, "Do the task ASAP" },
                    { new Guid("4090878a-3647-4a91-b67a-2205e917055f"), new Guid("735372f6-518d-4128-bfab-38c83160bf96"), false, "Fix the bugs" },
                    { new Guid("58f707e7-7199-42bc-96e3-59cf2a4d4e10"), new Guid("58085ad6-77bf-4e12-a253-f5f7102c3e48"), true, "Do something" },
                    { new Guid("720a165c-eed2-4684-aeec-cf326e618945"), new Guid("e00296ce-e78a-4f39-9695-8f0605106138"), true, "Do it before 10 pm" },
                    { new Guid("81bf8eb3-0ce2-4f77-888b-ac59ee9665fe"), new Guid("fb604f91-2ba3-48c8-bdf5-d7bd8878c909"), false, "Run 100 miles" },
                    { new Guid("9ae6f45b-2a3a-4f7b-aa74-8cc41b4a5d5b"), new Guid("fb604f91-2ba3-48c8-bdf5-d7bd8878c909"), false, "Make it fast" },
                    { new Guid("c42e02d8-4d6f-4c70-99c1-eeeef1e318c5"), new Guid("735372f6-518d-4128-bfab-38c83160bf96"), false, "Wash it" },
                    { new Guid("d251df32-4452-4b54-b418-03b8eb72b8db"), new Guid("152e8639-b070-42d5-af8f-6782494eaa56"), false, "Do a backflip" },
                    { new Guid("ef73fcfd-6fc2-4ec0-926e-ccdf4be9226f"), new Guid("5464431e-30a9-48fa-9cf6-1e176e45d0af"), false, "Clean the car" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Email", "EmployeeName", "Gender", "Position" },
                values: new object[,]
                {
                    { new Guid("152e8639-b070-42d5-af8f-6782494eaa56"), "gcradoc2@drupal.org", "Giselbert", "Male", "Teacher" },
                    { new Guid("5464431e-30a9-48fa-9cf6-1e176e45d0af"), "sygoe8@ebay.co.uk", "Sadye", "Female", "Computer Systems Analyst III" },
                    { new Guid("58085ad6-77bf-4e12-a253-f5f7102c3e48"), "vmitskevich0@harvard.edu", "Virge", "Male", "Analog Circuit Design manager" },
                    { new Guid("735372f6-518d-4128-bfab-38c83160bf96"), "fpalluschek5@amazonaws.com", "Florrie", "Female", "Account Coordinator" },
                    { new Guid("93f9d8e5-a28e-4f4c-af1e-291c3d2ca3f1"), "pbreache7@miibeian.gov.cn", "Pete", "Male", "Software Engineer II" },
                    { new Guid("97952a58-dfe3-43ea-b788-a74d2e5fc2cc"), "ereyne6@wisc.edu", "Elinor", "Female", "Account Executive" },
                    { new Guid("97cfd804-b86c-4b83-9a6b-73ef49aba28e"), "jpfeiffer3@instagram.com", "Jeremias", "Male", "Research Associate" },
                    { new Guid("c7543bce-6ce3-4c55-af4c-d426a1357b66"), "mdambrogio1@omniture.com", "Merle", "Male", "Administrative Officer" },
                    { new Guid("e00296ce-e78a-4f39-9695-8f0605106138"), "sstolze4@wp.com", "Sammy", "Male", "VP Product Management" },
                    { new Guid("fb604f91-2ba3-48c8-bdf5-d7bd8878c909"), "cgebby9@ask.com", "Corabel", "Female", "Analog Circuit Design manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeTasks");
        }
    }
}
