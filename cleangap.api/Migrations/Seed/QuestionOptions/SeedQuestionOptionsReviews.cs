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
        private void SeedQuestionOptionsReviews(DAL.CleanGapDataContext context)
        {
            SeedQuestionOptionsReviewsContactInfo(context);
            SeedQuestionOptionsReviewsDisplayOption(context);
            SeedQuestionOptionsReviewsEmailAsStated(context);   // UNPREDICTABLE BUG ON ID OR ID_QUESTION
        }

        

        private void SeedQuestionOptionsReviewsContactInfo(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    //Do you want to display Contact Details at the bottom of the page?
                    new question_options() { id = 250, id_question = 106, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 251, id_question = 106, input_type = "radio", option_text = "No", order = 2 }

            );
        }

        private void SeedQuestionOptionsReviewsDisplayOption(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    //Does your ERP have rep email addresses?
                    new question_options() { id = 252, id_question = 109, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 253, id_question = 109, input_type = "radio", option_text = "No", order = 2 },

                    //Would you rather display the email of each rep at the bottom of the review page
                    new question_options() { id = 254, id_question = 110, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 255, id_question = 110, input_type = "radio", option_text = "No", order = 2 },

                    //Do you want to show the original wholesale price along with the discounted price if you allow discounting?
                    new question_options() { id = 256, id_question = 111, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 257, id_question = 111, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Columns: Show Retail Price?
                    new question_options() { id = 258, id_question = 112, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 259, id_question = 112, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Columns: Show Season?
                    new question_options() { id = 260, id_question = 113, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 261, id_question = 113, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Columns: Show Dimension?
                    new question_options() { id = 262, id_question = 114, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 263, id_question = 114, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Columns: Show Product Group?
                    new question_options() { id = 264, id_question = 115, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 265, id_question = 115, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Columns: Show Warehouse?
                    new question_options() { id = 266, id_question = 116, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 267, id_question = 116, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Additional Columns: Show Product Type?
                    new question_options() { id = 268, id_question = 117, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 269, id_question = 117, input_type = "radio", option_text = "No", order = 2 }
            );
        }

        private void SeedQuestionOptionsReviewsEmailAsStated(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                   //Email as stated above in question 2 in the brand section?
                   new question_options() { id = 270, id_question = 107, input_type = "radio", option_text = "Yes", order = 1 },
                   new question_options() { id = 271, id_question = 107, input_type = "radio", option_text = "No", order = 2 },

                   // Will we display the contact address ? 
                   new question_options() { id = 272, id_question = 108, input_type = "radio", option_text = "Yes", order = 1 },
                   new question_options() { id = 273, id_question = 108, input_type = "radio", option_text = "No", order = 2 }

            );
        }

    }
}