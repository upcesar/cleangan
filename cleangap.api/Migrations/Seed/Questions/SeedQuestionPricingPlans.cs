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
        private void SeedQuestionPricingPlans(DAL.CleanGapDataContext context)
        {
            question_sections qsHeader = context.question_sections.Find(10);     // Pricing Plans

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 133, name = "pricing_plan_discount", description = "In RepSpark, pricing plans override all other discouting. Next in priority is user input discount. Last, would be any customer default discount. Is this how your discounting works?", page = 34, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 134, name = "pricing_plan_tech_call", description = "Tech call required", page = 34, parent_question_id = 133, parent_answer_value = "No", question_sections = qsHeader, id_section = qsHeader.id },
                        
                        new questions() { id = 135, name = "pricing_plan_min_qtd", description = "Do you have quantity minimums with upcharges if those aren’t met?", page = 35, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 136, name = "pricing_plan_what_min", description = "What are the minimums?", page = 35, parent_question_id = 135, parent_answer_value = "Yes", question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 137, name = "pricing_plan_what_min", description = "What is the upcharge?", page = 35, parent_question_id = 135, parent_answer_value = "Yes", question_sections = qsHeader, id_section = qsHeader.id },

                        new questions() { id = 138, name = "pricing_plan_have_plan", description = "Do you have pricing plans? These plans will be selectable on the header. All fields are optional but are basic parameters of the plans. If your plans are more complex, please email for a tech review.", page = 36, question_sections = qsHeader, id_section = qsHeader.id },
                        new questions() { id = 139, name = "pricing_plan_rep_plan", description = "Plan", page = 36, parent_question_id = 138, parent_answer_value = "Yes", question_sections = qsHeader, id_section = qsHeader.id, has_repeater = true }
            );

        }

        
    }
}