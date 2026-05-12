<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="stock-transfer-to-store.aspx.cs" Inherits="school_web.Inventory_management.stock_transfer_to_store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Stock Transfer
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .select2-container--default .select2-selection--single {
            background-color: #fff;
            border: 0px solid #aaa;
            border-radius: 4px;
        }

        .select2-container--default .select2-selection--single {
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
            line-height: 22px;
            font-size: 15px !important;
            font-weight: normal;
        }

        label {
            display: inline-block;
            font-weight: bold;
        }
    </style>
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
            <div class="breadcrumb-title pe-3">Stock Transfer </div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Stock Transfer </li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-12">
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="" style="margin-bottom: 10px;">
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Transfer From (Store) <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_transfer_from"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_transfer_from" runat="server" Text="Central Store" ReadOnly="true" class="form-control find-dv-txtbx" Style="height: 37px;"></asp:TextBox>
                                    <asp:HiddenField ID="hd_cart_id" runat="server" Value="2001" />
                                </div>

                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Transfer To (Store) <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_store"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_store" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_store_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-md-2" style="display: none;">
                                    <label for="validationCustom01" class="form-label">Invoice No. </label>
                                    <asp:TextBox ID="txt_invoice_no" runat="server" class="form-control find-dv-txtbx" Style="height: 37px;" AutoPostBack="true" OnTextChanged="txt_invoice_no_TextChanged"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                            </div>



                            <div class="row g-3 needs-validation" novalidate="" style="margin-top: 10px;">
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Item Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_Item"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_Item" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Item_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:HiddenField ID="hd_itemcode" runat="server" />
                                    <asp:HiddenField ID="hd_GSt_type" runat="server" />
                                    <asp:HiddenField ID="hd_stock_id" runat="server" />
                                    <asp:HiddenField ID="hd_inv_row_id" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Batch No </label>
                                    <asp:TextBox ID="txt_batch_no" runat="server" class="form-control" Style="pointer-events: none;"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Expiry Date </label>
                                    <asp:TextBox ID="txt_expiry_date" runat="server" class="form-control find-dv-txtbx" Style="pointer-events: none;"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Brand <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_brand"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_brand" runat="server" class="form-control find-dv-txtbx" Style="pointer-events: none;"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Unit<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_unit"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_unit" runat="server" class="form-control" Style="pointer-events: none;"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">HSN </label>
                                    <asp:Label ID="lbl_hsn" runat="server" class="form-control" Style="height: 35px;"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Purchase Rate <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_purchse_rate"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_purchse_rate" runat="server" class="form-control" Style="pointer-events: none;"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <label for="validationCustom01" class="form-label">Avl.Qty. <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_qty"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_aval_quantity" runat="server" class="form-control" Style="pointer-events: none;"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <label for="validationCustom01" class="form-label">Qty <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_qty"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_qty" runat="server" class="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Total Rate </label>
                                    <asp:TextBox ID="txt_total_rate" runat="server" class="form-control" Style="pointer-events: none;"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="validationCustom01" class="form-label">GST<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_gst"></asp:RequiredFieldValidator></sup></label>
                                            <asp:DropDownList ID="ddl_gst" Style="pointer-events: none;" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label for="validationCustom01" class="form-label">GST Value </label>
                                            <asp:TextBox ID="txt_gst_value" runat="server" class="form-control" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label for="validationCustom01" class="form-label">Net Amount </label>
                                            <asp:TextBox ID="txt_net_amount" runat="server" class="form-control" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" CssClass="btn btn-primary" ValidationGroup="a" />
                                    <asp:Button ID="Btn_Update" runat="server" Text="Update" OnClick="Btn_Update_Click" class="btn btn-primary" Visible="false" ValidationGroup="a" />
                                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" OnClick="Btn_Cancel_Click" class="btn btn-primary" Visible="true" ValidationGroup="a" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>



            <div class="col-xl-12">
                <div class="card">
                    <div class="card-body">

                        <div class="p-4 border rounded" style="float: left; width: 100%;">
                            <div class="col-sm-12" style="float: left; width: 100%;">
                                <asp:GridView ID="grd_view" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                <asp:Label ID="lbl_Item_Code" runat="server" Text='<%#Bind("Item_Code")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Brand_Id")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Inv_row_id" runat="server" Text='<%#Bind("Inv_row_id")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Stock_id" runat="server" Text='<%#Bind("Stock_id")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Store_id" runat="server" Text='<%#Bind("Store_id")%>' Visible="false"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HSN">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Hsn_no" runat="server" Text='<%#Bind("hsn_no")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Purchase_Rate" runat="server" Text='<%#Bind("Purchase_Rate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Total_amount" runat="server" Text='<%#Bind("Total_amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gst Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Gts_amount" runat="server" Text='<%#Bind("Gts_amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_final_amount" runat="server" Text='<%#Bind("Final_amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                <asp:LinkButton ID="lnk_edit" runat="server" ToolTip="Edit" CausesValidation="false" OnClick="lnk_edit_Click"><i class="lni lni-pencil"> </i></asp:LinkButton>
                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-12" style="float: left; width: 100%;">
                                    <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:Label ID="lbl_total_rate" Style="margin: 0px 15px 0px 0px; font-size: 14px;"
                                        runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_total_gst" runat="server" Style="margin: 0px 15px 0px 0px; font-size: 14px;" Text=""></asp:Label>
                                    <asp:Label ID="lbl_final_toatal" runat="server" Style="margin: 0px 15px 0px 0px; font-size: 14px;" Text=""></asp:Label>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">


                                            <label for="validationCustom01" class="form-label">Receiver Name </label>
                                            <asp:TextBox ID="txt_receivername" runat="server" class="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                            <label for="validationCustom01" class="form-label">Remarks </label>
                                            <asp:TextBox ID="txt_remarks" runat="server" class="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" style="float: right;">
                                 
                                    <label style="width: 100%"></label>
                                    <asp:Button ID="btn_final_submit" runat="server" Text="Final Submit" Visible="false" CssClass="btn btn-primary" OnClick="btn_final_submit_Click" Style="margin: 0px!important;" />
                                    <asp:Button ID="btn_print" runat="server" Text="Print" Visible="false" CssClass="btn btn-danger" OnClick="btn_print_Click" Style="margin: 0px!important;" />
                                
                                       
                                     </div>
                                        </div>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">


    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_Item.ClientID%>").select2();
            $("#<%=txt_purchse_rate.ClientID %>").on('input', function () {
                calculate();
            });
            $("#<%=txt_qty.ClientID %>").on('input', function () {
                calculate();
            });

            function calculate() {
                var mrp = parseFloat($("#<%=txt_purchse_rate.ClientID %>").val());
                var qnt = parseFloat($("#<%=txt_qty.ClientID %>").val());
                var avalqnt = parseFloat($("#<%=txt_aval_quantity.ClientID %>").val());

                if (avalqnt < qnt) {
                    alert("Available quantity will not greater than transfer quantity.");
                }

                var tot_amt = "";
                tot_amt = (mrp * qnt);
                $("#<%=txt_total_rate.ClientID %>").val(tot_amt.toFixed(2));



                var gst_perc = parseFloat($("#<%=ddl_gst.ClientID %>").val());
                var gst_value = ((tot_amt * gst_perc) / 100);

                $("#<%=txt_gst_value.ClientID %>").val(gst_value.toFixed(2));

                var final_amt = "";
                final_amt = (tot_amt);
                $("#<%=txt_net_amount.ClientID %>").val(final_amt.toFixed(2));
            }
        });
    </script>




</asp:Content>
