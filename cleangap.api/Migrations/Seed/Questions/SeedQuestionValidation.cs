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
        private void SeedQuestionValidation(DAL.CleanGapDataContext context)
        {
            question_sections qsHeader = context.question_sections.Find(9);     //Validation

            SeedQuestionValidationHeader(context, qsHeader);
            SeedQuestionValidationSubmission(context, qsHeader);

        }

        private void SeedQuestionValidationHeader(DAL.CleanGapDataContext context, question_sections qsHeader)
        {

            question_sections subSection = context.question_sections.Find(28);  //Header Validation

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 119, name = "validation_header_po", description = "Do you require a PO?", page = 30, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 120, name = "validation_header_def", description = "If any fields are not defaulted, do you require them to be filled out?", page = 30, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 121, name = "validation_header_cust", description = "Customer", page = 30, question_sections = qsHeader, parent_question_id = 120, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 122, name = "validation_header_cdate", description = "Cancel date", page = 30, question_sections = qsHeader, parent_question_id = 120, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 123, name = "validation_header_sdate", description = "Start date", page = 30, question_sections = qsHeader, parent_question_id = 120, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id }
            );

        }

        private void SeedQuestionValidationSubmission(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(28);  //Header Validation

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 124, name = "validation_submit_inv", description = "Do you want to validate for Inventory Availability?", page = 31, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 125, name = "validation_submit_ordt", description = "Are there specific order types that do not require inventory validation?", page = 31, parent_question_id = 124, parent_answer_value = "Yes", question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 126, name = "validation_submit_comma", description = "Please list order type(s), separated by comma", page = 31, question_sections = qsHeader, parent_question_id = 125, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },

                        new questions() { id = 127, name = "validation_submit_split", description = "Note* Order splitting isxxxxx. This requires inventory validation", page = 32, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 128, name = "validation_submit_sallow", description = "Allowing Splitting?", page = 32, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 129, name = "validation_submit_sline", description = "By Lines", page = 32, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 130, name = "validation_submit_ssize", description = "By Size", page = 32, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },

                        new questions() { id = 131, name = "validation_submit_min1", description = "Do you have a minimum quantity per order?", page = 33, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 132, name = "validation_submit_min2", description = "Do you have a minimum charge per order?", page = 33, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }
    }
}