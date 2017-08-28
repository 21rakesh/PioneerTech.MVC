using PioneerTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PioneerTech.MVC.Controllers
{
    public class TechnicalDetailsController : Controller
    {
        string connectionstring = @"Data Source=RAKI;Initial Catalog=PioneerEmployeeDB;Integrated Security=True";
        // GET: Technical
        public ActionResult Index()
        {
            DataTable dtbltechnical = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Technical_Details", sqlcon);
                sqlda.Fill(dtbltechnical);
            }
            return View(dtbltechnical);
        }
        // GET: Technical/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new TechnicalDetailsModels());
        }

        // POST: Technical/Create
        [HttpPost]
        public ActionResult Create(TechnicalDetailsModels techmodels)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "INSERT INTO Technical_Details VALUES(@UI,@Programming_Languages,@ORM_Technologies,@Databases)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@UI", techmodels.UI);
                cmd.Parameters.AddWithValue("@Programming_Languages", techmodels.Programming_Languages);
                cmd.Parameters.AddWithValue("@ORM_Technologies", techmodels.ORM_Technologies);
                cmd.Parameters.AddWithValue("@Databases", techmodels.Databases);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Technical/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TechnicalDetailsModels techmod = new TechnicalDetailsModels();
            DataTable dbtechnical = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string editquery = "SELECT * FROM Technical_Details WHERE EmployeeID=@EmployeeID";
                SqlDataAdapter sqladp = new SqlDataAdapter(editquery, sqlcon);
                sqladp.SelectCommand.Parameters.AddWithValue("@EmployeeID", id);
                sqladp.Fill(dbtechnical);
            }
            if (dbtechnical.Rows.Count == 1)
            {
                techmod.EmployeeID = Convert.ToInt32(dbtechnical.Rows[0][0].ToString());
                techmod.UI = dbtechnical.Rows[0][1].ToString();
                techmod.Programming_Languages = dbtechnical.Rows[0][2].ToString();
                techmod.ORM_Technologies = dbtechnical.Rows[0][3].ToString();
                techmod.Databases = dbtechnical.Rows[0][4].ToString();
                return View(techmod);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Technical/Edit/5
        [HttpPost]
        public ActionResult Edit(TechnicalDetailsModels tedmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string edquery = "UPDATE Technical_Details SET UI=@UI,Programming_Languages=@Programming_Languages,ORM_Technologies=@ORM_Technologies,Databases=@Databases WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(edquery, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", tedmodel.EmployeeID);
                cmd.Parameters.AddWithValue("@UI", tedmodel.UI);
                cmd.Parameters.AddWithValue("@Programming_Languages", tedmodel.Programming_Languages);
                cmd.Parameters.AddWithValue("@ORM_Technologies", tedmodel.ORM_Technologies);
                cmd.Parameters.AddWithValue("@Databases", tedmodel.Databases);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Technical/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "DELETE FROM Technical_Details WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}
