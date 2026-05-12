<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="overall-collection-report.aspx.cs" Inherits="school_web.Admin.overall_collection_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Head Wise Collection Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <link href="../assets/dropdownmultiselection/style.css" rel="stylesheet" />
    <script src="../assets/dropdownmultiselection/bootstrap-multiselect.js"></script>
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
    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_classs.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

    <script src="../assets/js/table2excel.js"></script>

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
                            <li class="breadcrumb-item active" aria-current="page">Head Wise Collection Report</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="Today_Collection.aspx">Day End Collection Report</a></li>
                        <li><a href="overall-collection-report.aspx" class="sub-mnu-p-a-active">Head Wise Collection Report</a></li>
                        <%--<li><a href="day-end-report-typewise.aspx">Monthly Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-admission-fee.aspx">Admission Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-annual-fee.aspx">Annual Fee Collection Report</a></li>--%>
                        <li><a href="day-end-report-of-form-sale.aspx">Form Sale Report</a></li>
                         <li><a href="day_End_Report_Summery_N.aspx">Day End Summary</a></li> 
                        <%--<li><a href="day-end-report-summery-headwise.aspx">Day End Summary Headwise</a></li>
                        <li><a href="Fee_Collection_Report.aspx">Fees Collection Summary</a></li>--%>
                        <%--<li><a href="userwise-payment-collection-report.aspx">User Wise Collection Report</a></li>--%>
                    </ul>
                </div>


                <div class="col-xl-12" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
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
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-2" id="classDV">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:ListBox ID="ddl_classs" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                                <div class="col-sm-3" id="dateDV">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                            <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                            <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />

                                                </div>
                                                <div class="col-sm-2">
                                                    <a href="javascript:" class="btn btn-primary find-dv-btn hidden" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>

                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class=" hidden btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
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
                                                        <span style="font-size: 14px; font-weight: bold;">Fee Head & Mode-Wise Fee Collection Report<asp:Label ID="lbl_date_period" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="grd-wpr" id="tblCustomers">
                                                <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                    <table class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Fee Type</th>
                                                                <th>Amount</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr data-ng-repeat="x in reportFeeS">
                                                                <td>{{$index+1}}</td>
                                                                <td>{{x.Content}}</td>
                                                                <td>{{x.Paid_amount}}</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: right; font-weight: 700">Total</td>
                                                                <td style="font-weight: 700">{{reportFeeS[reportFeeS[0].Roucounts].Ttl_amount}}</td>
                                                            </tr>


                                                            <tr>
                                                                <th>#</th>
                                                                <th>Payment Mode</th>
                                                                <th>Amount</th>
                                                            </tr>
                                                            <tr data-ng-repeat="x in reportMode">
                                                                <td>{{$index+1}}</td>
                                                                <td>{{x.Content}}</td>
                                                                <td>{{x.Paid_amount}}</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: right; font-weight: 700">Total</td>
                                                                <td style="font-weight: 700">{{reportMode[reportMode[0].Roucounts].Ttl_amount}}</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>


                                                </div>
                                                <div class="notFound" id="NotFounD">
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



    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_from_date" runat="server" />
    <asp:HiddenField ID="hd_to_date" runat="server" />
    <asp:HiddenField ID="hd_find_status" runat="server" />

    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            var find_status = $("#<%=hd_find_status.ClientID%>").val();
            var class_id = $("#<%=hd_class_id.ClientID%>").val();
            var from_date = $("#<%=hd_from_date.ClientID%>").val();
            var to_date = $("#<%=hd_to_date.ClientID%>").val();

            if (find_status == "1") {
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/overall-collection.asmx/fetch_overall_fee_collection", { params: { "Class_id": class_id, "FromDate": from_date, "ToDate": to_date } }).then(function (response) {
                    $scope.reportFeeS = response.data;
                    $("#intsLoader").addClass("hidden");
                    if ($scope.reportFeeS == "") {
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                        $("#excelbtnS").addClass("hidden");
                        $("#<%=print1.ClientID%>").addClass("hidden");
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");

                        $http.get("webServices/overall-collection.asmx/fetch_overall_fee_collection_by_mode", { params: { "Class_id": class_id, "FromDate": from_date, "ToDate": to_date } }).then(function (response) {
                            $scope.reportMode = response.data;
                        })

                        $("#excelbtnS").removeClass("hidden");
                        $("#<%=print1.ClientID%>").removeClass("hidden");
                    }
                })
            }




            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "Collection-Report.xls"
                });
            }
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>

</asp:Content>
