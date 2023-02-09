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
        /*private StudentDomain sd;

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
                Student_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
                GPA = 2.98,
                Graduation_Month = "May",
                Graduation_Year = "2025",
                Major = "Biology",
                Skills = "Can use Centrifuge",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "Bio Interest",
                Research_Project = "Bio Projects",
                Email = "Student@gmail.com",
                PreferPaid = true,
                PreferNonpaid = false,
                PreferCredit = true,
                PreferLocation = "Online",
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

        }*/
    }
}
