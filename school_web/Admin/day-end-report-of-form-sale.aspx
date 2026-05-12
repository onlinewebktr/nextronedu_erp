<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="day-end-report-of-form-sale.aspx.cs" Inherits="school_web.Admin.day_end_report_of_form_sale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Day End Report Of Form Sale
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

    <script src="../assets/Angular/angular-datatables.min.js"></script>
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

        /*============================================================*/
        table.dataTable td, table.dataTable th {
            position: relative;
        }

        .fxtblWpr {
            width: 100%;
            height: 410px;
            overflow: auto;
        }

        table {
            width: 100% !important;
            overflow-x: scroll;
        }


        .fixed-td {
            position: sticky !important;
            z-index: 2;
            left: 0;
            color: #000;
            background-color: #efff00 !important;
        }

        .fixed-hd {
            position: sticky !important;
            top: 0;
            z-index: 1 !important;
        }

        .left-top-td {
            z-index: 3 !important;
        }

        /*.scrollable-td {
            width: 200px;
        }*/

        .noline-break {
            white-space: nowrap;
            word-break: keep-all;
        }

        .txtbxstyles {
            margin: 0px;
            padding: 3px 4px 2px;
            height: 27px;
            border: 1px solid #959595;
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
                            <li class="breadcrumb-item active" aria-current="page">Form Sale Collection</li>
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
                    <ul class="sub-pag-menu-ul">
                        <li><a href="Today_Collection.aspx">Day End Collection Report</a></li>
                        <li><a href="overall-collection-report.aspx">Head Wise Collection Report</a></li>
                        <%--<li><a href="day-end-report-typewise.aspx">Monthly Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-admission-fee.aspx">Admission Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-annual-fee.aspx">Annual Fee Collection Report</a></li>--%>
                        <li><a href="day-end-report-of-form-sale.aspx" class="sub-mnu-p-a-active">Form Sale Report</a></li>
                         <li><a href="day_End_Report_Summery_N.aspx">Day End Summary</a></li> 
                        <%--<li><a href="day-end-report-summery-headwise.aspx">Day End Summary Headwise</a></li>
                        <li><a href="Fee_Collection_Report.aspx">Fees Collection Summary</a></li>--%>
                        <%--<li><a href="userwise-payment-collection-report.aspx">User Wise Collection Report</a></li>--%>
                    </ul>
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
                                                <div class="col-sm-6" style="display: none">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6" style="display: none">
                                                    <label for="validationCustom01" class="find-dv-lbl">Mode</label>
                                                    <asp:DropDownList ID="ddl_mode" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-6"  style="display: none">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
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


                                        <div class="col-sm-6">
                                            <%--<a href="#!" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Complete DCR</a>--%>
                                            <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find" />
                                            <asp:Button ID="btn_academic_dcr" OnClick="btn_academic_dcr_Click" Style="display: none" class="btn btn-primary find-dv-btn" runat="server" Text="Academic DCR" />
                                            <asp:Button ID="btn_hostel_dcr" OnClick="btn_hostel_dcr_Click" Style="display: none" class="btn btn-primary find-dv-btn" runat="server" Text="Hostel DCR" />

                                            <div id="excel" runat="server" visible="false">
                                                <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                            </div>

                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print">
                                                <i class='bx bx-printer'></i>Print</asp:LinkButton>
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
                                                        &nbsp;&nbsp;  website :
                                                                    <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        <span style="font-size: 14px; font-weight: bold;">Form Sale report for -
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                <div class="grd-wpr-new">
                                                    <div class="fxtblWpr">
                                                        <table class="table table-bordered" datatable="ng" dt-options="vm.dtOptions" style="border: 1px solid #c5c0c0;">
                                                            <thead>
                                                                <tr>
                                                                    <th class="scrollable-td fixed-hd">#</th>
                                                                    <th class="scrollable-td fixed-hd">Date</th>
                                                                    <th class="scrollable-td fixed-hd">By</th>
                                                                    <th class="scrollable-td fixed-hd">Invoice No.</th>
                                                                    <th class="scrollable-td fixed-hd">Student Name</th>
                                                                    <th class="scrollable-td fixed-hd">Father Name</th>
                                                                    <th class="scrollable-td fixed-hd">Class</th>
                                                                    <th class="scrollable-td fixed-hd">Month</th>
                                                                    <th class="scrollable-td fixed-hd">Mode</th>
                                                                    <th class="scrollable-td fixed-hd">Transaction No.</th>
                                                                    <th class="scrollable-td fixed-hd">Amount</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr data-ng-repeat="x in reportAmountS | filter : searchs">
                                                                    <td>{{$index+1}}</td>
                                                                    <td>{{x.Payment_date}}</td>
                                                                    <td>{{x.IssuedBy}}</td>
                                                                    <td>{{x.Slip_no}}</td>
                                                                    <td>{{x.Student_name}}</td>
                                                                    <td>{{x.Father_name}}</td>
                                                                    <td>{{x.Class_name}}</td>
                                                                    <%--<td>{{x.Sections}}</td>
                                                                <td>{{x.Roll_no}}</td>--%>
                                                                    <td>{{x.Months}}</td>
                                                                    <td>{{x.Payment_mode}}</td>
                                                                    <td>{{x.Transaction_Id}}</td>
                                                                    <td>{{x.PaidFesAmt}}</td>
                                                                </tr>
                                                            </tbody>
                                                            <tr>
                                                                <td colspan="10" class="fntbold" style="text-align: right">Total : </td>
                                                                <td class="fntbold">{{totalPaidFesAmt | number:2}}</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="paid-cat-div">
                                                        <ul class="mode-ul">
                                                            <li data-ng-repeat="x in reportmodes"><i>{{x.Payment_mode}} </i>: <span>{{x.Amounts}}</span></li>
                                                        </ul>
                                                    </div>
                                                </div>


                                                <div class="usewise-dv">
                                                    <div class="std-info-fnd noPrint" data-ng-click="ButtonClickShow()">
                                                        <span class="material-symbols-outlined fullscreenIco">close_fullscreen</span>
                                                    </div>
                                                    <div class="usewise-tbldv" id="userWiseCollectionInfo">
                                                        <h2 class="usewise-title">User-Wise Collection Details</h2>
                                                        <div class="grd-wpr-new">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>User Name</th>
                                                                    <th data-ng-repeat="x in reportmodesUser">{{x.Content}}</th>
                                                                    <th>Total</th>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportmodesUserAmt">
                                                                    <td>{{$index+1}}</td>
                                                                    <td>{{x.User_name}} ({{x.User_id}})</td>
                                                                    <td data-ng-repeat="item in x.MyFeeReportuser_amtItem track by $index">{{item.HeadFees}}</td>
                                                                    <td>{{x.PaidFesAmt}}</td>
                                                                </tr>
                                                            </table>
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

    <!--end row-->


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


    <asp:HiddenField ID="hd_collection_type" runat="server" />
    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_class_name" runat="server" />

    <script type="text/javascript">
        var app = angular.module('RpCardApp', ['datatables']);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            var collection_type = $("#<%=hd_collection_type.ClientID%>").val();
            var session_id = $("#<%=ddl_session.ClientID%>").val();
            var class_id = $("#<%=ddl_class.ClientID%>").val();
            var ddlID = '#' + '<%= ddl_session.ClientID %>';
            var session_name = $(ddlID + " option:selected").text();
            var class_name = $("#<%=hd_class_name.ClientID%>").val();
            var Fromdates = $("#<%=txt_from_date.ClientID%>").val();
            var Todates = $("#<%=txt_to_date.ClientID%>").val();


            var type = "MonthlyFee";
            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            var showStatus = "1";
            $scope.ButtonClickShow = function () {
                if (showStatus == "1") {
                    $("#userWiseCollectionInfo").addClass("hidden");
                    showStatus = "0";
                }
                else {
                    $("#userWiseCollectionInfo").removeClass("hidden");
                    showStatus = "1";
                }
            }

            $http.get("webServices/account/formSaleCollection.asmx/fetch_report_heading_day_end_amts", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name, "Collection_type": collection_type } }).then(function (response) {
                $scope.reportAmountS = response.data;
                $("#intsLoader").addClass("hidden");
                if ($scope.reportAmountS == "") {
                    $("#grdsdatA").addClass("hidden");
                    $("#NotFounD").removeClass("hidden");
                    $("#excelbtnS").addClass("hidden");
                    $("#<%=print1.ClientID%>").addClass("hidden");
                }
                else {
                    $http.get("webServices/account/formSaleCollection.asmx/fetch_report_modewise", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name, "Collection_type": collection_type } }).then(function (response) {
                        $scope.reportmodes = response.data;
                    })

                    ///===================================UserWise

                    $http.get("webServices/account/formSaleCollection.asmx/fetch_report_userwise_collection_head", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name, "Collection_type": collection_type } }).then(function (response) {
                        $scope.reportmodesUser = response.data;
                        if ($scope.reportmodesUser == "") {
                        }
                        else {
                            $http.get("webServices/account/formSaleCollection.asmx/fetch_report_userwise_collection_amt", { params: { "Session_id": session_id, "Class_id": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Session_name": session_name, "Class_name": class_name, "Collection_type": collection_type } }).then(function (response) {
                                $scope.reportmodesUserAmt = response.data;
                            })
                        }
                    })

                    // Calculate total PaidFesAmt
                    let totalPaidFesAmt = 0;
                    if ($scope.reportAmountS && Array.isArray($scope.reportAmountS)) {
                        $scope.reportAmountS.forEach(function (item) {
                            let amt = parseFloat(item.PaidFesAmt);
                            if (!isNaN(amt)) {
                                totalPaidFesAmt += amt;
                            }
                        });
                    }

                    // Save total to scope if needed
                    $scope.totalPaidFesAmt = totalPaidFesAmt;

                    $("#grdsdatA").removeClass("hidden");
                    $("#NotFounD").addClass("hidden");
                    $("#excelbtnS").removeClass("hidden");
                    $("#<%=print1.ClientID%>").removeClass("hidden");
                }
            })
            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "form-sale-collection-report.xls"
                });
            }
        });
        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>
</asp:Content>
