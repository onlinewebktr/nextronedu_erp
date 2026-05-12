<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="monthly-slip-with-yearly.aspx.cs" Inherits="school_web.Admin.slip.monthly_slip_with_yearly" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slip</title>
    <script src="../../js/jquery-1.10.2.min.js"></script>
    <link href="css/receipt.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/receipt.css" rel="stylesheet" type="text/css" />');
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
            <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="Button1" CssClass="back-btn" runat="server" OnClick="btn_back_Click" />
                        </div>

                        <div class="chckbx-sec">
                            <div class="chckbx-sec-inr">
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_both" onclick="myFunction('1')" runat="server" GroupName="aA" Text="Both" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_office_copy" onclick="myFunction('2')" runat="server" GroupName="aA" Text="Monthly Slip" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_student_copy" onclick="myFunction('3')" runat="server" GroupName="aA" Text="Student Copy" />
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
                <div class="mainautot" id="officecopY">
                    <div class="certificate-wpr1">
                        <asp:Image ID="Image2" runat="server" class="watermarklogos" />
                        <div class="certificate-wpr2">
                            <div class="mainwith">
                                <div class="top" style="display: none;">
                                    <div class="topcell_left">
                                        Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="topcell_right">
                                        School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="heading">
                                    <div class="leftlogoheading">
                                        <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                                    </div>
                                    <div class="righttextheading">
                                        <h1 class="firm-name-h">
                                            <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                        </h1>

                                        <div class="addres-firm">
                                            <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                        </div>
                                        <div class="email-firm">
                                            Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                            &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                        </div>
                                        <div runat="server" id="contact_no" visible="false" class="contact-frim">
                                            Contact No. :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="slipno">
                                    <div class="slipno_left">
                                        Receipt No :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="slipno_middle">
                                        <b>Fees Receipt (Monthly)</b>
                                    </div>
                                    <div class="slipno_right">
                                        Payment Date :
                        <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="studentdetails">
                                    <div class="student_left-p-info">
                                        <p>Name</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_studentname" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Admission No.</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Class</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="studentdetails">
                                    <div class="student_left-p-info">
                                        <p>Father Name</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_fathername" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Session</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_session" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Roll No.</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_rollno" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="pay_particular">
                                    <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" ShowFooter="True" OnRowDataBound="grd_fee_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Particular">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("Particular") %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle CssClass="td3" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_content1" runat="server" Font-Bold="true">Total</asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fee Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_payable" runat="server" Text='<%#Bind("fee_amount","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totalfeeamount" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_disc_amount" runat="server" Text='<%#Bind("disc_amt","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totaldiscount" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Paid Perviously">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_perviously_paid" runat="server" Text='<%#Bind("previously_paid","{0:n}") %>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totalpreviously" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Payable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_dues" runat="server" Text='<%#Bind("payable","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totalpaybale" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle CssClass="td2" />
                                                <ItemStyle CssClass="td2" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>



                                <div class="footer-ttl-div">
                                    <div class="footer-ttl-lft-div">
                                        <div class="footer-ttl-lft-p-dv">
                                            <p>Amount In Word</p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_amountinword" runat="server"></asp:Label>
                                        </div>

                                        <div class="footer-ttl-lft-p-dv">
                                            <p>Payment Mode</p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_paymentmode" runat="server"></asp:Label>
                                        </div>

                                        <div class="footer-ttl-lft-p-dv" runat="server" id="lbl_trans_noDv">
                                            <p>Transaction No.</p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_trans_no" runat="server"></asp:Label>
                                        </div>
                                        <div class="footer-ttl-lft-p-dv" runat="server" id="remarksDv">
                                            <p>Remarks </p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_remarks" runat="server"></asp:Label>
                                        </div> 
                                    </div>


                                    <div class="footer-ttl-rght-div">
                                        <div class="footer-ttl-rght-p-dv">
                                            <p>Paid </p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_paid_amount" runat="server"></asp:Label>
                                        </div>

                                        <div class="footer-ttl-rght-p-dv">
                                            <p>Dues </p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_dues" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="footer_auth_sig">
                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Accountant Signature</b>


                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Seal</b>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>



                <div class="mainautot" id="studentcopY">
                    <div class="certificate-wpr1">
                        <asp:Image ID="Image3" runat="server" class="watermarklogos" />
                        <div class="certificate-wpr2">
                            <div class="mainwith sdtcopymrgN" id="studentcopYInr">
                                <div class="top" style="display: none;">
                                    <div class="topcell_left">
                                        Affiliation No :
                        <asp:Label ID="lbl_affiliation_no1" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="topcell_right">
                                        School No. :
                        <asp:Label ID="lbl_schoolno1" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="heading">
                                    <div class="leftlogoheading">
                                        <asp:Image ID="Image1" runat="server" Style="height: 100px; width: 100px" />
                                    </div>
                                    <div class="righttextheading">
                                        <h1 class="firm-name-h">
                                            <asp:Label ID="lbl_heading1" runat="server"></asp:Label>
                                        </h1>

                                        <div class="addres-firm">
                                            <asp:Label ID="lbl_address1" runat="server"></asp:Label>
                                        </div>

                                        <div class="email-firm">
                                            Email Id. :<asp:Label ID="lbl_emaiid1" runat="server"></asp:Label>

                                            &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website1" runat="server"></asp:Label>
                                        </div>
                                        <div runat="server" id="contact_no1" visible="false" class="contact-frim">
                                            Contact No. :<asp:Label ID="lbl_contact_details1" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="slipno">
                                    <div class="slipno_left">
                                        Receipt No :
                        <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="slipno_middle">
                                        <b>Fees Receipt <asp:Label ID="lbl_yearly_type" runat="server"></asp:Label></b>
                                    </div>
                                    <div class="slipno_right">
                                        Payment Date :
                        <asp:Label ID="lbl_paymentdate1" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="studentdetails">
                                    <div class="student_left-p-info">
                                        <p>Name</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_studentname1" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Admission No.</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_aadmissionno1" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Class</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_class1" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="studentdetails">
                                    <div class="student_left-p-info">
                                        <p>Father Name</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_fathername1" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Session</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_session1" runat="server"></asp:Label>
                                    </div>
                                    <div class="student_left-p-info">
                                        <p>Roll No.</p>
                                        <i>:</i>
                                        <asp:Label ID="lbl_rollno1" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="pay_particular">
                                    <asp:GridView ID="grd_fee1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" ShowFooter="True" OnRowDataBound="grd_fee1_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Particular">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("Particular") %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle CssClass="td3" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_content1" runat="server" Font-Bold="true">Total</asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fee Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_payable" runat="server" Text='<%#Bind("fee_amount","{0:n}") %>'></asp:Label>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totalfeeamount" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_disc_amount" runat="server" Text='<%#Bind("disc_amt","{0:n}") %>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totaldiscount" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Paid Perviously">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_perviously_paid" runat="server" Text='<%#Bind("previously_paid","{0:n}") %>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totalpreviously" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Payable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_dues" runat="server" Text='<%#Bind("payable","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_totalpaybale" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle CssClass="td2" />
                                                <ItemStyle CssClass="td2" />
                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                </div>

                                <div class="footer-ttl-div">
                                    <div class="footer-ttl-lft-div">
                                        <div class="footer-ttl-lft-p-dv">
                                            <p>Amount In Word</p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_amountinword1" runat="server"></asp:Label>
                                        </div>

                                        <div class="footer-ttl-lft-p-dv">
                                            <p>Payment Mode</p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_paymentmode1" runat="server"></asp:Label>
                                        </div>

                                        <div class="footer-ttl-lft-p-dv" runat="server" id="lbl_trans_no1Dv">
                                            <p>Transaction No.</p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_trans_no1" runat="server"></asp:Label>
                                        </div>
                                        <div class="footer-ttl-lft-p-dv" runat="server" id="remarksDv1">
                                            <p>Remarks </p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_remarks1" runat="server"></asp:Label>
                                        </div> 

                                        <div class="footer_middle" style="display: none">
                                            <b>Adjust Amount :</b>
                                        </div>
                                        <div class="footer_right" style="display: none">
                                            <asp:Label ID="lbl_adjustamount1" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="footer-ttl-rght-div">
                                        <div class="footer-ttl-rght-p-dv">
                                            <p>Paid </p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_paid_amount1" runat="server"></asp:Label>
                                        </div>

                                        <div class="footer-ttl-rght-p-dv">
                                            <p>Dues </p>
                                            <i>:</i>
                                            <asp:Label ID="lbl_dues1" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>


                                <div class="footer_auth_sig">
                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Accountant Signature</b>


                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Seal</b>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hd_print_type" runat="server" />
        <script type="text/javascript">
            $(document).ready(function () {
                var PrintType = $('#<%= hd_print_type.ClientID %>').val();
                if (PrintType == "1") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";
                }
                else if (PrintType == "2") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " hidden";
                }
                else {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " hidden";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";
                }
            });





            function myFunction(PrintType) {
                if (PrintType == "1") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";

                    var StudentcopYInr = document.getElementById("studentcopYInr");
                    OfficecopY.classList.remove("hidden");
                    StudentcopY.classList.remove("hidden");
                    StudentcopYInr.classList.remove("extrClass");
                }
                else if (PrintType == "2") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " hidden";

                    var studentcopYInr = document.getElementById("studentcopYInr");
                    OfficecopY.classList.remove("hidden");
                    StudentcopY.classList.remove("showd");
                    studentcopYInr.classList.remove("extrClass");
                }
                else {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " hidden";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";

                    var studentcopYInr = document.getElementById("studentcopYInr");
                    studentcopYInr.className += " extrClass";
                    OfficecopY.classList.remove("showd");
                    StudentcopY.classList.remove("hidden");
                }
            }
        </script>
    </form>
</body>
</html>
