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
using RMM_Server.Services;

namespace RMM_Server.Tests
{
    public class ResearchDomainTest
    {
        private IResearchDomain rd;
        private Mock<IResearchRepository> mrr;
        private Mock<IResearchDomain> mrd;
        private Mock<IStudentDomain> msd;
        private Mock<IDepartmentDomain> mdd;
        private Mock<MatchService> mms;

        [SetUp]
        public void SetUp()
        {
            mrr = new Mock<IResearchRepository>();
            msd = new Mock<IStudentDomain>();
            mdd = new Mock<IDepartmentDomain>();
            mrd = new Mock<IResearchDomain>();
            mms = new Mock<MatchService>();
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
        public void TestGetAllResearch()
        {
            //arrange
            List<Research> rl = new List<Research>();
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
            rl.Add(r);

            mrr.Setup(x => x.GetAllResearch())
                .Returns(rl);

            //act
            var result = rd.GetAllResearch();

            //assert
            Assert.NotNull(result);
            mrr.Verify(x => x.GetAllResearch(), Times.Once);
        }

        [Test]
        public void TestGetAllResearchByStudentId()
        {
            //arrange
            List<Research> rl = new List<Research>();
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
            rl.Add(r);

            Student s = new Student()
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

            mrr.Setup(x => x.GetAllResearchByStudentId(It.IsAny<string>()))
                .Returns(rl);

            //act
            var result = rd.GetAllResearchByStudentId(s.Student_Id);

            //assert
            Assert.NotNull(result);
            mrr.Verify(x => x.GetAllResearchByStudentId(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TestGetLastIDFromResearch()
        {
            //arrange
            List<Research> rl = new List<Research>();
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
            rl.Add(r);

            mrr.Setup(x => x.GetLastIDFromResearch())
                .Returns(rl[0].Research_Id);

            //act
            var result = rd.GetLastIDFromResearch();

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result, 18);
            mrr.Verify(x => x.GetLastIDFromResearch(), Times.Once);
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
        public void TestDeleteResearchDeptByResearchID()
        {
            //arrange
            string[] s = { "Computer", "English" };
            Research r = new Research()
            {
                Research_Id = 1,
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

            mrr.Setup(x => x.DeleteResearchDeptByResearchID(It.IsAny<int>()));

            //act
            rd.DeleteResearchDeptByResearchID(r.Research_Id);

            //assert
            mrr.Verify(x => x.DeleteResearchDeptByResearchID(It.IsAny<int>()), Times.Once);
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

        [Test]
        public void TestGetResearchByFacultyId()
        {
            List<Research> rl = new List<Research>();
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
            rl.Add(r);

            mrr.Setup(x => x.GetResearchByFacultyId(It.IsAny<string>()))
                .Returns(rl);

            List<Research> result = rd.GetResearchByFacultyId(r.Faculty_Id);

            Assert.NotNull(result);
            mrr.Verify(x => x.GetResearchByFacultyId(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TestGetFilteredAndSearchedResearch_NoKeyword()
        {
            // unit test for else 
            string[] depts1 = { "Chemical Engineer", "Software Engineer" };
            List<Research> rl = new List<Research>();
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
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts1
            };
            rl.Add(r);

            string[] depts2 = { "Software Engineer" };
            r = new Research()
            {
                Research_Id = 19,
                Faculty_Id = "nii1",
                Name = "test",
                Description = "hello",
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
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                ResearchDepts = depts2
            };
            rl.Add(r);

            List<FilterValue> fv = new List<FilterValue>();
            FilterValue v = new FilterValue()
            {
                CategoryValue = "Department",
                CheckedValue = "Software Engineer"
            };
            fv.Add(v);

            Filter f = new Filter()
            {
                Research = rl,
                FilterValue = fv,
                Keyword = "",
                PsuID = "dxi5017"
            };

            // mocks
            mrr.Setup(x => x.GetAllResearch())
                .Returns(rl);

            List<Research> result = rd.GetFilteredAndSearchedResearch(f);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Research_Id, 18);
            Assert.AreEqual(result.Count, 2);
            mrr.Verify(x => x.GetAllResearch(), Times.Never);
        }

    [Test]
        public void TestGetFilteredAndSearchedResearch_NoKeyword_NoFilterValue()
        {
            // unit test for else if 
            Student s = new Student()
            {
                Student_Id = "dxi5017",
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

            string[] depts1 = { "Chemical Engineer", "Software Engineer", "Nursing" };
            List<Research> rl = new List<Research>();
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
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts1
            };
            rl.Add(r);

            string[] depts2 = { "Software Engineer" };
            r = new Research()
            {
                Research_Id = 19,
                Faculty_Id = "nii1",
                Name = "test",
                Description = "hello",
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
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                ResearchDepts = depts2
            };
            rl.Add(r);

            List<FilterValue> fv = new List<FilterValue>();

            Filter f = new Filter()
            {
                Research = rl,
                FilterValue = fv,
                Keyword = "",
                PsuID = "dxi5017"
            };

            // mocks
            mrr.Setup(x => x.GetAllResearch())
                .Returns(rl);
            msd.Setup(x => x.GetStudent(It.IsAny<string>()))
                .Returns(s);
            mdd.Setup(x => x.GetSubDeptByResearchId(It.IsAny<int>()))
                .Returns(depts1);

            List<Research> result = rd.GetFilteredAndSearchedResearch(f);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Research_Id, 19);
            Assert.AreEqual(result.Count, 2);
            mrr.Verify(x => x.GetAllResearch(), Times.Once);
        }

        [Test]
        public void TestGetFilteredAndSearchedResearch() 
        {
            // unit test for else 
            string[] depts1 = { "Chemical Engineer", "Software Engineer" };
            List<Research> rl = new List<Research>();
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
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts1
            };
            rl.Add(r);

            string[] depts2 = { "Software Engineer" };
            r = new Research()
            {
                Research_Id = 19,
                Faculty_Id = "nii1",
                Name = "test",
                Description = "hello",
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
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                ResearchDepts = depts2
            };
            rl.Add(r);

            List<FilterValue> fv = new List<FilterValue>();
            FilterValue v = new FilterValue()
            {
                CategoryValue = "Department",
                CheckedValue = "Software Engineer"
            };
            fv.Add(v);

            Filter f = new Filter()
            {
                Research = rl,
                FilterValue = fv,
                Keyword = "hello",
                PsuID = "dxi5017"
            };
 
            // mocks
            mrr.Setup(x => x.GetAllResearch())
                .Returns(rl);

            List<Research> result = rd.GetFilteredAndSearchedResearch(f);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Research_Id, 19);
            Assert.AreEqual(result.Count, 1);
            mrr.Verify(x => x.GetAllResearch(), Times.Never);
        }

        [Test]
        public void TestGetSearchedResearchByKeyword()
        {
            //arrange
            string[] depts1 = { "Chemical Engineer", "Software Engineer" };
            string[] s = { "Computer", "English" };
            List<Research> rl = new List<Research>();
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
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts1
            };
            rl.Add(r);

            string[] depts2 = { "Software Engineer" };
            r = new Research()
            {
                Research_Id = 19,
                Faculty_Id = "nii1",
                Name = "test",
                Description = "hello",
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
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                ResearchDepts = depts2
            };
            rl.Add(r);

            mrd.Setup(x => x.GetSearchedResearchByKeyword(It.IsAny<string>(), It.IsAny<List<Research>>()))
                .Returns(rl);

            //act
            var result = rd.GetSearchedResearchByKeyword("hello", rl);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void TestGetFilteredResearch()
        {
            //arrange
            string[] depts1 = { "Chemical Engineer", "Software Engineer" };
            string[] s = { "Computer", "English" };
            List<Research> rl = new List<Research>();
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
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts1
            };
            rl.Add(r);

            string[] depts2 = { "Software Engineer" };
            r = new Research()
            {
                Research_Id = 19,
                Faculty_Id = "nii1",
                Name = "test",
                Description = "hello",
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
                First_Name = "Nassem",
                Last_Name = "Ibrahim",
                ResearchDepts = depts2
            };
            rl.Add(r);

            List<FilterValue> fv = new List<FilterValue>();
            FilterValue v = new FilterValue()
            {
                CategoryValue = "Department",
                CheckedValue = "Software Engineer"
            };
            fv.Add(v);

            Filter f = new Filter()
            {
                Research = rl,
                FilterValue = fv,
                Keyword = "",
                PsuID = "dxi5017"
            };

            mrd.Setup(x => x.GetFilteredResearch(It.IsAny<Filter>()))
                .Returns(rl);

            //act
            var result = rd.GetFilteredResearch(f);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 2);
        }

        [Test]
        public void MatchResearchToStudent_NoMatches()
        {
            string[] depts1 = { "Chemical Engineer", "Software Engineer" };
            Student s = new Student()
            {
                Student_Id = "dxi5017",
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

            List<Research> rl = new List<Research>();
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
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts1
            };
            rl.Add(r);

            msd.Setup(x => x.GetStudent(It.IsAny<string>()))
                .Returns(s);

            mrr.Setup(x => x.GetAllResearch())
                .Returns(rl);

            var result = rd.MatchResearchToStudent(s.Student_Id);

            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 0);
            msd.Verify(x => x.GetStudent(It.IsAny<string>()), Times.Once);
            mrr.Verify(x => x.GetAllResearch(), Times.Once);
        }

        [Test]
        public void MatchResearchToStudent_Matches()
        {
            string[] depts1 = { "Chemical Engineer", "Software Engineer", "Biology" };
            string[] depts2 = { "Chemical Engineer", "Psychology", "Biology" };
            Student s = new Student()
            {
                Student_Id = "dxi5017",
                First_Name = "FirstName",
                Last_Name = "LastName",
                GPA = 2.98,
                Graduation_Month = "May",
                Graduation_Year = "2025",
                Major = "Biology",
                Skills = "Can use Centrifuge; Titration; Research Papers",
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

            List<Research> rl = new List<Research>();

            Research r = new Research()
            {
                Research_Id = 17,
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
                IsPaid = true,
                IsNonpaid = false,
                IsCredit = true,
                First_Name = "Ola",
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts1
            };
            rl.Add(r);
            r = new Research()
            {
                Research_Id = 18,
                Faculty_Id = "oxe2",
                Name = "test",
                Description = "test",
                Location = "test",
                Required_Skills = "Research Papers",
                Encouraged_Skills = "Titration",
                Start_Date = "11/19/2023",
                End_Date = "11/19/2023",
                Active = false,
                Address = "test",
                IsPaid = false,
                IsNonpaid = false,
                IsCredit = false,
                First_Name = "Ola",
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts2
            };
            rl.Add(r);
            r = new Research()
            {
                Research_Id = 19,
                Faculty_Id = "oxe2",
                Name = "test",
                Description = "test",
                Location = "test",
                Required_Skills = "Be good at Centrifuge",
                Encouraged_Skills = "Titration",
                Start_Date = "11/19/2023",
                End_Date = "11/19/2023",
                Active = false,
                Address = "test",
                IsPaid = false,
                IsNonpaid = false,
                IsCredit = false,
                First_Name = "Ola",
                Last_Name = "El-Rashiedy",
                ResearchDepts = depts2
            };
            rl.Add(r);

            msd.Setup(x => x.GetStudent(It.IsAny<string>()))
                .Returns(s);

            mrr.Setup(x => x.GetAllResearch())
                .Returns(rl);

            var result = rd.MatchResearchToStudent(s.Student_Id);

            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 3);
            Assert.Greater(result[0].MatchScore, result[1].MatchScore);
            Assert.Greater(result[1].MatchScore, result[2].MatchScore);
            msd.Verify(x => x.GetStudent(It.IsAny<string>()), Times.Once);
            mrr.Verify(x => x.GetAllResearch(), Times.Once);
        }

        [Test]
        public void ConvertsBoolToInt_True_To_1()
        {
            
            var result = rd.ConvertBoolToInt(true);

            Assert.NotNull(result);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ConvertsBoolToInt_False_To_0()
        {

            var result = rd.ConvertBoolToInt(false);

            Assert.NotNull(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ConvertDateTimeToDate()
        {

            List<Research> rl = new List<Research>();

            Research r = new Research()
            {
                Research_Id = 17,
                Faculty_Id = "oxe2",
                Name = "test",
                Description = "test",
                Location = "test",
                Required_Skills = "test",
                Encouraged_Skills = "test",
                Start_Date = "11/19/2023 00:00:00",
                End_Date = "11/19/2023 00:00:00",
                Active = false,
                Address = "test",
                IsPaid = true,
                IsNonpaid = false,
                IsCredit = true,
                First_Name = "Ola",
                Last_Name = "El-Rashiedy"
            };
            rl.Add(r);

            var result = rd.ConvertDateTimeToDate(rl);

            Assert.NotNull(result);
            Assert.AreEqual(result[0].Start_Date, "11/19/2023");
            Assert.AreEqual(result[0].End_Date, "11/19/2023");
        }


    }
}