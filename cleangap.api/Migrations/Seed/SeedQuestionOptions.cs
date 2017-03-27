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
            SeedQuestionOptionsERP(context);
        }
        private void SeedQuestionOptionsERP(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,
                    new question_options() { id = 1, id_question = 1, input_type = "textarea", order = 1 },
                    
                    new question_options() { id = 2, id_question = 2, input_type = "textarea", option_text = "Order Entry Data", order = 1 },
                    new question_options() { id = 3, id_question = 2, input_type = "textarea", option_text = "Order Export to ERP", order = 2 },
                    new question_options() { id = 4, id_question = 2, input_type = "textarea", option_text = "Order Confirmation from ERP", order = 3 },
                    new question_options() { id = 5, id_question = 2, input_type = "textarea", option_text = "Images", order = 4 },

                    // Responsible data access
                    new question_options() { id = 6, id_question = 3, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 7, id_question = 3, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 8, id_question = 3, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 9, id_question = 3, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible technical question
                    new question_options() { id = 10, id_question = 4, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 11, id_question = 4, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 12, id_question = 4, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 13, id_question = 4, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible Point6 specific business logic workflow
                    new question_options() { id = 14, id_question = 5, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 15, id_question = 5, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 16, id_question = 5, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 17, id_question = 5, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible setting up user access to the site
                    new question_options() { id = 18, id_question = 6, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 19, id_question = 6, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 20, id_question = 6, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 21, id_question = 6, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible receive notifications when a user requests access
                    new question_options() { id = 22, id_question = 7, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 23, id_question = 7, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 24, id_question = 7, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 25, id_question = 7, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 }

            );
        }
    }
}