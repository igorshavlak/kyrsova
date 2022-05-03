using Microsoft.EntityFrameworkCore.Migrations;

namespace gergergergerg.Migrations
{
    public partial class EditInquiryHeaderPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumer",
                table: "InquiryHeaders",
                newName: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "InquiryHeaders",
                newName: "PhoneNumer");
        }
    }
}
