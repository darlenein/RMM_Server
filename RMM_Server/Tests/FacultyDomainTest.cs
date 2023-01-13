using NUnit.Framework;
using RMM_Server.Domains;
using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Tests
{
    public class FacultyDomainTest
    {
        private FacultyDomain fd;

        [SetUp]
        public void SetUp()
        {
            fd = new FacultyDomain();
        }

        [Test]
        public void TestCreateFacultyCreatesFaculty()
        {
            //arrange
            Faculty f = new Faculty()
            {
                Id = "testID",
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = "Email",
                Office = "Office",
                Phone = "Phone",
                Link1 = null,
                Link2 = null,
                Link3 = null,
                AboutMe = "About Me",
                ResearchInterest = "Research Interests",
                ProfileUrl = "pfp"
            };

            //act
            var result = fd.CreateFaculty(f);

            //assert
            Assert.NotNull(result);
            fd.DeleteFacultyByID("testID");
        }
    }
}
