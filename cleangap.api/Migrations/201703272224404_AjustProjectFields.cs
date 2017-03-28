namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustProjectFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.answers", "id_project", c => c.Int());
            CreateIndex("dbo.answers", "id_project");
            AddForeignKey("dbo.answers", "id_project", "dbo.projects", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.answers", "id_project", "dbo.projects");
            DropIndex("dbo.answers", new[] { "id_project" });
            DropColumn("dbo.answers", "id_project");
        }
    }
}
