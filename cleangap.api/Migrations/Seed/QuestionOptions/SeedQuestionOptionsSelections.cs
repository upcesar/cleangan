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
        private void SeedQuestionOptionsSelections(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    //Default Selections page view for Rep Users?
                    new question_options() { id = 168, id_question = 67, input_type = "checkbox", option_text = "Grid", order = 1 },
                    new question_options() { id = 169, id_question = 67, input_type = "checkbox", option_text = "Lines Quick Add", order = 2 },
                    new question_options() { id = 170, id_question = 67, input_type = "checkbox", option_text = "Lines Size Entry", order = 3 },

                    //Default Selections page view for Customer/B2B?
                    new question_options() { id = 171, id_question = 68, input_type = "checkbox", option_text = "Grid", order = 1 },
                    new question_options() { id = 172, id_question = 68, input_type = "checkbox", option_text = "Lines Quick Add", order = 2 },
                    new question_options() { id = 173, id_question = 68, input_type = "checkbox", option_text = "Lines Size Entry", order = 3 },

                    //Units to Display for Reps
                    new question_options() { id = 174, id_question = 69, input_type = "checkbox", option_text = "True inventory", order = 1 },
                    new question_options() { id = 175, id_question = 69, input_type = "checkbox", option_text = "Limited", order = 2 },
                    new question_options() { id = 176, id_question = 69, input_type = "input-text", option_text = "Number to Limit at", order = 3 },

                    //Units to Display for Customer/B2B?
                    new question_options() { id = 177, id_question = 70, input_type = "checkbox", option_text = "True inventory", order = 1 },
                    new question_options() { id = 178, id_question = 70, input_type = "checkbox", option_text = "Limited", order = 2 },
                    new question_options() { id = 179, id_question = 70, input_type = "input-text", option_text = "Number to Limit at", order = 3 },

                    //Do you want the optional fields displayed?
                    new question_options() { id = 180, id_question = 71, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 181, id_question = 71, input_type = "radio", option_text = "No", order = 2 },

                    //Show Retail Price
                    new question_options() { id = 182, id_question = 72, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 183, id_question = 72, input_type = "radio", option_text = "No", order = 2 },

                    //Show Gender
                    new question_options() { id = 184, id_question = 73, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 185, id_question = 73, input_type = "radio", option_text = "No", order = 2 },

                    //Show Season
                    new question_options() { id = 186, id_question = 74, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 187, id_question = 74, input_type = "radio", option_text = "No", order = 2 },

                    //Show Dimension
                    new question_options() { id = 188, id_question = 75, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 189, id_question = 75, input_type = "radio", option_text = "No", order = 2 },

                    //Show Product Group
                    new question_options() { id = 190, id_question = 76, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 191, id_question = 76, input_type = "radio", option_text = "No", order = 2 },

                    //Show Warehouse
                    new question_options() { id = 192, id_question = 77, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 193, id_question = 77, input_type = "radio", option_text = "No", order = 2 },

                    //Show Product Type
                    new question_options() { id = 194, id_question = 78, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 195, id_question = 78, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Color
                    new question_options() { id = 196, id_question = 79, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 197, id_question = 79, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Product Category
                    new question_options() { id = 198, id_question = 80, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 199, id_question = 80, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Are there children product categories?
                    new question_options() { id = 200, id_question = 81, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 201, id_question = 81, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Gender
                    new question_options() { id = 202, id_question = 82, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 203, id_question = 82, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Product Group
                    new question_options() { id = 204, id_question = 83, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 205, id_question = 83, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Warehouse
                    new question_options() { id = 206, id_question = 84, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 207, id_question = 84, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Product Type
                    new question_options() { id = 208, id_question = 85, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 209, id_question = 85, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Price Band Filter
                    new question_options() { id = 210, id_question = 86, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 211, id_question = 86, input_type = "radio", option_text = "No", order = 2 },

                    //Misc Filters defaulting: Immediate Only
                    new question_options() { id = 212, id_question = 87, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 213, id_question = 87, input_type = "radio", option_text = "No", order = 2 },

                    //Misc Filters defaulting: Available Only
                    new question_options() { id = 214, id_question = 88, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 215, id_question = 88, input_type = "radio", option_text = "No", order = 2 },

                    //Misc Filters defaulting: Only Available Products
                    new question_options() { id = 216, id_question = 89, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 217, id_question = 89, input_type = "radio", option_text = "No", order = 2 }

            );
        }

    }
}