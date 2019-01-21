using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemindersManager.Migrations
{
    public partial class tbl_ReminderJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: new Guid("a13ab3c3-beba-404e-a87d-e681d6beaa25"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("f58d0b6e-23b2-4379-8b19-a939c9ef360a"));

            migrationBuilder.CreateTable(
                name: "ReminderJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReminderId = table.Column<Guid>(nullable: false),
                    JobId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderJobs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { new Guid("3c1c0203-b206-405f-a33f-956724992e41"), "franz.kafka@gmail.com", "Franz Kafka" });

            migrationBuilder.InsertData(
                table: "Reminders",
                columns: new[] { "Id", "AuthorId", "IsActive", "IsCancelled", "Notes", "RemindDate", "Subject" },
                values: new object[] { new Guid("12128e4b-4e8f-449b-98c1-8fc6b7c9b59b"), new Guid("3c1c0203-b206-405f-a33f-956724992e41"), true, false, "Write clean code!", new DateTime(2019, 1, 18, 6, 0, 0, 0, DateTimeKind.Unspecified), "Finish interview task" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReminderJobs");

            migrationBuilder.DeleteData(
                table: "Reminders",
                keyColumn: "Id",
                keyValue: new Guid("12128e4b-4e8f-449b-98c1-8fc6b7c9b59b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("3c1c0203-b206-405f-a33f-956724992e41"));

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { new Guid("f58d0b6e-23b2-4379-8b19-a939c9ef360a"), "franz.kafka@gmail.com", "Franz Kafka" });

            migrationBuilder.InsertData(
                table: "Reminders",
                columns: new[] { "Id", "AuthorId", "IsActive", "IsCancelled", "Notes", "RemindDate", "Subject" },
                values: new object[] { new Guid("a13ab3c3-beba-404e-a87d-e681d6beaa25"), new Guid("f58d0b6e-23b2-4379-8b19-a939c9ef360a"), true, false, "Write clean code!", new DateTime(2019, 1, 18, 6, 0, 0, 0, DateTimeKind.Unspecified), "Finish interview task" });
        }
    }
}
