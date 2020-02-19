using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hastnama.Ekipchi.DataAccess.Migrations
{
    public partial class addUserWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(nullable: false),
                    BankTransaction = table.Column<bool>(nullable: false),
                    IsDeposit = table.Column<bool>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BalanceBefore = table.Column<double>(nullable: false),
                    BalanceAfter = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    TeTransactionStatus = table.Column<int>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    PayerId = table.Column<Guid>(nullable: false),
                    ReceiverId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_Users_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserWallets",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    TotalDeposit = table.Column<double>(nullable: false),
                    TotalSpend = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    Income = table.Column<double>(nullable: false),
                    Takeable = table.Column<double>(nullable: false),
                    TotalWithDraw = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWallets", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserWallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    RequestId = table.Column<string>(nullable: true),
                    VerificationStatus = table.Column<int>(nullable: true),
                    ReferenceId = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_FinancialTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "FinancialTransactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_AuthorId",
                table: "FinancialTransactions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_PayerId",
                table: "FinancialTransactions",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_ReceiverId",
                table: "FinancialTransactions",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "UserWallets");

            migrationBuilder.DropTable(
                name: "FinancialTransactions");
        }
    }
}
