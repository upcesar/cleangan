namespace cleangap.api.Migrations
{
    using Models.Tables;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed partial class Configuration : DbMigrationsConfiguration<cleangap.api.DAL.CleanGapDataContext>
    {
        private void SeedQuestionsERP(DAL.CleanGapDataContext context)
        {
            question_sections qsERP = context.question_sections.Find(1);

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 1, name = "erp_concept", description = "What is your ERP?", page = 1, question_sections = qsERP },
                        new questions() { id = 2, name = "erp_data_transfer", description = "How is data going to be transferred for the following entities?", page = 2, question_sections = qsERP },
                        new questions() { id = 3, name = "erp_responsible", description = "Who is responsible for providing data access?", page = 3, question_sections = qsERP },
                        new questions() { id = 4, name = "erp_resp_worflow", description = "Who is responsible for Point6 specific business logic workflow questions?", page = 4, question_sections = qsERP },
                        new questions() { id = 5, name = "erp_resp_access", description = "Who is responsible for setting up user access to the site?", page = 6, question_sections = qsERP },
                        new questions() { id = 6, name = "erp_email_notif", description = "What e-mail should receive notifications when a user requests access?", page = 7, question_sections = qsERP }
            );
        }
        
    }
}