using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stavkiBot.ViewModel
{
    public class timer
    {
        public string tm { get; set; }
        public string ts { get; set; }
        public string tt { get; set; }
        public string ta { get; set; }
        public string md { get; set; }
    }
    public class league
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cc { get; set; }
    }
    public class home
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image_id { get; set; }
        public string cc { get; set; }
    }
    public class away
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image_id { get; set; }
        public string cc { get; set; }
    }
    public class result
    {
        public int id { get; set; }
        public int sport_id { get; set; }
        public int time { get; set; }
        public int time_status { get; set; }
        public home home { get; set; }
        public away away { get; set; }
        public timer timer { get; set; }
        public league league { get; set; }
        
        
        public string ss { get; set; }
        
        public class scores
        {
           

        }
        public class extra
        {
            public int length { get; set; }
            public string pitch { get; set; }
            public string weather { get; set; }
        }
        public int has_lineup { get; set; }
        public int bet365_id { get; set; }
    }
    public class MatchViewModel
    {
        public int success { get; set; }
        public class pager
        {
            public int page { get; set; }
            public int per_page { get; set; }
            public int total { get; set; }
   
        }
        public List<result> results { get; set; }
    }
}
