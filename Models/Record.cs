using BakalarPrace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakalarPrace.Models
{
    public class Record
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public User Author { get; set; }
        public DateTime Uploaded { get; set; }
        public string Location { get; set; }
        public List<CsvRow> CSV { get; set; }

        public Record(int id, string name, int aId, DateTime uploaded, string loc)
        {
            ID = id;
            Name = name;
            Uploaded = uploaded;
            Location = loc;

            Author = this.GetAuthorByID(aId);
        }

        public Record(int id, string name, int aId, DateTime uploaded, string loc, List<CsvRow> cs)
        {
            ID = id;
            Name = name;
            Uploaded = uploaded;
            Location = loc;
            CSV = cs;

            Author = this.GetAuthorByID(aId);
        }

        public Record()
        {
                
        }

        public User GetAuthorByID(int ID)
        {
            Database db = new Database();
            return db.GetUserByID(ID);
        }
    }
}
