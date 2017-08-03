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
        private void SeedQuestionOptionsBrands(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,
                    
                    // Are there multiple brands?
                    new question_options() { id = 27, id_question = 9, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 28, id_question = 9, input_type = "radio", option_text = "No", order = 2 },

                    // Can brands be combined on one order?
                    new question_options() { id = 29, id_question = 10, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 30, id_question = 10, input_type = "radio", option_text = "No", order = 2 },

                    // Are customers assigned a brand in the ERP?
                    new question_options() { id = 31, id_question = 11, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 32, id_question = 11, input_type = "radio", option_text = "No", order = 2 },

                    // Are lookups (colors, genders, sales reps, shipping options) brand specific?
                    new question_options() { id = 33, id_question = 12, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 34, id_question = 12, input_type = "radio", option_text = "No", order = 2 },

                    //Please mark what option below might be different per brand
                    new question_options() { id = 35, id_question = 13, input_type = "checkbox", option_text = "Gender", order = 1 },
                    new question_options() { id = 36, id_question = 13, input_type = "checkbox", option_text = "Color", order = 2 },
                    new question_options() { id = 37, id_question = 13, input_type = "checkbox", option_text = "Product Category", order = 3 },
                    new question_options() { id = 38, id_question = 13, input_type = "checkbox", option_text = "Season", order = 4 },
                    new question_options() { id = 39, id_question = 13, input_type = "checkbox", option_text = "Shipping Methods", order = 5 },
                    new question_options() { id = 40, id_question = 13, input_type = "checkbox", option_text = "Customer Terms", order = 6 },
                    new question_options() { id = 41, id_question = 13, input_type = "checkbox", option_text = "Salespeople", order = 7 },
                    new question_options() { id = 42, id_question = 13, input_type = "checkbox", option_text = "Divisions", order = 8 },

                    // Are there separate divisions within a brand?
                    new question_options() { id = 43, id_question = 14, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 44, id_question = 14, input_type = "radio", option_text = "No", order = 2 },

                    // Can divisions be combined on one order?
                    new question_options() { id = 45, id_question = 15, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 46, id_question = 15, input_type = "radio", option_text = "No", order = 2 },

                    // Do you want a division filter?
                    new question_options() { id = 47, id_question = 16, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 48, id_question = 16, input_type = "radio", option_text = "No", order = 2 },

                    // Are some sales reps limited to what divisions they can sell?
                    new question_options() { id = 49, id_question = 17, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 50, id_question = 17, input_type = "radio", option_text = "No", order = 2 },

                    // Please list our brand names as entered ERP (Repeater)
                    new question_options() { id = 51, id_question = 18, input_type = "input-text", option_text = "Brand Name", order = 1 },
                    new question_options() { id = 52, id_question = 18, input_type = "input-text", option_text = "Address 1", order = 2 },
                    new question_options() { id = 53, id_question = 18, input_type = "input-text", option_text = "Address 2", order = 3 },
                    new question_options() { id = 54, id_question = 18, input_type = "input-text", option_text = "City", order = 4 },
                    new question_options() { id = 55, id_question = 18, input_type = "input-text", option_text = "State", order = 5 },
                    new question_options() { id = 56, id_question = 18, input_type = "input-text", option_text = "Zip", order = 6 },
                    new question_options() { id = 57, id_question = 18, input_type = "input-text", option_text = "Country", order = 7 },
                    new question_options() { id = 58, id_question = 18, input_type = "input-text", option_text = "E-Mail", order = 8 },
                    new question_options() { id = 59, id_question = 18, input_type = "input-file", option_text = "Upload Logo", order = 9 },

                    //Are customers limited to a division in the ERP?
                    new question_options() { id = 60, id_question = 19, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 61, id_question = 19, input_type = "radio", option_text = "No", order = 2 },

                    //Are lookups (colors, genders, sales reps, shipping options) brand specific?
                    new question_options() { id = 62, id_question = 20, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 63, id_question = 20, input_type = "radio", option_text = "No", order = 2 },

                    //Please mark what option below might be different per division.Please check if different per brand
                    new question_options() { id = 64, id_question = 21, input_type = "checkbox", option_text = "Gender", order = 1 },
                    new question_options() { id = 65, id_question = 21, input_type = "checkbox", option_text = "Color", order = 2 },
                    new question_options() { id = 66, id_question = 21, input_type = "checkbox", option_text = "Product Category", order = 3 },
                    new question_options() { id = 67, id_question = 21, input_type = "checkbox", option_text = "Season", order = 4 },
                    new question_options() { id = 68, id_question = 21, input_type = "checkbox", option_text = "Shipping Methods", order = 5 },
                    new question_options() { id = 69, id_question = 21, input_type = "checkbox", option_text = "Customer Terms", order = 6 },
                    new question_options() { id = 70, id_question = 21, input_type = "checkbox", option_text = "Salespeople", order = 7 },
                    new question_options() { id = 71, id_question = 21, input_type = "checkbox", option_text = "Divisions", order = 8 },

                    //Does RepSpark need to be made aware of the warehouses that inventory is stored in?
                    new question_options() { id = 72, id_question = 22, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 73, id_question = 22, input_type = "radio", option_text = "No", order = 2 },

                    //Are customers limited in the ERP to what warehouses they can order from?
                    new question_options() { id = 74, id_question = 23, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 75, id_question = 23, input_type = "radio", option_text = "No", order = 2 },

                    //Are there multiple seasons?
                    new question_options() { id = 76, id_question = 24, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 77, id_question = 24, input_type = "radio", option_text = "No", order = 2 },

                    //Can seasons be combined on one order?
                    new question_options() { id = 78, id_question = 25, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 79, id_question = 25, input_type = "radio", option_text = "No", order = 2 },

                    //Do you want a season filter?
                    new question_options() { id = 80, id_question = 26, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 81, id_question = 26, input_type = "radio", option_text = "No", order = 2 },

                    //Do you want the sales person selecting the season of the order on the header ?
                    new question_options() { id = 82, id_question = 27, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 83, id_question = 27, input_type = "radio", option_text = "No", order = 2 }

            );
        }

    }
}