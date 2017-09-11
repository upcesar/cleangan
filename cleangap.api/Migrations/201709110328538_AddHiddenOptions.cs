namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHiddenOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.question_options", "hide_option", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.question_options", "hide_option");
        }
    }
}
