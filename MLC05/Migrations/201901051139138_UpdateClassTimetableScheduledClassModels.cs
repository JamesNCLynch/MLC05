namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassTimetableScheduledClassModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduledClassTypes", "ClassColour", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduledClassTypes", "ClassColour");
        }
    }
}
