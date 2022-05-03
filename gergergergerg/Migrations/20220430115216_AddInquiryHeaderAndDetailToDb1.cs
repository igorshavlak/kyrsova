using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gergergergerg.Migrations
{
    public partial class AddInquiryHeaderAndDetailToDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InquiryDetail",
                table: "InquiryDetail");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "InquiryHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumer",
                table: "InquiryHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InquiryDate",
                table: "InquiryHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InquiryDetail",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InquiryDetail",
                table: "InquiryDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryDetail_InquiryHeaderId",
                table: "InquiryDetail",
                column: "InquiryHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InquiryDetail",
                table: "InquiryDetail");

            migrationBuilder.DropIndex(
                name: "IX_InquiryDetail_InquiryHeaderId",
                table: "InquiryDetail");

            migrationBuilder.DropColumn(
                name: "InquiryDate",
                table: "InquiryHeaders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InquiryDetail");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "InquiryHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumer",
                table: "InquiryHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InquiryDetail",
                table: "InquiryDetail",
                column: "InquiryHeaderId");
        }
    }
}
