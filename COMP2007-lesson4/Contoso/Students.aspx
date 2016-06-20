<%@ Page Title="Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="COMP2007_lesson4.Students" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Student List</h1>
                <a href="/Contoso/StudentDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Student</a>

                <div>
                    <label for="PageSizeDropDownList"></label>
                    <asp:DropDownList runat="server" ID="PageSizeDropDownList" AutoPostBack="true" 
                        CssClass="btn btn-default bt-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>

                <asp:GridView ID="StudentsGridView" runat="server" CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="StudentsGridView_PageIndexChanging" 
                    DataKeyNames="StudentID" OnRowDeleting="StudentsGridView_RowDeleting" AllowSorting="true"  OnSorting="StudentsGridView_Sorting" OnRowDataBound="StudentsGridView_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="StudentID" HeaderText="Student ID" Visible="true" SortExpression="StudentID"/>
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" Visible="true" SortExpression="LastName"/>
                        <asp:BoundField DataField="FirstMidName" HeaderText="First Name" Visible="true" SortExpression="FirstName"/>
                        <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" Visible="true" DataFormatString="{0:MMM dd, yyyy}" SortExpression="EnrollmentDate"/>

                        <asp:HyperLinkField HeaderText="Edit" Text="<i calss='fa fa-encil-square-o fa-lg'></i> Edit"
                            NavigateUrl="~/Contoso/StudentDetails.aspx"  ControlStyle-CssClass="btn btn-primary btn-sm" DataNavigateUrlFields="StudentID"
                            runat="server" DataNavigateUrlFormatString="StudentDetails.aspx?StudentID={0}" />
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true" ButtonType="Link"
                            ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>

