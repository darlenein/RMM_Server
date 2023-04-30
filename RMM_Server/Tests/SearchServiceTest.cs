using NUnit.Framework;
using RMM_Server.Models;
using RMM_Server.Services;
using System.Collections.Generic;

namespace RMM_Server.Tests
{
    public class SearchServiceTest
    {
        private SearchService ss;
        private StudentSearchService sss;
        private FacultySearchService fss;
        [SetUp]
        public void SetUp()
        {
            ss = new SearchService();
            sss = new StudentSearchService();
            fss = new FacultySearchService();
        }

        [Test]
        public void Test_ResearchSearch_Software()
        {
            var mockResearchList = new List<Research>()
            {
                new Research()
                {
                   First_Name = "bob",
                   Last_Name = "turtle",
                   Description = "Software Expert",
                   Name = "Computer Software IOT",
                   Encouraged_Skills = "Computer Science and Java",
                   Required_Skills = "Computer Skills"
                },
                new Research()
                {
                   First_Name = "henrietta",
                   Last_Name = "artsy",
                   Description = "Art and painting",
                   Name = "How to paint a tree",
                   Encouraged_Skills = "dancing",
                   Required_Skills = "painting"
                }
            };
            var result = ss.Search("Software", mockResearchList);
            Assert.IsNotNull(result);
            Assert.Greater(result[0].SearchScore, result[1].SearchScore);
        }

        [Test]
        public void Test_ResearchSearch_Art()
        {
            var mockResearchList = new List<Research>()
            {
                new Research()
                {
                   First_Name = "bob",
                   Last_Name = "turtle",
                   Description = "Software Expert",
                   Name = "Computer Software IOT",
                   Encouraged_Skills = "Computer Science and Java",
                   Required_Skills = "Computer Skills"
                },
                new Research()
                {
                   First_Name = "henrietta",
                   Last_Name = "artsy",
                   Description = "Art and painting",
                   Name = "How to paint a tree",
                   Encouraged_Skills = "dancing",
                   Required_Skills = "painting"
                }
            };
            var result = ss.Search("Art", mockResearchList);
            Assert.IsNotNull(result);
            Assert.Greater(result[1].SearchScore, result[0].SearchScore);
        }

        [Test]
        public void Test_StudentSearch_Software()
        {
            var mockStudentList = new List<Student>()
            {
                new Student()
                {
                   First_Name = "timmy",
                   Last_Name = "turner",
                   Major = "Software Engineering",
                   PreferLocation = "Online",
                   Skills = "Java; Computer Science; Algorithms; Computers"
                },
                new Student()
                {
                   First_Name = "cosmo",
                   Last_Name = "fairy",
                   Major = "Theater",
                   PreferLocation = "On-Campus",
                   Skills = "Dancing; Magic; Flying; Crying; Art; Music"
                }
            };
            var result = sss.Search("Art", mockStudentList);
            Assert.IsNotNull(result);
            Assert.Greater(result[1].SearchScore, result[0].SearchScore);
        }

        [Test]
        public void Test_StudentSearch_Art()
        {
            var mockStudentList = new List<Student>()
            {
                new Student()
                {
                   First_Name = "timmy",
                   Last_Name = "turner",
                   Major = "Software Engineering",
                   PreferLocation = "Online",
                   Skills = "Java; Computer Science; Algorithms; Computers"
                },
                new Student()
                {
                   First_Name = "cosmo",
                   Last_Name = "fairy",
                   Major = "Theater",
                   PreferLocation = "On-Campus",
                   Skills = "Dancing; Magic; Flying; Crying; Art; Music"
                }
            };
            var result = sss.Search("Art", mockStudentList);
            Assert.IsNotNull(result);
            Assert.Greater(result[1].SearchScore, result[0].SearchScore);
        }

        [Test]
        public void Test_FacultySearch_Evil()
        {
            var mockFacultyList = new List<Faculty>()
            {
                new Faculty()
                {
                   First_Name = "timmy",
                   Last_Name = "turner",
                   Title = "Evil Mastermind Software Engineer",
                   About_Me = "Plots evil plans for world domination",
                   Research_Interest = "Java; Computer Science; Algorithms; Computers"
                },
                new Faculty()
                {
                   First_Name = "cosmo",
                   Last_Name = "fairy",
                   Title = "Master Fairy and Artist",
                   About_Me = "On-Campus",
                   Research_Interest = "Dancing; Magic; Flying; Crying; Art; Music"
                }
            };
            var result = fss.Search("Evil", mockFacultyList);
            Assert.IsNotNull(result);
            Assert.Greater(result[0].SearchScore, result[1].SearchScore);
        }

        [Test]
        public void Test_FacultySearch_Fairy()
        {
            var mockFacultyList = new List<Faculty>()
            {
                new Faculty()
                {
                   First_Name = "timmy",
                   Last_Name = "turner",
                   Title = "Evil Mastermind Software Engineer",
                   About_Me = "Plots evil plans for world domination",
                   Research_Interest = "Java; Computer Science; Algorithms; Computers"
                },
                new Faculty()
                {
                   First_Name = "cosmo",
                   Last_Name = "fairy",
                   Title = "Master Fairy and Artist",
                   About_Me = "On-Campus",
                   Research_Interest = "Dancing; Magic; Flying; Crying; Art; Music"
                }
            };
            var result = fss.Search("Fairy", mockFacultyList);
            Assert.IsNotNull(result);
            Assert.Greater(result[1].SearchScore, result[0].SearchScore);
        }
    }
}
