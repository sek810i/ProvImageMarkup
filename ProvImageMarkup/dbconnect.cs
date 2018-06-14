using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ProvImageMarkup

{
    class Dbconnect
    {
        public string DbPath { get; set; }
        public List<Record> ReadDb(string colName) {
            var csAccess = new OleDbConnectionStringBuilder
            {
                DataSource = DbPath,
                Provider = @"Microsoft.Jet.OleDb.4.0",
            };
            var conAccess = new OleDbConnection(csAccess.ConnectionString);
            var records = new List<Record>();
            try
            { 
                records = conAccess.Query<Record>(@"select id as pid, f1, " + colName + " as FilePath from main where entity like 'Страница%'  order by id").ToList();
            }
            catch (OleDbException ex)
            {
                if (ex.Message == "Отсутствует значение для одного или нескольких требуемых параметров.")
                {
                    MessageBox.Show(@"В таблице нет поля "+ colName, @"Ошибка");
                }
                else { MessageBox.Show(ex.Message, @"Ошибка"); }
            }
            
            return records;
        }
        public List<Fio> ReadFio()
        {
            var csAccess = new OleDbConnectionStringBuilder
            {
                DataSource = DbPath,
                Provider = @"Microsoft.Jet.OleDb.4.0",
            };
            var conAccess = new OleDbConnection(csAccess.ConnectionString);
            var fio = new List<Fio>();
            try
            {
                fio = conAccess.Query<Fio>(@"select id, f2 as Fam,f3 as Name,f4 as Otch from main where entity like 'Человек%' order by id").ToList();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message, @"Ошибка");
            }
            return fio;
        }
    }
}
