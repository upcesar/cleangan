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
        private void SeedQuestionInsignia(DAL.CleanGapDataContext context)
        {
            question_sections qsHeader = context.question_sections.Find(8);

            context.questions
                   .AddOrUpdate(
                        new questions() { id = 118, name = "insignnia_module", description = "Will you require RepSpark’s Insignnia module? If yes, RepSpark will contact you to set up a tech call regarding processes and workflows", page = 29, question_sections = qsHeader, id_section = qsHeader.id }
            );

        }
    }
}