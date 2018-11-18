using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace DesignPattern
{
    //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    //[MyProxyAttribute]
    public interface IAnimal
    {
        void Eating();
    }
}
