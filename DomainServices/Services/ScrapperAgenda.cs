using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    internal class ScrapperAgenda : Interfaces.IScrapperAgenda
    {
        private readonly IUnitOfWork uow;

        public ScrapperAgenda(IUnitOfWork _uow)
        {
            uow = _uow;

        }
    }
}
