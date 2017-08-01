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
        private void SeedQuestionLines(DAL.CleanGapDataContext context)
        {
            question_sections qsHeader = context.question_sections.Find(6);

            SeedQuestionLinesDiscPriceChanges(context, qsHeader);
            SeedQuestionLinesColumnsForDisplay(context, qsHeader);
            

        }

        private void SeedQuestionLinesDiscPriceChanges(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(24);  //Discounting & Price Changes

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 91, name = "lines_discount", description = "Do you allow percentage discounting of lines?", page = 24, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 92, name = "lines_discount_b2b", description = "For B2B?", page = 24, question_sections = qsHeader, parent_question_id = 91, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 93, name = "lines_discount_reps", description = "For Reps?", page = 24, question_sections = qsHeader, parent_question_id = 91, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 94, name = "lines_discount_admin", description = "For Admins?", page = 24, question_sections = qsHeader, parent_question_id = 91, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 95, name = "lines_discount_managers", description = "For Managers?", page = 24, question_sections = qsHeader, parent_question_id = 91, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },

                        new questions() { id = 96, name = "lines_custom_price", description = "Do you allow custom price changes on an item level?", page = 25, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 97, name = "lines_discount_b2b", description = "For B2B?", page = 25, question_sections = qsHeader, parent_question_id = 96, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 98, name = "lines_discount_reps", description = "For Reps?", page = 25, question_sections = qsHeader, parent_question_id = 96, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 99, name = "lines_discount_admin", description = "For Admins?", page = 25, question_sections = qsHeader, parent_question_id = 96, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 100, name = "lines_discount_managers", description = "For Managers?", page = 25, question_sections = qsHeader, parent_question_id = 96, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }

        private void SeedQuestionLinesColumnsForDisplay(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(25);  //Columns for Display

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 101, name = "lines_show_retail_price", description = "Standard Additional Information: Show Retail Price?", page = 26, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 102, name = "lines_show_gender", description = "Standard Additional Information: Show Gender?", page = 26, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 102, name = "lines_show_dimension", description = "Standard Additional Information: Show Dimensions?", page = 26, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 103, name = "lines_show_prod_group", description = "Standard Additional Information: Show Product Group?", page = 26, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 104, name = "lines_show_whs", description = "Standard Additional Information: Show Warehouse?", page = 26, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 105, name = "lines_show_prod_type", description = "Standard Additional Information: Show Product Type?", page = 26, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }
    }
}