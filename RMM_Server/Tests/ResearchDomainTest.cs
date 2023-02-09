﻿using Newtonsoft.Json;
using NUnit.Framework;
using RMM_Server.Domains;
using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Tests
{
    public class ResearchDomainTest
    {
        /* private ResearchDomain rd;

         [SetUp]
         public void SetUp()
         {
             rd = new ResearchDomain();
         }

         [Test]
         public void TestUpdateAppProgress()
         {
             Participant p = new Participant() { 
                 progress = 2,
                 research_id = 999999,
                 student_id = "testid123"
             };

             rd.UpdateApplicantProgress(p.progress, p.research_id, p.student_id);
             var result = rd.GetAppProgression(p.research_id, p.student_id);
             Assert.AreEqual(result.progress, p.progress);

             p.progress = 3;
             rd.UpdateApplicantProgress(p.progress, p.research_id, p.student_id);
             result = rd.GetAppProgression(p.research_id, p.student_id);
             Assert.AreEqual(result.progress, p.progress);
         }

         [Test]
         public void TestCreateResearchCreatesResearch()
         {
             //arrange
             string[] s = { "Computer", "English" };
             Research r = new Research()
             {
                 Id = 999998,
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

             //act
             var result = rd.AddResearch(r);

             //assert
             Assert.NotNull(result);
             rd.DeleteResearchByID(999998);
         }

         [Test]
         public void TestAddResearchApplicant()
         {
             //arrange
             Participant p = new Participant()
             {
                 progress = 0,
                 research_id = 999998,
                 student_id = "dxi1017"
             };

             //act
             var result = rd.AddResearchApplicant(p);

             //assert
             Assert.NotNull(result);
             rd.DeleteResearchApplicant(999998, "dxi1017");
         }

         [Test]
         public void TestGetResearchByID()
         {
             Research r = new Research()
             {
                 Id = 18,
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
                 Faculty_FirstName = "Ola",
                 Faculty_LastName = "El-Rashiedy"
             };

             //act
             Research result = rd.GetResearchByID(18);
             var object1Json = JsonConvert.SerializeObject(r);
             var object2Json = JsonConvert.SerializeObject(result);

             //assert
             Assert.AreEqual(object1Json, object2Json);
         }*/
    }
}
