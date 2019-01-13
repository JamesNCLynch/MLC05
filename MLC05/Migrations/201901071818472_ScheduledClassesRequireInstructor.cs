namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduledClassesRequireInstructor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduledClasses", "InstructorId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ScheduledClasses", new[] { "InstructorId_Id" });
            RenameColumn(table: "dbo.ScheduledClasses", name: "InstructorId_Id", newName: "Instructor_Id");
            AddColumn("dbo.ScheduledClasses", "IsCancelled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ScheduledClasses", "Instructor_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ScheduledClasses", "Instructor_Id");
            AddForeignKey("dbo.ScheduledClasses", "Instructor_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduledClasses", "Instructor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ScheduledClasses", new[] { "Instructor_Id" });
            AlterColumn("dbo.ScheduledClasses", "Instructor_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.ScheduledClasses", "IsCancelled");
            RenameColumn(table: "dbo.ScheduledClasses", name: "Instructor_Id", newName: "InstructorId_Id");
            CreateIndex("dbo.ScheduledClasses", "InstructorId_Id");
            AddForeignKey("dbo.ScheduledClasses", "InstructorId_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
