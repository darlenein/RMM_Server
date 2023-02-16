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
    }
}
