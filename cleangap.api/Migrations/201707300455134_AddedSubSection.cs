namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.questions", "id_subsection", c => c.Int());
            CreateIndex("dbo.questions", "id_subsection");
            AddForeignKey("dbo.questions", "id_subsection", "dbo.question_sections", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.questions", "id_subsection", "dbo.question_sections");
            DropIndex("dbo.questions", new[] { "id_subsection" });
            DropColumn("dbo.questions", "id_subsection");
        }
    }
}
