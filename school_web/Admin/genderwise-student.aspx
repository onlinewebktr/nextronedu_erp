<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="genderwise-student.aspx.cs" Inherits="school_web.Admin.genderwise_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Gender wise Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .home-grph-wpr {
            width: 114%;
            margin: 0px 0px 0px -110px;
        }
    </style>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
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
                <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Reports</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a></li>
                            <li class="breadcrumb-item active" aria-current="page">Gender wise Student's List</li>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Gender</label>
                                                        <asp:DropDownList ID="ddl_Gender" runat="server" class="form-select find-dv-txtbx">
                                                            <asp:ListItem>ALL</asp:ListItem>
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                            <asp:ListItem>Transgender</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
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
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div style="margin: 18px 0px 19px 0px; padding: 6px 0px 0px 0px; float: left; height: 33px; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; border-top: 1px solid #000; border-bottom: 1px solid #000;">
                                                        <span style="font-size: 14px; font-weight: bold;">Gender Wise Student List-<asp:Label ID="lbl_class222" runat="server"></asp:Label></span>
                                                    </div>
                                                    <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Adm. No.</th> 
                                                                <th>Adm. Date</th> 
                                                                <th>Admission Type</th>
                                                                <th>Gender</th> 
                                                                <th>Class</th>
                                                                <th>Session</th>
                                                                <th>Section</th>
                                                                <th>Roll No.</th>
                                                                <th>Student Name</th>
                                                                <th>Father Name</th>
                                                                <th>Action</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </td> 
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_admissiontype" runat="server" Text='<%#Bind("Admission_Type")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label7" runat="server" Text='<%#Bind("gender")%>'></asp:Label>
                                                                        </td> 
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_sesion" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                    href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                    <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                        <i class="bx bx-grid-horizontal"></i>
                                                                                    </div>
                                                                                </a>
                                                                                <ul class="dropdown-menu dropdown-menu-end"> 
                                                                                    <li>
                                                                                        <a class="dropdown-item" href="student-details.aspx?admNo=<%#Eval("admissionserialnumber") %>&ssion=<%#Eval("Session_id") %>&clss=<%#Eval("Class_id") %>" target="_blank"><i class='bx bx-printer'></i><span>View Student Details</span></a>
                                                                                    </li> 
                                                                                </ul>
                                                                            </div>
                                                                            <%--<asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>--%>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
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

    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_class" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />
    <asp:HiddenField ID="hd_gender" runat="server" />

    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'GenderWise Student Summary',
                width: 980,
                height: 420,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '95%' },
                isStacked: true,
                colors: ['#5470C6', '#91CC75', '#FAC858', '#EE6666', '#f6c7b6'],
                hAxis: {
                    textStyle: {
                        fontSize: 10, // or the number you want
                        italic: true
                    }
                },
            };
            $.ajax({
                type: "POST",
                url: "genderwise-student.aspx/GetChartData",
                data: "{Session: '" + $('#<%=hd_session.ClientID%>').val() + "', Class_id: '" + $('#<%=hd_class.ClientID%>').val() + "', Section: '" + $('#<%=hd_section.ClientID%>').val() + "', Gender: '" + $('#<%=hd_gender.ClientID%>').val() + "'}",
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
            var Gender = $('#<%= hd_gender.ClientID %>').val();


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
                url: "graph.asmx/Get_student_Genderwise",
                data: "{Session: '" + Session + "', Class_id: '" + Class + "', Section: '" + Section + "', Gender: '" + Gender + "'}",
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
