using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templater.Infrastructure.Interfaces;
using Templator.DTO.DTOModels;
using Templator.DTO.Models;
using Teplater.SQLite.Context;

namespace Templater.Infrastructure.Services.InDB
{
    public class SolutionDBStore : IStore<Solution>
    {
        private TeplaterSQLDB _db;

        public SolutionDBStore(TeplaterSQLDB db)
        {
            _db = db;
        }
        public Solution Add(Solution item)
        {
            if (_db.Solutions.Contains(item))
                return item;

            using (_db.Database.BeginTransaction())
            {
                _db.Solutions.Add(item);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }


            return item;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);

            if (item is null || (!_db.Solutions.Contains(item)))
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Remove(item);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public ICollection<Solution> GetAll() =>
            _db.Solutions.ToList();

        public Solution GetById(int id) =>
            GetAll().SingleOrDefault(el => el.Id == id);

        public bool Update(Solution item)
        {
            if (!_db.Solutions.Contains(item))
                return false;

            using (_db.Database.BeginTransaction())
            {
                var sol = GetById(item.Id);

                sol.templateFileName = item.templateFileName;

                sol.Data = item.Data;

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool Update(int id, Status status)
        {
            throw new NotImplementedException();
        }
    }

}
