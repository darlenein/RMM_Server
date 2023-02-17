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
        private Mock<IStudentRepository> msr;
        private Mock<IFacultyDomain> mfd;
        private Mock<IResearchDomain> mrd;
        private Student s = new Student();
        private List<Student> sl = new List<Student>();

        // identify method to be called before running each test 
        [SetUp]
        public void SetUp()
        {
            mfd = new Mock<IFacultyDomain>();
            msr = new Mock<IStudentRepository>();
            mrd = new Mock<IResearchDomain>();
            sl = new List<Student>();
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
        public void TestGetAllStudentsByResearch_NoStudentsApplied_ReturnsNothing()
        {
            //arrange
            msr.Setup(x => x.GetAllStudentsByResearch(It.IsAny<int>()))
                .Returns(new List<Student>());

            //act
            var result = sd.GetAllStudentsByResearch(19);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 0);
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
