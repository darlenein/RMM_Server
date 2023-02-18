using NUnit.Framework;
using RMM_Server.Domains;
using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using RMM_Server.Contracts;

namespace RMM_Server.Tests
{
    public class FacultyDomainTest
    {
        private IFacultyDomain fd;
        private Mock<IFacultyRepository> mfr;
        private Mock<IStudentDomain> msd;
        private Mock<IFacultyDomain> mfd;
        private Faculty f = new Faculty();
        private List<Faculty> fl = new List<Faculty>();

        [SetUp]
        public void SetUp()
        {
            fl = new List<Faculty>();
            mfr = new Mock<IFacultyRepository>();
            fd = new FacultyDomain(mfr.Object);

            f = new Faculty()
            {
                Faculty_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
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

            fl = new List<Faculty>();
            Faculty f_test = new Faculty()
            {
                Faculty_Id = "testID2",
                First_Name = "FirstName2",
                Last_Name = "LastName2",
                Title = "Title2",
                Email = "Email2",
                Office = "Office2",
                Phone = "Phone2",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                About_Me = "About Me2",
                Research_Interest = "Research Interests2",
                Profile_Url = "pfp2"
            };
            fl.Add(f_test);
            f = new Faculty()
            {
                Faculty_Id = "testID3",
                First_Name = "FirstName3",
                Last_Name = "LastName3",
                Title = "Title3",
                Email = "Email3",
                Office = "Office3",
                Phone = "Phone3",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                About_Me = "About Me3",
                Research_Interest = "Research Interests2",
                Profile_Url = "pfp3"
            };
            fl.Add(f_test);
        }

        [Test]
        public void TestCreateFacultyCreatesFaculty()
        {
            //arrange
            mfr.Setup(x => x.CreateFaculty(It.IsAny<Faculty>()))
            .Returns(f);

            //act
            var result = fd.CreateFaculty(f);

            //assert
            Assert.NotNull(result);
            mfr.Verify(x => x.CreateFaculty(It.IsAny<Faculty>()), Times.Once());
        }

        [Test]
        public void TestEditFacultyEditsFaculty()
        {
            f = new Faculty()
            {
                Faculty_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
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

           Faculty f2 = new Faculty()
            {
                Faculty_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
                Title = "Title",
                Email = "Email",
                Office = "Office",
                Phone = "Cellphone",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                About_Me = "SWENG",
                Research_Interest = "cool stuff",
                Profile_Url = "pfp"
            };
            //arrange
            mfr.Setup(x => x.EditFaculty(It.IsAny<Faculty>()));
            

            //act
            fd.EditFaculty(f2);

            //assert
            mfr.Verify(x => x.EditFaculty(It.IsAny<Faculty>()), Times.Once());
        }

        [Test]
        public void TestGetFaculty()
        {
            //arrange
            mfr.Setup(x => x.GetFaculty(It.IsAny<string>()))
            .Returns(f);

            //act
            var result = fd.GetFaculty(f.Faculty_Id);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Faculty_Id, "testID3");
            mfr.Verify(x => x.GetFaculty(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void TestGetAllFaculty()
        {
            //arrange
            mfr.Setup(x => x.GetAllFaculty())
                .Returns(fl);

            //act
            var result = fd.GetAllFaculty();

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 2);
            mfr.Verify(x => x.GetAllFaculty(), Times.Once());
        }

        public void TestGetFilteredandSearchedFaculty_NoKeyword()
        {
            //no filters currently for faculty list

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
            List<Faculty> fl = new List<Faculty>();
            Faculty f = new Faculty()
            {
                Faculty_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
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
            fl.Add(f);
            f = new Faculty()
            {
                Faculty_Id = "nii1",
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                Title = "Title",
                Email = "Email",
                Office = "Office",
                Phone = "Phone",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                About_Me = "stuff",
                Research_Interest = "cooler stuff",
                Profile_Url = "pfp"
            };
            fl.Add(f);

            List<FacultyFilterValue> ffv = new List<FacultyFilterValue>();


            FacultyFilter ff = new FacultyFilter()
            {
                faculty = fl,
                facultyFilterValue = ffv,
                keyword = "",
                psuID = "nii1"
            };

            // mocks
            mfr.Setup(x => x.GetAllFaculty())
                .Returns(fl);
            msd.Setup(x => x.GetStudent(It.IsAny<string>()))
                .Returns(s);


            List<Faculty> result = fd.GetFilteredAndSearchedFaculty(ff);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Faculty_Id, "nii1");
            Assert.AreEqual(result.Count, 2);
            mfr.Verify(x => x.GetAllFaculty(), Times.Once);
        }

        public void TestGetFilteredandSearchedFaculty()
        {
            //no filters currently for faculty list

            List<Faculty> fl = new List<Faculty>();
            Faculty f = new Faculty()
            {
                Faculty_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
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
            fl.Add(f);
            f = new Faculty()
            {
                Faculty_Id = "nii1",
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                Title = "Title",
                Email = "Email",
                Office = "Office",
                Phone = "Phone",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                About_Me = "stuff",
                Research_Interest = "cooler stuff",
                Profile_Url = "pfp"
            };
            fl.Add(f);

            List<FacultyFilterValue> ffv = new List<FacultyFilterValue>();


            FacultyFilter ff = new FacultyFilter()
            {
                faculty = fl,
                facultyFilterValue = ffv,
                keyword = "Naseem",
                psuID = "nii1"
            };

            // mocks
            mfr.Setup(x => x.GetAllFaculty())
                .Returns(fl);


            List<Faculty> result = fd.GetFilteredAndSearchedFaculty(ff);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Faculty_Id, "nii1");
            Assert.AreEqual(result.Count, 1);
            mfr.Verify(x => x.GetAllFaculty(), Times.Never);
        }

        public void TestGetSearchedFacultyByKeyword()
        {
          

            List<Faculty> fl = new List<Faculty>();
            Faculty f = new Faculty()
            {
                Faculty_Id = "testID",
                First_Name = "FirstName",
                Last_Name = "LastName",
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
            fl.Add(f);
            f = new Faculty()
            {
                Faculty_Id = "nii1",
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                Title = "Title",
                Email = "Email",
                Office = "Office",
                Phone = "Phone",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                About_Me = "stuff",
                Research_Interest = "cooler stuff",
                Profile_Url = "pfp"
            };
            fl.Add(f);

            mfd.Setup(x => x.GetSearchedFacultyByKeyword(It.IsAny<string>(), It.IsAny<List<Faculty>>()))
                .Returns(fl);

            //act
            var result = fd.GetSearchedFacultyByKeyword("Naseem", fl);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 1);
        }
    }
}
