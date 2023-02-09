using MySql.Data.MySqlClient;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Affinda.API;
using Affinda.API.Models;

namespace RMM_Server.Domains
{
    public class StudentDomain
    {

        
        public Student GetStudent(string id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * from student WHERE student_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            Student s = new Student();
            while (reader.Read())
            {
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FirstName = ConvertFromDBVal<string>(reader[1]);
                s.LastName = ConvertFromDBVal<string>(reader[2]);
                s.GPA = ConvertFromDBVal<double>(reader[3]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[4]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[5]);
                s.Major = ConvertFromDBVal<string>(reader[6]);
                s.Skills = ConvertFromDBVal<string>(reader[7]);
                s.Link1 = ConvertFromDBVal<string>(reader[8]);
                s.Link2 = ConvertFromDBVal<string>(reader[9]);
                s.Link3 = ConvertFromDBVal<string>(reader[10]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[12]);
                s.Email = ConvertFromDBVal<string>(reader[13]);
                if (ConvertFromDBVal<int>(reader[14]) == Convert.ToSByte(1))
                {
                    s.PreferPaid = true;
                }
                else
                {
                    s.PreferPaid = false;
                }
                if (ConvertFromDBVal<int>(reader[15]) == Convert.ToSByte(1))
                {
                    s.PreferNonpaid = true;
                }
                else
                {
                    s.PreferNonpaid = false;
                }
                if (ConvertFromDBVal<int>(reader[16]) == Convert.ToSByte(1))
                {
                    s.PreferCredit = true;
                }
                else
                {
                    s.PreferCredit = false;
                }
                s.PreferLocation = ConvertFromDBVal<string>(reader[17]);
                s.Minor = ConvertFromDBVal<string>(reader[18]);
            }
            reader.Close();

            return s;
        }

        public List<Student> GetAllStudent()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * FROM student";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Student> sl = new List<Student>();
            while (reader.Read())
            {
                Student s = new Student();
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FirstName = ConvertFromDBVal<string>(reader[1]);
                s.LastName = ConvertFromDBVal<string>(reader[2]);
                s.GPA = ConvertFromDBVal<double>(reader[3]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[4]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[5]);
                s.Major = ConvertFromDBVal<string>(reader[6]);
                s.Skills = ConvertFromDBVal<string>(reader[7]);
                s.Link1 = ConvertFromDBVal<string>(reader[8]);
                s.Link2 = ConvertFromDBVal<string>(reader[9]);
                s.Link3 = ConvertFromDBVal<string>(reader[10]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[12]);
                s.Email = ConvertFromDBVal<string>(reader[13]);
                if (ConvertFromDBVal<int>(reader[14]) == 1)
                {
                    s.PreferPaid = true;
                }
                else
                {
                    s.PreferPaid = false;
                }
                if (ConvertFromDBVal<int>(reader[15]) == 1)
                {
                    s.PreferNonpaid = true;
                }
                else
                {
                    s.PreferNonpaid = false;
                }
                if (ConvertFromDBVal<int>(reader[16]) == 1)
                {
                    s.PreferCredit = true;
                }
                else
                {
                    s.PreferCredit = false;
                }
                s.PreferLocation = ConvertFromDBVal<string>(reader[17]);
                s.Minor = ConvertFromDBVal<string>(reader[18]);
                sl.Add(s);
            }
            reader.Close();

            return sl;
        }

        public List<Student> GetAllStudentsByResearch(int research_id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, b.progress_bar, c.name FROM student AS a " +
                $"JOIN participant AS b ON a.student_id = b.student_id " +
                $"JOIN research AS c ON b.research_id = c.research_id " +
                $"WHERE b.research_id = '{research_id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Student> sl = new List<Student>();
            while (reader.Read())
            {
                Student s = new Student();
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FirstName = ConvertFromDBVal<string>(reader[1]);
                s.LastName = ConvertFromDBVal<string>(reader[2]);
                s.GPA = ConvertFromDBVal<double>(reader[3]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[4]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[5]);
                s.Major = ConvertFromDBVal<string>(reader[6]);
                s.Skills = ConvertFromDBVal<string>(reader[7]);
                s.Link1 = ConvertFromDBVal<string>(reader[8]);
                s.Link2 = ConvertFromDBVal<string>(reader[9]);
                s.Link3 = ConvertFromDBVal<string>(reader[10]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[12]);
                s.Email = ConvertFromDBVal<string>(reader[13]);
                if (ConvertFromDBVal<int>(reader[14]) == 1)
                {
                    s.PreferPaid = true;
                }
                else
                {
                    s.PreferPaid = false;
                }
                if (ConvertFromDBVal<int>(reader[15]) == 1)
                {
                    s.PreferNonpaid = true;
                }
                else
                {
                    s.PreferNonpaid = false;
                }
                if (ConvertFromDBVal<int>(reader[16]) == 1)
                {
                    s.PreferCredit = true;
                }
                else
                {
                    s.PreferCredit = false;
                }
                s.PreferLocation = ConvertFromDBVal<string>(reader[17]);
                s.Minor = ConvertFromDBVal<string>(reader[18]);
                s.Progression = ConvertFromDBVal<int>(reader[19]);
                s.Research_name = ConvertFromDBVal<string>(reader[20]);

                sl.Add(s);
            }
            reader.Close();

            return sl;
        }

        public Student CreateStudent(Student s)
        {
            int paid = ConvertBoolToInt(s.PreferPaid);
            int nonpaid = ConvertBoolToInt(s.PreferNonpaid);
            int credit = ConvertBoolToInt(s.PreferCredit);

            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"INSERT into student VALUES (" +
                $" '{s.Id}', '{s.FirstName}', '{s.LastName}', '{s.GPA}', '{s.GraduationMonth}', '{s.GraduationYear}'," +
                $" '{s.Major}', '{s.Skills}', '{s.Link1}', '{s.Link2}', '{s.Link3}', '{s.ResearchInterest}'," +
                $" '{s.ResearchProject}', '{s.Email}', '{paid}', '{nonpaid}', '{credit}', '{s.PreferLocation}', '{s.Minor}')";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();

            return s;
        }

        public Student EditStudent(Student s)
        {
            int paid = ConvertBoolToInt(s.PreferPaid);
            int nonpaid = ConvertBoolToInt(s.PreferNonpaid);
            int credit = ConvertBoolToInt(s.PreferCredit);

            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"UPDATE student " +
                $"SET first_name = '{s.FirstName}',  last_name = '{s.LastName}'," +
                $" GPA = {s.GPA}, graduation_month = '{s.GraduationMonth}'," +
                $" graduation_year = '{s.GraduationYear}', major = '{s.Major}', skills = '{s.Skills}'," +
                $" link1 = '{s.Link1}', link2 = '{s.Link2}', link3 = '{s.Link3}', " +
                $"research_interest = '{s.ResearchInterest}', research_project = '{s.ResearchProject}', " +
                $"email = '{s.Email}', preferPaid = {paid}, preferNonPaid = {nonpaid}," + 
                $"preferCredit = {credit}, minor = '{s.Minor}'" +
                $"WHERE student_id = '{s.Id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();

            return s;
        }

        public void DeleteStudentByID(string id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"DELETE from student WHERE student_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();
        }

        public void getParsedResume()
        {
            string resumePath = "path_to_file.pdf";
            var service = new ParseService("REPLACE_TOKEN");
            var resume = service.CreateResume(resumePath);
        }

        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
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
            if (sf.keyword == "" && sf.studentFilterValue.Count > 0)
            {
                result = GetFilteredStudents(sf);
            }
            else if (sf.keyword == "" && sf.studentFilterValue.Count == 0)
            {
                StudentDomain sd = new StudentDomain();
                if (sf.psuID == "") result = sd.GetAllStudent();
                else result = sd.GetSortedStudentsByFacultyID(sf.psuID);
            }
            else
            {
                result = GetSearchedStudentByKeyword(sf.keyword, sf.student);
                sf.student = result;
                if (sf.studentFilterValue.Count > 0) result = GetFilteredStudents(sf);
            }

            result = result.GroupBy(x => x.Id).OrderByDescending(c => c.Count()).SelectMany(c => c.Select(x => x)).Distinct().ToList();
            return result;
        }

        //Filtered Students Method
        public List<Student> GetFilteredStudents(StudentFilter sf)
        {
            List<Student> filteredResults = new List<Student>();
            List<Student> temp = new List<Student>();

            foreach (StudentFilterValue sfv in sf.studentFilterValue)
            {
                if (sfv.categoryValue == "Major")
                {
                    temp = sf.student.Where(x => x.Major == sfv.checkedValue).ToList();

                }
                if (sfv.categoryValue == "Status")
                {
                    //bool value = Convert.ToBoolean(fv.checkedValue);
                    //temp = f.research.Where(x => x.Active == value).ToList();
                }
                if (sfv.categoryValue == "Location")
                {
                    temp = sf.student.Where(x => x.PreferLocation == sfv.checkedValue).ToList();
                }
                if (sfv.categoryValue == "Incentive")
                {
                    if (sfv.checkedValue == "Paid") temp = sf.student.Where(x => x.PreferPaid == true).ToList();
                    if (sfv.checkedValue == "Nonpaid") temp = sf.student.Where(x => x.PreferNonpaid == true).ToList();
                    if (sfv.checkedValue == "Credit") temp = sf.student.Where(x => x.PreferCredit == true).ToList();
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
            FacultyDomain fd = new FacultyDomain();
            //DepartmentDomain dd = new DepartmentDomain();
            StudentDomain sd = new StudentDomain();
            List<Student> result = GetAllStudent();
            //List<Research> activeResearch = result.Where(x => x.Active == true).ToList();
            Faculty faculty = fd.GetFaculty(s);

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

            var sortedByName = result.OrderByDescending(x => x.FirstName);

            return (List<Student>)sortedByName;
            
        }
        
    }



}
