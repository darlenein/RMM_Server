using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ProfileManager.Domains;
using ProfileManager.Services;

namespace ProfileManager.Tests
{
    // mark class as a test
    [TestFixture] 
    public class StudentDomainTest
    {
        private StudentDomain sd;

        // identify method to be called before running each test 
        [SetUp] 
        public void SetUp()
        {
            sd = new StudentDomain();
        }

        // mark as a test
        [Test] 
        public void TestReturnOneReturnsOne()
        {
            int result = sd.ReturnOne();

            // params: expected, actual 
            Assert.AreEqual(1, result); 
        }
    }
}
