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
        private void SeedQuestionHeader(DAL.CleanGapDataContext context)
        {
            question_sections qsHeader = context.question_sections.Find(4);

            SeedQuestionHeaderCustomer(context, qsHeader);
            SeedQuestionHeaderOrderType(context, qsHeader);
            SeedQuestionHeaderDating(context, qsHeader);
            SeedQuestionHeaderTerms(context, qsHeader);
            SeedQuestionHeaderShipVia(context, qsHeader);
            SeedQuestionHeaderComments(context, qsHeader);

        }

        private void SeedQuestionHeaderCustomer(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(15);

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 28, name = "header_cust", description = "Customer: Is there a billing address with different stores (shippingaddresses)?", page = 10, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 29, name = "header_confirm_address", description = "Customer: confirm each address in the ERP is both the billing and shipping address", page = 10, question_sections = qsHeader, parent_question_id=28, parent_answer_value="No", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 30, name = "header_terms", description = "Does customer have default terms?", page = 10, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 31, name = "erp_responsible", description = "Do you want to apply customer specific discounts?", page = 10, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }

        private void SeedQuestionHeaderOrderType(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(16);

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 32, name = "header_cust", description = "Order Type: What are the order types available?", page = 11, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        
                        new questions() { id = 33, name = "header_ord_type_b2b", description = "Order Type available por B2B?", page = 11, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 34, name = "header_ord_type_default", description = "Is Default?", page = 11, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 35, name = "header_ord_type_custom_dev", description = "Any product limitations limited by order type *Could add custom development work", page = 11, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }

        private void SeedQuestionHeaderDating(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(17);

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 36, name = "header_dating_ls_date", description = "Dating: Lock Start Date", page = 12, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 37, name = "header_dating_ls_b2b", description = "For B2B", page = 12, question_sections = qsHeader, parent_question_id = 36, parent_answer_value="Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 38, name = "header_dating_ls_reps", description = "For Reps", page = 12, question_sections = qsHeader, parent_question_id = 36, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 39, name = "header_dating_ls_admin", description = "For Admins", page = 12, question_sections = qsHeader, parent_question_id = 36, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 40, name = "header_dating_ls_managers", description = "For Managers / CS", page = 12, question_sections = qsHeader, parent_question_id = 36, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },

                        new questions() { id = 41, name = "header_dating_managers", description = "Dating: Cancel Date", page = 13, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },

                        new questions() { id = 42, name = "header_dating_lc_date", description = "Dating: Lock Cancel Date", page = 14, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 43, name = "header_dating_lc_b2b", description = "For B2B", page = 14, question_sections = qsHeader, parent_question_id = 42, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 44, name = "header_dating_lc_reps", description = "For Reps", page = 14, question_sections = qsHeader, parent_question_id = 42, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 45, name = "header_dating_lc_admin", description = "For Admins", page = 14, question_sections = qsHeader, parent_question_id = 42, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 46, name = "header_dating_lc_managers", description = "For Managers / CS", page = 14, question_sections = qsHeader, parent_question_id = 42, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }

        private void SeedQuestionHeaderTerms(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(18);

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 47, name = "header_terms", description = "Terms", page = 15, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 48, name = "header_terms_lock", description = "Terms Lock", page = 15, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        
                        new questions() { id = 49, name = "header_terms_lock", description = "Terms Lock", page = 15, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 50, name = "header_terms_lock_b2b", description = "For B2B", page = 15, question_sections = qsHeader, parent_question_id = 48, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 51, name = "header_terms_lc_reps", description = "For Reps", page = 15, question_sections = qsHeader, parent_question_id = 48, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 52, name = "header_terms_lc_admin", description = "For Admins", page = 15, question_sections = qsHeader, parent_question_id = 48, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 53, name = "header_terms_lc_managers", description = "For Managers / CS", page = 15, question_sections = qsHeader, parent_question_id = 48, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },

                        new questions() { id = 54, name = "header_terms_default", description = "Do all customers have default terms?", page = 16, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 55, name = "header_terms_custom", description = "What term code should we default to?", page = 16, question_sections = qsHeader, parent_question_id = 54, parent_answer_value = "No", id_section = qsHeader.id, id_subsection = subSection.id }
                        
            );
        }

        private void SeedQuestionHeaderShipVia(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(19);

            context.questions
                   .AddOrUpdate(

                        new questions() { id = 56, name = "header_ship_via", description = "Ship Via", page = 17, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 57, name = "header_ship_via_en", description = "Ship Via", page = 17, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 58, name = "header_ship_via_b2b", description = "For B2B", page = 17, question_sections = qsHeader, parent_question_id = 57, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 59, name = "header_ship_via_reps", description = "For Reps", page = 17, question_sections = qsHeader, parent_question_id = 57, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 60, name = "header_ship_via_admin", description = "For Admins", page = 17, question_sections = qsHeader, parent_question_id = 57, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 61, name = "header_ship_via_managers", description = "For Managers / CS", page = 17, question_sections = qsHeader, parent_question_id = 57, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id }

            );
        }

        private void SeedQuestionHeaderComments(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(20);

            context.questions
                   .AddOrUpdate(

                        new questions() { id = 62, name = "header_comments", description = "Comments", page = 18, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 63, name = "header_one_time_ship", description = "One time ship to option", page = 18, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 64, name = "header_in_hand_date", description = "In-Hand Date", page = 18, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 65, name = "header_value_added_srvc", description = "Is there any value added service (customer specific) information that you want displayed in a popup on this page?", page = 18, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 66, name = "header_value_added_field", description = "Fields:", page = 18, question_sections = qsHeader, parent_question_id = 65, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 67, name = "header_values_order", description = "Are there values that must be set on the header in order for the user to proceed in the order process?", page = 18, question_sections = qsHeader, parent_question_id = 62, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id }

            );
        }
    }
}