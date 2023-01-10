using NUnit.Framework;
using RMM_Server.Domains;
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
    }
}
