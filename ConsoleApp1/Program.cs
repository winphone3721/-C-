using DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //test();

            Console.WriteLine("");
            Console.WriteLine("CustomProxy Sample");
            Console.WriteLine("================");
            Console.WriteLine("");
            // Create an instance of MyProxy.
            DynamicProxy myProxyInstance = new DynamicProxy(typeof(Cicada));
            // Get a CustomServer proxy.
            IAnimal myHelloServer = (Cicada)myProxyInstance.GetTransparentProxy();
            // Get stubdata.
            Console.WriteLine("GetStubData = " + RealProxy.GetStubData(myProxyInstance).ToString());
            // Get ProxyType.
            Console.WriteLine("Type of object represented by RealProxy is :"
                              + myProxyInstance.GetProxiedType());
            //myHelloServer.HelloMethod("RealProxy Sample");
            myHelloServer.Eating();
            //Console.WriteLine("");
            // Get a reference object from server.
            //Console.WriteLine("Create an objRef object to be marshalled across Application Domains...");
            //ObjRef CustomObjRef = myProxyInstance.CreateObjRef(typeof(CustomServer));
           //Console.WriteLine("URI of 'ObjRef' object =  " + CustomObjRef.URI);
        }

        // Acts as a custom proxy user.
        [PermissionSet(SecurityAction.LinkDemand)]
        public static void test()
        {
            IAnimal animal = null;
            DynamicProxy dynamicProxy = new DynamicProxy(typeof(Cicada));
            animal = (IAnimal)(dynamicProxy.GetTransparentProxy());
            // Get stubdata.
            Console.WriteLine("GetStubData = " + RealProxy.GetStubData(dynamicProxy).ToString());

            animal.Eating();
            //Assert.Pass();
        }
    }
}
