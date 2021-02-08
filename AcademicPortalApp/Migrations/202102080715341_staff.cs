namespace AcademicPortalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StaffName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "StaffName");
        }
    }
}
