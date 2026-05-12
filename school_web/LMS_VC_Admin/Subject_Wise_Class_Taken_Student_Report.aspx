<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Subject_Wise_Class_Taken_Student_Report.aspx.cs" Inherits="school_web.LMS_VC_Admin.Subject_Wise_Class_Taken_Student_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-display1 icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Student Attendence Reprot</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                     <asp:LinkButton ID="lnk_back" OnClick="lnk_back_Click" class="btn-shadow btn btn-info" runat="server"></asp:LinkButton>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="row">

            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        

                        <asp:Panel ID="pnl_view" runat="server" Visible="false">
                            <h5 class="card-title">
                            <asp:Label ID="lbl_month_year" runat="server" Style="color: #f81b1b;"></asp:Label>&nbsp; | Total Class Attended:-<asp:Label ID="lbl_total" runat="server" Style="color: #f81b1b;"></asp:Label>

                            </h5>


                            <hr />
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>

                                    <tr>

                                        <th>Sl No.</th>
                                        <th>Student Name</th>
                                         
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Subject</th>
                                        <th>Teacher</th>
                                        <th>Date</th>
                                        <th>Class Start Time</th>
                                        <th>Class Joining Time</th>
                                         

                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RpDetailsStudent" runat="server" OnItemDataBound="RpDetailsStudent_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>

                                                <td>
                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                                </td>
                                                 
                                                <td>
                                                    <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_CourseName" runat="server" Text='<%#Bind("CourseName") %>'></asp:Label>
                                                </td>

                                                 <td>
                                                    <asp:Label ID="lbl_teacherName" runat="server" Text='<%#Bind("teachername") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                </td>

                                               





                                                <td>
                                                    <asp:Label ID="lbl_Meeting_start_at" runat="server" Text='<%#Bind("Meeting_start_at","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>

                                                  <td>
                                                    <asp:Label ID="lbl_metingstartinme" runat="server"  ></asp:Label>

                                                       <asp:Label ID="lbl_userid" runat="server" Text='<%#Bind("User_id") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lbl_end_time" runat="server" Text='<%#Bind("End_Time","{0:hh:mm:ss tt}") %>' Visible="false"></asp:Label>
                                                </td>


                                                


                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
