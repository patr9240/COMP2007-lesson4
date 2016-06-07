using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_lesson4.Models;
using System.Web.ModelBinding;
namespace COMP2007_lesson4
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //redirect back to student page
            Response.Redirect("~/Students.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //use ef to connect to the server
            using(DefaultConnection db = new DefaultConnection())
            {
                //use the student model to create a new student object and save a new record
                Student newStudent = new Student();

                //add data to the student record
                newStudent.LastName = LastNameTextBox.Text;
                newStudent.FirstMidName = FirstNameTextBox.Text;
                newStudent.EnrollmentDate = Convert.ToDateTime(EnrollmentDateTextBox.Text);

                //use linq to add new student into the db
                db.Students.Add(newStudent);

                //save our changes
                db.SaveChanges();

                //redirect back to students page
                Response.Redirect("~/Students.aspx");
            }
        }
    }
}