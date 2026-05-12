<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="userwise-payment-collection-report.aspx.cs" Inherits="school_web.Admin.userwise_payment_collection_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Userwise Collection Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="slip/payment-slip.css" rel="stylesheet" />
    <style>
        th {
            font-weight: 500;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 25px;
            height: 25px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }

        .table-responsive {
            overflow-x: inherit;
        }


        .print-dv {
            margin: 0px auto;
            padding: 0px;
            width: 1196px;
            height: auto;
        }

        .print-dv-bx-wpr-inr {
            margin: 5px 0px 0px 0px;
            padding: 0px 0px;
            width: 100%;
            float: left;
        }

        .card-body {
            overflow: auto;
        }

        table td {
            text-align: center;
            padding: 1px 0px !important;
            border: 1px solid #ddd;
        }
    </style>



    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="slip/payment-slip.css" rel="stylesheet" type="text/css" />');
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
    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_sessions" runat="server" />
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
                            <li class="breadcrumb-item active" aria-current="page">UserWise Collection Report</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="Today_Collection.aspx">Day End Collection Report</a></li>
                        <li><a href="overall-collection-report.aspx">Head Wise Collection Report</a></li>
                        <%--<li><a href="day-end-report-typewise.aspx">Monthly Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-admission-fee.aspx">Admission Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-annual-fee.aspx">Annual Fee Collection Report</a></li>--%>
                        <li><a href="day-end-report-of-form-sale.aspx">Form Sale Report</a></li>
                        <li><a href="day_End_Report_Summery_N.aspx">Day End Summary</a></li>
                        <%--<li><a href="day-end-report-summery-headwise.aspx">Day End Summary Headwise</a></li>
                        <li><a href="Fee_Collection_Report.aspx">Fees Collection Summary</a></li>--%>
                        <li><a href="userwise-payment-collection-report.aspx" class="sub-mnu-p-a-active">User Wise Collection Report</a></li>
                    </ul>
                </div>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">User</label>
                                                        <asp:DropDownList ID="ddl_user" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
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
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                </div>
                                            </div>


                                            <asp:Panel ID="pnl_contnt_dv" runat="server" Visible="false">
                                                <div class="print-btn-sec" style="float: right">
                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                                                </div>

                                                <div class="print-dv-bx-wprrr" id="tblPrintIQ" runat="server">
                                                    <div class="print-dv">
                                                        <div class="print-dv-inr">
                                                            <div class="print-dv-bx-wpr">
                                                                <div class="head-printdv" style="border-bottom: 1px solid #ddd; margin: 2px 0px 0px 0px; float: left; width: 100%;">
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
                                                                            <span style="font-size: 14px; font-weight: bold;">Userwise Collection Summary Report of  :
                                                                            <asp:Label ID="lbl_report_of" runat="server"></asp:Label></span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>







                                                            <div class="print-dv-bx-wpr">
                                                                <h2 class="print-dv-bx-wpr-title-h">Fee Collection Summary</h2>
                                                                <div class="print-dv-bx-wpr-inr">
                                                                    <asp:Panel ID="adm_anul_tbls" runat="server">

                                                                        <table class="table-bordered" style="width: 100%">
                                                                            <tr>
                                                                                <td class="fntblds">Total Collection :
                                                            <asp:Label ID="lbl_paid_amount" runat="server" Text="0.00"></asp:Label></td>
                                                                                <td class="fntblds">Total Monthly Fee :
                                                            <asp:Label ID="lbl_ttl_mnthly_fee" runat="server" Text="0.00"></asp:Label></td>
                                                                                <td class="fntblds">Total Admission Fee :
                                                            <asp:Label ID="lbl_ttl_admission_fee" runat="server" Text="0.00"></asp:Label></td>

                                                                                <td class="fntblds">Total Annual Fee :
                                                            <asp:Label ID="lbl_ttl_annual_fee" runat="server" Text="0.00"></asp:Label></td>

                                                                                <td class="fntblds">Total Form Sale :
                                                            <asp:Label ID="lbl_form_seal" runat="server" Text="0.00"></asp:Label></td>

                                                                                <td class="fntblds">Total Other Fee :
                                                            <asp:Label ID="lbl_total_otherfee" runat="server" Text="0.00"></asp:Label></td>
                                                                            </tr>
                                                                        </table>

                                                                        <table class="table-bordered" style="width: 100%">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th class="fntsizes">#</th>
                                                                                    <th class="fntsizes">Date</th>
                                                                                    <th class="fntsizes">Slip No.</th>
                                                                                    <th class="fntsizes">Student Name</th>
                                                                                    <th class="fntsizes">Admission No.</th>
                                                                                    <th class="fntsizes">Class</th>
                                                                                    <th class="fntsizes">Paid Amt.</th>
                                                                                    <th class="fntsizes">Payment Mode</th>
                                                                                    <th class="fntsizes">Type</th>
                                                                                    <th class="fntsizes">User Id</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:Repeater ID="rd_view" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="Label7" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="fntsizes">
                                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: left;">
                                                                                                <asp:Label ID="lbl_type" runat="server" Text='<%#Bind("parameter_New")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: left;">
                                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("user_id")%>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>

                                                                                <%--<tr>
                                                                                <td colspan="2" class="txtalign-right txtbolds">Total : </td>
                                                                                <td class="txtbolds">
                                                                                    <asp:Label ID="lbl_ttl_payble_a" runat="server" Text="Label"></asp:Label>
                                                                                </td>
                                                                                <td class="txtbolds">
                                                                                    <asp:Label ID="lbl_ttl_disc_a" runat="server" Text="Label"></asp:Label>
                                                                                </td>
                                                                                <td class="txtbolds">
                                                                                    <asp:Label ID="lbl_ttl_after_disc_a" runat="server" Text="Label"></asp:Label>
                                                                                </td>
                                                                                <td class="txtbolds">
                                                                                    <asp:Label ID="lbl_ttl_paid_amt_a" runat="server" Text="Label"></asp:Label>
                                                                                </td>
                                                                                <td class="txtbolds">
                                                                                    <asp:Label ID="lbl_ttl_dues_a" runat="server" Text="Label"></asp:Label>
                                                                                </td>
                                                                            </tr>--%>
                                                                            </tbody>
                                                                        </table>

                                                                        <div class="footer-date-dv">
                                                                            <p class="footer-date-p">
                                                                                Print Date Time :
                                                                            <asp:Label ID="lbl_p_date" runat="server"></asp:Label>
                                                                            </p>
                                                                        </div>
                                                                        <div class="footer-usr-dv">
                                                                            <asp:Label ID="lbl_sig_name" class="footer-usr-sig-name-p" runat="server"></asp:Label>
                                                                            <p class="footer-usr-sig-p">Signature</p>
                                                                        </div>
                                                                    </asp:Panel>

                                                                </div>
                                                            </div>










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
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->
</asp:Content>
