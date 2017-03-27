using cleangap.api.DAL;
using cleangap.api.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Domain
{

    public interface IQuestionSectionsBO
    {
        List<question_sections> ToList();
    }

    public class QuestionSectionsBO : IQuestionSectionsBO
    {
        private int intQtySubSection;

        public QuestionSectionsBO()
        {
            intQtySubSection = 1;
        }

        public QuestionSectionsBO(int qtySubSections)
        {
            intQtySubSection = qtySubSections;
        }



        public List<question_sections> ToList()
        {
            List<question_sections> tblSections = new List<question_sections>();

            using (var db = new CleanGapDataContext())
            {
                tblSections = db.question_sections.ToList();
                List<questions> tblQuestions = db.questions.ToList();
            }

            return tblSections;
        }
    }
}