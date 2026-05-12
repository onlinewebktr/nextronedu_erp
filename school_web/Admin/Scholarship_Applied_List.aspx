<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_Applied_List.aspx.cs" Inherits="school_web.Admin.Scholarship_Applied_List" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Scholarship Applied List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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


        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .modal {
            background: rgb(0 0 0 / 52%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        .nowordbreak {
            white-space: nowrap;
            word-break: keep-all;
        }

        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 600px;
            }
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

            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-4">
                    <div class="card radius-10 border-start border-0 border-3 border-info">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div>
                                    <p class="mb-0 text-secondary">Total Student Scholarship</p>
                                    <h4 class="my-1 text-info" runat="server" id="ttlodR">00</h4>
                                    <p class="mb-0 font-13" runat="server" id="ttlodRLstWeeK">+00% from last week</p>
                                </div>
                                <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                    <i class='bx bx-user'></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4">
                    <div class="card radius-10 border-start border-0 border-3 border-danger">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div>
                                    <p class="mb-0 text-secondary">Total Paid Student Scholarship</p>
                                    <h4 class="my-1 text-danger" runat="server" id="ttlRvnuE">₹00</h4>
                                    <p class="mb-0 font-13" runat="server" id="ttlRevenueLstWeeK">+00% from last week</p>
                                </div>
                                <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                    <i class='bx bx-buildings'></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4">
                    <div class="card radius-10 border-start border-0 border-3 border-success">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div>
                                    <p class="mb-0 text-secondary">Total Unpaid Student Scholarship</p>
                                    <h4 class="my-1 text-success" runat="server" id="ttlCancelAmt">00</h4>
                                    <p class="mb-0 font-13" runat="server" id="ttlCancelAmtLstWeek">-00 from last week</p>
                                </div>
                                <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                    <i class='bx bxs-bar-chart-alt-2'></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Scholarship</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Applied Student List for Scholarship </li>
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
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">


                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Payment Status</label>
                                                        <asp:DropDownList ID="ddlPaymentmode" runat="server" class="form-select find-dv-txtbx">
                                                            <asp:ListItem>ALL</asp:ListItem>
                                                            <asp:ListItem>Unpaid</asp:ListItem>
                                                            <asp:ListItem>Paid</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Scholarship Name</label>
                                                        <asp:DropDownList ID="ddl_test_name" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>




                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_btn_find_date" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_btn_find_date_Click" />
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 0px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
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
                                                                        <span style="font-size: 14px; font-weight: bold;">Scholarship Applied List
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <table id="example2" class="table table-striped table-bordered dataTable" data-page-length='1000' role="grid" aria-describedby="example2_info">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Action</th>
                                                                            <th>Scholarship Name</th>
                                                                            <th>Reg. No.</th>
                                                                            <th>Reg. Date</th>
                                                                            <th>Scholarship For</th>
                                                                            <th>Session</th>
                                                                            <th>Student Name</th>
                                                                            <th>Father Name</th>
                                                                            <th>Mobile No.</th>

                                                                            <%--<th>Payment Mode</th>--%>
                                                                            <%--<th>Payment id</th>--%>
                                                                            <%--<th>Payment Status</th>--%>
                                                                            <th>School Branch</th>
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
                                                                                        <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                            <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                                href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                                <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                                    <i class="bx bx-grid-horizontal"></i>
                                                                                                </div>
                                                                                            </a>
                                                                                            <ul class="dropdown-menu dropdown-menu-end">

                                                                                                <li>
                                                                                                    <a class="dropdown-item" href="slip/Scholarship_reg.aspx?regiDs=<%#Eval("Registration_id") %>" target="_blank"><span>Print Student Info</span> </a>
                                                                                                </li>


                                                                                                <li>
                                                                                                    <asp:LinkButton ID="lnk_update_payment" class="dropdown-item" runat="server" OnClick="lnk_update_payment_Click">Make Payment</asp:LinkButton>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </div>



                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_fee_amount" runat="server" Text='<%#Bind("Payable_amount")%>' Visible="false"></asp:Label>

                                                                                        <asp:Label ID="lbl_payment_mode" runat="server" Visible="false" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_razorpay_payment_id" runat="server" Visible="false" Text='<%#Bind("razorpay_payment_id")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_Payment_Status" runat="server" Visible="false" Text='<%#Bind("Payment_Status")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Test_name" runat="server" Text='<%#Bind("Test_name")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("Registration_id")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Father_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Student_mob_no")%>'></asp:Label>
                                                                                    </td>
                                                                                    <%--<td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_razorpay_payment_id" runat="server" Text='<%#Bind("razorpay_payment_id")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Payment_Status" runat="server" Text='<%#Bind("Payment_Status")%>'></asp:Label>
                                                                                    </td>--%>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_school" class="nowordbreak" runat="server" Text='<%#Bind("School_branch_address")%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
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
            </div>
        </div>
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->



    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 600px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Update Payment</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="row g-3 needs-validation" novalidate="">
                        <div class="col-md-12">
                            <label for="validationCustom01" class="form-label">Student Name <sup>* </sup></label>
                            <asp:Label ID="lbl_std_name" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <label for="validationCustom01" class="form-label">Registration No. <sup>* </sup></label>
                            <asp:Label ID="lbl_reg_no" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <label for="validationCustom01" class="form-label">Registration Fee <sup>* </sup></label>
                            <asp:Label ID="lbl_reg_fee" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label for="validationCustom01" class="form-label">Payment Mode <sup>*</sup></label>
                            <asp:DropDownList ID="ddl_payment_mode" runat="server" class="form-select" OnSelectedIndexChanged="ddl_payment_mode_SelectedIndexChanged" AutoPostBack="true">
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
                                <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                <asp:ListItem>UPI</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12" runat="server" id="pnl_mode_t_n_dv" visible="false">
                            <label for="validationCustom01" class="form-label" id="lbl_mode_trns_no_adm" runat="server">Transaction No. <sup>* </sup></label>
                            <asp:TextBox ID="txt_transaction_no" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label for="validationCustom01" class="form-label">Remark</label>
                            <asp:TextBox ID="txt_remark" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-12">
                            <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
