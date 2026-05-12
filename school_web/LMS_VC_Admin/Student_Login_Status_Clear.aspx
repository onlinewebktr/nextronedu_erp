<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Student_Login_Status_Clear.aspx.cs" Inherits="school_web.LMS_VC_Admin.Student_Login_Status_Clear" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Login Status Clear
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: -59.4px!important;
        }
    </style>
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
                        <asp:Literal ID="ltUsertop" runat="server">  Student Login Status Clear</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions" style="display: none">
                    <a href="Add_student.aspx" class="btn-shadow btn btn-info">
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
                            <label>Student Admission No.</label>
                            <div class="input-group input-group-icon">
                                <asp:TextBox ID="txt_student_regid" runat="server" CssClass="form-control" Style="float: left; width: 50%;"></asp:TextBox>

                                <asp:Button ID="btn_find_dtudent_regid" runat="server" CssClass="btn btn-primary" Text="Find" OnClick="btn_find_dtudent_regid_Click" Style="float: left; width: auto; margin: 5px 0px 0px 13px;" />
                            </div>
                        </div>

                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
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
                                    <th>Sl. No.</th>

                                    <th>Student Name</th>
                                    <th>Admission No.</th>
                                    <th>Class</th>
                                    <th>Roll No.</th>
                                    <th>Section</th>
                                   
                                    <th>Gender</th>
                                    <th>Mobile</th>
                                    <th>Password</th>
                                    <th>Status</th>
                                    <th>Action</th>
                                    <th class="allCheckbox" style="width: 86px;">
                                        <asp:CheckBox ID="hdrChkBox" runat="server" />Check All
                                    </th>
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
                                                <asp:Label ID="lbl_class" runat="server" Font-Names="Arial" Text='<%#Bind("class") %>'></asp:Label>
                                                <asp:Label ID="lbl_Class_id" runat="server" Font-Names="Arial" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            
                                            <td>
                                                <asp:Label ID="lbl_gender" runat="server" Font-Names="Arial" Text='<%#Bind("gender") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_mobile" runat="server" Font-Names="Arial" Text='<%#Bind("mobilenumber") %>' style="word-break:break-all"></asp:Label>

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
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">

                                                            <asp:LinkButton ID="lnk_Clear_login" runat="server" CssClass="dropdown-item" OnClick="lnk_Clear_login_Click" OnClientClick='return confirm("Are you sure want to clear login ?")'><i class="dropdown-icon lnr lnr-lock"></i><span>Clear Login</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_mothername" Visible="false"  runat="server" Font-Names="Arial" Text='<%#Bind("mothername") %>'></asp:Label>
                                                            <asp:LinkButton ID="btn_active" runat="server" CssClass="dropdown-item" OnClick="btn_active_Click" Visible="false"></asp:LinkButton>
                                                         <asp:Label ID="lbl_session" runat="server" Font-Names="Arial" Text='<%#Bind("session") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td class="singleCheckbox" style="width: 43px!important;">
                                                <asp:CheckBox ID="rowChkBox" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td style="text-align:right" colspan="13">
                                             <asp:Button ID="btn_Clear_Login" runat="server" class="btn btn-primary" Text="Clear Login" OnClick="btn_Clear_Login_Click" />
                                    </td>
                                </tr>
                            </tfoot>
                        </table>

                        <div class="card-footer">
                            <div style="margin: 9px 0px 0px 0px; float: right; width: 100%;text-align: right;">
                           
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            var $allCheckbox = $('.allCheckbox :checkbox');
            var $checkboxes = $('.singleCheckbox :checkbox');
            $allCheckbox.change(function () {
                if ($allCheckbox.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox.removeAttr('checked');
                }
                else {
                    $allCheckbox.attr('checked', 'checked');
                }
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
        function checkAllRows(obj) {

            var objGridview = obj.parentNode.parentNode.parentNode;
            var list = objGridview.getElementsByTagName("input");

            for (var i = 0; i < list.length; i++) {
                var objRow = list[i].parentNode.parentNode;
                if (list[i].type == "checkbox" && obj != list[i]) {
                    if (obj.checked) {

                        //If the header checkbox is checked then check all 
                        //checkboxes and highlight all rows.

                        objRow.style.backgroundColor = "#99E5E5";
                        list[i].checked = true;
                    }
                    else {
                        objRow.style.backgroundColor = "#FFFFFF";
                        list[i].checked = false;
                    }
                }
            }
        }

        function checkUncheckHeaderCheckBox(obj) {
            var objRow = obj.parentNode.parentNode;

            if (obj.checked) {
                objRow.style.backgroundColor = "#99E5E5";
            }
            else {
                objRow.style.backgroundColor = "#FFFFFF";
            }
            var objGridView = objRow.parentNode;

            //Get all input elements in Gridview
            var list = objGridView.getElementsByTagName("input");
            for (var i = 0; i < list.length; i++) {
                var objHeaderChkBox = list[0];

                //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                var checked = true;

                if (list[i].type == "checkbox" && list[i] != objHeaderChkBox) {
                    if (!list[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            objHeaderChkBox.checked = checked;
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
