namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassAttendee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassAttendees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserClassAttendees",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ClassAttendee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ClassAttendee_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.ClassAttendees", t => t.ClassAttendee_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ClassAttendee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserClassAttendees", "ClassAttendee_Id", "dbo.ClassAttendees");
            DropForeignKey("dbo.ApplicationUserClassAttendees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserClassAttendees", new[] { "ClassAttendee_Id" });
            DropIndex("dbo.ApplicationUserClassAttendees", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserClassAttendees");
            DropTable("dbo.ClassAttendees");
        }
    }
}
