<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leaving-certificate-010.aspx.cs" Inherits="school_web.Admin.slip.TC.leaving_certificate_010" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="css/slc010.css" rel="stylesheet" /> 
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/slc010.css" rel="stylesheet" type="text/css" />');
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




                                    <div class="certificate-type-name-t-sec">
                                        <h1 class="t-certificate-type-name-h">School Leaving Certificate</h1>
                                    </div>
                                    <div class="certificate-content-t-sec">
                                        <div class="crtifcate-dvder3">
                                            <p class="crtifcate-dvder3-p-cntr">
                                                Admission No. :
                                                <asp:Label ID="lbl_school_no" runat="server" Style="display: none"></asp:Label>
                                                <asp:Label ID="lbl_admission_no" runat="server"></asp:Label>
                                            </p>
                                            <p class="crtifcate-dvder3-p-rght">
                                                Certificate No. :
                                                <asp:Label ID="lbl_certificate_no" runat="server"></asp:Label>
                                            </p>
                                        </div>

                                        <div class="crtifcate-contnt-t-wpr">
                                            <div class="t-certificate-img-dv-cnt-wpr">
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>1.</span> Name of the Pupil<i>:</i></p>
                                                    <asp:Label ID="lbl_student_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>2.</span>Mother's Name<i>:</i></p>
                                                    <asp:Label ID="lbl_mother_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>3.</span>Father's Name<i>:</i></p>
                                                    <asp:Label ID="lbl_father_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span style="height: 39px;">4.</span>Date of Birth (in Christian Era) according to Admission & Withdrawal Register(in figures)<i>:</i></p>
                                                    <asp:Label ID="lbl_dob" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="t-certificate-img-dv" style="display: none">
                                                <asp:Image ID="Image2" runat="server" />
                                            </div>


                                            <%--<div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>4.</span>Date of Birth (in Christian Era) according to Admission & Withdrawal Register(in figures)<i>:</i></p>
                                                <asp:Label ID="" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>--%>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft" style="text-align: right; padding: 0px 20px 0px 0px;">(In Words)<i>:</i></p>
                                                <asp:Label ID="lbl_Dob_in_word" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>5.</span>Nationality<i>:</i></p>
                                                <asp:Label ID="lbl_nationality" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>6.</span>Wherher the candidate belongs to SC/ST/OBC<i>:</i></p>
                                                <asp:Label ID="lbl_belongs_to_sc_st" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>7.</span>Date of first admission in the School with class<i>:</i></p>
                                                <asp:Label ID="lbl_date_of_admision" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>8.</span>Class in which the Pupil last studied<i>:</i></p>
                                                <asp:Label ID="lbl_class_in_last_studied" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">9.</span>School / Board Annual Examination last taken with result<i>:</i></p>
                                                <asp:Label ID="lbl_school_board_exam_taken" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>10.</span>Whether failed, if so once/twice in the same class<i>:</i></p>
                                                <asp:Label ID="lbl_failed_in_same_class" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>11.</span>Subject studied<i>:</i></p>
                                                <asp:Label ID="lbl_compalsory_subject" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">12.</span>Whether qualified for promotion to the higher class if so, to which class<i>:</i></p>
                                                <asp:Label ID="lbl_qualified_for_promotion" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>13.</span>Month upto which the pupil has paid school dues<i>:</i></p>
                                                <asp:Label ID="lbl_paid_all_fees" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">14.</span>Any fee concession availed of, if so, the nature of such concession<i>:</i></p>
                                                <asp:Label ID="lbl_any_fee_concession_availed" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>15.</span>Total no of working days in the academic session<i>:</i></p>
                                                <asp:Label ID="lbl_ttl_no_of_working" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">16.</span>Total no of working days pupil present in the school<i>:</i></p>
                                                <asp:Label ID="lbl_ttl_no_of_working_present" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">17.</span>Whether NCC Cadet/Boy Scout/Girl Guide (details may be given)<i>:</i></p>
                                                <asp:Label ID="lbl_ncc_cadet" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft">
                                                    <span style="height: 55px;">18.</span>Games played or extra curricular activities in which the pupil usually took part
                                                    <br />
                                                    (mention achievement level therein)<i>:</i>
                                                </p>
                                                <asp:Label ID="lbl_game_played_or_extra" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>19.</span>General Conduct<i>:</i></p>
                                                <asp:Label ID="lbl_general_conduct" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>20.</span>Date of application for certificate<i>:</i></p>
                                                <asp:Label ID="lbl_date_of_application" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>21.</span>Date of issue of certificate<i>:</i></p>
                                                <asp:Label ID="lbl_date_of_issue" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>22.</span>Reason for leaving the school<i>:</i></p>
                                                <asp:Label ID="lbl_reason_for_leaving" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>23.</span>Any other remarks<i>:</i></p>
                                                <asp:Label ID="lbl_any_other_remark" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="certificate-footer-t-sec signbtm" id="bydefult" runat="server">
                                        <p class="certificate-footer-pt-lft">
                                            Prepared By(Name & Designation)
                                        </p>
                                        <p class="certificate-footer-pt-cntr">Checked by(Name & Designation)</p>
                                        <p class="certificate-footer-pt-rght">Signature of Principal with date</p>
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
