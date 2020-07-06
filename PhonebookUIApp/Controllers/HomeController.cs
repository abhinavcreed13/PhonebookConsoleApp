using DataAccessLayer;
using PhonebookUIApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhonebookUIApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //SQL Server
            SQLDALManager manager = new SQLDALManager("dbConnKey");
            DataTable allUsers = manager.ExecuteStoredProcedure("getAllUsers");

            List<UserData> usersList = new List<UserData>();
            List<UserPhoneBookData> phonebook = new List<UserPhoneBookData>();
            foreach (DataRow dr in allUsers.Rows)
            {
                UserData user = new UserData
                {
                    UserId = Convert.ToInt32(dr["user_id"]),
                    UserName = dr["user_name"].ToString()
                };
                usersList.Add(user);

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@userId", Convert.ToInt32(dr["user_id"])));
                DataTable contacts = manager.ExecuteStoredProcedure("getContacts", param);

                List<UserData> contcts = new List<UserData>();
                foreach (DataRow contact in contacts.Rows)
                {
                    contcts.Add(new UserData
                    {
                        UserId = Convert.ToInt32(contact["user_id"]),
                        UserName = contact["user_name"].ToString()
                    });
                }
                phonebook.Add(new UserPhoneBookData { User = user, Contacts = contcts });
            }
            



            //Dummy data
            //List<UserData> dummyUsers = new List<UserData>
            //{
            //    new UserData { UserId = 1, UserName = "Alice"},
            //    new UserData { UserId = 2, UserName = "Bob"},
            //    new UserData { UserId = 3 , UserName = "Micheal"}
            //};

            //List<UserPhoneBookData> dummyPhonebook = new List<UserPhoneBookData>
            //{
            //    new UserPhoneBookData { User = usersList[0],
            //        Contacts = new List<UserData>{ usersList[1], usersList[2] } }
            //};

            UserModel model = new UserModel
            {
                Users = usersList,
                PhoneBook = phonebook
            };

            // strongly typed view concept
            return View(model);
        }
    }
}