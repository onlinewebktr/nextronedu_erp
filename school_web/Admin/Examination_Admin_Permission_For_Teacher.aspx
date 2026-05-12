<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Examination_Admin_Permission_For_Teacher.aspx.cs" Inherits="school_web.Admin.Examination_Admin_Permission_For_Teacher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Examination Module Permission 
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
        }

        .accordion {
            background-color: #428BCA!important;
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
      <!--start page wrapper -->
    <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">

                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">User Permission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Examination Module Permission </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-6">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Allow Permission"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Employee Name</label>
                                        <asp:DropDownList ID="ddl_UserName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_UserName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="control-label col-xs-12 col-sm-12 no-padding-right" style="text-align: left" for="password">Select Menu for User:</label>
                                        <div class="clearfix">
                                            <asp:Repeater ID="rep_menulist" runat="server" OnItemDataBound="rep_menulist_ItemDataBound">
                                                <ItemTemplate>
                                                    <div style="width: 100%; margin-bottom: 5px;">
                                                        <h3 class="accordion">
                                                            <span onclick="myFunction(<%#Eval("Group_id")%>)"><%#Eval("Group_name")%>
                                                                <asp:Label ID="lblGroup_id" runat="server" Text='<%#Bind("Group_id") %>' Visible="false"></asp:Label></span>
                                                            <span class="pull-right">
                                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" /></span>
                                                        </h3>
                                                        <div class="panel" id='<%#Eval("Group_id")%>'>
                                                            <asp:DataList ID="dl_menulist" runat="server" RepeatColumns="1" RepeatDirection="Horizontal">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_menu" runat="server" Text='<%#Bind("Menu_name") %>' AutoPostBack="true" OnCheckedChanged="chk_menu_CheckedChanged" />
                                                                    <asp:Label ID="lblmenu_id" runat="server" Text='<%#Bind("MenuID") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:DataList>
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
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Panel ID="pnl_grid" runat="server">
                                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div style="width: 100%; margin-bottom: 5px;">
                                                            <h3 class="accordion" onclick="mystyle(<%#Eval("Group_id")%><%#Eval("Group_id")%>)">
                                                                <%#Eval("Group_name")%><br></br>
                                                                <asp:Label ID="lblGroup_id" runat="server" Text='<%#Bind("Group_id") %>' Visible="false"></asp:Label>
                                                            </h3>
                                                            <div class="panel" id='<%#Eval("Group_id")%><%#Eval("Group_id")%>'>
                                                                <asp:DataList ID="dl_menulist" runat="server" RepeatColumns="1" RepeatDirection="Horizontal">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_menu" runat="server" Text='<%#Bind("Menu_name") %>' />
                                                                        <asp:Label ID="lblmenu_id" runat="server" Text='<%#Bind("MenuID") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
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
        </div>
        <!--end row-->
    </div>

    <!--end page wrapper -->
</asp:Content>
