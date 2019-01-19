namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateCreated", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DateModified", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "ModifiedBy", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "ModifiedBy");
            DropColumn("dbo.AspNetUsers", "DateModified");
            DropColumn("dbo.AspNetUsers", "DateCreated");
        }
    }
}
