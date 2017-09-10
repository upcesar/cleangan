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
        private void SeedQuestionOptionsHeaders(DAL.CleanGapDataContext context)
        {
            context.question_options
                   .AddOrUpdate(qo => qo.id,

                    //Customer: Is there a billing address with different stores (shippingaddresses)?
                    new question_options() { id = 84, id_question = 28, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 85, id_question = 28, input_type = "radio", option_text = "No", order = 2 },

                    //Customer: confirm each address in the ERP is both the billing and shipping address
                    new question_options() { id = 86, id_question = 29, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 87, id_question = 29, input_type = "radio", option_text = "No", order = 2 },

                    //Does customer have default terms?
                    new question_options() { id = 88, id_question = 30, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 89, id_question = 30, input_type = "radio", option_text = "No", order = 2 },

                    //Do you want to apply customer specific discounts?
                    new question_options() { id = 90, id_question = 31, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 91, id_question = 31, input_type = "radio", option_text = "No", order = 2 },

                    //Order Type: What are the order types available?
                    new question_options() { id = 92, id_question = 32, input_type = "input-text", option_text = "Name", order = 1 },
                    new question_options() { id = 93, id_question = 32, input_type = "input-date", option_text = "Start Date Default - Days from today", order = 2 },
                    new question_options() { id = 94, id_question = 32, input_type = "input-date", option_text = "Start Date: What is the Max number of Days from today?", order = 3 },
                    new question_options() { id = 95, id_question = 32, input_type = "input-date", option_text = "Cancel Date Default", order = 4 },
                    new question_options() { id = 96, id_question = 32, input_type = "input-date", option_text = "Cancel date: What is the Max number days from start date?", order = 5 },
                    new question_options() { id = 97, id_question = 32, input_type = "input-date", option_text = "Cancel date: What is the Min number days from start date?", order = 6 },

                    //order Type available por B2B?
                    new question_options() { id = 98, id_question = 33, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 99, id_question = 33, input_type = "radio", option_text = "No", order = 2 },

                    //Order type: Is Default?
                    new question_options() { id = 100, id_question = 34, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 101, id_question = 34, input_type = "radio", option_text = "No", order = 2 },

                    //Any product limitations limited by order type *Could add custom development work.
                    new question_options() { id = 102, id_question = 35, input_type = "textarea", option_text = "*Could add custom development work", order = 1 },

                    //Dating: Lock Start Date
                    new question_options() { id = 103, id_question = 36, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 104, id_question = 36, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For B2B
                    new question_options() { id = 105, id_question = 37, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 106, id_question = 37, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Reps
                    new question_options() { id = 107, id_question = 38, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 108, id_question = 38, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Admins
                    new question_options() { id = 109, id_question = 39, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 110, id_question = 39, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Managers / CS
                    new question_options() { id = 111, id_question = 40, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 112, id_question = 40, input_type = "radio", option_text = "No", order = 2 },

                    //Cancel Date
                    new question_options() { id = 113, id_question = 41, input_type = "radio", option_text = "Show", order = 1 },
                    new question_options() { id = 114, id_question = 41, input_type = "radio", option_text = "Hide", order = 2 },

                    //Dating: Lock Cancel Date
                    new question_options() { id = 115, id_question = 42, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 116, id_question = 42, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For B2B
                    new question_options() { id = 117, id_question = 43, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 118, id_question = 43, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Reps
                    new question_options() { id = 119, id_question = 44, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 120, id_question = 44, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Admins
                    new question_options() { id = 121, id_question = 45, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 122, id_question = 45, input_type = "radio", option_text = "No", order = 2 },

                    //Dating: For Managers / CS
                    new question_options() { id = 123, id_question = 46, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 124, id_question = 46, input_type = "radio", option_text = "No", order = 2 },

                    //Terms
                    new question_options() { id = 125, id_question = 47, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 126, id_question = 47, input_type = "radio", option_text = "Hide", order = 2 },

                    //Terms Locks
                    new question_options() { id = 127, id_question = 48, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 128, id_question = 48, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For B2B
                    new question_options() { id = 129, id_question = 49, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 130, id_question = 49, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For Reps
                    new question_options() { id = 131, id_question = 50, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 132, id_question = 50, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For Admins
                    new question_options() { id = 133, id_question = 51, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 134, id_question = 51, input_type = "radio", option_text = "No", order = 2 },

                    //Terms Locks - For Managers / CS
                    new question_options() { id = 135, id_question = 52, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 136, id_question = 52, input_type = "radio", option_text = "No", order = 2 },

                    //Do all customers have default terms?
                    new question_options() { id = 137, id_question = 53, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 138, id_question = 53, input_type = "radio", option_text = "No", order = 2 },

                    //What term code should we default to?
                    new question_options() { id = 139, id_question = 54, input_type = "textarea", option_text = "", order = 1 },

                    //Ship Via
                    new question_options() { id = 140, id_question = 55, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 141, id_question = 55, input_type = "radio", option_text = "Hide", order = 2 },

                    //Ship Via (Options)
                    /*
                    new question_options() { id = 142, id_question = 56, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 143, id_question = 56, input_type = "radio", option_text = "No", order = 2 },
                    */

                    //Ship Via - For B2B
                    new question_options() { id = 144, id_question = 57, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 145, id_question = 57, input_type = "radio", option_text = "No", order = 2 },

                    //Ship Via - For Reps
                    new question_options() { id = 146, id_question = 58, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 147, id_question = 58, input_type = "radio", option_text = "No", order = 2 },

                    //Ship Via - For Admins
                    new question_options() { id = 148, id_question = 59, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 149, id_question = 59, input_type = "radio", option_text = "No", order = 2 },

                    //Ship Via Managers / CS
                    new question_options() { id = 150, id_question = 60, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 151, id_question = 60, input_type = "radio", option_text = "No", order = 2 },

                    //Comments
                    new question_options() { id = 152, id_question = 61, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 153, id_question = 61, input_type = "radio", option_text = "Hide", order = 2 },

                    //One time ship to option
                    new question_options() { id = 154, id_question = 62, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 155, id_question = 62, input_type = "radio", option_text = "Hide", order = 2 },

                    //In-Hand Date
                    new question_options() { id = 156, id_question = 63, input_type = "radio", option_text = "Display", order = 1 },
                    new question_options() { id = 157, id_question = 63, input_type = "radio", option_text = "Hide", order = 2 },

                    //Is there any value added service (customer specific) information that you want displayed in a popup
                    new question_options() { id = 158, id_question = 64, input_type = "radio", option_text = "Yes", order = 1 },
                    new question_options() { id = 159, id_question = 64, input_type = "radio", option_text = "No", order = 2 },

                    //Fields - there any value added service (if yes). 
                    new question_options() { id = 160, id_question = 65, input_type = "checkbox", option_text = "Customer Type", order = 1 },
                    new question_options() { id = 161, id_question = 65, input_type = "checkbox", option_text = "Contact Name", order = 2 },
                    new question_options() { id = 162, id_question = 65, input_type = "checkbox", option_text = "Email", order = 3 },
                    new question_options() { id = 163, id_question = 65, input_type = "checkbox", option_text = "Phone", order = 4 },

                    //Are there values that must be set on the header in order for the user to proceed in the order process?
                    new question_options() { id = 164, id_question = 66, input_type = "checkbox", option_text = "Selecting a customer", order = 1 },
                    new question_options() { id = 165, id_question = 66, input_type = "checkbox", option_text = "Selecting order type", order = 2 },
                    new question_options() { id = 166, id_question = 66, input_type = "checkbox", option_text = "Entering a purchase order", order = 3 },
                    new question_options() { id = 167, id_question = 66, input_type = "checkbox", option_text = "Selecting an order type", order = 4 }

            );
        }

    }
}