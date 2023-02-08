using RMM_Server.Models;
using System.Collections.Generic;
using RMM_Server.Contracts;

namespace RMM_Server.Domains
{
    public class StudentDomain : IStudentDomain
    {
        private readonly IStudentRepository isr;

        public StudentDomain(IStudentRepository isr)
        {
            this.isr = isr;
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

        public void DeleteStudentByID(string id)
        {
            isr.DeleteStudentByID(id);
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
    }
}
