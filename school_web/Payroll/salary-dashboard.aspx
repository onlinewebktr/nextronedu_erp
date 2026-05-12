<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="salary-dashboard.aspx.cs" Inherits="school_web.Payroll.salary_dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Salary Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
    <div class="page-wrapper">
        <div class="page-content">
            <!--end row-->
            <div class="row">
                <div class="col-12 col-lg-12">
                    <div class="card radius-10">
                        <div class="card-body" style="padding: 1rem 0rem 1rem 1rem;">
                            <div class="d-flex align-items-center">
                                <div>
                                    <h6 class="mb-0">Month Wise Salary
                                        <asp:DropDownList ID="ddl_dep" class="form-select" runat="server"  Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged"></asp:DropDownList>
                                    </h6>
                                </div>
                            </div>
                            <div id="salary_mnthwise_summary" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-12 col-lg-12">
                    <div class="card radius-10">
                        <div class="card-body" style="padding: 1rem 0rem 1rem 1rem;">
                            <div class="d-flex align-items-center">
                                <div>
                                    <h6 class="mb-0">Month Wise PF</h6>
                                </div>
                            </div>
                            <div id="pf_mnthwise_summary" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-12 col-lg-12">
                    <div class="card radius-10">
                        <div class="card-body" style="padding: 1rem 0rem 1rem 1rem;">
                            <div class="d-flex align-items-center">
                                <div>
                                    <h6 class="mb-0">Month Wise ESI</h6>
                                </div>
                            </div>
                            <div id="esi_mnthwise_summary" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
        </div>
    </div>
      
    <asp:HiddenField ID="hd_dep_name" runat="server" />
   
    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var Dep = $('#<%= hd_dep_name.ClientID %>').val();
            //=================Order SummarY
            var myChart6 = echarts.init(document.getElementById('salary_mnthwise_summary'));
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

                    name: 'Salary',
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
                url: "graph.asmx/find_staff_salary_monthly_report",
                data: '{"DEP":"' + Dep + '" }',
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
                            data: ['Salary']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Salary',
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


            var myChart1 = echarts.init(document.getElementById('pf_mnthwise_summary'));
            myChart1.setOption({
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

                    name: 'PF',
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
                url: "graph.asmx/find_staff_pf_monthly_report",
                //data: '{"DEP":"' + Dep + '" }',
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart1.setOption({
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                type: 'shadow'
                            }
                        },
                        legend: {
                            selectedMode: 'multiple',
                            data: ['PF']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'PF',
                            type: 'bar',
                            data: JSONObject["yaxis"],
                            color: ['#6078ea'],
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

            //==========================================
            var myChart12 = echarts.init(document.getElementById('esi_mnthwise_summary'));
            myChart12.setOption({
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

                    name: 'ESI',
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
                url: "graph.asmx/find_staff_esi_monthly_report",
                //data: '{"DEP":"' + Dep + '" }',
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart12.setOption({
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                type: 'shadow'
                            }
                        },
                        legend: {
                            selectedMode: 'multiple',
                            data: ['ESI']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'ESI',
                            type: 'bar',
                            data: JSONObject["yaxis"],
                            color: ['#6078ea'],
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
