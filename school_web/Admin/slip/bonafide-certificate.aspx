<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bonafide-certificate.aspx.cs" Inherits="school_web.Admin.slip.bonafide_certificate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Invoice</title>
    <link href="css/bonafied.css" rel="stylesheet" />


    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/bonafied.css" rel="stylesheet" type="text/css" />');
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
                                            <asp:Label ID="lbl_crtificate_no" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                    <%--<div class="certificate-no-sec">
                                        <p class="certificate-adm-no-p">
                                            Admission No. : 
                                            <asp:Label ID="lbl_adm_no" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>--%>


                                    <div class="certificate-logo-sec">
                                        <asp:Image ID="Image1" runat="server" />
                                        <%--<img src="../../assets/images/logo-img.png" />--%>
                                    </div>
                                    <asp:Label ID="lbl_school_name" class="certificate-comp-name-h" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" Text="" class="certificate-comp-add-p"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" Text="" class="certificate-comp-add-p"></asp:Label>
                                    <p class="certificate-comp-mail-p">
                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                    </p>
                                    <div class="certificate-type-name-sec">
                                        <h1 class="certificate-type-name-h">Bonafide Certificate</h1>
                                    </div>

                                        <div class="certificate-content-sec">
                                        <p class="certificate-content-p">
                                            This is to certify that
                                            <asp:Label ID="lbl_student_name_c" runat="server"></asp:Label>, son/daughter of
                                            <asp:Label ID="lbl_guardian_name" runat="server"></asp:Label>, is a bona fide student of
                                            <asp:Label ID="lbl_class_grade" runat="server"></asp:Label> 
                                            in
                                            <asp:Label ID="lbl_class_c" runat="server" Text="Label"></asp:Label>  
                                            at Mount Sinai Mission School during the academic year
                                            <asp:Label ID="lbl_year" runat="server"></asp:Label>  
                                        </p>


                                        <p class="certificate-content-p" style="    padding: 0px 0px 5px 0px;">
                                            Student's Date of Birth : 
                                            <asp:Label ID="lbl_dob_c" runat="server"></asp:Label>   
                                        </p>

                                        <p class="certificate-content-p">
                                            Admission Number :
                                            <asp:Label ID="lbl_admission_no_c" runat="server"></asp:Label>    
                                        </p>


                                        <p class="certificate-content-p">
                                            <asp:Label ID="lbl_student_name_c1" runat="server"></asp:Label>   
                                            has been a student of this institution since
                                            <asp:Label ID="lbl_date_of_admission_c" runat="server"></asp:Label>, and during this period, [he/she/they] has/have shown
                                            <asp:Label ID="lbl_remark" runat="server"></asp:Label>.
                                        </p> 
                                    </div>

                                    <div class="certificate-footer-sec">
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
