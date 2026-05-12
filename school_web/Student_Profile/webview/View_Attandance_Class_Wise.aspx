<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Attandance_Class_Wise.aspx.cs" Inherits="school_web.Student_Profile.webview.View_Attandance_Class_Wise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Class Wise View Attendance</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />

    <script src="../../js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />

    <script src="../../js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../../assets/Angular/angular.min.js"></script>
    <style>
        .student-card {
      background-color: #e4e3e3;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin-bottom: 15px;
    padding: 15px;
    }

 
    .card-title {
      font-size: 20px;
      font-weight: bold;
      margin-bottom: 20px;
      color: #2c3e50;
      text-align: center;
    }

    .card-info {
      margin-bottom: 10px;
      font-size: 16px;
      color: #34495e;
      text-transform: uppercase;
    }

    .label {
      font-weight: bold;
      color: #555;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <div class="fullinfo" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
            <div style="margin: 0px; padding: 10px; float: left; height: auto; width: 100%; position: relative">
                <div class="container">
                    <div class="row">
                        
                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </p>
                        </div>
                         
                         
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <asp:Button ID="btn_submit" runat="server" Text="Find" class="mt-2 btn btn-primary my-btn my-btn" />
                        </div>
                    </div>
                   
                </div>

                <div class="clearfix"></div>
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-right: 5px; padding-left: 5px;">
                            <div class="texbox-border">
                                <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">
                                    <asp:HiddenField ID="hd_admission_no" runat="server" />
                                    <asp:HiddenField ID="hd_class_id" runat="server" />
                                    <asp:HiddenField ID="hd_session_id" runat="server" />
                                    <asp:HiddenField ID="hd_session_name" runat="server" />
                                    <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                                        <div id="notification1">
                                            <div id="pan1" class="notificationpan">
                                                <div style="float: left; width: 235px; height: auto;">
                                                    <asp:Label ID="lbl_js_message" runat="server" class="notif-txt"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
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
                                            <div class="grd-wpr" style="overflow: auto">
                                                <div id="tblPrintIQ" runat="server">

                                                    <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                        <div id="tblCustomers">
                                                            <div class="monthsdVS" data-ng-repeat="x in reportdaY track by $index">

                                                                 <div class="student-card">
 
    <div class="card-info"><span class="label">Name :</span> <asp:Label ID="lbl_name" runat="server" style="font-size: 12px;"  ></asp:Label></div>
    <div class="card-info"><span class="label">Admission No :</span> <asp:Label ID="lbl_admission_no" runat="server" style="font-size: 12px;"  ></asp:Label></div>
    <div class="card-info"><span class="label">Class :</span> <asp:Label ID="lbl_class" style="font-size: 12px;"  runat="server"  ></asp:Label><span class="label">Roll No.:</span> <asp:Label ID="lbl_roll_no" runat="server" style="font-size: 12px;"  ></asp:Label><span class="label">Section:</span> <asp:Label  style="font-size: 12px;"  ID="lbl_section" runat="server"  ></asp:Label></div>
  <div class="card-info"><span class="label">Attendance of Month :</span> <asp:Label ID="Label1" runat="server" style="font-size: 12px;"  >{{x.Month_name}}</asp:Label></div>
  </div>


                                                               <%-- <table class=" mrgn-brm">
                                                                    <tr>
                                                                        <td >
                                                                            <p class="monthsdVS-p" style="margin: 0px; font-weight: 600;"><span>Attendance of Month </span></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>--%>
                                                                 <div class="student-card">
                                                                <table class="table table-striped table-bordered mrgn-brm" >
                                                                    <tr class="headgroups1" style="display:none">
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Total Working Days</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_working_days}}</td>
                                                                    </tr>
                                                                    <tr class="headgroups2" style="display:none">
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Total Holidays</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_holiday_days}}</td>
                                                                    </tr>
                                                                    <tr class="headgroups3" style="display:none">
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Total Days</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_no_of_days}}</td>
                                                                    </tr>
                                                                    <tr class="headgroups4"  >
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Total Present</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_persent_days}}</td>
                                                                    </tr>
                                                                    <tr class="headgroups5">
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Total Absent</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_absent_days}}</td>
                                                                    </tr>
                                                                    <tr class="headgroups6">
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Total Leave</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_leave_days}}</td>
                                                                    </tr>
                                                                     <tr class="headgroups6" style="background-color: #ef91ff !important;">
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Not Marked By Teacher</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].total_nottackenby}}</td>
                                                                    </tr>
                                                                    <tr class="headgroups7" style="display:none">
                                                                        <td class="txtcenter fntstyles"><span class="txtnoWrap">Present(%)</span></td>
                                                                        <td class="txtcenter fntstyles">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Attendance_perc}}%</td>
                                                                    </tr>
                                                                </table>

                                                                     </div>
                                                                <table class=" mrgn-brm" style="display:none">
                                                                    <tr>
                                                                        <td>
                                                                            <p class="monthsdVS-p" style="margin: 5px 0px -5px 0px; font-weight: 600; font-size: 14px;">
                                                                                <span>Attendance Summary</span>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table class="table table-striped table-bordered mrgn-brm">
                                                                    <tr>
                                                                        <th>Day</th>
                                                                        <th>Attendance</th>
                                                                        <%--<th data-ng-repeat="item in x.MyStudentWiseAttendanceSItem track by $index" class="txtcenter {{item.dayNameClassHead}}"><i class="tdwdth">{{item.daYHead}} <span style="font-size: 12px; text-transform: uppercase;">({{item.dayNameHead}})</span></i></th>
                                                                        <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Working Days</span></th>
                                                                        <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Holidays</span></th>
                                                                        <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Days</span></th>
                                                                        <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Present</span></th>
                                                                        <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Absent</span></th>
                                                                        <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Leave</span></th>
                                                                        <th class="txtcenter headgroup2"><span class="txtnoWrap">Present(%)</span></th>--%>
                                                                    </tr>
                                                                    <tr data-ng-repeat="item in x.MyStudentWiseAttendanceSItem track by $index">
                                                                        <td style="position: relative" class="txtcenter {{item.dayNameClass}}"><span class="txtnoWrap"><i class="tdwdth">{{item.daYHead}}
                                                                            <br />
                                                                            <span style="font-size: 12px; text-transform: uppercase;">({{item.dayNameHead}})</span></i></span></td>
                                                                        <td style="position: relative" class="txtcenter {{item.dayNameClass}}"><span class="txtnoWrap">{{item.AttendanceS}}</span>
                                                                            <br />
                                                                            <span class="{{item.Holidaydetails}}">({{item.Holidaydetails}})</span> </td>
                                                                        <%--<td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_working_days}}</td>
                                                                        <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_holiday_days}}</td>
                                                                        <td class="txtcenter tdgroup1">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_no_of_days}}</td>
                                                                        <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_persent_days}}</td>
                                                                        <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_absent_days}}</td>
                                                                        <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_leave_days}}</td>
                                                                        <td class="txtcenter tdgroup1">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Attendance_perc}}%</td>--%>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>


                                                        <%--<div class="wdth100">
                                                            <p class="notesp" style="background: #ef91ff !important; width: auto; float: left;">
                                                                <span>*NA :</span>  Not Attendance
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
                                                        </div>--%>
                                                    </div>


                                                    <div class="notFound hidden" id="NotFounD">
                                                        <p>No record found.</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>




        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                //====FIND

                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var session_name = $("#<%=hd_session_name.ClientID%>").val();
                var monthId = $("#<%=ddl_month.ClientID%>").val();

                if (admission_no != "XZXZX") {
                    messge("Please Wait...");
                    $("#intsLoader").removeClass("hidden");

                    $http.get("webService/attendance.asmx/fetch_attendance_of_monthwise_studentwise", { params: { "Session_id": session_id, "Session_name": session_name, "Class_id": class_id, "Admission_no": admission_no, "MonthId": monthId } }).then(function (response) {
                        $scope.reportdaY = response.data;
                        if ($scope.reportdaY == "") {
                            $("#intsLoader").addClass("hidden");
                            $("#grdsdatA").addClass("hidden");
                            $("#NotFounD").removeClass("hidden");
                        }
                        else {
                            $("#intsLoader").addClass("hidden");
                            $("#grdsdatA").removeClass("hidden");
                            $("#NotFounD").addClass("hidden");
                            //var ggg = "Year : " + years + ", Month : " + monthsx + ", Class : " + classx + ", Section : " + section;
                            //document.getElementById('textwathfile').innerHTML = ggg;
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



        <style type="text/css">
            .daySunday {
                background: #ff7373 !important;
            }

            .tdwdth {
                display: inline-block;
                font-weight: 600;
                font-style: inherit;
                line-height: 24px;
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

            .monthsdVS {
                margin: 7px 0px 7px 0px;
                padding: 5px 5px;
                width: 100%;
                float: left;
            }

            .monthsdVS-p {
                margin: 0px 0px 5px 0px;
                padding: 0px;
                width: 100%;
                float: left;
                font-size: 16px;
                font-weight: 500;
                text-decoration: underline;
            }

                .monthsdVS-p span {
                    margin: 0px;
                    padding: 0px;
                }

            .mrgn-brm {
                margin: 0px;
            }

            /* .txtnoWrap {
            white-space: nowrap;
        }*/

            .std-infos {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
            }

                .std-infos table {
                    width: 100%;
                }

                    .std-infos table tr th {
                        padding: 5px 10px !important;
                        background: #f5fff6 !important;
                        color: #000;
                        font-size: 16px;
                        font-weight: 600;
                        border: 1px solid #ddd;
                    }

                    .std-infos table tr td {
                        padding: 5px 10px;
                        border: 1px solid #ddd;
                    }

            .wdth100 {
                width: 100%;
                float: left;
            }

            .ints-loader-wpr {
                display: none;
                margin: 0;
                padding: 0;
                width: 100%;
                height: auto;
                float: left;
                background: #ffffff80;
                position: fixed;
                z-index: 9999999999;
                left: 0;
                bottom: 0;
                top: 0;
            }

            .ints-loader-wpr-inr {
                margin: 0;
                padding: 0;
                width: 100%;
                height: 100%;
                float: left;
                display: flex;
                align-items: center;
                justify-content: center;
            }

            .ints-loader {
                background-color: #fff;
                padding: 6px 15px;
                height: auto;
                -webkit-border-radius: 9px 9px 9px 9px;
                border-radius: 2px;
                border: 1px solid #fff;
                box-shadow: 0 3px 14px 1px rgba(92,91,91,.85);
            }

            .ints-loader-txt {
                color: #414141;
                font-weight: 500;
                font-size: 16px;
                z-index: 999999;
                position: relative;
                letter-spacing: 1px;
                margin: 0;
                text-align: center;
            }

            .ints-loader-img {
                width: 30px;
                margin: 0 8px 0 0;
            }

            .hidden {
                display: none;
            }

            table tr th {
                padding: 5px 5px !important;
                vertical-align: middle !important;
                border-top: 1px solid #ddd;
                text-align: center;
                background: #0300d9;
                font-size: 13px;
            }

            table tr td {
                padding: 5px 5px !important;
                vertical-align: middle !important;
                text-align: center !important;
                font-size: 13px;
            }

            .fntstyles {
                font-weight: 600;
                color: #ffffff;
            }


            .headgroups1 {
                background: #c541c7 !important;
            }

            .headgroups2 {
                background: #cd8400 !important;
            }

            .headgroups3 {
                background: #41c7a2 !important;
            }

            .headgroups4 {
                background: #50cf00 !important;
            }

            .headgroups5 {
                background: #e7dd00 !important;
            }

            .headgroups6 {
                background: #58aac9 !important;
            }

            .headgroups7 {
                background: #889100 !important;
            }

            .headgroups7 {
                background: #58aac9 !important;
            }
        </style>

    </form>
</body>
</html>
