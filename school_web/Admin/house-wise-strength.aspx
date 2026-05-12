<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="house-wise-strength.aspx.cs" Inherits="school_web.Admin.house_wise_strength" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    House-Wise Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <script src="../assets/Angular/angular.min.js"></script>
    <link href="../assets/dropdownmultiselection/style.css" rel="stylesheet" />
    <script src="../assets/dropdownmultiselection/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/print-custom-std-print.css" rel="stylesheet" type="text/css" />');
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
    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_sections.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
     
    <script src="../assets/js/table2excel.js"></script>
    <style type="text/css">
        .txtcenter {
            text-align: center;
        }

        .fontwt700 {
            font-weight: 700;
        }

        .txtrght {
            text-align: right;
        }


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

        table.dataTable {
            margin-top: 0px !important;
            margin-bottom: 0px !important;
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
                <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">House-Wise Summary</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
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
                    <div class="card">
                        <div class="card-body">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:ListBox ID="ddl_classs" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                    <asp:ListBox ID="ddl_sections" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                                </div>


                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                </div>


                                                <div class="col-sm-2">
                                                    <a id="Export" class="btn btn-primary find-dv-btn" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print">
                                                        <i class='bx bx-printer'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>


                                        <div id="tblPrintIQ" runat="server">
                                            <div class="prnt-dv-wpr printborder" id="tblCustomers">
                                                <div class="pgslry-head-div head" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                    <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                        <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                    </div>
                                                    <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                        <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                            <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                        </h1>
                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            <span style="font-size: 14px; font-weight: bold;">House-Wise Summary
                                                                <asp:Label ID="lbl_class22" runat="server"></asp:Label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="grd-wpr">
                                                    <div class="prnt-dv-wpr printborder">
                                                        <table class="table table-bordered table-hover" style="border: 1px solid #c5c0c0;">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Class</th>
                                                                    <th>Section</th>
                                                                    <th data-ng-repeat="x in reportheaD">{{x.house_name}}</th>  
                                                                    <th>Total Student</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr data-ng-repeat="x in reportHcount">
                                                                    <td>{{$index+1}}</td>
                                                                    <td>{{x.Course_Name}}</td>
                                                                    <td>{{x.Section}}</td>
                                                                    <td data-ng-repeat="item in x.MyHouseCountItem track by $index">{{item.totalStudent}}</td>  
                                                                </tr>
                                                            </tbody> 
                                                            <tr>
                                                                <td colspan="3" style="text-align:right">Total</td>
                                                                <td data-ng-repeat="x in reportheaDttl">{{x.ttlStd}}</td>  
                                                            </tr>
                                                        </table>
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
        <!--end row-->
    </div>


    <asp:HiddenField ID="hd_find_status" runat="server" />
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />

    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            var find_status = $("#<%=hd_find_status.ClientID%>").val();
            var session_id = $("#<%=hd_session_id.ClientID%>").val();
            var session = $("#<%=hd_session.ClientID%>").val();
            var class_id = $("#<%=hd_class_id.ClientID%>").val();
            var section = $("#<%=hd_section.ClientID%>").val();


            if (find_status == "1") {
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");


                $http.get("webServices/student-report.asmx/fetch_house_header", { params: { "Session": session, "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                    $scope.reportheaD = response.data; 
                }).catch(function (error) {
                    // Handle the error here
                    console.error("Error fetching report card data:", error);
                    alert("Something went wrong. Please try again.");
                    $("#intsLoader").addClass("hidden"); // Hide the loader even if there's an error
                });


                $http.get("webServices/student-report.asmx/fetch_classwise_house_summary", { params: { "Session": session, "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                    $scope.reportHcount = response.data;
                    if ($scope.reportHcount == "") {
                        $("#intsLoader").addClass("hidden");
                        $("#tblCustomers").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                    }
                    else {
                        $("#tblCustomers").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        $("#intsLoader").addClass("hidden");
                    }
                }).catch(function (error) {
                    // Handle the error here
                    console.error("Error fetching report card data:", error);
                    alert("Something went wrong. Please try again.");
                    $("#intsLoader").addClass("hidden"); // Hide the loader even if there's an error
                });

                $http.get("webServices/student-report.asmx/fetch_house_final_total", { params: { "Session": session, "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                    $scope.reportheaDttl = response.data;
                }).catch(function (error) {
                    // Handle the error here
                    console.error("Error fetching report card data:", error);
                    alert("Something went wrong. Please try again.");
                    $("#intsLoader").addClass("hidden"); // Hide the loader even if there's an error
                });

            }

            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "House-Wise-Summary.xls"
                });
            }
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>


    <style>
        .hidden {
            display: none !important;
        }
    </style>
</asp:Content>
