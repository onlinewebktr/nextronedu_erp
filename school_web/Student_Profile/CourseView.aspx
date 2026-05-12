<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/User.Master" AutoEventWireup="true" CodeBehind="CourseView.aspx.cs" Inherits="school_web.Student_Profile.CourseView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Subject List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a> <i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item">Subject List</li>
    </ol>

    <div class="grid-form">
        <div class="grid-form1">
            <div class="panel-body">
                <div class="row" style="padding: 0px 0px 0px 0px;">
                    <div style="margin: 0px 0px 10px 0px; padding: 0px; float: left; width: 100%; height: auto;">
                        <h2 class=" blue_bg">Subject List
                        </h2>
                        <asp:HiddenField ID="hdUserId" runat="server" />
                        <div class="row form-group team-sec">
                            <asp:Repeater ID="RpEnrollCourse" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="team">
                                            <div class="member-img" style="display:none">
                                                <a href="CourseDetails.aspx?CourseId=<%#Eval("CourseID") %>" title="" class="ext-link">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Image") %>' CssClass="img-responsive" AlternateText='<%#Eval("CourseName") %>' />
                                                </a>
                                            </div>
                                            <div class="team-info">
                                                <h3>
                                                    <asp:Literal ID="Literal3" runat="server" Text='<%#Eval("CategoryName") %>' /></h3>
                                                <span>
                                                    <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("CourseName") %>' /></span>
                                                <p>
                                                    <asp:Literal ID="Literal2" runat="server" Text='<%#Eval("Description") %>' />
                                                </p>

                                                <a href='CourseDetails.aspx?CourseId=<%#Eval("CourseID") %>&sectionid=<%#Eval("section")%>' title="" class="lnk-default">View Details</a>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <%--<div class="row" style="padding: 0px 0px 0px 0px;">
                    <div style="margin: 0px 0px 10px 0px; padding: 0px; float: left; width: 100%; height: auto;">
                        <h2 class=" blue_bg">Completed Subject 
                        </h2>
                        <div class="row form-group team-sec">
                            <asp:Repeater ID="RpCompletedCourse" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="team">
                                            <div class="member-img">
                                                <a href="CourseDetails.aspx?CourseId=<%#Eval("CourseID") %>" title="" class="ext-link">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Image") %>' CssClass="img-responsive" AlternateText='<%#Eval("CourseName") %>' />
                                                </a>
                                            </div>
                                            <div class="team-info">
                                                <h3>
                                                    <asp:Literal ID="Literal3" runat="server" Text='<%#Eval("CategoryName") %>' /></h3>
                                                <span>
                                                    <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("CourseName") %>' /></span>
                                                <p>
                                                    <asp:Literal ID="Literal2" runat="server" Text='<%#Eval("Description") %>' />
                                                </p>
                                                <a href="CourseDetails.aspx?CourseId=<%#Eval("CourseID") %>" title="" class="lnk-default">View Details</a>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
