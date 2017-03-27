namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValueListQuestionOpt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.question_options", "values_list", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.question_options", "values_list");
        }
    }
}
