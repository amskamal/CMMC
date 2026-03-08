using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMMS.Migrations
{
    public partial class CreatingDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DedpartmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorFullName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VendorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "Engineers",
                columns: table => new
                {
                    EngId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngFullName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EngEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineers", x => x.EngId);
                    table.ForeignKey(
                        name: "FK_Engineers_Departments_Department ID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFullName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Departments_Department ID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    EqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EqSerialNo = table.Column<int>(type: "int", nullable: false),
                    EqName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitingTime = table.Column<DateTime>(name: "Visiting Time", type: "datetime2", nullable: false),
                    EqQuantity = table.Column<int>(type: "int", nullable: false),
                    EqCost = table.Column<int>(type: "int", nullable: false),
                    EqWarrantyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EqCurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorID = table.Column<int>(name: "Vendor ID", type: "int", nullable: false),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EqId);
                    table.ForeignKey(
                        name: "FK_Equipments_Departments_Department ID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Equipments_Vendors_Vendor ID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractCost = table.Column<double>(type: "float", nullable: false),
                    EquipmentID = table.Column<int>(name: "Equipment ID", type: "int", nullable: false),
                    VendorID = table.Column<int>(name: "Vendor ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_contracts_Equipments_Equipment ID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EqId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_contracts_Vendors_Vendor ID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(name: "Equipment ID", type: "int", nullable: false),
                    VendorID = table.Column<int>(name: "Vendor ID", type: "int", nullable: false),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventories_Departments_Department ID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Inventories_Equipments_Equipment ID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EqId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Inventories_Vendors_Vendor ID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    MaintenaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenaceDoneOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaintenaceDueOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EngRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BreakDownDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BreakDownDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsingAfterRectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RectDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RectCost = table.Column<double>(type: "float", nullable: false),
                    RecEngRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecUserRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngPerformance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownTime = table.Column<int>(type: "int", nullable: false),
                    ResponseTime = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false),
                    EngineerID = table.Column<int>(name: "Engineer ID", type: "int", nullable: false),
                    UserID = table.Column<int>(name: "User ID", type: "int", nullable: false),
                    VendorID = table.Column<int>(name: "Vendor ID", type: "int", nullable: false),
                    EquipmentID = table.Column<int>(name: "Equipment ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.MaintenaceId);
                    table.ForeignKey(
                        name: "FK_Maintenances_Departments_Department ID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Maintenances_Engineers_Engineer ID",
                        column: x => x.EngineerID,
                        principalTable: "Engineers",
                        principalColumn: "EngId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Maintenances_Equipments_Equipment ID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EqId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Maintenances_Users_User ID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Maintenances_Vendors_Vendor ID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contracts_Equipment ID",
                table: "contracts",
                column: "Equipment ID");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_Vendor ID",
                table: "contracts",
                column: "Vendor ID");

            migrationBuilder.CreateIndex(
                name: "IX_Engineers_Department ID",
                table: "Engineers",
                column: "Department ID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Department ID",
                table: "Equipments",
                column: "Department ID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Vendor ID",
                table: "Equipments",
                column: "Vendor ID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Department ID",
                table: "Inventories",
                column: "Department ID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Equipment ID",
                table: "Inventories",
                column: "Equipment ID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Vendor ID",
                table: "Inventories",
                column: "Vendor ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_Department ID",
                table: "Maintenances",
                column: "Department ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_Engineer ID",
                table: "Maintenances",
                column: "Engineer ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_Equipment ID",
                table: "Maintenances",
                column: "Equipment ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_User ID",
                table: "Maintenances",
                column: "User ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_Vendor ID",
                table: "Maintenances",
                column: "Vendor ID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Department ID",
                table: "Users",
                column: "Department ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "Engineers");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
