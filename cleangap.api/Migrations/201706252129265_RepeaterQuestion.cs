namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepeaterQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.questions", "has_repeater", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.questions", "has_repeater");
        }
    }
}
