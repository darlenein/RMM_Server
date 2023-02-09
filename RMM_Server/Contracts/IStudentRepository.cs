﻿using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IStudentRepository
    {
        public Student GetStudent(string id);
        public List<Student> GetAllStudent();
        public List<Student> GetAllStudentsByResearch(int research_id);
        public Student CreateStudent(Student s);
        public Student EditStudent(Student s);
        public void DeleteStudentByID(string id);
        public void getParsedResume();

    }
}