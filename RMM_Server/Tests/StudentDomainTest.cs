using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RMM_Server.Contracts;
using RMM_Server.Domains;
using RMM_Server.Models;
using RMM_Server.Services;
using Moq;

namespace RMM_Server.Tests
{
    // mark class as a test
    [TestFixture] 
    public class StudentDomainTest
    {
        private StudentDomain sd;
        private readonly Mock<IStudentRepository> msr = new Mock<IStudentRepository>();
        private readonly Mock<IFacultyDomain> mfd = new Mock<IFacultyDomain>();
        private readonly Mock<IResearchDomain> mrd = new Mock<IResearchDomain>();
        private Student s = new Student();
        private List<Student> sl = new List<Student>();

        // identify method to be called before running each test 
        [SetUp]
        public void SetUp()
        {
            sd = new StudentDomain(msr.Object, mfd.Object);
            s = new Student()
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

            Student s_test = new Student()
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
            sl.Add(s_test);
            s_test = new Student()
            {
                Student_Id = "testID1",
                First_Name = "FirstName1",
                Last_Name = "LastName1",
                GPA = 4.00,
                Graduation_Month = "May",
                Graduation_Year = "2026",
                Major = "Software Engineer",
                Skills = "Python; SQL;",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "SWE Interest",
                Research_Project = "SWE Projects",
                Email = "Student1@gmail.com",
                PreferPaid = true,
                PreferNonpaid = false,
                PreferCredit = true,
                PreferLocation = "Online",
                Minor = "Computer Engineer"
            };
            sl.Add(s_test);
        }

        [Test]
        public void TestCreateStudentCreatesStudent()
        {
            //arrange
            msr.Setup(x => x.CreateStudent(It.IsAny<Student>()))
                .Returns(s);

            //act
            var result = sd.CreateStudent(s);

            //assert
            Assert.NotNull(result);
            msr.Verify(x => x.CreateStudent(It.IsAny<Student>()), Times.Once);
        }

        [Test]
        public void TestGetStudent()
        {
            //arrange
            msr.Setup(x => x.GetStudent(It.IsAny<string>()))
                .Returns(s);

            //act
            var result = sd.GetStudent(s.Student_Id);

            //assert
            Assert.NotNull(result);
            msr.Verify(x => x.GetStudent(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TestGetAllStudentsByResearch()
        {
            List<Research> rl = new List<Research>();
            Research r = new Research()
            {
                Research_Id = 18,
                Faculty_Id = "oxe2",
                Name = "test",
                Description = "test",
                Location = "test",
                Required_Skills = "Python ;",
                Encouraged_Skills = " ;",
                Start_Date = "11/19/2023",
                End_Date = "11/19/2023",
                Active = false,
                Address = "test",
                IsPaid = false,
                IsNonpaid = false,
                IsCredit = false,
                First_Name = "Ola",
                Last_Name = "El-Rashiedy"
            };
            rl.Add(r);
            r = new Research()
            {
                Research_Id = 19,
                Faculty_Id = "nii1",
                Name = "test2",
                Description = "test2",
                Location = "test2",
                Required_Skills = "Python ;",
                Encouraged_Skills = "SQL ;",
                Start_Date = "11/19/2023",
                End_Date = "11/19/2023",
                Active = false,
                Address = "test2",
                IsPaid = false,
                IsNonpaid = false,
                IsCredit = false,
                First_Name = "Nassem",
                Last_Name = "Ibrahim"
            };
            rl.Add(r);
            r = new Research()
            {
                Research_Id = 20,
                Faculty_Id = "oxe2",
                Name = "test3",
                Description = "test3",
                Location = "test3",
                Required_Skills = "Python ; SQL",
                Encouraged_Skills = "test3",
                Start_Date = "11/19/2023",
                End_Date = "11/19/2023",
                Active = false,
                Address = "test3",
                IsPaid = false,
                IsNonpaid = false,
                IsCredit = false,
                First_Name = "Ola",
                Last_Name = "El-Rashiedy"
            };
            rl.Add(r);

            Participant p;


            //arrange
            msr.Setup(x => x.GetAllStudentsByResearch(It.IsAny<int>()))
                .Returns(sl);

            //act
            var result = sd.GetAllStudentsByResearch(19);

            //assert
            Assert.NotNull(result);
            msr.Verify(x => x.GetAllStudentsByResearch(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void TestGetAllStudent()
        {
            //arrange
            msr.Setup(x => x.GetAllStudent())
                .Returns(sl);

            //act
            var result = sd.GetAllStudent();

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 2);
            msr.Verify(x => x.GetAllStudent(), Times.Once);
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
