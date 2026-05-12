<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="school_web.Inventory_management.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inventory
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="css/new_dashboard.css" rel="stylesheet" />


    <style>
        .table th, .table td {
            padding: 8px !important;
            vertical-align: top !important;
            border-top: 1px solid #e9ecef !important;
            text-align: left;
        }

        .dashboard-bx-wpr-cntnt-p {
            font-size: 17px !important;
        }

        .dashboard-bx-wpr-cntnt-count-p {
            font-weight: 400 !important;
        }

        .inv-dashbrd-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .inv-dashbrd-bx-wpr {
            margin: 0px 0px 10px 0px;
            padding: 20px 10px;
            width: 100%;
            float: left;
            background: #f27b53;
            border: 1px solid #eb6032;
            color: #f7f7f7;
            border-radius: 3px;
            -webkit-box-shadow: 0 2px 8px 0 rgb(183 192 206);
        }

        .inv-dashbrd-bx-ico-sec {
            margin: 0px;
            padding: 0px;
            width: 25%;
            float: left;
        }

            .inv-dashbrd-bx-ico-sec i {
                margin: 0px;
                padding: 0px;
                font-size: 27px;
                line-height: 40px;
                width: 40px;
                text-align: center;
                box-shadow: 5px 3px 10px 0 rgb(21 15 15 / 30%);
                border-radius: 50% 50%;
            }

        .inv-dashbrd-bx-contnt-sec {
            margin: 0px;
            padding: 0px;
            width: 75%;
            float: left;
        }

            .inv-dashbrd-bx-contnt-sec h2 {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
                color: #f7f7f7;
                font-size: 17px;
                line-height: 25px;
            }

            .inv-dashbrd-bx-contnt-sec p {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
                font-size: 16px;
            }

        .inv-dashbrd-grdwprs {
            margin: 10px 0px 0px 0px;
            padding: 5px 5px;
            width: 100%;
            float: left;
            background: #fff;
            border: 1px solid #ddd;
            -webkit-box-shadow: 0 0px 4px 0 rgb(183 192 206 / 10%);
        }

        .inv-dashbrd-dmnd-h {
            margin: 0px;
            padding: 0px 0px 3px 0px;
            width: 100%;
            float: left;
            font-size: 20px;
            line-height: 25px;
            color: #3c3c3c;
        }

        .viwallbtns {
            margin: 1px 0px 0px 0px;
            padding: 2px 5px 2px 5px;
            width: auto;
            float: right;
            font-size: 13px;
            line-height: 15px;
            color: #ffffff;
            border-radius: 2px;
            background: #847DC3;
            border: 1px solid #6f67b5;
        }

            .viwallbtns:hover {
                color: #ffffff;
                background: #847DC3;
                border: 1px solid #6f67b5;
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

        <div class="inv-dashbrd-wpr">
            <div class="row">
                <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12">
                    <a href="Available_stock.aspx">
                        <div class="inv-dashbrd-bx-wpr">
                            <div class="inv-dashbrd-bx-ico-sec">
                                <i class="bx bx-grid-alt"></i>
                            </div>
                            <div class="inv-dashbrd-bx-contnt-sec">
                                <h2>Total Avl Item</h2>
                                <p runat="server" id="in_of_stocK">0</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12">
                    <a href="Out_of_stock.aspx">
                        <div class="inv-dashbrd-bx-wpr" style="background: #DE587B; border: 1px solid #d54268;">
                            <div class="inv-dashbrd-bx-ico-sec">
                                <i class="bx bx-message-square-x"></i>
                            </div>
                            <div class="inv-dashbrd-bx-contnt-sec">
                                <h2>Out of Stock Item</h2>
                                <p runat="server" id="out_of_stocK">0</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12" style="display: none">
                    <a href="Generated_Demand.aspx">
                        <div class="inv-dashbrd-bx-wpr" style="background: #847DC3; border: 1px solid #6f67b5;">
                            <div class="inv-dashbrd-bx-ico-sec">
                                <i class="bx bx-git-pull-request"></i>
                            </div>
                            <div class="inv-dashbrd-bx-contnt-sec">
                                <h2>Requested Demand</h2>
                                <p runat="server" id="demand_requesT">0</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-sm-3 col-xs-12 col-xs-12" style="display: none">
                    <a href="most-transfer-item.aspx">
                        <div class="inv-dashbrd-bx-wpr" style="background: #26B79A; border: 1px solid #26B79A;">
                            <div class="inv-dashbrd-bx-ico-sec">
                                <i class="bx bx-transfer-alt"></i>
                            </div>
                            <div class="inv-dashbrd-bx-contnt-sec">
                                <h2>Most Transferred</h2>
                                <p runat="server" id="MostTransfered">0</p>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>

        <div class="inv-dashbrd-grdwprs" style="display: none">
            <div class="row">
                <div class="col-lg-12 col-sm-12 col-xs-12 col-xs-12">
                    <h2 class="inv-dashbrd-dmnd-h">Recent Generated Demand <a href="Generated_Demand.aspx" class="viwallbtns">View All</a></h2>

                    <table style="width: 100%;" id="example22" class=" table table-hover table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Store Name</th>
                                <th>Store Type</th>
                                <th>Date</th>
                                <th>Demand No.</th>
                                <th>Total Qty</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Store_name" runat="server" Text='<%#Bind("Store_name")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Sector" runat="server" Text='<%#Bind("Sector")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Demand_no" runat="server" Text='<%#Bind("Demand_no")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Qty" runat="server" Text='<%#Bind("Qty")%>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lbl_Cart_id" runat="server" Text='<%#Bind("Store_id")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_Is_accept" runat="server" Text='<%#Bind("Is_accept")%>' Visible="false"></asp:Label>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="inv-dashbrd-grdwprs">
            <div class="row">
                <div class="col-lg-12 col-sm-12 col-xs-12 col-xs-12">
                    <h2 class="inv-dashbrd-dmnd-h">Top 10 Recent Purchase <a href="purchase-history.aspx" class="viwallbtns">View All</a></h2>

                    <table style="width: 100%;" id="example44" class=" table table-hover table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Date</th>
                                <th>Store Name</th>
                                <th>Invoice No.</th>
                                <th>Total Items</th>
                                <th>Total Qty.</th>
                                <th>Total Rate</th>
                                <th>Total Gst.</th>
                                <th>Total IGST</th>
                                <th>Total CGST</th>
                                <th>Total SGST</th>
                                <th>Grand Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("Purchase_date")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("Store_name")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_total_items" runat="server" Text='<%#Bind("Total_items")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Total_qty")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Total_Purchase_rate" runat="server" Text='<%#Bind("Total_Purchase_rate")%>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lbl_Total_Gst_value" runat="server" Text='<%#Bind("Total_Gst_value")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("Total_IGST")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("Total_CGST")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("Total_SGST")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("Grand_total")%>'></asp:Label>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="col-lg-12 col-sm-12 col-xs-12 col-xs-12">
                    <h2 class="inv-dashbrd-dmnd-h">Top 10 Recent Order <a href="Online_Order_Pending.aspx?order=all" class="viwallbtns">View All</a></h2>

                    <table style="width: 100%;" id="example444" class=" table table-hover table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Date</th>
                                <th>Name</th>
                                <th>Adm. No</th>
                                <th>App Order No.</th>
                                <th>Total Items</th>
                                <th>Total Qty.</th>
                                <th>Total Amount</th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="Repeater2" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("Date1")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Bill_No" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label ID="lbl_user_id" runat="server" Text='<%#Bind("user_id")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("Bill_No")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_total_items" runat="server" Text='<%#Bind("Total_Item")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Total_Quantity")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Total_Purchase_rate" runat="server" Text='<%#Bind("Total_amount")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
