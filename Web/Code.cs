using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public partial class Code_t
    {
        public string[] Tags {
            get {
                return Tag.Split(',');
            }
        }
        public int score {
            get {

                int score = Rating.HasValue ? Rating.Value : 0;
                return score; 
            }
        }
    }
}