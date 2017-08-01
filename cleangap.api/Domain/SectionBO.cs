using cleangap.api.DAL;
using cleangap.api.Models.Domain;
using cleangap.api.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Domain
{
    public interface ISectionBO
    {
        /// <summary>
        /// Get childer section (subsetions)
        /// </summary>
        List<SectionModel> GetChildren();
        SectionModel GetByQuestion();

    }

    /// <summary>
    /// Section Domain Class
    /// </summary>
    public class SectionBO : ISectionBO
    {
        private int _id;

        /// <summary>
        /// Section ID
        /// </summary>
        public int id { get { return _id; } }

        /// <summary>
        /// Constructor with ID parameters (Section / Question depending on the method call)
        /// </summary>
        public SectionBO(int pId)
        {
            _id = pId;
        }


        private void PopulateSubSections(List<SectionModel> listSection, List<question_sections> tblSection)
        {
            foreach (var item in tblSection)
            {
                QuestionsBO qBO = new QuestionsBO();

                listSection.Add(new SectionModel()
                {
                    id = item.id,
                    name = item.name,
                    questions = qBO.GetBySubSection(item.id)
                });
            }

        }

        /// <summary>
        /// Get childer section (subsetions)
        /// </summary>
        public List<SectionModel> GetChildren()
        {
            List<SectionModel> listSection = new List<SectionModel>();

            using (var db = new CleanGapDataContext())
            {
                var tblSection = db.question_sections
                                   .Where(x => x.parent_section.id == id)
                                   .ToList();

                PopulateSubSections(listSection, tblSection);

            }

            return listSection;
        }

        public SectionModel GetByQuestion()
        {
            using (var db = new CleanGapDataContext())
            {
                var qSection = db.questions.Find(id).question_sections;
                if (qSection.id > 0)
                {
                    return new SectionModel()
                    {
                        id = qSection.id,
                        name = qSection.name,
                    };
                }
            }

            return null;
        }
    }
}