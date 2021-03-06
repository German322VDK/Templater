using System;
using System.Collections.Generic;
using System.Linq;
using Templater.Infrastructure.Interfaces;
using Templator.DTO.DTOModels;
using Templator.DTO.Models;
using Teplater.SQLite.Context;

namespace Templater.Infrastructure.Services.InDB
{
    class TemplateDBStore : IStore<Template>
    {
        private TeplaterSQLDB _db;

        public TemplateDBStore(TeplaterSQLDB db)
        {
            _db = db;
        }


        public Template Add(Template item)
        {
            if (_db.Templates.FirstOrDefault(el => el.FileName == item.FileName) is not null)
                return item;

            using (_db.Database.BeginTransaction())
            {
                _db.Templates.Add(item);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }


            return item;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);

            if (item is null || (!_db.Templates.Contains(item)))
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Remove(item);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public ICollection<Template> GetAll() =>
            _db.Templates.ToList();

        public Template GetById(int id) =>
            GetAll().SingleOrDefault(el => el.Id == id);

        public bool Update(Template item)
        {
            if (!_db.Templates.Contains(item))
                return false;

            using (_db.Database.BeginTransaction())
            {
                var temp = GetById(item.Id);

                temp.FileName = item.FileName;
                temp.JSONKeys = item.JSONKeys;

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
