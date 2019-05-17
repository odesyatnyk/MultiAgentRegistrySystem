using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentRegistry.DataAccess.Migrations
{
    public partial class AddResponseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommandResponseId",
                table: "AgentsCommunicationLogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgentCommandResponses",
                columns: table => new
                {
                    AgentCommandResponseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentCommandId = table.Column<int>(nullable: false),
                    ResponseCode = table.Column<string>(nullable: false),
                    ResponseCodeDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentCommandResponses", x => x.AgentCommandResponseId);
                    table.ForeignKey(
                        name: "FK_AgentCommandResponses_AgentCommands_AgentCommandId",
                        column: x => x.AgentCommandId,
                        principalTable: "AgentCommands",
                        principalColumn: "AgentCommandId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentsCommunicationLogs_CommandResponseId",
                table: "AgentsCommunicationLogs",
                column: "CommandResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentCommandResponses_AgentCommandId",
                table: "AgentCommandResponses",
                column: "AgentCommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsCommunicationLogs_AgentCommandResponses_CommandResponseId",
                table: "AgentsCommunicationLogs",
                column: "CommandResponseId",
                principalTable: "AgentCommandResponses",
                principalColumn: "AgentCommandResponseId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentsCommunicationLogs_AgentCommandResponses_CommandResponseId",
                table: "AgentsCommunicationLogs");

            migrationBuilder.DropTable(
                name: "AgentCommandResponses");

            migrationBuilder.DropIndex(
                name: "IX_AgentsCommunicationLogs_CommandResponseId",
                table: "AgentsCommunicationLogs");

            migrationBuilder.DropColumn(
                name: "CommandResponseId",
                table: "AgentsCommunicationLogs");
        }
    }
}
