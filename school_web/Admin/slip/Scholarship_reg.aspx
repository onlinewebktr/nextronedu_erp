<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scholarship_reg.aspx.cs" Inherits="school_web.Admin.slip.Scholarship_reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scholarship Reg</title>
    <link href="reg.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="reg.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Open+Sans:100,200,300,400,500,600,700" rel="stylesheet" />');
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
        span {
            color: maroon !important;
            font-weight: bold;
        }

        span {
            color: #040404 !important;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="section">
            <div class="print_form">
                <div class="prnt-btn-sec">
                    <div class="prnt-btn-sec-cntr">
                        <div class="print-btn-sec">
                            <div class="noPrint" style="float: left">
                                <asp:Button ID="btn_back" runat="server" ToolTip="Back" CssClass="back-btn" OnClick="btn_back_Click" />
                            </div>
                            <div class="noPrint" style="float: right">
                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="print-btn"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="print-pg-sec" id="tblPrintIQ" runat="server">
                    <div class="print_form-inr">
                        <div class="t-crtifcate-hdr-sec">
                            <div class="t-crtifcate-logo-sec">
                                <asp:Image ID="Image1" runat="server" />
                            </div>
                            <div class="t-crtifcate-hdr-contnt-sec">
                                <asp:Label ID="lbl_school_name" class="t-certificate-comp-name-h" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_address" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_address_2" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                <asp:Label ID="lbl_contact_no" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                <p class="t-certificate-comp-mail-p">
                                    Email : 
                                        <asp:Label ID="lbl_email_school" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="certificate-type-name-t-sec">
                            <h1 class="t-certificate-type-name-h">Scholarship Registration Receipt -<asp:Label ID="lbl_session1" runat="server" Text=""></asp:Label></h1>
                        </div>


                        <div class="print_form-dtls">
                            <div class="bank-details" style="display: none">
                                <h2 class="bank-details-title">Bank Details For Payment</h2>
                                <div class="bank-details-1st-dv">
                                    <p class="bank-namep">Bank Name : PUNJAB NATIONAL BANK</p>
                                    <p class="bank-more-info-p">Account Holder Name : DELHI PUBLIC SCHOOL</p>
                                    <p class="bank-more-info-p">A/C No. : 1250050010048</p>
                                    <p class="bank-more-info-p">IFSC Code : PUNB0125020</p>
                                    <p class="bank-more-info-p">Branch  : CHOWKI</p>
                                </div>
                                <div class="bank-details-1st-dv bdrrght0">
                                    <p class="bank-namep">Bank Name : State Bank of India</p>
                                    <p class="bank-more-info-p">Account Holder Name : DELHI PUBLIC SCHOOL</p>
                                    <p class="bank-more-info-p">A/C No. : 10736589084</p>
                                    <p class="bank-more-info-p">IFSC Code : SBIN0007099</p>
                                    <p class="bank-more-info-p">Branch  : ANDUA</p>
                                </div>
                            </div>
                            <table class="print_table">
                                 <tr runat="server" id="branchSchoolDV">
                                    <td>Selected Branch :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_selected_branch" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Registration Id :</td>
                                    <td>
                                        <asp:Label ID="lbl_registrationid" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                    </td>


                                    <td rowspan="5" style="width: 130px; padding: 1px 1px;">
                                        <asp:Label ID="lbl_affix_photo" Visible="false" runat="server" Text="Affix Passport Size Photo" Style="text-align: center; width: 100%; float: left; color: #9b9b9b !important; font-weight: 400; font-size: 14px;"></asp:Label>
                                        <asp:Image ID="img_s_image" runat="server" class="studnt-img" />
                                        <asp:Image ID="img_s_sig" runat="server" class="studnt-sig" Style="display: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Gender :</td>
                                    <td>
                                        <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of birth :</td>
                                    <td>
                                        <asp:Label ID="lbl_dob" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Blood Group :</td>
                                    <td>
                                        <asp:Label ID="lbl_blood_group" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nationality :</td>
                                    <td>
                                        <asp:Label ID="lbl_nationality" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Religion :</td>
                                    <td>
                                        <asp:Label ID="lbl_religion" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Caste :</td>
                                    <td>
                                        <asp:Label ID="lbl_cast" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Category :</td>
                                    <td>
                                        <asp:Label ID="lbl_category" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>



                                <tr>
                                    <td>Name of last school :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_lastschool" runat="server" Text=""></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Height :</td>
                                    <td>
                                        <asp:Label ID="lbl_height" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Weight :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_weight" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>





                                <tr>
                                    <td>Session :</td>
                                    <td>
                                        <asp:Label ID="lbl_session" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Class :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr id="islunch" runat="server" visible="false">
                                    <td>Admission Type :</td>
                                    <td>

                                        <asp:Label ID="lbl_admissiontype" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Is Lunch</td>
                                    <td>

                                        <asp:Label ID="lbl_lunch" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Payment Info :</td>
                                </tr>

                                <tr>
                                    <td>Payment Mode</td>
                                    <td>
                                        <asp:Label ID="lbl_paymnet_mode" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                    </td>
                                    <td>Payment Status
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_Unpaid" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Payable amount</td>
                                    <td>
                                        <asp:Label ID="lbl_total" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                    </td>
                                    <td>Pyament Order Id
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_payment_order_id" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>




                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Father's Details :</td>
                                </tr>
                                <tr>
                                    <td>Father Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Occupation :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_occupation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Qualification :</td>
                                    <td>
                                        <asp:Label ID="lbl_qualification" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Designation :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_designation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mobile No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Email Id :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_email_id" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Annual Income :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_annual_income" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>




                                <%-- ============================ --%>

                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Mother's Details :</td>
                                </tr>
                                <tr>

                                    <td>Mother Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_mothername" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Occupation :</td>
                                    <td>
                                        <asp:Label ID="lbl_mon_occupation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Qualification :</td>
                                    <td>
                                        <asp:Label ID="lbl_mon_qualification" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Designation :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_mom_designation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mobile No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_mom_mobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Email Id :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_mom_email" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Annual Income :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_mom_income" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>







                                <%-- ============================ --%>
                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Sibling Details</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table class="print_table">

                                            <tr>
                                                <th style="text-align: left;">S. No.</th>
                                                <th style="text-align: left;">Name of Sibling</th>
                                                <th style="text-align: left;">Age</th>
                                                <th style="text-align: left;">School/College</th>
                                                <th style="text-align: left;">Class</th>
                                            </tr>
                                            <tr>
                                                <td>1.</td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_name1" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_age1" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_school_name1" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_class1" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>2.</td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_name2" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_age2" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_school_name2" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_sb_class2" runat="server" Text=""></asp:Label></td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>





                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Address Details :</td>
                                </tr>

                                <tr>
                                    <td>Address :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_adress" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>P.O.  :</td>
                                    <td>
                                        <asp:Label ID="lbl_poS" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>District :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_district" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City  :</td>
                                    <td>
                                        <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>State :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_states" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Pin Code :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>


                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; display:none">
                                <p class="dtls_grp-p" style="text-align: left; width: 50%; margin: 40px 0px 0px 0px; color: #000; padding: 0px 74px 0px 0px;">
                                    <asp:Image ID="img_fathers_image" Visible="false" runat="server" Style="width: 150px; height: 150px; padding: 2px; border: 2px solid #000;" />

                                    <br />

                                    <asp:Image ID="img_father_sig" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />
                                    <br />

                                    Father's Photo & Signature 
                                </p>
                                <p class="dtls_grp-p" style="text-align: right; width: 40%; margin: 40px 0px 0px 0px; color: #000; padding: 0px 1px 0px 0px;">
                                    <asp:Image ID="img_mother_photo" Visible="false" runat="server" Style="width: 150px; height: 150px; padding: 2px; border: 2px solid #000;" />

                                    <br />
                                    <asp:Image ID="img_mother_signature" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />
                                    <br />
                                    Mothers's Photo & Signature 
                                </p>
                            </div>



                            <div class="images-wprs" runat="server" id="decuments" style="display">
                                <div class="images-dv-wprs" id="img_family_photoDV" runat="server">
                                    <div class="images-wprsimges">
                                        <asp:Image ID="img_family_photo" runat="server" />
                                    </div>
                                    <div class="images-dv-wprs-name-pdv">
                                        <p class="images-dv-wprs-name-p">Family photo(Father,Mother,Ward)</p>
                                    </div>
                                </div>

                                <div class="images-dv-wprs" id="img_birth_cartificateDv" runat="server">
                                    <div class="images-wprsimges">
                                        <asp:Image ID="img_birth_cartificate" runat="server" />
                                    </div>
                                    <div class="images-dv-wprs-name-pdv">
                                        <p class="images-dv-wprs-name-p">Birth Certificate of the child</p>
                                    </div>
                                </div>
                                <div class="images-dv-wprs" id="img_residentialDV" runat="server">
                                    <div class="images-wprsimges">
                                        <asp:Image ID="img_residential" runat="server" />
                                    </div>
                                    <div class="images-dv-wprs-name-pdv">
                                        <p class="images-dv-wprs-name-p">Residential Certificate</p>
                                    </div>
                                </div>
                            </div>


                            <p class="dtls_grp-p">
                                Note : Hard copy of photo & id proof will needed on admission time.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
