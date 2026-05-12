<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Add_User_for_live_class.aspx.cs" Inherits="school_web.LMS_VC_Admin.Add_User_for_live_class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: 7.6px!important;
        }

        .table th, .table td {
            box-sizing: border-box;
            text-align: center;
        }

        input[type="radio"], input[type="checkbox"] {
            box-sizing: border-box;
            padding: 0;
            font-size: 28px;
            height: 20px!important;
            width: 20px!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Create User</asp:Literal>
                    </div>
                </div>


                <div class="page-title-actions" style="display: none">
                    <a href="Exam_List.aspx" class="btn-shadow btn btn-info">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        All Exam List
                    </a>
                </div>

            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hd_regid" runat="server" />
        <div class="row">

            <div class="col-lg-5">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">
                            <asp:Literal ID="ltUser" runat="server"></asp:Literal></h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group" >
                                    <label style="font-weight: bold;margin-top: 13px;">Designation<sup>**</sup></label>
                                    <asp:DropDownList ID="ddl_Designation" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label style="font-weight: bold">Name<sup>**</sup></label>
                                    <asp:TextBox ID="txt_name" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_name" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label style="font-weight: bold">Mobile No.<sup>**</sup></label>
                                    <asp:TextBox ID="txt_mobileno" runat="server" CssClass="form-control" placeholder="Mobile No." onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_mobileno" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>



                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label style="font-weight: bold">User Id<sup>**</sup></label>
                                    <asp:TextBox ID="txt_usrid" runat="server" CssClass="form-control" placeholder="User Id"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_usrid" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label style="font-weight: bold">Password<sup>**</sup></label>
                                    <asp:TextBox ID="txt_password" runat="server" CssClass="form-control" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_password" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <table border="0" style="width: 98%; margin-right: 2%;" class="table">
                                        <tr>
                                            <td style="font-weight: bold; text-align: left;">Class Info</td>
                                        </tr>
                                        <tr>
                                            <td style="border-top: 1px solid #000;">
                                                <asp:GridView ID="grd_class" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin: 0px 0px 2px 0px;"
                                                    CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" OnRowDataBound="grd_class_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                                                <asp:Label ID="lbl_CategoryID" runat="server" Text='<%#Bind("course_id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="hdrChkBox" runat="server" onClick="checkAllRows(this);" Text="All" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="rowChkBox" Style="font-size: 18px;" runat="server" onClick="checkUncheckHeaderCheckBox(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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

                                                                    objRow.style.backgroundColor = "#90ee90";
                                                                    objRow.style.Color = "#fff";
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
                                                            objRow.style.backgroundColor = "#90ee90";
                                                            objRow.style.Color = "#fff";
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
                                            </td>
                                        </tr>
                                    </table>



                                </div>
                            </div>


                        </div>




                        <div class="form-row">
                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="mt-2 btn btn-danger" OnClick="btn_cancel_Click" Visible="false" CausesValidation="false" />
                            <asp:Button ID="btn_Submit" runat="server" Text="Create" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" ValidationGroup="a" Style="float: right" />

                        </div>

                    </div>
                </div>
            </div>

            <div class="col-lg-7">
                <div class="main-card mb-3 card">
                    <div class="card-body">

                        <asp:HiddenField ID="HdID" runat="server" />
                        <h5 class="card-title">Added User  </h5>


                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl. No.</th>
                                    <th>Name</th>
                                    <th>Mobile No.</th>
                                    <th>Designation Name </th>
                                    <th>User Id</th>
                                    <th>Passowrd</th>
                                    <th>Action</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:HiddenField ID="hdUserID" runat="server" />
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Mobile_no" runat="server" Text='<%#Bind("Mobile_no") %>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lbl_Designation_Name" runat="server" Text='<%#Bind("Designation_Name") %>'></asp:Label>
                                                <asp:Label ID="lbl_desgnatioid" runat="server" Text='<%#Bind("Designation_id") %>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_User_Id" runat="server" Text='<%#Bind("User_Id") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Passowrd" runat="server" Text='<%#Bind("Passowrd") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click" CssClass="dropdown-item"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDel" runat="server" CssClass="dropdown-item" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete this user?');" CausesValidation="false">Delete</asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_view" runat="server" Text="Class Mapped" OnClick="lnk_view_Click" CssClass="dropdown-item"></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
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

    <div class="modal fade" id="Details" tabindex="-1" role="dialog" aria-labelledby="DetailsLabel" aria-hidden="true">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="DetailsLabel">User Mapped with Class </h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="scroll-area-lg">
                    <div class="scrollbar-container">
                        <div class="modal-body">
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 120px; text-align:left;">Name :</td>
                                    <td style="font-weight: bold;text-align:left;" colspan="4">
                                        <asp:Label ID="lbl_username" runat="server"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td style="width: 120px;text-align:left;">Designation  :</td>
                                    <td style="font-weight: bold;text-align:left;" colspan="4">
                                        <asp:Label ID="lblDesignation" runat="server"></asp:Label></td>

                                </tr>


                            </table>

                            <div class="col-lg-12">
                                <table class="table table-bordered">
                                    <tbody class="tblBox">
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grd_view" runat="server" ShowFooter="false" AutoGenerateColumns="false" Width="100%" Style="text-align: center;" Visible="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script>
        function SHow() {
            $("#Details").show();
            $('#Details').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#Details").hide();
            $('#Details').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>
</asp:Content>
