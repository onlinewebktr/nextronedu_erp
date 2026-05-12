<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="income-and-expense-report-new1.aspx.cs" Inherits="school_web.Admin.income_and_expense_report_new1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Income & Expense Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/print-icome-exp.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>

    <%--<script src="../assets/Angular/angular-datatables.min.js"></script>
    <script src="../assets/js/table2excel.js"></script>--%>

    <style>
        .btn i {
            vertical-align: middle;
            font-size: inherit;
            margin-top: -1em;
            margin-bottom: -1em;
            margin-right: 5px;
        }

        .paid-cat-div-p {
            font-weight: 600;
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 12px;
            text-transform: capitalize;
        }

        .chkstle {
            position: relative;
            padding: 6px 5px 1px 5px;
            margin: 0px 1px 5px 0px;
            cursor: pointer;
            font-size: 12px;
            font-weight: 600;
            float: left;
            border: 1px solid #bac419;
            background: #FBFFBB;
            border-radius: 2px;
        }

            .chkstle input {
                cursor: pointer;
                width: 21px;
            }

            .chkstle label {
                margin: -2px 0px 0px 0px;
                float: left;
            }

        .mode-ul {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            text-align: center;
        }

            .mode-ul li {
                margin: 0px 5px 0px 0px;
                padding: 2px 5px;
                list-style-type: none;
                display: inline;
                font-size: 13px;
                border: 1px solid #bac419;
                background: #FBFFBB;
                border-radius: 2px;
                font-weight: 600;
                color: #000;
            }

                .mode-ul li i {
                    margin: 0px;
                    padding: 0px;
                    font-style: normal;
                }

                .mode-ul li span {
                    margin: 0px;
                    padding: 0px;
                }

        .btn {
            padding: 5px 7px 4px;
            font-size: 13px;
        }

        .usewise-dv {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            position: relative;
        }

        .usewise-tbldv {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .usewise-title {
            margin: 5px 0px 5px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 17px;
        }

        .std-info-fnd {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            position: absolute;
            left: 0px;
            top: -22px;
            cursor: pointer;
            background: #000;
        }
    </style>
    <style>
        .sub-pag-menu-ul li a {
            color: #312F7F;
            border: 1px solid #312F7F;
        }

            .sub-pag-menu-ul li a:hover {
                background: #312F7F;
                color: #fff;
                border: 1px solid #312F7F;
            }

        .sub-mnu-p-a-active {
            background: #312F7F;
            color: #ffffff !important;
        }

        .dataTables_length {
            display: none;
        }

        table tr td {
            padding: 5px 0px;
            font-size: 14px;
            text-align: center;
        }

        .prnsign {
            margin: 40px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            text-align: right;
            font-weight: 500;
            color: #000;
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Account</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Income & Expense Report</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <div class="col-xl-12">
                    <div class="ints-loader-wpr" id="intsLoader">
                        <div class="ints-loader-wpr-inr">
                            <div class="ints-loader">
                                <p class="ints-loader-txt">
                                    <img src="../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="grd-wpr">
                        <div class="card">
                            <div class="card-body">
                                <div class="find-dv">
                                    <div class="row  g-3 needs-validation">
                                        <div class="col-sm-3" style="display: none">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-3">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                    <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-6">
                                                    <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                    <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" style="display: none">
                                            <label for="validationCustom01" class="find-dv-lbl">With Opening Balance</label>
                                            <asp:DropDownList ID="ddl_opening_balance" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style">
                                                <asp:ListItem>No</asp:ListItem>
                                                <asp:ListItem>Yes</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-sm-6">
                                            <%--<a href="#!" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Complete DCR</a>--%>
                                            <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find" />
                                            <%--<asp:Button ID="btn_academic_dcr" OnClick="btn_academic_dcr_Click" Style="display: none" class="btn btn-primary find-dv-btn" runat="server" Text="Academic DCR" />
                                            <asp:Button ID="btn_hostel_dcr" OnClick="btn_hostel_dcr_Click" Style="display: none" class="btn btn-primary find-dv-btn" runat="server" Text="Hostel DCR" />

                                            <div id="excel" runat="server" visible="false">
                                                <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                            </div>--%>

                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>


                                <div class="grd-wpr" id="tblCustomers">
                                    <div class="col-sm-12">
                                        <div id="tblPrintIQ" runat="server">
                                            <div class="head-printdv" style="border-bottom: 0px solid #000; margin: 0px; float: left; width: 100%;">
                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                </div>
                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 60%; float: left;">
                                                    <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                    </h1>
                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>
                                                        &nbsp;&nbsp;  website :
                                                        <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        <span style="font-size: 14px; font-weight: bold;">
                                                            <asp:Label ID="lbl_report_type" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                <div class="grd-wpr-new">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <td colspan="6" class="top-rows">Income & Expense Detail</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" class="width50 scnd-rows">Income</td>
                                                            <td colspan="3" class="width50 scnd-rows">Expense</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Sr.No</td>
                                                            <td>Income Detail</td>
                                                            <td>Amount</td>

                                                            <td>Sr.No</td>
                                                            <td>Expense Detail</td>
                                                            <td>Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span data-ng-repeat="x in reportIncome" class="incomeLeft txtcntr" style="{{x.isrowhide}}">{{$index+1}}</span>
                                                            </td>
                                                            <td>
                                                                <span class="incomeLeft" data-ng-repeat="x in reportIncome" style="{{x.isrowhide}}">{{x.Content}}</span>
                                                            </td>
                                                            <td>
                                                                <span class="incomeLeft txtright" data-ng-repeat="x in reportIncome" style="{{x.isrowhide}}">{{x.Amounts}}</span>
                                                            </td>
                                                            <td>
                                                                <span class="incomeLeft txtcntr" data-ng-repeat="x in reportExpense">{{$index+1}}</span>
                                                            </td>
                                                            <td>
                                                                <span class="incomeLeft" data-ng-repeat="x in reportExpense">{{x.Content}}</span>
                                                            </td>
                                                            <td>
                                                                <span class="incomeLeft txtright" data-ng-repeat="x in reportExpense">{{x.Amounts}}</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">Total
                                                            </td>
                                                            <td class="txtright">{{reportIncome[reportIncome[0].RowsCountss].Total_income}}
                                                            </td>

                                                            <td colspan="2">Total
                                                            </td>
                                                            <td class="txtright">{{reportExpense[reportExpense[0].RowsCountss].Total_Expense}}
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="6" class="last-rows">Net Amount : {{ incomeExpenseDifference }}</td>
                                                        </tr>
                                                    </table>

                                                    <p class="prnsign">Principal Sign</p>
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
    </div>

    <!--end row-->


    <style type="text/css">
        .hidden {
            display: none !important;
        }

        .fntbold {
            font-weight: 600;
        }

        .notFound {
            margin: 0px;
            padding: 20px 0px;
            width: 100%;
            float: left;
            background: #effbe8;
            text-align: center;
            border: 1px solid #d6edc2;
            font-size: 18px;
        }

            .notFound p {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
            }

        .incomeLeft {
            width: 100%;
            display: inline-block;
            padding: 5px 0px;
            font-size: 14px;
            text-align: left;
        }

        .txtright {
            text-align: right;
        }

        .width50 {
            width: 50%;
        }

        .txtcntr {
            text-align: center;
        }
    </style>


    <asp:HiddenField ID="hd_collection_type" runat="server" />
    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_class_name" runat="server" />

    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            var collection_type = $("#<%=hd_collection_type.ClientID%>").val();
            var Fromdates = $("#<%=txt_from_date.ClientID%>").val();
            var Todates = $("#<%=txt_to_date.ClientID%>").val();
            var OpBlnce = $("#<%=ddl_opening_balance.ClientID%>").val();

            // Show loader
            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            // Fetch income data
            $http.get("webServices/account/incomeExpenseNew1.asmx/fetch_report_income", { params: { "FromDate": Fromdates, "ToDate": Todates, "OpBlnce": OpBlnce } })
                .then(function (response) {
                    $scope.reportIncome = response.data;
                    calculateDifference(); // Calculate difference after fetching income
                    $("#intsLoader").addClass("hidden");

                    if ($scope.reportAmountS == "") {
                        $("#grdsdatA").addClass("hidden");
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                    }
                });

            // Fetch expense data
            $http.get("webServices/account/incomeExpenseNew1.asmx/fetch_report_expense", { params: { "FromDate": Fromdates, "ToDate": Todates, "OpBlnce": OpBlnce } })
                .then(function (response) {
                    $scope.reportExpense = response.data;
                    calculateDifference(); // Calculate difference after fetching expense
                    $("#intsLoader").addClass("hidden");
                });

            // Function to calculate the difference
            function calculateDifference() {
                const totalIncome = $scope.reportIncome && $scope.reportIncome.length > 0
                    ? $scope.reportIncome[$scope.reportIncome.length - 1].Total_income || 0
                    : 0;

                const totalExpense = $scope.reportExpense && $scope.reportExpense.length > 0
                    ? $scope.reportExpense[$scope.reportExpense.length - 1].Total_Expense || 0
                    : 0;


                $scope.incomeExpenseDifference = parseFloat((totalIncome - totalExpense).toFixed(2));
            }
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>
</asp:Content>
