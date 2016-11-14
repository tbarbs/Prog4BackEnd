using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;


namespace DataBase
{
    public class UILog
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; } // auto set when isnerted to the db
        public string condition { get; set; }
        public int temperature { get; set; }
        public int windSp { get; set; }
        public Boolean type { get; set; }


        public UILog() { } // must have a default constructor to use SQLite methods 

        public UILog(string condition, int temperature, int windSp, Boolean type)
        {
            this.condition = condition;
            this.temperature = temperature;
            this.windSp = windSp;
            this.type = type;
        }
        



        public override string ToString() // called when object given to list for default list display
        {
            String str = "Temperature: " + temperature + ", Condition: " + condition + ", Wind Speed:" + windSp + ", Type:" + type;
            return str ; 
        }

    }
}