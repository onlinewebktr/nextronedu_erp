<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="attendance-dashboard.aspx.cs" Inherits="school_web.Payroll.attendance_dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Attendance Dashboard
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
                                    <h6 class="mb-0">Attendance Summary
                                        <asp:TextBox ID="txt_att_date" runat="server" class="form-control datepicker"  Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;" AutoPostBack="true" OnTextChanged="txt_att_date_TextChanged"></asp:TextBox> 
                                    </h6>
                                </div>
                            </div>
                            <div id="attendance_summary" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
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
                                    <h6 class="mb-0">Attendance Summary Department Wise
                                        <asp:TextBox ID="txt_Dep_date" runat="server" class="form-control datepicker"  Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;" AutoPostBack="true" OnTextChanged="txt_Dep_date_TextChanged"></asp:TextBox> 
                                    </h6>
                                </div>
                            </div>
                            <div id="attendance_summary_dep" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
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
                                    <h6 class="mb-0">Attendance Summary Grade Wise
                                        <asp:TextBox ID="txt_grade_date" runat="server" class="form-control datepicker"  Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;" AutoPostBack="true" OnTextChanged="txt_grade_date_TextChanged"></asp:TextBox> 
                                    </h6>
                                </div>
                            </div>
                            <div id="attendance_summary_grade" style="width: 116%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -75px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     
    <asp:HiddenField ID="hd_date" runat="server" />
    <asp:HiddenField ID="hd_dep_date" runat="server" />
    <asp:HiddenField ID="hd_grade_date" runat="server" />
    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
             
            var Date = $('#<%= hd_date.ClientID %>').val();
            var Dep_Date = $('#<%= hd_dep_date.ClientID %>').val();
            var Grd_Date = $('#<%= hd_grade_date.ClientID %>').val();
            //=================Order SummarY
            var myChart6 = echarts.init(document.getElementById('attendance_summary'));
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
                url: "graph.asmx/find_staff_attendance_summary_report",
                data: '{"DatE":"' + Date + '" }',
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


            //====================
            var myChart1 = echarts.init(document.getElementById('attendance_summary_dep'));
            myChart1.setOption({
                title: { 
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
                url: "graph.asmx/find_staff_attendance_summary_report_depwise",
                data: '{"DatE":"' + Dep_Date + '" }',
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


            //====================
            var myChart11 = echarts.init(document.getElementById('attendance_summary_grade'));
            myChart11.setOption({
                title: {
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
                url: "graph.asmx/find_staff_attendance_summary_report_gradewise",
                data: '{"DatE":"' + Grd_Date + '" }',
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart11.setOption({
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
        });
    </script>
</asp:Content>
