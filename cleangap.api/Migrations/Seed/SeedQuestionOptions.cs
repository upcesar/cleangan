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
        private void SeedQuestionOptions(DAL.CleanGapDataContext context)
        {
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_options ON");
            SeedQuestionOptionsERP(context);
            SeedQuestionOptionsDomain(context);
            SeedQuestionOptionsBrands(context);
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT question_options OFF");
        }

        private void SeedQuestionOptionsERP(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,
                    new question_options() { id = 1, id_question = 1, input_type = "textarea", order = 1 },
                    
                    new question_options() { id = 2, id_question = 2, input_type = "textarea", option_text = "Order Entry Data", order = 1 },
                    new question_options() { id = 3, id_question = 2, input_type = "textarea", option_text = "Order Export to ERP", order = 2 },
                    new question_options() { id = 4, id_question = 2, input_type = "textarea", option_text = "Order Confirmation from ERP", order = 3 },
                    new question_options() { id = 5, id_question = 2, input_type = "textarea", option_text = "Images", order = 4 },

                    // Responsible data access
                    new question_options() { id = 6, id_question = 3, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 7, id_question = 3, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 8, id_question = 3, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 9, id_question = 3, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible technical question
                    new question_options() { id = 10, id_question = 4, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 11, id_question = 4, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 12, id_question = 4, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 13, id_question = 4, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible Point6 specific business logic workflow
                    new question_options() { id = 14, id_question = 5, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 15, id_question = 5, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 16, id_question = 5, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 17, id_question = 5, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible setting up user access to the site
                    new question_options() { id = 18, id_question = 6, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 19, id_question = 6, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 20, id_question = 6, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 21, id_question = 6, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 },

                    // Responsible receive notifications when a user requests access
                    new question_options() { id = 22, id_question = 7, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 23, id_question = 7, input_type = "input-text", option_text = "E-Mail", order = 2 },
                    new question_options() { id = 24, id_question = 7, input_type = "input-text", option_text = "Phone", order = 3 },
                    new question_options() { id = 25, id_question = 7, input_type = "drop-down", option_text = "Best way to reach", values_list= "e-mail,phone", order = 4 }

            );
        }

        private void SeedQuestionOptionsDomain(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,
                    new question_options() { id = 26, id_question = 8, input_type = "textarea", order = 1 }
            );
        }

        private void SeedQuestionOptionsBrands(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,
                    
                    // Are there multiple brands?
                    new question_options() { id = 27, id_question = 9, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 28, id_question = 9, input_type = "radio", option_text = "No", order = 2 },

                    // Can brands be combined on one order?
                    new question_options() { id = 29, id_question = 10, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 30, id_question = 10, input_type = "radio", option_text = "No", order = 2 },

                    // Are customers assigned a brand in the ERP?
                    new question_options() { id = 31, id_question = 11, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 32, id_question = 11, input_type = "radio", option_text = "No", order = 2 },

                    // Are lookups (colors, genders, sales reps, shipping options) brand specific?
                    new question_options() { id = 33, id_question = 12, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 34, id_question = 12, input_type = "radio", option_text = "No", order = 2 },

                    //Please mark what option below might be different per brand
                    new question_options() { id = 35, id_question = 13, input_type = "checkbox", option_text = "Gender", order = 1 },
                    new question_options() { id = 36, id_question = 13, input_type = "checkbox", option_text = "Color", order = 2 },
                    new question_options() { id = 37, id_question = 13, input_type = "checkbox", option_text = "Product Category", order = 3 },
                    new question_options() { id = 38, id_question = 13, input_type = "checkbox", option_text = "Season", order = 4 },
                    new question_options() { id = 39, id_question = 13, input_type = "checkbox", option_text = "Shipping Methods", order = 5 },
                    new question_options() { id = 40, id_question = 13, input_type = "checkbox", option_text = "Customer Terms", order = 6 },
                    new question_options() { id = 41, id_question = 13, input_type = "checkbox", option_text = "Salespeople", order = 7 },
                    new question_options() { id = 42, id_question = 13, input_type = "checkbox", option_text = "Divisions", order = 8 },

                    // Are there separate divisions within a brand?
                    new question_options() { id = 43, id_question = 14, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 44, id_question = 14, input_type = "radio", option_text = "No", order = 2 },

                    // Can divisions be combined on one order?
                    new question_options() { id = 45, id_question = 15, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 46, id_question = 15, input_type = "radio", option_text = "No", order = 2 },

                    // Do you want a division filter?
                    new question_options() { id = 47, id_question = 16, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 48, id_question = 16, input_type = "radio", option_text = "No", order = 2 },

                    // Are some sales reps limited to what divisions they can sell?
                    new question_options() { id = 49, id_question = 17, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 50, id_question = 17, input_type = "radio", option_text = "No", order = 2 },

                    // Please list our brand names as entered ERP (Repeater)
                    new question_options() { id = 51, id_question = 18, input_type = "input-text", option_text = "Brand Name", order = 1 },
                    new question_options() { id = 52, id_question = 18, input_type = "input-text", option_text = "Address 1", order = 2 },
                    new question_options() { id = 53, id_question = 18, input_type = "input-text", option_text = "Address 2", order = 3 },
                    new question_options() { id = 54, id_question = 18, input_type = "input-text", option_text = "City", order = 4 },
                    new question_options() { id = 55, id_question = 18, input_type = "input-text", option_text = "State", order = 5 },
                    new question_options() { id = 56, id_question = 18, input_type = "input-text", option_text = "Zip", order = 6 },
                    new question_options() { id = 57, id_question = 18, input_type = "input-text", option_text = "Country", order = 7 },
                    new question_options() { id = 58, id_question = 18, input_type = "input-text", option_text = "E-Mail", order = 8 },
                    new question_options() { id = 59, id_question = 18, input_type = "input-file", option_text = "Upload Logo", order = 9 },

                    //Are customers limited to a division in the ERP?
                    new question_options() { id = 59, id_question = 19, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 60, id_question = 19, input_type = "radio", option_text = "No", order = 2 },

                    //Are lookups (colors, genders, sales reps, shipping options) brand specific?
                    new question_options() { id = 61, id_question = 20, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 62, id_question = 20, input_type = "radio", option_text = "No", order = 2 },

                    //Please mark what option below might be different per division.Please check if different per brand
                    new question_options() { id = 63, id_question = 21, input_type = "checkbox", option_text = "Gender", order = 1 },
                    new question_options() { id = 64, id_question = 21, input_type = "checkbox", option_text = "Color", order = 2 },
                    new question_options() { id = 65, id_question = 21, input_type = "checkbox", option_text = "Product Category", order = 3 },
                    new question_options() { id = 66, id_question = 21, input_type = "checkbox", option_text = "Season", order = 4 },
                    new question_options() { id = 67, id_question = 21, input_type = "checkbox", option_text = "Shipping Methods", order = 5 },
                    new question_options() { id = 68, id_question = 21, input_type = "checkbox", option_text = "Customer Terms", order = 6 },
                    new question_options() { id = 69, id_question = 21, input_type = "checkbox", option_text = "Salespeople", order = 7 },
                    new question_options() { id = 70, id_question = 21, input_type = "checkbox", option_text = "Divisions", order = 8 },

                    //Does RepSpark need to be made aware of the warehouses that inventory is stored in?
                    new question_options() { id = 71, id_question = 22, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 72, id_question = 22, input_type = "radio", option_text = "No", order = 2 },

                    //Are customers limited in the ERP to what warehouses they can order from?
                    new question_options() { id = 73, id_question = 23, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 74, id_question = 23, input_type = "radio", option_text = "No", order = 2 },

                    //Are there multiple seasons?
                    new question_options() { id = 75, id_question = 24, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 76, id_question = 24, input_type = "radio", option_text = "No", order = 2 },

                    //Can seasons be combined on one order?
                    new question_options() { id = 77, id_question = 25, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 78, id_question = 25, input_type = "radio", option_text = "No", order = 2 },

                    //Do you want a season filter?
                    new question_options() { id = 79, id_question = 26, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 80, id_question = 26, input_type = "radio", option_text = "No", order = 2 },

                    //Do you want the sales person selecting the season of the order on the header ?
                    new question_options() { id = 81, id_question = 27, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 82, id_question = 27, input_type = "radio", option_text = "No", order = 2 },

                    //Customer: Is there a billing address with different stores (shippingaddresses)?
                    new question_options() { id = 83, id_question = 28, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 84, id_question = 28, input_type = "radio", option_text = "No", order = 2 },

                    //Customer: confirm each address in the ERP is both the billing and shipping address
                    new question_options() { id = 85, id_question = 29, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 86, id_question = 29, input_type = "radio", option_text = "No", order = 2 },

                    //Does customer have default terms?
                    new question_options() { id = 87, id_question = 30, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 88, id_question = 30, input_type = "radio", option_text = "No", order = 2 },

                    //Do you want to apply customer specific discounts?
                    new question_options() { id = 89, id_question = 31, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 90, id_question = 31, input_type = "radio", option_text = "No", order = 2 },

                    //Order Type: What are the order types available?
                    new question_options() { id = 91, id_question = 32, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 92, id_question = 32, input_type = "input-date", option_text = "Start Date Default - Days from today", order = 2 },
                    new question_options() { id = 93, id_question = 32, input_type = "input-date", option_text = "Start Date: What is the Max number of Days from today?", order = 3 },
                    new question_options() { id = 94, id_question = 32, input_type = "input-date", option_text = "Cancel Date Default", order = 4 },
                    new question_options() { id = 95, id_question = 32, input_type = "input-date", option_text = "Cancel date: What is the Max number days from start date?", order = 5 },
                    new question_options() { id = 96, id_question = 32, input_type = "input-date", option_text = "Cancel date: What is the Min number days from start date?", order = 6 },

                    //order Type available por B2B?
                    new question_options() { id = 97, id_question = 33, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 98, id_question = 33, input_type = "radio", option_text = "No", order = 2 },

                    //Order type: Is Default?
                    new question_options() { id = 99, id_question = 34, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 100, id_question = 34, input_type = "radio", option_text = "No", order = 2 },

                    //Any product limitations limited by order type *Could add custom development work.
                    new question_options() { id = 101, id_question = 35, input_type = "textarea", option_text = "*Could add custom development work", order = 1 },

                    //Dating: Lock Start Date
                    new question_options() { id = 102, id_question = 36, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 103, id_question = 36, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For B2B
                    new question_options() { id = 104, id_question = 37, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 105, id_question = 37, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Reps
                    new question_options() { id = 106, id_question = 38, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 107, id_question = 38, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Admins
                    new question_options() { id = 108, id_question = 39, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 109, id_question = 39, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Managers / CS
                    new question_options() { id = 110, id_question = 40, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 111, id_question = 40, input_type = "radio", option_text = "No", order = 2 },

                    //Cancel Date
                    new question_options() { id = 112, id_question = 41, input_type = "radio", option_text = "Show", order = 1 },
                    new question_options() { id = 113, id_question = 41, input_type = "radio", option_text = "Hide", order = 2 },

                    //Dating: Lock Cancel Date
                    new question_options() { id = 114, id_question = 42, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 115, id_question = 42, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For B2B
                    new question_options() { id = 116, id_question = 43, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 117, id_question = 43, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Reps
                    new question_options() { id = 118, id_question = 44, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 119, id_question = 44, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Admins
                    new question_options() { id = 120, id_question = 45, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 121, id_question = 45, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Managers / CS
                    new question_options() { id = 122, id_question = 46, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 123, id_question = 46, input_type = "radio", option_text = "No", order = 2 },

                    //Terms
                    new question_options() { id = 124, id_question = 47, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 125, id_question = 47, input_type = "radio", option_text = "Hide", order = 2 },

                    //Terms Locks
                    new question_options() { id = 126, id_question = 48, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 127, id_question = 48, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks
                    new question_options() { id = 128, id_question = 49, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 129, id_question = 49, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For B2B
                    new question_options() { id = 130, id_question = 50, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 131, id_question = 50, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For Reps
                    new question_options() { id = 132, id_question = 51, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 133, id_question = 51, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For Admins
                    new question_options() { id = 134, id_question = 52, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 135, id_question = 52, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For Managers / CS
                    new question_options() { id = 136, id_question = 53, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 137, id_question = 53, input_type = "radio", option_text = "No", order = 2 },

                    //Do all customers have default terms?
                    new question_options() { id = 138, id_question = 54, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 139, id_question = 54, input_type = "radio", option_text = "No", order = 2 },

                    //What term code should we default to?
                    new question_options() { id = 140, id_question = 55, input_type = "textarea", option_text = "", order = 1 },

                    //Ship Via
                    new question_options() { id = 141, id_question = 56, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 142, id_question = 56, input_type = "radio", option_text = "Hide", order = 2 },

                    //Ship Via (Options)
                    new question_options() { id = 143, id_question = 57, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 144, id_question = 57, input_type = "radio", option_text = "No", order = 2 },

                    //Ship Via - For B2B
                    new question_options() { id = 145, id_question = 58, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 146, id_question = 58, input_type = "radio", option_text = "No", order = 2 },

                    //Ship Via - For Reps
                    new question_options() { id = 147, id_question = 59, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 148, id_question = 59, input_type = "radio", option_text = "No", order = 2 },

                    //Ship Via - For Admins
                    new question_options() { id = 149, id_question = 60, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 150, id_question = 60, input_type = "radio", option_text = "No", order = 2 },

                    //Ship Via Managers / CS
                    new question_options() { id = 151, id_question = 61, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 152, id_question = 61, input_type = "radio", option_text = "No", order = 2 },
                    
                    //Comments
                    new question_options() { id = 153, id_question = 62, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 154, id_question = 62, input_type = "radio", option_text = "Hide", order = 2 },

                    //One time ship to option
                    new question_options() { id = 155, id_question = 63, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 156, id_question = 63, input_type = "radio", option_text = "Hide", order = 2 },

                    //In-Hand Date
                    new question_options() { id = 157, id_question = 64, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 158, id_question = 64, input_type = "radio", option_text = "Hide", order = 2 },

                    //Is there any value added service (customer specific) information that you want displayed in a popup
                    new question_options() { id = 159, id_question = 65, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 160, id_question = 65, input_type = "radio", option_text = "No", order = 2 },

                    //Fields - there any value added service (if yes). 
                    new question_options() { id = 161, id_question = 66, input_type = "checkbox", option_text = "Customer Type", order = 1 },
                    new question_options() { id = 162, id_question = 66, input_type = "checkbox", option_text = "Contact Name", order = 2 },
                    new question_options() { id = 163, id_question = 66, input_type = "checkbox", option_text = "Email", order = 3 },
                    new question_options() { id = 164, id_question = 66, input_type = "checkbox", option_text = "Phone", order = 4 },

                    //Are there values that must be set on the header in order for the user to proceed in the order process?
                    new question_options() { id = 165, id_question = 67, input_type = "checkbox", option_text = "Selecting a customer", order = 1 },
                    new question_options() { id = 166, id_question = 67, input_type = "checkbox", option_text = "Selecting order type", order = 2 },
                    new question_options() { id = 167, id_question = 67, input_type = "checkbox", option_text = "Entering a purchase order", order = 3 },
                    new question_options() { id = 168, id_question = 67, input_type = "checkbox", option_text = "Selecting an order type", order = 4 },

                    //Default Selections page view for Rep Users?
                    new question_options() { id = 169, id_question = 68, input_type = "checkbox", option_text = "Grid", order = 1 },
                    new question_options() { id = 170, id_question = 68, input_type = "checkbox", option_text = "Lines Quick Add", order = 2 },
                    new question_options() { id = 171, id_question = 68, input_type = "checkbox", option_text = "Lines Size Entry", order = 3 },

                    //Default Selections page view for Customer/B2B?
                    new question_options() { id = 172, id_question = 69, input_type = "checkbox", option_text = "Grid", order = 1 },
                    new question_options() { id = 173, id_question = 69, input_type = "checkbox", option_text = "Lines Quick Add", order = 2 },
                    new question_options() { id = 174, id_question = 69, input_type = "checkbox", option_text = "Lines Size Entry", order = 3 },

                    //Units to Display for Reps
                    new question_options() { id = 175, id_question = 70, input_type = "checkbox", option_text = "True inventory", order = 1 },
                    new question_options() { id = 176, id_question = 70, input_type = "checkbox", option_text = "Limited", order = 2 },
                    new question_options() { id = 177, id_question = 70, input_type = "checkbox", option_text = "Number to Limit at", order = 3 },

                    //Units to Display for Customer/B2B?
                    new question_options() { id = 178, id_question = 71, input_type = "checkbox", option_text = "True inventory", order = 1 },
                    new question_options() { id = 179, id_question = 71, input_type = "checkbox", option_text = "Limited", order = 2 },
                    new question_options() { id = 180, id_question = 71, input_type = "checkbox", option_text = "Number to Limit at", order = 3 },

                    //Do you want the optional fields displayed?
                    new question_options() { id = 181, id_question = 72, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 182, id_question = 72, input_type = "radio", option_text = "No", order = 2 },

                    //Show Retail Price
                    new question_options() { id = 183, id_question = 73, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 184, id_question = 73, input_type = "radio", option_text = "No", order = 2 },

                    //Show Gender
                    new question_options() { id = 185, id_question = 74, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 186, id_question = 74, input_type = "radio", option_text = "No", order = 2 },

                    //Show Season
                    new question_options() { id = 187, id_question = 75, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 188, id_question = 75, input_type = "radio", option_text = "No", order = 2 },

                    //Show Dimension
                    new question_options() { id = 189, id_question = 76, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 190, id_question = 76, input_type = "radio", option_text = "No", order = 2 },

                    //Show Product Group
                    new question_options() { id = 191, id_question = 77, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 192, id_question = 77, input_type = "radio", option_text = "No", order = 2 },

                    //Show Warehouse
                    new question_options() { id = 193, id_question = 78, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 194, id_question = 78, input_type = "radio", option_text = "No", order = 2 },

                    //Show Product Type
                    new question_options() { id = 195, id_question = 79, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 196, id_question = 79, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Color
                    new question_options() { id = 197, id_question = 80, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 198, id_question = 80, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Product Category
                    new question_options() { id = 201, id_question = 81, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 202, id_question = 81, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Are there children product categories?
                    new question_options() { id = 203, id_question = 82, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 204, id_question = 82, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Gender
                    new question_options() { id = 205, id_question = 83, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 206, id_question = 83, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Product Group
                    new question_options() { id = 207, id_question = 84, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 208, id_question = 84, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Warehouse
                    new question_options() { id = 209, id_question = 85, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 210, id_question = 85, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Product Type
                    new question_options() { id = 208, id_question = 86, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 209, id_question = 86, input_type = "radio", option_text = "No", order = 2 },

                    //Standard Filters: Price Band Filter
                    new question_options() { id = 210, id_question = 87, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 210, id_question = 87, input_type = "radio", option_text = "No", order = 2 },

                    //Misc Filters defaulting: Immediate Only
                    new question_options() { id = 211, id_question = 88, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 212, id_question = 88, input_type = "radio", option_text = "No", order = 2 },

                    //Misc Filters defaulting: Available Only
                    new question_options() { id = 213, id_question = 89, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 214, id_question = 89, input_type = "radio", option_text = "No", order = 2 },

                    //Misc Filters defaulting: Only Available Products
                    new question_options() { id = 215, id_question = 90, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 215, id_question = 90, input_type = "radio", option_text = "No", order = 2 }

            );
        }

    }
}