namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedirectSummary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.customers", "redirect_summary", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.customers", "redirect_summary");
        }
    }
}
