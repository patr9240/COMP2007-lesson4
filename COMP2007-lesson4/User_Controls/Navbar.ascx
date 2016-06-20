﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="COMP2007_lesson4.Navbar" %>
<nav class="navbar navbar-inverse" role="navigation">
    <div class="container-fluid">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="Default.aspx"><i class="fa fa-graduation-cap fa-lg"></i>Contoso University</a>
        </div>
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav navbar-right">
                <li id="home" runat="server"><a class="navbar-brand" href="/Default.aspx"><i class="fa fa-home fa-lg"></i> Home</a></li>

                <asp:PlaceHolder ID="PublicPlaceHolder" runat="server" >
                <li id="login" runat="server"><a class="navbar-brand" href="/Login.aspx"><i class="fa fa-sign-in fa-lg"></i> Login</a></li>
                <li id="register" runat="server"><a class="navbar-brand" href="/Register.aspx"><i class="fa fa-user-plus fa-lg"></i> Register</a></li>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="UserPlaceHolder" runat="server" >
                <li id="users" runat="server"><a class="navbar-brand" href="/Admin/Users.aspx"><i class="fa fa-users fa-lg"></i> Users</a></li>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="ContosoPlaceHolder" runat="server" >
                <li id="menu" runat="server"><a class="navbar-brand" href="/Contoso/MainMenu.aspx"><i class="fa fa-map-signs fa-lg"></i> Main Menu</a></li>
                <li id="students" runat="server"><a class="navbar-brand" href="/Contoso/Students.aspx"><i class="fa fa-leanpub fa-lg"></i> Students</a></li>
                <li id="courses" runat="server"><a class="navbar-brand" href="/Contoso/Courses.aspx"><i class="fa fa-book fa-lg"></i> Courses</a></li>
                <li id="departments" runat="server"><a class="navbar-brand" href="/Contoso/Departments.aspx"><i class="fa fa-puzzle-piece fa-lg"></i> Departments</a></li>
                <li id="logout" runat="server"><a class="navbar-brand" href="/Logout.aspx"><i class="fa fa-sign-out fa-lg"></i> Logout</a></li>
                </asp:PlaceHolder>

                <li id="contact" runat="server"><a class="navbar-brand" href="/Contact.aspx"><i class="fa fa-phone fa-lg"></i> Contact</a></li>
           </ul>
        </div>
        <!-- /.navbar-collapse -->
    </div>
    <!-- /.container-fluid -->
</nav>
