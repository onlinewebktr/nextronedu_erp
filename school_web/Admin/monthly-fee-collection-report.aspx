<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="monthly-fee-collection-report.aspx.cs" Inherits="school_web.Admin.monthly_fee_collection_report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Monthly Fees Collection Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal-dialog {
            max-width: 800px;
        }

        .modal {
            background: rgb(0 0 0 / 31%);
        }

        .home-grph-wpr {
            width: 114%;
            margin: 0px 0px 0px -50px;
        }

        .home-grph-wpr-smll {
            margin: 50px 0px 0px -50px;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Monthly Fees Collection Report</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection-monthly.aspx">Today Fee Collection</a></li>
                        <li><a href="monthly-fee-collection-report.aspx" class="sub-mnu-p-a-active">Bill-wise Fee Collection</a></li>
                        <li><a href="monthly-fee-collection-monthwise.aspx">Month-Wise Fee Collection</a></li>
                        <li><a href="report-headwise-fee-collection-monthly.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-monthley-fee-collection_N.aspx">Student & Head wise Fee Collection</a></li>
                         <li><a href="fee-collection-details.aspx">Student Payment Status</a></li>
                        <li><a href="Online_Monthly_Payment_History.aspx">Online Fee Collection (APP)</a></li>


                        <%--<li><a href="report-today-fees-collection-monthly.aspx">Today Monthly Fee Collection Summary</a></li>
                        <li><a href="monthly-fee-collection-monthwise.aspx">Monthly Fee Collection Month-Wisek</a></li>
                        <li><a href="monthly-fee-collection-report.aspx" class="sub-mnu-p-a-active">Today Monthly Fee Collection </a></li> 
                        <li><a href="report-headwise-fee-collection-monthly.aspx">Head wise Fee Collection(Day Boarding)</a></li>
                         <li><a href="report-mode-headwise-fee-collection.aspx">Payment Mode & Head wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-monthley-fee-collection_N.aspx">Student & Head wise Fee Collection</a></li>
                        <li><a href="typewise-fee-collection-report.aspx">Month Wise Fee Collection</a></li>
                        <li><a href="Online_Monthly_Payment_History.aspx">Online Monthly Fee Collection</a></li>--%>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
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
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="home-grph-wpr" style="display: none">
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
                                                <div class="prnt-dv-wpr printborder">


                                                    <div class="head-printdv" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">

                                                        <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                            <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                        </div>
                                                        <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                            <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
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
                                                                <span style="font-size: 14px; font-weight: bold;">Time Period-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table id="datatable" class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Date</th>
                                                                    <th>Slip No.</th>
                                                                    <th>Student Name</th>
                                                                    <th>Admission No.</th>
                                                                    <th>Mobile No.</th>
                                                                    <th>Class</th>
                                                                    <th>Paid Amt.</th>
                                                                    <th>Payment Mode</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
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
                                                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("session")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_mobile_no" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Amount1" runat="server" Text='<%#Bind("Amount1")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
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
                                                                                            <asp:Panel ID="Panel2_print" runat="server">
                                                                                                <a class="dropdown-item" href="slip/monthly-slip.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Slip</span></a>
                                                                                            </asp:Panel>
                                                                                        </li>

                                                                                    </ul>
                                                                                </div>
                                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("Addmission_no")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_slip_no" runat="server" Text='<%#Bind("Slip_no")%>' Visible="false"></asp:Label>
                                                                                <asp:LinkButton ID="lnkviewDt" class="dropdown-item" runat="server" CausesValidation="false" Style="display: none" OnClick="lnkviewDt_Click" ToolTip="Edit" Visible="false"> <i class="lni lni-pencil-alt"></i><span>View Details</span></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                        <table class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <tr>

                                                                <th>Total Paid Amount :
                                                            <asp:Label ID="lbl_fnl_paid" runat="server" Text="Label"></asp:Label></th>

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
                                                                        <i>Pos </i>:
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
                                                                </div>

                                                            </div>






                                                        </div>

                                                    </asp:Panel>
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
                title: 'Monthly Fees Collection Summary',
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
                url: "monthly-fee-collection-report.aspx/GetChartData",
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
                url: "graph.asmx/Get_monthly_fee_mode",
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
