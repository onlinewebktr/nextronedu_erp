<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Page_Seat_full.aspx.cs" Inherits="school_web.Scholarship.Print_Page_Seat_full" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seat_full</title>
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;523;600;700;800&display=swap" rel="stylesheet" />

    <link href="../print/Print.css" rel="stylesheet" />
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
                    <div class="print_form-inr">
                        <div class="t-crtifcate-hdr-sec">
                            <div class="t-crtifcate-logo-sec">
                                <asp:Image ID="Image1" runat="server" />
                            </div>
                            <div class="t-crtifcate-hdr-contnt-sec">
                                <asp:Label ID="lbl_school_name" class="t-certificate-comp-name-h" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_address" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                <asp:Label ID="lbl_contact_no" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                <p class="t-certificate-comp-mail-p">
                                    Email : 
                                        <asp:Label ID="lbl_email_school" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="certificate-type-name-t-sec">
                            <h1 class="t-certificate-type-name-h">Scholarship Registration Receipt -<asp:Label ID="lbl_session1" runat="server" Text=""></asp:Label>
                            </h1>
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




                                <tr>
                                    <td>Registration Id :</td>
                                    <td>
                                        <asp:Label ID="lbl_regid" runat="server" Text=""></asp:Label>

                                    </td>
                                    <td>Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
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

                                <tr>
                                    <td colspan="5" style="font-weight: 600;">Payment Info :</td>
                                </tr>
                                <tr>
                                    <td>Payment Mode</td>
                                    <td>
                                        <asp:Label ID="lbl_paymnet_mode" runat="server" Font-Bold="true" ForeColor="Maroon" Style="text-transform: uppercase;"></asp:Label>
                                    </td>
                                    <td>Payment Remarks
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_payemntremarks" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>

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
                                    <td colspan="5" style="font-weight: 600;">Personal Details :</td>
                                </tr>
                                <tr>
                                    <td>Student Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                                    </td>

                                    <td>Father Name :</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
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
                                    <td colspan="4" style="text-transform: uppercase; text-align: center">Seat is full please ask for refund</td>

                                </tr>
                            </table>



                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
