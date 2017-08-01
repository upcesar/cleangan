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
        private void SeedQuestionSelections(DAL.CleanGapDataContext context)
        {
            question_sections qsHeader = context.question_sections.Find(5);

            SeedQuestionSelectionPageView(context, qsHeader);
            SeedQuestionSelectionUnitDisplay(context, qsHeader);
            SeedQuestionSelectionFieldDisplay(context, qsHeader);
            SeedQuestionSelectionStandardFilter(context, qsHeader);
            SeedQuestionSelectionMiscFilter(context, qsHeader);

        }

        private void SeedQuestionSelectionPageView(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(20);  //Page View

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 68, name = "selection_view_reps", description = "Default Selections page view for Rep Users?", page = 19, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 69, name = "selection_view_cust", description = "Default Selections page view for For Customer/B2B?", page = 19, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }

        private void SeedQuestionSelectionUnitDisplay(DAL.CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(21);  //Availability Display

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 70, name = "selection_unit_display", description = "Unit to display for Rep?", page = 20, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 71, name = "selection_view_cust", description = "Unit to display for for Customer/B2B?", page = 20, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }

        private void SeedQuestionSelectionFieldDisplay(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(22);  //Fields to Display

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 72, name = "selection_opt_field", description = "Do you want the optional fields displayed?", page = 21, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 73, name = "selection_show_rtl", description = "Show Retail Price?", page = 21, question_sections = qsHeader, parent_question_id = 72, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 74, name = "selection_show_gender", description = "Show Gender?", page = 21, question_sections = qsHeader, parent_question_id = 72, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 75, name = "selection_show_season", description = "Show Season?", page = 21, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 76, name = "selection_show_dimension", description = "Show Dimension?", page = 21, question_sections = qsHeader, parent_question_id = 72, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 77, name = "selection_show_prod_grp", description = "Show Product Group?", page = 21, question_sections = qsHeader, parent_question_id = 72, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 78, name = "selection_show_whs", description = "Show Warehouse?", page = 21, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 79, name = "selection_show_prod_type", description = "Show Product Type?", page = 21, question_sections = qsHeader, parent_question_id = 72, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id }

            );
        }
        private void SeedQuestionSelectionStandardFilter(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(23);  //Filters

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 80, name = "sel_std_filter_color", description = "Standard Filters: Color?", page = 22, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 81, name = "sel_std_filter_prod_cat", description = "Standard Filters: Product Category", page = 22, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 82, name = "sel_std_filter_prod_child", description = "Standard Filters: Are there children product categories?", page = 22, question_sections = qsHeader, parent_question_id = 81, parent_answer_value = "Yes", id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 83, name = "sel_std_filter_gender", description = "Standard Filters: Gender?", page = 22, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 84, name = "sel_std_filter_prod_grp", description = "Standard Filters: Product Group?", page = 22, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 85, name = "sel_std_filter_whs", description = "Standard Filters: Warehouse?", page = 22, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 86, name = "sel_std_filter_prod_type", description = "Standard Filters: Product Type?", page = 22, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 87, name = "sel_std_filter_price_band", description = "Standard Filters: Price Band Filter?", page = 22, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }
        private void SeedQuestionSelectionMiscFilter(CleanGapDataContext context, question_sections qsHeader)
        {
            question_sections subSection = context.question_sections.Find(23);  //Filters

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 88, name = "sel_misc_filter_immediate", description = "Misc Filters defaulting: Immediate Only?", page = 23, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 89, name = "sel_misc_filter_available", description = "Misc Filters defaulting: Available Only?", page = 23, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id },
                        new questions() { id = 90, name = "sel_misc_filter_available_prod", description = "Misc Filters defaulting: Only Available Products?", page = 23, question_sections = qsHeader, id_section = qsHeader.id, id_subsection = subSection.id }
            );
        }
    }
}