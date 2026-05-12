<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Verify_Generated_OP.aspx.cs" Inherits="school_web.Inventory_management.Verify_Generated_OP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Verify  Generated PO
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
            line-height: 25px;
            font-size: 16px !important;
            font-weight: normal;
        }

        label {
            display: inline-block;
            font-weight: bold;
        }

        .aspNetDisabled {
            display: block;
            width: 100%;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            -webkit-appearance: auto;
            -moz-appearance: auto;
            appearance: auto;
            border-radius: 0.25rem;
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
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
            <div class="breadcrumb-title pe-3">Generate PO</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Verify  Generated PO
                        </li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-12">
                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Generate PO"></asp:Label>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded" style="background: #d0fbff66;">
                            <div class="row g-3 needs-validation" novalidate="">

                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">P.O. No. <sup>*</sup></label>
                                    <asp:TextBox ID="txt_purchase_order_number" runat="server" class="form-control find-dv-txtbx" Style="height: 37px;"></asp:TextBox>
                                </div>

                                <div class="col-md-3" style="padding: 26px 0px 0px 0px;">
                                    <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>
                            <br />

                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">Supplier Name <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_suppliername"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_suppliername" runat="server" class="form-control find-dv-txtbx " Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hd_party_id" runat="server" Value="cash" />
                                    <asp:Label ID="lbl_supplier_details" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>

                            </div>

                            <div class="row g-3 needs-validation" novalidate="" style="margin-top: 10px;">
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">Select Item <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_Item"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_Item" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Item_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                   <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">
                                        Brand <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_brand"></asp:RequiredFieldValidator></sup>
                                        <asp:LinkButton ID="lnk_refersh" Visible="false" runat="server"   Style="float: right; margin: 0px 0px 0px 4px; padding: 0px 5px; background-color: #bb00bb; color: #fff;"> <i class="bx bx-refresh"></i> </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton1" Visible="false" runat="server"  Text="+" Style="float: right; margin: 0px 0px 0px 4px; padding: 0px 5px; background-color: #bb00bb; color: #fff;" />

                                    </label>
                                    <asp:DropDownList ID="ddl_brand" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Select Unit<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_unit"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_unit" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                </div>
                                   <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Previous Rate <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_rate"></asp:RequiredFieldValidator></sup></label>
                                    <asp:Label ID="lbl_previous_rate" runat="server" class="form-control"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Qty <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_quantity"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="Txt_quantity" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="Txt_quantity_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Rate <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_rate"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_rate" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txt_rate_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Total Rate  </label>
                                    <asp:Label ID="lbl_total" runat="server" class="form-control"></asp:Label>
                                </div>
                                <div class="col-md-3" style="padding: 26px 0px 0px 0px;">
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
                <asp:Panel ID="pnl_added_items" runat="server" Visible="false">
                    <h6 class="mb-0 text-uppercase">Generated PO</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">

                                        <br />

                                        <div class="col-sm-12">
                                            <asp:GridView ID="GrdView_Generate_PO" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_rate" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_totalrate" runat="server" Text='<%#Bind("Total_rate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Purchase_id" runat="server" Text='<%#Bind("PO_no")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Item_id" runat="server" Text='<%#Bind("Item_id")%>' Visible="false"></asp:Label>
                                                            <%--<asp:Label ID="lbl_Brand_id" runat="server" Text='<%#Bind("Brand_id")%>' Visible="false"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:Button ID="btn_final_update" runat="server" CssClass="btn btn-primary" Text="Final Submit" OnClick="btn_final_update_Click" />
                                            <asp:Button ID="btn_print" runat="server" CssClass="btn btn-success" Visible="false" Text="Print" OnClick="btn_print_Click" />
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

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=ddl_Item.ClientID%>").select2();

        });
    </script>
</asp:Content>
