<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="finance-dashboard.aspx.cs" Inherits="school_web.Admin.finance_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Finance Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    
    <style>
        .dataTables_length {
            display: none;
        }

        .dataTables_filter {
            display: none;
        }

        .dataTables_paginate {
            display: none;
        }

        tbody, td, tfoot, th, thead, tr {
            font-weight: 400;
        }

        .doughnut-cart-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
    <div class="page-wrapper" data-ng-app="RpFinanceApp" data-ng-controller="RpFinanceAppCtrl">
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
            <%--<div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Discount Group</li>
                        </ol>
                    </nav>
                </div>
            </div>--%>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <%--<asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" Add Discount Group"></asp:Label>
                    <hr />--%>
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="float: left; width: 100%">
                                <div class="f-dashbrd-sec">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx">
                                                <p class="f-dashbrd-bx-txt1">Today's Collection</p>
                                                <h2 class="f-dashbrd-bx-txt2" id="todyCollc" runat="server"></h2>
                                                <p class="f-dashbrd-bx-txt3" id="todyCollcnobill" runat="server"></p>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx" style="background: linear-gradient(to bottom right, #63d457 0, #3cbf2d 100%);">
                                                <p class="f-dashbrd-bx-txt1">Current month till date</p>
                                                <h2 class="f-dashbrd-bx-txt2" id="collcofmonth" runat="server"></h2>
                                                <p class="f-dashbrd-bx-txt3" id="monthCollcnobill" runat="server"></p>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx" style="background: linear-gradient(to bottom right, #ff9e27 0, #d47d13 100%);">
                                                <p class="f-dashbrd-bx-txt1">Today's Discount</p>
                                                <h2 class="f-dashbrd-bx-txt2" runat="server" id="todaysDisc">₹0</h2>
                                                <p class="f-dashbrd-bx-txt3" id="ttlbildisc" runat="server"></p>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx" style="background: linear-gradient(to bottom right, #6cc8ff 0, #008d72 100%);">
                                                <p class="f-dashbrd-bx-txt1">Today's Online Collection</p>
                                                <h2 class="f-dashbrd-bx-txt2" id="appcollc" runat="server"></h2>
                                                <p class="f-dashbrd-bx-txt3" id="AppCollcnobill" runat="server"></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="f-dashbrd-fee-tkn-his">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-12">
                                            <div class="f-dashbrd-fee-tkn-his-inr">
                                                <div class="f-dashbrd-fee-tkn-his-inr-h">Last 15 Days Collection</div>
                                                <div class="f-dashbrd-fee-tkn-his-inr-wpr">
                                                    <canvas id="myBarChart" width="400" height="110"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="f-dashbrd-fee-tkn-his">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-8">
                                            <div class="f-dashbrd-fee-tkn-his-inr">
                                                <div class="f-dashbrd-fee-tkn-his-inr-h">Fee-wise Collection</div>
                                                <div class="f-dashbrd-fee-tkn-his-inr-wpr" style="padding: 0px 5px 5px 5px;">
                                                    <div class="f-dashbrd-hight-fix">
                                                        <table class="table table-bordered" style="margin: 0px;">
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Session</th>
                                                                <th>Fees Name</th>
                                                                <th>Today's Collection</th>
                                                                <th>Month till Date</th>
                                                            </tr>
                                                            <tr data-ng-repeat="x in reportFi">
                                                                <td>{{$index+1}}</td>
                                                                <td>{{x.Session_name}}</td>
                                                                <td>{{x.Fee_head}}</td>
                                                                <td>{{x.Totays_collection}}</td>
                                                                <td>{{x.Month_collection}}</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="doughnut-cart-wpr">
                                                <div class="f-dashbrd-fee-tkn-his-inr" style="padding: 0px 0px 20px 0px;">
                                                    <div class="f-dashbrd-fee-tkn-his-inr-h">Today Collection by Pay Mode</div>
                                                    <div class="f-dashbrd-fee-tkn-his-inr-wpr">
                                                        <canvas id="myDoughnutChart" width="400" height="400"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                  <div class="f-dashbrd-fee-tkn-his">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-12">
                                            <div class="f-dashbrd-fee-tkn-his-inr">
                                                <div class="f-dashbrd-fee-tkn-his-inr-h">Payment Receipt(Today)</div>
                                                <div class="f-dashbrd-fee-tkn-his-inr-wpr">
                                                    <table id="datatable_payment" class="table table-striped table-bordered dataTable">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Date</th>
                                                                <th>Voucher No.</th>
                                                                <th>Ledger</th>
                                                                <th>Mode</th>
                                                                <th>Remark</th>
                                                                <th>Attachment</th>
                                                                 <th> By</th>
                                                                <th>Amount</th>
                                                               
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="Rep_payment_Receipts" runat="server" OnItemDataBound="Rep_payment_Receipts_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="width: 170px;">
                                                                             <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_VoucherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                                         
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_dr_account" runat="server" Text='<%#Eval("Account_Dr") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                         <asp:Label ID="lbl_cr_account" runat="server" Text='<%#Eval("Account_CR") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                             <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                           <asp:Label ID="lblAttachments" runat="server" Text='<%#Bind("ImagePath") %>' Visible="false"  ></asp:Label>
                                                                          <a runat="server" id="a1" href='<%#Eval("ImagePath") %>' download style="display: block; padding: 0px 0px 0px 0px; font-family: ebrima; font-size: 16px; color: #0066CC; text-decoration: none;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                                                        </td>
                                                                        <td>
                                                                          
                                                                              <asp:Label ID="lbl_Createdby" runat="server" Text='<%#Bind("Createdby")%>'></asp:Label>
                                                                      </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
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



                                <div class="f-dashbrd-fee-tkn-his">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-12">
                                            <div class="f-dashbrd-fee-tkn-his-inr">
                                                <div class="f-dashbrd-fee-tkn-his-inr-h">Last 10 Cancelled Receipts</div>
                                                <div class="f-dashbrd-fee-tkn-his-inr-wpr">
                                                    <table id="datatable" class="table table-striped table-bordered dataTable">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Name</th>
                                                                <th>Adm. No.</th>
                                                                <th>Class</th>
                                                                <th>Section</th>
                                                                <th>Roll No.</th>
                                                                <th>Paid</th>
                                                                <th>Receipt Id</th>
                                                                <th>Date</th>
                                                                <th>Pay Mode</th>
                                                                <th>Cancel Remarks</th>
                                                                <th>Can. Time</th>
                                                                <th>Cancelled By</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_student_name" runat="server" class="nowordbreak" Text='<%#Bind("Student_name")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" class="nowordbreak" Text='<%#Bind("Addmission_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" class="nowordbreak" Text='<%#Bind("className")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label3" runat="server" class="nowordbreak" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" class="nowordbreak" Text='<%#Bind("Rollnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label5" runat="server" class="nowordbreak" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label6" runat="server" class="nowordbreak" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label7" runat="server" class="nowordbreak" Text='<%#Bind("Date")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label11" runat="server" class="nowordbreak" Text='<%#Bind("mode")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label8" runat="server" Text='<%#Bind("Remark")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label9" runat="server" class="nowordbreak" Text='<%#Bind("insert_time_date")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label10" runat="server" class="nowordbreak" Text='<%#Bind("Cancelled_by")%>'></asp:Label>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

    <asp:HiddenField ID="hd_from_date" runat="server" />
    <asp:HiddenField ID="hd_to_date" runat="server" />
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_session_name" runat="server" />
    <asp:HiddenField ID="hd_branch_id" runat="server" />
    <asp:HiddenField ID="hd_months" runat="server" />

    <script type="text/javascript">
        var app = angular.module('RpFinanceApp', []);
        app.controller('RpFinanceAppCtrl', function ($scope, $http, $exceptionHandler) {
            var fromiDate = $("#<%=hd_from_date.ClientID %>").val();
            var toiDate = $("#<%=hd_to_date.ClientID %>").val();
            $http.get("webServices/finance.asmx/fetch_headwise_fee", { params: { "FromiDate": fromiDate, "ToiDate": toiDate } }).then(function (response) {
                $scope.reportFi = response.data;
            })
        });

    </script>






    <script>
        $(document).ready(function () {
            var session_id = $('#<%= hd_session_id.ClientID %>').val();
            var session_name = $('#<%= hd_session_name.ClientID %>').val();
            var branch_id = $('#<%= hd_branch_id.ClientID %>').val();
            var monthsdays = $('#<%= hd_months.ClientID %>').val();
            $(function () {
                var type = "MonthlyFee";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "webServices/finance.asmx/find_months_collections_report_days",
                    data: '{"Session_id":"' + session_id + '","Session_name":"' + session_name + '","Branch_id":"' + branch_id + '", "Monthsdays":"' + monthsdays + '"}',
                    dataType: "json",
                    success: function (response) {
                        var chartData = JSON.parse(response.d);
                        load_chart(chartData)
                    },
                });
            })



            //==================
            $(function () {
                var type = "Modewise Payment";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "webServices/finance.asmx/find_modewise_amount",
                    data: '{"Session":"' + session_id + '"}',
                    dataType: "json",
                    success: function (response) {
                        var chartData = JSON.parse(response.d);
                        load_chart111(chartData)
                    },
                });
            })
        });

        function load_chart(data) {
            const options = {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                plugins: {
                    datalabels: {
                        anchor: 'end',
                        align: 'end',
                        formatter: (value) => value,
                        color: 'black',
                        font: {
                            weight: 'bold',
                            size: 12
                        }
                    }
                }
            };

            const ctx1 = document.getElementById('myBarChart').getContext('2d');
            const myBarChart = new Chart(ctx1, {
                type: 'bar',
                data: data,
                options: options,
                plugins: [ChartDataLabels] // Register the data labels plugin
            });
        }



        function load_chart111(data) {
            const config = {
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Doughnut Chart with Values',
                        },
                        datalabels: {
                            color: '#fff',
                            formatter: (value, context) => {
                                return value; // Display the value
                            },
                        },
                    },
                },
                plugins: [ChartDataLabels], // Register the datalabels plugin
            };

            const ctx = document.getElementById('myDoughnutChart').getContext('2d');
            const myDoughnutChart = new Chart(ctx, {
                type: 'doughnut',
                data: data,
                config: config,
                plugins: [ChartDataLabels] // Register the data labels plugin
            });
        }
    </script>
</asp:Content>
