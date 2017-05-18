namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProjectFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.projects", "full_name", c => c.String());
            AddColumn("dbo.projects", "sign_date", c => c.DateTime());
            AddColumn("dbo.projects", "digital_signature", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.projects", "digital_signature");
            DropColumn("dbo.projects", "sign_date");
            DropColumn("dbo.projects", "full_name");
        }
    }
}
