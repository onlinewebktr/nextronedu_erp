<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="school_web.Payroll.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
    <div class="page-wrapper">
        <div class="page-content">
            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-3">
                    <a href="#!">
                        <div class="card radius-10 border-start border-0 border-3 border-info">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Employees</p>
                                        <h4 class="my-1 text-info" runat="server" id="ttl_employees">00</h4>
                                        <%--<p class="mb-0 font-13" runat="server" id="ttlodRLstWeeK">+00% from last week</p>--%>
                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                        <i class='bx bx-user'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-danger">
                        <a href="#!">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Present Today</p>
                                        <h4 class="my-1 text-danger" runat="server" id="ttl_p_today">₹00</h4>
                                        <%--<p class="mb-0 font-13" runat="server" id="ttlRevenueLstWeeK">+00% from last week</p>--%>
                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bxs-bar-chart-alt-2'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-xl-3">
                    <a href="#!">
                        <div class="card radius-10 border-start border-0 border-3 border-success">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Absent Today</p>
                                        <h4 class="my-1 text-success" runat="server" id="ttl_absent">00</h4>
                                        <%--<p class="mb-0 font-13" runat="server" id="ttlCancelAmtLstWeek">-00 from last week</p>--%>
                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                        <i class='bx bxs-bar-chart-alt-2'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-xl-3">
                    <a href="#!">
                        <div class="card radius-10 border-start border-0 border-3 border-success">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">On Leave Today</p>
                                        <h4 class="my-1 text-success" runat="server" id="in_leave_tdy">00</h4>
                                        <%--<p class="mb-0 font-13" runat="server" id="ttlCancelAmtLstWeek">-00 from last week</p>--%>
                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto" style="background: linear-gradient(45deg, #b05900, #fff931)!important;">
                                        <i class='bx bxs-bar-chart-alt-2'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
            <!--end row-->
            <div class="row">
                <div class="col-12 col-lg-12">
                    <div class="card radius-10">
                        <div class="card-body" style="padding: 1rem 0rem 1rem 1rem;">
                            <div class="d-flex align-items-center">
                                <div>
                                    <h6 class="mb-0">Present Attendance Summary</h6>
                                </div>
                            </div>
                            <div id="order_summary" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hd_TenDayS" runat="server" />
    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var fromdate = $('#<%= hd_TenDayS.ClientID %>').val();

            //=================Order SummarY
            var myChart6 = echarts.init(document.getElementById('order_summary'));
            myChart6.setOption({
                title: {
                    //text: 'Date Wise LAB Patient',
                    textAlign: 'Right',
                    textStyle: { fontSize: 13 }
                },
                tooltip: {},
                legend: {

                },
                xAxis: {

                    data: [],
                    type: 'category',
                    nameRotate: 'xAxis',
                    axisTick: { show: false },
                    nameGap: 5,
                    min: 'dataMin',
                    interval: 0,

                    axisLabel: {
                        interval: 0,
                        inside: false,
                        rotate: 25,
                        showMaxLabel: true,
                        verticalAlign: "top",
                        lineHeight: 5,
                        fontSize: 10,
                        extraCssText: "width:100px; white-space:pre-wrap;"
                    },
                },
                yAxis: {},
                series: [{

                    name: 'Patient Count',
                    type: 'bar',
                    data: [],
                    label: {
                        normal: {
                            show: true,
                            position: 'inside'
                        }
                    }
                }]

            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "graph.asmx/find_order_summary_report_report",
                data: '{"Ten_Days":"' + fromdate + '" }',
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart6.setOption({
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                type: 'shadow'
                            }
                        },
                        legend: {
                            selectedMode: 'multiple',
                            data: ['Present Attendance']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Present Attendance',
                            type: 'bar',
                            data: JSONObject["yaxis"],
                            color: ['#96c93d'],
                            label: {
                                normal: {
                                    show: true,
                                    position: 'inside'
                                }
                            }
                        }
                        ]
                    });
                },
            });


        });
    </script>
</asp:Content>
