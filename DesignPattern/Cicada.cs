using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace DesignPattern
{

   
    public class Cicada : AnimalBase
    {
        public override void Eating()
        {
            System.Console.WriteLine("CHANEL 饿了");
        }
    }
}
