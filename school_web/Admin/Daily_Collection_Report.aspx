<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Daily_Collection_Report.aspx.cs" Inherits="school_web.Admin.Daily_Collection_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Daily Collection Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .reports {
            background-color: #d92550;
        }

        .Daily_Cash_Report {
            color: red !important;
        }

        .popup1, .popup2, .popup3, .popup4 {
            max-height: calc(100vh - 100px);
        }

        .maste-p {
            width: 88%;
            float: left;
            padding: 0px 0px 0px 8px;
            margin: 0px;
            font-size: 12px;
            line-height: 20px;
        }

        .table th, .table td {
            padding: 2px !important;
            vertical-align: top !important;
            border-top: 0px solid #e9ecef !important;
            text-align: left;
        }

        .chck-box {
            float: left;
        }

        .all-check {
            padding: 0px 0px 0px 10px;
            width: 50% !important;
            float: left;
        }

        .form-group {
            margin-bottom: 0rem;
        }

        .header_image {
            display: none;
        }

        @media print {
            .noPrint {
                display: none;
            }

            .header_image {
                display: block !important;
            }

            #Header, #Footer {
                display: none !important;
            }
        }

        th {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #fff !important;
            color: #000 !important;
            border-bottom: 1px solid #000 !important;
            border-left: 0px solid #000 !important;
            border-right: 0px solid #000 !important;
            border-top: 0px solid #000 !important;
        }

        tfoot td {
            background: #fff !important;
            color: #000 !important;
        }

        table.table-bordered.dataTable tbody th, table.table-bordered.dataTable tbody td {
            border-bottom-width: 0;
            border: 0px solid #000 !important;
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
                <div class="breadcrumb-title pe-3"><a href="#" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Account</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Daily Collection Report</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <hr />

                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row  g-3 needs-validation">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                                  <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"  ></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                                <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                                <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div> 
                                                    <div class="col-sm-6">
                                                        <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find" />
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr" id="tblCustomers">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="prnt-dv-wpr">
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
                                                                        <span style="font-size: 14px; font-weight: bold;">Balance Sheet/Daily Collection-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>

                                                            <asp:Panel ID="Panel1" runat="server" Style="width: 100%;">
                                                                <asp:Panel ID="pnl_grid_collection" runat="server" Style="width: 100%;" Visible="false">
                                                                    <div style="margin: 0px; padding: 0px; float: left; height: 30px; width: 100%">
                                                                        <strong>Student Fee Collection</strong>
                                                                    </div>
                                                                    <table id="example21" data-page-length='1500' class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>User</th>
                                                                                <th>Date</th>
                                                                                <th>Admission No.</th>
                                                                                <th>Student Name</th>
                                                                                <th>Slip No.</th>
                                                                                <th>MOP</th>
                                                                                <th>Amount</th>
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
                                                                                            <asp:Label ID="lbl_username" runat="server" Text='<%#Bind("username")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Addmission_no")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Slip_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_bankname" runat="server" Text='<%#Bind("Bank_name")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <td colspan="6"></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;"><b>Sub Total:-</b></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;">
                                                                                        <asp:Label ID="lbl_tot" runat="server" Font-Bold="true"></asp:Label></td>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>

                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="8" style="text-align: left;">
                                                                                    <asp:DataList ID="data_view" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Style="border-collapse: collapse; font-size: 12px!important; color: #c10900; font-weight: bold; width: auto; float: right; border: 1px solid #f00;">
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lbl_Payment_Type" runat="server" Text='<%#Bind("mode") %>'></asp:Label>:-
                                                                       
                                                                                <asp:Label ID="lbl_Received_amt" runat="server" Text='<%#Bind("totalamount") %>'></asp:Label>/-
                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                            </tr> 
                                                                        </tfoot> 
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnl_grid_FormSale_Collection" runat="server" Style="width: 100%;" Visible="false">
                                                                    <div style="margin: 0px; padding: 0px; float: left; height: 30px; width: 100%">
                                                                        <strong>Student Form Sale Collection</strong>
                                                                    </div>
                                                                    <table id="example211" data-page-length='1500' class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>User</th>
                                                                                <th>Date</th>
                                                                                <th>Form No.</th>
                                                                                <th>Student Name</th>
                                                                                <th>Slip No.</th>
                                                                                <th>MOP</th>
                                                                                <th>Amount</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="Repeater1_form_sale" runat="server" OnItemDataBound="Repeater1_form_sale_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_username" runat="server" Text='<%#Bind("username")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Slip_no" runat="server" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_bankname" runat="server" Text='<%#Bind("Bank_name")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <td colspan="6"></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;"><b>Sub Total:-</b></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;">
                                                                                        <asp:Label ID="lbl_tot" runat="server" Font-Bold="true"></asp:Label></td>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="8" style="text-align: left;">
                                                                                    <asp:DataList ID="datalist_form_sale_group" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Style="border-collapse: collapse; font-size: 12px!important; color: #c10900; font-weight: bold; width: auto; float: right; border: 1px solid #f00;">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_Type" runat="server" Text='<%#Bind("Payment_mode") %>'></asp:Label>:-
                                                                       
                                                                                <asp:Label ID="lbl_Received_amt" runat="server" Text='<%#Bind("totalamount") %>'></asp:Label>/-
                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                            </tr> 
                                                                        </tfoot>

                                                                    </table>

                                                                </asp:Panel>


                                                                <asp:Panel ID="pnl_grid_special_fee" runat="server" Style="width: 100%;" Visible="false">
                                                                    <div style="margin: 0px; padding: 0px; float: left; height: 30px; width: 100%">
                                                                        <strong>Student Special Fee Collection</strong>
                                                                    </div>
                                                                    <table id="example2112" data-page-length='1500' class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>User</th>
                                                                                <th>Date</th>
                                                                                <th>Adm. No.</th>
                                                                                <th>Student Name</th>
                                                                                <th>Slip No.</th>
                                                                                <th>MOP</th>
                                                                                <th>Amount</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="rp_special_Fee" runat="server" OnItemDataBound="rp_special_Fee_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                         <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_username" runat="server" Text='<%#Bind("username")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Slip_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_mode" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_bankname" runat="server" Text='<%#Bind("Payment_bank")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <td colspan="6"></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;"><b>Sub Total:-</b></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;">
                                                                                        <asp:Label ID="lbl_tot" runat="server" Font-Bold="true"></asp:Label></td>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>

                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="8" style="text-align: left;">
                                                                                    <asp:DataList ID="datalist_special_fee_group" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Style="border-collapse: collapse; font-size: 12px!important; color: #c10900; font-weight: bold; width: auto; float: right; border: 1px solid #f00;">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_Type" runat="server" Text='<%#Bind("Payment_mode") %>'></asp:Label>:-
                                                                       
                                                                                <asp:Label ID="lbl_Received_amt" runat="server" Text='<%#Bind("totalamount") %>'></asp:Label>/-
                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                            </tr>
                                                                        </tfoot>
                                                                    </table>
                                                                </asp:Panel>



                                                                <asp:Panel ID="pnl_grid_other_Collection" runat="server" Style="width: 100%;" Visible="false">
                                                                    <div style="margin: 0px; padding: 0px; float: left; height: 30px; width: 100%">
                                                                        <strong>Student Other Fee Collection</strong>
                                                                    </div>

                                                                    <table id="example2111" data-page-length='1500' class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>User</th>
                                                                                <th>Date</th>
                                                                                <th>Admission No.</th>
                                                                                <th>Student Name</th>
                                                                                <th>Slip No.</th>
                                                                                <th>MOP</th>
                                                                                <th>Amount</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="Repeater_other_fee_collection" runat="server" OnItemDataBound="Repeater_other_fee_collection_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_username" runat="server" Text='<%#Bind("username")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Payment_date")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Slip_no" runat="server" Text='<%#Bind("Slipid")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_bankname" runat="server" Text='<%#Bind("Bank_name")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Content_Fee","{0:n}") %>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <td colspan="6"></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;"><b>Sub Total:-</b></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;">
                                                                                        <asp:Label ID="lbl_tot" runat="server" Font-Bold="true"></asp:Label></td>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>

                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="8" style="text-align: left;">
                                                                                    <asp:DataList ID="data_list_other_fee_group" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Style="border-collapse: collapse; font-size: 12px!important; color: #c10900; font-weight: bold; width: auto; float: right; border: 1px solid #f00;">
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lbl_Payment_Type" runat="server" Text='<%#Bind("Payment_mode") %>'></asp:Label>:-
                                                                       
                                                                                <asp:Label ID="lbl_Received_amt" runat="server" Text='<%#Bind("totalamount") %>'></asp:Label>/-
                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                            </tr>

                                                                        </tfoot>

                                                                    </table>

                                                                </asp:Panel>

                                                                <asp:Panel ID="pnl_grid_Payment_Voucher" runat="server" Style="width: 100%;" Visible="false">
                                                                    <div style="margin: 0px; padding: 0px; float: left; height: 30px; width: 100%">
                                                                        <strong>Payment Voucher</strong>
                                                                    </div>

                                                                    <table id="example21111" data-page-length='1500' class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>User</th>
                                                                                <th>Date</th>
                                                                                <th>Ledger</th>
                                                                                <th>Description</th>
                                                                                <th>Slip No.</th>
                                                                                <th>MOP</th>
                                                                                <th>Amount</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="grid_payment_Voucher" runat="server" OnItemDataBound="grid_payment_Voucher_ItemDataBound1">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_username" runat="server" Text='<%#Bind("Created_by")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date_one")%>'></asp:Label>
                                                                                        </td>
                                                                                          <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Ledger" runat="server" Text='<%#Bind("accountledger")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Slip_no" runat="server" Text='<%#Bind("VoucherNo")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_mode" runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Debit","{0:n}") %>'></asp:Label>
                                                                                            <asp:Label ID="lbl_unique_entry_id" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <td colspan="6"></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;"><b>Sub Total:-</b></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;">
                                                                                        <asp:Label ID="lbl_tot" runat="server" Font-Bold="true"></asp:Label></td>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>

                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="8" style="text-align: left;">
                                                                                    <asp:DataList ID="data_list_payment" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Style="border-collapse: collapse; font-size: 12px!important; color: #c10900; font-weight: bold; width: auto; float: right; border: 1px solid #f00;">
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lbl_Payment_Type" runat="server" Text='<%#Bind("accountname") %>'></asp:Label>:-
                                                                       
                                                                                <asp:Label ID="lbl_Received_amt" runat="server" Text='<%#Bind("Debit") %>'></asp:Label>/-
                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                            </tr>

                                                                        </tfoot>

                                                                    </table>

                                                                </asp:Panel>

                                                                <asp:Panel ID="pnl_grid_Receipt_Voucher" runat="server" Style="width: 100%;" Visible="false">
                                                                    <div style="margin: 0px; padding: 0px; float: left; height: 30px; width: 100%">
                                                                        <strong>Receipt Voucher</strong>
                                                                    </div>

                                                                    <table id="example211111" data-page-length='1500' class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>User</th>
                                                                                <th>Date</th>
                                                                                <th>Ledger</th>
                                                                                <th>Description</th>
                                                                                <th>Slip No.</th>
                                                                                <th>MOP</th>
                                                                                <th>Amount</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="grid_Receipt_Voucher" runat="server" OnItemDataBound="grid_Receipt_Voucher_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_username" runat="server" Text='<%#Bind("Created_by")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date_one")%>'></asp:Label>
                                                                                        </td>

                                                                                           <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Ledger" runat="server" Text='<%#Bind("accountledger")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Slip_no" runat="server" Text='<%#Bind("VoucherNo")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_mode" runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Credit","{0:n}") %>'></asp:Label>
                                                                                            <asp:Label ID="lbl_unique_entry_id" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <td colspan="6"></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;"><b>Sub Total:-</b></td>
                                                                                    <td style="border-top: 2px solid #000!important; border-bottom: 2px solid #000!important; font-size: 12px;">
                                                                                        <asp:Label ID="lbl_tot" runat="server" Font-Bold="true"></asp:Label></td>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>

                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="8" style="text-align: left;">
                                                                                    <asp:DataList ID="data_list_Receipt" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Style="border-collapse: collapse; font-size: 12px!important; color: #c10900; font-weight: bold; width: auto; float: right; border: 1px solid #f00;">
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lbl_Payment_Type" runat="server" Text='<%#Bind("accountname") %>'></asp:Label>:-
                                                                       
                                                                                <asp:Label ID="lbl_Received_amt" runat="server" Text='<%#Bind("Debit") %>'></asp:Label>/-
                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                            </tr>

                                                                        </tfoot>

                                                                    </table>
                                                                </asp:Panel>

                                                                <table class=" table" style="border: 1px solid #ffa2a2; float: none; width: auto; font-size: 12px; margin: 10px auto; background-color: rgb(147 244 180 / 19%);">
                                                                    <tr>
                                                                        <td style="width: 190px; padding: 8px 0px 0px 8px !important;"><b>Received in Cash</b> <b style="float: right">:</b></td>
                                                                        <td style="width: 120px; padding: 8px 0px 0px 8px !important;">
                                                                            <asp:Label ID="lbl_received_in_cash" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>
                                                                        <td style="width: 160px; padding: 8px 0px 0px 8px !important;"><b>Receipt in Cash</b> <b style="float: right">:</b></td>
                                                                        <td style="width: 120px; padding: 8px 0px 0px 8px !important;">
                                                                            <asp:Label ID="lbl_Receipt_in_cash" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>

                                                                        <td style="padding: 8px 0px 0px 8px !important;"><b>Expense in Cash </b><b style="float: right">:</b></td>
                                                                        <td style="width: 120px; padding: 8px 0px 0px 8px !important;">
                                                                            <asp:Label ID="lbl_expense_in_cash" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>
                                                                        <td rowspan="3" style="font-size: 15px; text-align: center;">
                                                                            <b>Cash In Counter</b>
                                                                            <br />
                                                                            <asp:Label ID="lbl_total_cash_in_counter" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 8px 0px 0px 8px !important;"><b>Received at Bank</b> <b style="float: right">:</b></td>
                                                                        <td style="padding: 8px 0px 0px 8px !important;">
                                                                            <asp:Label ID="lbl_received_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>

                                                                        <td style="padding: 8px 0px 0px 8px !important;"><b>Receipt Voucher in Bank</b> <b style="float: right">:</b></td>
                                                                        <td style="padding: 8px 0px 0px 8px !important;">
                                                                            <asp:Label ID="lbl_Receipt_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>
                                                                        <td style="padding: 8px 0px 0px 8px !important;"><b>Expense in Bank</b> <b style="float: right">:</b></td>
                                                                        <td style="padding: 8px 0px 0px 8px !important;">
                                                                            <asp:Label ID="lbl_expense_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 8px 0px 8px 8px !important;"><b>Total Received Amount</b> <b style="float: right">:</b></td>
                                                                        <td style="padding: 8px 0px 8px 8px !important;">
                                                                            <asp:Label ID="lbl_totalreceived" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>
                                                                        <td style="padding: 8px 0px 8px 8px !important;"><b>Total Receipt Voucher Amount</b> <b style="float: right">:</b></td>
                                                                        <td style="padding: 8px 0px 8px 8px !important;">
                                                                            <asp:Label ID="lbl_totalrefund" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>
                                                                        <td style="padding: 8px 0px 8px 8px !important;"><b>Total Expense Amount</b> <b style="float: right">:</b></td>
                                                                        <td style="padding: 8px 0px 8px 8px !important;">
                                                                            <asp:Label ID="lbl_totalexpense" runat="server" Text="0" Font-Bold="true"></asp:Label>/-
                                                </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="7" style="padding: 8px 8px 8px 8px !important; border-top: 1px solid #ffa2a2 !important;">
                                                                            <div style="margin: 0px 10px 0px 0px; padding: 0px; width: 48%; float: left">


                                                                                <p style="margin: 0px; padding: 0px; width: 100%; float: left; font-weight: bold; font-size: 15px; color: #ff1818;">
                                                                                    Cash Transaction Summary
                                                       
                                                                                </p>
                                                                                <table style="min-width: 350px; float: none; margin: 0px auto; width: 100%; vertical-align: middle!important; text-align: center; font-size: 15px; padding: 5px!important; background-color: #f1f1f1; border: 1px solid #ccc;">

                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Opening Balance</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_opening_balance" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Cash Receive</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_cash_receive" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Cash (Other) Receive</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_cash_other_receive" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Receipt Voucher (in Cash)</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_Receipt_in_cash" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Gross Cash In Hand</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_gross_cash_in_hand" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Expense (in Cash)</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_expense_in_cash" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    

                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Closing Balance(in Cash)</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_closing_balance_in_cash" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <div style="margin: 0px 10px 0px 0px; padding: 0px; width: 48%; float: left">
                                                                                <p style="margin: 0px; padding: 0px; width: 100%; float: left; font-weight: bold; font-size: 15px; color: #ff1818;">
                                                                                    Bank Transaction Summary
                                                       
                                                                                </p>
                                                                                <table style="min-width: 350px; float: none; margin: 0px auto; width: 100%; vertical-align: middle!important; text-align: center; font-size: 15px; padding: 5px!important; background-color: #f1f1f1; border: 1px solid #ccc;">

                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Opening Balance</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_opening_balance_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Bank Receive</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_received_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Bank (Other) Receive</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_bank_other_receive" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Receipt Voucher (in Bank)</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_Receipt_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Gross Amount (in Bank)</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_gross_amount_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Total Expense (in Bank)</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_total_expense_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                    

                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <b style="float: left">Closing Balance(in Bank)</b> <b style="float: left">:</b>
                                                                                        </td>
                                                                                        <td style="text-align: right">
                                                                                            <asp:Label ID="lbl_closing_balance_in_bank" runat="server" Text="0" Font-Bold="true"></asp:Label>/-</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6">
                                                                            <asp:Label ID="lbl_print_date" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table> 
                                                            </asp:Panel>
                                                            <div style="margin: 62px 0px 0px 0px; padding: 0px; float: left; width: 100%; text-align: right">
                                                                <span>Principal Sign.</span>
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
        </div>
    </div>
</asp:Content>
