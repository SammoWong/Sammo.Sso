using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sammo.Sso.Infrastructure.Data.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    ClientId = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    ClientName = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    ClientSecrets = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    RedirectUris = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    DisplayName = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Remark = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    MasterType = table.Column<byte>(nullable: false, defaultValue: (byte)1),
                    MasterId = table.Column<string>(type: "CHAR(36)", nullable: true),
                    AccessType = table.Column<byte>(nullable: false, defaultValue: (byte)1),
                    AccessId = table.Column<string>(type: "CHAR(36)", nullable: true),
                    SortNumber = table.Column<int>(nullable: false, defaultValue: 1),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Code = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    NickName = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    RealName = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    IdCard = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Mobile = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Salt = table.Column<string>(type: "VARCHAR(64)", nullable: true),
                    Gender = table.Column<byte>(nullable: true),
                    Birthday = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Avatar = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    ParentId = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Code = table.Column<string>(type: "VARCHAR(100)", maxLength: 32, nullable: true),
                    Category = table.Column<byte>(nullable: false, defaultValue: (byte)1),
                    Url = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    Icon = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Rank = table.Column<short>(nullable: false, defaultValue: (short)1),
                    SortNumber = table.Column<int>(nullable: false, defaultValue: 1),
                    IsExpanded = table.Column<bool>(nullable: false, defaultValue: false),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Remark = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    RoleId = table.Column<Guid>(type: "CHAR(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Button",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    MenuId = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Code = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    JsEvent = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Url = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    Icon = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    SortNumber = table.Column<int>(nullable: false, defaultValue: 1),
                    Remark = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Button", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Button_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Button_MenuId",
                table: "Button",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ApplicationId",
                table: "Menu",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Button");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Application");
        }
    }
}
