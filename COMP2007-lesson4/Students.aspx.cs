using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using COMP2007_lesson4.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;
namespace COMP2007_lesson4
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading page for the first time, populate the student grid, if not don't repopulate
            if(!IsPostBack)
            {
                Session["SortColumn"] = "StudentID"; //default sort column
                Session["SortDirection"] = "ASC"; // default sort direction
                //get the student data
                this.GetStudents();
            }
        }

        /**
         * <summary>
         * This method gets the student data from the database
         * </summary>
         * @method GetStudents
         * @return {void}
         * */
        protected void GetStudents()
        {
            //connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                //query the students table using EF and LINQ
                var Students = (from allStudents in db.Students select allStudents);

                //bind results to gridview
                StudentsGridView.DataSource = Students.AsQueryable().OrderBy(SortString).ToList();
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
        /**
         * <summary>
         * This method changes the amount of student displayed per page when a different index is selected in the dropdown
         * </summary>
         * @method PageSizeDropDownList_SelectedIndexChanged
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         * */
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new page size
            StudentsGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //refresh
            this.GetStudents();
        }

        /**
         * <summary>
         * 
         * </summary>
         * @method StudentsGridView_Sorting
         * @param
         * @returns {void}
         * */
        protected void StudentsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetStudents();

            //toggle the direction from ASC and DSC
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DSC" : "ASC";
        }

        /**
         * <summary>
         * 
         * </summary>
         * */
        protected void StudentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if(e.Row.RowType == DataControlRowType.Header)//if header row is clicked
                {
                    LinkButton linkButton = new LinkButton();

                    for(int index = 0; index < StudentsGridView.Columns.Count - 1; index++)
                    {
                        if (StudentsGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if(Session["SortDirection"].ToString() == "ASC")
                            {
                                linkButton.Text = "<i class='fa fa-caret-up fa-lg'> </i>";
                            }
                            else
                            {
                                linkButton.Text = "<i class='fa fa-caret-down fa-lg'> </i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkButton);
                        }
                    }
                }
            }
        }
    }
}