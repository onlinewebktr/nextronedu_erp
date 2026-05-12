<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="add-package.aspx.cs" Inherits="school_web.Inventory_management.add_package" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add Package
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>

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

        .form-label {
            margin-bottom: 0rem !important;
            font-weight: bold !important;
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
            <div class="breadcrumb-title pe-3">Master</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Add Package </li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-7" style="margin: 0px auto;">
                <%--   <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Generate PO"></asp:Label>
                <hr />--%>

                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded" style="background: #d0fbff66;">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">
                                        Package Name <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_packagename"></asp:RequiredFieldValidator></sup>


                                    </label>
                                    <asp:TextBox ID="txt_packagename" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Class Name <sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_class_name" runat="server" class="form-select"></asp:DropDownList>



                                </div>


                            </div>
                            <div class="row g-3 needs-validation" novalidate="" style="margin-top: 10px;">
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">
                                        Item <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Item"></asp:RequiredFieldValidator></sup>

                                    </label>
                                    <asp:TextBox ID="txt_Item" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_Item_TextChanged"></asp:TextBox>
                                    <asp:HiddenField ID="hd_itemcode" runat="server" />
                                    <asp:HiddenField ID="hd_GSt_type" runat="server" />
                                </div>



                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Unit<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_unit"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_unit" runat="server" class="form-select"></asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Qty <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_qty"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_qty" onkeypress="return isNumberKey(event)" runat="server" class="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-3" style="padding: 19px 0px 0px 0px;">
                                    <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" CssClass="btn btn-primary" ValidationGroup="a" />

                                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" OnClick="Btn_Cancel_Click" class="btn btn-primary" Visible="true" ValidationGroup="a" />
                                </div>
                            </div>
                        </div>
                        <div class="p-4 border rounded">

                            <div class="col-sm-12">
                                <asp:GridView ID="grd_view" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%"   OnRowDataBound="grd_view_RowDataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("itemname")%>'></asp:Label>
                                                <asp:Label ID="lbl_Item_Code" runat="server" Text='<%#Bind("Item_Code")%>' Visible="false"></asp:Label>

                                                <asp:Label ID="lbl_Unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unitname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Sale_rate" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>Total</b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Net_total" runat="server" Text='<%#Bind("Total_Rate")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lbl_total_amount" runat="server"></asp:Label></b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-12" id="finaltotal" runat="server" style="text-align: center;" visible="false">

                                <asp:Button ID="btn_submit" runat="server" Text="Final Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" CausesValidation="false"  Style="margin: 0px!important;" OnClientClick="return confirm('Are you sure you want to submit?');" />


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



            $("#<%=txt_Item.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'add-package.aspx/Search_inventory_item',
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
