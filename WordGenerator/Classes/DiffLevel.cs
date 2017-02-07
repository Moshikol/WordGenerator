using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGenerator.DataSets.WordsTableAdapters;

namespace WordGenerator
{
    public class DiffLevel
    {
        public int ID { get; set; }

        public string Difflevel { get; set; }

        public DiffLevel(int id, string difflevel)
        {
            ID = id;
            Difflevel = difflevel;

        }

        public static List<DiffLevel> GetLstFromDb()
        {
            List<DiffLevel> Reslst = new List<DiffLevel>();

            DADiffLevel daDiff = new DADiffLevel();

            DataTable dt;
            dt = daDiff.GetDiffLevelData();
            foreach (DataRow dr in dt.Rows)
            {
                int tempid = 0;
                int.TryParse(dr["id"].ToString(), out tempid);
                Reslst.Add(new DiffLevel(tempid, dr["diff"].ToString()));
            }



            return Reslst;
        }

    }
}
