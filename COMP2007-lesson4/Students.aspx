﻿<%@ Page Title="Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="COMP2007_lesson4.Students" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Student List</h1>
                <asp:GridView ID="StudentsGridView" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField Datafield="StudentID" HeaderText="Student ID" Visible="true" />
                        <asp:BoundField Datafield="LastName" HeaderText="Last Name" Visible="true" />
                        <asp:BoundField Datafield="FirstMidName" HeaderText="First Name" Visible="true" />
                        <asp:BoundField Datafield="EnrollmentDate" HeaderText="Enrollment Date" Visible="true" DataFormatString="{0:MMM dd, yyyy}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>

