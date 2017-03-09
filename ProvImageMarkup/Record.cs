using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ProvImageMarkup
{
    public class Record
    {
        public int pid { get; set; }
        public String f1 { get; set; }
        public List<person> persons { get; set; }
        public string FilePath { get; set; }

        public static List<Record> addFIO (List<Fio> FIOList,List<Record> REC)
        {
            foreach (var records in REC)
            {
                if (records.persons.Count != 0) {
                    foreach (var pers in records.persons)
                    {
                        foreach (var f in FIOList)
                        {
                            if (pers.id == f.id)
                            {
                                pers.Fam = f.Fam;
                                pers.Name = f.Name;
                                pers.Otch = f.Otch;
                            }
                        }
                    }
                }
            }

            return REC;
        }

        public static List<Record> fullPersons(List<Record> Records, Boolean check)
        {
            foreach (Record rec in Records)
            {
                var p = new List<person>();
                var Xdoc = new XmlDocument();
                Xdoc.LoadXml(rec.f1.Trim());
                string s;
                if (check == true)
                {
                    // этот реплейс сделан на случай, если в FilePath указан не правильный образ
                    s = rec.FilePath.Replace(rec.FilePath.Substring(rec.FilePath.Length - 12, 12), Xdoc.SelectSingleNode(@"//i").Attributes["src"].Value.Substring(Xdoc.SelectSingleNode(@"//i").Attributes["src"].Value.Length - 12, 12));
                }
                else
                {
                    s = Xdoc.SelectSingleNode(@"//i").Attributes["src"].Value;
                }
                foreach (XmlElement nodes in Xdoc.SelectNodes("//s"))
                {
                    var pers = new person();
                    pers.id = Convert.ToInt32(nodes.Attributes["id"].Value);
                    pers.FilePath = s;
                    var dfnas = nodes.Attributes["c"].Value.Split(',');
                    pers.x = Convert.ToInt32(dfnas[0]);
                    pers.y = Convert.ToInt32(dfnas[1]);
                    pers.w = Convert.ToInt32(dfnas[2]);
                    pers.h = Convert.ToInt32(dfnas[3]);
                    p.Add(pers);
                }
                if (p.Count == 0)
                {
                    var pers = new person();
                    pers.FilePath = s;
                    p.Add(pers);
                }
                rec.persons = p;
            }
            return Records;
        }

    }
    public class person
    {
        public int id { get; set; }
        public String FilePath { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public String Fam { get; set; }
        public String Name { get; set; }
        public String Otch { get; set; }
    }
    public class Fio
    {
        public int id { get; set; }
        public String Fam { get; set; }
        public String Name { get; set; }
        public String Otch { get; set; }
    }
}
