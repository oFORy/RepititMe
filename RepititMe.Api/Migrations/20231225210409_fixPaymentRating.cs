using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepititMe.Api.Migrations
{
    /// <inheritdoc />
    public partial class fixPaymentRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PaymentRatingFromCommission",
                table: "Teachers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PaymentRatingFromProfile",
                table: "Teachers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PaidCommission",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentRatingFromCommission",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PaymentRatingFromProfile",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PaidCommission",
                table: "Orders");
        }
    }
}
