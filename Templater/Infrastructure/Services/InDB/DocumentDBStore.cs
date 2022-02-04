using System;
using System.Collections.Generic;
using System.Linq;
using Templater.Infrastructure.Interfaces;
using Templator.DTO.DTOModels;
using Teplater.SQLite.Context;

namespace Templater.Infrastructure.Services.InDB
{
    public class DocumentDBStore : IStore<Document>
    {
        private TeplaterSQLDB _db;

        public DocumentDBStore(TeplaterSQLDB db)
        {
            _db = db;
        }
        public Document Add(Document item)
        {
            if (_db.Documents.Contains(item))
                return item;

            using (_db.Database.BeginTransaction())
            {
                _db.Documents.Add(item);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }


            return item;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);

            if (item is null || (!_db.Documents.Contains(item)))
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Remove(item);

                _db.SaveChanges();
                
                _db.Database.CommitTransaction();
            }

            return true;
        }

        public ICollection<Document> GetAll() =>
            _db.Documents.ToList();

        public Document GetById(int id) =>
            GetAll().SingleOrDefault(el => el.Id == id);

        public bool Update(Document item)
        {
            if (!_db.Documents.Contains(item))
                return false;

            using (_db.Database.BeginTransaction())
            {
                var doc = GetById(item.Id);

                doc.FileName = item.FileName;

                doc.Status = item.Status;

                doc.Template = item.Template;

                doc.JSONValues = item.JSONValues;

                doc.DateTimeInitial = item.DateTimeInitial;

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }


    }
}
