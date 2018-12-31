namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassTypeandTimetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduledClassTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassTimetables",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Weekday = c.Int(nullable: false),
                        ScheduledClassType_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScheduledClassTypes", t => t.ScheduledClassType_Id)
                .Index(t => t.ScheduledClassType_Id);
            
            AddColumn("dbo.ScheduledClasses", "ScheduledClassType_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ScheduledClasses", "ScheduledClassType_Id");
            AddForeignKey("dbo.ScheduledClasses", "ScheduledClassType_Id", "dbo.ScheduledClassTypes", "Id");
            DropColumn("dbo.ScheduledClasses", "ScheduledClassName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScheduledClasses", "ScheduledClassName", c => c.String());
            DropForeignKey("dbo.ClassTimetables", "ScheduledClassType_Id", "dbo.ScheduledClassTypes");
            DropForeignKey("dbo.ScheduledClasses", "ScheduledClassType_Id", "dbo.ScheduledClassTypes");
            DropIndex("dbo.ClassTimetables", new[] { "ScheduledClassType_Id" });
            DropIndex("dbo.ScheduledClasses", new[] { "ScheduledClassType_Id" });
            DropColumn("dbo.ScheduledClasses", "ScheduledClassType_Id");
            DropTable("dbo.ClassTimetables");
            DropTable("dbo.ScheduledClassTypes");
        }
    }
}
