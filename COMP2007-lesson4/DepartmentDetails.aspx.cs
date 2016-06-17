﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_lesson4.Models;
using System.Web.ModelBinding;
namespace COMP2007_lesson4
{
    public partial class DepartmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetDepartment();
            }
        }

        protected void GetDepartment()
        {
            // populate teh form with existing data from the database
            int DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            // connect to the EF DB
            using (ContosoConnection db = new ContosoConnection())
            {
                // populate a department object instance with the DepartmentID from the URL Parameter
                Department updatedDepartment = (from department in db.Departments
                                          where department.DepartmentID == DepartmentID
                                          select department).FirstOrDefault();

                // map the department properties to the form controls
                if (updatedDepartment != null)
                {
                    DepartmentNameTextBox.Text = updatedDepartment.Name;
                    BudgetTextBox.Text = updatedDepartment.Budget.ToString();
                    

                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (ContosoConnection db = new ContosoConnection())
            {
                // use the Departments model to create a new department object and
                // save a new record
                Department newDepartment = new Department();

                int DepartmentID = 0;

                if (Request.QueryString.Count > 0) // our URL has a DepartmentID in it
                {
                    // get the id from the URL
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    // get the current student from EF DB
                    newDepartment = (from department in db.Departments
                                  where department.DepartmentID == DepartmentID
                                  select department).FirstOrDefault();
                }

                // add form data to the new department record
                newDepartment.Name = DepartmentNameTextBox.Text;
                newDepartment.Budget = Convert.ToDecimal(BudgetTextBox.Text);

                // use LINQ to ADO.NET to add / insert new department into the database

                if (DepartmentID == 0)
                {
                    db.Departments.Add(newDepartment);
                }


                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated departments page
                Response.Redirect("~/Departments.aspx");
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Students page
            Response.Redirect("~/Departments.aspx");
        }
    }
}