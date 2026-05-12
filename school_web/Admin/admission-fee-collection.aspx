<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="admission-fee-collection.aspx.cs" Inherits="school_web.Admin.admission_fee_collection" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Admission Fees Collection
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
                <div class="breadcrumb-title pe-3">Fees Collections</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Admission Fees Collection</li>
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
                                                    <div class="col-sm-3" id="Div2" runat="server">
                                                        <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Student Type</label>
                                                        <asp:DropDownList ID="ddl_std_type" runat="server" class="form-control find-dv-txtbx">
                                                            <asp:ListItem Value="0">ALL</asp:ListItem>
                                                            <asp:ListItem Value="1">New</asp:ListItem>
                                                            <asp:ListItem Value="2">Old</asp:ListItem>
                                                        </asp:DropDownList>
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
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="float: right">
                                                                <div class="angularfilter">
                                                                    <input type="text" data-ng-model="searchs" class="form-control" style="margin: 0px;" placeholder="type here to filter data" />
                                                                </div>
                                                            </div>

                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th class="hiddenOnPrint"></th>
                                                                        <th>Status</th>
                                                                        <th>Admission No.</th>
                                                                        <th>Name</th>
                                                                        <th>Class</th>
                                                                        <th>Section</th>
                                                                        <th>Roll No.</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>Payable Amount</th>
                                                                        <th>Paid Amount </th>
                                                                        <th>Dues Amount</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr data-ng-repeat="x in reportAmountS | filter : searchs">
                                                                        <td>{{$index+1}}</td>
                                                                        <td class="hiddenOnPrint"><a class="button-61 nowordbreak collect-feesss" href="{{x.Payment_link}}">Collect Fee</a></td>
                                                                        <td>{{x.StdStatus}}</td>
                                                                        <td>{{x.Admission_no}}</td>
                                                                        <td>{{x.Studentname}}</td>
                                                                        <td>{{x.Class_name}}</td>
                                                                        <td>{{x.Section}}</td>
                                                                        <td>{{x.Rollnumber}}</td>
                                                                        <td>{{x.Mobilenumber}}</td>
                                                                        <td>{{x.Payable_amount}}</td>
                                                                        <td>{{x.Paid_amount}}</td>
                                                                        <td>{{x.Dues_amount}}</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
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




            var ddlID = '#' + '<%= ddl_session.ClientID %>';
            var session_name = $(ddlID + " option:selected").text();

            var ddlIDDD = '#' + '<%= ddl_class.ClientID %>';
            var class_name = $(ddlIDDD + " option:selected").text();

            if (session_id == "0") {
                alert("Please choose session.");
                $("#<%=ddl_session.ClientID%>").focus();
                return;
            }



            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            var student_type = $("#<%=ddl_std_type.ClientID%>").val();
            $http.get("webServices/student-report.asmx/fetch_report_of_students", {
                params: { "Session_id": session_id, "Class_id": class_id, "StdType": student_type }
            }).then(function (response) {
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


            //====FIND
            $scope.ButtonClickFind = function () {
                var session_id = $("#<%=ddl_session.ClientID%>").val();
                var class_id = $("#<%=ddl_class.ClientID%>").val();

                var ddlID = '#' + '<%= ddl_session.ClientID %>';
                var session_name = $(ddlID + " option:selected").text();

                var ddlIDDD = '#' + '<%= ddl_class.ClientID %>';
                var class_name = $(ddlIDDD + " option:selected").text();

                var student_type = $("#<%=ddl_std_type.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/student-report.asmx/fetch_report_of_students", { params: { "Session_id": session_id, "Class_id": class_id, "StdType": student_type} }).then(function (response) {
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
            }


            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "Student-Dues-List.xls"
                });
            }
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>
</asp:Content>
