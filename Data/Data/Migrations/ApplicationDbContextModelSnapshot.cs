using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApp.Models;
using WebApp.Entities;

namespace Web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApp.Entities.Blacklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DateAdd");

                    b.Property<string>("Description");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("BlackLists");
                });

            modelBuilder.Entity("WebApp.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WebApp.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("CityId");

                    b.Property<int>("ContractSum");

                    b.Property<string>("CustomerUserId");

                    b.Property<DateTime>("DateClosed");

                    b.Property<DateTime>("DateContract");

                    b.Property<DateTime>("DateIn");

                    b.Property<DateTime>("DateMeeting");

                    b.Property<string>("FirstName");

                    b.Property<int?>("Gender");

                    b.Property<bool>("IsArchive");

                    b.Property<bool>("IsSiteAccess");

                    b.Property<string>("LastName");

                    b.Property<int>("MaxSum");

                    b.Property<string>("MidleName");

                    b.Property<int>("MinSum");

                    b.Property<int>("ReheshSum");

                    b.Property<string>("Resource");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CityId");

                    b.HasIndex("CustomerUserId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WebApp.Entities.CustomerCall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("CustomeCalls");
                });

            modelBuilder.Entity("WebApp.Entities.CustomerPhone", b =>
                {
                    b.Property<int>("CustomerPhoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Number");

                    b.Property<int>("Order");

                    b.HasKey("CustomerPhoneId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerPhone");
                });

            modelBuilder.Entity("WebApp.Entities.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("WebApp.Entities.DistrictToСlient", b =>
                {
                    b.Property<int>("ClientId");

                    b.Property<int>("DistrictId");

                    b.HasKey("ClientId", "DistrictId");

                    b.HasIndex("DistrictId");

                    b.ToTable("DistrictToСlient");
                });

            modelBuilder.Entity("WebApp.Entities.Housing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Building");

                    b.Property<int>("CityId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Currency");

                    b.Property<int>("DistrictId");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("House");

                    b.Property<DateTime>("InDate");

                    b.Property<bool>("IsArchive");

                    b.Property<DateTime?>("LastEditedAt");

                    b.Property<string>("LastName");

                    b.Property<string>("MidleName");

                    b.Property<bool>("PartherShip");

                    b.Property<DateTime>("RevisionDate");

                    b.Property<string>("Room");

                    b.Property<int>("StreetId");

                    b.Property<double>("Sum");

                    b.Property<int>("TypesHousingId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("StreetId");

                    b.HasIndex("TypesHousingId");

                    b.ToTable("Housing");
                });

            modelBuilder.Entity("WebApp.Entities.HousingCall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("HousingId");

                    b.Property<string>("Status");

                    b.Property<int>("StatusType");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("HousingId");

                    b.ToTable("HousingCalls");
                });

            modelBuilder.Entity("WebApp.Entities.HousingPhone", b =>
                {
                    b.Property<int>("HousingPhoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HousingId");

                    b.Property<string>("Number");

                    b.Property<int>("Order");

                    b.HasKey("HousingPhoneId");

                    b.HasIndex("HousingId");

                    b.ToTable("HousingPhone");
                });

            modelBuilder.Entity("WebApp.Entities.Sms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Smses");
                });

            modelBuilder.Entity("WebApp.Entities.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("WebApp.Entities.TypesHousing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TypesHousing");
                });

            modelBuilder.Entity("WebApp.Entities.TypesHousingToCustomer", b =>
                {
                    b.Property<int>("ClientId");

                    b.Property<int>("TypesHousingId");

                    b.HasKey("ClientId", "TypesHousingId");

                    b.HasIndex("TypesHousingId");

                    b.ToTable("TypesHousingToCustomer");
                });

            modelBuilder.Entity("WebApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("CityId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FIO")
                        .HasMaxLength(255);

                    b.Property<bool>("IsArchieved");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("OpenPassword")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Entities.Blacklist", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("WebApp.Entities.Customer", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("WebApp.Entities.City", "City")
                        .WithMany("Clients")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Models.ApplicationUser", "CustomerAccount")
                        .WithMany()
                        .HasForeignKey("CustomerUserId");
                });

            modelBuilder.Entity("WebApp.Entities.CustomerCall", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("WebApp.Entities.CustomerPhone", b =>
                {
                    b.HasOne("WebApp.Entities.Customer")
                        .WithMany("Phones")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("WebApp.Entities.District", b =>
                {
                    b.HasOne("WebApp.Entities.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("WebApp.Entities.DistrictToСlient", b =>
                {
                    b.HasOne("WebApp.Entities.Customer", "Clients")
                        .WithMany("DistrictToClients")
                        .HasForeignKey("ClientId");

                    b.HasOne("WebApp.Entities.District", "Districts")
                        .WithMany("DistrictToСlients")
                        .HasForeignKey("DistrictId");
                });

            modelBuilder.Entity("WebApp.Entities.Housing", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("WebApp.Entities.City", "City")
                        .WithMany("Buildings")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Entities.District", "District")
                        .WithMany("Buildings")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Entities.Street", "Street")
                        .WithMany("Buildings")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Entities.TypesHousing", "TypesHousing")
                        .WithMany()
                        .HasForeignKey("TypesHousingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Entities.HousingCall", b =>
                {
                    b.HasOne("WebApp.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("WebApp.Entities.Housing", "Housing")
                        .WithMany("Calls")
                        .HasForeignKey("HousingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Entities.HousingPhone", b =>
                {
                    b.HasOne("WebApp.Entities.Housing")
                        .WithMany("Phones")
                        .HasForeignKey("HousingId");
                });

            modelBuilder.Entity("WebApp.Entities.Sms", b =>
                {
                    b.HasOne("WebApp.Entities.Customer", "Client")
                        .WithMany("Smses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Entities.Street", b =>
                {
                    b.HasOne("WebApp.Entities.City", "City")
                        .WithMany("Streets")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("WebApp.Entities.TypesHousingToCustomer", b =>
                {
                    b.HasOne("WebApp.Entities.Customer", "Clients")
                        .WithMany("TypesHousingToCustomers")
                        .HasForeignKey("ClientId");

                    b.HasOne("WebApp.Entities.TypesHousing", "TypesHousing")
                        .WithMany("TypesHousingToCustomers")
                        .HasForeignKey("TypesHousingId");
                });

            modelBuilder.Entity("WebApp.Models.ApplicationUser", b =>
                {
                    b.HasOne("WebApp.Entities.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId");
                });
        }
    }
}
