<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Edit_Permission_Student_List.aspx.cs" Inherits="school_web.LMS_VC_Admin.Edit_Permission_Student_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Edit Student List (Granted Permission)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: -59.4px!important;
        }
    </style>
    <style>
        .highlightRow {
            background-color: #c5c5ff !important;
        }
    </style>

    <script>
        $(function () {
            $(".trclass").click(function () {
                $(this).addClass("highlightRow").siblings().removeClass("highlightRow");
            });
        });
    </script>
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
                        <asp:Literal ID="ltUsertop" runat="server"> Edit Student List (Granted Permission)</asp:Literal>

                    </div>
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
                    <div class="col-md-2" style="display: none">
                        <div class="position-relative form-group">
                            <label>Select Class</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_CourseCat1" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" style="display: none">
                        <asp:Button ID="btn_clear" runat="server" CssClass="btn btn-danger" Text="Delete Data" Style="margin-top: 30px" OnClick="btn_clear_Click" OnClientClick="return confirm('Are you sure want to delete data ?')" />
                    </div>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Student Name</th>
                                    <th>Father's Name</th>

                                    <th style="width: 111px;">Admission No.</th>
                                    <th style="width: 111px;">Admission Date</th>
                                    <th>Class</th>
                                    <th>Roll No.</th>
                                    <th>Section</th>
                                    <th>Session</th>
                                    <th>Gender</th>
                                    <th>Mobile No.</th>
                                    <th>Password</th>
                                    <th>Status</th>

                                    <th>Edit Status</th>
                                    <th>Action</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="trclass" id="row" runat="server">
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
                                                <asp:Label ID="lbl_gender" runat="server" Font-Names="Arial" Text='<%#Bind("gender") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_mobile" runat="server" Font-Names="Arial" Style="word-break: break-all" Text='<%#Bind("mobilenumber") %>'></asp:Label>

                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Password" runat="server" Font-Names="Arial" Text='<%#Bind("Pwd") %>'></asp:Label>
                                                <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_iStatus" runat="server" Font-Names="Arial" Text='<%#Bind("Status") %>' Visible="false"></asp:Label>

                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_edit_status" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_edit_Istatus" runat="server" Font-Names="Arial" Text='<%#Bind("Edit_Istatus") %>' Visible="false"></asp:Label>

                                            </td>


                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">

                                                         
                                                            <a id="popup1" href='Student_Full_Details.aspx?regid=<%#Eval("admissionserialnumber")%>' style="color: #000" target="_blank" class="dropdown-item"><i class="dropdown-icon lnr lnr-eye"></i><span>View</span></a>


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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
