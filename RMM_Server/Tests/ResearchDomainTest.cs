using Newtonsoft.Json;
using NUnit.Framework;
using RMM_Server.Contracts;
using RMM_Server.Domains;
using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;

namespace RMM_Server.Tests
{
    public class ResearchDomainTest
    {
        private IResearchDomain rd;
        private Mock<IResearchRepository> mrr = new Mock<IResearchRepository>();
        private Mock<IStudentDomain> msd = new Mock<IStudentDomain>();
        private Mock<IDepartmentDomain> mdd = new Mock<IDepartmentDomain>();

        [SetUp]
        public void SetUp()
        {
            rd = new ResearchDomain(mrr.Object, msd.Object, mdd.Object);
        }

        [Test]
        public void TestUpdateAppProgress()
        {
            Participant p = new Participant()
            {
                Progress_Bar = 2,
                Research_id = 999999,
                Student_id = "testid123"
            };

            mrr.Setup(x => x.UpdateApplicantProgress(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()));
            mrr.Setup(x => x.GetAppProgression(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(p);


            var result = rd.GetAppProgression(p.Research_id, p.Student_id);
            Assert.AreEqual(result.Progress_Bar, p.Progress_Bar);

            p.Progress_Bar = 3;
            rd.UpdateApplicantProgress(p.Progress_Bar, p.Research_id, p.Student_id);

            result = rd.GetAppProgression(p.Research_id, p.Student_id);
            Assert.AreEqual(result.Progress_Bar, p.Progress_Bar);
            mrr.Verify(x => x.UpdateApplicantProgress(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            mrr.Verify(x => x.GetAppProgression(It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void TestCreateResearchCreatesResearch()
        {
            //arrange
            string[] s = { "Computer", "English" };
            Research r = new Research()
            {
                Research_Id= 999998,
                Faculty_Id = "dxi1017",
                Name = "research",
                Description = "description",
                Location = "location",
                Required_Skills = "required",
                Encouraged_Skills = "encouraged",
                Start_Date = "2022-11-19",
                End_Date = "2023-11-19",
                Active = true,
                Address = "123 fake st",
                IsPaid = true,
                IsNonpaid = false,
                IsCredit = true,
                ResearchDepts = s
            };

            mrr.Setup(x => x.AddResearch(It.IsAny<Research>()))
                .Returns(r);

            //act
            var result = rd.AddResearch(r);

            //assert
            Assert.NotNull(result);
            mrr.Verify(x => x.AddResearch(It.IsAny<Research>()), Times.Once);
        }

        [Test]
        public void TestAddResearchApplicant()
        {
            //arrange
            Participant p = new Participant()
            {
                Progress_Bar = 0,
                Research_id = 999998,
                Student_id = "dxi1017"
            };

            mrr.Setup(x => x.AddResearchApplicant(It.IsAny<Participant>()))
                .Returns(p);

            //act
            var result = rd.AddResearchApplicant(p);

            //assert
            Assert.NotNull(result);
            mrr.Verify(x => x.AddResearchApplicant(It.IsAny<Participant>()), Times.Once);
        }

        [Test]
        public void TestGetResearchByID()
        {
            Research r = new Research()
            {
                Research_Id = 18,
                Faculty_Id = "oxe2",
                Name = "test",
                Description = "test",
                Location = "test",
                Required_Skills = "test",
                Encouraged_Skills = "test",
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

            mrr.Setup(x => x.GetResearchByID(It.IsAny<int>()))
                .Returns(r);

            //act
            Research result = rd.GetResearchByID(r.Research_Id);

            //assert
            Assert.NotNull(result);
            mrr.Verify(x => x.GetResearchByID(It.IsAny<int>()), Times.Once);
        }
    }
}
