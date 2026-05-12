<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="class-routing.aspx.cs" Inherits="school_web.Student_Profile.class_routing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Class Routine
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
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
            border-radius: 0px;
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_class" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />
    <div class="pagemainhh" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
        <div class="container">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="headingtablee">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="table-responsive">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="main-card mb-3 card">
                                            <div class="card-header">
                                                <h4 class="card-title">Class Routine</h4>
                                            </div>

                                            <div class="card-body" style="padding-top: 0px;">
                                                <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                    <table class="table table-striped table-bordered" id="tblCustomers">
                                                        <tr>
                                                            <th>Sl. No.</th>
                                                            <th>Period</th>
                                                            <th>Subject</th>
                                                        </tr>
                                                        <tr data-ng-repeat="x in reportHeadinG">
                                                            <td>{{$index+1}}</td>
                                                            <td class="{{x.Isclass_or_break}} {{x.Period_type_td}}"><span style="font-weight: 500;">{{x.Period_Name}}</span>
                                                                <br />
                                                                <span class="periodTimes">({{x.period_times}})</span> </td>

                                                            <td class="{{x.Isclass_or_break}} {{x.Period_type_td}}"><span class="{{x.Period_type}}">{{x.Subjects_name}}</span></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="notFound hidden" id="NotFounD" style="margin: 10px 0px 0px 0px;">
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

            var session_id = $("#<%=hd_session_id.ClientID%>").val();
            var class_id = $("#<%=hd_class.ClientID%>").val();
            var section = $("#<%=hd_section.ClientID%>").val();

            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            $http.get("webview/WebService1.asmx/fetch_class_routine_details", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                $scope.reportHeadinG = response.data;
                $("#intsLoader").addClass("hidden");
                if ($scope.reportHeadinG == "") {
                    $("#grdsdatA").addClass("hidden");
                    $("#NotFounD").removeClass("hidden");
                }
                else {
                    $("#grdsdatA").removeClass("hidden");
                    $("#NotFounD").addClass("hidden");
                    $http.get("webview/WebService1.asmx/fetch_class_routine_details_day", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                        $scope.reportdaY = response.data;
                    })
                }
            })
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>
</asp:Content>
