namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionsPage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.questions", "dependent_question_id");
            RenameColumn(table: "dbo.questions", name: "questions_id", newName: "dependent_question_id");
            RenameIndex(table: "dbo.questions", name: "IX_questions_id", newName: "IX_dependent_question_id");
            AddColumn("dbo.questions", "page", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.questions", "page");
            RenameIndex(table: "dbo.questions", name: "IX_dependent_question_id", newName: "IX_questions_id");
            RenameColumn(table: "dbo.questions", name: "dependent_question_id", newName: "questions_id");
            AddColumn("dbo.questions", "dependent_question_id", c => c.Int());
        }
    }
}
