using Google.T9Quiz.Lib.Classes.Common;
using Google.T9Quiz.Lib.Classes.Workers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Google.T9Quiz.Test
{
    [TestClass]
    public class MainTest
    {
        private T9V3 _wrapper = new T9V3();

        [TestMethod]
        public void TestWrapingSampleRowsOfDataSet()
        {
            Assert.AreEqual("44 444", _wrapper.GetNumberCodes("hi"));
            Assert.AreEqual("999337777", _wrapper.GetNumberCodes("yes"));
            Assert.AreEqual("333666 6660 022 2777", _wrapper.GetNumberCodes("foo  bar"));
            Assert.AreEqual("4433555 555666096667775553", _wrapper.GetNumberCodes("hello world"));
            Assert.AreEqual("0 0 0", _wrapper.GetNumberCodes("   "));
            Assert.AreEqual("0 02", _wrapper.GetNumberCodes("  a"));
            Assert.AreEqual("2 22299", _wrapper.GetNumberCodes("acx"));     
        }

        [TestMethod]
        public void TestCreatingAndWritingSmallDataSet()
        {
            Utility.WriteOutput("Datasets\\C-small-practice.in", "Output\\C-small-practice.out", _wrapper);
        }

        [TestMethod]
        public void TestCreatingAndWritingBigDataSet()
        {
            Utility.WriteOutput("Datasets\\C-large-practice.in", "Output\\C-large-practice.out", _wrapper);
        }
    }
}
