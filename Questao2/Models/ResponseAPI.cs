using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2.Models
{
    public class ResponseAPI
    {
        public int page { get; set; }

        public int per_page { get; set; }

        public int total { get; set; }

        public int total_pages { get; set; }

        public List<Data> data { get; set; }
    }

    public class Data
    {
        public string competition { get; set; }

        public int year { get; set; }

        public string round { get; set; }

        public string team1 { get; set; }

        public string team2 { get; set; }

        public int team1goals { get; set; }

        public int team2goals { get; set; }
    }

    public enum TYPE_TEAM
    {
        HOST = 1,
        VISITOR = 2
    }
}
