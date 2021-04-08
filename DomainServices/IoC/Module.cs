using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.IoC
{
    public class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();

            dic.Add(typeof(Interfaces.IScrapperAgenda), typeof(Services.ScrapperAgenda));

            return dic;
        }
    }
}
