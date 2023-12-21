using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepititMe.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "PaymentStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WaitingPayment",
                table: "PaymentStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "PaymentStatuses");

            migrationBuilder.DropColumn(
                name: "WaitingPayment",
                table: "PaymentStatuses");
        }
    }
}
