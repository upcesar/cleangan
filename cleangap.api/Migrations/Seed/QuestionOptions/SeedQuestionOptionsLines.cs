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
        private void SeedQuestionOptionsLines(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    //Do you allow percentage discounting of lines?
                    new question_options() { id = 218, id_question = 91, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 219, id_question = 91, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow percentage discounting of lines For B2B
                    new question_options() { id = 220, id_question = 92, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 221, id_question = 92, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow percentage discounting of lines For Reps
                    new question_options() { id = 222, id_question = 93, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 223, id_question = 93, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow percentage discounting of lines For Admins
                    new question_options() { id = 224, id_question = 94, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 225, id_question = 94, input_type = "radio", option_text = "No", order = 2 },
                
                    //Do you allow percentage discounting of lines For Managers
                    new question_options() { id = 226, id_question = 95, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 227, id_question = 95, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow custom price changes on an item level?
                    new question_options() { id = 228, id_question = 96, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 229, id_question = 96, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow custom price changes on an item level For B2B
                    new question_options() { id = 230, id_question = 97, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 231, id_question = 97, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow custom price changes on an item level For Reps
                    new question_options() { id = 232, id_question = 98, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 233, id_question = 98, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow custom price changes on an item level For Admins
                    new question_options() { id = 234, id_question = 99, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 235, id_question = 99, input_type = "radio", option_text = "No", order = 2 },

                    //Do you allow custom price changes on an item level For Managers
                    new question_options() { id = 236, id_question = 100, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 237, id_question = 100, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Information: Show Retail Price
                    new question_options() { id = 238, id_question = 100, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 239, id_question = 100, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Information: Show Gender
                    new question_options() { id = 240, id_question = 101, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 241, id_question = 101, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Information: Show Dimensions
                    new question_options() { id = 242, id_question = 102, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 243, id_question = 102, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Information: Show Product Group
                    new question_options() { id = 244, id_question = 103, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 245, id_question = 103, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Information: Show Warehouse
                    new question_options() { id = 246, id_question = 104, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 247, id_question = 104, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Information: Show Product Type?
                    new question_options() { id = 248, id_question = 105, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 249, id_question = 105, input_type = "radio", option_text = "No", order = 2 }

            );
        }

    }
}