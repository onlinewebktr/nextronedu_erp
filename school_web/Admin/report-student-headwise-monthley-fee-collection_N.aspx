<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="report-student-headwise-monthley-fee-collection_N.aspx.cs" Inherits="school_web.Admin.report_student_headwise_monthley_fee_collection_N" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student & Head wise Monthly Fees Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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
    <script src="../assets/js/table2excel.js"></script>

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
            font-size: 13px;
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student & Head wise Monthly Fees Collection</li>
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
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection-monthly.aspx">Today Fee Collection</a></li>
                        <li><a href="monthly-fee-collection-report.aspx">Bill-wise Fee Collection</a></li> 
                        <li><a href="monthly-fee-collection-monthwise.aspx">Month-Wise Fee Collection</a></li>
                        <li><a href="report-headwise-fee-collection-monthly.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-monthley-fee-collection_N.aspx" class="sub-mnu-p-a-active">Student & Head wise Fee Collection</a></li>
                        <li><a href="fee-collection-details.aspx">Student Payment Status</a></li>
                        <li><a href="Online_Monthly_Payment_History.aspx">Online Fee Collection (APP)</a></li> 


                        <%--<li><a href="report-today-fees-collection-monthly.aspx">Today Monthly Fee Collection Summary</a></li>
                        <li><a href="monthly-fee-collection-monthwise.aspx">Monthly Fee Collection Month-Wise</a></li>
                        <li><a href="monthly-fee-collection-report.aspx">Today Monthly Fee Collection </a></li> 
                        <li><a href="report-headwise-fee-collection-monthly.aspx">Head wise Fee Collection(Day Boarding)</a></li> 
                        <li><a href="report-student-headwise-monthley-fee-collection_N.aspx" class="sub-mnu-p-a-active">Student & Head wise Fee Collection</a></li>
                        <li><a href="typewise-fee-collection-report.aspx">Month Wise Fee Collection</a></li>
                        <li><a href="Online_Monthly_Payment_History.aspx">Online Monthly Fee Collection</a></li>  --%>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
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
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Find</a>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div id="excel" runat="server" visible="false">
                                                            <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                                        </div>

                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr" id="tblCustomers">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="head-printdv" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                                </h1>

                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                    &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">Time Period-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="prnt-dv-wpr printborder" id="grdsdatA">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Payment Date</th>
                                                                    <th>Invoice No.</th>
                                                                    <th>Admission No.</th>
                                                                    <th>Student Name</th>
                                                                    <th>Class</th>
                                                                    <th>Section</th>
                                                                    <th>Roll no.</th>
                                                                    <th>Invoice Month</th>
                                                                    <th data-ng-repeat="x in reportHeadinG">{{x.Content}}</th>
                                                                    <th>Net Amount</th>
                                                                    <th>Paid Amount </th>
                                                                    <th>Balance Amount</th>
                                                                    <th>Payment Mode</th>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportAmountS track by $index">
                                                                    <td>{{$index+1}}</td>
                                                                    <td>{{x.Payment_date}}</td>
                                                                    <td>{{x.Slip_no}}</td>
                                                                    <td>{{x.Admission_no}}</td>
                                                                    <td>{{x.Student_name}}</td>
                                                                    <td>{{x.Class_name}}</td>
                                                                    <td>{{x.Sections}}</td>
                                                                    <td>{{x.Roll_no}}</td>
                                                                    <td>{{x.Months}}</td> 
                                                                    <td data-ng-repeat="item in x.MyFeeReportItem track by $index">{{item.HeadFees}}</td>
                                                                    <td>{{x.Ttl_amts}}</td>
                                                                    <td>{{x.PaidFesAmt}}</td>
                                                                    <td>{{x.RestFesAmt}}</td>
                                                                    <td>{{x.Payment_mode}}</td>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportAmountSFnal track by $index">
                                                                    <td colspan="9" class="fntbold" style="text-align: right">Total : </td>
                                                                    <td class="fntbold" data-ng-repeat="item in x.MyFeeReportOverAllItem track by $index">{{item.HeadFees}}</td>
                                                                    <td class="fntbold">{{x.TotaLFinaLFeeS}}</td>
                                                                    <td class="fntbold">{{x.TotaLFinaLPaidFeeS}}</td>
                                                                    <td class="fntbold">{{x.TotaLFinaLRestFeeS}}</td>
                                                                    <td class="fntbold">-</td>
                                                                </tr>
                                                            </table>

                                                            <div class="paid-cat-div" style="display: none">
                                                                <div class="row">
                                                                    <div class="col-sm-4" data-ng-repeat="x in reportPaymOde">
                                                                        <p class="paid-cat-div-p">
                                                                            <i>{{x.Pay_mode}} </i>:
                                                            <asp:Label ID="lbl_by_cash" runat="server" Text="{{x.Paid_amt}}"></asp:Label>
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="notFound hidden" id="NotFounD">
                                                            <p>No record found.</p>
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
        </div>
        <!--end row-->
    </div>

    <style>
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
    </style>


    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {

            var session_id = $("#<%=ddl_session.ClientID%>").val();
            var class_id = $("#<%=ddl_class.ClientID%>").val();

            <%--var session_name = $("#<%=ddl_session.ClientID%>").text();
            var class_name = $("#<%=ddl_class.ClientID%>").text();--%>


            var ddlID = '#' + '<%= ddl_session.ClientID %>';
            var session_name = $(ddlID + " option:selected").text();

            var ddlIDDD = '#' + '<%= ddl_class.ClientID %>';
            var class_name = $(ddlIDDD + " option:selected").text();


            var Fromdates = $("#<%=txt_from_date.ClientID%>").val();
            var Todates = $("#<%=txt_to_date.ClientID%>").val();

            //alert(session_name); alert(class_name);
            var type = "MonthlyFee";
            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            $http.get("webServices/fee-report1.asmx/fetch_report_heading_day_end", {
                params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name }
            }).then(function (response) {
                $scope.reportHeadinG = response.data;
            })



            $http.get("webServices/fee-report1.asmx/fetch_report_heading_day_end_amts", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name } }).then(function (response) {
                $scope.reportAmountS = response.data;

                $("#intsLoader").addClass("hidden");
                if ($scope.reportAmountS == "") {
                    $("#grdsdatA").addClass("hidden");
                    $("#NotFounD").removeClass("hidden");
                    $("#excelbtnS").addClass("hidden");
                    $("#<%=print1.ClientID%>").addClass("hidden");
                }
                else {
                    $("#grdsdatA").removeClass("hidden");
                    $("#NotFounD").addClass("hidden");
                    $("#excelbtnS").removeClass("hidden");
                    $("#<%=print1.ClientID%>").removeClass("hidden");
                }
            })


            $http.get("webServices/fee-report1.asmx/fetch_report_heading_day_end_final_amts", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name } }).then(function (response) {
                $scope.reportAmountSFnal = response.data;
            })

            //$http.get("webServices/fee-report.asmx/fetch_report_ttl_by_mode", { params: { "Dates": dates, "Type": type } }).then(function (response) {
            //    $scope.reportPaymOde = response.data;
            //})


            //====FIND
            $scope.ButtonClickFind = function () {
                var session_id = $("#<%=ddl_session.ClientID%>").val();
                var class_id = $("#<%=ddl_class.ClientID%>").val();

                var ddlID = '#' + '<%= ddl_session.ClientID %>';
                var session_name = $(ddlID + " option:selected").text();

                var ddlIDDD = '#' + '<%= ddl_class.ClientID %>';
                var class_name = $(ddlIDDD + " option:selected").text();

                var Fromdates = $("#<%=txt_from_date.ClientID%>").val();
                var Todates = $("#<%=txt_to_date.ClientID%>").val();



                var type = "MonthlyFee";
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("webServices/fee-report1.asmx/fetch_report_heading_day_end", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name } }).then(function (response) {
                    $scope.reportHeadinG = response.data;
                })


                $http.get("webServices/fee-report1.asmx/fetch_report_heading_day_end_amts", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name } }).then(function (response) {
                    $scope.reportAmountS = response.data;

                    $("#intsLoader").addClass("hidden");

                    if ($scope.reportAmountS == "") {
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                        $("#excelbtnS").addClass("hidden");
                        $("#<%=print1.ClientID%>").addClass("hidden");
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        $("#excelbtnS").removeClass("hidden");
                        $("#<%=print1.ClientID%>").removeClass("hidden");
                    }
                })


                $http.get("webServices/fee-report1.asmx/fetch_report_heading_day_end_final_amts", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name } }).then(function (response) {
                    $scope.reportAmountSFnal = response.data;
                })

                //$http.get("webServices/fee-report.asmx/fetch_report_ttl_by_mode", { params: { "Dates": dates, "Type": type } }).then(function (response) {
                //    $scope.reportPaymOde = response.data;
                //})
            }


            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "MonthlyFee.xls"
                });
            }

        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>
</asp:Content>
