using NUnit.Framework;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Tests
{
    public class MatchServiceTest
    {
        private MatchService ms;
        [SetUp]
        public void SetUp()
        {
            ms = new MatchService();
        }
        [Test]
        public void Test_RemoveStopWords()
        {
            var result = ms.RemoveStopWords("I am a doggy");
            Assert.NotNull(result);
            Assert.AreEqual("I;doggy", result);

        }

        [Test]
        public void Test_RemoveStopWords_None()
        {
            var result = ms.RemoveStopWords("none");
            Assert.NotNull(result);
            Assert.AreEqual("none", result);

        }

        [Test]
        public void Test_NormalizeText()
        {
            var result = ms.NormalizeText("I HaVe 32 Friends!!");
            Assert.NotNull(result);
            Assert.AreEqual("i have  friends", result);

        }

        [Test]
        public void Test_TokenizeString()
        {
            var result = ms.TokenizeString("Computer ScIenCe MAJOR and jAVa exPert!!");
            Assert.NotNull(result);
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(result[0], "computer");
            Assert.AreEqual(result[1], "science");
            Assert.AreEqual(result[2], "major");
            Assert.AreEqual(result[3], "java");
            Assert.AreEqual(result[4], "expert");
        }
    }
}
