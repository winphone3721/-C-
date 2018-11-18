using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace DesignPattern
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [MyProxyAttribute]
    public abstract class AnimalBase : ContextBoundObject, IAnimal
    {
        public abstract void Eating();
        //{
            //throw new NotImplementedException();
       //}
    }
}
