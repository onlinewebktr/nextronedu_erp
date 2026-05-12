<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assets-attributes.aspx.cs" Inherits="school_web.Inventory_management.Slip.assets_attributes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="css/assets-print.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/assets-print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-sec" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl" runat="server" id="reportcrdDV">
            <div class="prnt-btn-sec" id="printBtns" runat="server">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="btn_back" CssClass="back-btn" runat="server" OnClick="btn_back_Click" />
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec">
                    <div class="invoice-height-manage">
                        <div class="invoice-wpr">
                            <div style="margin: 0px; padding: 0px; float: left; width: 100%">


                                <table style="margin-top: 5px; width:100%; float:left;padding:0px;font-size: 21px;border-bottom: 2px solid #000;">
                                    <tr>
                                        <td style="width: 100px; text-align: center; padding: 0px; border: 1px solid #fff;">
                                            <asp:Image ID="img_logo" runat="server" Style="width: 100px; margin: 0px 0px 0px 0px; border: 1px solid #a7a7a7; border: 1px solid #fff;" />
                                        </td>
                                        <td style="padding: 0px; border: 1px solid #fff;">

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

                            </div>
                            <div style="margin: 0px; padding: 0px; float: left; width: 100%">
                                <div class="report-card-wpr" style="height: 1200px">
                                    <div class="itmtbl-wpr">
                                        <h2 class="itmtbl-wpr-h">Item Details</h2>
                                        <table>
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Name</th>
                                                    <th>Purchase Date</th>
                                                    <th>Supplier</th>
                                                    <th>Invoice no.</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RPDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_item_Name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("Purchase_date")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_party_name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="att-itmtbl-wpr">
                                        <h2 class="att-itmtbl-wpr-h">Attribute Details</h2>
                                        <table>
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Attribute Name</th>
                                                    <th>Attribute No.</th>
                                                    <th>Valid Till Date</th>
                                                    <th>Reminder Date</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rp_attribute" OnItemDataBound="rp_attribute_ItemDataBound" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_attribute_name" runat="server" Text='<%#Bind("Attribute_name")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_attribute_no" runat="server" Text='<%#Bind("Attribute_no")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_attribute_valid_to_date" runat="server" Text='<%#Bind("Attribute_valid_to_date")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_attribute_Reminder_Date" runat="server" Text='<%#Bind("Attribute_Reminder_Date")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                                <asp:Label ID="lbl_attribute_valid_to_idate" Visible="false" runat="server" Text='<%#Bind("Attribute_valid_to_idate")%>'></asp:Label>
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
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
