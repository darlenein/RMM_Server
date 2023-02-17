using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IStudentDomain
    {
        public Student GetStudent(string id); //[x]
        public List<Student> GetAllStudent(); //[x]
        public List<Student> GetAllStudentsByResearch(int research_id); //[x][x]
        public Student CreateStudent(Student s); //[x]
        public Student EditStudent(Student s);
        public void getParsedResume();

        //new
        public List<Student> GetFilteredAndSearchedStudents(StudentFilter sf);
        public List<Student> GetFilteredStudents(StudentFilter sf);
        public List<Student> GetSearchedStudentByKeyword(string keyword, List<Student> student);
        //public List<Student> GetSortedStudentsByFacultyID(string s);

    }
}
