<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Abacus_certificate.aspx.cs" Inherits="school_web.Admin.slip.Abacus_certificate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Abacus certificate</title>
     <link href="css/certificate.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/certificate.css" rel="stylesheet" type="text/css" />');
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
                <div class="invoice-inr-secCertific">
                    <div class="invoice-wpr">
                       
                            <div class="certificate-wprhori">

                                <div class="crtificate-img-wpr" id="baground" runat="server">
                                    <img src="images/Abacus_certificate.jpg" />
                                </div>

                                <div class="certificate-horikk">
                                    <div class="certificate-content-t-sec">

                                        <div class="certtextkk">
                                            <asp:Label ID="lbl_year2" runat="server" class="certtextkk_label marg70" style="display:none"> </asp:Label>
                                        </div>

                                        <div class="certtextkk mcmainnn" style="margin: 61px 0px 0px 0px;">
                                            <asp:Label ID="lbl_name" runat="server" class="certtextkklabmm certmkmm2"> </asp:Label>
                                        </div>

                                        <div class="certtextkk mcmainnn" style="margin: 33px 0px 0px 0px;">
                                            <asp:Label ID="lbl_participated" runat="server" class="certtextkklabmm certmk3" style="width: 86%;
    text-align: center;"> </asp:Label>
                                             <asp:Label ID="lbl_division" runat="server" class="certtextkklabmm certmk4" style="padding-left: 14%;"> </asp:Label>
                                        </div>

                                         <div class="certtextkk mcmainnn" style="text-align: center;
     
    padding-left: 27%;
    width: 78%;">
                                            <asp:Label ID="lbl_player" runat="server" class="certtextkklabmm certmk5"> </asp:Label>
                                        </div>

                                        <div class="certtextkk mcmainnn2" style="margin: 224px 0px 0px 0px;">
                                            <asp:Label ID="lbl_date" runat="server" class="certtextkklabmm certmk6"></asp:Label>
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
