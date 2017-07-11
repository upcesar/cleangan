namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnswerIdxRepeaterField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.answers", "index_repeater", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.answers", "index_repeater");
        }
    }
}
