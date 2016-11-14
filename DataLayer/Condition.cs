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
    public class Condition
    {
        [PrimaryKey]
        public int yahooCondId { get; set; } // auto set when isnerted to the db
        public string condName { get; set; }
        public int condCode { get; set; }
        public int imgName { get; set; }


        public Condition() { } // must have a default constructor to use SQLite methods 

        public Condition(int yahooCondId, String condName, int condCode, int imgName)
        {
            this.yahooCondId = yahooCondId;
            this.condName = condName;
            this.condCode = condCode;
            this.imgName = imgName;
        }
        



        public override string ToString() // called when object given to list for default list display
        {
            return condName + " " + condCode; 
        }

    }
}