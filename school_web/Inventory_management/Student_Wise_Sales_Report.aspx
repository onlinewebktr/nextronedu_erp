<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Student_Wise_Sales_Report.aspx.cs" Inherits="school_web.Inventory_management.Student_Wise_Sales_Report" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Wise Sales Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .paid-cat-div {
            color: black;
        }

        .select2-container--default .select2-selection--single {
            background-color: #fff;
            border: 0px solid #aaa;
            border-radius: 4px;
            background-color: #fff;
            border: 0px solid #aaa;
            border-radius: 0px;
        }

        .select2-container .select2-selection--single {
            box-sizing: border-box;
            cursor: pointer;
            display: block;
            height: 25px;
            user-select: none;
            -webkit-user-select: none;
        }

        .select2-selection__rendered {
            display: block;
            width: 100%;
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #000;
            line-height: 25px;
            font-size: 16px !important;
            font-weight: normal;
        }

        label {
            display: inline-block;
            font-weight: bold;
        }

        @media print {
            .noPrint {
                display: none;
            }

            #Header, #Footer {
                display: none !important;
            }
        }
    </style>
    <style>
        .conf-alrt-inr {
            position: relative;
            top:25%;
            margin: 0px auto;
            border-radius: 2px;
            padding: 20px;
            width: 1000px;
            height: auto;
            background: #fff;
            -webkit-transition: -webkit-transform .3s ease-out;
            -o-transition: -o-transform .3s ease-out;
            transition: transform .3s ease-out;
            -webkit-transform: translate(0,-25%);
            -ms-transform: translate(0,-25%);
            -o-transform: translate(0,-25%);
            transform: translate(0,-25%);
            -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
            box-shadow: 0 5px 15px rgba(0,0,0,.5);
        }

        .modal {
            background: rgb(0 0 0 / 48%);
        }

        .popupTablWpR {
            width: 100%;
            margin: 0px;
            padding: 0px;
        }

        .popup-dt-h {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            font-size: 20px;
            color: #333;
            letter-spacing: .5px;
        }

        .conf-btn-ul {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            text-align: right;
        }

        table tr th {
            padding: 7px 5px !important;
            font-size: 12px;
            font-weight: 700;
        }

        table tr td {
            padding: 7px 5px !important;
            font-size: 12px;
            border: 1px solid #adadad;
        }

        tfoot, th, thead {
            background: #FFBA5F !important;
        }

        .table {
            border-color: #adadad;
        }

        .form-label-fnds {
            font-size: 12px;
        }

        .find-dv-txtbx {
            padding: 2px 6px;
            font-size: 12px;
        }

        label {
            font-size: 14px;
        }

        .txtalignrights {
            text-align: right;
        }

        .month-checkbox {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .month-checkbox label {
                margin: 0px 3px 0px 0px;
            }

        .fnd-box-row-wpr-h {
            background: #f5f3f3;
        }

        .card-body {
            padding: 0;
        }

        .card {
            background-color: rgb(255 255 255 / 0%);
            box-shadow: 0 2px 6px 0 rgb(218 218 253 / 0%), 0 2px 6px 0 rgb(206 206 238 / 0%);
        }

        .stdinfo-lft {
            width: 87%;
        }

        .stdinfo-rght {
            width: 13%;
        }

        .stdinfo-rght-imgwpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            height: 131px;
            overflow: hidden;
            border: 2px dashed #FFBA5F;
            border-radius: 2px;
        }

            .stdinfo-rght-imgwpr img {
                width: 100%;
            }

        .popup-adm-fees-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .popup-adm-std-wpr {
            margin: 0px;
            padding: 5px 7px 5px 7px;
            width: 100%;
            float: left;
            border-bottom: 1px solid #ddd;
        }

        .popup-adm-std-info {
            margin: 0px;
            padding: 0px;
            width: 85%;
            float: right;
        }

        .popup-adm-std-imgs {
            margin: 0px;
            padding: 0px;
            width: 13%;
            float: left;
            height: 131px;
            overflow: hidden;
            border: 2px dashed #FFBA5F;
            border-radius: 2px;
        }

            .popup-adm-std-imgs img {
                width: 100%;
            }

        .adm-box-wprs1 {
            margin: 0px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

            .adm-box-wprs1 table {
                margin: 0px;
            }

        .adm-box-wprs2 {
            margin: 0px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

        .adm-box-wprs3 {
            margin: 0px 0px 0px -1px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-left: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

        .lblfnts {
            font-size: 12.5px;
        }

        /* CSS */
        .button-37 {
            margin: 2px 0px 0px 0px;
            float: left;
            font-weight: 600;
            padding: 3px 25px 5px;
            background-color: #13aa52;
            border: 1px solid #13aa52;
            border-radius: 4px;
            box-shadow: rgba(0, 0, 0, .1) 0 2px 4px 0;
            box-sizing: border-box;
            color: #fff;
            cursor: pointer;
            font-size: 14px;
            text-align: center;
            transform: translateY(0);
            transition: transform 150ms, box-shadow 150ms;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

            .button-37:hover {
                color: #fff;
                box-shadow: rgba(0, 0, 0, .15) 0 3px 9px 0;
                transform: translateY(-2px);
            }

        .noclick {
            pointer-events: none;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel(secid) {
            var panel = document.getElementById(secid);
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(' <style  type="text/css" media="print">  td,th {border: 1px solid #000; padding: 0px 0px 0px 5px!important;}.paid-cat-div{color: black;}</style ><link href="Slip/print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Dosis:200,300,400,500,600,700,800&display=swap" rel="stylesheet"/><link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <div class="breadcrumb-title pe-3">Sale Entry</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Student Wise Sales Reports</li>
                    </ol>
                </nav>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-6">
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded serchbox">
                            <div class="row g-3 needs-validation" novalidate="">


                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">From Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_from_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_from_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">To Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_to_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_to_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>

                                <div class="col-md-2" style="padding: 26px 0px 0px 0px;">
                                    <asp:Button ID="Btn_Find" runat="server" Text="Find" OnClick="Btn_Find_Click" CssClass="btn btn-primary" ValidationGroup="a" Style="margin: 5px 0px 0px 0px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-6">
                <hr />
                <div class="card" style="display:none;">
                    <div class="card-body">
                        <div class="p-4 border rounded serchbox">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">Select Class  </label>

                                    <asp:DropDownList ID="ddl_classname" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_classname_SelectedIndexChanged"></asp:DropDownList>




                                </div>
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">Select Section</label>
                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>



                                </div>

                                <div class="col-md-2" style="padding: 26px 0px 0px 0px;">
                                    <asp:Button ID="btn_sale_id" runat="server" Text="Find" OnClick="btn_sale_id_Click" Style="margin: 5px 0px 0px 0px;" CssClass="btn btn-primary" ValidationGroup="a" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-xl-12" id="tblPrintIQ">
                <h6 class="mb-0 text-uppercase" style="font-size: 15px; margin: 10px 0px 10px 0px;">Student Wise Sales Reports:-<asp:Label ID="lbl_heading" runat="server"></asp:Label>


                    <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right"><i class="bx bx-export"> </i></asp:LinkButton>
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel('tblPrintIQ')" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right"><i class="bx bx-printer" aria-hidden="true"></i></asp:LinkButton>

                </h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-12">
                                            <table style="width: 100%;" id="example" class=" table table-hover table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Print</th>
                                                        <th>Name</th>
                                                        <th>Adm. No.</th>
                                                        <th>Class</th>
                                                        <th>Roll No</th>
                                                        <th>Invoice No.</th>
                                                        <th>Date</th>
                                                        <th>Total Items</th>
                                                        <th style="display: none">Total Qty.</th>
                                                        <th>Package Name</th>


                                                        <th>User Id</th>
                                                        <th>Pay Mode</th>
                                                        <th>Pay Transaction Id</th>
                                                        <th>Total Payable </th>
                                                        <th>Total Cash </th>
                                                        <th>Total Bank </th>
                                                        <th>Total Paid</th>
                                                        <th>Total Dues</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                        <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                            href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                            <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                <i class="bx bx-grid-horizontal"></i>
                                                                            </div>
                                                                        </a>
                                                                        <ul class="dropdown-menu dropdown-menu-end">
                                                                            <li>
                                                                                <a class="dropdown-item" href="Slip/Print_Sale_slip.aspx?unique_entry_id=<%#Eval("unique_no") %>&partyid=<%#Eval("party_id") %>" target="_blank"><i class='bx bx-printer'></i><span>Print slip</span></a>

                                                                            </li>

                                                                            <li>
                                                                                <asp:LinkButton ID="lnk_update_date" class="dropdown-item" runat="server" OnClick="lnk_update_date_Click"><i class='bx bx-memory-card'></i>Update Date & Payment Mode</asp:LinkButton>
                                                                                <asp:Label ID="lbl_Payment_TransactionId" runat="server" Text='<%#Bind("Payment_TransactionId")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Date1" runat="server" Text='<%#Bind("Date1")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_partyid" runat="server" Text='<%#Bind("party_id")%>' Visible="false"></asp:Label>

                                                                            </li>

                                                                        </ul>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_CustomerName" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>




                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_party_id1" runat="server" Text='<%#Bind("party_id")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_classname" runat="server" Text='<%#Bind("classname")%>'></asp:Label>
                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("Bill_No")%>'></asp:Label>


                                                                    <asp:Label ID="lbl_unique_entry_id" runat="server" Text='<%#Bind("unique_no")%>' Visible="false"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_total_items" runat="server" Text='<%#Bind("Total_Item")%>'></asp:Label>
                                                                </td>
                                                                <td style="display: none">
                                                                    <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Total_Quantity")%>'></asp:Label>
                                                                </td>


                                                                <td>
                                                                    <asp:Label ID="lbl_package_name" runat="server" Text='<%#Bind("Package_Name")%>'></asp:Label>
                                                                </td>


                                                                <td>
                                                                    <asp:Label ID="lbl_user_id" runat="server" Text='<%#Bind("user_id")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_Payment_Mode" runat="server" Text='<%#Bind("Bank_Payment_Mode")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_transction_id" runat="server" Text='<%#Bind("Payment_transaction")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_NetPayable" runat="server" Text='<%#Bind("NetPayable")%>'></asp:Label>
                                                                </td>


                                                                <td>
                                                                    <asp:Label ID="lbl_cash_amount" runat="server" Text='<%#Bind("Received_from_Cash")%>'></asp:Label>
                                                                </td>


                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("Received_from_Bank")%>'></asp:Label>
                                                                </td>


                                                                <td>
                                                                    <asp:Label ID="lbl_Total_Paid_Amount" runat="server" Text='<%#Bind("Total_Paid_Amount")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_Duse_Amount" runat="server" Text='<%#Bind("Duse_Amount")%>'></asp:Label>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <tr>

                                                        <th colspan="17" style="text-align: right;">
                                                            <asp:Label ID="lbl_fnl_paid" runat="server" Text="Label"></asp:Label></th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_total_dues" runat="server" Text="Label"></asp:Label></th>
                                                    </tr>

                                                </tfoot>
                                            </table>


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
                                                            <i>POS </i>:
                                                            <asp:Label ID="lbl_pos" runat="server" Text="00"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="col-sm-4">

                                                        <p class="paid-cat-div-p">
                                                            <i>Online </i>:
                                                            <asp:Label ID="lbl_online" runat="server" Text="00"></asp:Label>
                                                        </p>
                                                        <p class="paid-cat-div-p">
                                                            <i>Student Wallet </i>:
                                                            <asp:Label ID="lbl_Wallet" runat="server" Text="00"></asp:Label>
                                                        </p>
                                                    </div>

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

    <style>
        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }
    </style>
    <script type="text/javascript">
        function openModalCause() {
            $('#mdl_confirm').modal('show');
        }
        function open_item_pay() {

            $('#myModal3').modal('show');

        }
    </script>

    <div id="mdl_confirm" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Update Payment Date and Payment Mode</h5>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row">
                            <div class="col-md-12">

                                <label for="validationCustom01" class="form-label">Invoice No.<sup> </sup></label>
                                <asp:Label ID="lblinvoice_no" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>

                                <label for="validationCustom01" class="form-label">Payment Mode<sup> </sup></label>
                                <asp:Label ID="lblpaymentmode" Font-Bold="true" runat="server" ForeColor="Red"></asp:Label>
                                <br />
                                <label for="validationCustom01" class="form-label">Payment Date<sup> </sup></label>
                                <asp:Label ID="lbl_Paymentdate" Font-Bold="true" runat="server" ForeColor="Red"></asp:Label>

                                <br />

                                <label for="validationCustom01" class="form-label">Payment Mode<sup>*</sup></label>

                                <asp:DropDownList ID="ddl_paymentmode" runat="server" OnSelectedIndexChanged="ddl_paymentmode_SelectedIndexChanged" AutoPostBack="true" class="form-select find-dv-txtbx">

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
                                    <asp:ListItem>Wallet</asp:ListItem>
                                </asp:DropDownList>

                                <br />
                                <label for="validationCustom01" class="form-label" runat="server" id="lavel_tran" visible="false">Transaction No.</label>
                                <asp:TextBox ID="txt_tran_id" Visible="false" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>

                                <br />
                                <label for="validationCustom01" class="form-label">New Date<sup>*</sup></label>
                                <asp:TextBox ID="txt_newdate" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                <br />
                                <asp:Button ID="btn_conf_remove" Style="margin: 10px 10px 0px 0px;" OnClick="btn_conf_remove_Click" runat="server" Text="Submit" class="btn btn-danger find-dv-btn" OnClientClick="return confirm('Are you sure you want to update ?');" />

                                <a href="#!" data-dismiss="modal" style="margin: 10px 10px 0px 0px;" class="btn btn-primary find-dv-btn">Close</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--conf-alrt-sec --%>

    <script type="text/javascript"> 

        function ShowHideDiv(chkCash) {
            var cashdiv = document.getElementById("cashdiv");

            cashdiv.style.display = $(chkCash).prop('checked') ? "" : "none";
        }

        function ShowHideDivBank(chkBank) {
            var bankdiv = document.getElementById("bankdiv");
            var bankdiv1 = document.getElementById("bankdiv1");
            var pnl_mode_t_nS = document.getElementById("pnl_mode_t_nS");

            bankdiv.style.display = $(chkBank).prop('checked') ? "" : "none";
            bankdiv1.style.display = $(chkBank).prop('checked') ? "" : "none";
            pnl_mode_t_nS.style.display = $(chkBank).prop('checked') ? "" : "none";

        }



        $(document).ready(function () {
            var chkCash = $("#<%=chk_cash.ClientID %>");
             var chkBank = $("#<%=chk_bank.ClientID %>");

             ShowHideDiv(chkCash);
             ShowHideDivBank(chkBank);

             on_payment_mode_selection();
             $("#<%=ddl_paymentmode1.ClientID%>").on('change', function () {
                 on_payment_mode_selection();
             })
         });

        function on_payment_mode_selection() {
            if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Cash") {
                 $("#pnl_mode_t_nS").hide();


                 $("#bank_dt").hide();
             }

             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Wallet") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Wallet");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").hide();
             }


             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Netbanking") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Netbanking.");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");

                 $("#bank_dt").hide();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Deposited In Bank") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Deposited In Bank");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").show();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Sbdebit") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Sbdebit");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").hide();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Cheque") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Cheque");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").show();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "NEFT") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("NEFT");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("UTR No.");
                 $("#bank_dt").hide();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Debitcard") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Debitcard");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").hide();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Creditcard") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Creditcard");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").hide();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Otherdcard") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Otherdcard");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").hide();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "UPI") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("UPI");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").hide();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Demand Draft(DD)") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Demand Draft(DD");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                 $("#bank_dt").show();
             }
             if ($('#<%= ddl_paymentmode1.ClientID %> option:selected').val() == "Pos") {
                 $("#pnl_mode_t_nS").show();
                 $("#<%=lbl_payment_type.ClientID%>").text("Pos");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").hide();
            }


        }

        $(function () {
            $("#<%=txt_recived.ClientID %>").on('input', function () {
                    calculate();
                });
                $("#<%=txt_recived_from_bank.ClientID %>").on('input', function () {
                    calculate();
                });
                function calculate() {
                    var payble_amount = parseFloat($("#<%=txt_payble_amount.ClientID %>").val());
                    var recived_cash = parseFloat($("#<%=txt_recived.ClientID %>").val());
                    var txt_recived_from_bank = parseFloat($("#<%=txt_recived_from_bank.ClientID %>").val());




                    var ttl_recivedamt = (recived_cash + txt_recived_from_bank);
                    //alert(ttl_recivedamt);
                    if (payble_amount >= ttl_recivedamt) {
                        dues_amt = (payble_amount - ttl_recivedamt);
                        $("#<%=txt_total_paid.ClientID %>").val(ttl_recivedamt.toFixed(2));
                        $("#<%=txt_dues.ClientID %>").val(dues_amt.toFixed(2));
                    }
                    else if (isNaN(parseFloat(ttl_recivedamt))) //NaN
                    {
                        $("#<%=txt_recived.ClientID %>").val("0");
                        $("#<%=txt_total_paid.ClientID %>").val(0);
                        $("#<%=txt_dues.ClientID %>").val(payble_amount.toFixed(2));
                    }
                    else {
                        //alert(receive_from_cash);
                        var receive_from_cash = payble_amount - txt_recived_from_bank;
                        $("#<%=txt_recived.ClientID %>").val(receive_from_cash.toFixed(2));
                        $("#<%=txt_total_paid.ClientID %>").val(payble_amount.toFixed(2));
                        $("#<%=txt_dues.ClientID %>").val(0);
                    }




                    //======================




                }


            });

    </script>
    <div id="myModal3"  class="conf-alrt-sec modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="top: 45px!important;"><%--// data-backdrop="static" data-keyboard="false"--%>
        <div class="conf-alrt-inr" style="width: 750px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-8">
                        <h2 class="popup-dt-h">Update Payment Date and Payment Mode</h2>
                    </div>
                    <div class="col-md-4">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <a href="#!" data-dismiss="modal" style="margin: 0px 0px 10px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                                    class="btn btn-primary find-dv-btn">Close</a>
                            </li>
                        </ul>
                    </div>
                </div>

                <table style="width: 100%;" id="Table11" border="1" class="table table-hover table-bordered ">




                    <tr>
                        <td>Payable Amount</td>
                        <td>
                            <asp:TextBox ID="txt_payble_amount" runat="server" CssClass="form-control find-dv-txtbx noclick"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>Date</td>
                        <td>
                            <asp:TextBox ID="txt_paydate" runat="server" CssClass="form-control find-dv-txtbx datepicker"></asp:TextBox></td>
                    </tr>





                    <tr>
                        <td>Payment Mode</td>
                        <td>

                            <asp:CheckBox ID="chk_cash" runat="server" Text="Cash" Checked="true" onclick="ShowHideDiv(this)" />
                            <asp:CheckBox ID="chk_bank" runat="server" Text="Bank" onclick="ShowHideDivBank(this)" />
                        </td>
                    </tr>

                    <tr id="cashdiv">
                        <td>Recived from Cash</td>
                        <td>
                            <asp:TextBox ID="txt_recived" runat="server" CssClass="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)" Text="0.00"></asp:TextBox>

                        </td>
                    </tr>

                    <tr id="bankdiv1">
                        <td>Payment Type</td>
                        <td>
                            <asp:DropDownList ID="ddl_paymentmode1" runat="server" AutoPostBack="false" class="form-select find-dv-txtbx">
                                <asp:ListItem>Select</asp:ListItem>
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
                                <asp:ListItem>Wallet</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="bankdiv">
                        <td>Recived from
                                <asp:Label ID="lbl_payment_type" runat="server" Text="Pos"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txt_recived_from_bank" runat="server" CssClass="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)" Text="0.00"></asp:TextBox>

                        </td>
                    </tr>

                    <tr id="pnl_mode_t_nS">
                        <td>
                            <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label-fnds" Text="Transaction No."></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txt_trnaction_no" runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Total Paid</td>
                        <td>
                            <asp:TextBox ID="txt_total_paid" runat="server" CssClass="form-control find-dv-txtbx noclick"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Total Dues</td>
                        <td>
                            <asp:TextBox ID="txt_dues" runat="server" CssClass="form-control find-dv-txtbx noclick"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Remarks</td>
                        <td>
                            <asp:TextBox ID="txt_remarks_amt" runat="server" CssClass="form-control find-dv-txtbx" Style="height: 50px" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_pay_now" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btn_pay_now_Click" Style="width: 100%;" OnClientClick="return confirm('Are you sure you want pay?');" />
                        </td>
                    </tr>


                </table>
            </div>
        </div>
    </div>
   <%-- <div id="fadeup"></div>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">

    <%--<style>
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1050;
            display: block;
            width: 100%;
            height: 100%;
            overflow: hidden;
            outline: 0;
        }
    </style>--%>
    <%--<script type="text/javascript">
        function open_item_pay() {
            $("#myModal3").show();
            $('#myModal3').addClass('show');
            $('#myModal3').addClass('modal');
            //$('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal3").hide();
           
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>--%>
</asp:Content>
