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
    public class ProjectDetailsController : Controller
    {
        string connectionstring = @"Data Source=RAKI;Initial Catalog=PioneerEmployeeDB;Integrated Security=True";
        // GET: Project
        public ActionResult Index()
        {
            DataTable dtblproject = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Project_Details", sqlcon);
                sqlda.Fill(dtblproject);
            }
            return View(dtblproject);
        }
        // GET: Project/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProjectDetailsModel());
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectDetailsModel prmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "INSERT INTO Project_Details VALUES(@Project_Name,@Client_Name,@Location,@Roles,@EmployeeID)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Project_Name", prmodel.Project_Name);
                cmd.Parameters.AddWithValue("@Client_Name", prmodel.Client_Name);
                cmd.Parameters.AddWithValue("@Location", prmodel.Location);
                cmd.Parameters.AddWithValue("@Roles", prmodel.Roles);
                cmd.Parameters.AddWithValue("@EmployeeID", prmodel.EmployeeID);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            ProjectDetailsModel projmod = new ProjectDetailsModel();
            DataTable dbproject = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string editquery = "SELECT * FROM Project_Details WHERE EmployeeID=@EmployeeID";
                SqlDataAdapter sqladp = new SqlDataAdapter(editquery, sqlcon);
                sqladp.SelectCommand.Parameters.AddWithValue("@EmployeeID", id);
                sqladp.Fill(dbproject);
            }
            if (dbproject.Rows.Count == 1)
            {
                projmod.EmployeeID = Convert.ToInt32(dbproject.Rows[0][0].ToString());
                projmod.Project_Name= dbproject.Rows[0][1].ToString();
                projmod.Client_Name = dbproject.Rows[0][2].ToString();
                projmod.Location = dbproject.Rows[0][3].ToString();
                projmod.Roles = dbproject.Rows[0][4].ToString();
                return View(projmod);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(ProjectDetailsModel prdmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string edquery = "UPDATE Project_Details SET Project_Name=@Project_Name,Client_Name=@Client_Name,Location=@Location,Roles=@Roles WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(edquery, sqlcon);
                cmd.Parameters.AddWithValue("@Project_Name", prdmodel.Project_Name);
                cmd.Parameters.AddWithValue("@Client_Name", prdmodel.Client_Name);
                cmd.Parameters.AddWithValue("@Location", prdmodel.Location);
                cmd.Parameters.AddWithValue("@Roles", prdmodel.Roles);
                cmd.Parameters.AddWithValue("@EmployeeID", prdmodel.EmployeeID);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "DELETE FROM Project_Details WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
