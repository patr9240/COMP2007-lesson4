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
        
    }
}