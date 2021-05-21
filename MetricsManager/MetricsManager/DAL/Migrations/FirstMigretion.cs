using FluentMigrator;

namespace MetricsManager.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigretion : Migration
    {
        public override void Down()
        {
            Delete.Table("agents");
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
        }

        public override void Up()
        {
            Create.Table("agents")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("name").AsString();
            Create.Table("cpumetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
            Create.Table("dotnetmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
            Create.Table("hddmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
            Create.Table("networkmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
            Create.Table("rammetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
        }
    }
}
