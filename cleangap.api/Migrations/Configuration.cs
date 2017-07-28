namespace cleangap.api.Migrations
{
    using Models.Tables;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL;

    internal sealed partial class Configuration : DbMigrationsConfiguration<cleangap.api.DAL.CleanGapDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "cleangap.api.DAL.CleanGrapDataContext";
        }        
        private void SeedQuestions(DAL.CleanGapDataContext context)
        {
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT questions ON");

            SeedQuestionsERP(context);
            SeedQuestionsDomain(context);
            SeedQuestionPartitions(context);
            SeedQuestionHeader(context);
            SeedQuestionSelections(context);
            SeedQuestionLines(context);
            SeedQuestionReviews(context);

            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT questions OFF");
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

            SeedSections(context);
            SeedQuestions(context);
            SeedQuestionOptions(context);

        }

        
    }
}
