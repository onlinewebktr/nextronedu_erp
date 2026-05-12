<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="month-setting-for-monthly-fee.aspx.cs" Inherits="school_web.Admin.month_setting_for_monthly_fee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
   Custom Month Selection For Monthly Fee
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Custom Month Selection For Monthly Fee</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Custom Month Selection For Monthly Fee  <a href="custom-month-selection-list.aspx" cssclass="btn btn-primary form-fnd-btns" style="background: #f00; border: 1px solid #ddd; width: auto; margin: -3px 0px -2px 0px; padding: 2px 10px 3px; float: right; color: #fff; border-radius: 3px;">View</a></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Month</th>
                                                        <th>No. of Month Selection</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_month_position" Visible="false" runat="server" Text='<%#Bind("Position")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_month_id" Visible="false" runat="server" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:DropDownList ID="ddl_no_of_month" AutoPostBack="true" OnSelectedIndexChanged="ddl_no_of_month_SelectedIndexChanged" Style="width: 100px;" class="form-select" runat="server"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>


                                            <div class="form-group col-xs-10 col-sm-3 col-md-12 col-lg-12">
                                                <asp:Button ID="btn_reset" OnClick="btn_reset_Click" runat="server" class="btn btn-sm btn-danger" Text="Reset" Style="margin: 0px 0px 0px 0px; padding: 6px 10px 8px; float: right;" />
                                                <asp:Button ID="btn_save" OnClick="btn_save_Click" runat="server" class="btn btn-sm btn-success" Text="Save" Style="margin: 0px 10px 0px 0px; padding: 6px 10px 8px; float: right;" />
                                            </div>
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
