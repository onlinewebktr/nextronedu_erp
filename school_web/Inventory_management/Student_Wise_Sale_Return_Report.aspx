<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Student_Wise_Sale_Return_Report.aspx.cs" Inherits="school_web.Inventory_management.Student_Wise_Sale_Return_Report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Sales Return Report
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
                        <li class="breadcrumb-item active" aria-current="page">Student Wise Sales Return Reports</li>
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
                                    <asp:Button ID="Btn_Find" runat="server" Text="Find" OnClick="Btn_Find_Click" CssClass="btn btn-primary" ValidationGroup="a" Style="margin: 0px 0px 0px 0px; padding: 4px 10px 4px 12px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-6" style="display:none">
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded serchbox">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Select Session  </label>

                                    <asp:DropDownList ID="ddl_seesion" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>




                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Select Class  </label>

                                    <asp:DropDownList ID="ddl_classname" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_classname_SelectedIndexChanged"></asp:DropDownList>




                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Select Section</label>
                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>



                                </div>

                                <div class="col-md-2" style="padding: 26px 0px 0px 0px;">
                                    <asp:Button ID="btn_sale_id" runat="server" Text="Find" OnClick="btn_sale_id_Click" Style="margin: 0px 0px 0px 0px; padding: 4px 10px 4px 12px;"
                                        CssClass="btn btn-primary" ValidationGroup="a" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-xl-12" id="tblPrintIQ">
                <h6 class="mb-0 text-uppercase" style="font-size: 15px; margin: 10px 0px 10px 0px;">Student Wise Sales Return  Reports:-<asp:Label ID="lbl_heading" runat="server"></asp:Label>


                    <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right"><i class="bx bx-export"> </i></asp:LinkButton>
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel('tblPrintIQ')" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right"><i class="bx bx-printer" aria-hidden="true"></i></asp:LinkButton>

                </h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <%--  <div class="table-responsive">--%>
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
                                                    <th>Admission No.</th>
                                                    <th>Class</th>
                                                    <th>Section</th>
                                                    <th>Roll No.</th>
                                                    <th>Invoice No.</th>
                                                    <th>Date</th>
                                                    <th>Total Items</th>
                                                    <th>User Id</th>
                                                    <th>Return Mode</th>
                                                    <th>Return Transaction Id</th>
                                                    <th>Return Amount </th>

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
                                                                            <a class="dropdown-item" href="Slip/Print_Return_Sale_slip.aspx?unique_entry_id=<%#Eval("unique_entry_id") %>&partyid=<%#Eval("party_id") %>" target="_blank"><i class='bx bx-printer'></i><span>Print slip</span></a>

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
                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                            </td>
                                                            <td>

                                                                <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("New_Bill")%>'></asp:Label>


                                                                <asp:Label ID="lbl_unique_entry_id" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("Date1")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_total_items" runat="server" Text='<%#Bind("Total_items")%>'></asp:Label>
                                                            </td>
                                                            




                                                            <td>
                                                                <asp:Label ID="lbl_user_id" runat="server" Text='<%#Bind("user_id")%>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_Payment_Mode" runat="server" Text='<%#Bind("Pay_Mode")%>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_transction_id" runat="server" Text='<%#Bind("Pay_Tran_id")%>'></asp:Label>
                                                            </td>









                                                            <td style="text-align: right;">
                                                                <asp:Label ID="lbl_Total_Paid_Amount" runat="server" Text='<%#Bind("Pay_Amount")%>'></asp:Label>
                                                            </td>



                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            <tfoot>
                                                <tr>

                                                    <th colspan="17" style="text-align: right;">
                                                        <asp:Label ID="lbl_fnl_paid" runat="server" Text="Label"></asp:Label></th>

                                                </tr>

                                            </tfoot>
                                        </table>


                                        <div class="paid-cat-div">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <p class="paid-cat-div-p">
                                                        <i>Cash </i>:
                                                            <asp:Label ID="lbl_by_cash" runat="server" Text="00"></asp:Label>
                                                    </p>


                                                </div>
                                                <div class="col-sm-3">
                                                    <p class="paid-cat-div-p">
                                                        <i>Netbanking </i>:
                                                            <asp:Label ID="lbl_by_netbanking" runat="server" Text="00"></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-sm-3">
                                                    <p class="paid-cat-div-p">
                                                        <i>UPI</i> :
                                                            <asp:Label ID="lbl_upi" runat="server" Text="00"></asp:Label>
                                                    </p>

                                                </div>
                                                <div class="col-sm-3">
                                                    <p class="paid-cat-div-p">
                                                        <i>Wallet </i>:
                                                            <asp:Label ID="lbl_Wallet" runat="server" Text="00"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>






                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--</div>--%>
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
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
