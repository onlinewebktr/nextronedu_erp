<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Send_Message_to_student.aspx.cs" Inherits="school_web.LMS_VC_Admin.Send_Message_to_student" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Send Message To Students
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .input-group {
            position: relative;
            display: flex;
            flex-wrap: wrap;
            align-items: stretch;
            width: 100%;
            margin: 11px 0px 0px 0px;
        }

        .dt-button-collection {
            margin-top: 9.6px!important;
        }
    </style>
    <script type="text/javascript">
        function checkAllRows(obj) {

            var objGridview = obj.parentNode.parentNode.parentNode;
            var list = objGridview.getElementsByTagName("input");

            for (var i = 0; i < list.length; i++) {
                var objRow = list[i].parentNode.parentNode;
                if (list[i].type == "checkbox" && obj != list[i]) {
                    if (obj.checked) {

                        //If the header checkbox is checked then check all 
                        //checkboxes and highlight all rows.

                        objRow.style.backgroundColor = "#0baf36";
                        //objRow.style.Color = "#271515";
                        list[i].checked = true;
                    }
                    else {
                        objRow.style.backgroundColor = "#FFFFFF";
                        //objRow.style.Color = "#271515";

                        list[i].checked = false;
                    }
                }
            }
        }
        function checkUncheckHeaderCheckBox(obj) {
            var objRow = obj.parentNode.parentNode;

            if (obj.checked) {
                objRow.style.backgroundColor = "#0baf36";
                objRow.style.Color = "#fff";
            }
            else {
                objRow.style.backgroundColor = "#FFFFFF";
                //objRow.style.Color = "#271515";
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Send Message To Students</asp:Literal>

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
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Student List</h5>
                        <div class="form-row">
                            <div class="col-md-2">
                                <label>Select Class</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddl_CourseCat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th class="allCheckbox">
                                        <asp:CheckBox ID="hdrChkBox" runat="server" />
                                    </th>
                                    <th>Student Name</th>
                                    <th>Father's Name</th>
                                    <th>Mobile</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td class="singleCheckbox">
                                                <asp:CheckBox ID="rowChkBox" runat="server" />
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("Original_Name") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_fathername" runat="server" Font-Names="Arial" Text='<%#Bind("fathername") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_mobile" runat="server" Font-Names="Arial" Text='<%#Bind("mobilenumber") %>'></asp:Label>
                                                <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>


                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Write Your Message</h5>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:TextBox ID="txt_message" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                            </div>
                            <div class="input-group">
                                <label>Message Type<sup>*</sup></label>
                                <asp:RadioButton ID="rd_english" runat="server" Text="English" GroupName="aa" />
                                <asp:RadioButton ID="rd_hndi" runat="server" Text="Hindi" GroupName="aa" />
                            </div>
                            <div class="card-footer pull-right">
                                <asp:Button ID="btn_submit" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
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
</asp:Content>
