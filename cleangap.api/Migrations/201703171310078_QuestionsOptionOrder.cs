namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionsOptionOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.question_options", "order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.question_options", "order");
        }
    }
}
