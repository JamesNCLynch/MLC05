namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropClassAttendee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduledClassClassAttendees", "ScheduledClass_Id", "dbo.ScheduledClasses");
            DropForeignKey("dbo.ScheduledClassClassAttendees", "ClassAttendee_Id", "dbo.ClassAttendees");
            DropForeignKey("dbo.ApplicationUserClassAttendees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserClassAttendees", "ClassAttendee_Id", "dbo.ClassAttendees");
            DropIndex("dbo.ScheduledClassClassAttendees", new[] { "ScheduledClass_Id" });
            DropIndex("dbo.ScheduledClassClassAttendees", new[] { "ClassAttendee_Id" });
            DropIndex("dbo.ApplicationUserClassAttendees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClassAttendees", new[] { "ClassAttendee_Id" });
            DropTable("dbo.ClassAttendees");
            DropTable("dbo.ScheduledClassClassAttendees");
            DropTable("dbo.ApplicationUserClassAttendees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserClassAttendees",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ClassAttendee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ClassAttendee_Id });
            
            CreateTable(
                "dbo.ScheduledClassClassAttendees",
                c => new
                    {
                        ScheduledClass_Id = c.String(nullable: false, maxLength: 128),
                        ClassAttendee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScheduledClass_Id, t.ClassAttendee_Id });
            
            CreateTable(
                "dbo.ClassAttendees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ApplicationUserClassAttendees", "ClassAttendee_Id");
            CreateIndex("dbo.ApplicationUserClassAttendees", "ApplicationUser_Id");
            CreateIndex("dbo.ScheduledClassClassAttendees", "ClassAttendee_Id");
            CreateIndex("dbo.ScheduledClassClassAttendees", "ScheduledClass_Id");
            AddForeignKey("dbo.ApplicationUserClassAttendees", "ClassAttendee_Id", "dbo.ClassAttendees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserClassAttendees", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScheduledClassClassAttendees", "ClassAttendee_Id", "dbo.ClassAttendees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScheduledClassClassAttendees", "ScheduledClass_Id", "dbo.ScheduledClasses", "Id", cascadeDelete: true);
        }
    }
}
