<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Custome_Student_List_Download_Excel.aspx.cs" Inherits="school_web.Admin.Custome_Student_List_Download_Excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Customised Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/dropdownmultiselection/style.css" rel="stylesheet" />
    <script src="../assets/dropdownmultiselection/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/print-custom-std-print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
    <style>
        .head {
            display: none;
        }

        label {
            display: inline-block !important;
            font-size: 13px !important;
            color: #000;
            margin-left: 9px !important;
            font-weight: bold;
        }

        .home-grph-wpr-dv {
            width: 100%;
            float: left;
            overflow: hidden;
        }


        .home-grph-wpr {
            width: 114%;
            margin: 0px 0px 0px -110px;
        }

        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 43%);
        }

        .modal-header {
            padding: 0.3rem 1rem;
        }

        .hdrmodify table tr th {
            text-align: left;
            font-size: 12px;
            color: #fff;
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 12px;
            text-align: left;
        }

        .popupheadinG {
            margin: -1px 0px 0px 0px;
            padding: 2px 0px 3px 5px;
            width: 100%;
            float: left;
            font-size: 16px;
            font-weight: 500;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #ddd;
            background: #ffba5f;
            color: #000;
        }

        .find-dv-lbl {
            margin: 0px !important;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_classs.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_sections.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Customised Student List</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <%--<asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                                    <asp:ListBox ID="ddl_classs" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                    <asp:ListBox ID="ddl_sections" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                                    <%--<asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>--%>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Student Type</label>
                                                    <asp:DropDownList ID="ddl_studenttype" runat="server" class="form-control find-dv-txtbx">
                                                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                        <asp:ListItem Value="New">New Admission</asp:ListItem>
                                                        <asp:ListItem Value="NT">Old Admission</asp:ListItem>
                                                        <asp:ListItem Value="Transferred">Transferred to Next Session</asp:ListItem>
                                                        <asp:ListItem Value="RTE">RTE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                </div>


                                                <div class="col-sm-3">
                                                    <a id="btnExportCSV" class="btn btn-primary find-dv-btn"><i class='bx bx-download'></i>Excel</a>
                                                    <asp:LinkButton ID="btn_excels" Visible="false" Style="display: none" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>


                                                    <a href="#" data-toggle="modal" data-target="#myModal" class="btn btn-primary find-dv-btn"><i class="bx bx-customize"></i></a>
                                                </div>
                                            </div>
                                        </div>


                                        <div id="tblPrintIQ" runat="server">
                                            <div class="prnt-dv-wpr printborder">
                                                <div class="pgslry-head-div head" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                    <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                        <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                    </div>
                                                    <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                        <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                            <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                        </h1>
                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            <span style="font-size: 14px; font-weight: bold;">Student List <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                        </div>
                                                    </div>
                                                </div>


                                                <%--<div class="home-grph-wpr-dv">
                                                    <div class="home-grph-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-9">
                                                                <div id="chart"></div>
                                                            </div>
                                                            <div class="col-xl-3">
                                                                <div class="home-grph-wpr-smll">
                                                                    <div id="daily_collection" class="card card-statistic-2" style="width: 100%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 10px;">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>


                                                <div class="grd-wpr" id="reportS">
                                                    <table id="Table1" aria-describedby="example2_info" style="display: none; background-color: #080a0e; margin-bottom: 15px;">
                                                        <thead>
                                                            <tr>
                                                                <th style="background: #c7c7c7!important;">
                                                                    <p style="width: auto; float: left; background-color: #e9e9e9; padding: 3px 38px 3px 3px; margin: 0px; font-size: 14px; color: #282828;">
                                                                        Total Student:-<asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                                                                    </p>
                                                                    <p style="width: auto; float: left; background-color: #e9e9e9; padding: 3px 38px 3px 3px; margin: 0px; font-size: 14px; color: #282828;">
                                                                        Total Readmission:-
                                    <asp:Label ID="lbltotal_readmission" runat="server">0</asp:Label>

                                                                    </p>
                                                                    <p style="width: auto; float: left; background-color: #e9e9e9; padding: 3px 38px 3px 3px; margin: 0px; font-size: 14px; color: #282828;">
                                                                        Total New Admission:-
                                  <asp:Label ID="lbl_newadmission" runat="server">0</asp:Label>
                                                                    </p>


                                                                    <p style="width: auto; float: left; background-color: #e9e9e9; padding: 3px 38px 3px 3px; margin: 0px; font-size: 14px; color: #282828;">
                                                                        Total Transferred To Next Session:-
                                  <asp:Label ID="lbl_total_trasfer_tonextsession" runat="server">0</asp:Label>
                                                                    </p>
                                                                </th>


                                                            </tr>
                                                        </thead>
                                                    </table>


                                                    <div class="table-responsive">

                                                        <asp:GridView ID="grd_studentlist" runat="server" Width="100%" Style="text-align: center;" class="table table-bordered sortable-table"></asp:GridView>
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
        </div>
        <!--end row-->
    </div>


    <script>
        $(document).ready(function () {
            $(".sortable-table th").click(function () {
                var table = $(this).parents("table").eq(0);
                var rows = table.find("tr:gt(0)").toArray().sort(comparer($(this).index()));
                this.asc = !this.asc;
                if (!this.asc) {
                    rows = rows.reverse();
                }
                for (var i = 0; i < rows.length; i++) {
                    table.append(rows[i]);
                }
            });

            function comparer(index) {
                return function (a, b) {
                    var valA = getCellValue(a, index), valB = getCellValue(b, index);
                    return $.isNumeric(valA) && $.isNumeric(valB) ?
                        valA - valB : valA.toString().localeCompare(valB);
                };
            }
            function getCellValue(row, index) {
                return $(row).children("td").eq(index).text();
            }
        });
    </script>

    <script>
        $("#btnExportCSV").click(function () {
            var csv = [];
            $("#<%= grd_studentlist.ClientID %> tr").each(function () {
                var row = [];
                $(this).find("th,td").each(function () {
                    var text = $(this).text().trim();

                    // Escape double quotes and wrap value in quotes to handle commas
                    text = '"' + text.replace(/"/g, '""') + '"';

                    row.push(text);
                });
                csv.push(row.join(","));
            });

            var csvString = csv.join("\n");
            var blob = new Blob([csvString], { type: "text/csv;charset=utf-8;" });
            var link = document.createElement("a");

            link.href = URL.createObjectURL(blob);
            link.download = "StudentList.csv";
            link.click();
        });
    </script>


    <!--end page wrapper -->
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 1000px;
                margin: 1.75rem auto;
            }
        }
    </style>
    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Mark Filled   <span class="chkbx-all">
                        <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" Style="color: #fff!important" /></span></h5>
                    <a href="#!" data-dismiss="modal" class="btn btn-secondary">Close</a>
                </div>
                <div class="modal-body">
                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                        <div class="c-feild-wprs">
                            <asp:Repeater ID="RpList1" runat="server" OnItemDataBound="RpList1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="c-feild-wprs-inrs" runat="server" id="tdDVS">
                                        <asp:CheckBox ID="chk_column_name" runat="server" Text='<%#Bind("Name")%>' />
                                        <asp:Label ID="lbl_column" Visible="false" runat="server" Text='<%#Bind("Columns_name")%>'></asp:Label>
                                        <asp:Label ID="lbl_heading_name" Visible="false" runat="server" Text='<%#Bind("Heading_name")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:Button ID="Button1" runat="server" Text="Save & Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                        <asp:Button ID="btn_reset" runat="server" Text="Reset" class="btn btn-danger find-dv-btn" OnClick="btn_reset_Click" />

                        <style>
                            .c-feild-wprs {
                                margin: 0px;
                                padding: 0px;
                                width: 100%;
                                float: left;
                                border: 1px solid #ddd;
                            }

                            .c-feild-wprs-inrs {
                                margin: 0px;
                                padding: 2px 5px;
                                width: 25%;
                                float: left;
                                border: 1px solid #ddd;
                                border-top: 0px;
                                border-left: 0px;
                            }

                                .c-feild-wprs-inrs label {
                                    margin: 3px 0px 0px 5px !important;
                                    padding: 0px;
                                    float: left;
                                    font-weight: 500;
                                }

                                .c-feild-wprs-inrs input {
                                    margin: 5px 0px 5px 0px;
                                    padding: 0px;
                                    float: left;
                                }

                            .fullWidthDV {
                                margin: 0px;
                                padding: 0px;
                                width: 100%;
                                float: left;
                            }
                        </style>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>


    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_class" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />
    <asp:HiddenField ID="hd_student_Type" runat="server" />

    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <%--<script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Student Summary',
                width: 980,
                height: 420,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '95%' },
                isStacked: true,
                colors: ['#00a7cd', '#ff7703', '#ec8f6e', '#f3b49f', '#f6c7b6'],
                hAxis: {
                    textStyle: {
                        fontSize: 10, // or the number you want
                        italic: true
                    }
                },
            };
            $.ajax({
                type: "POST",
                url: "student-list.aspx/GetChartData",
                data: "{Session: '" + $('#<%=hd_session.ClientID%>').val() + "', Class_id: '" + $('#<%=hd_class.ClientID%>').val() + "', Section: '" + $('#<%=hd_section.ClientID%>').val() + "', Student_type: '" + $('#<%=hd_student_Type.ClientID%>').val() + "'}",
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


        //==============================Order Status SummarY

        $(document).ready(function () {

            var Session = $('#<%= hd_session.ClientID %>').val();
            var Class = $('#<%= hd_class.ClientID %>').val();
            var Section = $('#<%= hd_section.ClientID %>').val();
            var Student_Type = $('#<%= hd_student_Type.ClientID %>').val();


            var myChart2 = echarts.init(document.getElementById('daily_collection'));
            myChart2.setOption({

                title: {
                    text: '',
                    subtext: '',
                    left: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{a} <br/>{b} : {c} ({d}%)'
                },
                legend: {
                    orient: 'vertical',
                    left: 'left',
                    data: ['IPD', 'OPD', 'EMERGNCY', 'PROCEDURE', 'LAB']
                },
                series: [
                    {
                        name: 'Student',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '60%'],
                        data: [],
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "graph.asmx/Get_student_status_summary_counts",
                data: "{Session: '" + Session + "', Class_id: '" + Class + "', Section: '" + Section + "', Student_type: '" + Student_Type + "'}",
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart2.setOption({
                        legend: {

                            data: JSONObject["nmv"],
                        },

                        series: [{
                            data: JSONObject["nmv"],
                        }
                        ]
                    });
                },

            });
        });
    </script>--%>
</asp:Content>
