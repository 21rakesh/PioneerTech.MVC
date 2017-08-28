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
    public class EducationDetailsController : Controller
    {
        string connectionstring = @"Data Source=RAKI;Initial Catalog=PioneerEmployeeDB;Integrated Security=True";
        // GET: EducationDetails
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtbleducation = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Education_Details", sqlcon);
                sqlda.Fill(dtbleducation);
            }

            return View(dtbleducation);
        }

        // GET: EducationDetails/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new EducationDetailsModel());
        }

        // POST: EducationDetails/Create
        [HttpPost]
        public ActionResult Create(EducationDetailsModel edmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "INSERT INTO Education_Details VALUES(@CourseType,@YearOfPass,@CourseSpecialisation)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@CourseType", edmodel.CourseType);
                cmd.Parameters.AddWithValue("@YearOfPass", edmodel.YearOfPass);
                cmd.Parameters.AddWithValue("@CourseSpecialisation", edmodel.CourseSpecialisation);
                
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: EducationDetails/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EducationDetailsModel edumod = new EducationDetailsModel();
            DataTable dbeducation = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string editquery = "SELECT * FROM Education_Details WHERE EmployeeID=@EmployeeID";
                SqlDataAdapter sqladp = new SqlDataAdapter(editquery, sqlcon);
                sqladp.SelectCommand.Parameters.AddWithValue("@EmployeeID", id);
                sqladp.Fill(dbeducation);
            }
            if (dbeducation.Rows.Count == 1)
            {
                edumod.EmployeeID = Convert.ToInt32(dbeducation.Rows[0][0].ToString());
                edumod.CourseType = dbeducation.Rows[0][1].ToString();
                edumod.YearOfPass= Convert.ToInt32(dbeducation.Rows[0][2].ToString());
                edumod.CourseSpecialisation = dbeducation.Rows[0][3].ToString();
                return View(edumod);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: EducationDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(EducationDetailsModel edtmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string edquery = "UPDATE Education_Details SET CourseType=@CourseType,YearOfPass=@YearOfPass,CourseSpecialisation=@CourseSpecialisation WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(edquery, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", edtmodel.EmployeeID);
                cmd.Parameters.AddWithValue("@CourseType", edtmodel.CourseType);
                cmd.Parameters.AddWithValue("@YearOfPass", edtmodel.YearOfPass);
                cmd.Parameters.AddWithValue("@CourseSpecialisation", edtmodel.CourseSpecialisation);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: EducationDetails/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "DELETE FROM Education_Details WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
