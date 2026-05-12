<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_purchase_history.aspx.cs" Inherits="school_web.Inventory_management.Slip.Print_purchase_history" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Entry Bill</title>

    <link href="../css/Print.css" rel="stylesheet" />
    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>
    <style>
        .branch {
            margin: 0px auto;
            /* float: left; */
            padding: 0;
            height: auto;
            width: 800px;
            position: relative;
        }



        .containerNotFound {
            /* width: 25%; */
            margin-left: 2%;
            padding: 2%;
            text-align: center;
        }



        .printSlip {
            width: 100%;
            margin: 0px auto;
        }

        th {
            font-weight: bold;
            font-size: 11px;
            padding: 3px;
        }

        td {
            padding: 3px;
        }
        /**/
        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #fff !important;
            padding: 5px 11px 5px 8px !important;
            font-size: 12px !important;
            color: #000;
        }

        table tr td {
            padding: 4px 4px !important;
            border: 1px solid #1e1c1c;
            font-size: 14px;
            font-weight: 500;
        }

        .printBtn {
            float: right;
            width: 40px;
            margin-right: 20px;
            margin-top: 5px;
        }

        @media print {
            .noPrint1 {
                display: none;
            }

            .cnt_data {
                page-break-after: always;
                padding-bottom: 200px;
            }

            #Header, #Footer {
                display: none !important;
            }
        }
    </style>
    <script type="text/javascript">
        function pop(div) {
            document.getElementById(div).style.display = 'block';
        }
        function hide(div) {
            document.getElementById(div).style.display = 'none';
        }
        //To detect escape button
        document.onkeydown = function (evt) {
            evt = evt || window.event;
            if (evt.keyCode == 27) {
                hide('popDiv');
            }
        };
    </script>
    <style>
        .ontop {
            z-index: 999;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            display: none;
            position: absolute;
            background-color: #cccccc;
            color: #aaaaaa;
            opacity: .4;
            filter: alpha(opacity = 50);
        }

        #popup {
            width: 300px;
            height: 200px;
            position: absolute;
            color: #000000;
            background-color: #ffffff;
            /* To align popup window at the center of screen*/
            top: 50%;
            left: 50%;
            margin-top: -100px;
            margin-left: -150px;
        }
    </style>
    <style>
        /* Styles go here */
        /*.data-Table {
                margin-top: 100px;
        }*/
        /*.page-header, .page-header-space {
            height: 128px;
        }*/

        .page-header, .page-footer img {
            display: none;
        }

        .page-footer, .page-footer-space {
            height: 70px;
        }

        .page-footer {
            position: fixed;
            bottom: 20px;
            width: 800px;
        }

        .page-header {
            position: fixed;
            /*top: 0mm;*/
            /*width: 100%;*/
            width: 800px;
        }



        @page {
            margin: 10mm;
        }

        @media print {
            thead {
                display: table-header-group;
            }

            tfoot {
                display: table-footer-group;
            }

            button {
                display: none;
            }

            body {
                margin: 0;
            }

            .page-header, .page-footer img {
                display: block;
            }

            /* .page-header, .page-header-space {
                height: 128px;
            }*/
        }
    </style>

    <script language="javascript" type="text/javascript">
        function down() {
            window.top.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="divSlip">
            <asp:Label ID="lbl_session" runat="server" Visible="false"></asp:Label>
            <div class="printTopDiv noPrint1">

                <a href="javascript:" onclick="printit()">
                    <img src="../../images/print-recipt.png" class="printBtn noPrint1" />
                </a>
                <asp:ImageButton ID="btn_back" runat="server" OnClientClick="down();" ImageUrl="~/Inventory_management/Slip/close_icon.png" Height="36px" Style="margin-left: 245px" Width="37px" />

            </div>
            <div class="container">
                <div class="row">
                    <div class="branch" id="print_page">
                        <asp:HiddenField ID="hd_entry_id" runat="server" />
                        <div style="margin: 0px auto; margin: 0px; float: left; height: auto; width: 100%; padding: 0px 0px 0px 0px; border-collapse: collapse;">
                            <div class="row" style="width: 100%; float: left;">
                                <div class="printSlip">

                                    <table class="data-Table">
                                        <thead>
                                            <tr>
                                                <td colspan="4" class="border-top" style="border: none;">
                                                    <div id="page_header_space" runat="server" class="page-header-space" style="text-align: center;">
                                                        <asp:Image ID="img_header" runat="server" Style="margin: 0px auto; padding: 0px; width: 100%; border-bottom: 2px solid #fff;" />
                                                        <asp:Panel ID="headertext" runat="server" Visible="true">
                                                            <table style="margin-top: 5px; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 100px; text-align: center; padding: 0px;">
                                                                        <asp:Image ID="img_logo" runat="server" Style="width: 100px; margin: 0px 0px 0px 0px; border: 1px solid #a7a7a7;" />
                                                                    </td>
                                                                    <td style="padding: 0px;">

                                                                        <h2 style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                            <asp:Label ID="lbl_hospital_name" runat="server" Style="font-size: 20px;"></asp:Label></h2>
                                                                        <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                            <asp:Label ID="lbl_address1" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                        </p>
                                                                        <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                            <asp:Label ID="lbl_address2" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                        </p>
                                                                        <h2 style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                            <asp:Label ID="lbl_email_mobile" runat="server" Style="font-size: 12px;"></asp:Label></h2>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="border-top" style="border: none;">
                                                    <div style="background-color: #ebf1cc; box-shadow: 5px 5px #526c74; width: 50%; margin: 0px auto; text-align: center; font-size: 17px;">Purchase Bill</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 50%;">
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px;">
                                                        <asp:Label ID="lbl_partyname" runat="server" Style="font-size: 12px; float: left"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px;">
                                                        <asp:Label ID="lbl_mobile_no" runat="server" Style="font-size: 12px; float: left; font-weight: normal;"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px;">
                                                        <asp:Label ID="lbl_address" runat="server" Style="font-size: 11px; float: left; font-weight: normal;"></asp:Label>
                                                    </p>
                                                </td>
                                                <td colspan="2">
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                        <asp:Label ID="lbl_date" runat="server" Style="font-size: 11px; float: right"></asp:Label><br />
                                                        <asp:Label ID="lbl_po_no" runat="server" Style="font-size: 11px; float: right;"></asp:Label>
                                                    </p>
                                                </td>
                                            </tr>

                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="4">
                                                    <div class="page">


                                                        <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Item Name</th>
                                                                    <th>Brand</th>
                                                                    <%--<th>HSN No.</th>--%>
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
                                                                            <%--<td>
                                                                                <asp:Label ID="lbl_hsn_no" runat="server" Text='<%#Bind("Hsn_no")%>'></asp:Label>
                                                                            </td>--%>
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

                                                                            <td>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Purchase_rate")%>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label9" runat="server" Text='<%#Bind("discount_amount")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("discount_perc")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label10" runat="server" Text='<%#Bind("taxable")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Gst_value")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("Gst_percent")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%#Bind("Net_total")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 0px 30px 20px 0px; padding-left: 5px; text-align: left;">
                                                    <p>
                                                        Remarks :-<br />
                                                        <asp:Label ID="lbl_remarks" runat="server"></asp:Label>
                                                    </p>
                                                </td>
                                                <td colspan="2" style="padding: 0px 0px 20px 0px; padding-left: 5px; text-align: right;">
                                                    <table style="width: 50%; float: right;">
                                                        <tr>
                                                            <td>Total Amount :</td>
                                                            <td>
                                                                <asp:Label ID="lbl_Total_rate" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Discount :</td>
                                                            <td>
                                                                <asp:Label ID="lbl_discount" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td>GST :</td>
                                                            <td>
                                                                <asp:Label ID="lbl_GST" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>SGST :</td>
                                                            <td>
                                                                <asp:Label ID="lbl_sgst" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>CGST :</td>
                                                            <td>
                                                                <asp:Label ID="lbl_cgst" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Expense :</td>
                                                            <td>
                                                                <asp:Label ID="lbl_expense" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Grand Total :</td>
                                                            <td>
                                                                <asp:Label ID="lbl_grandtotal" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </table>


                                                </td>
                                            </tr>

                                        </tbody>


                                        <tfoot>
                                            <tr id="footer_sapace" runat="server">
                                                <td colspan="4" style="padding-top: 0px; padding-bottom: 0px; padding-left: 5px;">
                                                    <!--place holder for the fixed-position footer-->
                                                    <div id="page_footer_space" runat="server" class="page-footer-space">
                                                        <p style="padding-top: 20px; float: right; padding-bottom: 5px; text-align: center;">
                                                            <asp:Label ID="Label1" runat="server" Text="Authorised Signatory" Style="font-size: 15px; color: #ccc;"></asp:Label><br />
                                                            <br />
                                                        </p>
                                                        <asp:Image ID="img_footer" runat="server" Style="margin: 0px auto; padding: 0px; width: 100%;" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </tfoot>

                                    </table>
                                </div>
                            </div>



                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
