<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="day-end-report-summery-headwise.aspx.cs" Inherits="school_web.Admin.day_end_report_summery_headwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Day End Report Summery 
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
                            <li class="breadcrumb-item active" aria-current="page">Day End Report Summary</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row">
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
                        <li><a href="day-end-report-of-admission-fee.aspx"><%--Day End Report of --%>Admission Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-annual-fee.aspx"><%--Day End Report of --%>Annual Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-form-sale.aspx">Form Sale Report</a></li>
                        <li><a href="day_End_Report_Summery_N.aspx">Day End Summary</a></li>
                        <li><a href="day-end-report-summery-headwise.aspx" class="sub-mnu-p-a-active">Day End Summary Headwise</a></li>
                        <li><a href="Fee_Collection_Report.aspx">Fees Collection Summary</a></li>
                        <li><a href="userwise-payment-collection-report.aspx">User Wise</a></li>
                    </ul>
                </div>
                <div class="col-xl-12" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row  g-3 needs-validation">
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddl_class" Visible="false" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
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
                                                        <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div id="excel" runat="server" visible="false">
                                                            <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                                        </div>

                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <label for="validationCustom01" class="form-label Llabel">Class</label>
                                                        <span class="chkbx-all allCheckbox">
                                                            <asp:CheckBox ID="selectAllCheckbox" Checked="true" onclick="toggle(this);" runat="server" Text="Select All" />
                                                        </span>
                                                        <br />
                                                        <asp:Repeater ID="rd_view" runat="server">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkContainer" Checked="true" class="checkboxes chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                                                <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>

                                            <script type="text/javascript"> 
                                                function toggle(source) {
                                                    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
                                                    for (var i = 0; i < checkboxes.length; i++) {
                                                        if (checkboxes[i] != source)
                                                            checkboxes[i].checked = source.checked;
                                                    }
                                                }
                                            </script>


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
                                                                    <span style="font-size: 14px; font-weight: bold;">Day end report for -
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="prnt-dv-wpr printborder" id="grdsdatA">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 noPrint" style="float: right">
                                                                <div class="angularfilter">
                                                                    <input type="text" data-ng-model="searchs" class="form-control" style="margin: 0px;" placeholder="type here to filter data" />
                                                                </div>
                                                            </div>
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Date</th>
                                                                        <th>Invoice No.</th>
                                                                        <th>Adm. No.</th>
                                                                        <th>Name</th>
                                                                        <th>Class</th>
                                                                        <th>Section</th>
                                                                        <th>Roll no.</th>
                                                                        <th>Month</th>
                                                                        <th data-ng-repeat="x in reportHeadinG">{{x.Content}}</th>
                                                                        <th>Total</th>
                                                                        <th>Mode</th>
                                                                        <th>By</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr data-ng-repeat="x in reportAmountS | filter : searchs">
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
                                                                        <td>{{x.PaidFesAmt}}</td>
                                                                        <td>{{x.Payment_mode}}</td>
                                                                        <td>{{x.IssuedBy}}</td>
                                                                    </tr>
                                                                    <tr data-ng-repeat="x in reportAmountSFnal track by $index">
                                                                        <td colspan="9" class="fntbold" style="text-align: right">Total : </td>
                                                                        <td class="fntbold" data-ng-repeat="item in x.MyFeeReportOverAllItem track by $index">{{item.HeadFees}}</td>
                                                                        <td class="fntbold">{{x.TotaLFinaLPaidFeeS}}</td>
                                                                        <td class="fntbold">-</td>
                                                                        <td class="fntbold">-</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                            <div class="paid-cat-div">
                                                                <ul class="mode-ul">
                                                                    <li data-ng-repeat="x in reportmodes"><i>{{x.Payment_mode}} </i>: <span>{{x.Amounts}}</span></li>
                                                                </ul>
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

    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_class_name" runat="server" />

    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {

            var session_id = $("#<%=ddl_session.ClientID%>").val();
            var class_id = $("#<%=hd_class_id.ClientID%>").val();


            var ddlID = '#' + '<%= ddl_session.ClientID %>';
            var session_name = $(ddlID + " option:selected").text();


            var class_name = $("#<%=hd_class_name.ClientID%>").val();


            var Fromdates = $("#<%=txt_from_date.ClientID%>").val();
            var Todates = $("#<%=txt_to_date.ClientID%>").val();

            //alert(session_name); alert(class_name);
            var type = "MonthlyFee";
            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            $http.get("webServices/dayEndReport.asmx/fetch_report_heading_day_end", {
                params: { "Sessionid": session_id, "Classid": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Sessionname": session_name }
            }).then(function (response) {
                $scope.reportHeadinG = response.data;
            })



            $http.get("webServices/dayEndReport.asmx/fetch_report_heading_day_end_amts", { params: { "Sessionid": session_id, "Classid": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Sessionname": session_name } }).then(function (response) {
                $scope.reportAmountS = response.data;

                $("#intsLoader").addClass("hidden");
                if ($scope.reportAmountS == "") {
                    $("#grdsdatA").addClass("hidden");
                    $("#NotFounD").removeClass("hidden");
                    $("#excelbtnS").addClass("hidden");
                    $("#<%=print1.ClientID%>").addClass("hidden");
                }
                else {

                    $http.get("webServices/dayEndReport.asmx/fetch_report_heading_day_end_final_amts", { params: { "Sessionid": session_id, "Classid": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Sessionname": session_name } }).then(function (response) {
                        $scope.reportAmountSFnal = response.data;
                    })

                    $http.get("webServices/dayEndReport.asmx/fetch_report_modewise", { params: { "Sessionid": session_id, "Classid": class_id, "FromDate": Fromdates, "ToDate": Todates, "Type": type, "Sessionname": session_name } }).then(function (response) {
                        $scope.reportmodes = response.data;
                    })

                    $("#grdsdatA").removeClass("hidden");
                    $("#NotFounD").addClass("hidden");
                    $("#excelbtnS").removeClass("hidden");
                    $("#<%=print1.ClientID%>").removeClass("hidden");
                }
            })




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
