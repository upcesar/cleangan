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
            SeedQuestionOptions(context);


            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_sections OFF");

        }

        
    }
}
