using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentRegistry.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgentTypes",
                columns: table => new
                {
                    AgentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentTypeName = table.Column<string>(nullable: false),
                    AgentTypeDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTypes", x => x.AgentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    ExceptionLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ErrorMessage = table.Column<string>(nullable: false),
                    StackTrace = table.Column<string>(nullable: false),
                    InnerExceptionMessage = table.Column<string>(nullable: true),
                    InnerExceptionStackTrace = table.Column<string>(nullable: true),
                    DateTimeLogging = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.ExceptionLogId);
                });

            migrationBuilder.CreateTable(
                name: "ScannerLogs",
                columns: table => new
                {
                    ScannerLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTimeScanStart = table.Column<DateTime>(nullable: false),
                    DateTimeScanEnd = table.Column<DateTime>(nullable: true),
                    IsSuccess = table.Column<bool>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScannerLogs", x => x.ScannerLogId);
                });

            migrationBuilder.CreateTable(
                name: "AgentCommands",
                columns: table => new
                {
                    AgentCommandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentTypeId = table.Column<int>(nullable: false),
                    CommandName = table.Column<string>(nullable: false),
                    CommandDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentCommands", x => x.AgentCommandId);
                    table.ForeignKey(
                        name: "FK_AgentCommands_AgentTypes_AgentTypeId",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentTypes",
                        principalColumn: "AgentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScannerLogId = table.Column<int>(nullable: false),
                    AgentTypeId = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: false),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agents_AgentTypes_AgentTypeId",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentTypes",
                        principalColumn: "AgentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agents_ScannerLogs_ScannerLogId",
                        column: x => x.ScannerLogId,
                        principalTable: "ScannerLogs",
                        principalColumn: "ScannerLogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentsCommunicationLogs",
                columns: table => new
                {
                    AgentCommunicationLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentFromId = table.Column<int>(nullable: false),
                    AgentToId = table.Column<int>(nullable: false),
                    CommandId = table.Column<int>(nullable: false),
                    DateTimeCommunication = table.Column<DateTime>(nullable: false),
                    IsSuccess = table.Column<bool>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsCommunicationLogs", x => x.AgentCommunicationLogId);
                    table.ForeignKey(
                        name: "FK_AgentsCommunicationLogs_Agents_AgentFromId",
                        column: x => x.AgentFromId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AgentsCommunicationLogs_Agents_AgentToId",
                        column: x => x.AgentToId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AgentsCommunicationLogs_AgentCommands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "AgentCommands",
                        principalColumn: "AgentCommandId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentCommands_AgentTypeId",
                table: "AgentCommands",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgentTypeId",
                table: "Agents",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ScannerLogId",
                table: "Agents",
                column: "ScannerLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentsCommunicationLogs_AgentFromId",
                table: "AgentsCommunicationLogs",
                column: "AgentFromId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentsCommunicationLogs_AgentToId",
                table: "AgentsCommunicationLogs",
                column: "AgentToId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentsCommunicationLogs_CommandId",
                table: "AgentsCommunicationLogs",
                column: "CommandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentsCommunicationLogs");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "AgentCommands");

            migrationBuilder.DropTable(
                name: "ScannerLogs");

            migrationBuilder.DropTable(
                name: "AgentTypes");
        }
    }
}
