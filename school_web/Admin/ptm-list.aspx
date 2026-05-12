<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="ptm-list.aspx.cs" Inherits="school_web.Admin.ptm_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    PTM List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <style>
        .fade:not(.show) {
            opacity: 1;
        }

        .modal-dialog {
            max-width: 700px;
            top: 30px;
        }

        .modal {
            background: rgb(0 0 0 / 31%);
        }
    </style>
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
                            <li class="breadcrumb-item active" aria-current="page">Add Category</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row" data-ng-app="ComplainApp" data-ng-controller="ComplainAppCtrl">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">PTM List</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Teacher List</label>
                                                        <asp:DropDownList ID="ddl_teacher" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th></th>
                                                        <th>Created By</th>
                                                        <th>Meeting Date</th>
                                                        <th>Meeting Id</th>
                                                        <th>Topic</th>
                                                        <th>Duration</th>
                                                        <th>Start Time</th>
                                                        <th>End Time</th>
                                                        <th>Class</th>
                                                        <th>Section</th>
                                                        <th>Approved By</th>
                                                        <th>Created On</th>
                                                        <th>Approved On</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <a href="#!" data-toggle="modal" data-target="#myModal" data-ng-click="btnFndByMeetiD('<%# Eval("Zoom_Meeting_id") %>')" style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; border-radius: 2px; font-weight: 500; width: 83px; width: 87px; float: left; font-size: 13px;">View Attend</a>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("Teacher_name")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("Zoom_Meeting_id")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("Topic")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label13" runat="server" Text='<%#Bind("Duration")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label7" runat="server" Text='<%#Bind("Start_Time")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_End_Time" runat="server" Text='<%#Bind("End_Time")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label15" runat="server" Text='<%#Bind("Class_name")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label16" runat="server" Text='<%#Bind("section")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("Approved_By")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Bind("Approved_On_date")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Bind("CreatedOn_date")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label14" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
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
                    </div>




                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 3px 17px;">
                                    <h5 class="modal-title">Join Student Details</h5>
                                    <a href="#!" data-dismiss="modal" class="btn btn-secondary">Close</a>
                                </div>
                                <div class="modal-body">
                                    <div style="width: 100%; margin: 0px; padding: 0px">
                                        <table class="table table-striped table-bordered dataTable" style="width: 100%">
                                            <tr>
                                                <th>#</th>
                                                <th>Session</th>
                                                <th>Admission No.</th>
                                                <th>Class</th>
                                                <th>Section</th>
                                                <th>Roll Number</th>
                                                <th>Student Name</th>
                                            </tr>
                                            <tr data-ng-repeat="x in meets">
                                                <td>{{$index+1}}</td>
                                                <td>{{x.Session}}</td>
                                                <td>{{x.Admission_no}}</td>
                                                <td>{{x.Class_name}}</td>
                                                <td>{{x.Section}}</td>
                                                <td>{{x.Rollnumber}}</td>
                                                <td>{{x.Studentname}}</td>
                                            </tr>
                                        </table>
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


    <script type="text/javascript">
        var app = angular.module('ComplainApp', []);
        app.controller('ComplainAppCtrl', function ($scope, $http) {

            //FindByOrderId 
            $scope.btnFndByMeetiD = function (Meet_id) {
                // alert(Meet_id);
                $http.get("chat.asmx/fetch_meeting_history", { params: { "Request_id": Meet_id } }).then(function (response) {
                    $scope.meets = response.data;
                })
            }
        });
    </script>
</asp:Content>
