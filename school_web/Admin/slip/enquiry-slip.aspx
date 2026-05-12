<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="enquiry-slip.aspx.cs" Inherits="school_web.Admin.slip.enquiry_slip" %>

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
                                    <h1 class="slips-type-name-h">Enquiry Form</h1>
                                </div>
                            </div>

                            <div class="slips-body-contnt-sec">
                                <div class="slips-body-contnt-frst-sec">
                                    <table class="table-bordered">
                                        <tr>
                                            <td class="tdpadd">Name :  </td>
                                        </tr>
                                        <tr>
                                            <td class="tdpadd">Class :  </td>
                                        </tr>
                                        <tr>
                                            <td class="tdpadd">Father's Name :  </td>
                                        </tr>
                                        <tr>
                                            <td class="tdpadd">Contact No. :  </td>
                                        </tr>
                                        <tr>
                                            <td class="tdpadd1">Address :  </td>
                                        </tr>
                                        <tr>
                                            <td class="tdpadd2">Remarks :  </td>
                                        </tr>
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
