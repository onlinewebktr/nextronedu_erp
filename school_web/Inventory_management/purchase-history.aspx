<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="purchase-history.aspx.cs" Inherits="school_web.Inventory_management.purchase_history" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Purchase History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
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
            printWindow.document.write(' <style  type="text/css" media="print">  td,th {border: 1px solid #000; padding: 0px 0px 0px 5px!important;} </style><link href="Slip/print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Dosis:200,300,400,500,600,700,800&display=swap" rel="stylesheet"/><link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />');
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
    <div class="page-content" >
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
            <div class="breadcrumb-title pe-3">Purchase Entry</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Purchase History</li>
                    </ol>
                </nav>
            </div>
        </div>

        <div class="row" >
            <div class="col-xl-6">
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded serchbox">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-12">
                                    <label for="validationCustom01" class="form-label">Select Supplier Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_supplier"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_supplier" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">From Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_from_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_from_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">To Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_to_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_to_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>

                                <div class="col-md-2" style="padding: 26px 0px 0px 0px;">
                                    <asp:Button ID="Btn_Find" runat="server" Text="Find" OnClick="Btn_Find_Click" CssClass="btn btn-primary" ValidationGroup="a" />
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
                                <div class="col-md-10">
                                    <label for="validationCustom01" class="form-label">Select Item Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_Item"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_Item" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>


                                <div class="col-md-2" style="padding: 26px 0px 0px 0px;">
                                    <asp:Button ID="btn_itemwise" runat="server" Text="Find" OnClick="btn_itemwise_Click" CssClass="btn btn-primary" ValidationGroup="a" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-xl-12" id="tblPrintIQ">
                <h6 class="mb-0 text-uppercase">Purchase History
                       <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right"><i class="bx bx-export"> </i></asp:LinkButton>
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel('tblPrintIQ')" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right"><i class="bx bx-printer" aria-hidden="true"></i></asp:LinkButton>

                </h6>
                <hr />
                <div class="card" >
                    <div class="card-body">
                        <div class="table-responsive">

                            <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_view_items" OnClick="lnk_view_items_Click" runat="server">View Slip</asp:LinkButton>
                                            <asp:LinkButton ID="lnk_view_item_details" OnClick="lnk_view_item_details_Click" runat="server">View Item</asp:LinkButton>


                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("Purchase_date")%>'></asp:Label>



                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase From">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_party_name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label>
                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_firm" runat="server" Text='<%#Bind("firm")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_partyid" runat="server" Text='<%#Bind("party_id")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_unique_entry_id" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Items">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_total_items" runat="server" Text='<%#Bind("Total_items")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Total_qty")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Total_Purchase_rate" runat="server" Text='<%#Bind("Total_Purchase_rate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Taxable">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text='<%#Bind("Total_taxable")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total GST.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Total_Gst_value" runat="server" Text='<%#Bind("Total_Gst_value")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total IGST">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("Total_IGST")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total CGST">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("Total_CGST")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total SGST">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("Total_SGST")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Expense">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_txt_freight" runat="server" Text='<%#Bind("txt_freight")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Total_netamount" runat="server" Text='<%#Bind("Total_netamount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Round Off">
                                        <ItemTemplate>
                                       <asp:Label ID="Label15" runat="server" Text='<%#Bind("roundoff")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grand Total">
                                        <ItemTemplate>
                                             <asp:Label ID="Label5" runat="server" Text='<%#Bind("Grand_total")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>




                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog" >
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Item Details</h5>
                    <a onclick="return PrintPanel('tdprint')" runat="server" class="btn noPrint" style="float: right; color: #fff; background-color: #2292f1; border-color: #51585e; cursor: pointer;">Print</a>

                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary noPrint" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body" style="max-height: 400px; overflow: auto;">
                    <div class="row" id="tdprint">
                        <table style="width: 100%;" class=" table table-hover table-striped table-bordered">
                            <tr>
                                <td>Party Name :-
                                    <asp:Label ID="lblparty_name" runat="server"></asp:Label>
                                </td>
                                <td>Invoice No. :- 
                                    <asp:Label ID="lblinvoice_no" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Item Name</th>
                                    <th>Brand</th>

                                    <th>Batch No</th>
                                    <th>Expiry Date</th>
                                    <th>Qty</th>
                                    <th>Unit</th>

                                    <th>Rate</th>

                                    <th>Total</th>
                                    <th>Disc(%)</th>
                                    <th>Taxable</th>
                                    <th>GST(%)</th>
                                    <th>Net Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GrdView_Generate_PO" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Batch_no" runat="server" Text='<%#Bind("Batch_no")%>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Expiry_date" runat="server" Text='<%#Bind("Expiry_date")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                            </td>

                                            <td style="text-align: right;">
                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Purchase_rate")%>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label9" runat="server" Text='<%#Bind("discount_amount")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("discount_perc")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label10" runat="server" Text='<%#Bind("taxable")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Gst_value")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("Gst_percent")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label8" runat="server" Text='<%#Bind("Net_total")%>'></asp:Label>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="13" style="text-align: right;">
                                        <asp:Label ID="lbl_total_value" runat="server" Font-Bold="true"></asp:Label>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
   <%-- <script type="text/javascript">
        $(function () {
            $("#<%=ddl_supplier.ClientID%>").select2();
            $("#<%=ddl_Item.ClientID%>").select2();

        });
    </script>--%>
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>
</asp:Content>
