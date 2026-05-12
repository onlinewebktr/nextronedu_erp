<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="day-end-report-of-admission-fee.aspx.cs" Inherits="school_web.Admin.day_end_report_of_admission_fee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Day End Report of Admission Fee
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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
                            <li class="breadcrumb-item active" aria-current="page">Day End Report of Admission Fee</li>
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
                        <li><a href="Today_Collection.aspx">Day End Collection Report</a></li>
                        <li><a href="overall-collection-report.aspx">Overall Collection Report</a></li>
                        <li><a href="day-end-report-typewise.aspx"><%--Day End Report of --%>Monthly Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-admission-fee.aspx" class="sub-mnu-p-a-active"><%--Day End Report of --%>Admission Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-annual-fee.aspx"><%--Day End Report of --%>Annual Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-form-sale.aspx">Form Sale Report</a></li> 
                        <li><a href="day_End_Report_Summery_N.aspx">Day End Summary</a></li>
                        <li><a href="day-end-report-summery-headwise.aspx">Day End Summary Headwise</a></li>
                        <li><a href="Fee_Collection_Report.aspx">Fees Collection Summary</a></li>
                        <li><a href="userwise-payment-collection-report.aspx">User Wise</a></li> 
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                        <asp:TextBox ID="txt_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-7">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Find</a>
                                                    </div>


                                                    <div class="col-sm-3">
                                                         <div id="excel" runat="server" visible="false" >
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>

                                                             </div>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">

                                                        <div class="prnt-dv-wpr printborder" id="grdsdatA">
                                                            <table class="table table-bordered" id="tblCustomers">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Admission No.</th>
                                                                    <th>Student Name</th>
                                                                    <th>Class</th>
                                                                    <th>Section</th>
                                                                    <th>Roll no.</th>
                                                                    <th data-ng-repeat="x in reportHeadinG">{{x.Content}}</th>
                                                                    <th>Total</th>
                                                                    <th>Payment Mode</th>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportAmountS">
                                                                    <td>{{$index+1}}</td>
                                                                    <td>{{x.Admission_no}}</td>
                                                                    <td>{{x.Student_name}}</td>
                                                                    <td>{{x.Class_name}}</td>
                                                                    <td>{{x.Sections}}</td>
                                                                    <td>{{x.Roll_no}}</td>

                                                                    <td class="{{x.showFeeS1}}">{{x.DshowFeeS1}}</td>
                                                                    <td class="{{x.showFeeS2}}">{{x.DshowFeeS2}}</td>
                                                                    <td class="{{x.showFeeS3}}">{{x.DshowFeeS3}}</td>
                                                                    <td class="{{x.showFeeS4}}">{{x.DshowFeeS4}}</td>
                                                                    <td class="{{x.showFeeS5}}">{{x.DshowFeeS5}}</td>
                                                                    <td class="{{x.showFeeS6}}">{{x.DshowFeeS6}}</td>
                                                                    <td class="{{x.showFeeS7}}">{{x.DshowFeeS7}}</td>
                                                                    <td class="{{x.showFeeS8}}">{{x.DshowFeeS8}}</td>
                                                                    <td class="{{x.showFeeS9}}">{{x.DshowFeeS9}}</td>
                                                                    <td class="{{x.showFeeS10}}">{{x.DshowFeeS10}}</td>
                                                                    <td class="{{x.showFeeS11}}">{{x.DshowFeeS11}}</td>
                                                                    <td class="{{x.showFeeS12}}">{{x.DshowFeeS12}}</td>
                                                                    <td class="{{x.showFeeS13}}">{{x.DshowFeeS13}}</td>
                                                                    <td class="{{x.showFeeS14}}">{{x.DshowFeeS14}}</td>
                                                                    <td class="{{x.showFeeS15}}">{{x.DshowFeeS15}}</td>
                                                                    <td>{{x.TotaLFeeS}}</td>
                                                                    <td>{{x.Payment_mode}}</td>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportAmountSFnal">
                                                                    <td colspan="6" class="fntbold" style="text-align: right">Total : </td>
                                                                    <td class="fntbold {{x.showFeeS1}}">{{x.DshowFeeS1}}</td>
                                                                    <td class="fntbold {{x.showFeeS2}}">{{x.DshowFeeS2}}</td>
                                                                    <td class="fntbold {{x.showFeeS3}}">{{x.DshowFeeS3}}</td>
                                                                    <td class="fntbold {{x.showFeeS4}}">{{x.DshowFeeS4}}</td>
                                                                    <td class="fntbold {{x.showFeeS5}}">{{x.DshowFeeS5}}</td>
                                                                    <td class="fntbold {{x.showFeeS6}}">{{x.DshowFeeS6}}</td>
                                                                    <td class="fntbold {{x.showFeeS7}}">{{x.DshowFeeS7}}</td>
                                                                    <td class="fntbold {{x.showFeeS8}}">{{x.DshowFeeS8}}</td>
                                                                    <td class="fntbold {{x.showFeeS9}}">{{x.DshowFeeS9}}</td>
                                                                    <td class="fntbold {{x.showFeeS10}}">{{x.DshowFeeS10}}</td>
                                                                    <td class="fntbold {{x.showFeeS11}}">{{x.DshowFeeS11}}</td>
                                                                    <td class="fntbold {{x.showFeeS12}}">{{x.DshowFeeS12}}</td>
                                                                    <td class="fntbold {{x.showFeeS13}}">{{x.DshowFeeS13}}</td>
                                                                    <td class="fntbold {{x.showFeeS14}}">{{x.DshowFeeS14}}</td>
                                                                    <td class="fntbold {{x.showFeeS15}}">{{x.DshowFeeS15}}</td>
                                                                    <td class="fntbold" colspan="2">{{x.TotaLFinaLFeeS}}</td>
                                                                </tr>
                                                            </table>

                                                            <div class="paid-cat-div">
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

            var dates = $("#<%=txt_date.ClientID%>").val();
            var type = "AdmissionFee";
            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            $http.get("graph.asmx/fetch_report_heading_day_end", { params: { "Dates": dates, "Type": type } }).then(function (response) {
                $scope.reportHeadinG = response.data;
            })


            $http.get("graph.asmx/fetch_report_heading_day_end_amts", { params: { "Dates": dates, "Type": type } }).then(function (response) {
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


            $http.get("graph.asmx/fetch_report_heading_day_end_final_amts", { params: { "Dates": dates, "Type": type } }).then(function (response) {
                $scope.reportAmountSFnal = response.data;
            })

            $http.get("graph.asmx/fetch_report_ttl_by_mode", { params: { "Dates": dates, "Type": type } }).then(function (response) {
                $scope.reportPaymOde = response.data;
            })


            //====FIND
            $scope.ButtonClickFind = function () {
                var dates = $("#<%=txt_date.ClientID%>").val();
                var type = "AdmissionFee";
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("graph.asmx/fetch_report_heading_day_end", { params: { "Dates": dates, "Type": type } }).then(function (response) {
                    $scope.reportHeadinG = response.data;
                })


                $http.get("graph.asmx/fetch_report_heading_day_end_amts", { params: { "Dates": dates, "Type": type } }).then(function (response) {
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


                $http.get("graph.asmx/fetch_report_heading_day_end_final_amts", { params: { "Dates": dates, "Type": type } }).then(function (response) {
                    $scope.reportAmountSFnal = response.data;
                })

                $http.get("graph.asmx/fetch_report_ttl_by_mode", { params: { "Dates": dates, "Type": type } }).then(function (response) {
                    $scope.reportPaymOde = response.data;
                })
            }


            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "AdmissionFee.xls"
                });
            }

        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>
</asp:Content>
