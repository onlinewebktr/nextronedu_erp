<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="character-certificateBD.aspx.cs" Inherits="school_web.Admin.slip.character_certificateBD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Print Invoice</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />


    <link href="certificatebd.css" rel="stylesheet" />


    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="certificatebd.css" rel="stylesheet" type="text/css" />');
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
                                <div class="certificate-wpr2" style="height:1100px;">
                                    
                                    <div class="t-crtifcate-hdr-sec" style="text-align: center; width: 100%;border-bottom: 1px solid #afafaf;" id="header_img" runat="server" visible="false">
                                        <asp:Image ID="img_header" runat="server" style="width:96%"/>
                                    </div>
                                    <div class="t-crtifcate-hdr-sec" id="header_txt" runat="server" visible="false">
                                        <div class="certificate-logo-sec">
                                            <asp:Image ID="Image1" runat="server" />

                                        </div>
                                        <asp:Label ID="lbl_school_name" class="certificate-comp-name-h" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lbl_address" runat="server" Text="" class="certificate-comp-add-p"></asp:Label>
                                        <asp:Label ID="lbl_contact_no" runat="server" Text="" class="certificate-comp-add-p"></asp:Label>
                                        <p class="certificate-comp-mail-p">
                                            Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>





                                    <div class="certificate-type-name-sec">
                                        <h1 class="certificate-type-name-h">Character Certificate</h1>
                                    </div>

                                      <div class="income-footer-sec" style="margin: 18px 0px 0px 0px; text-align: right;position: unset;">
                                            <p class="issue-dates" style="float: right;">
                                                Issue Date :
                                               
                                                 <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                            </p>

                                        </div>
                                    <div style="width:100%; float:left; margin: 6px 0px 8px 0px;">
                                        <div class="certificate-no-sec">
                                        <p class="certificate-no-p">
                                            Certificate No. :
                                            <asp:Label ID="lbl_crtificate_no" runat="server" Text=""></asp:Label>
                                        </p>
                                        
                                            
                                        
                                    </div>
                                    <div class="certificate-no-sec">
                                        <p class="certificate-adm-no-p">
                                            Admission No. : 
                                            <asp:Label ID="lbl_adm_no" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                    </div>

                                    <div class="certificate-content-sec">
                                        <p class="certificate-content-p">
                                            This is to certify that
                                            <asp:Label ID="lbl_std_name" runat="server" Text=""></asp:Label>
                                            daughter/son of
                                            <asp:Label ID="lbl_fther_name" runat="server" Text=""></asp:Label>
                                            and
                                             <asp:Label ID="lbl_mother_name" runat="server" Text=""></asp:Label>
                                            is/was a bonafide student of
                                             <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>
                                            during the academic year
                                            <asp:Label ID="llb_session" runat="server" Text=""></asp:Label>
                                        </p>

                                        <p class="certificate-content-note-p">As per school records and to the best of our knowledge he/she bears a good moral character.</p>
                                    </div>

                                  
                                     <div class="certificate-footer-sec-new" style="padding: 0px 0px; bottom: 15px;" id="bydefult" runat="server">
                                            <p class="certificate-footer-pt-lft">
                                                Signature of Class Teacher 
                                            </p>
                                            <p class="certificate-footer-pt-cntr">Office Incharge</p>
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
