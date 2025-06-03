using System;
using BusToursInEurope.Core.Entites;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusToursInEurope.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameToRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<RouteBus>("Name", "RoutesBuses", type: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
