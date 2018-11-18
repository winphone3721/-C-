using Castle.DynamicProxy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace DesignPattern
{
    //[AttributeUsage(AttributeTargets.Class)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class DynamicProxy : RealProxy
    {
        String myUri;
        MarshalByRefObject myMarshalByRefObject;
        public DynamicProxy() : base()
        {
            Console.WriteLine("MyProxy Constructor Called...");
            myMarshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(typeof(CustomServer));
            ObjRef myObjRef = RemotingServices.Marshal(myMarshalByRefObject);
            myUri = myObjRef.URI;
        }
        public DynamicProxy(Type type1) : base(type1)
        {
            Console.WriteLine("MyProxy Constructor Called...");
            myMarshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(type1);
            ObjRef myObjRef = RemotingServices.Marshal(myMarshalByRefObject);
            myUri = myObjRef.URI;
        }
        public DynamicProxy(Type type1, MarshalByRefObject targetObject) : base(type1)
        {
            Console.WriteLine("MyProxy Constructor Called...");
            myMarshalByRefObject = targetObject;
            ObjRef myObjRef = RemotingServices.Marshal(myMarshalByRefObject);
            myUri = myObjRef.URI;
        }
        public override IMessage Invoke(IMessage myMessage)
        {
            Console.WriteLine("MyProxy 'Invoke method' Called...");
            if (myMessage is IMethodCallMessage)
            {
                if (myMessage.Properties["__MethodName"].Equals("Eating"))
                {
                    Console.WriteLine("我要执行吃了1");
                }
                //if(myMessage.Properties)
                Console.WriteLine("IMethodCallMessage");
            }
            if (myMessage is IMethodReturnMessage)
            {
                Console.WriteLine("IMethodReturnMessage");
            }
            if (myMessage is IConstructionCallMessage)
            {
                // Initialize a new instance of remote object
                IConstructionReturnMessage myIConstructionReturnMessage =
                   this.InitializeServerObject((IConstructionCallMessage)myMessage);
                ConstructionResponse constructionResponse = new
                   ConstructionResponse(null, (IMethodCallMessage)myMessage);
                return constructionResponse;
            }
            IDictionary myIDictionary = myMessage.Properties;
            IMessage returnMessage;
            myIDictionary["__Uri"] = myUri;

            // Synchronously dispatch messages to server.
            returnMessage = ChannelServices.SyncDispatchMessage(myMessage);
            // Pushing return value and OUT parameters back onto stack.
            IMethodReturnMessage myMethodReturnMessage = (IMethodReturnMessage)returnMessage;
            return returnMessage;
        }
        public override ObjRef CreateObjRef(Type ServerType)
        {
            Console.WriteLine("CreateObjRef Method Called ...");
            CustomObjRef myObjRef = new CustomObjRef(myMarshalByRefObject, ServerType);
            myObjRef.URI = myUri;
            return myObjRef;
        }
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override void GetObjectData(SerializationInfo info,
                                            StreamingContext context)
        {
            // Add your custom data if any here.
            base.GetObjectData(info, context);
        }
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public class CustomObjRef : ObjRef
        {
            public CustomObjRef(MarshalByRefObject myMarshalByRefObject, Type serverType) :
                              base(myMarshalByRefObject, serverType)
            {
                Console.WriteLine("ObjRef 'Constructor' called");
            }
            // Override this method to store custom data.
            [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
            public override void GetObjectData(SerializationInfo info,
                                               StreamingContext context)
            {
                base.GetObjectData(info, context);
            }
        }
    }
    public class ProxySample
    {
        // Acts as a custom proxy user.
        [PermissionSet(SecurityAction.LinkDemand)]
        public static void Main()
        {
            IAnimal animal = null;
            DynamicProxy dynamicProxy = new DynamicProxy(typeof(Cicada));
            animal = (IAnimal)(dynamicProxy.GetTransparentProxy());
            animal.Eating();
            //Assert.Pass();
        }
    }
}
