using FluentMigrator;
using FluentMigrator.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRA.News.Web.Migrations
{
    [Migration(202303030001)]
    public class InitialTables_202303030001 : Migration
    {

        public override void Down()
        {
            Delete.Table("Articles");
        }
        public override void Up()
        {
            Create.Table("Articles")
                .WithColumn("Id").AsInt32().Identity(100, 1).NotNullable().PrimaryKey()
                .WithColumn("Author").AsString(int.MaxValue).Nullable()
                .WithColumn("Title").AsString(int.MaxValue).Nullable()
                .WithColumn("Description").AsString(int.MaxValue).Nullable()
                .WithColumn("Content").AsString(int.MaxValue).Nullable()
                .WithColumn("Url").AsString(int.MaxValue).Nullable()
                .WithColumn("UrlToImage").AsString(int.MaxValue).Nullable()
                .WithColumn("PublishedAt").AsString(int.MaxValue).Nullable()
                .WithColumn("Language").AsString(2).Nullable();
        }
    }
}
