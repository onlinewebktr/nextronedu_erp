<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="employee-dashboard.aspx.cs" Inherits="school_web.Payroll.employee_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Employee Dashboard
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
                                    <h6 class="mb-0">Degination Wise Staff
                                        <asp:DropDownList ID="ddl_gender" AutoPostBack="true" OnSelectedIndexChanged="ddl_gender_SelectedIndexChanged" runat="server" class="form-select" Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;">
                                        </asp:DropDownList>

                                       
                                    </h6>
                                </div>

                                  <div style="margin: 0px 0px 0px 19px;">
                                    <h6 class="mb-0"> 
                                         

                                       Total Employees :<asp:Label ID="lbl_total_employee" runat="server" Text="0"></asp:Label>
                                    </h6>
                                </div>
                            </div>
                            <div id="staff_summary" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
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
                                    <h6 class="mb-0">Department Wise Staff
                                        <asp:DropDownList ID="ddl_gender_dep" AutoPostBack="true" OnSelectedIndexChanged="ddl_gender_dep_SelectedIndexChanged" runat="server" class="form-select" Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;">
                                        </asp:DropDownList>
                                    </h6>
                                </div>

                                 <div style="margin: 0px 0px 0px 19px;">
                                    <h6 class="mb-0"> 
                                         

                                       Total Employees :<asp:Label ID="lbl_Employees1" runat="server" Text="0"></asp:Label>
                                    </h6>
                                </div>
                            </div>
                            <div id="staff_dep_wise" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
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
                                    <h6 class="mb-0">Grade Wise Staff</h6>
                                </div>
                                <div style="margin: 0px 0px 0px 19px;">
                                    <h6 class="mb-0"> 
                                         

                                       Total Employees :<asp:Label ID="lbl_total_employee_grad" runat="server" Text="0"></asp:Label>
                                    </h6>
                                </div>
                            </div>
                            <div id="staff_grade_wise" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
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
                                    <h6 class="mb-0">Religion Wise Staff</h6>
                                </div>
                                  <div style="margin: 0px 0px 0px 19px;">
                                    <h6 class="mb-0"> 
                                         

                                       Total Employees :<asp:Label ID="lbltotal_employee_2" runat="server" Text="0"></asp:Label>
                                    </h6>
                                </div>
                            </div>
                            <div id="staff_religion_wise" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField ID="hd_gender" runat="server" />
    <asp:HiddenField ID="hd_gender_dep" runat="server" />
    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var Gender = $('#<%= hd_gender.ClientID %>').val();
            var GenderDep = $('#<%= hd_gender_dep.ClientID %>').val();

            //=================Order SummarY
            var myChart6 = echarts.init(document.getElementById('staff_summary'));
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

                    name: 'Staff',
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
                url: "graph.asmx/find_staff_summary_report_report",
                data: '{"GendeR":"' + Gender + '" }',
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
                            data: ['Staff']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Staff',
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


            //============================
            //=================Staff Dep Wise
            var myChart7 = echarts.init(document.getElementById('staff_dep_wise'));
            myChart7.setOption({
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

                    name: 'Staff',
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
                url: "graph.asmx/find_staff_summary_report_report_dep",
                data: '{"GendeR":"' + GenderDep + '" }',
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart7.setOption({
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                type: 'shadow'
                            }
                        },
                        legend: {
                            selectedMode: 'multiple',
                            data: ['Staff']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Staff',
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

            //=================Staff Dep Wise
            var myChart8 = echarts.init(document.getElementById('staff_grade_wise'));
            myChart8.setOption({
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

                    name: 'Staff',
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
                url: "graph.asmx/find_staff_summary_report_report_grade",
                //data: '{"GendeR":"' + GenderDep + '" }',
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart8.setOption({
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                type: 'shadow'
                            }
                        },
                        legend: {
                            selectedMode: 'multiple',
                            data: ['Staff']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Staff',
                            type: 'bar',
                            data: JSONObject["yaxis"],
                            color: ['#b05900'],
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

            //=================Staff Dep Wise
            var myChart9 = echarts.init(document.getElementById('staff_religion_wise'));
            myChart9.setOption({
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

                    name: 'Staff',
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
                url: "graph.asmx/find_staff_summary_report_report_religion",
                //data: '{"GendeR":"' + GenderDep + '" }',
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart9.setOption({
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                type: 'shadow'
                            }
                        },
                        legend: {
                            selectedMode: 'multiple',
                            data: ['Staff']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Staff',
                            type: 'bar',
                            data: JSONObject["yaxis"],
                            color: ['#9c27b0'],
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
