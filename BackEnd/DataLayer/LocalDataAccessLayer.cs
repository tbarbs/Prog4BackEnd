using System;
using System.Collections.Generic;
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
            return dbConnection.Get<User>(0);
        }
        public void addUser(User info)
        {
            dbConnection.Insert(info);
        }
        public void addCondition(Condition info)
        {
            dbConnection.Insert(info);
        }
        public Condition getConditionByID(int id)
        {
            return dbConnection.Get<Condition>(id);
        }
        public void addTemperature(Temperature info)
        {
            dbConnection.Insert(info);
        }
        public Temperature getTempuratureByID(int id)
        {
            return dbConnection.Get<Temperature>(id);
        }
        public void addWind(Wind info)
        {
            dbConnection.Insert(info);
        }
        public Wind getWindByID(int id)
        {
            return dbConnection.Get<Wind>(id);
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