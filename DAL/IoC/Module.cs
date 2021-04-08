using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IoC
{
    public class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();

            dic.Add(typeof(Domain.IRepositories.IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            dic.Add(typeof(Domain.IRepositories.IUnitOfWorkTransaction), typeof(UnitOfWork.UnitOfWorkTransaction));

            return dic;
        }
    }
}
