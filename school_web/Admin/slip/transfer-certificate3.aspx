<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="transfer-certificate3.aspx.cs" Inherits="school_web.Admin.slip.transfer_certificate3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>transfer certificate</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />


    <link href="certificate3.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="certificate3.css" rel="stylesheet" type="text/css" />');
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
    <style>
        .certificate-wpr1-t {
            margin: 0;
            padding: 0px 7px 7px !important;
            width: 100%;
            float: left;
            border: 2px dotted #878787;
            position: relative;
            align-items: center;
            display: flex;
        }

        .crtifcate-dvder3-p-lft {
        }
    </style>

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
                            <div class="certificate-wpr1-t">
                                <asp:Image ID="Image3" runat="server" class="watermarklogos" />
                                <div class="certificate-wprtcs">
                                    <%--<div class="t-crtifcate-hdr-sec">
                                        <div class="certificate3-logo-sec" style="margin: 7px 0px 0px 0px;">
                                            <asp:Image ID="Image1" runat="server" />

                                            <asp:Label ID="lbl_school_name" class="t-certificate-comp-name-h" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:Label ID="lbl_school_info" runat="server" Style="margin: 0px 0px 0px 0px;" class="certificate3-comp-add-p"></asp:Label>
                                            <asp:Label ID="lbl_address" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                            <asp:Label ID="lbl_contact_no" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                            <p class="t-certificate-comp-mail-p" style="margin: 0px 0px 0px 0px;">
                                                Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                            </p>

                                            <p class="certificate3-comp-mail-p" style="margin: 0px 0px 0px 0px;">
                                                Website : 
                                        <asp:Label ID="lbl_website" runat="server" Text=""></asp:Label>
                                            </p>

                                            <asp:Label ID="lbl_school_info_3" runat="server" Style="margin: 0px 0px 0px 0px;" class="certificate3-comp-add-p"></asp:Label>
                                        </div>
                                    </div>--%>


                                    <div class="report-card-head">

                                        <div class="t-crtifcate-hdr-sec" id="header_txt" runat="server" visible="false">
                                            <div class="report-card-left-dv">
                                                <asp:Image ID="Image1" runat="server" />
                                            </div>
                                            <div class="report-card-cntr-dv">
                                                <asp:Label ID="lbl_school_name" class="report-card-schlname schoolname" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_school_info" runat="server" Style="margin: 0px 0px 0px 0px;" class="certificate3-comp-add-p"></asp:Label>

                                                <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add font-size13"></asp:Label>
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
                                        <div class="t-crtifcate-hdr-sec" style="text-align: center;width: 85%;" id="header_img" runat="server" visible="false" >
                                            <asp:Image ID="img_header" runat="server" />
                                        </div>

                                        <div class="report-card-rght-dv">
                                            <asp:Image ID="img_student_img" style="margin: -8px 0px 0px 0px;" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                        </div>
                                        <asp:Label ID="lbl_school_info_3" runat="server" Style="margin: 0px 0px 0px 0px;" class="certificate3-comp-add-p"></asp:Label>
                                    </div>

                                    <div class="certificate-type-name-t-sec">
                                        <h1 class="t-certificate-type-name-h" style="text-transform: uppercase">स्थानांतरण प्रमाण-पत्र / Transfer Certificate</h1>
                                    </div>

                                    <div class="certificate-content-t-sec">
                                        <%-- <div class="crtifcate-dvder3">


                                            <p class="crtifcate-dvder3-p-lft">
                                                विद्यालय सं/School No. :
                                                <asp:Label ID="lbl_school_no" runat="server"></asp:Label>
                                            </p>
                                            <p class="crtifcate-dvder3-p-cntr">
                                                SL. No. :
                                                <asp:Label ID="lbl_certificate_no" runat="server"></asp:Label>
                                            </p>

                                            <p class="crtifcate-dvder3-p-rght">
                                                CBSE Affiliation No. :
                                                <asp:Label ID="lbl_cbse_aff" runat="server"></asp:Label>
                                            </p>
                                        </div>--%>

                                        <div class="crtifcate-dvder3">
                                            <p class="crtifcate-dvder3-p-lft">
                                                विद्यालय सं/School No. :
                                                <asp:Label ID="lbl_school_no" runat="server" CssClass="crtifcate-dvder3label"></asp:Label>
                                            </p>
                                            <p class="crtifcate-dvder3-p-cntr">
                                                पुस्तक नं./ Book No. 
                                                <asp:Label ID="lbl_lbl_book_no" runat="server" CssClass="crtifcate-dvder3label"></asp:Label>
                                            </p>
                                            <p class="crtifcate-dvder3-p-cntr">
                                                क्र. सं./T.C.No.-:
                                                <asp:Label ID="lbl_certificate_no" runat="server" CssClass="crtifcate-dvder3label"></asp:Label>
                                            </p>
                                        </div>

                                        <div class="crtifcate-dvder3">
                                            <p class="crtifcate-dvder3-p-admin">
                                                Adm No.-  :
                                                <asp:Label ID="lbl_admission_no" runat="server" CssClass="crtifcate-dvder3label"></asp:Label>
                                            </p>
                                            <p class="crtifcate-dvder3-p-cbseaffi">
                                                CBSE Affiliation No. :
                                                <asp:Label ID="lbl_cbse_aff" runat="server" CssClass="crtifcate-dvder3label"></asp:Label>
                                            </p>

                                            <p class="crtifcate-dvder3-p-cntr">
                                                Renewed upto :
                                                <asp:Label ID="lbl_Renewedupto" runat="server" CssClass="crtifcate-dvder3label"></asp:Label>
                                            </p>
                                        </div>

                                        <div class="crtifcate-dvder3">
                                            <p class="crtifcate-dvder3-p-schoolstatus">
                                                Status of School :
                                                <asp:Label ID="lbl_status_school" runat="server"></asp:Label>
                                            </p>
                                            <p class="crtifcate-dvder3-p-cntr">
                                            </p>

                                            <p class="crtifcate-dvder3-p-cntr">
                                            </p>
                                        </div>
                                        <div class="crtifcate-dvder3">
                                            <p class="crtifcate-dvder3-p-Registration_ix_xii">
                                                Registration No. of Candidate(In case Class IX to XII)
                                                <asp:Label ID="lbl_Registration_ix_xii" runat="server"></asp:Label>
                                            </p>
                                        </div>

                                        <div class="crtifcate-contnt-t-wpr">
                                            <div class="t-certificate-img-dv-cnt-wpr">
                                                <%-- <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>1.</span> Admission No.<i>:</i></p>
                                                    <asp:Label ID="lbl_admission_no1" class="crtifcate-contnt-t-p-rght dec-width1" runat="server"></asp:Label>
                                                </div>--%>
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>1.</span>विद्यार्थी का नाम/Name of the Pupil<i>:</i></p>
                                                    <asp:Label ID="lbl_student_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>2.</span>पिता का नाम/Father's Name<i>:</i></p>
                                                    <asp:Label ID="lbl_father_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>3.</span>माता का नाम/Mother's Name<i>:</i></p>
                                                    <asp:Label ID="lbl_mother_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>

                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>4.</span>राष्ट्रीयता/Nationality<i>:</i></p>
                                                    <asp:Label ID="lbl_Nationality" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>


                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width">
                                                        <span style="height: 38px;">5.</span>क्या अनु०जाति/ज०जा०/ पिछड़ा वर्ग से सम्बन्धित हैं/Whether 
                                                     the pupil belongs to SC/ST/OBC Category <i>:</i>
                                                    </p>
                                                    <asp:Label ID="lbl_stobc" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>

                                                <div class="t-certificate-img-dv" style="display: none">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/assets/images/add_subject.png" />
                                                </div>

                                            </div>




                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span style="height: 38px;">6.</span>प्रवेश पुस्तिका के अनुसार जन्म तिथि/Date of birth<br />
                                                    to the admission register(in figures)(in words)<i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_dob" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>7.</span>प्रस्तावित विषय/Subjects offered<i>:</i></p>
                                                <asp:Label ID="lbl_compalsory_subject" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span>8.</span>पिछली कक्षा जिसमें विद्यार्थी अध्ययनरत था (अंकों में)/
                                                    Class<br />
                                                    <span style="width: 72%; margin-left: 31px;">in which the pupil last studied (In Words)</span><i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_class_in_last_studied" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span style="height: 40px;">9.</span>पिछले विद्यालय/बोर्ड परीक्षा एवं परिणाम/School/Board
                                                    <br />
                                                    <span style="width: 72%; margin-left: 0px;">Annual examination last taken with result</span><i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_school_board_exam_taken" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">10.</span>क्या उच्च कक्षा में पदोन्नति का अधिकारी है? Whether qualified for promotion to the next higher class?<i>:</i></p>
                                                <asp:Label ID="lbl_qualified_for_promotion" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span>11.</span>क्या विद्यार्थी ने विद्यालय की सभी देव राशि का भुगतान कर दिया है?/<br />
                                                    <span style="width: 83%; margin-left: 31px;">Whether the pupil has paid all dues to the school?</span><i>:</i>

                                                </p>
                                                <asp:Label ID="lbl_dues_fees" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span style="height: 40px;">12.</span>क्या विद्यार्थी को कोई शुल्क रियायत प्रदान की गई थी, यदि हाँ, तो उसकी प्रकृति/Whether the pupil was in receipt of<br />
                                                    <span style="width: 83%; margin-left: 31px;">Any fee concession? If so, the nature of such concession
                                                    </span><i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_any_fee_concession_availed" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>13.</span>विद्यालय छोड़ने का कारण/Reason for leaving the school <i>:</i></p>
                                                <asp:Label ID="lbl_reason_for_leaving" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>


                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span>14.</span>क्या विद्यार्थी एन०सी०सी० कैडेट/स्काउट है? विवरण दें।Whether<br />
                                                    <span style="width: 83%; margin-left: 31px;">N.C.C. Cadet/ boy Scout /Girl Guide (Detail may Be given)</span><i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_ncc" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span>15.</span>अंतिम तिथि तक उपस्थितियों की कुल संख्या /No. of meetings<br />
                                                    <span style="width: 83%; margin-left: 31px;">up to date </span><i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_meetings" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span>16.</span>विद्यार्थी की विद्यालय दिवसों की कुल उपस्थितियाँ /Number of<br />
                                                    <span style="width: 83%; margin-left: 31px;">School-days the pupil attended</span> <i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_ttl_no_of_working" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>17.</span>सामान्य आचरण /General conduct<i>:</i></p>
                                                <asp:Label ID="lbl_general_conduct" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>


                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>18.</span>प्रमाण-पत्र जारी करने की तिथि Date of issue of certificate<i>:</i></p>
                                                <asp:Label ID="lbl_date_of_issue" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>









                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>19.</span>एसआरएन नंबर / SRN NO.<i>:</i></p>
                                                <asp:Label ID="lbl_srn_no" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>


                                            <div class="crtifcate-contnt-t-row" style="display: none">
                                                <p class="crtifcate-contnt-t-p-lft"><span>20.</span>Any other remarks<i>:</i></p>
                                                <asp:Label ID="lbl_any_other_remark" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="certificate-footer-sec-new" style="padding: 0px 0px; bottom: 65px;" id="bydefult" runat="server">
                                        <p class="certificate-footer-pt-lft" style="font-family: sans-serif;">
                                            तैयारकर्ता / Prepared By
                                        <br />
                                            (Name & Designation) 
                                        </p>
                                        <p class="certificate-footer-pt-cntr" style="font-family: sans-serif;">
                                            जांचकर्ता/Checked By<br />
                                            (Name & Designation)
                                        </p>
                                        <p class="certificate-footer-pt-rght" style="font-family: sans-serif;">
                                            <span style="margin-right: 61px">प्राचार्य/कार्यालय मोहर</span>
                                            <br />
                                            (Sign. of Principal with office seal)
                                        </p>
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
    </form>
</body>
</html>
