using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace WebApp.Migrations
{
    public partial class RenameEntityes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_DistrictToСlient_Client_ClientId", table: "DistrictToСlient");
            migrationBuilder.DropForeignKey(name: "FK_Street_District_DistrictId", table: "Street");
            migrationBuilder.DropColumn(name: "DistrictId", table: "Street");
            migrationBuilder.DropTable("Building");
            migrationBuilder.DropTable("Client");
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    ContractSum = table.Column<int>(nullable: false),
                    CustomerUserId = table.Column<string>(nullable: true),
                    DateClosed = table.Column<DateTime>(nullable: false),
                    DateContract = table.Column<DateTime>(nullable: false),
                    DateIn = table.Column<DateTime>(nullable: false),
                    DateMeeting = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    IsSite = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    MaxSum = table.Column<int>(nullable: false),
                    MidleName = table.Column<string>(nullable: true),
                    MinSum = table.Column<int>(nullable: false),
                    ReheshSum = table.Column<int>(nullable: false),
                    Resource = table.Column<string>(nullable: true),
                    SmsId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_ApplicationUser_CustomerUserId",
                        column: x => x.CustomerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Sms_SmsId",
                        column: x => x.SmsId,
                        principalTable: "Sms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Housing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    DistrictId = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    House = table.Column<string>(nullable: true),
                    InDate = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    MidleName = table.Column<string>(nullable: true),
                    PartherShip = table.Column<int>(nullable: false),
                    RevisionDate = table.Column<DateTime>(nullable: false),
                    Room = table.Column<string>(nullable: true),
                    StreetId = table.Column<int>(nullable: false),
                    Sum = table.Column<double>(nullable: false),
                    TypesHousingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Housing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Housing_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Housing_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Housing_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Housing_Street_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Street",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Housing_TypesHousing_TypesHousingId",
                        column: x => x.TypesHousingId,
                        principalTable: "TypesHousing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "CustomerPhone",
                columns: table => new
                {
                    CustomerPhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhone", x => x.CustomerPhoneId);
                    table.ForeignKey(
                        name: "FK_CustomerPhone_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "HousingPhone",
                columns: table => new
                {
                    HousingPhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HousingId = table.Column<int>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousingPhone", x => x.HousingPhoneId);
                    table.ForeignKey(
                        name: "FK_HousingPhone_Housing_HousingId",
                        column: x => x.HousingId,
                        principalTable: "Housing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddColumn<bool>(
                name: "IsArchieved",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Sms",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Call",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Blacklist",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Blacklist_ApplicationUser_ApplicationUserId",
                table: "Blacklist",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Call_ApplicationUser_ApplicationUserId",
                table: "Call",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_DistrictToСlient_Customer_ClientId",
                table: "DistrictToСlient",
                column: "ClientId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Blacklist_ApplicationUser_ApplicationUserId", table: "Blacklist");
            migrationBuilder.DropForeignKey(name: "FK_Call_ApplicationUser_ApplicationUserId", table: "Call");
            migrationBuilder.DropForeignKey(name: "FK_DistrictToСlient_Customer_ClientId", table: "DistrictToСlient");
            migrationBuilder.DropColumn(name: "IsArchieved", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "ClientId", table: "Sms");
            migrationBuilder.DropColumn(name: "ApplicationUserId", table: "Call");
            migrationBuilder.DropColumn(name: "ApplicationUserId", table: "Blacklist");
            migrationBuilder.DropTable("CustomerPhone");
            migrationBuilder.DropTable("HousingPhone");
            migrationBuilder.DropTable("Customer");
            migrationBuilder.DropTable("Housing");
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apartment = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    DistrictId = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    House = table.Column<string>(nullable: true),
                    Housing = table.Column<string>(nullable: true),
                    InDate = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    MidleName = table.Column<string>(nullable: true),
                    PartherShip = table.Column<int>(nullable: false),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    Phone3 = table.Column<string>(nullable: true),
                    RevisionDate = table.Column<DateTime>(nullable: false),
                    StreetId = table.Column<int>(nullable: false),
                    Sum = table.Column<double>(nullable: false),
                    TypesHousingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Building_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Building_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Building_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Building_Street_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Street",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Building_TypesHousing_TypesHousingId",
                        column: x => x.TypesHousingId,
                        principalTable: "TypesHousing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    ContractSum = table.Column<int>(nullable: false),
                    DateClosed = table.Column<DateTime>(nullable: false),
                    DateContract = table.Column<DateTime>(nullable: false),
                    DateIn = table.Column<DateTime>(nullable: false),
                    DateMeeting = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    IsSite = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    MaxSum = table.Column<int>(nullable: false),
                    MidleName = table.Column<string>(nullable: true),
                    MinSum = table.Column<int>(nullable: false),
                    PhoneNumber1 = table.Column<string>(nullable: true),
                    PhoneNumber2 = table.Column<string>(nullable: true),
                    PhoneNumber3 = table.Column<string>(nullable: true),
                    ReheshSum = table.Column<int>(nullable: false),
                    Resource = table.Column<string>(nullable: true),
                    SmsId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Client_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Client_Sms_SmsId",
                        column: x => x.SmsId,
                        principalTable: "Sms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Street",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_DistrictToСlient_Client_ClientId",
                table: "DistrictToСlient",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Street_District_DistrictId",
                table: "Street",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
