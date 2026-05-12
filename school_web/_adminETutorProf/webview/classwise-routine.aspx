<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="classwise-routine.aspx.cs" Inherits="school_web._adminETutorProf.webview.classwise_routine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">  
    <title>Class Routine</title>
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
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Raleway:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../../assets/Angular/angular.min.js"></script>
    <style>
        body {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-family: 'Poppins', sans-serif;
        }

        h1, h2, h3, h4, h5, h6 {
            font-family: 'Poppins', sans-serif;
        }

        p {
            font-family: 'Poppins', sans-serif;
        }

        a {
            font-family: 'Poppins', sans-serif;
        }

        .messbox-sec-h2 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #fff;
            background-color: #109be1;
        }

        .fullinfo {
            margin: 0px 0px 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .textcont1 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 13px;
            line-height: 20px;
            color: #000;
            text-align: left;
        }

        .textcont3 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 12px;
            line-height: 20px;
            color: #000;
            text-align: left;
            font-weight: bold;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: -23px;
        }

        .texbox-border {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .btn {
            padding: 2px 17px 2px 17px !important;
            margin: 5px 0px 0px 0px;
        }
        /******************Notification**********************/
        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 133px !important;
            right: 10px;
            padding: 10px 10px;
            width: 290px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgba(154, 154, 154, 0.8);
        }


        .closenotificationpan {
            position: absolute;
            margin: 0px 0px 0px 0px;
            top: 6px;
            right: 6px;
            cursor: pointer;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999;
        }

        table {
            /*box-shadow: 0 1px 1px 0 rgb(0 0 0 / 14%), 0 7px 10px -5px rgb(244 67 54 / 40%);*/
            /*background: linear-gradient( 60deg,#f7807e,#e53935);*/
            border-radius: 0px;
            /*border: 1px dashed #d1c5c5!important;*/
            background: #fff !important;
            border-bottom: 0px solid #c8c5c5;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 5px;
            vertical-align: middle;
            border-color: #ddd0;
            font-size: 12px;
            color: #000;
            padding: 5px 5px 5px 5px !important;
            text-align: center;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > .table-bordered > tbody > tr > .table-bordered > tfoot > tr > th {
            background: #e1dddd !important;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #e7e7e7;
            text-align: center;
            padding: 3px 4px 3px 5px;
            font-size: 12px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 26px !important;
            font-size: 13px !important;
            padding: 2px 5px;
            font-weight: 400;
        }

        .table {
            margin-bottom: 9px !important;
        }

        label {
            display: inline !important;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
        }










        .rdobtnS {
            margin: 0px 0px 0px 0px;
            font-size: 12px;
        }

            .rdobtnS tr {
                padding: 0px 2px;
                width: 33px;
                float: left;
            }

                .rdobtnS tr td {
                    padding: 0px;
                    width: 30px;
                    margin: 0px;
                    height: 30px;
                    float: left;
                }

                    .rdobtnS tr td label {
                        position: relative;
                        top: -28px;
                    }


        .att-imgs {
            padding: 3px;
            width: 44px;
            height: 44px;
            border: 1px solid #a1a1a1;
            border-radius: 50%;
        }

        .att-name {
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            font-weight: 500;
        }

        .att-adm_no {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .att-roll {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .img-dv {
            padding: 0px;
            width: 45px;
            float: left;
        }

        .contnt-dv {
            padding: 0px 0px 0px 10px;
            float: left;
            width: 52%;
            text-align: left;
        }

        .action-dv {
            padding: 8px 0px 0px 0px;
            float: right;
        }

        .ui-datepicker-trigger {
            display: none;
        }
    </style>
    <style>
        tbody, td, tfoot, th, thead, tr {
            text-align: center;
            font-size: 13px;
        }

        tfoot, th, thead {
            color: #fff;
            background: #13b1f3 !important;
        }

        .periodTimes {
            font-size: 11px;
            font-weight: 500;
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
            word-break: break-all;
        }

        .BreakStyleTd {
            background: #FFFFC9 !important;
        }

        .r-day-date {
            margin: 0px;
            text-transform: uppercase;
            font-weight: 600;
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
            margin: 3px 0px 0px 0px;
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

        .form-control {
            display: block;
            width: 100%;
            height: 25px !important;
            font-size: 14px !important;
            padding: 2px 5px;
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

        .my-btn {
            color: #fff;
            background-color: #fdb351 !important;
            border-color: #fdb351 !important;
            font-size: 13px !important;
            line-height: 23px !important;
            font-weight: 400 !important;
        }

            .my-btn:hover {
                color: #fff;
                background-color: #fdb351 !important;
            }

        .yesMyClass {
            background: #46ed43;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_teacher_id" runat="server" />

        <div class="fullinfo" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
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
            <div class="clearfix"></div>
            <div class="container">
                <div class="row">
                    <div class="clearfix"></div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont1 ">Class</p>
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont3">
                            <asp:DropDownList ID="ddl_classs" runat="server" class="form-control mdl-txtbx-tyle" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <%--<select id="ddl_class" ng-click="onClassChange()" runat="server" class="form-control mdl-txtbx-tyle">
                                <option value="0">Select</option>
                                <option data-ng-repeat="x in ddlclass" value="{{x.Class_id}}" class="form-select mdl-txtbx-tyle">{{x.Class_name}}</option>
                            </select>--%>
                        </p>
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont1 ">Section</p>
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont3">
                            <asp:DropDownList ID="ddl_sections" runat="server" class="form-control mdl-txtbx-tyle"></asp:DropDownList>
                            <%--<select id="ddl_section" runat="server" class="form-control mdl-txtbx-tyle">
                                <option value="0">Select</option>
                                <option data-ng-repeat="x in ddlsection" value="{{x.Section}}" class="form-select mdl-txtbx-tyle">{{x.Section}}</option>
                            </select>--%>
                        </p>
                    </div>

                    <div class="clearfix"></div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;"></div>
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                        <a href="javascript:" class="btn btn-primary my-btn" data-ng-click="ButtonClickFind()" style="padding: 7px 10px;">Find</a>
                        <a href="javascript:" class="btn btn-primary" data-ng-click="ButtonClickFind()" style="padding: 7px 10px; line-height: 23px !important; font-size: 13px;">Refresh</a>
                    </div>

                    <div class="col-lg-12">
                        <div class="texbox-border" style="overflow: auto;">
                            <div class="prnt-dv-wpr printborder hidden" id="grdsdatA" style="width: {{reportHeadinG[0].TblwidtH}}">
                                <table class="table table-striped table-bordered" id="tblCustomers">
                                    <tr>
                                        <th>Day</th>
                                        <th data-ng-repeat="x in reportHeadinG" style="width: 160px;">{{x.Period_Name}}
                                                                    <br />
                                            <span class="periodTimes">({{x.period_times}})</span> </th>
                                    </tr>
                                    <tr data-ng-repeat="x in reportdaY track by $index">
                                        <td class="{{x.DaYcolors}}"><span class="r-day-date">{{x.Day_date}}</span><br />
                                            <span class="r-day">{{x.Day_name}}</span><br />
                                            <span class="r-day-teacher">{{x.Teachers}}</span></td>
                                        <td style="position: relative; width: 160px;" data-ng-repeat="item in x.MyRoutineSubjectTeacherItem track by $index" rowspan="{{item.Tblrowcount}}" class="{{item.Isclass_or_break}} {{item.Period_type_td}} {{item.IsMyClass}}"><span style="font-weight: 500;" class="{{item.Period_type}}">{{item.Subjects_name}} </span>
                                            <br />
                                            <span class="teachers-names {{item.Teachers_name}}"><span>{{item.Teachers_name}}</span></span>
                                        </td>
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

                <%--var session_id = $("#<%=hd_session_id.ClientID%>").val();
                $http.get("WebService1.asmx/fetch_ddl_class", { params: { "Session_id": session_id } }).then(function (response) {
                    $scope.ddlclass = response.data;
                })

                //---FetchSuject-----------------
                $scope.onClassChange = function () {
                    var session_id = $("#<%=hd_session_id.ClientID%>").val();
                    var class_id = $("#<%=ddl_class.ClientID%>").val();
                    $http.get("WebService1.asmx/fetch_section_by_class", { params: { "Session_id": session_id, "Class_id": class_id } }).then(function (response) {
                        $scope.ddlsection = response.data;
                    })
                }--%>


                //====FIND
                $scope.ButtonClickFind = function () {
                    var session_id = $("#<%=hd_session_id.ClientID%>").val();
                    var class_id = $("#<%=ddl_classs.ClientID%>").val();
                    var section = $("#<%=ddl_sections.ClientID%>").val();
                    var teacher_id = $("#<%=hd_teacher_id.ClientID%>").val();
                    //alert(class_id);
                    if (class_id != "0") {
                        messge("Please Wait...");
                        $("#intsLoader").removeClass("hidden");

                        $http.get("WebService1.asmx/fetch_class_routine_details_teacher", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                            $scope.reportHeadinG = response.data;
                            if ($scope.reportHeadinG == "") {
                                $("#grdsdatA").addClass("hidden");
                                $("#NotFounD").removeClass("hidden");
                                $("#intsLoader").addClass("hidden");
                            }
                            else {
                                $("#grdsdatA").removeClass("hidden");
                                $("#NotFounD").addClass("hidden");
                                $http.get("WebService1.asmx/fetch_class_routine_details_day_teacher", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Teacher_id": teacher_id } }).then(function (response) {
                                    $scope.reportdaY = response.data;
                                    $("#intsLoader").addClass("hidden");
                                })
                            }
                        })
                    }

                }
            });


            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }
        </script>
    </form>
</body>
</html>
