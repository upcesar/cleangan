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
        private void SeedQuestionBrands(DAL.CleanGapDataContext context)
        {
            question_sections qsBrands = context.question_sections.Find(3);
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 9, name = "brands_multiple", description = "Are there multiple brands?", page = 10, id_section = qsBrands.id },
                        new questions() { id = 10, name = "brand_separeted", description = "Are there separate divisions within a brand?", page = 11, id_section = qsBrands.id }
            );
            SeedDependentQtnMultBrands(context, qsBrands);
            SeedDependentQtnSepDivBrand(context, qsBrands);
        }

        private void SeedDependentQtnMultBrands(DAL.CleanGapDataContext context, question_sections qsBrands)
        {
            questions qMB = context.questions.Find(8);  // Are there multiple brands? Yes
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 11, name = "brands_combined", description = "Can brands be combined on one order?", page = 10, dependent_question = qMB, dependent_answer_value = "Y", id_section = qsBrands.id },
                        new questions() { id = 12, name = "brands_assigned", description = "Are customers assigned a brand in the ERP? *Note:  If a customer is assigned to a brand, they will only exist int hat brand.", page = 10, dependent_question = qMB, dependent_answer_value = "Y", id_section = qsBrands.id },
                        new questions() { id = 13, name = "brand_lookup", description = "Are lookups (colors, genders, sales reps, shipping options) brand specific? IE: is there a potential for the same lookup code to exist in multiple brands with different descriptions.", page = 10, dependent_question = qMB, dependent_answer_value = "Y", id_section = qsBrands.id }
            );
            SeedDependentQtnDivCombBrands(context, qsBrands);
        }

        private void SeedDependentQtnSepDivBrand(DAL.CleanGapDataContext context, question_sections qsBrands)
        {
            questions qSepDiv = context.questions.Find(8);  // Are there separate divisions within a brand? Yes
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 14, name = "brand_div_combined", description = "Can divisions be combined on one order?", page = 11, dependent_question= qSepDiv, dependent_answer_value= "Y", id_section = qsBrands.id },
                        new questions() { id = 15, name = "brand_division", description = "Do you want a division filter?", page = 11, dependent_question = qSepDiv, dependent_answer_value = "Y", id_section = qsBrands.id },
                        new questions() { id = 16, name = "brand_reps_div", description = "Are some sales reps limited to what divisions they can sell?", page = 11, dependent_question = qSepDiv, dependent_answer_value = "Y", id_section = qsBrands.id }
            );
        }

        private void SeedDependentQtnDivCombBrands(DAL.CleanGapDataContext context, question_sections qsBrands)
        {
            questions qDivCombBrand = context.questions.Find(13);  // Can divisions be combined on one order? Yes
            context.questions
                   .AddOrUpdate(            
                        new questions() { id = 17, name = "brand_div_filter", description = "Do you want a division filter?", page = 11, dependent_question = qDivCombBrand, dependent_answer_value = "Y", id_section = qsBrands.id },
                        new questions() { id = 18, name = "brand_sale_div", description = "Are some sales reps limited to what divisions they can sell?", page = 11, dependent_question = qDivCombBrand, id_section = qsBrands.id }            
            );
        }

    }
}