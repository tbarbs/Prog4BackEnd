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
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; } // auto set when isnerted to the db
        public string name { get; set; }
        public string login { get; set; }
        public int cldRngStart { get; set; }
        public int cldRngEnd { get; set; }
        public int hotRngStart { get; set; }
        public int hotRngEnd { get; set; }

        public User() { } // must have a default constructor to use SQLite methods 

        public User(string name, int cldRngStart, int cldRngEnd, int hotRngStart, int hotRngEnd)
        {
            this.name = name;
            this.cldRngStart = cldRngStart;
            this.cldRngEnd = cldRngEnd;
            this.hotRngStart = hotRngStart;
            this.hotRngEnd = hotRngEnd;
        }
        



        public override string ToString() // called when object given to list for default list display
        {
            return name; 
        }

    }
}