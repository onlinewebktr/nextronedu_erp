<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_Sale_Slip.aspx.cs" Inherits="school_web.Admin.slip.Form_Sale_Slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form Sale Slip</title>
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
            padding: 3px 0px;
            height: auto;
            width: 54%;
            float: left;
            text-align: left;
            line-height: 20px;
            font-weight: 700;
        }

        .student_middle {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 45%;
            float: left;
            font-weight: 700;
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

        .watermarklogos {
            width: 40%;
            position: absolute;
            left: 30%;
            right: 30%;
        }

        .certificate-wpr1 {
            margin: 0;
            padding: 0px;
            width: 100%;
            float: left;
            position: relative;
            align-items: center;
            display: flex;
        }

        .certificate-wpr2 {
            margin: 0;
            padding: 0px 0px;
            width: 100%;
            float: left;
            position: relative;
            background: rgb(255 255 255 / 90%);
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
                <div class="certificate-wpr1">
                    <asp:Image ID="Image2" runat="server" class="watermarklogos" />
                    <div class="certificate-wpr2">
                        <div class="mainwith">
                            <div class="top">
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
                                    Index No. :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="slipno_middle">
                                    <b>Form Sale Receipt (Student copy) </b>

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
                                <div class="student_middle">
                                    Father's Name :
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
                                    Guardian's Name :
                                </div>
                                <div class="student_right">
                                    <asp:Label ID="lbl_guardian_name" runat="server" Font-Bold="true"></asp:Label>
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
                                <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" ShowFooter="True" OnRowDataBound="grd_fee_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Session" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("session") %>'></asp:Label>

                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Mode">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_paymentmode" runat="server" Text='<%#Bind("Payment_Mode") %>'></asp:Label>

                                            </ItemTemplate>


                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_remarkss" runat="server" Text='<%#Bind("Remarks") %>'></asp:Label>

                                            </ItemTemplate>


                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Class">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("class") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate><b>Total</b></FooterTemplate>

                                        </asp:TemplateField>









                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>

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
                                    <b>Paid :</b>
                                </div>
                                <div class="footer_right">
                                    <asp:Label ID="lbl_paid_amount" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="footer_left">
                            </div>
                            <div class="footer_middle" style="display: none">
                                <b>Uniqe No. :</b>
                            </div>
                            <div class="footer_right" style="display: none">
                                <asp:Label ID="lbl_unique_no" runat="server" Font-Bold="true"></asp:Label>
                            </div>


                            <div class="footer_auth_sig">
                                <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Accountant Signature</b>


                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Seal</b>
                            </div>

                        </div>
                    </div>
                </div>
            </div>



            <div class="mainautot">
                <div class="certificate-wpr1" style="margin: 10px 0px 0px 0px;">
                    <asp:Image ID="Image3" runat="server" class="watermarklogos" />
                    <div class="certificate-wpr2">
                        <div class="mainwith">
                            <div class="top">
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
                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                        <asp:Label ID="lbl_heading1" runat="server"></asp:Label>


                                    </h1>

                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        <asp:Label ID="lbl_address1" runat="server"></asp:Label>


                                    </div>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        Email Id. :<asp:Label ID="lbl_emaiid1" runat="server"></asp:Label>

                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website1" runat="server"></asp:Label>
                                    </div>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        Contact Details :<asp:Label ID="lbl_contact_details1" runat="server"></asp:Label>


                                    </div>
                                </div>
                            </div>

                            <div class="slipno">
                                <div class="slipno_left">
                                    Index No. :
                        <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="slipno_middle">
                                    <b>Form Sale Receipt (Office copy) </b>
                                </div>
                                <div class="slipno_right">
                                    Receipt Date :
                        <asp:Label ID="lbl_paymentdate1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="studentdetails">
                                <div class="student_left">
                                    Name :
                        <asp:Label ID="lbl_studentname1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="student_middle">
                                    Father's Name :
                           <asp:Label ID="lbl_fathername1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="student_right" style="width: 5%">
                                </div>
                            </div>
                            <div class="studentdetails">
                                <div class="student_left">
                                    Mobile No. :
                        <asp:Label ID="lbl_mobileno1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="student_middle">
                                    Guardian's name
                                </div>
                                <div class="student_right">
                                    <asp:Label ID="lbl_guardian_name1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>

                            <div class="studentdetails">
                                <div class="student_left" style="width: 100%">
                                    Address :
                           <asp:Label ID="lbl_adres1" runat="server" Font-Bold="true"></asp:Label>

                                </div>

                            </div>

                            <div class="studentdetails" style="display: none">
                                <div class="student_left" style="width: 100%">
                                    Remarks :
                           <asp:Label ID="lbl_remarks1" runat="server" Font-Bold="true"></asp:Label>

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


                                        <asp:TemplateField HeaderText="Session" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("session") %>'></asp:Label>

                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Mode">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_paymentmode" runat="server" Text='<%#Bind("Payment_Mode") %>'></asp:Label>

                                            </ItemTemplate>


                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_remarkss" runat="server" Text='<%#Bind("Remarks") %>'></asp:Label>

                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("class") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate><b>Total</b></FooterTemplate>

                                        </asp:TemplateField>









                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>

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
                            <asp:Label ID="lbl_amountinword1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="footer_middle">
                                    <b>Paid :</b>
                                </div>
                                <div class="footer_right">
                                    <asp:Label ID="lbl_paid_amount1" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="footer_left">
                            </div>
                            <div class="footer_middle" style="display: none">
                                <b>Uniqe No. :</b>
                            </div>
                            <div class="footer_right" style="display: none">
                                <asp:Label ID="lbl_unique_no_1" runat="server" Font-Bold="true"></asp:Label>
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
    </form>
</body>
</html>
