<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dues_Sync_List.aspx.cs" Inherits="school_web.LMS_VC_Admin.Dues_Sync_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Dues Sync List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Dues Sync List</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                    <a href="Add_student.aspx" class="btn-shadow btn btn-info" style="display: none">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        Add Student
                    </a>
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
        <div class="clearfix"></div>
        <div class="main-card mb-3 card">
            <div class="card-body">
                <div class="form-row">
                    <div class="col-md-2">
                        <div class="position-relative form-group">
                            <label>Select Class</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_CourseCat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="position-relative form-group">
                            <label>Student Section Wise.</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:TextBox ID="txt_student_regid" runat="server" CssClass="form-control" Style="float: left; width: 50%; display: none"></asp:TextBox>
                                <asp:Button ID="btn_section_wise" runat="server" CssClass="btn btn-primary" Text="Find" OnClick="btn_section_wise_Click" Style="float: left; width: auto; margin: 5px 0px 0px 13px;" />
                                <asp:Button ID="btn_find_dtudent_regid" runat="server" CssClass="btn btn-primary" Visible="false" Text="Find" OnClick="btn_find_dtudent_regid_Click" Style="float: left; width: auto; margin: 5px 0px 0px 13px;" />
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Visible="true" Text="Reset" OnClick="Button1_Click" Style="float: left; width: auto; margin: 5px 0px 0px 13px;" />
                            </div>
                        </div>

                    </div>
                   

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                           <h5 class="card-title">
                                  No. of Dues Sync:-<asp:Label ID="lbl_total" runat="server" Style="color: #f81b1b;"></asp:Label>

                            </h5>


                            <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Student Name</th>
                                    <th>Father's Name</th>

                                    <th style="width: 137px;">Admission No.</th>
                                    <th style="width: 137px;">Admission Date</th>
                                    <th style="width: 100px;">Class</th>
                                    <th style="width: 100px;">Roll No.</th>
                                    <th>Section</th>
                                    <th style="width: 100px;">Session</th>
                                    
                                    <th>Mobile</th>
                                    
                                     
                                     
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server"  >
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_fathername" runat="server" Font-Names="Arial" Text='<%#Bind("fathername") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_dateofadmission" runat="server" Font-Names="Arial" Text='<%#Bind("dateofadmission") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_class" runat="server" Font-Names="Arial" Text='<%#Bind("class") %>'></asp:Label>
                                                <asp:Label ID="lbl_Class_id" runat="server" Font-Names="Arial" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_session" runat="server" Font-Names="Arial" Text='<%#Bind("session") %>'></asp:Label>
                                            </td>
                                            
                                            <td>
                                                <asp:Label ID="lbl_mobile" style="word-break:break-all" runat="server" Font-Names="Arial" Text='<%#Bind("mobilenumber") %>'></asp:Label>

                                            </td>

                                            
                                             
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
