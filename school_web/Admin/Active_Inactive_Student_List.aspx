<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Active_Inactive_Student_List.aspx.cs" EnableEventValidation="false" ValidateRequest="false" Inherits="school_web.Admin.Active_Inactive_Student_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Active/Inactive Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }
    </style>


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
                            <li class="breadcrumb-item active" aria-current="page">Active/Inactive Student List</li>
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
                            <div class="table-responsive">
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
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_Section" runat="server" class="form-control find-dv-txtbx">
                                                            <asp:ListItem Value="0">ALL</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Status</label>
                                                        <asp:DropDownList ID="ddl_Status" runat="server" class="form-control find-dv-txtbx">
                                                            <asp:ListItem Value="0">ALL</asp:ListItem>
                                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                                            <asp:ListItem Value="2">Inactive</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div id="content">

                                                            <div class="pgslry-head-div head">

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
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                    </div>
                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <span style="font-size: 14px; font-weight: bold;">Student List-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>



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




                                                            <div style="margin: 18px 0px 19px 0px; padding: 6px 0px 0px 0px; float: left; height: 33px; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; border-top: 1px solid #000; border-bottom: 1px solid #000;">
                                                                <span style="font-size: 14px; font-weight: bold;">Active/Inactive Student-<asp:Label ID="lbl_class222" runat="server"></asp:Label></span>
                                                            </div>

                                                            <table id="Table1" aria-describedby="example2_info" style="background-color: #080a0e; margin-bottom: 5px; float: left;">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="background: #000!important;">
                                                                            <p style="width: auto; float: left; background-color: #080a0e; padding: 3px 38px 3px 3px; margin: 0px; font-size: 16px; color: #fff">
                                                                                Total Student:-<asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                                                                            </p>
                                                                            <p style="width: auto; float: left; background-color: #080a0e; padding: 3px 38px 3px 3px; margin: 0px; font-size: 16px; color: #fff">
                                                                                Total Active Student List:-
                                    <asp:Label ID="lbltotal_active" runat="server">0</asp:Label>

                                                                            </p>
                                                                            <p style="width: auto; float: left; background-color: #080a0e; padding: 3px 38px 3px 3px; margin: 0px; font-size: 16px; color: #fff">
                                                                                Total Inactive:-
                                  <asp:Label ID="lbltotal_inactive" runat="server">0</asp:Label>
                                                                            </p>



                                                                        </th>


                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                            <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Session">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Admission No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Section">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Roll No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Student Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%#Bind("id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_istatus" runat="server" Visible="false" Text='<%#Bind("Status")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>


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
        </div>
        <!--end row-->
    </div>

    <script src="../Echart/echarts.min.js"></script>

    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_class" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />
    <asp:HiddenField ID="hd_status" runat="server" />

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: '',
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
                url: "Active_Inactive_Student_List.aspx/GetChartData",
                data: "{Session: '" + $('#<%=hd_session.ClientID%>').val() + "', Class_id: '" + $('#<%=hd_class.ClientID%>').val() + "', Section: '" + $('#<%=hd_section.ClientID%>').val() + "', Statuss: '" + $('#<%=hd_status.ClientID%>').val() + "'}",
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
            var Status = $('#<%= hd_status.ClientID %>').val();


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
                url: "graph.asmx/Get_student_status_summary",
                data: "{Session: '" + Session + "', Class_id: '" + Class + "', Section: '" + Section + "', Statuss: '" + Status + "'}",
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
    </script>
</asp:Content>
