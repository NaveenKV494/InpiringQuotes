using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InspiringQuotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class deleteincluded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteTag_Quotes_QuotesId",
                table: "QuoteTag");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteTag_Tag_TagsTagId",
                table: "QuoteTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "QuotesId",
                table: "QuoteTag",
                newName: "QuotesQuoteId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Quotes",
                newName: "QuoteId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Quotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteTag_Quotes_QuotesQuoteId",
                table: "QuoteTag",
                column: "QuotesQuoteId",
                principalTable: "Quotes",
                principalColumn: "QuoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteTag_Tags_TagsTagId",
                table: "QuoteTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteTag_Quotes_QuotesQuoteId",
                table: "QuoteTag");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteTag_Tags_TagsTagId",
                table: "QuoteTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Quotes");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameColumn(
                name: "QuotesQuoteId",
                table: "QuoteTag",
                newName: "QuotesId");

            migrationBuilder.RenameColumn(
                name: "QuoteId",
                table: "Quotes",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteTag_Quotes_QuotesId",
                table: "QuoteTag",
                column: "QuotesId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteTag_Tag_TagsTagId",
                table: "QuoteTag",
                column: "TagsTagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
