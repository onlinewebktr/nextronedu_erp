<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="income-certificate.aspx.cs" Inherits="school_web.Admin.slip.income_certificate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bonafide Certificate</title>
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
                        <div class="dobs-invoice-wpr">
                            <asp:Image ID="img_watermark" runat="server" class="wtr-mrk-img" />
                            <div class="dob-wpr">
                                <div class="income-dob-wpr1">
                                    <div class="certificate-wpr2"> 
                                        <div class="certificate-no-sec" id="affliation_no" runat="server" visible="false" style="display:none">
                                            <p class="certificate-adm-no-p"> 
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

                                            <div class="report-card-rght-dv" style="display: none;">
                                                <asp:Image ID="img_student_img" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                            </div>

                                            <div class="income-footer-sec" style="margin: 18px 0px 0px 0px; text-align: right;position: unset;">
                                            <p class="issue-dates" style="float: left;text-align: left;">
                                                Certificate No. :
                                                <asp:Label ID="lbl_crtificate_no" runat="server" Text=""></asp:Label>
                                            </p>
                                            <p class="issue-dates" style="float: right;">
                                                Issue Date :
                                                <asp:Label ID="lbl_issue_date" runat="server" Text=""></asp:Label>
                                            </p> 
                                        </div>

                                            <div class="dob-type-name-sec" id="head1Dv" runat="server" style="padding: 0px 0px 40px 0px;">
                                                <h1 class="dob-type-name-h" id="head1" runat="server">Bonafide Certificate</h1>
                                            </div>
                                        </div>

                                         

                                        


                                        <h2 class="dob-to-whome-h dob-to-whome-h-mctm" id="head2" runat="server">To Whom this may Concern</h2>



                                        <asp:Panel ID="pnl_content1" runat="server">
                                            <div class="dob-content-sec paddlftsrght">
                                                <p class="dob-content-p">
                                                    This is to certify that Master/Miss
                                                    <asp:Label ID="lbl_std_name" runat="server" Text=""></asp:Label>
                                                    Son/Daughter of
                                                    <asp:Label ID="lbl_fther_name" runat="server" Text=""></asp:Label>
                                                    resident of 
                                                    <asp:Label ID="lbl_sttd_address" runat="server" Text=""></asp:Label>
                                                    is a regular student of grade
                                                    <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>

                                                    in academic session
                                                    <asp:Label ID="lbl_session" runat="server"></asp:Label>
                                                    in our school,
                                                    his/her Admission No. is
                                                    <asp:Label ID="lbl_admission_no" runat="server" Text=""></asp:Label>
                                                    <br />
                                                    <br />
                                                    As per admission withdrawal register of the school his/her date of birth is 
                                                    <asp:Label ID="lbl_date_of_birth" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_dob_in_word" runat="server"></asp:Label>
                                                    <br />
                                                    <br />
                                                    We wish all success in her/his future.
                                                </p>
                                                <asp:Panel ID="pnl_fee_details" runat="server" Visible="false">
                                                    <p class="dob-content-note-p">
                                                        His/Her tuition fee according to our school register for the session
                                            <asp:Label ID="lbl_std_session" runat="server" Text=""></asp:Label>
                                                        is under (year ending
                                                <asp:Label ID="lbl_month_name" runat="server" Text=""></asp:Label>)
                                                tuition fee of
                                                <asp:Label ID="lbl_std_name1" runat="server" Text=""></asp:Label>
                                                    </p>


                                                    <p class="dob-content-note-p">
                                                        Tuition Fee :
                                                <asp:Label ID="lbl_tuition_fee" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p class="dob-content-note-p" runat="server" id="hostelFeeDV">
                                                        Hostel Fee :
                                                <asp:Label ID="lbl_hostel_fee" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p class="dob-content-note-p" runat="server" id="transporTFeeDV">
                                                        Transport Fee :
                                                <asp:Label ID="lbl_transport_fee" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p class="dob-content-note-p" id="trmFeeDV" runat="server">
                                                        Term Fee :
                                                <asp:Label ID="lbl_term_fee" runat="server" Text=""></asp:Label>
                                                    </p>

                                                    <p class="dob-content-note-p">
                                                        The total amount paid during the current financial year is thus
                                                <asp:Label ID="lbl_final_amt" runat="server" Text=""></asp:Label>
                                                        (<asp:Label ID="lbl_in_word" runat="server" Text=""></asp:Label>)
                                                    </p>
                                                </asp:Panel>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnl_content2" runat="server" Visible="false">
                                            <div class="dob-content-sec paddlftsrght">
                                                <p class="dob-content-p">
                                                    This is to certify that Master/Miss
                                            <asp:Label ID="lbl_std_name2" runat="server"></asp:Label>
                                                    Son/Daughter of
                                            <asp:Label ID="lbl_fther_name2" runat="server"></asp:Label>

                                                    resident of
                                             <asp:Label ID="lbl_sttd_address2" runat="server"></asp:Label>
                                                    He/She is a regular student of grade
                                            <asp:Label ID="lbl_class2" runat="server"></asp:Label>
                                                    in academic session
                                                <asp:Label ID="lbl_session2" runat="server"></asp:Label>
                                                    in our school, his/her Admission No. is
                                                <asp:Label ID="lbl_admission_no2" runat="server"></asp:Label>.
                                                </p>
                                                <p class="dob-content-note-p">
                                                    He/She has paid Rs.
                                                    <span style="font-weight: 700">
                                                        <asp:Label ID="lbl_final_amt2" runat="server"></asp:Label>/-</span>
                                                    <span style="font-weight: 700">(<asp:Label ID="lbl_in_word2" runat="server"></asp:Label>
                                                        Only)</span>
                                                    for the period from  
                                                    <asp:Label ID="lbl_period_from" runat="server"></asp:Label>
                                                    to
                                                    <asp:Label ID="lbl_month_name2" runat="server"></asp:Label>. 
                                                </p>

                                            </div>
                                        </asp:Panel>
                                        <%--<div class="income-footer-sec"> 
                                            <p class="income-footer-p-rght income-footer-p-rghtcstm">PRINCIPAL</p>
                                        </div>--%>

                                        <div class="certificate-footer-sec-new" style="padding: 0px 0px; bottom: 250px; right: 50px;" id="bydefult" runat="server">
                                            <%-- <p class="certificate-footer-pt-lft">
                                                Signature of Class Teacher 
                                            </p>
                                            <p class="certificate-footer-pt-cntr">Office Incharge</p>--%>
                                            <p class="certificate-footer-pt-rght">Signature of Principal</p>
                                        </div>

                                        <div class="sig-dv" id="Sig_setting" runat="server">
                                            <div class="sig-left" runat="server" id="Position1" visible="false" style="float: left">
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
                                            <div class="sig-left" runat="server" id="Position3" visible="false" style="float: right">
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
