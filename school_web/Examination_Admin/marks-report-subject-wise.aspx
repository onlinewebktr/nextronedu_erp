<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="marks-report-subject-wise.aspx.cs" Inherits="school_web.Examination_Admin.marks_report_subject_wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Subject wise Marks Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/print-report.css" rel="stylesheet" type="text/css" />');
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

        tfoot, th, thead {
            background: #ffffff!important;
            font-size: 13px;
            font-weight: 500;
            text-align: center;
        }

        tbody, td, tfoot, th, thead, tr {
            text-align: center;
        }

        .title-rp {
            font-size: 16px;
            font-weight: 500;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 5px 0px;
            float: left;
            width: 100%;
            text-align: center;
        }

        .thbg1 {
            background: #ebebeb !important;
        }

        .thbg2 {
            background: #fff5aa !important;
        }

        .thbg3 {
            background: #9eebf7 !important;
        }

        .thbg4 {
            background: #f2b8fd !important;
        }

        .thbg5 {
            background: #a9f7be !important;
        }

        .thbg6 {
            background: #bacff5 !important;
        }


        .thbg12 {
            background: #ffbcbc !important;
        }

        .thbg13 {
            background: #22ddca !important;
        }

        .thbg14 {
            background: #a6ff81 !important;
        }

        .thbg15 {
            background: #ffb860 !important;
        }



        .thbg16 {
            background: #afff8e !important;
        }

        .thbg17 {
            background: #b9c5ff !important;
        }

        @media print {
            thead {
                display: table-header-group;
            }

            tfoot {
                display: table-footer-group;
            }
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
                <div class="breadcrumb-title pe-3">Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Subject wise Marks Report</li>
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
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">Term</label>
                                                                <asp:DropDownList ID="ddl_term" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">Subject</label>
                                                                <asp:DropDownList ID="ddl_subject" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="col-sm-1">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Find</a>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>

                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="prnt-dv-wpr printborder" id="tblCustomers">
                                                            <p id="GFG" class="title-rp"></p>
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="thbg1">#</th>
                                                                        <th class="thbg1">Student Name</th>
                                                                        <th class="thbg1">Admission No.</th>
                                                                        <th class="thbg1">Roll No.</th>
                                                                        <th data-ng-repeat="x in reportHeadinG" colspan="{{x.ColSpan}}" class="{{x.bgColor}}">{{x.Assesment_head}}</th>
                                                                        <th class="thbg16">Total</th>
                                                                        <th rowspan="2" class="thbg17">Grade</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th class="thbg1" colspan="4">Maximum Marks</th>
                                                                        <th data-ng-repeat="x in reportHeadinGMaxM" class="{{x.bgColor}}">{{x.MaxMarks}}</th>
                                                                        <th class="thbg16">{{reportMarksR[0].Full_mark}}</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr data-ng-repeat="x in reportMarksR track by $index">
                                                                        <td>{{$index+1}}</td>
                                                                        <td>{{x.Student_name}}</td>
                                                                        <td>{{x.Admission_no}}</td>
                                                                        <td>{{x.Roll_no}}</td>
                                                                        <td data-ng-repeat="item in x.MyStudentMaxMarkList track by $index">{{item.ResultMark}}</td>
                                                                        <td>{{x.Total_marks}}</td>
                                                                        <td>{{x.Grade}}</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                        <%--<div class="notFound hidden" id="NotFounD">
                                                            <p>No record found.</p>
                                                        </div>--%>
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






            //====FIND
            $scope.ButtonClickFind = function () {
                var session_id = $("#<%=ddl_session.ClientID%>").val();
                var class_id = $("#<%=ddl_class.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();
                var term_id = $("#<%=ddl_term.ClientID%>").val();
                var subject_id = $("#<%=ddl_subject.ClientID%>").val();

                //var session = document.getElementById('ddl_session');
                //var Session_name = session.options[session.selectedIndex].text;
                //alert(Session_name);

                var classss = document.getElementById('ContentPlaceHolder1_ddl_class');
                var Class_name = classss.options[classss.selectedIndex].text;

                var terms = document.getElementById('ContentPlaceHolder1_ddl_term');
                var Term_name = terms.options[terms.selectedIndex].text;
                // alert(Term_name);

                var subjecs = document.getElementById('ContentPlaceHolder1_ddl_subject');
                var Subject_name = subjecs.options[subjecs.selectedIndex].text;


                //alert(subject_id);
                if (session_id == "0") {
                    alert("Please select session.");
                }
                else if (class_id == "0") {
                    alert("Please select class.");
                }
                else if (section == "Select") {
                    alert("Please select section.");
                }
                else if (term_id == "0") {
                    alert("Please select term.");
                }
                else if (subject_id == "0") {
                    alert("Please select subject.");
                }
                else { 
                    messge("Please Wait...");
                    $("#intsLoader").removeClass("hidden");

                    $http.get("webService/table-sheet.asmx/fetch_report_mark_heading", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Subject_id": subject_id } }).then(function (response) {
                        $scope.reportHeadinG = response.data;
                    })

                    $http.get("webService/table-sheet.asmx/fetch_report_maxmark", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Subject_id": subject_id } }).then(function (response) {
                        $scope.reportHeadinGMaxM = response.data;
                    })


                    $http.get("webService/table-sheet.asmx/fetch_report_marks_with_student", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Subject_id": subject_id, "Section": section } }).then(function (response) {
                        $scope.reportMarksR = response.data;
                        $("#intsLoader").addClass("hidden");
                        document.getElementById('GFG').innerHTML = 'Class : ' + Class_name + ', Section : ' + section + ', Term : ' + Term_name + ', Subject : ' + Subject_name;

                    })
                }
            }


            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "SubjectMarks.xls"
                });
            }
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>
</asp:Content>
