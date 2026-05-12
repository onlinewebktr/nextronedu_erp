<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="teacherwise-class-attendance-report.aspx.cs" Inherits="school_web.Admin.teacherwise_class_attendance_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Teacherwise Attendance Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <style>
        tbody, td, tfoot, th, thead, tr {
            font-size: 14px;
        }

        tfoot, th, thead {
            color: #fff;
            background: #13b1f3 !important;
        }

        .periodTimes {
            font-size: 11px;
        }

        .dys-monday {
            background: #9DCD33 !important;
            color: #fff;
        }

        .dys-tuesday {
            background: #F5A12F !important;
            color: #fff;
        }

        .dys-wednesday {
            background: #0698DB !important;
            color: #fff;
        }

        .dys-thursday {
            background: #CBDC46 !important;
            color: #fff;
        }

        .dys-friday {
            background: #FA69FA !important;
            color: #fff;
        }

        .dys-saturday {
            background: #33cd78 !important;
            color: #fff;
        }

        .BreakStyle {
            width: 10px;
            display: inline-block;
            text-transform: uppercase;
            font-weight: 500;
            font-size: 20px;
        }

        .BreakStyleTd {
            background: #FFFFC9 !important;
        }

        .r-day-date {
            margin: 0px;
            text-transform: uppercase;
        }

        .r-day {
            margin: 7px 0px 7px 0px;
            padding: 0px 7px 7px 7px;
            float: left;
            width: 100%;
            text-transform: uppercase;
            border-bottom: 1px solid #ddd;
        }

        .r-day-teacher {
            margin: 0px;
            text-transform: uppercase;
        }

        .teachers-names {
            margin: 7px 0px 7px 0px;
            padding: 7px 0px 0px 0px;
            float: left;
            width: 100%;
            text-transform: uppercase;
            border-top: 1px solid #ddd;
        }

        .editbttns {
            margin: 0px;
            padding: 0px;
            position: absolute;
            top: -1px;
            right: 2px;
            font-size: 17px;
            color: #ff5200;
        }

        .modal {
            background: rgb(0 0 0 / 53%);
        }

        .modal-dialog {
            top: 10%;
        }

        .modal-dialog {
            max-width: 800px;
        }

        .modal-header {
            padding: 8px 15px !important;
        }

        .mdl-txtbx-tyle {
            margin: 3px 0px 15px 0px;
            float: left;
            width: 100%;
            padding: 5px 5px;
        }

        .NotEditable {
            color: #d7d6d5;
            cursor: no-drop;
            pointer-events: none;
        }

        .assignedtblwpr {
            margin: 0px;
            padding: 10px 15px;
            width: 100%;
            float: left;
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

        .conf-alrt-sec {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 999999;
            background: rgba(0, 0, 0, 0.26);
        }

        .conf-alrt-inr {
            position: relative;
            top: 30%;
            margin: 0px auto;
            border-radius: 2px;
            padding: 20px;
            width: 300px;
            height: auto;
            background: #fff;
            -webkit-transition: -webkit-transform .3s ease-out;
            -o-transition: -o-transform .3s ease-out;
            transition: transform .3s ease-out;
            -webkit-transform: translate(0,-25%);
            -ms-transform: translate(0,-25%);
            -o-transform: translate(0,-25%);
            transform: translate(0,-25%);
            -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
            box-shadow: 0 5px 15px rgba(0,0,0,.5);
        }

        .conf-alrt-msg-p {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            font-size: 15px;
            color: #333;
            letter-spacing: .5px;
        }

        .conf-btn-ul {
            margin: 15px 0px 37px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            text-align: right;
        }

            .conf-btn-ul li {
                margin: 0px;
                padding: 0px;
                list-style-type: none;
                display: inline;
            }

                .conf-btn-ul li a {
                    margin: 0px 5px;
                    padding: 0px 10px 1px;
                    text-decoration: none;
                    background: #0072ff;
                    color: #fff;
                    width: 65px;
                    float: right;
                    text-align: center;
                    border-radius: 3px;
                    font-size: 13px;
                    line-height: 29px;
                    font-weight: 600;
                }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_user_id" runat="server" />
    <asp:HiddenField ID="hd_class_attendance_tcher" runat="server" />
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
                <div class="breadcrumb-title pe-3">Routine</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Classwise Teacher Attendance Report</li>
                        </ol>
                    </nav>
                </div>
            </div>

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
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                                <asp:TextBox ID="txt_date_from" runat="server" class="form-control mdl-txtbx-tyle"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                                <asp:TextBox ID="txt_date_to" runat="server" class="form-control mdl-txtbx-tyle"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Teacher</label>
                                                        <select id="ddl_teacher" runat="server" class="form-control mdl-txtbx-tyle">
                                                            <option value="0">Select</option>
                                                            <option data-ng-repeat="x in ddlteachers" value="{{x.Teacher_id}}" class="form-select mdl-txtbx-tyle">{{x.TeacherName}}</option>
                                                        </select>
                                                    </div>
                                                     
                                                    <div class="col-sm-1">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()" style="padding: 7px 10px;">Find</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                        <table class="table table-striped table-bordered" id="tblCustomers">
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Class</th>
                                                                <th>Section</th>
                                                                <th>Subject Type</th>
                                                                <th>Subject Name</th>
                                                                <th>Total No. of Class</th>
                                                                <th>Total No. of Class Taken</th>
                                                                <th></th>
                                                            </tr>
                                                            <tr data-ng-repeat="x in reportClassTaken track by $index">
                                                                <td>{{$index+1}}</td>
                                                                <td>{{x.Class_name}}</td>
                                                                <td>{{x.Section}}</td> 
                                                                <td>{{x.Subject_type}}</td>
                                                                <td>{{x.Subject_name}}</td>
                                                                <td>{{x.Total_class_of_subject}}</td>
                                                                <td>{{x.No_of_class_taken}}</td>
                                                                <td style="text-transform: uppercase; width: 50px;"><a data-ng-click="ButtonClickView(x.Session_id,x.Class_id,x.Section,x.Subject_id,x.Teacher_id)" href="#!" data-toggle="modal" data-target="#exampleModal"><i class="bx bx-detail"></i></a></td>
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
                </div>





                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Class Taken history</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <th>#</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Subject</th>
                                        <th>Period</th>
                                        <th>Teacher</th>
                                        <th>Date</th>
                                    </tr>
                                    <tr data-ng-repeat="x in tchetkenclass">
                                        <td>{{$index+1}}</td>
                                        <td>{{x.Class_name}}</td>
                                        <td>{{x.Section}}</td>
                                        <td>{{x.Subject_name}}</td>

                                        <td>{{x.Period_Name}}</td>
                                        <td>{{x.Teacher_name}}</td>
                                        <td>{{x.Attendance_date}}</td>
                                    </tr>
                                </table>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>





    <!--end row-->

    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            var session_id = $("#<%=hd_session_id.ClientID%>").val();
            $http.get("webServices/routine.asmx/fetch_teachers_ddl", { params: { "Session_id": session_id } }).then(function (response) {
                $scope.ddlteachers = response.data;
            })


            //====FIND
            $scope.ButtonClickFind = function () {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var teacher_id = $("#<%=ddl_teacher.ClientID%>").val(); 

                var date_from = $("#<%=txt_date_from.ClientID%>").val();
                var date_to = $("#<%=txt_date_to.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("webServices/routine.asmx/fetch_class_taken_by_teacherBT", { params: { "From_date": date_from, "To_date": date_to, "Session_id": session_id, "Teacher_id": teacher_id } }).then(function (response) {
                    $scope.reportClassTaken = response.data;
                    $("#intsLoader").addClass("hidden");
                    if ($scope.reportHeadinG == "") {
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                    }
                })
            }
            //====FIND
            $scope.ButtonClickView = function (Session_id, Class_id, Section, Subject_id, Teacher_id) {
                var date_from = $("#<%=txt_date_from.ClientID%>").val();
                var date_to = $("#<%=txt_date_to.ClientID%>").val();
                $http.get("webServices/routine.asmx/fetch_teacher_detail_class_taken", { params: { "From_date": date_from, "To_date": date_to, "Session_id": Session_id, "Class_id": Class_id, "Section": Section, "Subject_id": Subject_id, "Teacher_id": Teacher_id } }).then(function (response) {
                    $scope.tchetkenclass = response.data;
                })
            }

        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }


        $(function () {
            $("#<%=txt_date_from.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
                manDate: '0',
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_date_to.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
                manDate: '0',
            }).attr("readonly", "true");
        });
    </script>
</asp:Content>
