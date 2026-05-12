<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="unsuccess.aspx.cs" Inherits="LMS.print.unsuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                        <div class="print_form-img">
                            <img src="smileprintlogo.png" style="height:100px; width:100px;" />
                            <hr style="border: 1px solid #000;" />

                            <h2 class="titl-big-h">Admission Registration Receipt</h2>


                        </div>
                        <div class="print_form-dtls">
                            <table class="print_table">
                                <tr>
                                    <td colspan="5" style="font-size: 20px; color: red; text-align: center; font-weight: bold;">Sorry, your payment failed. No charges were made.
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


                                    <td rowspan="6" style="width: 130px; padding: 1px 1px;">
                                        <asp:Image ID="img_s_image" runat="server" class="studnt-img" />
                                        <asp:Image ID="img_s_sig" runat="server" class="studnt-sig" />
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
                                    <td>Cast :</td>
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
                                    <td colspan="3">
                                        <asp:Label ID="lbl_lastschool" runat="server" Text=""></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Class :</td>
                                    <td>
                                        <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Admission Type :</td>
                                    <td>
                                        <asp:Label ID="lbl_admissiontype" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">Payment Info :</td>

                                </tr>
                                <tr>
                                    <td colspan="5">

                                        <table class="table table-bordered">
                                            <tr>
                                                <td style="padding: 5px;">Payable amount type

                                                </td>
                                                <td style="padding: 5px;">
                                                    <asp:Label ID="lbl_total" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>


                                                </td>
                                                <td style="padding: 5px;">Payable type

                                                </td>
                                                <td style="padding: 5px;">
                                                    <asp:Label ID="lbl_paybaltype" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>


                                                </td>
                                                <td style="padding: 5px;">Transaction Id

                                                </td>
                                                <td style="padding: 5px;">
                                                    <asp:Label ID="lbl_tranid" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>


                                                </td>

                                            </tr>
                                            <%--  <tr>
                                                <td colspan="6">
                                                     <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Maroon" style="font-size: 18px;">You payment has been failure</asp:Label>
                                                </td>
                                            </tr>--%>
                                        </table>

                                    </td>

                                </tr>
                                <tr>
                                    <td>Father Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Mother Name :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_mothername" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Address :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_adress" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City  :</td>
                                    <td>
                                        <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Pin Code :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Father Mobile No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_father_mobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Mother Mobile No. :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_mother_mobile" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Student Mobile No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_student_mobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Mobile No. for use of SMS :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_no_for_use_of_sms" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>WhatsApp No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_whatsapp_no" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Email :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>

                            <p class="dtls_grp-p">
                                Any query please contact to +91 620 3184 684 
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
