﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemindersManager.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(maxLength: 80, nullable: false),
                    Notes = table.Column<string>(maxLength: 500, nullable: true),
                    RemindDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCancelled = table.Column<bool>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { new Guid("7e5ed0cc-4b68-411d-9a65-07b17bded6fe"), "franz.kafka@gmail.com", "Franz Kafka" });

            migrationBuilder.InsertData(
                table: "Reminders",
                columns: new[] { "Id", "AuthorId", "IsActive", "IsCancelled", "Notes", "RemindDate", "Subject" },
                values: new object[] { new Guid("98adeabd-ed7a-4a39-a14d-a1e4c8bed9a8"), new Guid("7e5ed0cc-4b68-411d-9a65-07b17bded6fe"), true, false, "Write clean code!", new DateTime(2019, 1, 18, 6, 0, 0, 0, DateTimeKind.Unspecified), "Finish interview task" });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_AuthorId",
                table: "Reminders",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
