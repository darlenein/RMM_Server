using NUnit.Framework;
using ProfileManager.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileManager.Tests
{
    public class FacultyDomainTest
    {
        private FacultyDomain fd;

        [SetUp]
        public void SetUp()
        {
            fd = new FacultyDomain();
        }
    }
}
