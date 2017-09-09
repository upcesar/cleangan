namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHiddenQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.questions", "hide_question", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.questions", "hide_question");
        }
    }
}
