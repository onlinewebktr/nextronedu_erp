<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment-voucher-n.aspx.cs" Inherits="school_web.Admin.slip.payment_voucher_n" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slip</title>
    <link href="payment-slip-n.css" rel="stylesheet" />
    <%--<script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>--%>

    <script type="text/javascript">
        function PrintWindow() {
            window.print();
            // CheckWindowState();
        }

        function CheckWindowState() {
            if (document.readyState == "complete") {
                window.close();
            }
            else {
                setTimeout("CheckWindowState()", 2000)
            }
        }

        PrintWindow();
    </script>


    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="payment-slip-n.css" rel="stylesheet" type="text/css" />');
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

        <div class="main">
            <%--<div class="mainautot">
                <div style="padding: 0px 0px 0px 0px; margin: 0px; height: 29px; width: 870px; float: left;">
                    <asp:Button ID="btn_back" runat="server" Text="Back" class="noPrint" OnClick="btn_back_Click"
                        Style="float: left;" />
                    <asp:Button ID="btn_print" runat="server" Text="Printit" class="noPrint" OnClick="btn_print_Click"
                        Style="float: right;" />
                </div>
            </div>--%>

            <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="Button1" CssClass="back-btn" runat="server" OnClick="btn_back_Click" />
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tblPrintIQ" runat="server">
                <div class="mainautot">
                    <div class="mainwith">
                        <div class="top">
                            <div class="topcell_left">
                                Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="topcell_right" style="display: none">
                                School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="heading">
                            <div class="leftlogoheading">
                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                            </div>
                            <div class="righttextheading">
                                <h1 class="frmsname">
                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                </h1>

                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                </div>
                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>
                                    &nbsp;&nbsp;
                                
                            <asp:Label ID="lbl_website" runat="server" Style="display: none"></asp:Label>
                                </div>
                                <div runat="server" id="contact_no" visible="false" style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                    Contact No. :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="slipno">
                            <div class="slipno_left">
                                &nbsp;No :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="slipno_middle">
                                <b>Demand Receipt  (Office copy)</b>
                            </div>
                            <div class="slipno_right">
                                Date :
                        <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="studentdetails">
                            <div class="studentdetails">

                                <p class="admissionno-pr">
                                    <i>Admission No. :</i>
                                    <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                                </p>
                                <p class="class-p">
                                    <i>Class  :</i>
                                    <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                </p>
                                <p class="section-p">
                                    <i>Section  :</i>
                                    <asp:Label ID="lbl_section" runat="server"></asp:Label>
                                </p>
                                <p class="roll-no-p">
                                    <i>Roll No.  :</i>
                                    <asp:Label ID="lbl_rollno" runat="server"></asp:Label>
                                </p>

                                <div class="std-dt-50">
                                    <p class="admissionno-p">
                                        <i>Name  :</i>
                                        <asp:Label ID="lbl_studentname" runat="server"></asp:Label>
                                    </p>
                                    <p class="admissionno-p">
                                        <i>Father's Name  :</i>
                                        <asp:Label ID="lbl_fathername" runat="server"></asp:Label>
                                    </p>
                                </div>

                                <div class="std-dt-50">
                                    <p class="admissionno-p">
                                        <i>Session  :</i>
                                        <asp:Label ID="lbl_session" runat="server" Font-Bold="true"></asp:Label>
                                    </p>
                                    <p class="admissionno-p">
                                        <i>Phone No.  :</i>
                                        <asp:Label ID="lbl_phone_no" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="pay_particular">
                            <style>
                                td {
                                    text-align: center;
                                }
                            </style>
                            <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="false">
                                <table style="width: 100%;" border="1">

                                    <tr>
                                        <th>Month</th>
                                        <th>Fees Head</th>
                                        <th class="txtrght">Fees Amount</th>
                                        <th class="txtrght">Discount</th>
                                        <th class="txtrght">Paid Previously</th>
                                        <th class="txtrght">Payable</th>
                                    </tr>


                                    <asp:Repeater ID="rp_fee_details_Office_copy" runat="server">
                                        <ItemTemplate>
                                            <tr id="row" runat="server">
                                                <td>
                                                    <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                </td>
                                                <td class="txtrght">
                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                                                </td>
                                                <td class="txtrght">
                                                    <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                </td>
                                                <td class="txtrght">
                                                    <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                </td>
                                                <td class="txtrght">
                                                    <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <tr>
                                        <th colspan="2" class="txtrght">Total :
                                        </th>
                                        <th class="txtrght">
                                            <asp:Label ID="lbl_fee_amount" runat="server" Text=""></asp:Label></th>
                                        <th class="txtrght">
                                            <asp:Label ID="lbl_discount" runat="server" Text=""></asp:Label></th>
                                        <th class="txtrght">
                                            <asp:Label ID="lbl_paid_prev" runat="server" Text=""></asp:Label></th>
                                        <th class="txtrght">
                                            <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></th>

                                    </tr>
                                </table>

                            </asp:Panel>
                        </div>

                        <div class="pay_particular-ttls">
                            <div class="fees-amt-dvp">
                                <p class="fee-months-name">
                                    Total Tuition Fee
                            <asp:Label ID="lbl_fee_months" runat="server"></asp:Label>
                                </p>
                                <%--<p class="fee-months-name">
                                    Late Fine till Date
                                </p>--%>
                                <p class="fee-months-name" style="display: none">
                                    Previous Due
                                </p>

                                <p class="fee-months-name" style="display: none">
                                    Total
                                </p>
                            </div>

                            <div class="fees-amt-dv">
                                <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232;">
                                    ₹
                                <asp:Label ID="lbl_fee_rupee" runat="server"></asp:Label>
                                </p>

                                <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232; display: none">
                                    ₹
                                    <asp:Label ID="lbl_prev_dues" runat="server"></asp:Label>
                                </p>
                                <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232; display: none">
                                    ₹
                                    <asp:Label ID="lbl_ttl_amts" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>


                        <div class="pay_particular-ttls1">
                            <p class="amt-inwrd">Amount in word :  <asp:Label ID="lbl_amt_in_word1" runat="server"></asp:Label></p>
                            <div class="footer_auth_sig">
                                
                                <p class="paytobe-made">
                                    Late fine will be charge after
                                    <asp:Label ID="lbl_pay_date" runat="server"></asp:Label>
                                </p>
                                <p class="footer_auth_sig-rght">Authorised Signatory</p>
                            </div>
                        </div>
                        <div class="notedvs" runat="server" id="notesDt">
                            <p class="notedvs-note">Note : </p>


                            <asp:Repeater ID="rd_view" runat="server">
                                <ItemTemplate>
                                    <p class="notedvs-note-p">
                                        <span class="notedvs-note-p-no"><%#Container.ItemIndex+1 %>.</span>
                                        <asp:Label ID="lbl_desc" class="notedvs-note-p-txt" runat="server" Text='<%#Bind("Description_note")%>'></asp:Label>
                                    </p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>




                    <div class="mainautot">
                        <div class="mainwith mainwith2">
                            <div class="top">
                                <div class="topcell_left">
                                    Affiliation No :
                        <asp:Label ID="lbl_affiliation_no1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="topcell_right" style="display: none">
                                    School No. :
                        <asp:Label ID="lbl_schoolno1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="heading">
                                <div class="leftlogoheading">
                                    <asp:Image ID="Image1" runat="server" Style="height: 100px; width: 100px" />
                                </div>
                                <div class="righttextheading">
                                    <h1 class="frmsname">
                                        <asp:Label ID="lbl_heading1" runat="server"></asp:Label>
                                    </h1>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        <asp:Label ID="lbl_address1" runat="server"></asp:Label>
                                    </div>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        Email Id. :<asp:Label ID="lbl_emaiid1" runat="server"></asp:Label>
                                        &nbsp;&nbsp;  
                                      <asp:Label ID="lbl_website1" runat="server" Style="display: none"></asp:Label>
                                    </div>
                                    <div runat="server" id="contact_no1" visible="false" style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        Contact No. :<asp:Label ID="lbl_contact_details1" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="slipno">
                                <div class="slipno_left">
                                    No :
                        <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="slipno_middle">
                                    <b>Demand Receipt (Student copy)</b>
                                </div>
                                <div class="slipno_right">
                                    Date :
                        <asp:Label ID="lbl_paymentdate1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>


                            <div class="studentdetails">

                                <p class="admissionno-pr">
                                    <i>Admission No. :</i>
                                    <asp:Label ID="lbl_aadmissionno1" runat="server"></asp:Label>
                                </p>
                                <p class="class-p">
                                    <i>Class  :</i>
                                    <asp:Label ID="lbl_class1" runat="server"></asp:Label>
                                </p>
                                <p class="section-p">
                                    <i>Section  :</i>
                                    <asp:Label ID="lbl_section1" runat="server"></asp:Label>
                                </p>
                                <p class="roll-no-p">
                                    <i>Roll No.  :</i>
                                    <asp:Label ID="lbl_rollno1" runat="server"></asp:Label>
                                </p>


                                <div class="std-dt-50">
                                    <p class="admissionno-p">
                                        <i>Name  :</i>
                                        <asp:Label ID="lbl_studentname1" runat="server"></asp:Label>
                                    </p>
                                    <p class="admissionno-p">
                                        <i>Father's Name  :</i>
                                        <asp:Label ID="lbl_fathername1" runat="server"></asp:Label>
                                    </p>


                                </div>

                                <div class="std-dt-50">
                                    <p class="admissionno-p">
                                        <i>Session  :</i>
                                        <asp:Label ID="lbl_session1" runat="server" Font-Bold="true"></asp:Label>
                                    </p>
                                    <p class="admissionno-p">
                                        <i>Phone No.  :</i>
                                        <asp:Label ID="lbl_phone_no1" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>


                            <div class="pay_particular">
                                <style>
                                    td {
                                        text-align: center;
                                    }
                                </style>
                                <asp:Panel ID="pnl_month_wise_fee_details_part2" runat="server" Visible="false">
                                    <table style="width: 100%;" border="1">

                                        <tr>
                                            <th>Month</th>
                                            <th>Fees Head</th>
                                            <th class="txtrght">Fees Amount</th>
                                            <th class="txtrght">Discount</th>
                                            <th class="txtrght">Paid Previously</th>
                                            <th class="txtrght">Payable</th>
                                        </tr>


                                        <asp:Repeater ID="rp_fee_details_Office_copy_part2" runat="server">
                                            <ItemTemplate>
                                                <tr id="row" runat="server">
                                                    <td>
                                                        <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                    </td>
                                                    <td class="txtrght">
                                                        <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                                                    </td>
                                                    <td class="txtrght">
                                                        <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                    </td>
                                                    <td class="txtrght">
                                                        <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                    </td>
                                                    <td class="txtrght">
                                                        <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <tr>
                                            <th colspan="2" class="txtrght">Total :
                                            </th>
                                            <th class="txtrght">
                                                <asp:Label ID="lbl_fee_amount1" runat="server" Text=""></asp:Label></th>
                                            <th class="txtrght">
                                                <asp:Label ID="lbl_discount1" runat="server" Text=""></asp:Label></th>
                                            <th class="txtrght">
                                                <asp:Label ID="lbl_paid_prev1" runat="server" Text=""></asp:Label></th>
                                            <th class="txtrght">
                                                <asp:Label ID="lbl_total1" runat="server" Text=""></asp:Label></th>

                                        </tr>
                                    </table>

                                </asp:Panel>
                            </div>




                            <div class="pay_particular-ttls">
                                <div class="fees-amt-dvp">
                                    <p class="fee-months-name">
                                       Total Tuition Fee
                            <asp:Label ID="lbl_fee_months1" runat="server"></asp:Label>
                                    </p>


                                    <p class="fee-months-name" style=" display:none">
                                        Previous Due
                                    </p>

                                    <p class="fee-months-name" style=" display:none">
                                        Total
                                    </p>
                                </div>
                                <div class="fees-amt-dv">
                                    <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232;">
                                        ₹
                                <asp:Label ID="lbl_fee_rupee1" runat="server"></asp:Label>
                                    </p>

                                    <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232; display:none">
                                        ₹
                                        <asp:Label ID="lbl_prev_dues1" runat="server"></asp:Label>
                                    </p>
                                    <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232; display:none">
                                        ₹
                                        <asp:Label ID="lbl_ttl_amts1" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>

                            <div class="pay_particular-ttls1">
                                <p class="amt-inwrd">Amount in word :  <asp:Label ID="lbl_amt_in_word" runat="server" Text=""></asp:Label></p>
                                <div class="footer_auth_sig">
                                    <p class="paytobe-made">
                                        Late fine will be charge after
                                        <asp:Label ID="lbl_pay_date1" runat="server"></asp:Label>
                                    </p>
                                    <p class="footer_auth_sig-rght">Authorised Signatory</p>
                                </div>
                            </div>


                            <div class="notedvs" runat="server" id="notesDt1">
                                <p class="notedvs-note">Note : </p> 
                                <asp:Repeater ID="rd_notes" runat="server">
                                    <ItemTemplate>
                                        <p class="notedvs-note-p">
                                            <span class="notedvs-note-p-no"><%#Container.ItemIndex+1 %>.</span>
                                            <asp:Label ID="lbl_desc" class="notedvs-note-p-txt" runat="server" Text='<%#Bind("Description_note")%>'></asp:Label>
                                        </p>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>


        <div style="height: 1px; overflow: hidden">
            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Month">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                            <asp:CheckBox ID="chk_month" Checked="true" runat="server" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
