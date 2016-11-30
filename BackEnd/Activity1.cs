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

using DataBase;

namespace BackEnd
{
    [Activity(Label = "Activity1", MainLauncher = true)]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout1);

            LocalDataAccessLayer data = LocalDataAccessLayer.getInstance();

           data.addCondition(new Condition(1, "Rain", 1, 1));
           data.addTemperature(new Temperature(0, 1));
           data.addWind( new Wind(15,2) );
           data.addTemperature(new Temperature(2, 2));

            data.addCondition(new Condition(2, "Sun", 2, 2));
            data.addTemperature(new Temperature(20, 6));
            data.addTemperature(new Temperature(30, 16));

            data.addLogEntry(1, 20, 15, true);
            data.addLogEntry(1, 30, 15, true);
            data.addLogEntry(2, 2, 15, false);
            data.addLogEntry(2, 0, 15, false);

            data.addUser(new User("Tory", "Poop"));

            data.getMessage(2, 20, 15);


            //TextView t = FindViewById<TextView>(Resource.Id.textView1);
           // t.Text = ""+ num;


        }
    }
}