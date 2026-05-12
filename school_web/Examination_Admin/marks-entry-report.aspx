<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="marks-entry-report.aspx.cs" Inherits="school_web.Examination_Admin.marks_entry_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Mark's Entry Status Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <link href="../assets/dropdownmultiselection/style.css" rel="stylesheet" />
    <script src="../assets/dropdownmultiselection/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_classs.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

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


    <style type="text/css">
        .greenColor {
            color: #007400;
        }

        .redColor {
            color: #f10000;
        }

        table tr td {
            border: 1px solid #dee2e6;
            padding: 4px 5px;
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
                <div class="breadcrumb-title pe-3">Reports</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Mark's Entry Status Report</li>
                        </ol>
                    </nav>
                </div>
            </div>


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
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:ListBox ID="ddl_classs" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Term</label>
                                                <asp:DropDownList ID="ddl_term" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Exam</label>
                                                <asp:DropDownList ID="ddl_exam" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>

                                            <div class="col-sm-2">
                                                <%--<a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>--%>

                                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                    ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                            </div>
                                            <%--<div class="col-sm-2">
                                                <asp:Button ID="btn_print_all" runat="server" Text="Print All" class="btn btn-primary find-dv-btn" OnClick="btn_print_all_Click" Style="float: right" />
                                            </div>--%>
                                        </div>
                                    </div>
                                    <div class="grd-wpr" style="min-height: 500px;">
                                        <div id="tblPrintIQ" runat="server">
                                            <div class="prnt-dv-wpr printborder" id="tblCustomers">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Class</th>
                                                        <th>Subject</th>
                                                        <th>Teacher</th>
                                                        <th>Status</th>
                                                    </tr>
                                                    <tr data-ng-repeat="x in reportTecher">
                                                        <td>{{$index+1}}</td>
                                                        <td>{{x.Class_name}}-{{x.Section}}</td>
                                                        <td>{{x.Subject_name}}</td>
                                                        <%--<td>{{x.Assessment_Name}}</td>--%>
                                                        <td>{{x.Teacher_name}}({{x.UserID}})</td>
                                                        <td><span data-ng-repeat="item in x.MysubjStstusList track by $index" class="{{item.Colors}} {{item.Single_unit}}">{{item.Status}}</span>

                                                            <table>
                                                                <tr data-ng-repeat="item in x.MysubjStstusList track by $index" class="{{item.Multile_unit}}">
                                                                    <td class="{{item.Colors}}">{{item.Unit_name}}</td>
                                                                    <td class="{{item.Colors}}">{{item.Status}}</td>
                                                                </tr>
                                                            </table>
                                                        </td>
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
            </div>
        </div>
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_find_status" runat="server" />
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_term_id" runat="server" />
    <asp:HiddenField ID="hd_exam" runat="server" />

    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {

            var find_status = $("#<%=hd_find_status.ClientID%>").val();
            var session_id = $("#<%=hd_session_id.ClientID%>").val();
            var class_id = $("#<%=hd_class_id.ClientID%>").val();
            var term_id = $("#<%=hd_term_id.ClientID%>").val();
            var exam_name = $("#<%=hd_exam.ClientID%>").val();

            if (find_status == "1") {
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("webService/teacher-mark-status.asmx/fetch_report_mark_status_of_teacher", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Exam_name": exam_name } }).then(function (response) {
                    $scope.reportTecher = response.data;
                    $("#intsLoader").addClass("hidden");
                })

                $("#intsLoader").addClass("hidden");
            }

            //$http.get("webService/table-sheet.asmx/fetch_report_maxmark", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Subject_id": subject_id } }).then(function (response) {
            //    $scope.reportHeadinGMaxM = response.data;
            //})


            //$http.get("webService/table-sheet.asmx/fetch_report_marks_with_student", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Subject_id": subject_id, "Section": section } }).then(function (response) {
            //    $scope.reportMarksR = response.data;
            //    $("#intsLoader").addClass("hidden");
            //    document.getElementById('GFG').innerHTML = 'Class : ' + Class_name + ', Section : ' + section + ', Term : ' + Term_name + ', Subject : ' + Subject_name;

            //})

            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "MarksEntryStatus.xls"
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
