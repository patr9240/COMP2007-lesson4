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
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading page for the first time, populate the student grid, if not don't repopulate
            if(!IsPostBack)
            {
                //get the student data
                this.GetStudents();
            }
        }

        /**
         * <summary>
         * This method gets the student data from the database
         * </summary>
         * 
         * @method GetStudents
         * @return {void}
         * */
        protected void GetStudents()
        {
            //connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                //query the students table using EF and LINQ
                var Students = (from allStudents in db.Students select allStudents);

                //bind results to gridview
                StudentsGridView.DataSource = Students.ToList();
                StudentsGridView.DataBind();
            }
        }
        /**
         * <summary>
         * This event handler deletes a student from the databse using EF
         * </summary>
         * @method StudentsGridView_RowDeleting
         * @param {object} sender
         * @param {GridViewDeleteEventArgs}
         * @returns {void}
         * */
        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            int selectedRow = e.RowIndex;

            //get the selected StudentID using the grids datakey collection
            int StudentID = Convert.ToInt32(StudentsGridView.DataKeys[selectedRow].Values["StudentID"]);

            //use ef to find the selelcted student and delete it
            using (DefaultConnection db = new DefaultConnection())
            {
                //create object of the student class and store the query string inside of it
                Student deletedStudent = (from studentRecords in db.Students
                                          where studentRecords.StudentID == StudentID
                                          select studentRecords).FirstOrDefault();

                //remove the selected student from the db
                db.Students.Remove(deletedStudent);

                //save db changes
                db.SaveChanges();

                //refresh gridview
                this.GetStudents();

            }
        }
        /**
         * <summary>
         * This event handler allows pagination for the students page
         * </summary>
         * @method StudentsGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         * */
        protected void StudentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set the new page number
            StudentsGridView.PageIndex = e.NewPageIndex;

            //refresh the grid
            this.GetStudents();
        }
    }
}