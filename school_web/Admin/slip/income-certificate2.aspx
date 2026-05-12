<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="income-certificate2.aspx.cs" Inherits="school_web.Admin.slip.income_certificate2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bonafide Certificate</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />

    <link href="css/certificate2.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="css/certificate2.css" rel="stylesheet" type="text/css" />');
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
                        <div class="dobs-invoice-wpr">
                            <asp:Image ID="img_watermark" runat="server" class="wtr-mrk-img" />
                            <div class="dob-wpr">
                                <div class="income-dob-wpr1">
                                    <div class="certificate-wpr2">
                                        <div class="certificate-no-sec">
                                            <p class="certificate-no-p">
                                                Certificate No. :
                                            <asp:Label ID="lbl_crtificate_no" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="certificate-no-sec" runat="server" visible="false" id="affliation_no">
                                            <p class="certificate-adm-no-p">
                                                Affiliation No. : 
                                            <asp:Label ID="lbl_affliation_no" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="report-card-head">
                                            <div class="t-crtifcate-hdr-sec" id="header_txt" runat="server" visible="false">
                                                <div class="report-card-left-dv">
                                                    <asp:Image ID="Image1" runat="server" />
                                                </div>
                                                <div class="report-card-cntr-dv">
                                                    <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add"></asp:Label>
                                                    <asp:Label ID="lbl_contact_no" runat="server" class="report-card-schl-cont"></asp:Label>
                                                    <p class="report-card-schl-emil" style="display: none">
                                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                                    </p>
                                                    <p class="report-card-schl-emil v-false {{reportCardSubS[0].Is_website_show}}">
                                                        Website : 
                                        <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                    </p>

                                                </div>
                                            </div>
                                            <div class="t-crtifcate-hdr-sec" style="text-align: center; width: 100%;" id="header_img" runat="server" visible="false">
                                                <asp:Image ID="img_header" runat="server" />
                                            </div>

                                            <div class="report-card-rght-dv" style="display: none">
                                                <asp:Image ID="img_student_img" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                            </div>
                                        </div>
                                         <div class="income-footer-sec" style="margin: 18px 0px 0px 0px;text-align: right;">
                                            <p class="issue-dates" style="float: right;">
                                                Issue Date :
                                                <asp:Label ID="lbl_issue_date" runat="server" Text=""></asp:Label>
                                            </p>
                                            <%-- <p class="certificate-footer-p-lft">
                                            Date :
                                            <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                        </p>
                                        <p class="certificate-footer-p-cntr">Office Incharge</p>--%>
                                        </div>
                                        <h2 class="dob-to-whome-h">To Whom so ever it may Concern</h2>

                                        <div class="dob-content-sec">
                                            <asp:Panel ID="pnl_type_zero" runat="server" Visible="false">
                                                <p class="dob-content-p">
                                                    This is to certify that Mr./Miss.
                                            <asp:Label ID="lbl_std_name" runat="server" Text=""></asp:Label>
                                                    Son / Daughter of Mr.
                                            <asp:Label ID="lbl_fther_name" runat="server" Text=""></asp:Label>
                                                    and Mrs. 
                                                <asp:Label ID="lbl_mother_name" runat="server" Text=""></asp:Label>
                                                    is a Bonafide student of the above said institution studying in standard 
                                            <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>.
                                                His admission no. is  
                                                <asp:Label ID="lbl_admission_no" runat="server" Text=""></asp:Label>
                                                    and his date of birth is 
                                                <asp:Label ID="lbl_date_of_birth" runat="server" Text=""></asp:Label>
                                                    for the academic session
                                                <asp:Label ID="lbl_session" runat="server" Text=""></asp:Label>
                                                </p>
                                            </asp:Panel>


                                            <asp:Panel ID="pnl_fee_details" runat="server" Visible="false">
                                                <p class="dob-content-p">
                                                    This is to certify that Mr./Miss.
                                            <asp:Label ID="lbl_std_name11" runat="server" Text=""></asp:Label>
                                                    Son / Daughter of Mr.
                                            <asp:Label ID="lbl_fther_name1" runat="server" Text=""></asp:Label>
                                                    and Mrs. 
                                                <asp:Label ID="lbl_mother_name1" runat="server" Text=""></asp:Label>
                                                    is a student of Class
                                            <asp:Label ID="lbl_class1" runat="server" Text=""></asp:Label>.
                                                He/She has paid tuition fees of Rs. 
                                                    <asp:Label ID="lbl_tuition_fee" runat="server" Text=""></asp:Label>/-
                                                    <br />
                                                    <br />

                                                    (<asp:Label ID="lbl_in_word" runat="server" Text=""></asp:Label>) for the academic session 
                                                    <asp:Label ID="lbl_month_name" runat="server" Text=""></asp:Label>

                                                </p>
                                            </asp:Panel>
                                        </div>

                                       

                                        <div class="certificate-footer-sec-new" style="padding: 0px 0px; bottom: 15px;" id="bydefult" runat="server">
                                           <%-- <p class="certificate-footer-pt-lft">
                                                Signature of Class Teacher 
                                            </p>
                                            <p class="certificate-footer-pt-cntr">Office Incharge</p>--%>
                                            <p class="certificate-footer-pt-rght">Signature of Principal</p>
                                        </div>

                                         <div class="sig-dv" id="Sig_setting" runat="server">
                                            <div class="sig-left" runat="server" id="Position1" visible="false" style="float:left">
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
                                            <div class="sig-left" runat="server" id="Position3" visible="false" style="float:right">
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
        </div>
    </form>
</body>
</html>
