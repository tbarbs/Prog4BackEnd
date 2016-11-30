using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;

namespace DataBase
{
    class LocalDataAccessLayer
    {
        //Code for singlton design pattern setup
        private static LocalDataAccessLayer data = null;
        public static LocalDataAccessLayer getInstance()
        {
            if (data == null)
                data = new LocalDataAccessLayer();
            
            return data;
        }

        //Regular class data and methods
        private SQLiteConnection dbConnection = null;

        /*=====================================================================
        * Constructor
        =====================================================================*/
        private LocalDataAccessLayer()
        {
            setUpDB();
        }

        /*=====================================================================
         * Deconstructor (Called when the object is destroyed)
         * closes connection to the database
          =====================================================================*/
        ~LocalDataAccessLayer()
        {
            shutDown();
        }

        /*=====================================================================
        * Deconstructor (Called when the object is destroyed);
         =====================================================================*/
        private void shutDown()
        {
            if (dbConnection != null)
                dbConnection.Close();
        }

        /*=====================================================================
         * Initial setup of tables in the database
         =====================================================================*/
        private void setUpTables()
        {
            dbConnection.CreateTable<User>(); // example table being created
            dbConnection.CreateTable<UILog>();
            dbConnection.CreateTable<Condition>();
            dbConnection.CreateTable<Temperature>();
            dbConnection.CreateTable<Wind>();
        }
        /*=====================================================================
         * Initial connection to the database
         =====================================================================*/
        private void setUpDB()
        {
            //get the path to where the application can store internal data 
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            //string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbFileName = "AppData.db"; // name we want to give to our db file
            string fullDBPath = System.IO.Path.Combine(folderPath, dbFileName); // properly formate the path for the system we are on


            //if file does not already exist it will be created for us
            dbConnection = new SQLiteConnection(fullDBPath);
            setUpTables(); // this happens very time.
        }


        //greeting methods
        public string getMessage(int currCon, int currTemp, int currWnd)
        {
            string message = getGreeting() + "\r\n";
            message += getFeeling(currCon, currTemp, currWnd);
            return message;
        }
        private string getGreeting()
        {
            DateTime currTime = DateTime.Now;
            string greeting = "";
            if(currTime.Hour>0 && currTime.Hour<12)
            {
                greeting = "Good Morning ";
            }
            else if (currTime.Hour>=12 && currTime.Hour<5)
            {
                greeting = "Good Afternoon ";
            }
            else
            {
                greeting = "Good Evening ";
            }
            greeting += getUser().name + ",";
            return greeting;
        }
        private string getFeeling(int currCon, int currTemp, int currWnd)
        {
            int cond = getConditionByID(currCon).condCode;
            int temp = getTempuratureByID(currTemp).tempCode;
            int wnd = getWindByID(currWnd).windCode;
            int currWeather = cond + temp + wnd;
            User user = getUser();
            user.updateRng();
            string feeling = "";
            if(currWeather>=user.cldRngStart && currWeather<=user.cldRngEnd)
            {
                feeling = "You may feel cold today.";
            }
            else if(currWeather >= user.hotRngStart && currWeather <= user.hotRngEnd)
            {
                feeling = "You may feel hot today.";
            }
            else
            {
                feeling = "Not enough data has been entered.";
            }
            return feeling;
        }

        //manipulating DB methods
        public void addLogEntry(int condition, int temperature, int windSp, Boolean type)
        {
            Condition cond = getConditionByID(condition);
            Temperature temp = getTempuratureByID(temperature);
            Wind wnd = getWindByID(windSp);
            int locCondID = cond.condCode;
            int locTempID = temp.tempCode;
            int locWndID = wnd.windCode;
            UILog newEntry = new UILog(locCondID, locTempID, locWndID, type);
            addUILog(newEntry);
        }
        private void addUILog(UILog info)
        {
            dbConnection.Insert(info);
        }

        public UILog getLogEntryByID(int id)
        {
            return dbConnection.Get<UILog>(id);
        }

        public void deleteLogEntryByID(int id)
        {
            dbConnection.Delete<UILog>(id);
        }

        public void updateUserInfo(User info)
        {
            dbConnection.Update(info);
        }
        public User getUser()
        {
            int index = 1;
            User user = dbConnection.Get<User>(index);
            return user;
        }
        public void addUser(User info)
        {
            dbConnection.Insert(info);
        }
        public void updateLogin(string loginStr)
        {
            User user = getUser();
            user.login = loginStr;
        }
        public void addCondition(Condition info)
        {
            dbConnection.Insert(info);
        }
        public Condition getConditionByID(int id)
        {
            List<Condition> list = new List <Condition>(dbConnection.Table<Condition>());
            List<Condition> list2 = new List<Condition>(list.Where<Condition>(p => p.yahooCondId == id));

            Condition row = list2[0];
            //return new List<Student>(dbConnection.Table<Student>().OrderBy(st => st.name));

            return row;
        }
        public void addTemperature(Temperature info)
        {
            dbConnection.Insert(info);
        }
        public Temperature getTempuratureByID(int id)
        {
            List<Temperature> list = new List<Temperature>(dbConnection.Table<Temperature>());
            List<Temperature> list2 = new List<Temperature>(list.Where<Temperature>(p => p.temperature == id));

            Temperature row = list2[0];
            //return new List<Student>(dbConnection.Table<Student>().OrderBy(st => st.name));

            return row;
        }
        public void addWind(Wind info)
        {
            dbConnection.Insert(info);
        }
        public Wind getWindByID(int id)
        {
            List<Wind> list = new List<Wind>(dbConnection.Table<Wind>());
            List<Wind> list2 = new List<Wind>(list.Where<Wind>(p => p.windSp == id));

            Wind row = list2[0];
            //return new List<Student>(dbConnection.Table<Student>().OrderBy(st => st.name));

            return row;
        }

        public List<UILog> getAllLogEntries()
        {
            //gets all elements in the UILog table and packages it into a List
            return new List<UILog>(dbConnection.Table<UILog>());
        }


        /* public List<Student> getAllStudentsOrdered()
         {
             //gets all elements in the Student table and packages it into a List
             return new List<Student>(dbConnection.Table<Student>().OrderBy(st => st.name));
         }*/
    }
}