namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendClassType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduledClassTypes", "Difficulty", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduledClassTypes", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduledClassTypes", "Description");
            DropColumn("dbo.ScheduledClassTypes", "Difficulty");
        }
    }
}
