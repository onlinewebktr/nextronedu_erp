<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Online_Monthly_Payment_History.aspx.cs" Inherits="school_web.Admin.Online_Monthly_Payment_History" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Online Monthly Payment History
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

        .card {
            position: relative;
            display: unset;
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
                            <li class="breadcrumb-item active" aria-current="page">Online Monthly Payment History</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection-monthly.aspx">Today Fee Collection</a></li>
                        <li><a href="monthly-fee-collection-report.aspx">Bill-wise Fee Collection</a></li> 
                        <li><a href="monthly-fee-collection-monthwise.aspx">Month-Wise Fee Collection</a></li>
                        <li><a href="report-headwise-fee-collection-monthly.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-monthley-fee-collection_N.aspx">Student & Head wise Fee Collection</a></li>
                        <li><a href="fee-collection-details.aspx">Student Payment Status</a></li>
                        <li><a href="Online_Monthly_Payment_History.aspx" class="sub-mnu-p-a-active">Online Fee Collection (APP)</a></li> 

                        <%-- <li><a href="report-today-fees-collection-monthly.aspx">Today Monthly Fee Collection Summary</a></li>
                        <li><a href="monthly-fee-collection-monthwise.aspx">Monthly Fee Collection Month-Wisek</a></li>
                        <li><a href="monthly-fee-collection-report.aspx">Today Monthly Fee Collection </a></li> 
                        <li><a href="report-headwise-fee-collection-monthly.aspx">Head wise Fee Collection(Day Boarding)</a></li> 
                        <li><a href="report-student-headwise-monthley-fee-collection_N.aspx">Student & Head wise Fee Collection</a></li>
                        <li><a href="typewise-fee-collection-report.aspx">Month Wise Fee Collection</a></li>
                        <li><a href="Online_Monthly_Payment_History.aspx" >Online Monthly Fee Collection</a></li>  --%>
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
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
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
                                                        <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                                            <table id="example21" class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Date</th>
                                                                        <th>Slip No.</th>
                                                                        <th>Roll No.</th>
                                                                        <th>Student Name</th>
                                                                        <th>Admission No.</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>Class</th>

                                                                        <th>Payment Mode</th>
                                                                        <th>Transaction Id</th>
                                                                        <th>Month</th>
                                                                        <th>Action</th>
                                                                        <th>Paid Amt.</th>
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
                                                                                    <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
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
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_razorpay_payment_id" runat="server" Text='<%#Bind("razorpay_payment_id")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_monthname" runat="server" Text='<%#Bind("monthname")%>'></asp:Label>
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
                                                                                </td>
                                                                                   <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Amount1" runat="server" Text='<%#Getamount_comma_seperated(Eval("Paid_amt").ToString())%>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                                <tfoot>
                                                                    <tr>
                                                                        <th colspan="13" style="text-align: right;">Total Payble Amount :
                                                            <asp:Label ID="lbl_fnl_payble" runat="server" Text="Label"></asp:Label></th>
                                                                    </tr>
                                                                </tfoot>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </div>



                                                <%-- =============== --%>
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
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->




    <asp:HiddenField ID="hd_sessions" runat="server" />
    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_from_date" runat="server" />
    <asp:HiddenField ID="hd_to_date" runat="server" />


</asp:Content>
