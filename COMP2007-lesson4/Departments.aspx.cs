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
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading page for the first time, populate the department grid, if not don't repopulate
            if (!IsPostBack)
            {
                Session["SortColumn"] = "DepartmentID"; //default sort column
                Session["SortDirection"] = "ASC"; // default sort direction
                //get the Department data
                this.GetDepartments();
            }
        }

        /**
         * <summary>
         * This method gets the department data from the database
         * </summary>
         * @method GetDepartments
         * @return {void}
         * */
        protected void GetDepartments()
        {
            //connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                //query the departments table using EF and LINQ
                var Departments = (from allDepartments in db.Departments select allDepartments);

                //bind results to gridview
                DepartmentsGridView.DataSource = Departments.AsQueryable().OrderBy(SortString).ToList();
                DepartmentsGridView.DataBind();
            }
        }

        /**
         * <summary>
         * This event handler allows pagination for the departments page
         * </summary>
         * @method DepartmentsGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         * */
        protected void DepartmentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set the new page number
            DepartmentsGridView.PageIndex = e.NewPageIndex;

            //refresh the grid
            this.GetDepartments();
        }

        /**
         * <summary>
         * This event handler deletes a department from the databse using EF
         * </summary>
         * @method DepartmentsGridView_RowDeleting
         * @param {object} sender
         * @param {GridViewDeleteEventArgs}
         * @returns {void}
         * */
        protected void DepartmentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            int selectedRow = e.RowIndex;

            //get the selected DepartmentID using the grids datakey collection
            int DepartmentID = Convert.ToInt32(DepartmentsGridView.DataKeys[selectedRow].Values["DepartmentID"]);

            //use ef to find the selelcted Department and delete it
            using (DefaultConnection db = new DefaultConnection())
            {
                //create object of the department class and store the query string inside of it
                Department deletedDepartment = (from departmentRecords in db.Departments
                                          where departmentRecords.DepartmentID == DepartmentID
                                          select departmentRecords).FirstOrDefault();

                //remove the selected department from the db
                db.Departments.Remove(deletedDepartment);

                //save db changes
                db.SaveChanges();

                //refresh gridview
                this.GetDepartments();

            }
        }
        /**
         * <summary>
         * 
         * </summary>
         * @method DepartmentsGridView_Sorting
         * @param
         * @returns {void}
         * */
        protected void DepartmentsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetDepartments();

            //toggle the direction from ASC and DSC
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DSC" : "ASC";
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * @method DepartmentsGridView_RowDataBound
         * @param {object} sender
         * @param {GridViewRowEventArgs} e
         * @returns {void}
         * */
        protected void DepartmentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)//if header row is clicked
                {
                    LinkButton linkButton = new LinkButton();

                    for (int index = 0; index < DepartmentsGridView.Columns.Count - 1; index++)
                    {
                        if (DepartmentsGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
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
        /**
         * <summary>
         * This method changes the amount of departments displayed per page when a different index is selected in the dropdown
         * </summary>
         * @method PageSizeDropDownList_SelectedIndexChanged
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         * */
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new page size
            DepartmentsGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //refresh
            this.GetDepartments();
        }
    }
}