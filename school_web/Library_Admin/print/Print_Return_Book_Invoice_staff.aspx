<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Return_Book_Invoice_staff.aspx.cs" Inherits="school_web.Library_Admin.print.Print_Return_Book_Invoice_staff" %>

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
                                    <asp:RadioButton ID="rdo_office_copy" onclick="myFunction('2')" runat="server" GroupName="aA" Text="Office Copy" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_student_copy" onclick="myFunction('3')" runat="server" GroupName="aA" Text="Staff Copy" />
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
                    <div class="mainwith">
                        <div class="top" style="display:none">
                            <div class="topcell_left" >
                                Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="topcell_right">
                                School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="heading" style="position: relative">
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

                            <img id="qrcode" runat="server" alt="QR" title="QR" style="position: absolute; top: 5px; right: 6px;" />
                        </div>

                        <div class="slipno">
                            <div class="slipno_left">
                                Receipt No :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="slipno_middle">
                                <b>Book Return Receipt (Office copy)</b>
                            </div>
                            <div class="slipno_right">
                                Book Return Date :
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
                                <p>Emp. Code</p>
                                <i>:</i>
                                <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                            </div>
                            <div class="student_left-p-info">
                                <p>User Type</p>
                                <i>:</i>
                                <asp:Label ID="lbl_class" runat="server"></asp:Label>
                            </div>
                        </div>
                         

                        <div class="pay_particular">
                            <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%" Style="text-align: center;"  OnRowDataBound="grd_fee_RowDataBound" ShowFooter="True">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                             <asp:Label ID="lbl_NoOfPages" runat="server" Text='<%#Bind("NoOfPages") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Book Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("NameOfBook") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Book No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("book_no") %>'></asp:Label>

                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Location/Sublocation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_location" runat="server" Text='<%#Bind("location") %>'></asp:Label>

                                            <asp:Label ID="lbl_sublocation" runat="server" Text='<%#Bind("Sub_Location") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Barcode">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Bar_code" runat="server" Text='<%#Bind("Bar_code") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Edition">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Edition" runat="server" Text='<%#Bind("Edition") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    

                                    <asp:TemplateField HeaderText="Due Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_duedate" runat="server" Text='<%#Bind("due_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Return Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_returndate" runat="server" Text='<%#Bind("returned_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Extra Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Extra_days" runat="server" Text='<%#Bind("Extra_days") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fine/Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fine" runat="server" Text='<%#Bind("fine")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Damage Fine">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_damagebookfine" runat="server" Text='<%#Bind("damagebookfine")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>Total</b>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Total_Fine" runat="server" Text='<%#Bind("Total_Fine")%>'></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <b>
                                                <asp:Label ID="lbl_total" runat="server"></asp:Label></b>
                                        </FooterTemplate>
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

                            </div>


                         
                        </div>



                        <div class="footer_auth_sig" style="position: relative">
                            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Library Incharge Signature</b>


                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Seal</b>
                        </div>

                    </div>
                </div>

                <div class="mainautot" id="studentcopY">
                    <div class="mainwith sdtcopymrgN" id="studentcopYInr">
                        <div class="top" style="display:none">
                            <div class="topcell_left">
                                Affiliation No :
                        <asp:Label ID="lbl_affiliation_no1" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="topcell_right">
                                School No. :
                        <asp:Label ID="lbl_schoolno1" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="heading" style="position:relative">
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
                            <img id="qrcode2" runat="server" alt="QR" title="QR" style="position: absolute; top: 5px; right: 6px;" />
                        </div>

                        <div class="slipno">
                            <div class="slipno_left">
                                Receipt No :
                        <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="slipno_middle">
                                <b>Book Return Receipt(Staff copy)</b>
                            </div>
                            <div class="slipno_right">
                                Book Return Date  :
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
                                <p>Emp. Code</p>
                                <i>:</i>
                                <asp:Label ID="lbl_aadmissionno1" runat="server"></asp:Label>
                            </div>
                            <div class="student_left-p-info">
                                <p>User Type</p>
                                <i>:</i>
                                <asp:Label ID="lbl_class1" runat="server"></asp:Label>
                            </div>
                        </div>
                        

                        <div class="pay_particular">
                            <asp:GridView ID="grd_fee1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" OnRowDataBound="grd_fee1_RowDataBound" ShowFooter="True">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Book Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("NameOfBook") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Book No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("book_no") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Location/Sublocation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_location" runat="server" Text='<%#Bind("location") %>'></asp:Label>
                                            --
                                            <asp:Label ID="lbl_sublocation" runat="server" Text='<%#Bind("Sub_Location") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Barcode">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Bar_code" runat="server" Text='<%#Bind("Bar_code") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Edition">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Edition" runat="server" Text='<%#Bind("Edition") %>'></asp:Label>
                                             <asp:Label ID="lbl_NoOfPages" runat="server" Text='<%#Bind("NoOfPages") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                 
                                    <asp:TemplateField HeaderText="Due Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_duedate" runat="server" Text='<%#Bind("due_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Return Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_returndate" runat="server" Text='<%#Bind("returned_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Extra Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Extra_days" runat="server" Text='<%#Bind("Extra_days") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fine/Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_fine" runat="server" Text='<%#Bind("fine")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Damage Fine">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_damagebookfine" runat="server" Text='<%#Bind("damagebookfine")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>Total</b>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Total_Fine" runat="server" Text='<%#Bind("Total_Fine")%>'></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <b>
                                                <asp:Label ID="lbl_total" runat="server"></asp:Label></b>
                                        </FooterTemplate>
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

                            </div>


                         
                        </div>

                        <div class="footer_auth_sig">
                            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Library Incharge Signature</b>


                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Seal</b>
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
