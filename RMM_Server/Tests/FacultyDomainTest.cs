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
        private Mock<IFacultyRepository> mfr = new Mock<IFacultyRepository>();
        private Faculty f = new Faculty();
        private List<Faculty> fl = new List<Faculty>();

        [SetUp]
        public void SetUp()
        {
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
        public void TestGetFaculty()
        {
            //arrange
            mfr.Setup(x => x.GetFaculty(It.IsAny<string>()))
            .Returns(f);

            //act
            var result = fd.GetFaculty(f.Faculty_Id);

            //assert
            Assert.NotNull(result);
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
    }
}
