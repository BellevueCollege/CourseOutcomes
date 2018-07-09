using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CourseOutcomesWCF.Model;
using System.Data.Objects;

namespace CourseOutcomesWCF
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CourseOutcomes" in code, svc and config file together.
	public class CourseOutcomes : IService1
	{
        public string GetCourseOutcome(string courseSubject, string courseNumber)
        {


            string learningOutcomeOutput = String.Format("<ul><li>No outcome found for {0} {1}</li></ul>", courseSubject, courseNumber);

            using (CurriculumEntities db = new CurriculumEntities())
            {
                courseSubject = courseSubject.Replace('^', '&');
                var results = db.usp_SELECT_CourseOutcomes(courseSubject.Trim(), courseNumber.Trim());

                foreach (string outcome in results)
                {
                    learningOutcomeOutput = outcome;
                }

            }

            return learningOutcomeOutput;
        }

        public string GetData(int data)
		{
			return string.Format("You entered: {0}", data);
		}

        public CompositeType GetDataUsingDataContract(CompositeType composite)
		{
			if (composite == null)
			{
				throw new ArgumentNullException("composite");
			}
			if (composite.BoolValue)
			{
				composite.StringValue += "Suffix";
			}
			return composite;
		}
	}
}
