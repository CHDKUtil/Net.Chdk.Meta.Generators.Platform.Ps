using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Chdk.Meta.Generators.Platform.Ps.Tests
{
    [TestClass]
    public class PsPlatformGeneratorTest
    {
        private IPlatformGenerator platformGenerator;

        public PsPlatformGeneratorTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddPlatformGenerator()
                .AddPsPlatformGenerator()
                .BuildServiceProvider();
            platformGenerator = serviceProvider.GetService<IPlatformGenerator>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullModels()
        {
            var result = platformGenerator.GetPlatform(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyModels()
        {
            var result = platformGenerator.GetPlatform(new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNullModel()
        {
            var result = platformGenerator.GetPlatform(new string[] { null });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyModel()
        {
            var result = platformGenerator.GetPlatform(new[] { string.Empty });
        }

        [TestMethod]
        public void TestUnsupported()
        {
            Test(null, "IXY 32S");
        }

        [TestMethod]
        public void TestSinglePowershot()
        {
            Test("s100", "PowerShot S100");
        }

        [TestMethod]
        public void TestPowershotS1IS()
        {
            Test("s1is", "PowerShot S1 IS");
        }

        [TestMethod]
        public void TestPowershotSX1IS()
        {
            Test("sx1", "PowerShot SX1 IS");
        }

        [TestMethod]
        public void TestPowershotSX10IS()
        {
            Test("sx10", "PowerShot SX10 IS");
        }

        [TestMethod]
        public void TestPowershotSX20IS()
        {
            Test("sx20", "PowerShot SX20 IS");
        }

        [TestMethod]
        public void TestPowershotSX100IS()
        {
            Test("sx100is", "PowerShot SX100 IS");
        }

        [TestMethod]
        public void TestPowershotSX110IS()
        {
            Test("sx110is", "PowerShot SX110 IS");
        }

        [TestMethod]
        public void TestPowershotSX200IS()
        {
            Test("sx200is", "PowerShot SX200 IS");
        }

        [TestMethod]
        public void TestPowershotSXHS()
        {
            Test("sx100hs", "PowerShot SX100 HS");
        }

        [TestMethod]
        public void TestPowershotG7X()
        {
            Test("g7x", "PowerShot G7 X");
        }

        [TestMethod]
        public void TestPowershotG7XMarkII()
        {
            Test("g7x2", "PowerShot G7 X Mark II");
        }

        [TestMethod]
        public void TestElphIS()
        {
            Test("ixus140_elph130", "PowerShot ELPH 130 IS", "IXUS 140");
        }

        [TestMethod]
        public void TestElphHS()
        {
            Test("ixus115_elph100hs", "PowerShot ELPH 100 HS", "IXUS 115 HS");
        }

        [TestMethod]
        public void TestBareIxus()
        {
            Test("ixus_s100", "PowerShot S100", "Digital IXUS");
        }

        [TestMethod]
        public void TestPowershotIxus()
        {
            Test("ixus40_sd300", "PowerShot SD300", "Digital IXUS 40");
        }

        [TestMethod]
        public void TestIxusWireless()
        {
            Test("ixusw_sd430", "PowerShot SD430", "Digital IXUS Wireless");
        }

        [TestMethod]
        public void TestHypotheticalIxusWireless2()
        {
            Test("ixusw2_sd440", "PowerShot SD440", "Digital IXUS Wireless 2");
        }

        [TestMethod]
        public void TestIxusZoom()
        {
            Test("ixusizoom_sd30", "PowerShot SD30", "Digital IXUS i Zoom");
        }

        [TestMethod]
        public void TestHypotheticalIxusZoom2()
        {
            Test("ixusizoom2_sd40", "PowerShot SD40", "Digital IXUS i Zoom 2");
        }

        [TestMethod]
        public void TestIxusIS()
        {
            Test("ixus800_sd700",
                "PowerShot SD700 IS",
                "Digital IXUS 800 IS");
        }

        [TestMethod]
        public void TestIxusTi()
        {
            Test("ixus900_sd900", "PowerShot SD900", "Digital IXUS 900 Ti");
        }

        [TestMethod]
        public void TestIxusFirst()
        {
            Test("ixus245", "IXUS 245 HS", "IXY 430F");
        }

        [TestMethod]
        public void TestEOSM()
        {
            Test("m3", "EOS M3");
        }

        private void Test(string expected, params string[] args)
        {
            var actual = platformGenerator.GetPlatform(args);
            Assert.AreEqual(expected, actual);
        }
    }
}
