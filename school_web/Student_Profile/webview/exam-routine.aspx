<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exam-routine.aspx.cs" Inherits="school_web.Student_Profile.webview.exam_routine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />

    <link href="../../font-awesome-4.0.3/css/font-awesome.css" rel="stylesheet" />
    <script src="../../assets/Angular/angular.min.js"></script>
    <style type="text/css">
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
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
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
            font-size: 13px;
            color: #000;
            padding: 6px 7px 6px 7px !important;
            border: 1px solid #ddd;
        }
    </style>

    <style type="text/css">
        tbody, td, tfoot, th, thead, tr {
            text-align: center;
            font-size: 13px;
        }

        tfoot, th, thead {
            color: #fff;
            background: #Fdb351 !important;
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
            display: inline-block;
            text-transform: uppercase;
            font-weight: 500;
            font-size: 13px;
        }

        .BreakStyleTd {
            background: #FFFFC9 !important;
        }

        tfoot, th, thead {
            background: #Fdb351 !important;
        }
    </style>

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
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_term_id" runat="server" />
        <asp:HiddenField ID="hd_exam_id" runat="server" />
        <asp:HiddenField ID="hd_shift" runat="server" />
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <div class="fullinfo" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="texbox-border" style="overflow: auto;">
                            <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                <table class="table table-striped table-bordered" id="tblSingleShift">
                                    <tr>
                                        <th colspan="8" style="text-align: center; background-color: #f7f7f7">{{reportRoutine[0].Exam_name}} - EXAMINATION TIME TABLE </th>
                                    </tr>
                                    <tr>
                                        <th>SN</th>
                                        <th>DATE</th>
                                        <th>TIME</th>
                                        <th>DAY</th>
                                        <th>Subject</th>
                                    </tr>
                                    <tr data-ng-repeat="item in reportRoutine">
                                        <td>{{$index+1}}</td>
                                        <td>{{item.Exam_date}}</td>
                                        <td>{{item.Exam_time}} - {{item.Exam_end_time1}}</td>
                                        <td>{{item.Day}}</td>
                                        <td>{{item.Subject_name}}</td>
                                    </tr>
                                </table>


                                <table class="table table-striped table-bordered" id="tblDoubleShift">
                                    <tr>
                                        <th colspan="8" style="text-align: center; background-color: #f7f7f7">{{reportRoutineDSHFT[0].Exam_name}} - EXAMINATION TIME TABLE </th>
                                    </tr>
                                    <tr style="background-color: #f7f7f7">
                                        <th colspan="2"></th>
                                        <th>1ST SITTING <br /> ({{reportRoutineDSHFT[0].Shift1_start}} TO {{reportRoutineDSHFT[0].Shift1_end}})</th>
                                        <th>2ND SITTING <br /> ({{reportRoutineDSHFT[0].Shift2_start}} TO {{reportRoutineDSHFT[0].Shift2_end}})</th>
                                    </tr>
                                    <tr>
                                        <th>SN</th>
                                        <th>DATE</th>
                                        <th>SUBJECT</th>
                                        <th>SUBJECT</th>
                                    </tr>
                                    <tr data-ng-repeat="item in reportRoutineDSHFT">
                                        <td>{{$index+1}}</td>
                                        <td>{{item.Exam_date}}<br />
                                            <span>({{item.Day}})</span></td>
                                        <td>{{item.Subject_name1}}</td>
                                        <td>{{item.Subject_name2}}</td>
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


        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();
                var exam_id = $("#<%=hd_exam_id.ClientID%>").val();
                var shift_Type = $("#<%=hd_shift.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                if (shift_Type == "2") {
                    $("#tblSingleShift").addClass("hidden");
                    $("#tblDoubleShift").removeClass("hidden");
                    $http.get("WebService1.asmx/fetch_exam_routine_details_double_shift", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Term_id": term_id, "Exam_id": exam_id, "Shift_Type": shift_Type, "Admission_no": admission_no } }).then(function (response) {
                        $scope.reportRoutineDSHFT = response.data;
                        $("#intsLoader").addClass("hidden");
                        if ($scope.reportRoutineDSHFT == "") {
                            $("#grdsdatA").addClass("hidden");
                            $("#NotFounD").removeClass("hidden");
                        }
                        else {
                            $("#grdsdatA").removeClass("hidden");
                            $("#NotFounD").addClass("hidden");
                        }
                    })
                }
                else {
                    $("#tblSingleShift").removeClass("hidden");
                    $("#tblDoubleShift").addClass("hidden");
                    $http.get("WebService1.asmx/fetch_exam_routine_details", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Term_id": term_id, "Exam_id": exam_id, "Shift_Type": shift_Type, "Admission_no": admission_no } }).then(function (response) {
                        $scope.reportRoutine = response.data;
                        $("#intsLoader").addClass("hidden");
                        if ($scope.reportRoutine == "") {
                            $("#grdsdatA").addClass("hidden");
                            $("#NotFounD").removeClass("hidden");
                        }
                        else {
                            $("#grdsdatA").removeClass("hidden");
                            $("#NotFounD").addClass("hidden");
                        }
                    })
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
