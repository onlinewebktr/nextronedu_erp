<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="certificates.aspx.cs" Inherits="school_web.Admin.slip.certificates" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Certificate</title>
    <link href="css/m-certificates.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/m-certificates.css" rel="stylesheet" type="text/css" />');
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
                                <asp:Image ID="img_templete" runat="server" />
                            </div>

                            <div class="certificate-horikk">
                                <asp:Panel ID="pnl_attendance_certificate" runat="server" Visible="false">
                                    <div class="certificate-content-t-sec">
                                        <div class="certtextkk mcmainnn">
                                            <asp:Label ID="lbl_name" runat="server" class="certtextkklabmm certmkmm2"> </asp:Label>
                                        </div>

                                        <div class="certtextkk scndrows">
                                            <asp:Label ID="lbl_class" runat="server" class="certtextkklabmm certmk3" Style="width: 45%;"> </asp:Label>
                                            <asp:Label ID="lbl_section" runat="server" class="certtextkklabmm certmk4" Style="padding-left: 8%;"> </asp:Label>
                                        </div>

                                        <div class="certtextkk thrdrows">
                                            <asp:Label ID="lbl_academic_year" runat="server" class="certtextkklabmm certmk5"></asp:Label>
                                        </div>

                                        <div class="certtextkk frethrows">
                                            <asp:Label ID="lbl_date" runat="server" class="certtextkklabmm certmk6"></asp:Label>
                                        </div>


                                        <div class="principaSign">
                                            <div class="principaSignInr">
                                                <asp:Image ID="img_pric_sign" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_competition_certificate" runat="server" Visible="false">
                                    <div class="certificate-content-t-sec">
                                        <div class="certtextkk mcmainnnComp">
                                            <asp:Label ID="lbl_std_name1" runat="server" class="certtextkklabmm certmkmm2" Style="padding-left: 22%;"></asp:Label>
                                        </div>

                                        <div class="certtextkk scndrows">
                                            <asp:Label ID="lbl_class1" runat="server" class="certtextkklabmm certmk3" Style="width: 36%;"> </asp:Label>
                                            <asp:Label ID="lbl_section1" runat="server" class="certtextkklabmm certmk4" Style="padding-left: 6%; width: 11%;"> </asp:Label>
                                            <asp:Label ID="lbl_securing" runat="server" class="certtextkklabmm certmk4" Style="padding-left: 15%;"> </asp:Label>
                                        </div>

                                        <div class="certtextkk thrdrows">
                                            <asp:Label ID="lbl_competition_name" runat="server" class="certtextkklabmm certmk5" Style="padding-left: 10%; width: 350px;"></asp:Label>
                                            <asp:Label ID="lbl_academic_year1" runat="server" class="certtextkklabmm certmk5" Style="padding-left: 38%;"></asp:Label>
                                        </div>

                                        <div class="certtextkk frethrows" style="margin: 83px 0px 0px 0px;">
                                            <asp:Label ID="lbl_issue_date" runat="server" class="certtextkklabmm certmk6"></asp:Label>
                                        </div>


                                        <div class="principaSign">
                                            <div class="principaSignInr">
                                                <asp:Image ID="Image2" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_rank_certificate" runat="server" Visible="false">
                                    <div class="certificate-content-t-sec">
                                        <div class="certtextkk mcmainnn-rank">
                                            <asp:Label ID="lbl_std_name2" runat="server" class="certtextkklabmm certmkmm2"> </asp:Label>
                                        </div>

                                        <div class="certtextkk scndrows-rank">
                                            <asp:Label ID="lbl_class2" runat="server" class="certtextkklabmm certmk3-rank" Style="width: 38%;"> </asp:Label>
                                            <asp:Label ID="lbl_section2" runat="server" class="certtextkklabmm certmk4-rank" Style="padding-left: 5%; width: 10%;"> </asp:Label>
                                            <asp:Label ID="lbl_securing2" runat="server" class="certtextkklabmm certmk4-rank" Style="padding-left: 16%;"> </asp:Label>
                                        </div>

                                        <div class="certtextkk thrdrows-rank">
                                            <asp:Label ID="lbl_academic_year2" runat="server" class="certtextkklabmm certmk5-rank"></asp:Label>
                                        </div>

                                        <div class="certtextkk frethrows-rank">
                                            <asp:Label ID="lbl_issue_date2" runat="server" class="certtextkklabmm certmk6-rank"></asp:Label>
                                        </div>


                                        <div class="principaSign">
                                            <div class="principaSignInr">
                                                <asp:Image ID="Image3" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
