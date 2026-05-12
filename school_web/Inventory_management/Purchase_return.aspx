<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Purchase_return.aspx.cs" Inherits="school_web.Inventory_management.Purchase_return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Purchase Return 
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
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out
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
            <div class="breadcrumb-title pe-3">Purchase Return  </div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Purchase Return </li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-12">
                <%--   <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Generate PO"></asp:Label>
                <hr />--%>
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded serchbox"  >
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Supplier Name <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_suppliername"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_suppliername" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_suppliername_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label" style="width: 100%;">
                                    </label>
                                    <asp:Label ID="lbl_supplier_details" runat="server" Style="min-height: 35px;"></asp:Label>
                                    <asp:HiddenField ID="hd_party_id" runat="server" Value="cash" />
                                    <asp:HiddenField ID="hd_state_code" runat="server" />
                                    <asp:HiddenField ID="hd_cart_id" runat="server" Value="2001" />
                                    <asp:HiddenField ID="hd_cart_name" runat="server" Value="Central Store" />
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Invoice No. <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_invoice_no"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="txt_invoice_no" runat="server" class="form-select find-dv-txtbx" Style="height: 37px;" AutoPostBack="true" OnTextChanged="txt_invoice_no_TextChanged"></asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                            </div>
                            
                        </div>
                        <div class="p-4 border rounded" style="background: #f1f1f1;">

                            <div class="col-sm-12">
                                <asp:GridView ID="grd_view" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_view_RowDataBound">
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
                                                 <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                 <asp:Label ID="lbl_stockid" runat="server" Text='<%#Bind("Stock_id")%>' Visible="false"></asp:Label>
                                                 <asp:Label ID="lbl_stock_item_unique_entry_id" runat="server" Text='<%#Bind("stock_item_unique_entry_id")%>' Visible="false"></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch no">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Batch_no" runat="server" Text='<%#Bind("Batch_no")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Expiry_date" runat="server" Text='<%#Bind("Expiry_date")%>'></asp:Label>
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
                                                <asp:Label ID="lbl_Hsn_no" runat="server" Text='<%#Bind("Hsn_no")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_qty" runat="server" Text='<%#Bind("Quantity")%>' Style="width: 40px;" AutoPostBack="true" OnTextChanged="txt_qty_TextChanged1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rate" runat="server" Text='<%#Bind("Purchase_rate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_gst_per" runat="server" Text='<%#Bind("Gst_percent")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_gst_value" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Net_total" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-12" id="finaltotal" runat="server" visible="false">
                                <div class="row" style="background-color: #5be2f7;
  padding-top: 5px;
  border-radius: 5px;
  padding-bottom: 5px;">
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Total Items:  </label>
                                            <asp:Label ID="lbl_totl_items" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Total Qty.:  </label>
                                            <asp:Label ID="lbl_total_qty" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Total:  </label>
                                            <asp:Label ID="lbl_total_" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Total taxable:  </label>
                                            <asp:Label ID="lbl_total_taxable" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Total GST:  </label>
                                            <asp:Label ID="lbl_total_gst_value" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Round Off:  </label>
                                            <asp:Label ID="lbl_round_off" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Grand Total:  </label>
                                            <asp:Label ID="lbl_grand_total" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <label style="width: 100%"></label>
                                        <asp:Button ID="btn_submit" runat="server" Text="Final Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" ValidationGroup="a" Style="margin: 0px!important;" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            searching();
        });
        function searching() {
            $("#<%=txt_suppliername.ClientID%>").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: 'Search.aspx/Search_supplier_name',
                        data: "{ 'itemName': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                //response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        }
    </script>
</asp:Content>
