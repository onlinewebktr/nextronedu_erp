<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Sales_Return.aspx.cs" Inherits="school_web.Inventory_management.Sales_Return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Sales Return
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            padding: 2px 0px 5px 9px!important;
        }

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
            <div class="breadcrumb-title pe-3">Sale Entry  </div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Sales Return </li>
                    </ol>
                </nav>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-12">
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded serchbox">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Invoice No.  </label>
                                    <asp:TextBox ID="txt_invoice_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" CssClass="btn btn-primary" Style="margin: 27px 0px 0px 0px; padding: 3px 8px 3px 8px;" />

                                </div>

                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Return Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                            </div>

                        </div>


                        <div class="p-4 border rounded" style="background: #f1f1f1;" id="pnl_full" runat="server" visible="false">
                            <div class="col-sm-12">
                                <table class="tab-content table">

                                    <tr>
                                        <th>Adm. No.
                                        </th>
                                        <th>Session
                                        </th>
                                        <th>Roll No.
                                        </th>
                                        <th>Section
                                        </th>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_adm_no" runat="server"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_session" runat="server"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_roll_no" runat="server"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_section" runat="server"></asp:Label>

                                        </td>
                                    </tr>

                                    <tr>
                                        <th>Invoice No.
                                        </th>
                                        <th>Sale By
                                        </th>

                                        <th>Date
                                        </th>

                                        <th>Payment Mode.
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_invoice_no" runat="server"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_sale_by" runat="server"></asp:Label>

                                        </td>

                                        <td>
                                            <asp:Label ID="lbl_date" runat="server"></asp:Label>

                                        </td>

                                        <td>
                                            <asp:Label ID="lbl_payment_mode" runat="server"></asp:Label>

                                        </td>
                                    </tr>

                                </table>
                            </div>

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
                                                <asp:Label ID="lbl_Item_Code" runat="server" Text='<%#Bind("Item_code")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Barcode")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Unit_id" runat="server" Text='<%#Bind("unit_id")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                <asp:Label ID="lbl_stock_item_unique_entry_id" runat="server" Text='<%#Bind("stock_item_unique_entry_id")%>' Visible="false"></asp:Label>

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

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_qty" runat="server" Text='<%#Bind("Quantity")%>' Style="width: 40px;" AutoPostBack="true" OnTextChanged="txt_qty_TextChanged1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rate" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_gst_per" runat="server" Text='<%#Bind("GST_Percent")%>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Returen Remarks">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddl_returnregion" runat="server" class="form-select find-dv-txtbx" Style="height: 27px; padding: 3px 4px 4px 10px;">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Replacement</asp:ListItem>
                                                    <asp:ListItem>Damage</asp:ListItem>
                                                    <asp:ListItem>Fitting Issue</asp:ListItem>
                                                    <asp:ListItem>Do Not Want Anymore</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                 <asp:CheckBox ID="rowChkBox" AutoPostBack="true" runat="server" OnCheckedChanged="rowChkBox_CheckedChanged"   />
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-sm-12" id="finaltotal" runat="server" visible="false">
                                <div class="row" style="background-color: #5be2f7; padding-top: 5px; border-radius: 5px; padding-bottom: 5px;">
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

                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Mode :  </label>
                                            <asp:DropDownList ID="ddl_Return_Mode" runat="server" class="form-select find-dv-txtbx" Style="height: 27px; padding: 3px 4px 4px 10px;">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Cash</asp:ListItem>
                                                <asp:ListItem>UPI</asp:ListItem>
                                                <asp:ListItem>Netbnaking</asp:ListItem>
                                                <asp:ListItem>Wallet</asp:ListItem>
                                            </asp:DropDownList>




                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4" id="pnl_mode_t_nS">
                                        <div class="position-relative form-group">
                                            <label>
                                                <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label-fnds" Text="Transaction No."></asp:Label>
                                            </label>
                                            <asp:TextBox ID="txt_transaction_id" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>




                                        </div>
                                    </div>


                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                        <div class="position-relative form-group">
                                            <label>Remarks :  </label>
                                            <asp:TextBox ID="txt_remarks" runat="server" class="form-control find-dv-txtbx" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
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




            on_payment_mode_selection();
            $("#<%=ddl_Return_Mode.ClientID%>").on('change', function () {
                on_payment_mode_selection();
            })
        });

        function on_payment_mode_selection() {
            if ($('#<%= ddl_Return_Mode.ClientID %> option:selected').val() == "Select") {
                $("#pnl_mode_t_nS").hide();
            }
            if ($('#<%= ddl_Return_Mode.ClientID %> option:selected').val() == "Cash") {
                $("#pnl_mode_t_nS").hide();
            }
            if ($('#<%= ddl_Return_Mode.ClientID %> option:selected').val() == "Wallet") {
                $("#pnl_mode_t_nS").hide();
            }
            if ($('#<%= ddl_Return_Mode.ClientID %> option:selected').val() == "UPI") {
                $("#pnl_mode_t_nS").show();

                $("#<%=lbl_mode_trns_no.ClientID%>").text("UTR No.");

            }


            if ($('#<%= ddl_Return_Mode.ClientID %> option:selected').val() == "Netbanking") {
                $("#pnl_mode_t_nS").show();

                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");


            }



        }
    </script>
</asp:Content>
