using RMM_Server.Models;
using System.Collections.Generic;
using RMM_Server.Contracts;
using System.Linq;
using RMM_Server.Services;

namespace RMM_Server.Domains
{
    public class StudentDomain : IStudentDomain
    {
        private readonly IStudentRepository isr;
        private readonly IFacultyDomain ifr;

        public StudentDomain(IStudentRepository isr, IFacultyDomain ifr)
        {
            this.isr = isr;
            this.ifr = ifr;
        }

        public Student GetStudent(string id)
        {
            Student result = isr.GetStudent(id);
            return result;
        }

        public List<Student> GetAllStudent()
        {
            List<Student> result = isr.GetAllStudent();
            return result;
        }

        public List<Student> GetAllStudentsByResearch(int research_id)
        {
            List<Student> result = isr.GetAllStudentsByResearch(research_id);
            return result;
        }

        public Student CreateStudent(Student s)
        {
            Student result = isr.CreateStudent(s);
            return result;
        }

        public Student EditStudent(Student s)
        {
            Student result = isr.EditStudent(s);
            return result;
        }

        public void getParsedResume()
        {
            isr.getParsedResume();
        }


        public int ConvertBoolToInt(bool b)
        {
            if (b == true) return 1;
            else return 0;
        }

        //Bottom Methods Added by Jiawen and Amit
        //Student Filter Method = DONE
        public List<Student> GetFilteredAndSearchedStudents(StudentFilter sf)
        {
            List<Student> result;
            if (sf.Keyword == "" && sf.StudentFilterValue.Count > 0)
            {
                result = GetFilteredStudents(sf);
            }
            else if (sf.Keyword == "" && sf.StudentFilterValue.Count == 0)
            {
                  result = isr.GetAllStudent();
               // else result = GetSortedStudentsByFacultyID(sf.psuID);
            }
            else
            {
                result = GetSearchedStudentByKeyword(sf.Keyword, sf.Student);
                sf.Student = result;
                if (sf.StudentFilterValue.Count > 0) result = GetFilteredStudents(sf);
            }

            result = result.GroupBy(x => x.Student_Id).OrderByDescending(c => c.Count()).SelectMany(c => c.Select(x => x)).Distinct().ToList();
            return result;
        }

        //Filtered Students Method
        public List<Student> GetFilteredStudents(StudentFilter sf)
        {
            List<Student> filteredResults = new List<Student>();
            List<Student> temp = new List<Student>();

            foreach (StudentFilterValue sfv in sf.StudentFilterValue)
            {
                if (sfv.CategoryValue == "Major")
                {
                    temp = sf.Student.Where(x => x.Major == sfv.CheckedValue).ToList();

                }
                if (sfv.CategoryValue == "Location")
                {
                    temp = sf.Student.Where(x => x.PreferLocation == sfv.CheckedValue).ToList();
                }
                if (sfv.CategoryValue == "Incentive")
                {
                    if (sfv.CheckedValue == "Paid") temp = sf.Student.Where(x => x.PreferPaid == true).ToList();
                    if (sfv.CheckedValue == "Nonpaid") temp = sf.Student.Where(x => x.PreferNonpaid == true).ToList();
                    if (sfv.CheckedValue == "Credit") temp = sf.Student.Where(x => x.PreferCredit == true).ToList();
                }
                filteredResults.AddRange(temp);
            }

            return filteredResults;
        }

        //keyword student = DONE
        public List<Student> GetSearchedStudentByKeyword(string keyword, List<Student> student)
        {
            StudentSearchService ss = new StudentSearchService();
            List<Student> temp = ss.Search(keyword, student);
            var searchedResults = temp.Where(x => x.SearchScore > 0).OrderByDescending(x => x.SearchScore).ToList();
            return searchedResults;
        }

        //sort by faculty id
        
        public List<Student> GetSortedStudentsByFacultyID(string s)
        {
            
            //DepartmentDomain dd = new DepartmentDomain();
            List<Student> result = GetAllStudent();
            //List<Research> activeResearch = result.Where(x => x.Active == true).ToList();
            Faculty faculty = ifr.GetFaculty(s);

            //Have to get each student list
            
            /*
            foreach (Student s in result)
            {
                //not sure about this one
                //r.Major = dd.GetSubDeptByResearchId(r.Id
                s.StudentNames = sd.GetStudent(s.Id);
                
              
            }*/

            //var sortedByMinor = result.OrderByDescending(x => x.Id).ThenByDescending(x => x.PreferLocation == student.PreferLocation).ThenBy(x => x.ResearchDepts[0] == student.Minor).ThenBy(x => x.ResearchDepts[1] == student.Minor).ThenBy(x => x.ResearchDepts[2] == student.Minor).ToList();
            //var sortedByMajor = sortedByMinor.OrderByDescending(x => x.ResearchDepts[0] == student.Major).ThenBy(x => x.ResearchDepts[1] == student.Major && x.Active == true).ThenBy(x => x.ResearchDepts[2] == student.Major && x.Active == true).ToList();
            //var sortedByStatus = sortedByMajor.OrderByDescending(x => x.Active == true).ToList();

            var sortedByName = result.OrderByDescending(x => x.First_Name);

            return (List<Student>)sortedByName;
            
        }
        
    }
}
