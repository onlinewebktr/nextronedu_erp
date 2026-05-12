<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="Student_Attendance_Summary_class_wise.aspx.cs" Inherits="school_web._adminETutorProf.webview.Student_Attendance_Summary_class_wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Attendance Summary Class Wise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
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

    <div class="fullinfo">
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="col-lg-12">
            <div class="main-card mb-3 card">


                <div class="card-body" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                    <div class="form-row">
                        <table style="margin: 0px; padding: 0px; float: left; width: 100%;font-size: 15px;">
                            <tr>
                                <td>Year
                                </td>
                                <td>Month
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlyear" runat="server" class="form-select"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_month" runat="server" class="form-select"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Class
                                </td>
                                <td>Section
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-select"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()" style="padding: 7px 10px !important;
    font-size: 13px!important;">Find</a>
                                </td>
                            </tr>
                        </table>











                    </div>
                    <hr />


                    <div style="width: 350px; float: left; height: auto; margin: 0px; overflow: scroll">
                        <div class="ints-loader-wpr" id="intsLoader">
                            <div class="ints-loader-wpr-inr">
                                <div class="ints-loader">
                                    <p class="ints-loader-txt">

                                        <img src="../../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
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
                                                    <p id="textwathfile" class="hidden" style="margin: 0px 0px 2px 0px; padding: 3px 10px 3px 10px; width: 100%; float: left; font-size: 13px; font-weight: 600; letter-spacing: 1px; border: 1px solid #ddd;">
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
                                                <th style="display:none" data-ng-repeat="x in reportHeadinG" class="txtcenter {{x.dayNameClass}}"  ><i class="tdwdth">{{x.daY}} <span style="font-size: 12px; text-transform: uppercase;">({{x.dayName}})</span></i></th>

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
                                                <td style="position: relative; display:none" data-ng-repeat="item in x.MyAttendanceSItem track by $index" class="txtcenter {{item.dayNameClass}}"><span class="txtnoWrap">{{item.AttendanceS}}</span> </td>
                                                <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_working_days}}</td>
                                                <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_holiday_days}}</td>
                                                <td class="txtcenter tdgroup1">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_no_of_days}}</td>
                                                <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_persent_days}}</td>
                                                <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_absent_days}}</td>
                                                <td class="txtcenter">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Total_leave_days}}</td>
                                                <td class="txtcenter tdgroup1">{{x.MyAttendanceSItem[x.MyAttendanceSItem[0].Total_no_of_days_less_one].Attendance_perc}}%</td>
                                            </tr>
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
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <script type="text/javascript">



        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            pageload();
            $scope.ButtonClickFind = function () {
              
                pageload();
            }
            function pageload() {
             

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
                $http.get("../../Admin/webServices/attendance.asmx/fetch_attendance_of_monthwise_heading", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Years": years, "Months": months } }).then(function (response) {
                    $scope.reportHeadinG = response.data;
                    if ($scope.reportHeadinG == "") {
                        $("#intsLoader").addClass("hidden");
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        $http.get("../../Admin/webServices/attendance.asmx/fetch_attendance_of_monthwise", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Years": years, "Months": months } }).then(function (response) {
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
