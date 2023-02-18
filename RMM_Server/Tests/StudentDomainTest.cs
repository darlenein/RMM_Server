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
        private Mock<IStudentDomain> msd;
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
        public void TestEditStudentEditsStudent()
        {
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

            Student s2 = new Student()
            {
                Student_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
                GPA = 2.98,
                Graduation_Month = "June",
                Graduation_Year = "2027",
                Major = "Chemistry",
                Skills = "Can make chemicals",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "Chem Interest",
                Research_Project = "Chem Projects",
                Email = "Student@gmail.com",
                PreferPaid = false,
                PreferNonpaid = true,
                PreferCredit = true,
                PreferLocation = "Online",
                Minor = "Psychology"
            };
            //arrange
            msr.Setup(x => x.EditStudent(It.IsAny<Student>()));
                

            //act
            sd.EditStudent(s2);

            //assert
            msr.Verify(x => x.EditStudent(It.IsAny<Student>()), Times.Once);
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
            Assert.AreEqual(result.Student_Id, "testID");
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
            Assert.AreEqual(result.Count, 2);
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

        [Test]
        public void TestGetFilteredandSearchedStudents_NoKeyword()
        {
            List<Student> sl = new List<Student>();
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
            sl.Add(s);
            s = new Student()
            {
                Student_Id = "abc123",
                First_Name = "john",
                Last_Name = "jimbo",
                GPA = 3.98,
                Graduation_Month = "June",
                Graduation_Year = "2027",
                Major = "Chemistry",
                Skills = "Can make chemicals",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "Chem Interest",
                Research_Project = "Chem Projects",
                Email = "john@gmail.com",
                PreferPaid = false,
                PreferNonpaid = true,
                PreferCredit = true,
                PreferLocation = "Online",
                Minor = "Biology"
            };
            sl.Add(s);

            List<StudentFilterValue> sfv = new List<StudentFilterValue>();
            StudentFilterValue sv = new StudentFilterValue()
            {
                CategoryValue = "GPA",
                CheckedValue = "Ascending"
            };
            sfv.Add(sv);

            StudentFilter sf = new StudentFilter()
            {
                Student = sl,
                StudentFilterValue = sfv,
                Keyword = "",
                PsuID = "nii1"
            };

            // mocks
            msr.Setup(x => x.GetAllStudent())
                .Returns(sl);

            List<Student> result = sd.GetFilteredAndSearchedStudents(sf);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Student_Id, "testID");
            Assert.AreEqual(result.Count, 2);
            msr.Verify(x => x.GetAllStudent(), Times.Never);
        }

        [Test]
        public void TestGetFilteredandSearchedStudents_NoKeyword_NoFilterValue()
        {
           Faculty f = new Faculty()
            {
                Faculty_Id = "nii1",
                First_Name = "Naseem",
                Last_Name = "Ibrahim",
                Title = "Title",
                Email = "Email",
                Office = "Office",
                Phone = "Phone",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                About_Me = "About Me",
                Research_Interest = "Research Interests",
                Profile_Url = "pfp"
            };
            List<Student> sl = new List<Student>();
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
            sl.Add(s);
            s = new Student()
            {
                Student_Id = "abc123",
                First_Name = "john",
                Last_Name = "jimbo",
                GPA = 3.98,
                Graduation_Month = "June",
                Graduation_Year = "2027",
                Major = "Chemistry",
                Skills = "Can make chemicals",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "Chem Interest",
                Research_Project = "Chem Projects",
                Email = "john@gmail.com",
                PreferPaid = false,
                PreferNonpaid = true,
                PreferCredit = true,
                PreferLocation = "Online",
                Minor = "Biology"
            };
            sl.Add(s);

            List<StudentFilterValue> sfv = new List<StudentFilterValue>();


            StudentFilter sf = new StudentFilter()
            {
                Student = sl,
                StudentFilterValue = sfv,
                Keyword = "",
                PsuID = "nii1"
            };

            // mocks
            msr.Setup(x => x.GetAllStudent())
                .Returns(sl);
            mfd.Setup(x => x.GetFaculty(It.IsAny<string>()))
                .Returns(f);
 

            List<Student> result = sd.GetFilteredAndSearchedStudents(sf);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Student_Id, "abc123");
            Assert.AreEqual(result.Count, 2);
            msr.Verify(x => x.GetAllStudent(), Times.Once);
        }

        [Test]
        public void TestGetFilteredandSearchedStudents()
        {
            List<Student> sl = new List<Student>();
            Student s = new Student()
            {
                Student_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
                GPA = 2.98,
                Graduation_Month = "May",
                Graduation_Year = "2025",
                Major = "Chemistry",
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
            sl.Add(s);
            s = new Student()
            {
                Student_Id = "abc123",
                First_Name = "john",
                Last_Name = "jimbo",
                GPA = 3.98,
                Graduation_Month = "June",
                Graduation_Year = "2027",
                Major = "Chemistry",
                Skills = "Can make chemicals",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "Crazy stuff",
                Research_Project = "Chem Projects",
                Email = "john@gmail.com",
                PreferPaid = false,
                PreferNonpaid = true,
                PreferCredit = true,
                PreferLocation = "Online",
                Minor = "Biology"
            };
            sl.Add(s);

            List<StudentFilterValue> sfv = new List<StudentFilterValue>();
            StudentFilterValue sv = new StudentFilterValue()
            {
                CategoryValue = "Major",
                CheckedValue = "Chemistry"
            };
            sfv.Add(sv);

            StudentFilter sf = new StudentFilter()
            {
                Student = sl,
                StudentFilterValue = sfv,
                Keyword = "Crazy Stuff",
                PsuID = "nii1"
            };

            // mocks
            msr.Setup(x => x.GetAllStudent())
                .Returns(sl);

            List<Student> result = sd.GetFilteredAndSearchedStudents(sf);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Student_Id, "abc123");
            Assert.AreEqual(result.Count, 1);
            msr.Verify(x => x.GetAllStudent(), Times.Never);
        }

        [Test]
        public void TestGetSearchedStudentByKeyword()
        {
            List<Student> sl = new List<Student>();
            Student s = new Student()
            {
                Student_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
                GPA = 2.98,
                Graduation_Month = "May",
                Graduation_Year = "2025",
                Major = "Chemistry",
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
            sl.Add(s);
            s = new Student()
            {
                Student_Id = "abc123",
                First_Name = "john",
                Last_Name = "jimbo",
                GPA = 3.98,
                Graduation_Month = "June",
                Graduation_Year = "2027",
                Major = "Chemistry",
                Skills = "Can make chemicals",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "Crazy stuff",
                Research_Project = "Chem Projects",
                Email = "john@gmail.com",
                PreferPaid = false,
                PreferNonpaid = true,
                PreferCredit = true,
                PreferLocation = "Online",
                Minor = "Biology"
            };
            sl.Add(s);

            msd.Setup(x => x.GetSearchedStudentByKeyword(It.IsAny<string>(), It.IsAny<List<Student>>()))
                .Returns(sl);

            //act
            var result = sd.GetSearchedStudentByKeyword("john", sl);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void TestGetFilteredStudents()
        {
            List<Student> sl = new List<Student>();
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
            sl.Add(s);
            s = new Student()
            {
                Student_Id = "abc123",
                First_Name = "john",
                Last_Name = "jimbo",
                GPA = 3.98,
                Graduation_Month = "June",
                Graduation_Year = "2027",
                Major = "Chemistry",
                Skills = "Can make chemicals",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                Research_Interest = "Chem Interest",
                Research_Project = "Chem Projects",
                Email = "john@gmail.com",
                PreferPaid = false,
                PreferNonpaid = true,
                PreferCredit = true,
                PreferLocation = "Online",
                Minor = "Biology"
            };
            sl.Add(s);

            List<StudentFilterValue> sfv = new List<StudentFilterValue>();
            StudentFilterValue sv = new StudentFilterValue()
            {
                CategoryValue = "GPA",
                CheckedValue = "Ascending"
            };
            sfv.Add(sv);

            StudentFilter sf = new StudentFilter()
            {
                Student = sl,
                StudentFilterValue = sfv,
                Keyword = "",
                PsuID = "nii1"
            };

            // mocks
            msr.Setup(x => x.GetAllStudent())
                .Returns(sl);

            var result = sd.GetFilteredStudents(sf);

            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 2);
            
        }
    }
    
}
