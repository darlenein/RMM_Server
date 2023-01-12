using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RMM_Server.Domains;
using RMM_Server.Models;
using RMM_Server.Services;

namespace RMM_Server.Tests
{
    // mark class as a test
    [TestFixture] 
    public class StudentDomainTest
    {
        private StudentDomain sd;

        // identify method to be called before running each test 
        [SetUp] 
        public void SetUp()
        {
            sd = new StudentDomain();
        }


        [Test]
        public void TestCreateStudentCreatesStudent()
        {
            //arrange
            Student s = new Student()
            {
                Id = "testID",
                FirstName = "FirstName",
                LastName = "LastName",
                GPA = 2.98,
                GraduationMonth = "May",
                GraduationYear = "2025",
                Major = "Biology",
                Skills = "Can use Centrifuge",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                ResearchInterest = "Bio Interest",
                ResearchProject = "Bio Projects",
                Email = "Student@gmail.com",
                PreferPaid = true,
                PreferNonpaid = false,
                PreferCredit = true,
                PreferLocation = 2,
                Minor = "Psychology"
            };

            //act
            var result = sd.CreateStudent(s);
           
            //assert
            Assert.NotNull(result);
            sd.DeleteStudentByID("testID");
        }

        [Test]
        public void TestConvertTrueBoolToInt()
        {
            bool value = true;
            var result = sd.ConvertBoolToInt(value);
            Assert.AreEqual(1, result);
            
        }

        [Test]
        public void TestConvertFalseBoolToInt()
        {
            bool value = false;
            var result = sd.ConvertBoolToInt(value);
            Assert.AreEqual(0, result);

        }
    }
}
