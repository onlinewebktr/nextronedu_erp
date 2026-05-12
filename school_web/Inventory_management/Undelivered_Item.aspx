<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Undelivered_Item.aspx.cs" Inherits="school_web.Inventory_management.Undelivered_Item" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Undelivered Product
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
                        <li class="breadcrumb-item active" aria-current="page">Undelivered Product</li>
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
                <div class="card">
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
                <h6 class="mb-0 text-uppercase" style="font-size: 15px; margin: 10px 0px 10px 0px;">Undelivered Product Date :-<asp:Label ID="lbl_heading" runat="server"></asp:Label>


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
                                                    <th>Admission No.</th>
                                                    <th>Class</th>
                                                    <th>Roll No</th>
                                                    <th>Invoice No.</th>
                                                    <th>Date</th>
                                                    <th>Total Items</th>
                                                    <th style="display: none">Total Qty.</th>

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
                                                                            <asp:LinkButton ID="lnk_viewitem" class="dropdown-item" runat="server" OnClick="lnk_viewitem_Click"><i class='bx bx-memory-card'></i>View Item</asp:LinkButton>
                                                                            <asp:Label ID="lbl_unique_entry_id1" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>

                                                                            <asp:Label ID="lbl_Date1" runat="server" Text='<%#Bind("Date1")%>' Visible="false"></asp:Label>

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

                                                        <th colspan="16" style="text-align: right;">
                                                            <asp:Label ID="lbl_fnl_paid" runat="server" Text="Label"></asp:Label></th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_total_dues" runat="server" Text="Label"></asp:Label></th>
                                                    </tr>
                                            </tfoot>
                                        </table>


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

        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 864px !important;
                margin: 1.75rem auto;
            }
        }
    </style>
    <script type="text/javascript">
        function openModalCause() {
            $('#mdl_confirm').modal('show');
        }
    </script>

    <div id="mdl_confirm" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Item List</h5>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row">
                            <div class="col-md-12">


                                <asp:GridView ID="GrdView_item" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_item_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_itemname" runat="server" Text='<%#Bind("Description_Item")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Quantity" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_avlQuantity" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delivery Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Is_Stock_Delivered" runat="server" Text='<%#Bind("Is_Stock_Delivered")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>



                                                <asp:Label ID="lbl_Item_unique_entry_id" runat="server" Text='<%#Bind("Item_unique_entry_id")%>' Visible="false"></asp:Label>


                                                <asp:Label ID="lbl_stock_item_unique_entry_id" runat="server" Text='<%#Bind("stock_item_unique_entry_id")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Item_code" runat="server" Text='<%#Bind("Item_code")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_unit_id" runat="server" Text='<%#Bind("unit_id")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Rate" runat="server" Text='<%#Bind("Rate")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>





                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="hdrChkBox" runat="server" onClick="checkAllRows(this);" Text="All" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="rowChkBox" runat="server" onClick="checkUncheckHeaderCheckBox(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <script type="text/javascript">
                                    function checkAllRows(obj) {

                                        var objGridview = obj.parentNode.parentNode.parentNode;
                                        var list = objGridview.getElementsByTagName("input");

                                        for (var i = 0; i < list.length; i++) {
                                            var objRow = list[i].parentNode.parentNode;
                                            if (list[i].type == "checkbox" && obj != list[i]) {
                                                if (obj.checked) {

                                                    //If the header checkbox is checked then check all 
                                                    //checkboxes and highlight all rows.

                                                    objRow.style.backgroundColor = "#0baf36";
                                                    objRow.style.Color = "#fff";
                                                    list[i].checked = true;
                                                }
                                                else {
                                                    objRow.style.backgroundColor = "#FFFFFF";
                                                    list[i].checked = false;
                                                }
                                            }
                                        }
                                    }
                                    function checkUncheckHeaderCheckBox(obj) {
                                        var objRow = obj.parentNode.parentNode;

                                        if (obj.checked) {
                                            objRow.style.backgroundColor = "#0baf36";
                                            objRow.style.Color = "#fff";
                                        }
                                        else {
                                            objRow.style.backgroundColor = "#FFFFFF";
                                        }
                                        var objGridView = objRow.parentNode;

                                        //Get all input elements in Gridview
                                        var list = objGridView.getElementsByTagName("input");
                                        for (var i = 0; i < list.length; i++) {
                                            var objHeaderChkBox = list[0];

                                            //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                                            var checked = true;

                                            if (list[i].type == "checkbox" && list[i] != objHeaderChkBox) {
                                                if (!list[i].checked) {
                                                    checked = false;
                                                    break;
                                                }
                                            }
                                        }
                                        objHeaderChkBox.checked = checked;
                                    }
                                </script>




                                <asp:Button ID="btn_btn_delver_process" Style="margin: 10px 10px 0px 0px; float: right;" OnClick="btn_btn_delver_process_Click" runat="server" Text="Submit" class="btn btn-danger find-dv-btn" OnClientClick="return confirm('Are you sure you want to Deliver
 ?');" />

                                <a href="#!" data-dismiss="modal" style="margin: 10px 10px 0px 0px;" class="btn btn-primary find-dv-btn">Close</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
