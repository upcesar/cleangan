namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFieldDepQuestion : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.questions", name: "dependent_question_id", newName: "parent_question_id");
            RenameIndex(table: "dbo.questions", name: "IX_dependent_question_id", newName: "IX_parent_question_id");
            AddColumn("dbo.questions", "parent_answer_value", c => c.String());
            DropColumn("dbo.questions", "dependent_answer_value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.questions", "dependent_answer_value", c => c.String());
            DropColumn("dbo.questions", "parent_answer_value");
            RenameIndex(table: "dbo.questions", name: "IX_parent_question_id", newName: "IX_dependent_question_id");
            RenameColumn(table: "dbo.questions", name: "parent_question_id", newName: "dependent_question_id");
        }
    }
}
