<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="annual-fee-collection-report.aspx.cs" Inherits="school_web.Admin.annual_fee_collection_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Annual Fees Collection Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal-dialog {
            max-width: 800px;
        }

        .modal {
            background: rgb(0 0 0 / 31%);
        }

        /*.table-responsive {
            overflow-x: inherit;
        }*/

        .home-grph-wpr {
            width: 114%;
            margin: 0px 0px 0px -50px;
        }

        .home-grph-wpr-smll {
            margin: 50px 0px 0px -50px;
        }
    </style>
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Annual Fees Collection Report</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection-annual.aspx">Today Fee Collection Summary</a></li>
                        <li><a href="annual-fee-collection-report.aspx" class="sub-mnu-p-a-active">Today Fees Collection</a></li>
                        <li><a href="report-headwise-fee-collection-annual.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-student-wise-annual-fee-collection.aspx">Accumulated Student Wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-annual-fee-collection_N.aspx">Student & Head wise Fee Collection</a></li>
                    </ul>
                </div>
                <div class="col-xl-12">
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
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Payment Mode</label>

                                                        <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select find-dv-txtbx">
                                                            <asp:ListItem>All</asp:ListItem>
                                                            <asp:ListItem>Cash</asp:ListItem>
                                                            <asp:ListItem>Pos</asp:ListItem>
                                                            <asp:ListItem>Netbanking</asp:ListItem>
                                                            <asp:ListItem>Deposited In Bank</asp:ListItem>
                                                            <asp:ListItem>Sbdebit</asp:ListItem>
                                                            <asp:ListItem>Cheque</asp:ListItem>
                                                            <asp:ListItem>NEFT</asp:ListItem>
                                                            <asp:ListItem>Debitcard</asp:ListItem>
                                                            <asp:ListItem>Creditcard</asp:ListItem>
                                                            <asp:ListItem>Otherdcard</asp:ListItem>
                                                            <asp:ListItem>UPI</asp:ListItem>
                                                             <asp:ListItem>Online</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>



                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
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
                                                <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Date</th>
                                                            <th>Slip No.</th>
                                                            <th>Student Name</th>
                                                            <th>Admission No.</th>
                                                            <th>Session</th>
                                                            <th>Class</th>
                                                            <%-- <th>Payable Amt.</th>--%>
                                                            <%--<th>Disc. Amt.</th>--%>
                                                            <th>Paid Amt.</th>
                                                            <%--  <th>Dues Amt.</th>--%>
                                                            <th>Payment Mode</th>
                                                            <%--<th>Father Name</th>--%>
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
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label7" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("Addmission_no")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label5" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </td>



                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Total_paid_amt")%>'></asp:Label>
                                                                    </td>


                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                        <%--<asp:Label ID="Label1" runat="server" Visible="false" Text='<%#Bind("Total_Payble_amt")%>'></asp:Label>
                                                                        <asp:Label ID="Label9" runat="server" Visible="false" Text='<%#Bind("Total_Disc_amt")%>'></asp:Label>
                                                                        <asp:Label ID="Label8" runat="server" Visible="false" Text='<%#Bind("Total_dues_amt")%>'></asp:Label>--%>
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
                                                                                    <a class="dropdown-item" href="slip/annual-slip.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Slip</span></a>
                                                                                    <%--<a class="dropdown-item" href="slip/Admission_slip.aspx?admissionno=<%#Eval("Addmission_no") %>&academicyear=<%#Eval("Acamedic_Semester_Id") %>&sessionid=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Slip</span></a>--%>
                                                                                </li>
                                                                                <li>
                                                                                    <asp:LinkButton ID="lnkviewDt" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkviewDt_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span>View Details</span></asp:LinkButton>
                                                                                </li>
                                                                            </ul>
                                                                        </div>
                                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("Addmission_no")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_slip_no" runat="server" Text='<%#Bind("Slip_no")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                                <table class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <tr>
                                                        <th style="display: none">Total Payble Amount :
                                                            <asp:Label ID="lbl_fnl_payble" runat="server" Text="Label"></asp:Label></th>
                                                        <th style="display: none">Total Disc. Amount :
                                                            <asp:Label ID="lbl_fnl_disc_amt" runat="server" Text="Label"></asp:Label></th>
                                                        <th>Total Paid Amount :
                                                            <asp:Label ID="lbl_fnl_paid" runat="server" Text="Label"></asp:Label></th>
                                                        <th style="display: none">Total Dues Amount :
                                                            <asp:Label ID="lbl_fnl_duesS" runat="server" Text="Label"></asp:Label></th>
                                                    </tr>
                                                </table>


                                                <%-- =============== --%>
                                                <div class="paid-cat-div">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <p class="paid-cat-div-p">
                                                                <i>Cash </i>:
                                                            <asp:Label ID="lbl_by_cash" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>Netbanking </i>:
                                                            <asp:Label ID="lbl_by_netbanking" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>Deposited In Bank</i> :
                                                            <asp:Label ID="lbl_by_deposit" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>Sbdebit </i>:
                                                            <asp:Label ID="lbl_by_sb" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                        </div>


                                                        <div class="col-sm-4">
                                                            <p class="paid-cat-div-p">
                                                                <i>Cheque </i>:
                                                            <asp:Label ID="lbl_by_chequeS" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>NEFT </i>:
                                                            <asp:Label ID="lbl_by_neft" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>Debitcard </i>:
                                                            <asp:Label ID="lbl_by_debitcard" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>Creditcard </i>:
                                                            <asp:Label ID="lbl_by_credit_card" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <p class="paid-cat-div-p">
                                                                <i>Otherdcard </i>:
                                                            <asp:Label ID="lbl_by_other_card" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>UPI </i>:
                                                            <asp:Label ID="lbl_by_upi" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>Branch</i> :
                                                            <asp:Label ID="lbl_by_branch" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                <i>Pos</i> :
                                                            <asp:Label ID="lbl_pos" runat="server" Text="00"></asp:Label>
                                                            </p>

                                                        </div>


                                                         <div class="col-sm-4">
                                                            <p class="paid-cat-div-p">
                                                                <i>Online </i>:
                                                            <asp:Label ID="lbl_online" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                               
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                                
                                                            </p>
                                                            <p class="paid-cat-div-p">
                                                               
                                                            </p>
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
    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->


    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title">Fees Details</h5>
                    <a href="#!" onclick="closeModal()" class="btn btn-secondary">Close</a>
                    <%--<asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />--%>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <td>Parameter</td>
                                        <th>Date</th>
                                        <th>Fees Type</th>
                                        <th>Payble Amt.</th>
                                        <th>Paid Amt.</th>
                                        <th>Dues Amt.</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rp_fee_info" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("parameter")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("feetype")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lbl_payable" runat="server" Text='<%#Bind("payable")%>'></asp:Label>
                                                </td>

                                                <td style="text-align: left;">
                                                    <asp:Label ID="lbl_paid" runat="server" Text='<%#Bind("paid")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("dues")%>'></asp:Label>
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


    <!--end page wrapper -->
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function closeModal() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>



    <asp:HiddenField ID="hd_sessions" runat="server" />
    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_from_date" runat="server" />
    <asp:HiddenField ID="hd_to_date" runat="server" />

    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Annual Fees Collection Summary',
                width: 940,
                height: 400,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '95%' },
                isStacked: true,
                is3D: true,
                colors: ['#00a7cd', '#ff7703', '#ec8f6e', '#f3b49f', '#f6c7b6'],
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
                url: "annual-fee-collection-report.aspx/GetChartData",
                data: "{Session: '" + $('#<%=hd_session.ClientID%>').val() + "', From_date: '" + $('#<%=hd_from_date.ClientID%>').val() + "', To_date: '" + $('#<%=hd_to_date.ClientID%>').val() + "'}",
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

            var Session = $('#<%= hd_sessions.ClientID %>').val();
            var From_date = $('#<%= hd_from_date.ClientID %>').val();
            var To_date = $('#<%= hd_to_date.ClientID %>').val();

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
                url: "graph.asmx/Get_annual_fee_mode",
                data: "{Session: '" + Session + "', From_date: '" + From_date + "', To_date: '" + To_date + "'}",
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
