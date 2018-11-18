using DesignPattern;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            IAnimal animal = null;
            DynamicProxy dynamicProxy = new DynamicProxy(typeof(Cicada));
           // animal = (Animal)(dynamicProxy.GetTransparentProxy()) ;
            animal.Eating();
            Assert.Pass();
        }
    }
}