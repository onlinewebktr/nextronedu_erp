<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Page.aspx.cs" Inherits="LMS.print.Print_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online Registration Form Details</title>

    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;523;600;700;800&display=swap" rel="stylesheet" />
    <link href="Print.css" rel="stylesheet" />
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Open+Sans:100,200,300,400,500,600,700" rel="stylesheet" />');
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
                    <asp:Panel ID="Panel1" runat="server">
                        <div style="margin: 0px 0px 0px 0px; padding: 5px 8px; width: 100%; height: auto; float: left; border: 1px solid #ddd;">
                            <div style="margin: 0px 0px 0px 0px; padding: 0px 0px; width: 100%; float: left; border-bottom: 1px solid #afafaf;">
                                <div style="margin: 0px 0px 0px 0px; padding: 0px 0px; width: 13%; float: left;">
                                    <asp:Image ID="Image1" runat="server" Style="width: 100%" />
                                </div>
                                <div style="margin: 0px 0px 0px 0px; padding: 0px 0px; width: 87%; float: left">
                                    <asp:Label Style="margin: 10px 0px 5px 0px; padding: 0px 0px; width: 100%; float: left; text-align: center; font-size: 26px; font-weight: 600;"
                                        ID="lbl_school_name" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" Text="" Style="margin: 5px 0px 0px 0px; padding: 0px 0px; width: 100%; float: left; font-size: 15px; text-align: center;"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" Text="" style="margin: 5px 0px 0px 0px;
    padding: 0px 0px;
    width: 100%;
    float: left;
    font-size: 15px;
    text-align: center;"   ></asp:Label>
                                    <p style="margin: 10px 0px 0px 0px; padding: 0px 0px; width: 100%; float: left; text-align: center; font-size: 16px;">
                                        Email : 
                                        <asp:Label ID="lbl_email_school" Style="margin: 0px 0px 0px 0px; padding: 0px 0px; text-decoration: underline;"
                                            runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div style="margin: 6px 0px 6px 0px; padding: 0px 0px; width: 100%; text-align: center; float: left;">
                                <h1 style="margin: 0; padding: 4px 10px; width: auto; background: #c7c7c7; display: initial; font-weight: 500; text-transform: uppercase; font-size: 22px; letter-spacing: 1px;">Admission Registration Receipt -<asp:Label Style="color: maroon !important; font-weight: bold;"
                                    ID="lbl_session1" runat="server" Text=""></asp:Label>
                                </h1>
                            </div>


                            <div style="margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 100%; height: auto; float: left;">

                                <table style="margin: 0px 0px 0px 0px; padding: 10px 40px 10px 10px; width: 100%; height: auto; float: left;">
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Registration Id :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_registrationid" Style="color: maroon !important; font-weight: bold;"
                                                runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px">Date :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_date" Style="color: maroon !important; font-weight: bold;"
                                                runat="server" Text=""></asp:Label>
                                        </td>


                                        <td rowspan="5" style="width: 130px; padding: 1px 1px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Image ID="img_s_image" runat="server" class="studnt-img" Style="margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 130px; height: 145px;" />
                                            <asp:Image ID="img_s_sig" runat="server" class="studnt-sig" Style="display: none; width: 130px; height: 45px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Name :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Gender :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_gender" Style="color: maroon !important; font-weight: bold;" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Date of birth :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_dob" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>Blood Group :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_blood_group" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Nationality :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_nationality" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Religion :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_religion" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Category :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_category" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">&nbsp;</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_cast" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Height :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_height" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Weight :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_weight" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Name of last school :</td>
                                        <td colspan="4" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_lastschool" runat="server" Text=""></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Session :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_session" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Class :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="subject_div" runat="server">
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Subject Taken :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;" colspan="4">
                                            <asp:Label ID="lbl_subject_taken" runat="server" Text=""></asp:Label>
                                        </td> 
                                    </tr>
                                    <tr id="islunch" runat="server" visible="false">
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Admission Type :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">

                                            <asp:Label ID="lbl_admissiontype" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Is Lunch</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">

                                            <asp:Label ID="lbl_lunch" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="5" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Sibling Details</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="text-align: left;">S. No.</th>
                                        <th style="text-align: left;">Name of Sibling</th>
                                        <th style="text-align: left;">Age</th>
                                        <th style="text-align: left;">School/College</th>
                                        <th style="text-align: left;">Class</th>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">1.</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_name1" runat="server" Text=""></asp:Label></td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_age1" runat="server" Text=""></asp:Label></td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_school_name1" runat="server" Text=""></asp:Label></td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_class1" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">2.</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_name2" runat="server" Text=""></asp:Label></td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_age2" runat="server" Text=""></asp:Label></td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_school_name2" runat="server" Text=""></asp:Label></td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_sb_class2" runat="server" Text=""></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td colspan="5" style="font-weight: 600; padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Payment Info :</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Payment Mode</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_paymnet_mode" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Payment Status
                                        </td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_Unpaid" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Payable amount</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_total" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Pyament Order Id
                                        </td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_payment_order_id" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td colspan="5" style="font-weight: 600; padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Father's Details :</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Father Name :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Occupation :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_occupation" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Qualification :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_qualification" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Designation :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_designation" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Mobile No. :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Email Id :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_email_id" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Annual Income :</td>
                                        <td colspan="4" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_annual_income" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Ifparentsaredivorced" runat="server">
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;" colspan="4">If parents are divorced, living separately or widowed, with whom is the child living? :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_is_devorced" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="childlivewith" runat="server">
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;" colspan="4">Who does the child live with? :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_child_live_with" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="withwhomthechildlives" runat="server">
                                        <td  style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;" colspan="3">Write the name and relationship of the person with whom the child lives. :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_divorced_guardian_name" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>



                                    <%-- ============================ --%>

                                    <tr>
                                        <td colspan="5" style="font-weight: 600; padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Mother's Details :</td>
                                    </tr>
                                    <tr>

                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Mother Name :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mothername" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Occupation :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mon_occupation" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Qualification :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mon_qualification" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Designation :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mom_designation" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Mobile No. :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mom_mobile" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Email Id :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mom_email" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Annual Income :</td>
                                        <td colspan="4" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_mom_income" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>







                                    <%-- ============================ --%>

                                    <tr>
                                        <td colspan="5" style="font-weight: 600; padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Address Details :</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Address :</td>
                                        <td colspan="4" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_adress" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">P.O.  :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_poS" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">District :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_district" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">City  :</td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">State :</td>
                                        <td colspan="2" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_states" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">Pin Code :</td>
                                        <td colspan="4" style="padding: 7px 10px; border: 1px solid #ddd; font-size: 15px;">
                                            <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                    <p style="text-align: right; width: 50%; margin: 40px 0px 0px 0px; color: #000; padding: 0px 74px 0px 0px; font-size: 16px; line-height: 29px; font-weight: 600; float: right;">
                                        <asp:Image ID="img_fathers_image" Visible="false" runat="server" Style="width: 150px; height: 150px; padding: 2px; border: 2px solid #000;" />

                                        <br />

                                        <asp:Image ID="img_father_sig" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />
                                        <br />

                                        Father's Photo & Signature 
                                    </p>
                                    <p style="text-align: left; width: 40%; margin: 40px 0px 0px 0px; color: #000; padding: 0px 1px 0px 0px; font-size: 16px; line-height: 29px; font-weight: 600; height: auto; float: left;">
                                        <asp:Image ID="img_mother_photo" Visible="false" runat="server" Style="width: 150px; height: 150px; padding: 2px; border: 2px solid #000;" />

                                        <br />
                                        <asp:Image ID="img_mother_signature" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />
                                        <br />
                                        Mothers's Photo & Signature 
                                    </p>
                                </div>

                                 <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; text-align:left" runat="server" visible="false" id="qrcode">
                                      <p style="text-align: left; width: 100%; margin: 40px 0px 0px 0px; color: #000; padding: 0px 0px 0px 0px; font-size: 16px; line-height: 29px; font-weight: 600; float: left;">
                                        <asp:Image ID="Image2" runat="server" Style="padding: 2px; border: 2px solid #000;
    padding: 2px;
    border: 2px solid #000;
    width: 100%;" />

                                        <br />

                                         

                                        QR Code Paymnet Slip
                                    </p>
                                     </div>



                                <p style="height: auto; width: 100%; margin: 8px 0px 0px 0px; padding: 0px 0px 0px 0px; float: left; color: #fc1111; font-size: 16px; line-height: 29px; font-weight: 600; text-align: center;">
                                    Note : Hard copy of photo & id proof will needed on admission time.
                                </p>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
