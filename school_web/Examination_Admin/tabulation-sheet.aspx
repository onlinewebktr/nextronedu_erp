<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="tabulation-sheet.aspx.cs" Inherits="school_web.Examination_Admin.tabulation_sheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Tabulation Sheet
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <script src="../assets/js/table2excel.js"></script>
    <link href="slip/assets/css/tabulationSheet.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="slip/assets/css/tabulationSheet.css" rel="stylesheet" type="text/css" />');
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
                            <li class="breadcrumb-item active" aria-current="page">Tabulation Sheet</li>
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
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Term</label>
                                                        <asp:DropDownList ID="ddl_term" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
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
                                            <div class="grd-wpr hidden" id="tbultionSheetDV">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="prnt-dv-wpr printborder" id="tblCustomers">
                                                            <p id="GFG" class="title-rp"></p>
                                                            <table class="table  table-bordered" style="width: 100%">
                                                                <tr>
                                                                    <td rowspan="2">Admission No.</td>
                                                                    <td rowspan="2">Name</td>
                                                                    <td rowspan="2">Roll No.</td>

                                                                    <th data-ng-repeat="x in reportAssmentHeadinG" colspan="{{x.Ttl_subject}}" class="{{x.Bg_colors}}">{{x.Assessment_Name}} </th>

                                                                    <th colspan="{{reportAssmentHeadinGCoscholastic[0].RowCounTS}}" class="nowordbreak {{reportAssmentHeadinGCoscholastic[0].Bg_colors}}">CO-SCHOLASTIC</th>
                                                                    <th colspan="{{reportAssmentHeadinGDescpline[0].RowCounTS}}" class="nowordbreak {{reportAssmentHeadinGDescpline[0].Bg_colors}}">DISCIPLINE</th>
                                                                </tr>
                                                                <tr>
                                                                    <th data-ng-repeat="x in reportSubjectHeadinG" class="subjth {{x.Bg_colors}}">
                                                                        <p class="rotateTextS">{{x.Subject_name}} <span>({{x.Subject_id}})</span></p>
                                                                    </th>

                                                                    <th data-ng-repeat="x in reportAssmentHeadinGCoscholastic" class="subjth {{x.Bg_colors}}">
                                                                        <p class="rotateTextS">{{x.Subject_name}} <span>({{x.Subject_code}})</span></p>
                                                                    </th>

                                                                    <th data-ng-repeat="x in reportAssmentHeadinGDescpline" class="subjth {{x.Bg_colors}}">
                                                                        <p class="rotateTextS">{{x.Subject_name}} <span>({{x.Subject_code}})</span></p>
                                                                    </th>
                                                                </tr>


                                                                <tr data-ng-repeat="x in reportSubjectMarkS track by $index">
                                                                    <td><span class="nowordbreak">{{x.Admission_no}}</span></td>
                                                                    <td><span class="nowordbreak">{{x.studentname}}</span></td>
                                                                    <td><span class="nowordbreak">{{x.rollnumber}}</span></td>


                                                                    <td data-ng-repeat="item in x.MySubjectMarksItem track by $index">{{item.Marks}}</td>
                                                                    <td data-ng-repeat="items in x.MySubjectTotalMarksItem track by $index">{{items.Total_subject_marks}}</td>
                                                                    <td data-ng-repeat="items in x.MySubjectTotalGradeItem track by $index">{{items.subject_grade}}</td>
                                                                    <td data-ng-repeat="items in x.MySubjectMarksItemCS track by $index">{{items.Marks}}</td>
                                                                    <td data-ng-repeat="items in x.MySubjectMarksItemDesc track by $index">{{items.Marks}}</td>
                                                                </tr>
                                                                <%--<tr data-ng-repeat="item in x.MySubjectMarkShowItemII track by $index">
                                                                    <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{$index+1}}</td>
                                                                    <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">{{item.Subject}}</td>
                                                                    <td>{{item.Subject_name}}</td>
                                                                    <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIII track by $index">{{itemx.Marks}}</td>
                                                                    <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Total_marks}}</td>
                                                                    <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{item.Grade}}">{{item.Grade}}</td>
                                                                </tr>--%>
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

    <asp:HiddenField ID="hd_branch" runat="server" />
    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {






            //====FIND
            $scope.ButtonClickFind = function () {
                var session_id = $("#<%=ddl_session.ClientID%>").val();
                var class_id = $("#<%=ddl_class.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();
                var term_id = $("#<%=ddl_term.ClientID%>").val();
                var branch_id = $("#<%=hd_branch.ClientID%>").val();





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
                else {
                    var session = document.getElementById('ddl_session');
                    var Session_name = session.options[session.selectedIndex].text;


                    var classss = document.getElementById('ContentPlaceHolder1_ddl_class');
                    var Class_name = classss.options[classss.selectedIndex].text;

                    var terms = document.getElementById('ContentPlaceHolder1_ddl_term');
                    var Term_name = terms.options[terms.selectedIndex].text;


                    messge("Please Wait...");
                    $("#intsLoader").removeClass("hidden");

                    $http.get("webService/tabulation-sheet.asmx/fetch_tabulation_assesment", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Section": section } }).then(function (response) {
                        $scope.reportAssmentHeadinG = response.data;
                    })

                    $http.get("webService/tabulation-sheet.asmx/fetch_tabulation_subjects", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Section": section } }).then(function (response) {
                        $scope.reportSubjectHeadinG = response.data;
                    })


                    $http.get("webService/tabulation-sheet.asmx/fetch_tabulation_student_marks", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Section": section } }).then(function (response) {
                        $scope.reportSubjectMarkS = response.data;
                        document.getElementById('GFG').innerHTML = 'Session : ' + Session_name + ', Class : ' + Class_name + ', Section : ' + section + ', Term : ' + Term_name ;
                        $("#tbultionSheetDV").removeClass("hidden");
                        $("#intsLoader").addClass("hidden");

                    })


                    //======================
                    $http.get("webService/tabulation-sheet.asmx/fetch_tabulation_coscholastic", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Section": section, "Branch_id": branch_id } }).then(function (response) {
                        $scope.reportAssmentHeadinGCoscholastic = response.data;
                    })

                    //======================
                    $http.get("webService/tabulation-sheet.asmx/fetch_tabulation_Descpline", { params: { "Session_id": session_id, "Class_id": class_id, "Term_id": term_id, "Section": section, "Branch_id": branch_id } }).then(function (response) {
                        $scope.reportAssmentHeadinGDescpline = response.data;
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
