<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leaving-certificate.aspx.cs" Inherits="school_web.Admin.slip.leaving_certificate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Invoice</title>
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
                                        <p class="certificate-no-p">
                                            Certificate No. :
                                            <asp:Label ID="lbl_crtificate_no" runat="server" Text="DPS/FKK/00213?CC"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="certificate-no-sec">
                                        <p class="certificate-adm-no-p">
                                            Admission No. : 
                                            <asp:Label ID="lbl_adm_no" runat="server" Text="5984"></asp:Label>
                                        </p>
                                    </div>


                                    <div class="certificate-logo-sec">
                                        <asp:Image ID="Image1" runat="server" />
                                        <%--<img src="../../assets/images/logo-img.png" />--%>
                                    </div>
                                    <asp:Label ID="lbl_school_name" class="certificate-comp-name-h" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" class="certificate-comp-add-p"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" Text="" class="certificate-comp-add-p"></asp:Label>
                                    <p class="certificate-comp-mail-p">
                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                    </p>
                                    <div class="certificate-type-name-sec">
                                        <h1 class="certificate-type-name-h">School Leaving Certificate</h1>

                                    </div>
                                    <h1 class="certificate-type-name-h1">To Whomsoever It may concern</h1>
                                    <div class="certificate-content-sec">
                                        <p class="certificate-content-p">
                                            This is to certify that Master/Miss 
                                             <asp:Label ID="lbl_std_name" runat="server"></asp:Label>
                                            Admission No. 
                                             <asp:Label ID="lbl_adm_no1" runat="server"></asp:Label>
                                            son / daughter of Mr. 
                                             <asp:Label ID="lbl_fther_name" runat="server"></asp:Label>
                                            was a student of this school. He/She appeared / passed the All INDIA SENIOR SCHOOL CERTIFICATE Examination
                                             (<asp:Label ID="lbl_class" runat="server"></asp:Label>)
                                            conducted by the Central Board of Secondary Education in 
                                             <asp:Label ID="llb_session" runat="server"></asp:Label>
                                        </p>
                                    </div>

                                    <div class="certificate-footer-secs">
                                        <p class="certificate-footer-p-lft">
                                            Date :
                                            <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                        </p>
                                        <p class="certificate-footer-p-cntr">Office Incharge</p>
                                        <p class="certificate-footer-p-rght">Principal</p>
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
