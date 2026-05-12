<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Homework_replay.aspx.cs" Inherits="school_web.InstructorProfile.Homework_replay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Homework Reply Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-note icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Homework Class Wise</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
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
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">

                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th colspan="11">
                                        <p style="width: auto; float: left; background-color: #1b2dfe; padding: 4px 9px 0px 0px; margin: 0px 5px 0px 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                            <asp:RadioButton ID="rd_all" runat="server" Checked="true" GroupName="qw" Text="All" OnCheckedChanged="rd_all_CheckedChanged" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                        </p>
                                        <p style="width: auto; float: left; background-color: #eb920c; padding: 4px 9px 0px 0px; margin: 0px 5px 0px 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                            <asp:RadioButton ID="rd_Replied" runat="server"  GroupName="qw" Text="Replied" OnCheckedChanged="rd_Replied_CheckedChanged" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                        </p>
                                    
                                        <p style="width: auto; float: left; background-color: #e50800; padding: 4px 9px 0px 0px; margin: 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                            <asp:RadioButton ID="rd_no_replyed" runat="server" GroupName="qw" Text="No Reply" OnCheckedChanged="rd_no_replyed_CheckedChanged" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                        </p>
                                            <p style="width: auto; float: left; background-color: #29a700; padding: 4px 9px 0px 0px; margin: 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                            <asp:RadioButton ID="rd_Checked" runat="server" GroupName="qw" Text="Checked" OnCheckedChanged="rd_Checked_CheckedChanged" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                        </p>
                                        <span style="float: right">
                                            <p style="width: auto; float: left; background-color: #1b2dfe; padding: 3px;color:#fff; margin: 0px 3px; font-weight: 500;">
                                                Total Student : 
                                    <asp:Label ID="lbl_total_data" runat="server" Text="Label"></asp:Label>
                                            </p>
                                            <p style="width: auto; float: left; background-color: #eb920c; padding: 3px;color:#fff; margin: 0px 3px; font-weight: 500;">
                                                Total Replied : 
                                    <asp:Label ID="lbl_replied" runat="server" Text="Label"></asp:Label>
                                            </p>
                                           
                                           
                                             <p style="width: auto; float: left; background-color: #e50800; color:#fff; padding: 3px; margin: 0px 0px 0px 7px!important; font-weight: 500;">
                                                Total Not Replied : 
                                    <asp:Label ID="lbl_notreplyed" runat="server" Text="Label"></asp:Label>
                                            </p>

                                              <p style="width: auto; float: left; background-color: #29a700; padding: 3px;color:#fff; margin: 0px 3px; font-weight: 500;">
                                                Total Checked : 
                                    <asp:Label ID="lbl_Checked" runat="server" Text="Label"></asp:Label>
                                            </p>
                                        </span>
                                    </th>
                                </tr>
                                <tr>
                                    <th>Sl. No.</th>
                                    <th>Student Name</th>
                                    <th>Admission No</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Roll No.</th>
                                    <th> Upload Date</th>
                                    <th>Completion Date</th>
                                    
                                    <th>Reply Date</th>
                                    <th>Reply Status</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CategoryName" runat="server" Font-Names="Arial" Text='<%#Bind("classname") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_homeworkuploaddate" runat="server" Font-Names="Arial" Text='<%#Bind("Upload_Date") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Text='<%#Bind("CompletingDate") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_studentreplydate" runat="server" Font-Names="Arial" Text='<%#Bind("replydate_student") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_replystatus" runat="server" Font-Names="Arial" Text='<%#Bind("Status") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">

                                                            <asp:LinkButton ID="lnk_view" runat="server" CssClass="dropdown-item" OnClick="lnk_view_Click"><i class="dropdown-icon lnr-inbox"></i><span>Check Now</span></asp:LinkButton>



                                                        </div>
                                                    </div>
                                                </div>


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
    <asp:HiddenField ID="hd_homeworkid" runat="server" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
