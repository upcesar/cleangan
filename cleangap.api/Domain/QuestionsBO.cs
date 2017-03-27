using cleangap.api.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Domain
{
    public interface IQuestionsBO
    {
        bool CreateSections(int CustomderID);
        void Load(int page);

    }
    public class QuestionsBO : IQuestionsBO
    {
        public bool CreateSections(int CustomderID)
        {
            bool created = false;
            using (var db = new CleanGapDataContext())
            {
                //

                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;



                db.SaveChanges();
            }

            return created;
        }

        public void Load(int NumPage)
        {
            using (var db = new CleanGapDataContext())
            {
                var qResult = db.questions.Where(q => q.page == NumPage).OrderBy(q=>q.id).ToList();

            }
        }
    }
}