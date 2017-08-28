using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using PioneerTest.Models;

namespace PioneerTech.MVC.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        string connectionstring = @"Data Source=RAKI;Initial Catalog=PioneerEmployeeDB;Integrated Security=True";
        // GET: EmployeeDetails
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblemployee = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Employee_Details", sqlcon);
                sqlda.Fill(dtblemployee);
            }

                return View(dtblemployee);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new EmployeeDetailsModel());
        }

        // POST: EmployeeDetails/Create
        [HttpPost]
        public ActionResult Create(EmployeeDetailsModel empmodel)
        {
               using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    sqlcon.Open();
                    string query = "INSERT INTO Employee_Details VALUES(@First_Name,@Last_Name,@Email,@Mobile_Number,@AlternateMobileNumber,@Address1,@Address2,@Current_Country,@Home_Country,@ZipCode)";
                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    cmd.Parameters.AddWithValue("@First_Name", empmodel.First_Name);
                    cmd.Parameters.AddWithValue("@Last_Name", empmodel.Last_Name);
                    cmd.Parameters.AddWithValue("@Email", empmodel.Email);
                    cmd.Parameters.AddWithValue("@Mobile_Number", empmodel.Mobile_Number);
                    cmd.Parameters.AddWithValue("@AlternateMobileNumber", empmodel.AlternateMobileNumber);
                    cmd.Parameters.AddWithValue("@Address1", empmodel.Address1);
                    cmd.Parameters.AddWithValue("@Address2", empmodel.Address2);
                    cmd.Parameters.AddWithValue("@Current_Country", empmodel.Current_Country);
                    cmd.Parameters.AddWithValue("@Home_Country", empmodel.Home_Country);
                    cmd.Parameters.AddWithValue("@ZipCode", empmodel.ZipCode);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            
        }

        // GET: EmployeeDetails/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeDetailsModel edmod = new EmployeeDetailsModel();
            DataTable dtbemployee = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string editquery = "SELECT * FROM Employee_Details WHERE EmployeeID=@EmployeeID";
                SqlDataAdapter sqladp = new SqlDataAdapter(editquery, sqlcon);
                sqladp.SelectCommand.Parameters.AddWithValue("@EmployeeID", id);
                sqladp.Fill(dtbemployee);
            }
            if (dtbemployee.Rows.Count == 1)
            {
                edmod.EmployeeID = Convert.ToInt32(dtbemployee.Rows[0][0].ToString());
                edmod.First_Name = dtbemployee.Rows[0][1].ToString();
                edmod.Last_Name = dtbemployee.Rows[0][2].ToString();
                edmod.Email = dtbemployee.Rows[0][3].ToString();
                edmod.Mobile_Number = dtbemployee.Rows[0][4].ToString();
                edmod.AlternateMobileNumber = dtbemployee.Rows[0][5].ToString();
                edmod.Address1 = dtbemployee.Rows[0][6].ToString();
                edmod.Address2 = dtbemployee.Rows[0][7].ToString();
                edmod.Current_Country = dtbemployee.Rows[0][8].ToString();
                edmod.Home_Country = dtbemployee.Rows[0][9].ToString();
                edmod.ZipCode = Convert.ToInt32(dtbemployee.Rows[0][10].ToString());
                return View(edmod);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: EmployeeDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeDetailsModel edmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "UPDATE Employee_Details SET First_Name=@First_Name, Last_Name=@Last_Name, Email=@Email, Mobile_Number=@Mobile_Number, AlternateMobileNumber=@AlternateMobileNumber, Address1=@Address1, Address2=@Address2, Current_Country=@Current_Country, Home_Country=@Home_Country, ZipCode=@Zipcode WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", edmodel.EmployeeID);
                cmd.Parameters.AddWithValue("@First_Name", edmodel.First_Name);
                cmd.Parameters.AddWithValue("@Last_Name", edmodel.Last_Name);
                cmd.Parameters.AddWithValue("@Email", edmodel.Email);
                cmd.Parameters.AddWithValue("@Mobile_Number", edmodel.Mobile_Number);
                cmd.Parameters.AddWithValue("@AlternateMobileNumber", edmodel.AlternateMobileNumber);
                cmd.Parameters.AddWithValue("@Address1", edmodel.Address1);
                cmd.Parameters.AddWithValue("@Address2", edmodel.Address2);
                cmd.Parameters.AddWithValue("@Current_Country", edmodel.Current_Country);
                cmd.Parameters.AddWithValue("@Home_Country", edmodel.Home_Country);
                cmd.Parameters.AddWithValue("@ZipCode", edmodel.ZipCode);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
           
        }

        // GET: EmployeeDetails/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "DELETE FROM Employee_Details WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID",id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
