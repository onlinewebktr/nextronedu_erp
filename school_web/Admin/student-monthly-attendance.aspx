<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="student-monthly-attendance.aspx.cs" Inherits="school_web.Admin.student_monthly_attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    StudentWise Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../assets/js/table2excel.js"></script>
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
    <style type="text/css">
        .daySunday {
            background: #ff7373 !important;
        }

        .tdwdth {
            width: 65px !important;
            display: inline-block;
            font-weight: 600;
            font-style: inherit;
        }

        .daypresenT {
            background: #5afb3d !important;
        }

        .dayabsenT {
            background: #fff84b !important;
        }

        .dayleavE {
            background: #ffb100 !important;
        }

        .txtcenter {
            text-align: center;
        }

        .notattendances {
            background: #ef91ff !important;
        }

        tfoot, th, thead {
            color: #fff;
        }

        .notesp {
            margin: 0px 0px 5px 0px;
            padding: 2px 5px 2px 5px;
        }

            .notesp span {
                font-weight: 600;
            }

        .headgroup1 {
            background: #c541c7 !important;
        }

        .headgroup2 {
            background: #58aac9 !important;
        }

        .tdgroup1 {
            background: #c5ef96 !important;
        }

        .txtnoWrap {
            white-space: nowrap;
        }

        .wdth100 {
            width: 100%;
            float: left;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_user_id" runat="server" />
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Student Attndance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">StudentWise Attendance</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <div id="notification1">
                    <div id="pan1" class="notificationpan">
                        <div style="float: left; width: 235px; height: auto;">
                            <asp:Label ID="lbl_js_message" runat="server" class="notif-txt"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Year</label>
                                                        <asp:DropDownList ID="ddlyear" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()" style="padding: 7px 10px;">Find</a>
                                                    </div>
                                                    <div class="col-sm-3" style="padding-left: 0px;">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()" style="padding: 7px 10px;"><i class='bx bx-download'></i>Excel</a>


                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px; padding: 7px 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>
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
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                        <div id="tblCustomers">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <p id="textwathfile" class="hidden" style="margin: 0px 0px 2px 0px; padding: 3px 10px 3px 10px; width: 100%; float: left; font-size: 15px; font-weight: 600; letter-spacing: 1px; border: 1px solid #ddd;">
                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table class="table table-striped table-bordered">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th><span class="txtnoWrap">Adm. No.</span></th>
                                                                    <th><span class="txtnoWrap">Class</span></th>
                                                                    <th>Section</th>
                                                                    <th><span class="txtnoWrap">Roll No.</span></th>
                                                                    <th><span class="txtnoWrap">Name</span></th>
                                                                    <th data-ng-repeat="x in reportHeadinG" class="txtcenter {{x.dayNameClass}}" style=""><i class="tdwdth">{{x.daY}} <span style="font-size: 12px; text-transform: uppercase;">({{x.dayName}})</span></i></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Working Days</span></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Holidays</span></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Days</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Present</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Absent</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Leave</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Present(%)</span></th>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportdaY track by $index">
                                                                    <td>{{$index+1}}</td>
                                                                    <td><span class="txtnoWrap">{{x.Admission_no}}</span> </td>
                                                                    <td><span class="txtnoWrap">{{x.Class_name}}</span></td>
                                                                    <td><span class="txtnoWrap">{{x.Section}}</span></td>
                                                                    <td><span class="txtnoWrap">{{x.Roll_no}}</span></td>
                                                                    <td><span class="txtnoWrap">{{x.Student_name}}</span></td>
                                                                    <td style="position: relative" data-ng-repeat="item in x.MyAttendanceSItem track by $index" class="txtcenter {{item.dayNameClass}}"><span class="txtnoWrap">{{item.AttendanceS}}</span> </td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_working_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_holiday_days}}</td>
                                                                    <td class="txtcenter tdgroup1">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_no_of_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_persent_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_absent_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_leave_days}}</td>
                                                                    <td class="txtcenter tdgroup1">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Attendance_perc}}%</td>
                                                                </tr>
                                                            </table>


                                                            <div class="wdth100">
                                                                <p class="notesp" style="background: #ef91ff !important; width: auto; float: left;">
                                                                    <span>*NA :</span>  No Attendance
                                                                </p>
                                                            </div>
                                                            <div class="wdth100">
                                                                <p class="notesp" style="background: #5afb3d  !important; width: auto; float: left;"><span>*P :</span> Present</p>
                                                            </div>
                                                            <div class="wdth100">
                                                                <p class="notesp" style="background: #fff84b !important; width: auto; float: left;"><span>*A : </span>Absent</p>
                                                            </div>
                                                            <div class="wdth100">
                                                                <p class="notesp" style="background: #ffb100 !important; width: auto; float: left;"><span>*L : </span>Leave</p>
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

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                //====FIND
                $scope.ButtonClickFind = function () {
                    var years = $("#<%=ddlyear.ClientID%>").val();
                    var months = $("#<%=ddl_month.ClientID%>").val();
                    var session_id = $("#<%=hd_session_id.ClientID%>").val();
                    var class_id = $("#<%=ddlclass.ClientID%>").val();
                    var section = $("#<%=ddl_section.ClientID%>").val();

                    if (years == "Select") {
                        alert("Please select year.");
                        $("#<%=ddlyear.ClientID%>").focus();
                        return;
                    }
                    if (months == "0") {
                        alert("Please select month.");
                        $("#<%=ddl_month.ClientID%>").focus();
                        return;
                    }
                    if (class_id == "0") {
                        alert("Please select class.");
                        $("#<%=ddlclass.ClientID%>").focus();
                        return;
                    }
                    if (section == "Select") {
                        alert("Please select section.");
                        $("#<%=ddl_section.ClientID%>").focus();
                        return;
                    }
                    $("#textwathfile").addClass("hidden");
                    messge("Please Wait...");
                    $("#intsLoader").removeClass("hidden");
                    $http.get("webServices/attendance.asmx/fetch_attendance_of_monthwise_heading", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Years": years, "Months": months } }).then(function (response) {
                        $scope.reportHeadinG = response.data;
                        if ($scope.reportHeadinG == "") {
                            $("#intsLoader").addClass("hidden");
                            $("#grdsdatA").addClass("hidden");
                            $("#NotFounD").removeClass("hidden");
                        }
                        else {
                            $("#grdsdatA").removeClass("hidden");
                            $("#NotFounD").addClass("hidden");
                            $http.get("webServices/attendance.asmx/fetch_attendance_of_monthwise", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Years": years, "Months": months } }).then(function (response) {
                                $scope.reportdaY = response.data;
                                if ($scope.reportdaY == "") {
                                    $("#intsLoader").addClass("hidden");
                                }
                                else {
                                    $("#intsLoader").addClass("hidden");
                                    $("#textwathfile").removeClass("hidden");
                                    var monthsx = $("#<%=ddl_month.ClientID%> option:selected").text();
                                    var classx = $("#<%=ddlclass.ClientID%> option:selected").text();
                                    var ggg = "Year : " + years + ", Month : " + monthsx + ", Class : " + classx + ", Section : " + section;
                                    document.getElementById('textwathfile').innerHTML = ggg;
                                }
                            })
                        }
                    })
                }

                $scope.Export = function () {
                    $("#tblCustomers").table2excel({
                        filename: "Attendance.xls"
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
