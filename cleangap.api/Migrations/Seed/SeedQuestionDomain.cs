﻿namespace cleangap.api.Migrations
{
    using Models.Tables;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed partial class Configuration : DbMigrationsConfiguration<cleangap.api.DAL.CleanGapDataContext>
    {
        private void SeedQuestionsDomain(DAL.CleanGapDataContext context)
        {
            context.questions
                   .AddOrUpdate(
                        new questions() { id = 7, name = "domain_url", description = "URL Prefix Name: [name of company].repspark.net (no spaces or special characters allowed)", page = 8, question_sections = context.question_sections.Find(2) }
            );
        }
    }
}