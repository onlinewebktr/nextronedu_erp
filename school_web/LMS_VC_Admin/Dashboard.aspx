<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Dashboard.aspx.cs" Inherits="school_web.LMS_VC_Admin.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://cdn.jsdelivr.net/jquery/latest/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />
    <style>
        .body-tabs li.nav-item {
            border: 1px solid;
        }

        .chart {
            display: none;
            width: 100%;
            height: 300px;
        }

        .chart_active {
            display: inherit;
        }

        .panel-heading {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }

            .panel-heading a {
                color: #fff;
            }

        .panel.panel-oranges {
            background: #fd8403;
            color: white;
        }

        .panel.panel-blue {
            background: #438eb9;
            color: white;
        }

        .panel.panel-red {
            background: #de0a0a;
            color: white;
        }

        .panel.panel-green {
            background: #21a70a;
            color: white;
        }

        .panel-heading a {
            color: #fff;
        }

        .product_row {
            float: left;
            width: 99%;
            margin-left: 15px;
            margin-bottom: 15px;
            background-color: rgb(255, 220, 135);
            padding: 7px 15px;
            border-top: 6px solid;
            margin-top: 10px;
        }

            .product_row span {
                color: rgb(0, 0, 0);
                font-size: 17px;
                position: relative;
                font-weight: 600;
                letter-spacing: 0.7px;
                width: 99%;
            }

        .col-md-3 {
            margin-top: 35px;
        }

        .stat-widget-five {
            min-height: 60px;
        }

            .stat-widget-five .stat-icon {
                font-size: 50px;
                line-height: 50px;
                position: absolute;
                left: 30px;
                top: 20px;
            }

        .dib {
            display: inline-block;
        }

        .flat-color-1 {
            color: #00c292;
        }

        .stat-widget-five .stat-content {
            margin-left: 100px;
        }

        .dib {
            display: inline-block;
        }

        .stat-widget-five .stat-text {
            color: #fff;
            font-size: 20px;
        }

        .stat-widget-five .stat-heading {
            color: #fafafa;
            font-size: 14px;
        }

        .flat-color-2 {
            color: #ab8ce4;
        }

        .flat-color-3 {
            color: #03a9f3;
        }

        .flat-color-4 {
            color: #fb9678;
        }

        .tabs-animated .nav-link {
            padding: 0.5rem;
            margin: 0px;
        }

            .tabs-animated .nav-link.active, .tabs-animated .nav-link:hover {
                color: #fff;
                background: linear-gradient(to right, #da8cff, #9a55ff);
            }
    </style>
    <style>
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 300px;
            height: 140px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
            left: 602px;
            top: 102px;
        }
    </style>
    <script type="text/javascript">
        function ShowProgress() {
            // alert('sdsjgdhsdgfsd');
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function ShowProgress_hide() {
            // alert('sdsjgdhsdgfsd');

            document.getElementsByClassName('loading').style.visibility = 'hidden';

        }
        $('form').on("submit", function () {
            // alert('sdsjgdhsdgfsd');
            ShowProgress();
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-graph3 on pe-7s-monitor icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        Dashboard
                    </div>
                </div>

            </div>
        </div>
        <asp:HiddenField ID="hd_id" runat="server" />
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="row">
            <div class="card-body" style="padding: 0px 1.25rem;">
                <div class="row">
                    <div class="col-lg-3 col-md-6">
                        <a href="Add_Teacher.aspx">
                            <div class="card bg-night-sky">
                                <div class="card-body">
                                    <div class="stat-widget-five">
                                        <div class="stat-icon dib  text-white">
                                            <i class="pe-7s-add-user"></i>
                                        </div>
                                        <div class="stat-content">
                                            <div class="text-left dib">
                                                <div class="stat-text">
                                                    <span class="count">
                                                        <asp:Label ID="lbl_Teachers" runat="server" Text="0"></asp:Label></span>
                                                </div>
                                                <div class="stat-heading">Total Teachers</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <div class="col-lg-3 col-md-6">
                        <a href="View_student_list.aspx">
                            <div class="card bg-arielle-smile">
                                <div class="card-body">
                                    <div class="stat-widget-five">
                                        <div class="stat-icon dib  text-white">
                                            <i class="pe-7s-users"></i>
                                        </div>
                                        <div class="stat-content">
                                            <div class="text-left dib">
                                                <div class="stat-text">
                                                    <span class="count">
                                                        <asp:Label ID="lbl_Students" runat="server" Text=""></asp:Label></span>
                                                </div>
                                                <div class="stat-heading">Total Students</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <a href="Live_Meeting_info.aspx">
                            <div class="card bg-grow-early">
                                <div class="card-body">
                                    <div class="stat-widget-five">
                                        <div class="stat-icon dib  text-white">
                                            <i class="pe-7s-monitor"></i>
                                        </div>
                                        <div class="stat-content">
                                            <div class="text-left dib">
                                                <div class="stat-text">
                                                    <span class="count">
                                                        <asp:Label ID="lblenroll" runat="server" Text=""></asp:Label></span>
                                                </div>
                                                <div class="stat-heading">Live Classes</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <a href="#">
                            <div class="card bg-strong-bliss">
                                <div class="card-body">
                                    <div class="stat-widget-five">
                                        <div class="stat-icon dib  text-white">
                                            <i class="pe-7s-browser"></i>
                                        </div>
                                        <div class="stat-content">
                                            <div class="text-left dib">
                                                <div class="stat-text">
                                                    <span class="count">
                                                        <asp:Label ID="lbl_Licence" runat="server" Text="" ></asp:Label>
                                                        <asp:Label ID="lbl_duse_syncdate" runat="server" Text="" Visible="false"></asp:Label>
                                                    </span>
                                                </div>
                                                <div class="stat-heading">No. of Licence</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>


                </div>
            </div>
        </div>
        <div class="main-card mb-3 card" style="display:none">
            <div class="card-header-tab card-header">
                <div class="card-header-title font-size-lg text-capitalize font-weight-normal">
                    <i class="header-icon lnr-cloud-download icon-gradient bg-happy-itmeo"></i>
                    Analytics 
                </div>
                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                    <div id="reportrange" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc; width: 100%">
                        <i class="fa fa-calendar"></i>&nbsp;
                         <asp:Label ID="lblDateText" runat="server"></asp:Label><asp:Literal ID="LtTime" runat="server" Visible="false"></asp:Literal><i class="fa fa-caret-down"></i>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="row">
                        <ul class="body-tabs body-tabs-layout tabs-animated  nav">
                            <li class="nav-item">
                                <a role="tab" class="nav-link active" id="tab-0" data-toggle="tab" href="#tab-content-0">
                                    <span>All</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a role="tab" class="nav-link" id="tab-1" data-toggle="tab" href="#tab-content-1">
                                    <span>Online Classes</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a role="tab" class="nav-link" id="tab-2" data-toggle="tab" href="#tab-content-2">
                                    <span>Teachers</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a role="tab" class="nav-link" id="tab-3" data-toggle="tab" href="#tab-content-3">
                                    <span>Students</span>
                                </a>
                            </li>
                        </ul>


                        <script type="text/javascript" src="https://www.google.com/jsapi"></script>
                        <div class="tab-content">
                            <div class="tab-pane tabs-animation fade show active" id="tab-content-0" role="tabpanel">
                                <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
                                <div id="chart_div" style="width: 1000px; height: 400px;">
                                </div>
                            </div>
                            <div class="tab-pane tabs-animation fade" id="tab-content-1" role="tabpanel">
                                <asp:Literal ID="ltScriptsOnline" runat="server"></asp:Literal>
                                <div id="chartOnline" style="width: 1000px; height: 400px;">
                                </div>
                            </div>
                            <div class="tab-pane tabs-animation fade" id="tab-content-2" role="tabpanel">
                                <asp:Literal ID="ltScriptsTeacher" runat="server"></asp:Literal>
                                <div id="chartTeachers" style="width: 1000px; height: 400px;">
                                </div>
                            </div>
                            <div class="tab-pane tabs-animation fade" id="tab-content-3" role="tabpanel">
                                <asp:Literal ID="ltScriptsStudents" runat="server"></asp:Literal>
                                <div id="chartStudents" style="width: 1000px; height: 400px;">
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdIsactive" runat="server" />
    <asp:HiddenField ID="hdStartDate" runat="server" />
    <asp:HiddenField ID="hdEndDate" runat="server" />
    <asp:HiddenField ID="hdfUserType" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="Button1_Click" />

    <script type="text/javascript">
        $(function () {

            var start = moment().subtract(29, 'days');
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                $('#<%=hdIsactive.ClientID%>').val(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                $('#<%=hdStartDate.ClientID%>').val(start.format('YYYYMMDD'));
                $('#<%=hdEndDate.ClientID%>').val(end.format('YYYYMMDD'));
                var button = $('#<%=Button1.ClientID%>');
                button.click();
                return;
            }

            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 15 Days': [moment().subtract(14, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);
        }); cb(start, end);
    </script>

    <div style="height: 1px; overflow: hidden">
        <asp:Button ID="btnSubmit" runat="server" Text="Load Customers"
            OnClick="btnSubmit_Click" OnClientClick="retun ShowProgress();" />
    </div>
    <div class="loading" align="center" id="a1" runat="server">
        Please wait. for send push
        <br />
        <br />
        <img src="../images/loader.gif" />
    </div>




</asp:Content>
