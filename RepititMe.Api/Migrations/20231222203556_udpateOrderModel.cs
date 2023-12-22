using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepititMe.Api.Migrations
{
    /// <inheritdoc />
    public partial class udpateOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotificationTimeAccept",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotificationTimeFirstLesson",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationTimeAccept",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NotificationTimeFirstLesson",
                table: "Orders");
        }
    }
}
