//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Text.RegularExpressions;
//using System.Data.SqlClient;
//using Dapper;
//using System.Threading;


//namespace ReadOctopusLog
//{
//    class Program
//    {
//        #region Глобальные переменные
//        public static int count;
//        public static List<LineText> glog;
//        #endregion
//        #region Класс для БД
//        public class LineText
//        {
//            public string dateEvent { get; set; }
//            public string typeEvent { get; set; }
//            public string metodEvent { get; set; }
//            public string batchID { get; set; }
//            public string projectID { get; set; }
//            public string textLog { get; set; }

//        }
//        #endregion
//        #region main
//        static void Main(string[] args)
//        {
//            var log = FuilClass();

//            var tr1 = new Task(countrow);
//            tr1.Start();
//            FuilDB(log);

//        }
//        #endregion
//        #region Кол-во записей записанных в БД
//        public static void countrow()
//        {
//            var sqlcs = new SqlConnectionStringBuilder()
//            {
//                DataSource = "fobos",
//                InitialCatalog = "TestOLAP",
//                IntegratedSecurity = true

//            };
//            var sqlcon = new SqlConnection(sqlcs.ConnectionString);

//            int dbcount = (int)sqlcon.ExecuteScalar("Select count(1) from OctopusLog where left(dateEvent,10) = '2016-12-26'");
//            for (int i = 0; i < glog.Count;)
//            {
//                dbcount = (int)sqlcon.ExecuteScalar("Select count(1) from OctopusLog where left(dateEvent,10) = '2016-12-26'");
//                i = dbcount;
//                Double proc;
//                proc = 100.0 / glog.Count * i;
//                Console.WriteLine("Загружено {0}% строк", proc);
//                Thread.Sleep(5000);

//            }

//        }
//        #endregion
//        #region Заполнение класса их файла
//        public static List<LineText> FuilClass()
//        {


//            string path = @"c:\Octopus22.txt";
//            if (!File.Exists(path))
//            {
//                var log2 = new List<LineText>();
//                return log2;

//            }
//            else
//            {
//                string[] txtfile = File.ReadAllLines(path, Encoding.Unicode);
//                //LineText log = new LineText { "1", "1", "1", "1"};
//                var log = new List<LineText>();
//                Regex patternDate = new Regex(@"^(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3})");
//                Regex patternType = new Regex(@"(?<=\[\d{1,}\] )\w{1,}");
//                Regex patternMetod = new Regex(@"(?<=\[\d{1,}\] \w{1,} )\w{1,}");
//                Regex patternBatch = new Regex(@"(?<=OBatch: )\d{1,}");
//                Regex patternProject = new Regex(@"(?<=\(Prj\: )\d{1,}(?=\))");
//                Regex patternText = new Regex(@"(?<=\[\(null\)\] - ).*");
//                int CountRow = 0;
//                foreach (string s in txtfile)
//                {
//                    LineText elemlog = new LineText();

//                    if (patternDate.IsMatch(s))
//                    {

//                        Match match = patternDate.Match(s);
//                        elemlog.dateEvent = match.Value;

//                    }
//                    if (patternType.IsMatch(s))
//                    {

//                        Match match = patternType.Match(s);
//                        elemlog.typeEvent = match.Value;

//                    }
//                    if (patternMetod.IsMatch(s))
//                    {

//                        Match match = patternMetod.Match(s);
//                        elemlog.metodEvent = match.Value;

//                    }
//                    if (patternBatch.IsMatch(s))
//                    {

//                        Match match = patternBatch.Match(s);
//                        elemlog.batchID = match.Value;

//                    }
//                    if (patternProject.IsMatch(s))
//                    {

//                        Match match = patternProject.Match(s);
//                        elemlog.projectID = match.Value;

//                    }
//                    if (patternText.IsMatch(s))
//                    {

//                        Match match = patternText.Match(s);
//                        elemlog.textLog = match.Value;

//                    }
//                    log.Add(elemlog);
//                    CountRow++;
//                    if (CountRow % 1000 == 0)
//                    { Console.WriteLine("Нашёл {0} строк", CountRow); }

//                }

//                count = log.Count;
//                glog = log;
//                return log;

//            }
//        }
//        #endregion
//        #region Запись в БД
//        public static void FuilDB(List<LineText> log)
//        {
//            var sqlcs = new SqlConnectionStringBuilder()
//            {
//                DataSource = "fobos",
//                InitialCatalog = "TestOLAP",
//                IntegratedSecurity = true

//            };
//            Console.WriteLine("Пишу в базу");
//            var sqlcon = new SqlConnection(sqlcs.ConnectionString);
//            sqlcon.Open();
//            sqlcon.Execute("insert into OctopusLog (dateEvent, typeEvent, metodEvent, batchID, projectID, textLog)" +
//                "values (@dateEvent, @typeEvent, @metodEvent, @batchID, @projectID, @textLog)", log);


//            sqlcon.Close();

//            Console.WriteLine("Я всё сдалал. Нажми любую копку (Кроме питания и ребута)");
//            Console.ReadKey(true);
//        }
//        #endregion
//    }
//}
