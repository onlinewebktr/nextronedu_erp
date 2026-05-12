<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Page_career.aspx.cs" Inherits="school_web.print.Print_Page_career" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body, #form1 {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            position: absolute;
            font-family: sans-serif;
            font-size: 13px;
        }

        .main {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .mainautot {
            margin: 0px auto;
            padding: 0px;
            height: auto;
            width: 870px;
        }

        .mainwith {
            margin: 0px;
            padding: 5px;
            height: auto;
            width: 860px;
            float: left;
            border: 1px solid #000;
        }

        .top {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .topcell_left {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 50%;
            float: left;
            text-align: left;
        }

        .topcell_right {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 50%;
            float: left;
            text-align: right;
        }

        .heading {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .leftlogoheading {
            margin: 0px;
            padding: 0px;
            height: 110px;
            width: 20%;
            float: left;
        }

        .righttextheading {
            margin: 0px;
            padding: 0px;
            height: 110px;
            width: 80%;
            float: left;
        }

        .slipno {
            margin: 0px;
            padding: 0px 0px 4px 0px;
            height: auto;
            width: 100%;
            float: left;
            border-bottom: 2px solid #000;
        }

        .slipno_left {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 33%;
            float: left;
            text-align: left;
        }

        .slipno_middle {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 47%;
            float: left;
            text-align: center;
        }

        .slipno_right {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 20%;
            float: left;
            text-align: right;
        }

        .studentdetails {
            margin: 4px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }


        .student_left {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 40%;
            float: left;
            text-align: left;
        }

        .student_middle {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 30%;
            float: left;
            text-align: center;
        }

        .student_right {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 30%;
            float: left;
            text-align: right;
        }

        .pay_particular {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .td2 {
            text-align: right;
            padding: 0px 3px 0px 0px;
        }

        .td3 {
            text-align: left;
            padding: 0px 0px 0px 5px;
        }

        .footer {
            margin: 10px 0px 0px 0px;
            border-top: 2px solid #000;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .footer_left {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 50%;
            float: left;
            text-align: left;
        }

        .footer_middle {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 20%;
            float: left;
            text-align: right;
        }

        .footer_right {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 30%;
            float: left;
            text-align: right;
        }

        .footer_auth_sig {
            margin: 150px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
            text-align: left;
        }

        @media print {
            .noPrint {
                display: none;
            }
        }
    </style>
    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">

            <div class="mainautot">


                <div style="padding: 0px 0px 0px 0px; margin: 0px; height: 29px; width: 870px; float: left;">
                    <asp:Button ID="btn_back" runat="server" Text="Back" class="noPrint" OnClick="btn_back_Click"
                        Style="float: left;" />
                    <asp:Button ID="btn_print" runat="server" Text="Printit" class="noPrint" OnClick="btn_print_Click"
                        Style="float: right;" />
                </div>
            </div>
            <div class="mainautot">


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
                    <div class="heading">
                        <div class="leftlogoheading">
                            <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                        </div>
                        <div class="righttextheading">
                            <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                            </h1>

                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                <asp:Label ID="lbl_address" runat="server"></asp:Label>


                            </div>
                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                            </div>
                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                            </div>
                        </div>
                    </div>

                    <div class="slipno">
                        <div class="slipno_left">
                            Apply Id :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="slipno_middle">
                            STAFF RECRUITMENT- 
                            <asp:Label ID="lbl_session" runat="server" Font-Bold="true"></asp:Label>

                        </div>
                        <div class="slipno_right">
                            Receipt Date :
                        <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="studentdetails">
                        <div class="student_left">
                            Name :
                        <asp:Label ID="lbl_studentname" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="student_middle" style="width: 55%">
                            Father Name :
                           <asp:Label ID="lbl_fathername" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="student_right" style="width: 5%">
                        </div>
                    </div>

                    <div class="studentdetails">
                        <div class="student_left">
                            Mobile No. :
                        <asp:Label ID="lbl_mobileno" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="student_middle">
                        </div>
                        <div class="student_right">
                        </div>
                    </div>


                    <div class="studentdetails">
                        <div class="student_left" style="width: 100%">
                            Address :
                           <asp:Label ID="lbl_adress" runat="server" Font-Bold="true"></asp:Label>

                        </div>

                    </div>
                    <div class="studentdetails" style="display: none">
                        <div class="student_left" style="width: 100%">
                            Remarks :
                           <asp:Label ID="lbl_remarks" runat="server" Font-Bold="true"></asp:Label>

                        </div>

                    </div>
                    <div class="pay_particular">
                        <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" ShowFooter="false">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Apply For">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_applyfor" runat="server" Text='<%#Bind("Apply_for") %>'></asp:Label>

                                    </ItemTemplate>


                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Receipt mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_paymentmode" runat="server">Online </asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_payment_id" runat="server" Text='<%#Bind("razorpay_payment_id") %>'> </asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate><b>Total</b></FooterTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblamount" runat="server" Text='<%#Bind("Payable_amount") %>'></asp:Label>


                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totalpay" runat="server" Font-Bold="true"></asp:Label>




                                    </FooterTemplate>
                                </asp:TemplateField>




                            </Columns>

                        </asp:GridView>

                    </div>

                    <div class="footer">
                        <div class="footer_left">
                            Amount In Word :
                            <asp:Label ID="lbl_amountinword" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="footer_middle">
                        </div>
                        <div class="footer_right">
                            <asp:Label ID="lbl_paid_amount" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="footer">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="footer_left">
                    </div>
                    <div class="footer" style="border-top: 2px solid #fff;">
                        This is system generated receipt doesn't required signature
                    </div>


                    <div class="footer_auth_sig" style="display: none">
                        <div style="margin: 0px 0px 0px 0px; padding: 0px; height: auto; width: 25%; float: left; text-align: left;">
                            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Accountant Signature</b>
                        </div>
                        <div style="margin: 0px 0px 0px 0px; padding: 0px; height: auto; width: 25%; float: left; text-align: center;">
                            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Seal</b>
                        </div>
                        <div style="margin: 0px 0px 0px 0px; padding: 0px; height: auto; width: 50%; float: left; text-align: right;">

                            <b>Login User (
                                            <asp:Label ID="lbl_user_id" runat="server" Font-Bold="true"></asp:Label>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b>
                        </div>
                    </div>

                </div>
            </div>





        </div>
    </form>
</body>
</html>
