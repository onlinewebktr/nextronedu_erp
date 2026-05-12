<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bonafide.aspx.cs" Inherits="school_web.Admin.slip.bonafide.bonafide" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="css/certificate010.css" rel="stylesheet" />
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/certificate010.css" rel="stylesheet" type="text/css" />');
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
                        <div class="chckbx-sec" id="midredio" runat="server">
                            <div class="chckbx-sec-inr">
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_both" onclick="myFunction('1')" runat="server" GroupName="aA" Text="Print in Letter Head" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_office_copy" onclick="myFunction('2')" runat="server" GroupName="aA" Text="Print in Normal Page" />
                                </div>
                            </div>
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec">
                    <div class="invoice-wpr" id="invoicewpr">
                        <div class="certificate-wpr" id="certificatewpr">
                            <div class="certificate-wpr1-t" id="certificatewpr1">
                                <asp:Image ID="Image3" runat="server" class="watermarklogos" Style="display: none" />
                                <div class="certificate-wpr2">
                                    <div class="school-header-imgs" id="contentHeader">
                                        <div id="contentHeaderDv">
                                            <div class="t-crtifcate-hdr-sec" id="header_txt" runat="server" visible="false">
                                                <div class="t-crtifcate-logo-sec">
                                                    <asp:Image ID="Image1" runat="server" />
                                                </div>
                                                <div class="t-crtifcate-hdr-contnt-sec">
                                                    <asp:Label ID="lbl_school_name" class="t-certificate-comp-name-h" runat="server" Text=""></asp:Label>

                                                    <asp:Label ID="lbl_address" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                                    <p class="crtifcate-dvder3-aff-nos" runat="server" id="cbseAffDV">
                                                        <asp:Label ID="lbl_cbse_aff" runat="server"></asp:Label>
                                                    </p>
                                                    <asp:Label ID="lbl_contact_no" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                                    <p class="t-certificate-comp-mail-p">
                                                        Email : 
                                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="school-header-imgs" id="imageHeader">
                                        <div id="header_imgDV">
                                            <div class="t-crtifcate-hdr-sec" style="text-align: center" id="header_img" runat="server" visible="false">
                                                <asp:Image ID="img_header" runat="server" />
                                            </div>
                                        </div>
                                    </div>





                                    <div class="certificate-content-t-sec">
                                        <div class="crtifcate-dvder3">
                                            <p class="crtifcate-dvder3-p-rght txtlft">
                                                Certificate No. :
                                                <asp:Label ID="lbl_certificate_no" runat="server"></asp:Label>
                                            </p>
                                            <p class="crtifcate-dvder3-p-cntr txtrght">
                                                Issue Date :
                                                <asp:Label ID="lbl_issue_date" runat="server"></asp:Label>
                                            </p>
                                        </div>

                                        <div class="certificate-type-name-t-sec">
                                            <h1 class="t-certificate-type-name-h">To Whom So Ever It May Concern</h1>

                                            <div class="report-card-rght-dv">
                                                <asp:Image ID="img_student_img" runat="server" />
                                            </div>
                                        </div>

                                        <div class="dob-content-sec paddlftsrght">
                                            <p class="dob-content-p">
                                                This is to certify that Master/Miss 
                                                <asp:Label ID="lbl_std_name" runat="server"></asp:Label>
                                                Son/Daughter of 
                                                <asp:Label ID="lbl_father_name" runat="server"></asp:Label>
                                                resident of 
                                                <asp:Label ID="lbl_std_address" runat="server"></asp:Label>
                                                is a regular student of grade 
                                                <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                                in academic session 
                                                <asp:Label ID="lbl_session" runat="server"></asp:Label>
                                                in our school, his/her Admission No. is 
                                                 <asp:Label ID="lbl_adm_no" runat="server"></asp:Label>
                                                <br />
                                                <br />

                                                As per admission withdrawal register of the school his/her date of birth is 
                                                <asp:Label ID="lbl_dob" runat="server"></asp:Label>
                                            </p>


                                            <p class="signofPrincipal">Signature of Principal</p>
                                        </div>
                                    </div>

                                    <div class="certificate-footer-t-sec signbtm" id="bydefult" runat="server"  style="display:none">
                                        <p class="certificate-footer-pt-lft">
                                            Prepared By(Name & Designation)
                                        </p>
                                        <p class="certificate-footer-pt-cntr">Checked by(Name & Designation)</p>
                                        <p class="certificate-footer-pt-rght">Signature of Principal with date</p>
                                    </div>
                                    <div class="sig-dv" id="Sig_setting" runat="server" style="display:none">
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
                                        <div class="sig-left" runat="server" id="Position3" visible="false" style="float: right;">
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


        <asp:HiddenField ID="hd_print_type" Value="1" runat="server" />
        <asp:HiddenField ID="hd_header_type" runat="server" />
        <script type="text/javascript">
            $(document).ready(function () {
                var headerType = $('#<%= hd_header_type.ClientID %>').val();
                var PrintType = $('#<%= hd_print_type.ClientID %>').val();
                if (PrintType == "1") {
                    if (headerType == "Img") {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.className += " hidden";
                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.className += " headerfixHeight";

                        //=========================================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.className += " hidden";
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.className += " hidden";
                    }
                    else {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.className += " hidden";
                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.className += " hidden";

                        //=======================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.className += " hidden";
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.className += " headerfixHeight";
                    }

                    var header_imgDV = document.getElementById("invoicewpr");
                    header_imgDV.className += " noborder";
                    var certificatewpr = document.getElementById("certificatewpr");
                    certificatewpr.className += " noborder";
                    var certificatewpr1 = document.getElementById("certificatewpr1");
                    certificatewpr1.className += " noborder";
                }
                else {
                    if (headerType == "Img") {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.classList.remove("hidden");

                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.classList.remove("headerfixHeight");

                        //=========================================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.className += " hidden";
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.className += " hidden";
                    }
                    else {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.className += " hidden";
                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.className += " hidden";

                        //=======================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.classList.remove("hidden");
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.classList.remove("headerfixHeight");
                    }
                    var header_imgDV = document.getElementById("invoicewpr");
                    header_imgDV.classList.remove("noborder");
                    var certificatewpr = document.getElementById("certificatewpr");
                    certificatewpr.classList.remove("noborder");
                    var certificatewpr1 = document.getElementById("certificatewpr1");
                    certificatewpr1.classList.remove("noborder");
                }
            });


            //<div class="invoice-wpr" id="invoicewpr">
            //    <div class="certificate-wpr" id="certificatewpr">
            //        <div class="certificate-wpr1-t" id="certificatewpr1">


            function myFunction(PrintType) {
                var headerType = $('#<%= hd_header_type.ClientID %>').val();
                if (PrintType == "1") {
                    if (headerType == "Img") {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.className += " hidden";
                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.className += " headerfixHeight";

                        //=========================================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.className += " hidden";
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.className += " hidden";
                    }
                    else {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.className += " hidden";
                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.className += " hidden";

                        //=======================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.className += " hidden";
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.className += " headerfixHeight";
                    }
                    var header_imgDV = document.getElementById("invoicewpr");
                    header_imgDV.className += " noborder";
                    var certificatewpr = document.getElementById("certificatewpr");
                    certificatewpr.className += " noborder";
                    var certificatewpr1 = document.getElementById("certificatewpr1");
                    certificatewpr1.className += " noborder";
                }
                else {
                    if (headerType == "Img") {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.classList.remove("hidden");

                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.classList.remove("headerfixHeight");

                        //=========================================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.className += " hidden";
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.className += " hidden";
                    }
                    else {
                        var header_imgDV = document.getElementById("header_imgDV");
                        header_imgDV.className += " hidden";
                        var imageHeader = document.getElementById("imageHeader");
                        imageHeader.className += " hidden";

                        //=======================
                        var contentHeaderDv = document.getElementById("contentHeaderDv");
                        contentHeaderDv.classList.remove("hidden");
                        var contentHeader = document.getElementById("contentHeader");
                        contentHeader.classList.remove("headerfixHeight");
                    }
                    var header_imgDV = document.getElementById("invoicewpr");
                    header_imgDV.classList.remove("noborder");
                    var certificatewpr = document.getElementById("certificatewpr");
                    certificatewpr.classList.remove("noborder");
                    var certificatewpr1 = document.getElementById("certificatewpr1");
                    certificatewpr1.classList.remove("noborder");
                }
            }
        </script>
    </form>
</body>
</html>
