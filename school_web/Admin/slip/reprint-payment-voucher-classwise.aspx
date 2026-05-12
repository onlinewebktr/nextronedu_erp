<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reprint-payment-voucher-classwise.aspx.cs" Inherits="school_web.Admin.slip.reprint_payment_voucher_classwise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slip</title>
    <link href="css/payment-voucher.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/payment-voucher.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="mainautot">
                <div style="padding: 0px 0px 0px 0px; margin: 0px; height: 29px; width: 870px; float: left;">
                    <asp:Button ID="btn_back" runat="server" Text="Back" class="noPrint" OnClick="btn_back_Click"
                        Style="float: left;" />
                    <%--<asp:Button ID="btn_print" runat="server" Text="Printit" class="noPrint" OnClick="btn_print_Click"
                        Style="float: right;" />--%>
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" class="form-btn btn-print" Text="Print" Style="background: #ddd; color: #000; padding: 1px 7px 3px; float: right; border-radius: 3px; border: 1px solid #878787; text-decoration: none;"></asp:LinkButton>
                </div>
            </div>

            <div id="tblPrintIQ" runat="server">
                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                    <ItemTemplate>
                        <div class="mainautot">
                            <div class="mainwith">
                                <div class="top">
                                    <div class="topcell_left">
                                        Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true" Text='<%#Eval("Affiliation") %>'></asp:Label>
                                    </div>
                                    <div class="topcell_right" style="display: none">
                                        School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true" Text='<%#Eval("school_no") %>'></asp:Label>
                                    </div>
                                </div>
                                <div class="heading">
                                    <div class="leftlogoheading">
                                        <img src="<%#Eval("logo") %>" style="height: 100px; width: 100px" />
                                    </div>
                                    <div class="righttextheading">
                                        <h1 style="margin: 23px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 16px; line-height: 27px; text-decoration: underline;">
                                            <asp:Label ID="lbl_heading" runat="server" Text='<%#Eval("firm_name") %>'></asp:Label>
                                        </h1>

                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                            <asp:Label ID="lbl_address" runat="server" Text='<%#Eval("address1") %>'></asp:Label>


                                        </div>
                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                            Email Id. :<asp:Label ID="lbl_emaiid" runat="server" Text='<%#Eval("email") %>'></asp:Label>

                                            &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server" Text='<%#Eval("website") %>'></asp:Label>
                                        </div>
                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; display: none">
                                            Contact Details :<asp:Label ID="lbl_contact_details" runat="server" Text='<%#Eval("contact_no") %>'></asp:Label>
                                        </div>

                                        <div style="margin: 10px 0px 10px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; font-weight: 600;">
                                        </div>
                                    </div>
                                </div>

                                <div class="slipno">
                                    <div class="slipno_left">
                                        Receipt No :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true" Text='<%#Eval("Slip_id") %>'></asp:Label>
                                    </div>
                                    <div class="slipno_middle">
                                        <b>Payment Receipt</b>
                                    </div>
                                    <div class="slipno_right">
                                        Date :
                        <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true" Text='<%#Eval("Created_date") %>'></asp:Label>
                                    </div>
                                </div>
                                <div class="studentdetails">
                                    <div class="studentdetails">
                                        <p class="admissionno-p">
                                            Admission No. :
                            <asp:Label ID="lbl_aadmissionno" runat="server" Text='<%#Eval("Admission_no") %>'></asp:Label>
                                        </p>
                                        <p class="class-p">
                                            Class  :
                            <asp:Label ID="lbl_class" runat="server" Text='<%#Eval("class") %>'></asp:Label>
                                        </p>
                                        <p class="section-p">
                                            Section  :
                            <asp:Label ID="lbl_section" runat="server" Text='<%#Eval("Section") %>'></asp:Label>
                                        </p>
                                        <p class="roll-no-p">
                                            Roll No.  :
                            <asp:Label ID="lbl_rollno" runat="server" Text='<%#Eval("rollnumber") %>'></asp:Label>
                                        </p>
                                        <p class="namesss-p">
                                            Name  :
                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Eval("studentname") %>'></asp:Label>
                                        </p>
                                        <p class="fthers-namesss-p">
                                            Father's Name  :
                            <asp:Label ID="lbl_fathername" runat="server" Text='<%#Eval("fathername") %>'></asp:Label>
                                        </p>
                                        <p class="phones-no-p">
                                            Phone No.  :
                            <asp:Label ID="lbl_phone_no" runat="server" Text='<%#Eval("mobilenumber") %>'></asp:Label>
                                        </p>

                                        <p class="phones-no-p">
                                            Session  :
                             <asp:Label ID="lbl_session" runat="server" Text='<%#Eval("session") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <div class="pay_particular">
                                    <div class="fees-amt-dvp">
                                        <p class="fee-months-name">
                                            Monthly Fee 
                            (<asp:Label ID="lbl_fee_months" runat="server" Text='<%#Eval("Months") %>'></asp:Label>)
                                        </p>
                                        <p class="fee-months-name" id="Previous_month_level" runat="server" visible="false">
                                            Previous Month Dues
                                        </p>

                                        <p class="fee-months-name" id="Previous_year_level" runat="server" visible="false">
                                            Previous Year Dues
                                        </p>

                                        <p class="fee-months-name">
                                            Total
                                        </p>


                                    </div>

                                    <div class="fees-amt-dv">
                                        <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232;">
                                            ₹
                                <asp:Label ID="lbl_fee_rupee" runat="server" Visible="false" Text='<%#Eval("Amount") %>'></asp:Label>
                                            <asp:Label ID="lbl_fee_rupee_dis" runat="server"></asp:Label>

                                            <asp:Label ID="lbl_Session_id" runat="server" Visible="false" Text='<%#Eval("Session_id") %>'></asp:Label>
                                        </p>
                                        <%--<p class="fees-amt-dv-p prevous-duess" style="border-bottom: 0px dashed #323232;">
                                             
                                            <asp:Label ID="Label2" runat="server" Style="display: none"></asp:Label>
                                        </p>--%>
                                        <p class="fees-amt-dv-p prevous-duess" id="Previous_month_val" runat="server" visible="false">
                                            ₹
                                            
                                             <asp:Label ID="lbl_prevous_dues_month" runat="server" Text='<%#Eval("Previous_dues") %>'></asp:Label>
                                        </p>

                                        <p class="fees-amt-dv-p prevous-duess" id="Previous_year_val" runat="server" visible="false">
                                            ₹
                                            
                                             <asp:Label ID="lbl_prev_dues_dis" runat="server"></asp:Label>
                                        </p>
                                        <p class="fees-amt-dv-p prevous-duess" style="border-bottom: 0px dashed #323232; border-top: 1px dashed #323232;">
                                            <%-- ₹--%>

                                            <asp:Label ID="lbl_lbl_ttl_amts_dis" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <div class="pay-last-date">
                                    Payment to be made within
                       <asp:Label ID="lbl_pay_date" runat="server" Text='<%#Eval("Last_date_of_payment") %>'></asp:Label>

                                </div>

                                <div class="footer_auth_sig">
                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Signature of Depository</b>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Sign of Cashier</b>
                                </div>

                                <asp:Label ID="lbl_rd_viewsss" runat="server">
                                    <div class="notedvs" runat="server" id="notesDt">
                                        <p class="notedvs-note">Note : </p>


                                        <asp:Repeater ID="rd_viewsss" runat="server">
                                            <ItemTemplate>
                                                <p class="notedvs-note-p">
                                                    <span class="notedvs-note-p-no"><%#Container.ItemIndex+1 %>.</span>
                                                    <asp:Label ID="lbl_desc" class="notedvs-note-p-txt" runat="server" Text='<%#Bind("Description_note")%>'></asp:Label>
                                                </p>
                                            </ItemTemplate>
                                        </asp:Repeater>


                                    </div>
                                </asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
