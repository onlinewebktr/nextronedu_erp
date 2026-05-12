<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesEntry.aspx.cs" Inherits="school_web.Inventory_management.SalesEntry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sale Entry</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--favicon-->
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/sweetalert2@11.min.js"></script>
    <link href="css/sale_entry.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Lato:ital,wght@0,100;0,300;0,400;1,100&display=swap" rel="stylesheet" />
    <link href="../assets/css/icons.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            // var textBox = document.getElementById("#lbl_time");

            function time() {
                var d = new Date();
                var s = d.getSeconds();
                var m = d.getMinutes();
                var h = d.getHours();
                // textBox.value =
                // 

                document.getElementById("<%=lbl_time.ClientID%>").innerText = ("0" + h).substr(-2) + ":" + ("0" + m).substr(-2) + ":" + ("0" + s).substr(-2);
            }

            setInterval(time, 1000);
        });
       

          

    
    </script>
    <script>

        function alertSuccess(msg) {

            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: msg,
                timer: 2000,
                showConfirmButton: false
            });

        }

        function alertError(msg) {

            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: msg
            });

        }

    </script>
    <style>
     .mob::placeholder {
    color: #ffffff !important;
    opacity: 1 !important;
    font-weight: 500;
}
     .mob
     {
          color: #ffffff !important;
     }
     .mob.form-control:focus {
    color: #fff !important;
}
        .ui-autocomplete {
    z-index: 999999 !important;
    max-height: 250px;
    overflow-y: auto;
    overflow-x: hidden;
}
        .ui-menu .ui-menu-item-wrapper.ui-state-active {
    background: #198754 !important;
    color: white !important;
    font-size:13px;
}
        tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: none;
            border-width: 0;
        }

        label {
            font-size: 14px !important;
            margin-top: 6px;
            padding: 0px 7px 0px 0px !important;
            font-weight: bold;
        }

        .td1 {
            border: none;
        }

        .notificationpan {
            z-index: 999999999999 !important;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="page-wrapper">
                <div class="page-content">
                    <div id="notification"  >
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
                    <div class="page-breadcrumb d-flex align-items-center mb-2 eeewe">


                        <div class="col-xl-5">
                            <div class="ps-3 d-none d-sm-flex">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb mb-0 p-0">
                                        <li class="breadcrumb-item">
                                            <a href="Dashboard.aspx" style="color: #fff;"><i class="bx bx-home-alt" style="color: #fff;"></i></a>
                                        </li>
                                        <li class="breadcrumb-item active" aria-current="page" style="color: #fff;">Sale Entry</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                        <div class="col-xl-5">
                            <div class="ps-3 d-none d-sm-flex">
                                <asp:RadioButton ID="rd_item_wise" runat="server" Text="Item Wise Sale" AutoPostBack="true" Checked="true" GroupName="ak" OnCheckedChanged="rd_item_wise_CheckedChanged" />
                                <asp:RadioButton ID="rd_package_wise" runat="server" Text="Package Wise Sale" AutoPostBack="true" GroupName="ak" OnCheckedChanged="rd_package_wise_CheckedChanged" />
                            </div>
                        </div>
                        <div class="col-xl-1">
                            <a href="#" data-toggle="modal" data-target="#myModal4" style="background: #0c17e8; color: #fff; padding: 5px 9px 5px 10px; border-radius: 5px; margin: 0px 0px 0px 0px; text-decoration: none; position: absolute; right: 96px; top: 7px; font-size: 14px;"><i class="bx bx-plus-medical"></i>Customer Details</a>
                        </div>
                        <div class="col-xl-1">
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb mb-0 p-0">


                                    <li class="breadcrumb-item">
                                        <a href="Dashboard.aspx" style="background: #e80c0c; color: #fff; padding: 5px 5px 5px 5px; border-radius: 5px; margin: 0px 0px 0px 0px; text-decoration: none; position: absolute; right: 8px; top: 7px; font-size: 14px;"><i class="bx bx-arrow-back"></i>Back</a>
                                    </li>

                                </ol>
                            </nav>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-xl-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="p-4 border rounded" style="background: #0f3013; color: #fff;">
                                        <div class="row">
                                            <div class="col-xl-3">
                                                <div class="fnd-box-wpr">

                                                    <div class="fnd-box-wpr-inr">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Bill No.</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">


                                                                    <asp:TextBox ID="txt_temp_bill_no" runat="server" CssClass="form-control find-dv-txtbx disabled" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Prev. Sold Bill No.</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <asp:Label ID="lbl_prevsoldbillno" runat="server" CssClass="form-control find-dv-txtbx disabled">001</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <div class="fnd-box-wpr">

                                                    <div class="fnd-box-wpr-inr">

                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-2 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Sale To</label>
                                                                </div>
                                                                <div class="col-xl-10 padd-lft-5">
                                                                    <asp:TextBox ID="txt_seal_to" runat="server" CssClass="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_seal_to_TextChanged" Enabled="false"></asp:TextBox>
                                                                    <asp:HiddenField ID="hd_party_id" runat="server" />


                                                                </div>

                                                                <div class="col-xl-2 padd-rght-5" style="display: none">
                                                                    <label for="validationCustom01" class="form-label-fnds">Mobile No.</label>
                                                                </div>
                                                                <div class="col-xl-2 padd-lft-5" style="display: none">
                                                                    <asp:TextBox ID="txt_mobileno" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>




                                                                </div>



                                                            </div>
                                                        </div>
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-2 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Address     </label>
                                                                </div>
                                                                <div class="col-xl-10 padd-lft-5">
                                                                    <asp:TextBox ID="txt_address" runat="server" CssClass="form-control find-dv-txtbx disabled" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3">

                                                <div class="fnd-box-wpr">

                                                    <div class="fnd-box-wpr-inr">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Date</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">



                                                                    <asp:TextBox ID="txt_payment_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                    <script>
                                                                        $(function () {
                                                                            $("#<%=txt_payment_date.ClientID %>").datepicker({
                                                                                dateFormat: "dd/mm/yy",
                                                                                changeMonth: true,
                                                                                changeYear: true,
                                                                                yearRange: "2021:2024",
                                                                                maxDate: '0',
                                                                            }).attr("readonly", "true");
                                                                        });
                                                                    </script>


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="fnd-box-row-wpr" style="display: none">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Time</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <asp:Label ID="lbl_time" runat="server" CssClass="form-control find-dv-txtbx disabled"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>



                                        </div>
                                        <div class="row">
                                            <div class="col-xl-12">
                                                <table class="tab-content table" style="color: #fff;">
                                                    <tr>
                                                        <th>Description Name
                                                        </th>
                                                        <th>Selected Item
                                                        </th>
                                                        <th>Avl. Stock
                                                        </th>
                                                        <th>Qty.
                                                        </th>
                                                        <th>Unit
                                                        </th>
                                                        <th>Rate
                                                        </th>
                                                        <th>Total
                                                        </th>
                                                        <th>Disc.(%)
                                                        </th>
                                                        <th>Disc.(RS)
                                                        </th>
                                                        <th>Taxable.
                                                        </th>
                                                        <th style="display:none">GST(%)
                                                        </th>
                                                        <th style="display:none">>GST Value
                                                        </th>
                                                        <th>Net Total
                                                        </th>
                                                        <th></th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txt_item_Descriptin" Style="width: 200px" runat="server" CssClass="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_item_Descriptin_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_selected_item" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false" Style="width: 200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_avl_stock" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_qty" runat="server" CssClass="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_qty_TextChanged"></asp:TextBox>

                                                        </td>
                                                        <td>



                                                            <asp:TextBox ID="txt_unit" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>


                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_rate" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_total" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_dis_percentage" runat="server" CssClass="form-control find-dv-txtbx" OnTextChanged="txt_dis_percentage_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_dis_amount" runat="server" CssClass="form-control find-dv-txtbx" OnTextChanged="txt_dis_percentage_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_taxbalevalue" runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox>
                                                        </td>
                                                        <td style="display:none">>
                                                            <asp:TextBox ID="txt_gst_per" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="display:none">>
                                                            <asp:TextBox ID="txt_gstvalue" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_net_total" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btn_add_item" runat="server" Text="Add" CssClass="btn btn-primary" Style="width: 100%; width: 100%; padding: 0px 5px 0px 5px;" OnClick="btn_add_item_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>




                                        </div>

                                        <div class="row">
                                            <div class="col-xl-9">
                                                <div class="border rounded" style="background-color: #f0ebeb; min-height: 320px;">
                                                    <asp:GridView ID="grd_view" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_view_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_description" runat="server" Text='<%#Bind("Description_Item")%>'></asp:Label>


                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HSN">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Hsn_no" runat="server" Text='<%#Bind("HSN_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rate" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Disc.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_discount_amount" runat="server" Text='<%#Bind("Discount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Taxable Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Taxable" runat="server" Text='<%#Bind("Taxable")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="GST(%)" visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_gst_per" runat="server" Text='<%#Bind("GST_Percent")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GST Value" visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_gst_value" runat="server" Text='<%#Bind("Total_GST")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Net Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Net_total" runat="server" Text='<%#Bind("NetTotal")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="lnkEdit" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDel" runat="server" CssClass="lnkdelete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Item_Code" runat="server" Text='<%#Bind("Item_code")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_stock_item_unique_entry_id" runat="server" Text='<%#Bind("stock_item_unique_entry_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Discount_Percent" runat="server" Text='<%#Bind("Discount_Percent")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Package_id" runat="server" Text='<%#Bind("Package_id")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="border rounded" style="background-color: #690259;">

                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row" style="margin-top: 10px;">
                                                            <div class="col-xl-1 padd-rght-5">
                                                                <label for="validationCustom01" class="form-label-fnds" style="color: #f7ff00;">Total Items</label>
                                                            </div>
                                                            <div class="col-xl-1 padd-lft-5">
                                                                <asp:Label ID="lbl_total_items" Style="color: #f7ff00;" Font-Bold="true" runat="server">0</asp:Label>
                                                            </div>

                                                            <div class="col-xl-2 padd-rght-5">
                                                                <label for="validationCustom01" style="color: #f7ff00;" class="form-label-fnds">Total Quantity</label>
                                                            </div>
                                                            <div class="col-xl-1 padd-lft-5">
                                                                <asp:Label ID="lbl_total_qry" Font-Bold="true" Style="color: #f7ff00;" runat="server">0</asp:Label>
                                                            </div>

                                                            <div class="col-xl-1 padd-rght-5">
                                                                <label for="validationCustom01" class="form-label-fnds" style="color: #f7ff00;">Remarks</label>
                                                            </div>
                                                            <div class="col-xl-6 padd-lft-5">
                                                                <asp:TextBox ID="txt_remarks" TextMode="MultiLine" runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3">
                                                <div class="border rounded">


                                                    <table class="tab-content table" style="color: #fff; border-bottom-width: 0px;">
                                                        <tr>
                                                            <td>Total Amount</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_total_amount" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Total Discount</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_discount" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Total Taxable</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_total_taxable" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Total GST</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_total_tax" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>




                                                        <tr style="display: none">
                                                            <td>Trans. Charge</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_transprtation" runat="server" OnTextChanged="txt_transprtation_TextChanged" CssClass="form-control find-dv-txtbx" AutoPostBack="true">0</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none">
                                                            <td>Extra. Charge</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_expense" runat="server" CssClass="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_expense_TextChanged">0</asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Grand. Total</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_total_gr" runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Flat Discount</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_float_dis_count" runat="server" CssClass="form-control find-dv-txtbx" OnTextChanged="txt_float_dis_count_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                        </tr>




                                                        <tr>
                                                            <td>Round of</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_round" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Net Paybale</td>
                                                            <td>
                                                                <asp:TextBox ID="txt_net_total_amount" runat="server" CssClass="form-control find-dv-txtbx" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td colspan="2">
                                                                <asp:Button ID="btn_seal_fina" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btn_seal_fina_Click" Style="width: 100%;" />




                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </div>

                                        </div>
                                    </div>




                                </div>

                            </div>

                        </div>

                    </div>


                </div>
            </div>
        </div>
        <script src="Auto_complete/jquery-ui.js"></script>
        <link href="Auto_complete/jquery-ui.css" rel="stylesheet" />




        <script type="text/javascript">
            $(function () {
                <%--var sessionid = $("#<%=ddl_session.ClientID%>").val();--%>
                $("#<%=txt_seal_to.ClientID%>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: 'SalesEntry.aspx/GetRooPath',
                            data: "{ 'PathRooT': '" + request.term + "'}",
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
                                    response([{ label: 'No results found.' }]);
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
            });

            $(function () {
               
              
                $("#<%=txt_Mobile_no.ClientID%>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: 'SalesEntry.aspx/Getcustomername',

                            data: "{ 'PathRooT': '" + request.term + "'}",
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
                                    response([{ label: 'Please Add New Custome' }]);
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
            });

            $(function () {
                $("#<%=txt_item_Descriptin.ClientID%>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: 'SalesEntry.aspx/Getitem',
                            data: "{ 'PathRooT': '" + request.term + "',saletype :'" + $('#<%= hd_saletype.ClientID %>').val() + "',session_id :'" + $('#<%= hd_session_id.ClientID %>').val() + "'}",
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
                                    response([{ label: 'No results found.' }]);
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
            });

        </script>
        <asp:DropDownList ID="ddl_session" runat="server" Visible="false"></asp:DropDownList>

        <script type="text/javascript">
            function open_item_details() {
                $('#myModal2').modal('show');

            }
        </script>
        <script type="text/javascript">
            function open_item_pay() {
                $('#myModal3').modal('show');

            }

            function open_add_student() {
                $('#myModal4').modal('show');

            }
        </script>
        <div id="myModal2" runat="server" class="conf-alrt-sec modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="conf-alrt-inr" style="width: 750px;">
                <div class="popupTablWpR">
                    <div class="row">
                        <div class="col-md-6">
                            <h2 class="popup-dt-h">Item Details</h2>
                        </div>
                        <div class="col-md-6">
                            <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                                <li>
                                    <a href="#!" data-dismiss="modal" style="margin: 0px 0px 10px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                                        class="btn btn-primary find-dv-btn">Close</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <asp:Panel ID="Panel1" runat="server">

                        <table style="width: 100%;" id="Table1" border="1" class="table table-hover table-bordered ">

                            <tr>
                                <th>SL. No.</th>
                                <th>Item Name</th>
                                <th>HSN Code</th>
                                <th>Brand</th>
                                <th>Available Quantity</th>
                                <th>Unit</th>
                                <th>Sale Price</th>
                                <th>Select</th>

                            </tr>


                            <asp:Repeater ID="rp_std" runat="server" OnItemDataBound="rp_std_ItemDataBound">
                                <ItemTemplate>
                                    <tr runat="server" id="trR">
                                        <td>
                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItem_Name" runat="server" Text='<%#Bind("itemname") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_HSN" runat="server" Text='<%#Bind("hsn_no") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_brandname" runat="server" Text='<%#Bind("Brand_name") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Quantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Unit" runat="server" Text='<%#Bind("Unit") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Sale_rate" runat="server" Text='<%#Bind("Sale_rate") %>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:LinkButton ID="lnk_select" runat="server" OnClick="lnk_select_Click">Select</asp:LinkButton>

                                            <asp:Label ID="lbl_Store_id" Visible="false" runat="server" Text='<%#Bind("Store_id") %>'></asp:Label>
                                            <asp:Label ID="lbl_Stock_id" Visible="false" runat="server" Text='<%#Bind("Stock_id") %>'></asp:Label>
                                            <asp:Label ID="lbl_Item_Code" Visible="false" runat="server" Text='<%#Bind("Item_Code") %>'></asp:Label>
                                            <asp:Label ID="lbl_GST_Percent" Visible="false" runat="server" Text='<%#Bind("GST_Percent") %>'></asp:Label>

                                            <asp:Label ID="lbl_Unit_id" Visible="false" runat="server" Text='<%#Bind("Unit_id") %>'></asp:Label>

                                            <asp:Label ID="lbl_Expiry_date" Visible="false" runat="server" Text='<%#Bind("Expiry_date") %>'></asp:Label>

                                            <asp:Label ID="lbl_Batch_no" Visible="false" runat="server" Text='<%#Bind("Batch_no") %>'></asp:Label>
                                            <asp:Label ID="lbl_stock_item_unique_entry_id" Visible="false" runat="server" Text='<%#Bind("stock_item_unique_entry_id") %>'></asp:Label>
                                            <asp:Label ID="lbl_Purchase_Rate" Visible="false" runat="server" Text='<%#Bind("Purchase_Rate") %>'></asp:Label>
                                            <asp:Label ID="lbl_Brand_Id" Visible="false" runat="server" Text='<%#Bind("Brand_Id") %>'></asp:Label>

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                        <table style="width: 100%;" id="Table2" border="1" class="table table-hover table-bordered ">

                            <tr>
                                <th>SL. No.</th>
                                <th>Package Name</th>
                                <th>Package Amount</th>
                                <th>Class</th>
                                <th>Select</th>

                            </tr>


                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr runat="server" id="trR">
                                        <td>
                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItem_Name" runat="server" Text='<%#Bind("Package_Name") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Package_Amount" runat="server" Text='<%#Bind("Package_Amount") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnk_select_package" runat="server" OnClick="lnk_select_package_Click">Select</asp:LinkButton>
                                            <asp:Label ID="lbl_unique_entry_id" Visible="false" runat="server" Text='<%#Bind("unique_entry_id") %>'></asp:Label>

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>




                </div>
            </div>
        </div>
        <style>
            sup {
                top: -.5em;
                color: #ef0505 !important;
            }
        </style>
        <div id="myModal4" runat="server" class="conf-alrt-sec modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="top: 56px;">
            <div class="conf-alrt-inr" style="width: 750px; padding: 8px 12px 0px 10px;    height: 370px;">
                <div class="popupTablWpR">
                    <div class="row">
                        <div class="col-md-6">
                            <h2 class="popup-dt-h">Customer Information</h2>
                        </div>
                        <div class="col-md-6">
                            <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                                <li>
                                    <a href="#!" data-dismiss="modal" style="margin: 0px 0px 10px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                                        class="btn btn-primary find-dv-btn">Close</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <asp:Panel ID="pnl_student_entery" runat="server">
                        <table style="width: 100%;" id="Table111" border="1" class="table table-hover table-bordered ">
                            <tr>
                                  <asp:TextBox ID="txt_Mobile_no" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control mob" placeholder="Search Customer by Mobile No. " AutoPostBack="true" OnTextChanged="txt_Mobile_no_TextChanged" style="    margin: 0px 0px 6px 0px;
    background-color: #0c850e;
    border: 2px solid #000000;"></asp:TextBox>
                            </tr>


                               <tr>
                                <td class="td1" style="font-size: 14px;">Customer Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_student_name" ErrorMessage="Requred" ValidationGroup="ck"></asp:RequiredFieldValidator>

                                </sup>
                                </td>
                                <td class="td1" style="font-size: 14px;">Father's Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_father_name" ErrorMessage="Requred" ValidationGroup="ck"></asp:RequiredFieldValidator>

                                </sup>
                                </td>
                                <td class="td1" style="font-size: 14px;">
                                       Class
                                   </td>
                                
                              </tr>
                            <tr>
                                <td class="td1">
                                    <asp:TextBox ID="txt_student_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td class="td1">
                                    <asp:TextBox ID="txt_father_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form-control"></asp:TextBox>

                                </td>
                                <td class="td1">
                                     <asp:DropDownList ID="ddl_classname" runat="server" CssClass="form-select"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                
                                
                                
                                <td class="td1" style="font-size: 14px;">Section<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_section" ErrorMessage="Requred" ValidationGroup="ck"></asp:RequiredFieldValidator>

                                </sup>
                                </td>
                                  <td class="td1" style="font-size: 14px;">Address<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_adress" ErrorMessage="Requred" ValidationGroup="ck"></asp:RequiredFieldValidator>

                                </sup>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1">
                                    <asp:TextBox ID="txt_section" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                 <td class="td1" colspan="2">
                                    <asp:TextBox ID="txt_adress" TextMode="MultiLine" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <asp:Button ID="btn_Add_studnt" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_Add_studnt_Click" ValidationGroup="ck" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <asp:Label ID="lbl_msg2" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>

                                </td>
                            </tr>

                        </table>

                    </asp:Panel>








                </div>
            </div>



        </div>


        <style>
            .conf-alrt-inr {
                position: relative;
                top: 20%;
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
                   background: #ff0202 !important;
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

        <div id="myModal3" runat="server" class="conf-alrt-sec modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="top: 45px!important;">
            <div class="conf-alrt-inr" style="width: 750px;">
                <div class="popupTablWpR">
                    <div class="row">
                        <div class="col-md-6">
                            <h2 class="popup-dt-h">Receipt Details</h2>
                        </div>
                        <div class="col-md-6">
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
                            <td>Available Wallet Amount</td>
                            <td>
                                <asp:TextBox ID="txt_avl_Wallet" runat="server" CssClass="form-control find-dv-txtbx noclick"  Text="0.00"></asp:TextBox>

                            </td>
                        </tr>


                        <tr>
                            <td>Payable Amount</td>
                            <td>
                                <asp:TextBox ID="txt_payble_amount" runat="server" CssClass="form-control find-dv-txtbx noclick"></asp:TextBox></td>
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
                                <asp:DropDownList ID="ddl_paymentmode" runat="server"  AutoPostBack="false" class="form-select find-dv-txtbx">
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
                                    <asp:ListItem>Online</asp:ListItem>
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
                                <asp:Button ID="btn_pay_now" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btn_pay_now_Click" Style="width: 100%;" OnClientClick="return confirm('Are you sure you want pay?');" />
                            </td>
                        </tr>


                    </table>
                </div>
            </div>
        </div>
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
                $("#<%=ddl_paymentmode.ClientID%>").on('change', function () {
                    on_payment_mode_selection();
                })
            });

            function on_payment_mode_selection() {
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cash") {
                    $("#pnl_mode_t_nS").hide();
                    
                    
                    $("#bank_dt").hide();
                }

                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Wallet") {
                    $("#pnl_mode_t_nS").hide();
                    $("#<%=lbl_payment_type.ClientID%>").text("Wallet");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").hide();
                }


                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Netbanking") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Netbanking.");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");

                    $("#bank_dt").hide();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Deposited In Bank") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Deposited In Bank");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").show();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Sbdebit") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Sbdebit");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").hide();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cheque") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Cheque");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").show();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "NEFT") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("NEFT");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("UTR No.");
                    $("#bank_dt").hide();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Debitcard") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Debitcard");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").hide();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Creditcard") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Creditcard");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").hide();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Otherdcard") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Otherdcard");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").hide();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "UPI") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("UPI");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").hide();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Demand Draft(DD)") {
                    $("#pnl_mode_t_nS").show();
                    $("#<%=lbl_payment_type.ClientID%>").text("Demand Draft(DD");
                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                    $("#bank_dt").show();
                }
                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Pos") {
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
                calculate();
            });
               
            function calculate() {

                var payble_amount =
                    parseFloat($("#<%=txt_payble_amount.ClientID %>").val()) || 0;

               var recived_cash =
                   parseFloat($("#<%=txt_recived.ClientID %>").val()) || 0;

               var recived_bank =
                   parseFloat($("#<%=txt_recived_from_bank.ClientID %>").val()) || 0;

               // Bank amount cannot exceed payable
               if (recived_bank > payble_amount) {

                   recived_bank = payble_amount;

                   $("#<%=txt_recived_from_bank.ClientID %>")
            .val(recived_bank.toFixed(2));
    }

    var ttl_recivedamt = recived_cash + recived_bank;

    // Prevent extra total payment
    if (ttl_recivedamt > payble_amount) {

        recived_cash = payble_amount - recived_bank;

        if (recived_cash < 0) {
            recived_cash = 0;
        }

        $("#<%=txt_recived.ClientID %>")
            .val(recived_cash.toFixed(2));

        ttl_recivedamt = recived_cash + recived_bank;
    }

    var dues_amt = payble_amount - ttl_recivedamt;

    if (dues_amt < 0) {
        dues_amt = 0;
    }

    $("#<%=txt_total_paid.ClientID %>")
        .val(ttl_recivedamt.toFixed(2));

               $("#<%=txt_dues.ClientID %>")
                   .val(dues_amt.toFixed(2));
           }
        </script>
        <asp:HiddenField ID="hd_saletype" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />

        <asp:HiddenField ID="hdn_new_customer" runat="server" Value="0" />

    </form>
</body>
</html>
