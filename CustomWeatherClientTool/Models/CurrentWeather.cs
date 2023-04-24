using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWeatherClientTool.Models
{
    public class CurrentWeather
    {
        public float temperature { get; set; }
        public float windspeed { get; set; }
        public float winddirection { get; set;}
        public int weathercode { get; set;}
        public int is_day { get; set; }
        public string time { get; set;}
    }
}
