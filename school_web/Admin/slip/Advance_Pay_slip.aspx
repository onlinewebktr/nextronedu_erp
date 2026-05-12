<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Advance_Pay_slip.aspx.cs" Inherits="school_web.Admin.slip.Advance_Pay_slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Advance Slip</title>
       <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <link href="css/receipt-a5.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/receipt-a5.css" rel="stylesheet" type="text/css" />');
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
                            <asp:Button ID="Button1" CssClass="back-btn" runat="server" OnClick="Button1_Click" />
                        </div>

                        <div class="chckbx-sec">
                            <div class="chckbx-sec-inr">
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_both" onclick="myFunction('1')" runat="server" GroupName="aA" Text="Both Copy" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_office_copy" onclick="myFunction('2')" runat="server" GroupName="aA" Text="Parent's Copy" />
                                </div>

                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_student_copy" onclick="myFunction('3')" runat="server" GroupName="aA" Text="School  Copy" />
                                </div>
                            </div>
                        </div>


                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="printpagewprs">
                <div id="tblPrintIQ" runat="server">
                    <div class="mainautot">
                        <div class="printlefts" id="officecopY">
                            <div class="certificate-wpr1">
                                <asp:Image ID="Image2" runat="server" class="watermarklogos" />
                                <div class="certificate-wpr2">
                                    <div class="mainwith">
                                        <div class="heading" id="ContentHeader" runat="server">
                                            <div class="leftlogoheading">
                                                <asp:Image ID="imglogo" runat="server" />
                                            </div>
                                            <div class="righttextheading">
                                                <h1 class="firm-name-h" style="height: 57px;">
                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                </h1>
                                                <p class="slipnonam" style="width: 100% !important; float: left; margin: 0px; text-align: center;">
                                                    Advance Slip
                                                </p>
                                            </div>
                                        </div>
                                        <div class="heading-template" id="TempleteHeader" runat="server" visible="false">
                                            <asp:Image ID="img_header" runat="server" />
                                            <p class="slipnonam" style="width: 100% !important; float: left; margin: 0px; text-align: center;">
                                                Form Sale Slip
                                            </p>
                                        </div>

                                        <div class="studentdetails"> 
                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>R.No</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                                                </div>

                                                <div class="student_left-p-info wdth45">
                                                    <p>Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_studentname" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45">
                                                    <p>Session</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_session" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Father's</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_fathername" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45">
                                                    <p> </p>
                                                    <i> </i>
                                                     
                                                </div> 
                                            </div>
                                             
                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Form No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                                                </div> 
                                            </div>

                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Mode</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_mode" class="txttrfrmingert" Text="Cash" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45">
                                                    <p>Issued By</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_issued_by" Text="suraj kumar" runat="server"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row" runat="server" id="payChekOnline">
                                                <div class="student_left-p-info wdth55">
                                                    <p id="paytrnoname" runat="server">Cheque No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_no" class="txttrfrmingert" Text="" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45" style="display:none">
                                                    <p>Tr. Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_date" Text="04/02/2024" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="student-info-row" runat="server" id="payBankName">
                                                <div class="student_left-p-info slipnodv">
                                                    <p class="slipnonam">Bank Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_bank_name" Text=" " runat="server"></asp:Label>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="pay_particular">
                                            <table class="table table-bordered table-striped" style="width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th class="txtcnter">Sr. No.</th>
                                                        <th>Particular</th>
                                                        <th class="txtcnter">To Pay</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="grd_fees" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="txtcnter">
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_feetype" runat="server" Text='Advance Payment'></asp:Label>
                                                                </td>

                                                                <td class="txtcnter">
                                                                    <asp:Label ID="lbl_paid" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">Total</td>
                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_ttl_to_pay" class="fntbold" runat="server"></asp:Label>

                                                        </td>

                                                    </tr>
                                                    <%--<tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">scholarship</td>
                                                         
                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_discount_amt" class="fntbold" runat="server"></asp:Label></td>

                                                    </tr>--%>
                                                    <tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">Paid Amount</td>

                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_paid" class="fntbold" runat="server"></asp:Label></td>

                                                    </tr>
                                                    <%-- <tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">Remaining Amount</td>
                                                       
                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_remaining_amt" class="fntbold" runat="server"></asp:Label></td>
                                                    </tr>--%>
                                                </tbody>
                                            </table>
                                            <p class="remarksp" id="rmrkdV" runat="server">
                                                Remarks :
                                                <asp:Label ID="lbl_remarks" runat="server"></asp:Label>
                                            </p>
                                        </div>



                                        <div class="footer_auth_sig">
                                            <p>Signature <span></span></p>
                                        </div>

                                        <div class="notediv">
                                            <p>[Note : If any correction, Please consult to fee counter]</p>
                                            <p>[Note : Deposited Money is not refundable.]</p>
                                        </div>


                                        <p class="whichcopypp">PARENT'S COPY</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="printrghts" id="studentcopY">
                            <div class="certificate-wpr1">
                                <asp:Image ID="Image3" runat="server" class="watermarklogos" />
                                <div class="certificate-wpr2">
                                    <div class="mainwith" id="studentcopYInr">
                                        <div class="heading" id="ContentHeader1" runat="server">
                                            <div class="leftlogoheading">
                                                <asp:Image ID="Image1" runat="server" />
                                            </div>
                                            <div class="righttextheading">
                                                <h1 class="firm-name-h" style="height: 57px;">
                                                    <asp:Label ID="lbl_heading1" runat="server"></asp:Label>

                                                </h1>
                                                <p class="slipnonam" style="width: 100% !important; float: left; margin: 0px; text-align: center;">
                                                   Advance Slip
                                                </p>
                                            </div>
                                        </div>

                                        <div class="heading-template" id="TempleteHeader1" runat="server" visible="false">
                                            <asp:Image ID="img_header1" runat="server" />
                                            <p class="slipnonam" style="width: 100% !important; float: left; margin: 0px; text-align: center;">
                                                Form Sale Slip
                                            </p>
                                        </div>

                                        <div class="studentdetails">
                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>R.No</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                                                </div>

                                                <div class="student_left-p-info wdth45">
                                                    <p>Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_paymentdate1" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_studentname1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45">
                                                    <p>Session</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_session1" runat="server"></asp:Label>
                                                </div> 
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Father's</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_fathername1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45">
                                                    <p> </p>
                                                    <i> </i>
                                                    
                                                </div>
                                            </div> 
                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Form No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_aadmissionno1" runat="server"></asp:Label>
                                                </div> 
                                            </div>

                                            <div class="student-info-row">
                                                <div class="student_left-p-info wdth55">
                                                    <p>Mode</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_mode1" class="txttrfrmingert" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45">
                                                    <p>Issued By</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_issued_by1" runat="server"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row" runat="server" id="payChekOnline1">
                                                <div class="student_left-p-info wdth55">
                                                    <p id="paytrnoname1" runat="server">Cheque No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_no1" class="txttrfrmingert" Text="" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info wdth45" style="display:none">
                                                    <p>Tr. Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_date1" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="student-info-row" runat="server" id="payBankName1">
                                                <div class="student_left-p-info slipnodv">
                                                    <p class="slipnonam">Bank Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_bank_name1" Text="" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="pay_particular">
                                            <table class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th class="txtcnter">Sr. No.</th>
                                                        <th>Particular</th>
                                                        <th class="txtcnter">To Pay</th> 
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rp_fees1" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="txtcnter">
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_feetype" runat="server" Text='Advance Payment'></asp:Label>
                                                                </td>

                                                                <td class="txtcnter">
                                                                    <asp:Label ID="lbl_paid" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">Total</td>
                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_ttl_to_pay1" class="fntbold" runat="server"></asp:Label></td>

                                                    </tr>
                                                    <%--  <tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">Scholarship</td>
                                                         
                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_discount_amt1" class="fntbold" runat="server"></asp:Label></td>

                                                    </tr>--%>
                                                    <tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">Paid Amount</td>

                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_paid1" class="fntbold" runat="server"></asp:Label></td>

                                                    </tr>
                                                    <%-- <tr>
                                                        <td></td>
                                                        <td class="fntbold txtrght">Remaining Amount</td>
                                                       
                                                        <td class="txtcnter">
                                                            <asp:Label ID="lbl_remaining_amt1" class="fntbold" runat="server"></asp:Label></td>
                                                    </tr>--%>
                                                </tbody>
                                            </table>
                                            <p class="remarksp" id="rmrkdV1" runat="server">
                                                Remarks :
                                                <asp:Label ID="lbl_remarks1" runat="server"></asp:Label>
                                            </p>
                                        </div>



                                        <div class="footer_auth_sig">
                                            <p>Signature <span></span></p>
                                        </div>

                                        <div class="notediv">
                                            <p>[Note : If any correction, Please consult to fee counter.]</p>
                                            <p>[Note : Deposited Money is not refundable.]</p>
                                        </div>
                                        <p class="whichcopypp">SCHOOL COPY</p> 
                                    </div>
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
