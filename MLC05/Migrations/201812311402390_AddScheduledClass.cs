namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScheduledClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduledClasses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ScheduledClassName = c.String(),
                        InstructorId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.InstructorId_Id)
                .Index(t => t.InstructorId_Id);
            
            CreateTable(
                "dbo.ScheduledClassClassAttendees",
                c => new
                    {
                        ScheduledClass_Id = c.String(nullable: false, maxLength: 128),
                        ClassAttendee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScheduledClass_Id, t.ClassAttendee_Id })
                .ForeignKey("dbo.ScheduledClasses", t => t.ScheduledClass_Id, cascadeDelete: true)
                .ForeignKey("dbo.ClassAttendees", t => t.ClassAttendee_Id, cascadeDelete: true)
                .Index(t => t.ScheduledClass_Id)
                .Index(t => t.ClassAttendee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduledClasses", "InstructorId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ScheduledClassClassAttendees", "ClassAttendee_Id", "dbo.ClassAttendees");
            DropForeignKey("dbo.ScheduledClassClassAttendees", "ScheduledClass_Id", "dbo.ScheduledClasses");
            DropIndex("dbo.ScheduledClassClassAttendees", new[] { "ClassAttendee_Id" });
            DropIndex("dbo.ScheduledClassClassAttendees", new[] { "ScheduledClass_Id" });
            DropIndex("dbo.ScheduledClasses", new[] { "InstructorId_Id" });
            DropTable("dbo.ScheduledClassClassAttendees");
            DropTable("dbo.ScheduledClasses");
        }
    }
}
