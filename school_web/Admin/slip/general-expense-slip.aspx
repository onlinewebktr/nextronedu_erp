<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="general-expense-slip.aspx.cs" Inherits="school_web.Admin.slip.general_expense_slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Slip</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />
    <link href="pay-slip.css" rel="stylesheet" />

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="pay-slip.css" rel="stylesheet" type="text/css" />');
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
        <div class="invoice-sec">
            <div class="prnt-btn-sec">
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
                    <div class="invoice-wpr">
                        <div class="slips-wpr">

                            <div class="slips-logo-sec">
                                <asp:Image ID="Image1" runat="server" />
                            </div>
                            <div class="slips-contnt-sec">
                                <asp:Label ID="lbl_school_name" class="slips-comp-name-h" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_address" runat="server" Text="" class="slips-comp-add-p"></asp:Label>
                                <asp:Label ID="lbl_contact_no" runat="server" Text="" class="slips-comp-add-p"></asp:Label>
                                <p class="slips-comp-mail-p">
                                    Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                </p>
                                <div class="slips-type-name-sec">
                                    <h1 class="slips-type-name-h">Payment Voucher</h1>
                                </div>
                            </div>

                            <div class="slips-body-contnt-sec">
                                <div class="slips-body-contnt-frst-sec">
                                    <div class="exp-slip-tbl-wpr">
                                        <table class="table-bordered" style="margin: 5px 0px 0px 0px;">
                                            <tr>
                                                <th colspan="2">Vendor Details</th>
                                            </tr>
                                            <tr>
                                                <td>Slip No. : 
                                                <asp:Label ID="lbl_slip_no" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Date : 
                                                <asp:Label ID="lbl_dates" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Vendor Name : 
                                                <asp:Label ID="lbl_vendor_name" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Contact Person Name : 
                                                <asp:Label ID="lbl_contact_person_name" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Mobile No. : 
                                                <asp:Label ID="lbl_v_mobile_no" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Address : 
                                                <asp:Label ID="lbl_v_address" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>


                                        <table class="table-bordered" style="margin: 5px 0px 0px 0px;">
                                            <tr>
                                                <th colspan="2">Receiver Details</th>
                                            </tr>
                                            <tr>
                                                <td>Name : 
                                                <asp:Label ID="lbl_rec_name" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Mobile No. : 
                                                <asp:Label ID="lbl_rec_mobile" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>

                                        <table class="table-bordered" style="margin: 5px 0px 0px 0px;"  runat="server" id="bill_dts" visible="false">
                                            <tr>
                                                <th colspan="3">Bill Details</th>
                                            </tr>
                                            <tr>
                                                <td>Bill No. : 
                                                <asp:Label ID="lbl_bill_no" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Bill Date : 
                                                <asp:Label ID="lbl_bill_date" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Bill Amount : 
                                                <asp:Label ID="lbl_bill_amt" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                        <table class="table-bordered" style="margin: 5px 0px 0px 0px;">
                                            <tr>
                                                <th colspan="3">Payment Details</th>
                                            </tr>
                                            <tr>
                                                <td>Payment Amount : 
                                                <asp:Label ID="lbl_pay_amt" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Payment Mode : 
                                                <asp:Label ID="lbl_pay_mode" runat="server" Text=""></asp:Label></td>
                                            </tr>

                                            <tr runat="server" id="ifcheckdV" visible="false">
                                                <td>Cheque No. : 
                                                <asp:Label ID="lbl_cheque_no" runat="server" Text=""></asp:Label></td>
                                                <td class="txt-rght">Cheque Date : 
                                                <asp:Label ID="lbl_cheque_date" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="ifcheckdV2" visible="false">
                                                <td colspan="2">Bank Name : 
                                                <asp:Label ID="lbl_cheque_bank_name" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr id="ifneftdV" visible="false" runat="server">
                                                <td colspan="2">URT No. : 
                                                <asp:Label ID="lbl_utr_no" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-rght">Amount in word : 
                                                <asp:Label ID="lbl_amt_in_word" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <%--<tr>
                                            <td colspan="2">Remarks :
                                                <asp:Label ID="lbl_remarks" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="txt-bld txt-rght">Total :
                                                <asp:Label ID="lbl_ttl" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lbl_ttl_in_rupee" runat="server" class="ttl-inwords"></asp:Label>
                                            </td>
                                        </tr>--%>
                                            <tr>
                                                <td colspan="2" class="txt-rght txt-bld">
                                                    <asp:Label ID="lbl_foor_school_name" runat="server" class="foot-school-name"></asp:Label>

                                                    <asp:Label ID="Label1" runat="server" class="foot-sig" Text="Signature"></asp:Label>

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
    </form>
</body>
</html>
