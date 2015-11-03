using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static GCDCalculationClass.GCDCalculationClass;

namespace GCDCalculationClass.Tests
{
    [TestFixture]
    public class GCDCalculationClassTests
    {
        
        [Test]
        public void EuclideanAlgorithmUsualTests()
        {
            Assert.AreEqual(11, EuclideanAlgorithm(11,22));
            Assert.AreEqual(11, EuclideanAlgorithm(11, 22, 33));
            Assert.AreEqual(11, EuclideanAlgorithm(11, 22, 33, 44));
            Assert.AreEqual(1144, EuclideanAlgorithm(572010296, 1143980552));
            Assert.AreEqual(1, EuclideanAlgorithm(999999937, 2103980552));
            Assert.AreEqual(0, EuclideanAlgorithm(0));
            Assert.AreEqual(int.MaxValue, EuclideanAlgorithm(int.MaxValue));
            Assert.AreEqual(int.MaxValue, EuclideanAlgorithm(int.MaxValue,  -int.MaxValue, int.MaxValue));
        }


        [ExpectedException(typeof(ArgumentNullException))]
        [TestCase(null)]
        [TestCase(new int[]{})]
        public void EuclideanAlgorithmNullTests(int[] numbers)
        {
            EuclideanAlgorithm(numbers);
        }

        [Test]
        public void BinaryAlgorithmUsualTests()
        {
            Assert.AreEqual(11, BinaryAlgorithm(11, 22));
            Assert.AreEqual(11, BinaryAlgorithm(11, 22, 33));
            Assert.AreEqual(11, BinaryAlgorithm(11, 22, 33, 44));
            Assert.AreEqual(1144, BinaryAlgorithm(572010296, 1143980552));
            Assert.AreEqual(1, BinaryAlgorithm(999999937, 2103980552));
            Assert.AreEqual(0, BinaryAlgorithm(0));
            Assert.AreEqual(int.MaxValue, BinaryAlgorithm(int.MaxValue));
            Assert.AreEqual(int.MaxValue, BinaryAlgorithm(int.MaxValue, -int.MaxValue, int.MaxValue));
        }


        [ExpectedException(typeof(ArgumentNullException))]
        [TestCase(null)]
        [TestCase(new int[] { })]
        public void BinaryAlgorithmNullTests(int[] numbers)
        {
            BinaryAlgorithm(numbers);
        }
    }
}
