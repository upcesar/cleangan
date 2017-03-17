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
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "cleangap.api.DAL.CleanGrapDataContext";
        }
        private void SeedQuestionOptions(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,
                    new question_options() { id = 1, id_question = 1, input_type = "textarea", order = 1 },
                    new question_options() { id = 2, id_question = 2, input_type = "textarea", option_text = "Order Entry Data", order = 1 },
                    new question_options() { id = 3, id_question = 2, input_type = "textarea", option_text = "Order Export to ERP", order = 2 },
                    new question_options() { id = 4, id_question = 2, input_type = "textarea", option_text = "Order Confirmation from ERP", order = 3 },
                    new question_options() { id = 5, id_question = 2, input_type = "textarea", option_text = "Images", order = 4 }
            );
        }
        private void SeedQuestions(DAL.CleanGapDataContext context)
        {
            SeedQuestionsERP(context);
            SeedQuestionsDomain(context);
            SeedQuestionBrands(context);
        }
        protected override void Seed(cleangap.api.DAL.CleanGapDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_sections ON");

            SeedSections(context);
            SeedSubSections(context);
            SeedQuestions(context);

            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_sections OFF");

        }

        
    }
}
