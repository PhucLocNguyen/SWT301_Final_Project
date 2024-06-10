using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterGemstone",
                columns: table => new
                {
                    MasterGemstoneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kind = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Clarity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Shape = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MasterGe__D4657CE325820E1C", x => x.MasterGemstoneID);
                });

            migrationBuilder.CreateTable(
                name: "Stones",
                columns: table => new
                {
                    StoneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kind = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", maxLength: 255, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stones__59F240A0F68BB9CA", x => x.StoneID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfJewellery",
                columns: table => new
                {
                    TypeOfJewelleryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TypeOfJe__F1D25D48390D573D", x => x.TypeOfJewelleryID);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyCard",
                columns: table => new
                {
                    WarrantyCardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Warranty__3C3D832A529B2241", x => x.WarrantyCardID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Blog__54379E50328EC178", x => x.BlogID);
                    table.ForeignKey(
                        name: "FK__Blog__ManagerID__3C69FB99",
                        column: x => x.ManagerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ManagerID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Material__C506131755BDA89F", x => x.MaterialID);
                    table.ForeignKey(
                        name: "FK__Material__Manage__412EB0B6",
                        column: x => x.ManagerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DesignRule",
                columns: table => new
                {
                    DesignRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinSizeMasterGemstone = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxSizeMasterGemstone = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinSizeJewellery = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxSizeJewellery = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TypeOfJewelleryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DesignRu__3850E3634E9D1DEE", x => x.DesignRuleId);
                    table.ForeignKey(
                        name: "FK__DesignRul__TypeO__5EBF139D",
                        column: x => x.TypeOfJewelleryID,
                        principalTable: "TypeOfJewellery",
                        principalColumn: "TypeOfJewelleryID");
                });

            migrationBuilder.CreateTable(
                name: "Design",
                columns: table => new
                {
                    DesignID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightOfMaterial = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StoneID = table.Column<int>(type: "int", nullable: true),
                    MasterGemstoneID = table.Column<int>(type: "int", nullable: true),
                    ManagerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TypeOfJewelleryID = table.Column<int>(type: "int", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Design__32B8E17FAAEB8C8B", x => x.DesignID);
                    table.ForeignKey(
                        name: "FK__Design__ManagerI__4AB81AF0",
                        column: x => x.ManagerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Design__MasterGe__49C3F6B7",
                        column: x => x.MasterGemstoneID,
                        principalTable: "MasterGemstone",
                        principalColumn: "MasterGemstoneID");
                    table.ForeignKey(
                        name: "FK__Design__Material__4CA06362",
                        column: x => x.MaterialID,
                        principalTable: "Material",
                        principalColumn: "MaterialID");
                    table.ForeignKey(
                        name: "FK__Design__ParentID__47DBAE45",
                        column: x => x.ParentID,
                        principalTable: "Design",
                        principalColumn: "DesignID");
                    table.ForeignKey(
                        name: "FK__Design__StoneID__48CFD27E",
                        column: x => x.StoneID,
                        principalTable: "Stones",
                        principalColumn: "StoneID");
                    table.ForeignKey(
                        name: "FK__Design__TypeOfJe__4BAC3F29",
                        column: x => x.TypeOfJewelleryID,
                        principalTable: "TypeOfJewellery",
                        principalColumn: "TypeOfJewelleryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    RequirementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExpectedDelivery = table.Column<DateOnly>(type: "date", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DesignID = table.Column<int>(type: "int", nullable: true),
                    Design3D = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GoldPriceAtMoment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StonePriceAtMoment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MachiningFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StaffNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Requirem__7DF11E7DA4E2ED57", x => x.RequirementID);
                    table.ForeignKey(
                        name: "FK__Requireme__Desig__4F7CD00D",
                        column: x => x.DesignID,
                        principalTable: "Design",
                        principalColumn: "DesignID");
                });

            migrationBuilder.CreateTable(
                name: "Have",
                columns: table => new
                {
                    WarrantyCardID = table.Column<int>(type: "int", nullable: false),
                    RequirementID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Have__FBE292CD11001214", x => new { x.WarrantyCardID, x.RequirementID });
                    table.ForeignKey(
                        name: "FK__Have__Requiremen__5535A963",
                        column: x => x.RequirementID,
                        principalTable: "Requirements",
                        principalColumn: "RequirementID");
                    table.ForeignKey(
                        name: "FK__Have__WarrantyCa__5441852A",
                        column: x => x.WarrantyCardID,
                        principalTable: "WarrantyCard",
                        principalColumn: "WarrantyCardID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequirementsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__9B556A584CDCE2CF", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payment__Custome__5BE2A6F2",
                        column: x => x.CustomerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Payment__Require__5CD6CB2B",
                        column: x => x.RequirementsID,
                        principalTable: "Requirements",
                        principalColumn: "RequirementID");
                });

            migrationBuilder.CreateTable(
                name: "UsersRequirement",
                columns: table => new
                {
                    UsersID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequirementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UsersReq__EC83D35F40A575F1", x => new { x.UsersID, x.RequirementID });
                    table.ForeignKey(
                        name: "FK__UsersRequ__Requi__52593CB8",
                        column: x => x.RequirementID,
                        principalTable: "Requirements",
                        principalColumn: "RequirementID");
                    table.ForeignKey(
                        name: "FK__UsersRequ__Users__5165187F",
                        column: x => x.UsersID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20936102-504f-4787-bc3b-810be16f6914", null, "Manager", "MANAGER" },
                    { "8b516022-520d-4bb7-bf7b-dcd1c23f8465", null, "Customer", "CUSTOMER" },
                    { "d423646f-2ab4-4430-83fc-1c385c077d0b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_ManagerID",
                table: "Blog",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Design_ManagerID",
                table: "Design",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Design_MasterGemstoneID",
                table: "Design",
                column: "MasterGemstoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Design_MaterialID",
                table: "Design",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Design_ParentID",
                table: "Design",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Design_StoneID",
                table: "Design",
                column: "StoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Design_TypeOfJewelleryID",
                table: "Design",
                column: "TypeOfJewelleryID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignRule_TypeOfJewelleryID",
                table: "DesignRule",
                column: "TypeOfJewelleryID");

            migrationBuilder.CreateIndex(
                name: "IX_Have_RequirementID",
                table: "Have",
                column: "RequirementID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_ManagerID",
                table: "Material",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CustomerID",
                table: "Payment",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RequirementsID",
                table: "Payment",
                column: "RequirementsID");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_DesignID",
                table: "Requirements",
                column: "DesignID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRequirement_RequirementID",
                table: "UsersRequirement",
                column: "RequirementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "DesignRule");

            migrationBuilder.DropTable(
                name: "Have");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "UsersRequirement");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "WarrantyCard");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "Design");

            migrationBuilder.DropTable(
                name: "MasterGemstone");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Stones");

            migrationBuilder.DropTable(
                name: "TypeOfJewellery");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
