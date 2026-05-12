<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Create_Supplier.aspx.cs" Inherits="school_web.Inventory_management.Create_Supplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Create Supplier
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-label {
            margin-bottom: 0.5rem !important;
            font-weight: bold !important;
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div id="notification">
            <div id="pan" class="notificationpan">

                <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                    <div class="d-flex align-items-center">

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
                        <li class="breadcrumb-item active" aria-current="page">Supplier Details </li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-12">
                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="">
                                 <div class="row">
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_suppliername"></asp:RequiredFieldValidator></sup></label>

                                    <asp:TextBox ID="txt_suppliername" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Address<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_address"></asp:RequiredFieldValidator></sup></label>

                                    <asp:TextBox ID="txt_address" runat="server" CssClass="form-control"></asp:TextBox>


                                </div>
                                     </div>
                                <div class="row">



                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">City<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_city"></asp:RequiredFieldValidator></sup></label>

                                        <asp:TextBox ID="txt_city" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">State<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_state"></asp:RequiredFieldValidator></sup></label>

                                        <asp:DropDownList ID="ddl_state" runat="server" CssClass="form-control find-dv-txtbx"></asp:DropDownList>


                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_mobile_no"></asp:RequiredFieldValidator></sup></label>

                                        <asp:TextBox ID="txt_mobile_no" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Care of<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Care_of"></asp:RequiredFieldValidator></sup></label>

                                        <asp:TextBox ID="txt_Care_of" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>

                                </div>
                                <div class="row">


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">GSTIN No.<sup> </sup></label>

                                        <asp:TextBox ID="txt_gstin" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>

                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">GSTIN Type<sup> </sup></label>

                                        <asp:DropDownList ID="ddl_gstin_type" runat="server" CssClass="form-control find-dv-txtbx">
                                            <asp:ListItem>UnRegistered</asp:ListItem>
                                            <asp:ListItem>Regular</asp:ListItem>
                                            <asp:ListItem>Composition</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">PAN No.<sup> </sup></label>

                                        <asp:TextBox ID="txt_pan_no" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Bank Name<sup></sup></label>

                                        <asp:TextBox ID="txt_bank_name" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="row">


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Account No.<sup></sup></label>

                                        <asp:TextBox ID="txt_account_no" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">IFSC Code<sup></sup></label>

                                        <asp:TextBox ID="txt_ifsc_code" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">
                                            Route<sup></sup>


                                        </label>

                                        <asp:DropDownList ID="ddl_route" runat="server" CssClass="form-control find-dv-txtbx"></asp:DropDownList>

                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Opening Balance<sup></sup></label>

                                        <asp:TextBox ID="txt_opening_balance" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Type<sup></sup></label>

                                        <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control find-dv-txtbx">
                                            <asp:ListItem>Dr</asp:ListItem>
                                            <asp:ListItem>Cr</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Date<sup></sup></label>

                                        <asp:TextBox ID="txt_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>


                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" CssClass="btn btn-primary" ValidationGroup="a" Style="margin-top: 27px;" />
                                    <asp:Button ID="Btn_Update" runat="server" Text="Update" OnClick="Btn_Update_Click" class="btn btn-primary" Visible="false" ValidationGroup="a" Style="margin-top: 27px;" />
                                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" OnClick="Btn_Cancel_Click" class="btn btn-dark" Visible="true"  CausesValidation="false"  Style="margin-top: 27px;" />

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
</asp:Content>
