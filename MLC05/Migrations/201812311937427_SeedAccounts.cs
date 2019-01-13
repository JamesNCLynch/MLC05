namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAccounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduledClasses", "ClassStartTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduledClasses", "ClassStartTime");
        }
    }
}
