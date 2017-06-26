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
        private void SeedQuestionPartitions(DAL.CleanGapDataContext context)
        {
            question_sections qsBrands = context.question_sections.Find(3);
            context.questions.AddOrUpdate(
                    new questions() { id = 9, name = "brands_multiple", description = "Are there multiple brands?", page = 5, id_section = qsBrands.id },
                    new questions() { id = 14, name = "brand_separeted", description = "Are there separate divisions within a brand?", page = 6, id_section = qsBrands.id }
            );
            SeedDependentQtnMultBrands(context, qsBrands);
            SeedDependentQtnSepDivBrand(context, qsBrands);
        }

        private void SeedDependentQtnMultBrands(DAL.CleanGapDataContext context, question_sections qsBrands)
        {
            questions qMB = context.questions.Find(9);  // Are there multiple brands? Yes
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 10, name = "brands_combined", description = "Can brands be combined on one order?", page = 5, parent_question_id = qMB.id, parent_answer_value = "Yes", id_section = qsBrands.id },
                        new questions() { id = 11, name = "brands_assigned", description = "Are customers assigned a brand in the ERP? *Note:  If a customer is assigned to a brand, they will only exist int hat brand.", page = 5, parent_question_id = qMB.id, parent_answer_value = "Yes", id_section = qsBrands.id },
                        new questions() { id = 12, name = "brand_lookup", description = "Are lookups (colors, genders, sales reps, shipping options) brand specific? IE: is there a potential for the same lookup code to exist in multiple brands with different descriptions.", page = 5, parent_question_id = qMB.id, parent_answer_value = "Yes", id_section = qsBrands.id }
            );
            SeedDependentQtnLookupBrand(context, qsBrands);
        }

        private void SeedDependentQtnLookupBrand(DAL.CleanGapDataContext context, question_sections qsBrands)
        {
            questions qBrandLookup = context.questions.Find(12);  //Are lookups (colors, genders, sales reps, shipping options) brand specific?
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 13, name = "options_per_brand", description = "Please mark what option below might be different per brand", page = 5, parent_question_id = qBrandLookup.id, parent_answer_value = "Yes", id_section = qsBrands.id }                        
            );
        }

        private void SeedDependentQtnSepDivBrand(DAL.CleanGapDataContext context, question_sections qsBrands)
        {
            questions qSepDiv = context.questions.Find(14);  // Are there separate divisions within a brand? Yes
            context.questions.AddOrUpdate(
                        new questions() { id = 15, name = "brand_div_combined", description = "Can divisions be combined on one order?", page = 6, parent_question_id = qSepDiv.id, parent_answer_value = "Yes", id_section = qsBrands.id },
                        new questions() { id = 18, name = "brand_list_erp", description = "Please list our brand names as entered ERP", page = 6, parent_question_id = qSepDiv.id, parent_answer_value = "Yes", id_section = qsBrands.id, has_repeater = true }
            );
            SeedDependentQtnDivCombBrands(context, qsBrands);
        }
        private void SeedDependentQtnDivCombBrands(DAL.CleanGapDataContext context, question_sections qsBrands)
        {
            questions qDivCombBrand = context.questions.Find(15);  // Can divisions be combined on one order? Yes
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 16, name = "brand_division", description = "Do you want a division filter?", page = 6, parent_question_id = qDivCombBrand.id, parent_answer_value = "Yes", id_section = qsBrands.id },
                        new questions() { id = 17, name = "brand_reps_div", description = "Are some sales reps limited to what divisions they can sell?", page = 6, parent_question_id = qDivCombBrand.id, parent_answer_value = "Yes", id_section = qsBrands.id }
            );
        }
    }
}