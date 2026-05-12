<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Userpermissions.aspx.cs" Inherits="school_web.Admin.User_permissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Employee Permission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function myFunction(id) {
            var x = document.getElementById(id);
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }

    </script> 
    <script type="text/javascript">
        function mystyle(id) {

            var x = document.getElementById(id);

            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }

    </script>
    <style>
        .box-table td {
            padding: 0px;
        }

        td label {
            font-weight: 400;
            font-size: 14px;
            padding: 7px 6px;
            font-weight: 800;
            margin-top: -8px;
            position: absolute;
        }

        .checkbxDv2 input {
            width: 20px;
            height: 20px;
        }

        .checkbxDv1 input {
            width: 20px;
            height: 20px;
        }

        .checkbxDv {
            display: inline;
            position: relative;
            margin: 0px 7px 0px 0px
        }

            .checkbxDv label {
                width: 32px;
                z-index: 99;
                height: 32px;
                position: relative;
                margin: 0px 0px 0px 0px;
                padding: 0px;
            }

            .checkbxDv i {
                position: absolute;
                right: 0px;
                bottom: 4px;
                padding: 0px;
                margin: 0px;
                line-height: 26px;
            }


            .checkbxDv input {
                width: 25px;
                height: 25px;
            }

        .accordion {
            background-color: #428BCA !important;
            color: #fff7f7;
            cursor: pointer;
            padding: 5px;
            width: 100%;
            border: none;
            text-align: left;
            outline: none;
            font-size: 15px;
            transition: 0.4s;
            height: 30px;
            margin: 0px;
        }


        .panel {
            padding: 0px 0px;
            display: block;
            background-color: white;
            overflow: hidden;
            border: 1px solid #ccc;
            border-radius: 0px;
            padding: 0px 10px;
        }

        .waiting {
            padding: 15px 15px 15px 14px;
            font-size: 16px;
            bottom: 0px;
            left: 1px;
            top: 35%;
            background: #fff0;
            color: #1a1313;
            height: 55px !important;
            z-index: 1000;
            font-size: 17px;
            text-align: center;
            width: 99.8%;
            position: fixed;
        }

        .bx_add {
            background: #2b9300;
            font-size: 18px;
            color: #fff;
            padding: 0px 5px 0px 4px !important;
            border-radius: 3px;
        }

        .bx_edit {
            background: #1fc510;
            font-size: 18px;
            color: #fff;
            padding: 0px 5px 0px 4px !important;
            border-radius: 3px;
        }

        .bx_delete {
            background: #f50000;
            font-size: 18px;
            color: #fff;
            padding: 0px 5px 0px 4px !important;
            border-radius: 3px;
        }

        .bx_print {
            background: #0640ff;
            font-size: 18px;
            color: #fff;
            padding: 0px 5px 0px 4px !important;
            border-radius: 3px;
        }

        .bx_Download {
            background: #ff06d5;
            font-size: 18px;
            color: #fff;
            padding: 0px 5px 0px 4px !important;
            border-radius: 3px;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: fixed !important;
            z-index: 999;
            /* float: left; */
            width: 100%;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            var chkAll = $('.chkHeader').click(function () {
                //Check header and item's checboxes on click of header checkbox
                chk_menu.prop('checked', $(this).is(':checked'));
            });
            var chk_menu = $(".item").click(function () {
                //If any of the item's checkbox is unchecked then also uncheck header's checkbox
                chkAll.prop('checked', chkItem.filter(':not(:checked)').length == 0);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">


        <!--start page wrapper -->
        <div class="page-wrapper">
            <div class="page-content">
                <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                    <div class="breadcrumb-title pe-3">User</div>
                    <div class="ps-3">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb mb-0 p-0">
                                <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                                </li>
                                <li class="breadcrumb-item active" aria-current="page">Employee Permission</li>
                            </ol>
                        </nav>
                    </div>
                </div>

                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-12">
                                    <label for="validationCustom01" class="form-label">Employee</label>
                                    <div class="select2-dv">
                                        <asp:DropDownList ID="ddl_UserName" runat="server" CssClass="single-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_UserName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="HdID" runat="server" />
                        <div id="notification">
                            <div id="pan" class="notificationpan">

                                <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;"
                                    class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                                    <div class="d-flex align-items-center">
                                        <div class="font-35 text-white">
                                            <i class='bx bxs-check-circle'></i>
                                        </div>
                                        <div class="ms-3">
                                            <h6 class="mb-0 text-white">Success Alerts</h6>
                                            <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                                        </div>
                                    </div>
                                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                                </div>

                                <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                                    <div class="d-flex align-items-center">
                                        <div class="font-35 text-dark">
                                            <i class='bx bx-info-circle'></i>
                                        </div>
                                        <div class="ms-3">
                                            <h6 class="mb-0 text-dark">Warning Alerts</h6>
                                            <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                                        </div>
                                    </div>
                                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-6">
                                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;"
                                    class="mb-0 text-uppercase" Text="Allow Permission"></asp:Label>
                                <hr />
                                <div class="card">
                                    <div class="card-body">
                                        <div class="p-4 border rounded">
                                            <div class="row g-3 needs-validation" novalidate="">

                                                <div class="col-md-12">
                                                    <div class="clearfix">
                                                        <asp:Repeater ID="rep_menulist" runat="server" OnItemDataBound="rep_menulist_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div style="width: 100%; margin-bottom: 5px;">
                                                                    <h3 class="accordion">
                                                                        <span onclick="myFunction(<%#Eval("Group_id")%>)"><%#Eval("Group_name")%>
                                                                            <asp:Label ID="lblGroup_id" runat="server" Text='<%#Bind("Group_id") %>' Visible="false"></asp:Label></span>
                                                                        <span class="pull-right checkbxDv2">
                                                                            <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" /></span>
                                                                    </h3>
                                                                    <div class="panel" id='<%#Eval("Group_id")%>'>
                                                                        <asp:DataList ID="dl_menulist_submenu" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Style="width: 100%;" OnItemDataBound="dl_menulist_submenu_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <div style="margin: 0px; padding: 2px 2px 2px 2px; width: 100%; float: left">
                                                                                    <div style="margin: 0px; padding: 2px 2px 2px 2px; width: 100%; float: left"
                                                                                        class="checkbxDv1">
                                                                                        <asp:CheckBox ID="chk_menu" runat="server" Text='<%#Bind("Menu_name") %>' AutoPostBack="true" OnCheckedChanged="chk_menu_CheckedChanged" />
                                                                                        <asp:Label ID="lblmenu_id" runat="server" Text='<%#Bind("MenuID") %>' Visible="false"></asp:Label>
                                                                                    </div>

                                                                                    <asp:DataList ID="dl_chiled_menu_submenu" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Style="width: 100%;">
                                                                                        <ItemTemplate>

                                                                                             <asp:Label ID="lblchiled_menu_submenu" runat="server" Text='<%#Bind("Child_Menu_Add_edit_delete") %>' Visible="false"></asp:Label>
                                                                                            <div style="margin: 0px; padding: 0px 2px 2px 2px; width: 100%; float: left; border-bottom: 1px solid #00000070;"
                                                                                                id="pnl_tool1" runat="server" visible="true">
                                                                                                <div class="checkbxDv" title="Add">
                                                                                                    <asp:CheckBox ID="chk_add" runat="server" Text=" " />
                                                                                                    <i class='bx bxs-add-to-queue bx_add'></i>
                                                                                                </div>

                                                                                                <div class="checkbxDv" title="Edit">
                                                                                                    <asp:CheckBox ID="chk_edit_permission" runat="server" Text=" " />
                                                                                                    <i class="bx bx bx-edit bx_edit"></i>
                                                                                                </div>

                                                                                                <div class="checkbxDv" title="Delete">
                                                                                                    <asp:CheckBox ID="chk_delete" runat="server" Text=" " />
                                                                                                    <i class="bx bx-message-square-x bx_delete"></i>
                                                                                                </div>
                                                                                                <div class="checkbxDv" title="Download">
                                                                                                    <asp:CheckBox ID="chk_Download" runat="server" Text=" " />
                                                                                                    <i class="bx bx-download bx_Download"></i>
                                                                                                </div>
                                                                                                <div class="checkbxDv" title="Print">
                                                                                                    <asp:CheckBox ID="chk_print" runat="server" Text=" " />
                                                                                                    <i class="bx bxs-printer bx_print"></i>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>







                                                                                </div>

                                                                            </ItemTemplate>
                                                                        </asp:DataList>

                                                                        <div style="margin: 0px; padding: 0px 2px 2px 2px; width: 100%; float: left; border-bottom: 1px solid #00000070;"
                                                                            id="div_payroll_permission" runat="server" visible="false">

                                                                            <asp:Repeater ID="rp_payroll" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Header" class="chkstle" runat="server" Text='<%#Eval("Header") %>' />
                                                                                    <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Menu_Id")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                        <asp:Button ID="btn_allow_permission" runat="server" Text="Allow Permission" class="btn btn-primary" OnClick="btn_allow_permission_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:HiddenField ID="hdfID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdUserId" runat="server" Value="0" />
                            <div class="col-xl-6">
                                <h6 class="mb-0 text-uppercase">Working Permission List</h6>
                                <hr />
                                <div class="card">
                                    <div class="card-body">
                                        <div class="p-4 border rounded">
                                            <div class="row g-3 needs-validation" novalidate="">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnl_grid" runat="server">
                                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div style="width: 100%; margin-bottom: 5px;">
                                                                    <h3 class="accordion" onclick="mystyle(<%#Eval("Group_id")%><%#Eval("Group_id")%>)">
                                                                        <%#Eval("Group_name")%>
                                                                        <span class="pull-right checkbxDv2">
                                                                            <asp:CheckBox ID="chkHeaderRemove" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeaderRemove_CheckedChanged" /></span>
                                                                        <br></br>
                                                                        <asp:Label ID="lblGroup_id" runat="server" Text='<%#Bind("Group_id") %>' Visible="false"></asp:Label>


                                                                    </h3>
                                                                    <div class="panel" id='<%#Eval("Group_id")%><%#Eval("Group_id")%>'>
                                                                        <asp:DataList ID="dl_menulist" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" OnItemDataBound="dl_menulist_ItemDataBound">
                                                                            <ItemTemplate>


                                                                                <div style="margin: 0px; padding: 2px 2px 2px 2px; width: 100%; float: left"
                                                                                    class="checkbxDv1">
                                                                                    <asp:CheckBox ID="chk_menu_permission_granted" runat="server" Text='<%#Bind("Menu_name") %>' AutoPostBack="true" OnCheckedChanged="chk_menu_permission_granted_CheckedChanged" />
                                                                                    <asp:Label ID="lblmenu_id" runat="server" Text='<%#Bind("MenuID") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_MainMenuId" runat="server" Text='<%#Bind("MainMenuId") %>' Visible="false"></asp:Label>
                                                                                </div>

                                                                                    <asp:DataList ID="dl_chiled_menu_submenu_user_permission" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Style="width: 100%;" OnItemDataBound="dl_chiled_menu_submenu_user_permission_ItemDataBound">
                                                                                        <ItemTemplate>

                                                                                             <asp:Label ID="Is_add" runat="server" Text='<%#Bind("Is_add") %>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="Is_Edit" runat="server" Text='<%#Bind("Is_Edit") %>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="Is_delete" runat="server" Text='<%#Bind("Is_delete") %>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="Is_Download" runat="server" Text='<%#Bind("Is_Download") %>' Visible="false"></asp:Label>
                                                                                              <asp:Label ID="lbl_Is_Print" runat="server" Text='<%#Bind("Is_Print") %>' Visible="false"></asp:Label>

                                                                                            <div style="margin: 0px; padding: 0px 2px 2px 2px; width: 100%; float: left; border-bottom: 1px solid #00000070;"
                                                                                                id="pnl_tool1" runat="server" visible="true">
                                                                                                <div class="checkbxDv" title="Add" id="add" runat="server">
                                                                                                    <asp:CheckBox ID="chk_add" runat="server" Text=" " />
                                                                                                    <i class='bx bxs-add-to-queue bx_add'></i>
                                                                                                </div>

                                                                                                <div class="checkbxDv" title="Edit" id="Edit" runat="server">
                                                                                                    <asp:CheckBox ID="chk_edit_permission" runat="server" Text=" " />
                                                                                                    <i class="bx bx bx-edit bx_edit"></i>
                                                                                                </div>

                                                                                                <div class="checkbxDv" title="Delete" id="Delete" runat="server">
                                                                                                    <asp:CheckBox ID="chk_delete" runat="server" Text=" " />
                                                                                                    <i class="bx bx-message-square-x bx_delete"></i>
                                                                                                </div>
                                                                                                <div class="checkbxDv" title="Download" id="Download" runat="server">
                                                                                                    <asp:CheckBox ID="chk_Download" runat="server" Text=" " />
                                                                                                    <i class="bx bx-download bx_Download"></i>
                                                                                                </div>
                                                                                                <div class="checkbxDv" title="Print" id="Print" runat="server">
                                                                                                    <asp:CheckBox ID="chk_print" runat="server" Text=" " />
                                                                                                    <i class="bx bxs-printer bx_print"></i>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>


                                                                            </ItemTemplate>
                                                                        </asp:DataList>

                                                                        <div style="margin: 0px; padding: 10px 2px 2px 2px; width: 100%; float: left; border-bottom: 1px solid #00000070;"
                                                                            id="div_payroll_permission_user" runat="server" visible="false">
                                                                            <asp:Repeater ID="rp_payroll_user" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Header" class="chkstle" runat="server" Text='<%#Eval("Header") %>' />
                                                                                    <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Menu_Id")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:Button ID="btn_remove" runat="server" Text="Remove Permission" class="btn btn-primary" OnClick="btn_remove_Click" />
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress2"
                    runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                    DynamicLayout="False">
                    <ProgressTemplate>
                        <p class="waiting">
                            &nbsp;&nbsp;&nbsp;
                                            <img src="../images/Processing.gif" />

                        </p>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <!--end row-->
        </div>
    </div>
    <!--end page wrapper -->
</asp:Content>
