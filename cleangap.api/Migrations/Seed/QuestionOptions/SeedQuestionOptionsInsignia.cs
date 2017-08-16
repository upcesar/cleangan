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
        private void SeedQuestionOptionsInsignia(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    //Will you require RepSpark’s Insignnia module?
                    new question_options() { id = 274, id_question = 118, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 275, id_question = 118, input_type = "radio", option_text = "No", order = 2 }

            );
        }

    }
}