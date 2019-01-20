namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassAttendances",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EnrolledDate = c.DateTime(nullable: false),
                        NoShow = c.Boolean(nullable: false),
                        Attendee_Id = c.String(maxLength: 128),
                        ScheduledClasses_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Attendee_Id)
                .ForeignKey("dbo.ScheduledClasses", t => t.ScheduledClasses_Id)
                .Index(t => t.Attendee_Id)
                .Index(t => t.ScheduledClasses_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassAttendances", "ScheduledClasses_Id", "dbo.ScheduledClasses");
            DropForeignKey("dbo.ClassAttendances", "Attendee_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClassAttendances", new[] { "ScheduledClasses_Id" });
            DropIndex("dbo.ClassAttendances", new[] { "Attendee_Id" });
            DropTable("dbo.ClassAttendances");
        }
    }
}
