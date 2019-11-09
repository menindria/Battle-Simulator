using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleSimulator.Infrastructure.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Started = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Army",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumberOfAttacks = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    NumberOfUnits = table.Column<int>(nullable: false),
                    StrategyAndAttackOption = table.Column<int>(nullable: false),
                    BattleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Army", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Army_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BattleId = table.Column<int>(nullable: false),
                    OffensiveArmyId = table.Column<int>(nullable: false),
                    DefensiveArmyId = table.Column<int>(nullable: true),
                    LogType = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Log_Army_DefensiveArmyId",
                        column: x => x.DefensiveArmyId,
                        principalTable: "Army",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Army_OffensiveArmyId",
                        column: x => x.OffensiveArmyId,
                        principalTable: "Army",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Army_BattleId",
                table: "Army",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_BattleId",
                table: "Log",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_DefensiveArmyId",
                table: "Log",
                column: "DefensiveArmyId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_OffensiveArmyId",
                table: "Log",
                column: "OffensiveArmyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Army");

            migrationBuilder.DropTable(
                name: "Battle");
        }
    }
}
