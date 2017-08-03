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
        private void SeedQuestionOptionsPricingPlan(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    // Is this how your discounting works?
                    new question_options() { id = 303, id_question = 133, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 304, id_question = 133, input_type = "radio", option_text = "No", order = 2 },

                    // Do you have quantity minimums with upcharges if those aren’t met?
                    new question_options() { id = 305, id_question = 135, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 306, id_question = 135, input_type = "radio", option_text = "No", order = 2 },

                    // What are the minimums?
                    new question_options() { id = 307, id_question = 136, input_type = "input-text", option_text = "", order = 1 },

                    // What is the upcharge?
                    new question_options() { id = 308, id_question = 137, input_type = "input-text", option_text = "", order = 1 },

                    // Do you have pricing plans? These plans will be selectable on the header.
                    new question_options() { id = 309, id_question = 138, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 310, id_question = 138, input_type = "radio", option_text = "No", order = 2 },

                    // Plan
                    new question_options() { id = 311, id_question = 139, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 312, id_question = 139, input_type = "input-date", option_text = "Minimum Start Date", order = 2 },
                    new question_options() { id = 313, id_question = 139, input_type = "input-date", option_text = "Maximum Entry Date", order = 3 },
                    new question_options() { id = 314, id_question = 139, input_type = "input-text", option_text = "Minimum Units", order = 4 },
                    new question_options() { id = 315, id_question = 139, input_type = "input-text", option_text = "Minimum Wholesale Amount", order = 5 },
                    new question_options() { id = 316, id_question = 139, input_type = "input-text", option_text = "Results", order = 6 },
                    new question_options() { id = 317, id_question = 139, input_type = "input-text", option_text = "Terms Change", order = 7 },
                    new question_options() { id = 318, id_question = 139, input_type = "input-text", option_text = "Pricing Discount", order = 8 },
                    new question_options() { id = 319, id_question = 139, input_type = "input-text", option_text = "What brands is this active for?", order = 9 },
                    new question_options() { id = 320, id_question = 139, input_type = "input-text", option_text = "What divisions is this active for?", order = 10 }

            );
        }

    }
}