<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="slc008.aspx.cs" Inherits="school_web.Admin.slip.TC.slc008" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print SLC</title>
    <link href="css/slc008.css" rel="stylesheet" />
    
     <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/slc008.css" rel="stylesheet" type="text/css" />');
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
                            <div class="certificate-wpr1-t">
                                <asp:Image ID="Image3" runat="server" class="watermarklogos"  style="display:none"/>
                                <div class="certificate-wpr2">
                                    <div class="t-crtifcate-hdr-sec" id="header_txt" runat="server" visible="false">
                                        <div class="t-crtifcate-logo-sec">
                                            <asp:Image ID="Image1" runat="server" />
                                        </div>
                                        <div class="t-crtifcate-hdr-contnt-sec">
                                            <asp:Label ID="lbl_school_name" class="t-certificate-comp-name-h" runat="server" Text=""></asp:Label>
                                            <p class="crtifcate-dvder3-aff-nos" runat="server" id="cbseAffDV">
                                                <asp:Label ID="lbl_cbse_aff" runat="server"></asp:Label>
                                            </p>

                                            <asp:Label ID="lbl_address" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                            <asp:Label ID="lbl_contact_no" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                            <p class="t-certificate-comp-mail-p">
                                                Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                            </p>


                                        </div>
                                    </div>
                                    <div class="t-crtifcate-hdr-sec" style="text-align: center" id="header_img" runat="server" visible="false">
                                        <asp:Image ID="img_header" runat="server" style="width: 800px;" />
                                    </div>


                                    <div class="certificate-type-name-t-sec">
                                        <h1 class="t-certificate-type-name-h">School Leaving Certificate</h1>
                                    </div>

                                    <div class="certificate-content-t-sec">
                                        <div class="crtifcate-dvder3">
                                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 50%">
                                                <p class="crtifcate-dvder3-p-rght" style="width: 100%; line-height: 30px; text-align: left;">
                                                    SL. No. :
                                                <asp:Label ID="lbl_certificate_no" runat="server"></asp:Label>
                                                </p>
                                                <p class="crtifcate-dvder3-p-rght" style="display: none; margin: 4px 0px 0px 0px; width: 100%; text-align: left; line-height: 30px;" id="udise_no" runat="server" visible="false">
                                                    UDISE :
                                                <asp:Label ID="lbl_udise_no" runat="server"></asp:Label>
                                                </p>
                                            </div>

                                            <div style="margin: 0px; padding: 0px; float: right; height: auto; width: 50%">
                                                <p class="crtifcate-dvder3-p-cntr" runat="server" style="width: 100%; float: right;   line-height: 30px; text-align: right;font-size: 19px;">
                                                    Admission No. :
                                                <asp:Label ID="lbl_admission_no" runat="server"></asp:Label>


                                                </p>
                                                <p class="crtifcate-dvder3-p-cntr" id="pin_no" runat="server" visible="false" style="width: 100%; float: right; line-height: 30px; text-align: right; font-size: 19px;">
                                                    Student P.E.N. No. :
                                                <asp:Label ID="lbl_pin_no" runat="server"></asp:Label>


                                                </p>


                                            </div>
                                        </div>

                                        <div class="crtifcate-contnt-t-wpr">
                                            <div class="t-certificate-img-dv-cnt-wpr">

                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>1.</span> Student Name<i>:</i></p>
                                                    <asp:Label ID="lbl_student_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                               <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>2.</span>Mother's Name<i>:</i></p>
                                                    <asp:Label ID="lbl_mother_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                                <div class="crtifcate-contnt-t-row">
                                                    <p class="crtifcate-contnt-t-p-lft dec-width"><span>3.</span>Father's/Gurdian's Name<i>:</i></p>
                                                    <asp:Label ID="lbl_father_name" runat="server" class="crtifcate-contnt-t-p-rght dec-width1"></asp:Label>
                                                </div>
                                                

                                            </div>
                                            <div class="t-certificate-img-dv">
                                                <asp:Image ID="Image2" runat="server" />
                                            </div>
                                            
                                                <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>4.</span>Date of birth(in christion Era)(In figures)<i>:</i></p>
                                                <asp:Label ID="lbl_dob" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                             <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft pl_1"><span> </span>According to Admission and Withdrawal Register)(in words<i>:</i></p>
                                                <asp:Label ID="lbl_dob_word" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>5.</span>Nationality<i>:</i></p>
                                                <asp:Label ID="lbl_nationality" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>6.</span>Whether the candidate belongs to schedule caste or <span style="width: 100%;
    padding: 0px 0px 0px 35px;">schedule trible or OBC</span><i>:</i></p>
                                                <asp:Label ID="lbl_belongs_to_sc_st" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>7.</span>Date of first admission in the school with Class<i>:</i></p>
                                                <asp:Label ID="lbl_date_of_admision" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                          
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>8.</span>Class in which the pupil last studied<i>:</i></p>
                                                <asp:Label ID="lbl_class_in_last_studied" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>9.</span>School/Board Annual examination last taken with <span style="width: 100%;
    padding: 0px 0px 0px 35px;"> result</span> <i>:</i></p>
                                                 <asp:Label ID="lbl_school_board_exam_taken" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            
                                             <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>10.</span>Whether failed, if so once/twice in the same class<i>:</i></p>
                                                <asp:Label ID="lbl_failed_in_same_class" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>11.</span>Subject Studied <i>:</i></p>
                                                <asp:Label ID="lbl_compalsory_subject" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                             <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>12.</span>Whether qualified for promotion to the higher class <i>:</i></p>
                                                <asp:Label ID="lbl_whether_qualified_for_promotion" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                               <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft pl_1"><span> </span>if so, to which class<i>:</i></p>
                                                <asp:Label ID="lbl_promot_class" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">13.</span>Month Upto which the pupil has paid school<i>:</i></p>
                                                <asp:Label ID="lbl_monthuptopaid" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>14.</span>Any fee concession availed of. if so, the nature of <span style="width: 100%;
    padding: 0px 0px 0px 35px;">such concession</span>  <i>:</i></p>
                                                <asp:Label ID="lbl_concession" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>15.</span>Total working days in the academic session<i>:</i></p>
                                                <asp:Label ID="lbl_ttl_no_of_working" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>16.</span>Total no. of days pupil present in the school<i>:</i></p>
                                                <asp:Label ID="lbl_ttl_no_of_working_present" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>

                                                <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>17.</span>Whether NCC Cadet/Boy Scout/Girl Guide<span style="width: 100%;
    padding: 0px 0px 0px 35px;"> (Details may be given)</span><i>:</i></p>
                                                <asp:Label ID="lbl_ncc_cadet" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span style="height: 40px;">18.</span>Games played or extra curricular activites in which the pupil usually took part( mention achivement level therein)<i>:</i></p>
                                                <asp:Label ID="lbl_game_played_or_extra" runat="server" class="crtifcate-contnt-t-p-rght"></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>19.</span>General conduct<i>:</i></p>
                                                <asp:Label ID="lbl_general_conduct" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>

                                             
                                         
                                           
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>20.</span>Date of application for certificate<i>:</i></p>
                                                <asp:Label ID="lbl_date_of_application" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>21.</span>Date of issue for certificate<i>:</i></p>
                                                <asp:Label ID="lbl_date_of_issue" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>22.</span>Reason for leaving the school<i>:</i></p>
                                                <asp:Label ID="lbl_reason_for_leaving" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                            <div class="crtifcate-contnt-t-row">
                                                <p class="crtifcate-contnt-t-p-lft"><span>23.</span>Any remarks<i>:</i></p>
                                                <asp:Label ID="lbl_any_other_remark" runat="server" class="crtifcate-contnt-t-p-rght" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="certificate-footer-t-sec signbtm" id="bydefult" runat="server">
                                        <p class="certificate-footer-pt-lft">
                                            Signature of <br />Class Teacher 
                                        </p>
                                        <p class="certificate-footer-pt-cntr">Checked by<br />(With full name and designation)</p>
                                        <p class="certificate-footer-pt-rght">Signature of Principal with date<br /> <span style="text-align: center;
    width: 119%;
    float: left;">School Seal</span> </p>
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
