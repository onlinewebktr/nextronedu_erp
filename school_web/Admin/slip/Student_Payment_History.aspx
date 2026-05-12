<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Payment_History.aspx.cs" Inherits="school_web.Admin.slip.Student_Payment_History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Payment History</title>
       <script src="../../js/jquery-1.10.2.min.js"></script>
    <link href="css/student_payment_history.css" rel="stylesheet" />
    <style>


       /* @media only screen and (max-device-width: 480px) {

            .printptn {
                display: none;
            }

            .college1 {
                display: none;
            }
        }*/
    </style>
   <%-- <script type="text/javascript">
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
    </script>--%>

     <script type="text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
             var printWindow = window.open('', '', 'height=400,width=800');
             printWindow.document.write('<html><head>');
             printWindow.document.write('</head><body>');
             printWindow.document.write('<link href="css/student_payment_history.css" rel="stylesheet" type="text/css" />');
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

            <div class="mainautot">


                <div style="padding: 8px 0px 12px 0px;; margin: 0px; height: 29px; width: 870px; float: left;" class="printptn">
                    <asp:Button ID="btn_back" runat="server" Text="Back" class="noPrint" OnClick="btn_back_Click"
                        Style="float: left;" />
                    <asp:Button ID="btn_print" runat="server" Text="Print" class="noPrint" OnClientClick="return PrintPanel()"
                        Style="float: right;
    height: 30px;
    margin-top: 1px;
    width: 69px;" />
                </div>
            </div>

            <div class="mainautot" id="tblPrintIQ" runat="server">
                <div class="mainwith">
                    <div class="top" style="display: none">
                        <div class="topcell_left">
                            Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="topcell_right">
                            College No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="heading" style="border-bottom: 2px solid #000;">
                        <div class="leftlogoheading">
                            <asp:Image ID="imglogo" runat="server" Style="height: 75px; width: 90px" />
                        </div>
                        <div class="righttextheading">
                            <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                            </h1>

                            <div style="margin: 3px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                <asp:Label ID="lbl_address" runat="server"></asp:Label>


                            </div>
                            <div style="margin: 3px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                            </div>
                            <div style="margin: 3px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                Contact No. :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                            </div>
                            <div style="margin: 3px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; font-weight: bold;">
                                Payment History  


                            </div>
                        </div>
                    </div>


                    <div class="studentdetails">
                        <div class="student_left">
                            Name :
                        <asp:Label ID="lbl_studentname" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="student_middle">
                            Admission No. :
                            <asp:Label ID="lbl_aadmissionno" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="student_right">
                            Class :
                        <asp:Label ID="lbl_class" runat="server" Font-Bold="true"></asp:Label>-
                            <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="studentdetails">
                        <div class="student_left">
                            Father Name :
                        <asp:Label ID="lbl_fathername" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="student_middle">
                            Session :
                            <asp:Label ID="lbl_session" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="student_right">
                            Roll :
                        <asp:Label ID="lbl_rollno" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>

                    <div class="pay_particular">

                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" Style="font-weight: bold; color: black"></asp:Label>
                        <asp:GridView ID="grd_fee" runat="server" CssClass="bodrerf1" border="0" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" BorderStyle="None" CellPadding="0" GridLines="Vertical" ShowFooter="True" OnRowDataBound="grd_fee_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="td3" Width="50px" />
                                    <HeaderStyle CssClass="td3" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Receipt No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Receipt_No" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle CssClass="td3" Width="160px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Receipt_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle CssClass="td3" Width="90px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payment Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle CssClass="td3" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_mode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="td3" Width="100px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Description") %>' Style="word-break: break-all"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle CssClass="td3" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Paid">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Amount" runat="server" Font-Bold="true" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="td2" Width="100px" />
                                    <HeaderStyle CssClass="td2" />
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_total_paid" runat="server" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle CssClass="td2" />

                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                        <div class="sig-left" runat="server" id="signDVS">
                            <div class="rght-sig-img-dv">
                                <asp:Image ID="Image1" class="rght-sig-img" runat="server" />
                            </div>
                            <p class="sig-ps ng-binding">Accountant</p>
                        </div> 
                    </div>
                </div>
            </div>
        </div>




    </form>
</body>
</html>
