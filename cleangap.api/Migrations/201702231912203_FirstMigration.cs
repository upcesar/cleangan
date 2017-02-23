namespace cleangap.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.answers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        answers_value = c.String(maxLength: 250, unicode: false),
                        id_question_option = c.Int(),
                        id_customer = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.customers", t => t.id_customer)
                .ForeignKey("dbo.question_options", t => t.id_question_option)
                .Index(t => t.id_question_option)
                .Index(t => t.id_customer);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        email = c.String(maxLength: 255, unicode: false),
                        password = c.String(maxLength: 32, unicode: false),
                        token_signin = c.String(maxLength: 64),
                        token_forgot_pass = c.String(maxLength: 64, unicode: false),
                        token_expire = c.DateTime(),
                        creation_date = c.DateTime(),
                        edition_date = c.DateTime(),
                        confirmation_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.surveys",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_section = c.Int(),
                        id_customer = c.Int(),
                        is_open = c.Boolean(),
                        project_status = c.Int(),
                        creation_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.question_sections", t => t.id_section)
                .ForeignKey("dbo.customers", t => t.id_customer)
                .Index(t => t.id_section)
                .Index(t => t.id_customer);
            
            CreateTable(
                "dbo.project_follow_up",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_survey = c.Int(),
                        id_staff = c.Int(),
                        staff_comments = c.String(unicode: false),
                        customer_comments = c.String(unicode: false),
                        url_attachment = c.String(maxLength: 255, unicode: false),
                        staff_inquiry_date = c.DateTime(),
                        cust_response_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.staff", t => t.id_staff)
                .ForeignKey("dbo.surveys", t => t.id_survey)
                .Index(t => t.id_survey)
                .Index(t => t.id_staff);
            
            CreateTable(
                "dbo.staff",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fullname = c.String(maxLength: 255, unicode: false),
                        phone = c.String(maxLength: 20, unicode: false),
                        email = c.String(maxLength: 255, unicode: false),
                        password = c.String(maxLength: 32, unicode: false),
                        id_leader = c.Int(),
                        is_leader = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.question_sections",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.questions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(),
                        id_section = c.Int(),
                        dependent_question_id = c.Int(),
                        dependent_answer_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.question_sections", t => t.id_section)
                .Index(t => t.id_section);
            
            CreateTable(
                "dbo.question_options",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        option_text = c.String(maxLength: 150, unicode: false),
                        input_type = c.String(maxLength: 10, fixedLength: true),
                        id_question = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.questions", t => t.id_question)
                .Index(t => t.id_question);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.surveys", "id_customer", "dbo.customers");
            DropForeignKey("dbo.surveys", "id_section", "dbo.question_sections");
            DropForeignKey("dbo.questions", "id_section", "dbo.question_sections");
            DropForeignKey("dbo.question_options", "id_question", "dbo.questions");
            DropForeignKey("dbo.answers", "id_question_option", "dbo.question_options");
            DropForeignKey("dbo.project_follow_up", "id_survey", "dbo.surveys");
            DropForeignKey("dbo.project_follow_up", "id_staff", "dbo.staff");
            DropForeignKey("dbo.answers", "id_customer", "dbo.customers");
            DropIndex("dbo.question_options", new[] { "id_question" });
            DropIndex("dbo.questions", new[] { "id_section" });
            DropIndex("dbo.project_follow_up", new[] { "id_staff" });
            DropIndex("dbo.project_follow_up", new[] { "id_survey" });
            DropIndex("dbo.surveys", new[] { "id_customer" });
            DropIndex("dbo.surveys", new[] { "id_section" });
            DropIndex("dbo.answers", new[] { "id_customer" });
            DropIndex("dbo.answers", new[] { "id_question_option" });
            DropTable("dbo.question_options");
            DropTable("dbo.questions");
            DropTable("dbo.question_sections");
            DropTable("dbo.staff");
            DropTable("dbo.project_follow_up");
            DropTable("dbo.surveys");
            DropTable("dbo.customers");
            DropTable("dbo.answers");
        }
    }
}
