<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="need-help.aspx.cs" Inherits="school_web.Library_Admin.need_help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
      <style>
        .coml-lst-ul {
            margin: 7px 0px 0px 0px;
            padding: 0px;
            float: right;
        }

            .coml-lst-ul li {
                margin: 0px;
                padding: 0px;
                list-style-type: none;
            }

                .coml-lst-ul li a {
                    margin: 0px;
                    padding: 3px 6px;
                    background: #0d6efd;
                    color: #fff;
                    border-radius: 2px;
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
                <div class="breadcrumb-title pe-3">Complain</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Complain</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="hd_temp_id" runat="server" />
            <asp:HiddenField ID="hd_school_name" runat="server" />
            <asp:HiddenField ID="hd_school_id" runat="server" />
            <asp:HiddenField ID="hd_user_name" runat="server" />
            <asp:HiddenField ID="hd_branch" runat="server" />
            <div class="row">
                <div class="col-xl-2"></div>
                <div class="col-xl-8">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Create Complain"></asp:Label>
                    <ul class="coml-lst-ul">
                        <li>
                            <a href="complain-list.aspx">Complain List</a>
                        </li>
                    </ul>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Complain Remark<sup>*</sup></label>
                                        <asp:TextBox ID="txt_remark" runat="server" class="form-control" TextMode="MultiLine" Style="height: 80px"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Attachment<sup>*</sup></label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btn_save_attechment" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_save_attechment_Click" Style="margin: 24px 0px 0px 0px;" />
                                    </div>
                                    <div class="col-md-12" id="documentS" runat="server" visible="false">
                                        <label for="validationCustom01" class="form-label">Uploaded Attachment</label>
                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Attachment</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>

                                                                <td style="text-align: left;">
                                                                    <a href='<%# Eval("Documents") %>' download>View</a>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-8" style="display: none">
                    <h6 class="mb-0 text-uppercase">Complain List</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Request Id</th>
                                                        <th>Remarks</th>
                                                        <th>Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_views" runat="server">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_request_id" runat="server" Text='<%#Bind("Request_id")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("New_Request_date")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
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



    <script type="text/javascript">
        var app = angular.module('ComplainApp', []);
        app.controller('ComplainAppCtrl', function ($scope, $http) {

            //FindByOrderId 
            $scope.btnFndByOrderNo = function (AssignNo) {
                var paramiter1 = AssignNo;  
                $http.get("../Admin/graph.asmx/fetch_std_hostel_details", { params: { "Paramiter1": paramiter1 } }).then(function (response) {
                    $scope.hstlHistry = response.data;
                    //if ($scope.odrHistry == "") {
                    //    $("#orderHistoryTable").addClass("hidden");
                    //    $("#not_found").removeClass("hidden");
                    //}
                    //else
                    //{
                    //    $("#orderHistoryTable").removeClass("hidden");
                    //    $("#not_found").addClass("hidden");
                    //}
                })
            }
        });
    </script>
</asp:Content>
