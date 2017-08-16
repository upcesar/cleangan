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
        private void SeedQuestionOptions(DAL.CleanGapDataContext context)
        {            
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_options ON");
            //SeedQuestionOptionsERP(context);
            //SeedQuestionOptionsDomain(context);
            //SeedQuestionOptionsBrands(context);
            //SeedQuestionOptionsHeaders(context);
            //SeedQuestionOptionsSelections(context);
            //SeedQuestionOptionsLines(context);
            //SeedQuestionOptionsReviews(context);
            //SeedQuestionOptionsInsignia(context);
            //SeedQuestionOptionsValidation(context);
            //SeedQuestionOptionsPricingPlan(context);
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_options OFF");            
        }

    }
}