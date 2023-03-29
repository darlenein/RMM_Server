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
        private readonly IResearchRepository irr;

        public StudentDomain(IStudentRepository isr, IFacultyDomain ifr, IResearchRepository irr)
        {
            this.isr = isr;
            this.ifr = ifr;
            this.irr = irr;
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

        public void EditStudent(Student s)
        {
            isr.EditStudent(s);
            
        }

        public void getParsedResume()
        {
            isr.GetParsedResume();
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

        public void InsertIntoStudentHiddenResearch(string student_id, int research_id)
        {
            isr.InsertIntoStudentHiddenResearch(student_id, research_id);
        }

        public List<Student> GetAllRankedStudentsByResearch(int research_id)
        {
            var result = RankStudentToResearch(research_id);
            return result;
        }

        public List<Student> RankStudentToResearch(int research_id)
        {
            MatchService ms = new MatchService();

            // get all students by research id 
            List<Student> result = GetAllStudentsByResearch(research_id);
            Research r = irr.GetResearchByID(research_id);

            r.Required_Skills = r.Required_Skills.Trim();
            r.Encouraged_Skills = r.Encouraged_Skills.Trim();

            float match_score = 0;


            foreach (Student s in result)
            {
                match_score = 0;
                // for each: majors, minors, double major match (+0.3, +0.1, +0.3) 
                foreach (string dept in r.ResearchDepts)
                {
                    if (dept == s.Major || (dept == s.Major2 && s.Major2 != null))
                    {
                        match_score += (float)0.3;
                    }
                    if (dept == s.Minor && s.Minor != null)
                    {
                        match_score += (float)0.1;
                    }
                }

                // if research_location == student's preferred location, match_score +0.1
                if (r.Location == s.PreferLocation)
                {
                    match_score += (float)0.1;
                }

                // if research isCredit, isPaid, isNonpaid == student's, foreach match_score +0.05
                if (r.IsPaid == s.PreferPaid && s.PreferPaid == true)
                {
                    match_score += (float)0.05;
                }
                if (r.IsNonpaid == s.PreferNonpaid && s.PreferNonpaid == true)
                {
                    match_score += (float)0.05;
                }
                if (r.IsCredit == s.PreferCredit && s.PreferCredit == true)
                {
                    match_score += (float)0.05;
                }

                // get list of required skills, if matches student +0.2 and remove from student skill list 
                s.Skills = s.Skills.Trim();
                string[] tokenized_StudentSkills = ms.TokenizeString(s.Skills);

                if (r.Required_Skills != ";" && s.Skills != ";")
                {
                    // if skills not empty, get list of student skill, noramlize, and tokenize 
                    string[] tokenized_RequiredSkill = ms.TokenizeString(r.Required_Skills);

                    // for each required skill, see if it matches for each student skill
                    foreach (string rs in tokenized_RequiredSkill)
                    {
                        int counter = 0;
                        foreach (string ss in tokenized_StudentSkills)
                        {
                            if (rs == ss)
                            {
                                match_score += (float)0.5;

                                // remove fromt list 
                                tokenized_StudentSkills = tokenized_StudentSkills.Where(x => x != tokenized_StudentSkills[counter]).ToArray();
                                break;
                            }
                            counter++;
                        }
                    }
                }

                // get list of encouraged skills, if matches student +0.1, and remove from list 
                if (r.Encouraged_Skills != ";" && s.Skills != ";")
                {
                    // if skills not empty, get list of student skill, noramlize, and tokenize 
                    string[] tokenized_EncouragedSkill = ms.TokenizeString(r.Encouraged_Skills);
                    foreach (string es in tokenized_EncouragedSkill)
                    {
                        int counter = 0;
                        foreach (string ss in tokenized_StudentSkills)
                        {
                            if (es == ss)
                            {
                                match_score += (float)0.1;
                                tokenized_StudentSkills = tokenized_StudentSkills.Where(x => x != tokenized_StudentSkills[counter]).ToArray();
                                break;
                            }
                            counter++;
                        }
                    }
                }
                s.MatchScore = match_score;

            }

            // sort list by match_score
            result = result.OrderByDescending(x => x.MatchScore).ToList();

            return result;
        }

        //sort by faculty id
        /*
        public List<Student> GetSortedStudentsByFacultyID(string s)
        {
            
            List<Student> result = GetAllStudent();
            Faculty faculty = ifr.GetFaculty(s);

            //Have to get each student list
            /*foreach (Student s in result)
            {
                s.StudentIDs = ids.GetStudentByID(s.Student_Id);
            }

            //var sortedByMinor = result.OrderByDescending(x => x.Student_Id).ThenByDescending(x => x.PreferLocation == faculty.).ThenBy(x => x.ResearchDepts[0] == student.Minor).ThenBy(x => x.ResearchDepts[1] == student.Minor).ThenBy(x => x.ResearchDepts[2] == student.Minor).ToList();
            //var sortedByMajor = sortedByMinor.OrderByDescending(x => x.ResearchDepts[0] == student.Major).ThenBy(x => x.ResearchDepts[1] == student.Major && x.Active == true).ThenBy(x => x.ResearchDepts[2] == student.Major && x.Active == true).ToList();
            //var sortedByStatus = sortedByMajor.OrderByDescending(x => x.Active == true).ToList();

            var sortedByName = result.OrderByDescending(x => x.Student_Id).ToList();

            return sortedByName;
            
        }*/

    }
}
