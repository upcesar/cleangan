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
        private void SeedSubSections(DAL.CleanGapDataContext context)
        {
            context.question_sections
                   .AddOrUpdate(qs => qs.id,
            #region Subsection => Header
                        new question_sections() { id = 15, name = "Customer Information", parent_section = context.question_sections.Find(4) },
                        new question_sections() { id = 16, name = "Order Types", parent_section = context.question_sections.Find(4) },
                        new question_sections() { id = 17, name = "Dating", parent_section = context.question_sections.Find(4) },
                        new question_sections() { id = 18, name = "Terms and Ship Via", parent_section = context.question_sections.Find(4) },
                        new question_sections() { id = 19, name = "Misc. Header Items", parent_section = context.question_sections.Find(4) },
            #endregion
            #region Subsection => Selections
                        new question_sections() { id = 20, name = "Page View", parent_section = context.question_sections.Find(5) },
                        new question_sections() { id = 21, name = "Availability Display", parent_section = context.question_sections.Find(5) },
                        new question_sections() { id = 22, name = "Fields to Display", parent_section = context.question_sections.Find(5) },
                        new question_sections() { id = 23, name = "Filters", parent_section = context.question_sections.Find(5) },
            #endregion
            #region Subsection => Lines
                        new question_sections() { id = 24, name = "Discounting & Price Changes", parent_section = context.question_sections.Find(6) },
                        new question_sections() { id = 25, name = "Columns for Display", parent_section = context.question_sections.Find(6) },
            #endregion
            #region Subsection => Reviews
                        new question_sections() { id = 26, name = "Contact Information", parent_section = context.question_sections.Find(7) },
                        new question_sections() { id = 27, name = "Display Options", parent_section = context.question_sections.Find(7) },
            #endregion
            #region Subsection => Validation
                        new question_sections() { id = 28, name = "Header Validation", parent_section = context.question_sections.Find(9) },
                        new question_sections() { id = 29, name = "Submission Validation", parent_section = context.question_sections.Find(9) },
            #endregion
            #region Subsection => Order Submission
                        new question_sections() { id = 30, name = "Queues", parent_section = context.question_sections.Find(11) },
                        new question_sections() { id = 31, name = "Notification", parent_section = context.question_sections.Find(11) }
                        #endregion
            );
        }
        private void SeedSections(DAL.CleanGapDataContext context)
        {
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_sections ON");
            
            context.question_sections
                   .AddOrUpdate(qs => qs.id,
                        new question_sections() { id = 1, name = "ERP Questions" },
                        new question_sections() { id = 2, name = "Domain Name" },
                        new question_sections() { id = 3, name = "Brands" },
                        new question_sections() { id = 4, name = "Header" },
                        new question_sections() { id = 5, name = "Selections" },
                        new question_sections() { id = 6, name = "Lines" },
                        new question_sections() { id = 7, name = "Reviews" },
                        new question_sections() { id = 8, name = "Insignia" },
                        new question_sections() { id = 9, name = "Validation" },
                        new question_sections() { id = 10, name = "Pricing Plans" },
                        new question_sections() { id = 11, name = "Order Submission" },
                        new question_sections() { id = 12, name = "Reports" },
                        new question_sections() { id = 13, name = "Summary" },
                        new question_sections() { id = 14, name = "Terms & Conditions" }
            );

            SeedSubSections(context);

            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_sections OFF");

        }

    }
}