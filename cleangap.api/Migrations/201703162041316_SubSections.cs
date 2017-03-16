namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubSections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.surveys", "edition_date", c => c.DateTime());
            AddColumn("dbo.question_sections", "description", c => c.String());
            AddColumn("dbo.question_sections", "parent_section_id", c => c.Int());
            AddColumn("dbo.questions", "dependent_answer_value", c => c.String());
            AddColumn("dbo.questions", "questions_id", c => c.Int());
            CreateIndex("dbo.question_sections", "parent_section_id");
            CreateIndex("dbo.questions", "questions_id");
            AddForeignKey("dbo.question_sections", "parent_section_id", "dbo.question_sections", "id");
            AddForeignKey("dbo.questions", "questions_id", "dbo.questions", "id");
            DropColumn("dbo.questions", "dependent_answer_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.questions", "dependent_answer_id", c => c.Int());
            DropForeignKey("dbo.questions", "questions_id", "dbo.questions");
            DropForeignKey("dbo.question_sections", "parent_section_id", "dbo.question_sections");
            DropIndex("dbo.questions", new[] { "questions_id" });
            DropIndex("dbo.question_sections", new[] { "parent_section_id" });
            DropColumn("dbo.questions", "questions_id");
            DropColumn("dbo.questions", "dependent_answer_value");
            DropColumn("dbo.question_sections", "parent_section_id");
            DropColumn("dbo.question_sections", "description");
            DropColumn("dbo.surveys", "edition_date");
        }
    }
}
