<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="character-certificate2.aspx.cs" Inherits="school_web.Admin.slip.character_certificate2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Character Certificate</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />
    <link href="certificate.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="certificate.css" rel="stylesheet" type="text/css" />');
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
                        <div class="certificate-wpr">
                            <div class="certificate-wpr1">
                                <asp:Image ID="Image2" runat="server" class="watermarklogos" />
                                <div class="certificate-wpr2">
                                    <div class="certificate-no-sec">
                                        <p class="certificate-no-p-n">
                                            Certificate No. :
                                            <asp:Label ID="lbl_crtificate_nos" runat="server" Text=""></asp:Label>
                                        </p>
                                        <br />

                                    </div>
                                    <div class="certificate-no-sec">
                                        <p class="certificate-adm-no-p-n">
                                            Admission No. : 
                                            <asp:Label ID="lbl_adm_nos" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                    <div class="t-crtifcate-hdr-sec" style="text-align: center; width: 100%;" id="header_img" runat="server" visible="false">
                                        <asp:Image ID="img_header" runat="server" />
                                    </div>
                                    <div class="t-crtifcate-hdr-sec" id="header_txt" runat="server" visible="false">
                                        <asp:Label ID="lbl_school_name" class="certificate-comp-name-h-new" runat="server" Text=""></asp:Label>
                                        <p class="certificate-comp-aff-p-new">
                                            <asp:Label ID="lbl_affiliate" runat="server" Text=""></asp:Label>
                                        </p>
                                        <asp:Label ID="lbl_school_code" class="certificate-comp-schl-cod-p-new" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lbl_address" runat="server" Text="" class="certificate-comp-add-p-new"></asp:Label>
                                        <div class="certificate-logo-sec-new">
                                            <asp:Image ID="Image1" runat="server" />
                                        </div>
                                        <asp:Label ID="lbl_estd" runat="server" Text="" class="certificate-comp-estd-p-new"></asp:Label>
                                    </div>



                                    <div class="certificate-type-name-sec-new">
                                        <h1 class="certificate-type-name-h-new">Character Certificate</h1>
                                    </div>
                                    <%--<div class="income-footer-sec" style="margin: 18px 0px 0px 0px; text-align: right;position: unset;">
                                        

                                    </div>--%>
                                    <div class="certificate-content-sec-new">
                                        <p class="certificate-content-p-new">
                                            This is to certify that
                                            <asp:Label ID="lbl_std_name" runat="server" Text=""></asp:Label>
                                            daughter/son of
                                            <asp:Label ID="lbl_fther_name" runat="server" Text=""></asp:Label>




                                            has been a bonafide student of this school from (year) 
                                            <asp:Label ID="lbl_admision_year" runat="server"></asp:Label>
                                            <%--<br />--%>
                                            <%--in--%>
                                            <asp:Label ID="lbl_admission_class" runat="server" Style="display: none"></asp:Label>
                                            to (year)
                                            <asp:Label ID="lbl_current_year" runat="server"></asp:Label>

                                            <%--in --%>
                                            <asp:Label ID="lbl_current_class" runat="server" Style="display: none"></asp:Label>
                                            His/Her date of birth as per the school record is
                                            <asp:Label ID="lbl_dob" runat="server"></asp:Label>
                                            He/She completed ICSE Board Examination held in the year
                                            <asp:Label ID="lbl_exam_year" runat="server"></asp:Label>.
                                            with Unique ID No.
                                            <asp:Label ID="lbl_uid" runat="server"></asp:Label>
                                        </p>

                                        <p class="certificate-content-note-p-new">His/Her character and conduct has been <span>GOOD</span>  I wish him/her a good success in life</p>
                                    </div>


                                    <div class="certificate-footer-sec-new" style="padding: 0px 0px; bottom: 15px;" id="bydefult" runat="server">
                                        <p class="certificate-footer-pt-lft">
                                            Issue Date : <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                        </p>
                                        <%--<p class="certificate-footer-pt-cntr">Office Incharge</p>--%>
                                        <p class="certificate-footer-pt-rght">Signature of Principal</p>
                                    </div>

                                    <div class="sig-dv" id="Sig_setting" runat="server">
                                        <div class="sig-left" runat="server" id="Position1" visible="false">
                                            <div class="lft-sig-img-dv">
                                                <img runat="server" id="sign1" class="lft-sig-img" />
                                            </div>
                                            <p class="sig-ps">
                                                <asp:Label ID="lbl_deg1" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="sig-left" runat="server" id="Position2" visible="false">
                                            <div class="cntr-sig-img-dv">
                                                <img runat="server" id="sign2" class="cntr-sig-img" />
                                            </div>
                                            <p class="sig-ps">
                                                <asp:Label ID="lbl_deg2" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="sig-left" runat="server" id="Position3" visible="false">
                                            <div class="rght-sig-img-dv">
                                                <img id="sign3" runat="server" class="rght-sig-img" />
                                            </div>
                                            <p class="sig-ps">
                                                <asp:Label ID="lbl_deg3" runat="server"></asp:Label> 
                                            </p>
                                        </div>
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
