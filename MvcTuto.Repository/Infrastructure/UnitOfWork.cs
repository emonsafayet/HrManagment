using MvcTuto.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MVCTutorial.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MvcTutorialEntitiesContainer _dbContext;

        public UnitOfWork()
        {
            _dbContext = new MvcTutorialEntitiesContainer();
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
        }
    }

}