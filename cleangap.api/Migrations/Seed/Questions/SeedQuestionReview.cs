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
        private void SeedQuestionReviews(DAL.CleanGapDataContext context)
        {
            question_sections qsHeader = context.question_sections.Find(7);

            SeedQuestionReviewContactInfo(context, qsHeader);
            SeedQuestionReviewDisplayOpt(context, qsHeader);

        }

        private void SeedQuestionReviewContactInfo(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 106, name = "review_display_contact", description = "Do you want to display Contact Details at the bottom of the page?", page = 27, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 107, name = "review_display_address", description = "Will we display the contact address?", page = 27, question_sections = qsHeader, parent_question_id = 106, parent_answer_value = "Yes", id_section = qsHeader.id },
                        new questions() { id = 107, name = "review_display_email", description = "Email as stated above in question 2 in the brand section?", page = 27, question_sections = qsHeader, parent_question_id = 106, parent_answer_value = "Yes", id_section = qsHeader.id },
                        new questions() { id = 108, name = "review_display_email", description = "Does your ERP have rep email addresses?", page = 27, question_sections = qsHeader, parent_question_id = 106, parent_answer_value = "Yes", id_section = qsHeader.id },
                        new questions() { id = 109, name = "review_email_erp", description = "Would you rather display the email of each rep at the bottom of the review page?", page = 27, question_sections = qsHeader, parent_question_id = 108, parent_answer_value = "Yes", id_section = qsHeader.id }
            );
        }

        private void SeedQuestionReviewDisplayOpt(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 110, name = "review_original_wholesale", description = "Do you want to show the original wholesale price along with the discounted price if you allow discounting?", page = 28, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 111, name = "review_original_ret_price", description = "Standard Additional Columns: Show Retail Price?", page = 28, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 112, name = "review_original_gender", description = "Standard Additional Columns: Show Gender?", page = 28, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 113, name = "review_original_season", description = "Standard Additional Columns: Show Season?", page = 28, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 114, name = "review_original_dim", description = "Standard Additional Information: Show Dimensions?", page = 28, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 115, name = "review_original_prod_grp", description = "Standard Additional Information: Show Product Group?", page = 28, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 116, name = "review_original_whs", description = "Standard Additional Information: Show Warehouse?", page = 28, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 117, name = "review_original_prod_type", description = "Standard Additional Information: Show Product Type?", page = 28, question_sections = qsHeader, id_section = qsHeader.id }
            );
        }
    }
}