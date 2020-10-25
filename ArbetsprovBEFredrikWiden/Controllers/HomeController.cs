using ArbetsprovBEFredrikWiden.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace subba1.web.Controllers
{
    public class HomeController : Controller
    {
        private string CS = ConfigurationManager.ConnectionStrings["DBSub"].ConnectionString;
        // GET: Home
        public ActionResult Index()
        {
            var listOfPearson = new List<Person>();

            using (SqlConnection con = new SqlConnection(CS))
            {
                //string sql = "SELECT cp.id, cp.FirstName, cp.LastName, cp.EmailAdress, cp.PhoneNumber, f.CompanyName FROM [ContactPerson] cp INNER JOIN [Companies] f ON cp.CompanyID = f.CompanyId";
                //SqlCommand cmd = new SqlCommand(sql, con);

                SqlCommand cmd = new SqlCommand("SELECT * FROM ContactPerson", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var person = new Person();

                    person.id = Convert.ToInt32(rdr["id"]);
                    person.FirstName = rdr["FirstName"].ToString();
                    person.LastName = rdr["LastName"].ToString();
                    person.EmailAdress = rdr["EmailAdress"].ToString();
                    person.PhoneNumber = rdr["PhoneNumber"].ToString();
                    person.CompanyID = Convert.ToInt32(rdr["CompanyID"]);
                    listOfPearson.Add(person);
                }
            }

            return View(listOfPearson);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Person person)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    string sql = "INSERT INTO [ContactPerson](FirstName,LastName,EmailAdress,PhoneNumber,CompanyID) values (@first,@last,@email,@phone,@companyID)";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@first", person.FirstName);
                    cmd.Parameters.AddWithValue("@last", person.LastName);
                    cmd.Parameters.AddWithValue("@email", person.EmailAdress);
                    cmd.Parameters.AddWithValue("@phone", person.PhoneNumber);
                    cmd.Parameters.AddWithValue("@companyID", person.CompanyID);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception)
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    string sql = "SELECT * FROM [ContactPerson] WHERE id = " + id;
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var person = new Person();

                        person.FirstName = rdr["FirstName"].ToString();
                        person.LastName = rdr["LastName"].ToString();
                        person.EmailAdress = rdr["EmailAdress"].ToString();
                        person.PhoneNumber = rdr["PhoneNumber"].ToString();
                        person.CompanyID = Convert.ToInt32(rdr["CompanyID"]);

                        return View(person);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person person)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    string sql = "UPDATE [ContactPerson] SET FirstName= @first, LastName = @last, EmailAdress= @email, PhoneNumber=@phone, CompanyID= @companyID WHERE ID = " + id;
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@first", person.FirstName);
                    cmd.Parameters.AddWithValue("@last", person.LastName);
                    cmd.Parameters.AddWithValue("@email", person.EmailAdress);
                    cmd.Parameters.AddWithValue("@phone", person.PhoneNumber);
                    cmd.Parameters.AddWithValue("@companyID", person.CompanyID);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}