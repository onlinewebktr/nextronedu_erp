<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="LMS_and_Payroll_Admin_Permission_For_Teacher.aspx.cs" Inherits="school_web.Admin.LMS_Admin_Permission_For_Teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Other Module Permission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 20px;
            height: 20px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <li class="breadcrumb-item active" aria-current="page">Other Module Permission</li>
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

                                            <table style="margin: 0px 0px 11px 0px; padding: 0px; float: left; height: auto; width: 100%">
                                                <tr>
                                                    <td style="font-weight: bold;">
                                                        <asp:CheckBox ID="chk_LMS_Admin" runat="server" Text="LMS Admin" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">
                                                        <asp:CheckBox ID="chk_payrolladmin" runat="server" Text="Payroll Admin" />
                                                    </td>
                                                </tr>
                                            </table>


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
                                            <asp:Panel ID="pnl_grid" runat="server" Visible="false">

                                                <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Menu Name
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rp_addedmenu" runat="server"  >
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="rowChkBox1" runat="server" Text='<%#Bind("Menu")%>'  Style="font-weight: bold;" />
                                                                         <asp:Label ID="lbl_Menu" runat="server" Text='<%#Bind("Menu")%>' Visible="false"></asp:Label>
                                                                    </td>


                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>

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

</asp:Content>
