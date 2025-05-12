﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauApp.Migrations
{
    /// <inheritdoc />
    public partial class AddArriver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Arriver",
                table: "Reservations",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arriver",
                table: "Reservations");
        }
    }
}
