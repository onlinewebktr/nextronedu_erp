<%@ Page Title="" Language="C#" MasterPageFile="~/Online_Test_admin/Admin.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="school_web.Online_Test_admin.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Welcome To Online Test Admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        
        .chart-data-figure {
            margin: 10px 0px;
            padding: 5px 0px;
            width: 98%;
            float: left;
            text-align: center;
            font-weight: 500;
            font-size: 14px;
        }

            .chart-data-figure i {
                margin: 0px;
                padding: 3px 5px 4px;
                font-style: normal;
                color: #fff;
                border-radius: 2px;
            }

            .chart-data-figure span {
                margin: 0px;
                padding: 0px;
                font-weight: 600;
            }

        .bg1i {
            background: #b97781;
        }

        .bg2i {
            background: #596fdd;
        }

        .bg3i {
            background: #00c13b;
        }

        .bg4i {
            background: #c5bf00;
        }
        .table th, .table td {
            padding: 8px !important;
            vertical-align: top !important;
            border-top: 1px solid #e9ecef !important;
            text-align: left;
        }

        .dashboard-bx-wpr-cntnt-p {
            font-size: 17px !important;
        }

        .dashboard-bx-wpr-cntnt-count-p {
            font-weight: 400 !important;
        }

        .inv-dashbrd-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .inv-dashbrd-bx-wpr {
            margin: 0px 0px 10px 0px;
            padding: 20px 10px;
            width: 100%;
            float: left;
            background: #f27b53;
            border: 1px solid #eb6032;
            color: #f7f7f7;
            border-radius: 3px;
            -webkit-box-shadow: 0 2px 8px 0 rgb(183 192 206);
        }

        .inv-dashbrd-bx-ico-sec {
            margin: 0px;
            padding: 0px;
            width: 25%;
            float: left;
        }

            .inv-dashbrd-bx-ico-sec i {
                margin: 0px;
                padding: 0px;
                font-size: 27px;
                line-height: 40px;
                width: 40px;
                text-align: center;
                box-shadow: 5px 3px 10px 0 rgb(21 15 15 / 30%);
                border-radius: 50% 50%;
            }

        .inv-dashbrd-bx-contnt-sec {
            margin: 0px;
            padding: 0px;
            width: 75%;
            float: left;
        }

            .inv-dashbrd-bx-contnt-sec h2 {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
                color: #f7f7f7;
                font-size: 13px;
                line-height: 25px;
            }

            .inv-dashbrd-bx-contnt-sec p {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
                font-size: 16px;
            }

        .inv-dashbrd-grdwprs {
            margin: 10px 0px 0px 0px;
            padding: 5px 5px;
            width: 100%;
            float: left;
            background: #fff;
            border: 1px solid #ddd;
            -webkit-box-shadow: 0 0px 4px 0 rgb(183 192 206 / 10%);
        }

        .inv-dashbrd-dmnd-h {
            margin: 0px;
            padding: 0px 0px 3px 0px;
            width: 100%;
            float: left;
            font-size: 20px;
            line-height: 25px;
            color: #3c3c3c;
        }

        .viwallbtns {
            margin: 1px 0px 0px 0px;
            padding: 2px 5px 2px 5px;
            width: auto;
            float: right;
            font-size: 13px;
            line-height: 15px;
            color: #ffffff;
            border-radius: 2px;
            background: #847DC3;
            border: 1px solid #6f67b5;
        }

            .viwallbtns:hover {
                color: #ffffff;
                background: #847DC3;
                border: 1px solid #6f67b5;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-wrapper">
        <div class="page-content">
            <div class="inv-dashbrd-wpr">
                <div class="row">
                    <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12">
                        <a href="#" style="cursor:pointer">
                            <div class="inv-dashbrd-bx-wpr">
                                <div class="inv-dashbrd-bx-ico-sec">
                                    <i class="bx bx-notepad"></i>
                                </div>
                                <div class="inv-dashbrd-bx-contnt-sec">
                                    <h2>Total Active Test</h2>
                                    <p runat="server" id="lbl_total_test">0</p>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12">
                        <a href="#">
                            <div class="inv-dashbrd-bx-wpr" style="background: #26B79A; border: 1px solid #26B79A;">
                                <div class="inv-dashbrd-bx-ico-sec">
                                    <i class="bx bx-notepad"></i>
                                </div>
                                <div class="inv-dashbrd-bx-contnt-sec">
                                    <h2>Total Inactive Test</h2>
                                    <p runat="server" id="lbl_inactivetotal_test">0</p>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12">
                        <a href="#">
                            <div class="inv-dashbrd-bx-wpr" style="background: #DE587B; border: 1px solid #d54268;">
                                <div class="inv-dashbrd-bx-ico-sec">
                                    <i class="bx bx-notepad"></i>
                                </div>
                                <div class="inv-dashbrd-bx-contnt-sec">
                                    <h2>Total Attempted</h2>
                                    <p runat="server" id="lbl_TotalAttempted">0</p>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12">
                        <a href="#">
                            <div class="inv-dashbrd-bx-wpr" style="background: #847DC3; border: 1px solid #6f67b5;">
                                <div class="inv-dashbrd-bx-ico-sec">
                                    <i class="bx bx-notepad"></i>
                                </div>
                                <div class="inv-dashbrd-bx-contnt-sec">
                                    <h2>Total Not Attempted</h2>
                                    <p runat="server" id="lbl_TotalnotAttempted">0</p>
                                </div>
                            </div>
                        </a>
                    </div>

                </div>
            </div>

            <div class="inv-dashbrd-wpr">
                <div class="row">
                    <div class="col-12 col-lg-12">
                        <div class="card radius-10">
                            <div class="card-body" style="padding: 1rem 0rem 2rem 1rem;">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <h6 class="mb-0">Month Wise Student attempted 
                                    <asp:DropDownList ID="ddl_months" AutoPostBack="true" OnSelectedIndexChanged="ddl_months_SelectedIndexChanged" runat="server" class="form-select" Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;">
                                    </asp:DropDownList></h6>
                                    </div>
                                </div>
                                <div class="chart-container-0">
                                    <div class="chart" style="overflow: hidden;">
                                        <div id="chart" style="margin: 0px 0px 0px -134px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


        </div>
    </div>
    <div style="display: none">
        <asp:DropDownList ID="ddlsession" runat="server"></asp:DropDownList>
    </div>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        /*        $(document).ready(function () {*/
        
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Online Test',
                width: 1350,
                height: 400,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '95%' },
                isStacked: true,
                is3D: true,
                colors: ['#15CA20', '#008CFF', '#F7F700', '#11FFFD', '#AA398F'],
                hAxis: {
                    textStyle: {
                        fontSize: 10, // or the number you want
                        is3D: true,
                        italic: true
                    }
                }
            };
            $.ajax({

                type: "POST",
                url: "Home.aspx/GetChartData",
                data: "{Session: '" + $('#<%=ddlsession.ClientID%>').val() + "', month: '" + $('#<%= hd_months.ClientID %>').val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        var data = google.visualization.arrayToDataTable(r.d);
                        var chart = new google.visualization.ColumnChart($("#chart")[0]);
                        chart.draw(data, options);
                    },
                    failure: function (r) {
                        alert(r.d);
                    },
                    error: function (r) {
                        alert(r.d);
                    }
                });
        }
        /*}*/





    </script>
    <asp:HiddenField ID="hd_months" runat="server" />
</asp:Content>
