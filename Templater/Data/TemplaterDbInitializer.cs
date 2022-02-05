using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Teplater.SQLite.Context;
using Templater.Infrastructure.Methods;
using Templator.DTO.DTOModels;
using Templater.Infrastructure.Mapping;

namespace Templater.Data
{
    public class TemplaterDbInitializer
    {
        private TeplaterSQLDB _db;
        private const string dirPathDocs = "Docs";
        private const string dirPathTemp = "Templates";

        public TemplaterDbInitializer(TeplaterSQLDB db)
        {
            _db = db;
        }

        public void Initialize()
        {
            var db = _db.Database;

            if (db.GetPendingMigrations().Any())
            {
                db.Migrate();
            }

            InitializeFolder();

            InitializeTemplates();
        }

        private void InitializeFolder()
        {
            if (!Directory.Exists(dirPathDocs))
                Directory.CreateDirectory(dirPathDocs);
        }

        private void InitializeTemplates()
        {
            if (_db.Templates.Any())
            {
                return;
            }

            var files = Directory.GetFiles(dirPathTemp);

            var fileNames = files.Select(el => 
            {
                var items = el.Split("\\");

                return items[items.Length - 1];
            }).ToArray();

            var filesText = new List<string>();

            var marks = new List<ICollection<string>>();

            foreach (var file in files)
            {
                var text = WordMethods.GetTextFromWord(file);

                filesText.Add(text);

                marks.Add(WordMethods.SearchFromTo(text, '<', '>'));
            }

            var templates = new List<Template>();

            for (int i = 0; i < files.Length; i++)
            {
                var template = new Template
                {
                    FileName = fileNames[i],
                    JSONKeys = marks[i].ToJSONKeys()
                };

                templates.Add(template);
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Templates.AddRange(templates);

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

        }


    }
}
