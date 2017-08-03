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
        private void SeedQuestionOptionsValidation(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    // Do you require a PO?
                    new question_options() { id = 278, id_question = 119, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 279, id_question = 119, input_type = "radio", option_text = "No", order = 2 },

                    // If any fields are not defaulted, do you require them to be filled out?
                    new question_options() { id = 280, id_question = 120, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 281, id_question = 120, input_type = "radio", option_text = "No", order = 2 },

                    // Customer
                    new question_options() { id = 282, id_question = 121, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 283, id_question = 121, input_type = "radio", option_text = "No", order = 2 },

                    // Cancel date
                    new question_options() { id = 284, id_question = 122, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 285, id_question = 122, input_type = "radio", option_text = "No", order = 2 },

                    // Start date
                    new question_options() { id = 286, id_question = 123, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 287, id_question = 123, input_type = "radio", option_text = "No", order = 2 },

                    // Do you want to validate for Inventory Availability?
                    new question_options() { id = 288, id_question = 124, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 289, id_question = 124, input_type = "radio", option_text = "No", order = 2 },

                    // Are there specific order types that do not require inventory validation?
                    new question_options() { id = 290, id_question = 125, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 291, id_question = 125, input_type = "radio", option_text = "No", order = 2 },

                    // please list order type(s), separated by comma
                    new question_options() { id = 292, id_question = 126, input_type = "textarea", option_text = "", order = 1 },

                    // Note* Order splitting isxxxxx. This requires inventory validation
                    new question_options() { id = 293, id_question = 127, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 294, id_question = 127, input_type = "radio", option_text = "No", order = 2 },

                    // Allowing Splitting
                    new question_options() { id = 295, id_question = 128, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 296, id_question = 128, input_type = "radio", option_text = "No", order = 2 },

                    // By Lines
                    new question_options() { id = 297, id_question = 129, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 298, id_question = 129, input_type = "radio", option_text = "No", order = 2 },

                    // By Size
                    new question_options() { id = 299, id_question = 130, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 300, id_question = 130, input_type = "radio", option_text = "No", order = 2 },

                    // please list order type(s), separated by comma
                    new question_options() { id = 301, id_question = 131, input_type = "input-text", option_text = "", order = 1 },

                    // please list order type(s), separated by comma
                    new question_options() { id = 302, id_question = 132, input_type = "input-text", option_text = "", order = 1 }

            );
        }

    }
}