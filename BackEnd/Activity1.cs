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

            //LocalDataAccessLayer data = LocalDataAccessLayer.getInstance();

           //data.addCondition(new Condition(1, "Poop", 1, 1));
           //data.addTemperature(new Temperature(20, 1));
           //data.addWind( new Wind(15,2) );


            //data.addLogEntry(1, 20, 15, false);

            //int num = User.createCStart();


            TextView t = FindViewById<TextView>(Resource.Id.textView1);
            //t.Text = ""+ num;


        }
    }
}