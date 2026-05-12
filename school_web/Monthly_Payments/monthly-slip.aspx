<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="monthly-slip.aspx.cs" Inherits="school_web.Monthly_Payments.monthly_slip_aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slip</title>
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
            width: 30%;
            float: left;
            text-align: left;
        }

        .slipno_middle {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 50%;
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
            margin: 115px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
            text-align: left;
        }

        .student_left-p-info {
            margin: 3px 0px 3px 0px;
            padding: 0px;
            height: auto;
            width: 33.3%;
            float: left;
        }

            .student_left-p-info p {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                height: auto;
                width: 31%;
                float: left;
                font-weight: 500;
            }

            .student_left-p-info i {
                width: 10px;
                float: left;
                font-weight: 600;
                font-style: inherit;
            }

            .student_left-p-info span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                height: auto;
                width: 64%;
                float: left;
                font-weight: 600;
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
            <div class="mainautot" style="display:none">


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
                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; display: none">
                                Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                            </div>
                        </div>
                    </div>

                    <div class="slipno">
                        <div class="slipno_left">
                            Receipt No :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="slipno_middle">
                            <b>Fees Receipt (Office copy)</b>
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
                            <p>Roll</p>
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

                    <style>
                        .footer-ttl-div {
                            margin: 2px 0px 0px 0px;
                            padding: 5px 0px 0px 0px;
                            width: 100%;
                            float: left;
                            border-top: 1px solid #4c4c4c;
                        }

                        .footer-ttl-lft-div {
                            margin: 0px;
                            padding: 0px;
                            width: 75%;
                            float: left;
                        }


                        .footer-ttl-lft-p-dv {
                            margin: 0px;
                            padding: 4px 0px 4px 0px;
                            width: 100%;
                            float: left;
                            font-size: 13px;
                            font-weight: 500;
                        }

                            .footer-ttl-lft-p-dv p {
                                margin: 0px;
                                padding: 0px;
                                width: 18%;
                                float: left;
                            }

                            .footer-ttl-lft-p-dv i {
                                width: 10px;
                                float: left;
                                font-weight: 600;
                                font-style: inherit;
                            }

                            .footer-ttl-lft-p-dv span {
                                margin: 0px;
                                padding: 0px;
                                width: 70%;
                                float: left;
                            }



                        /*====================*/
                        .footer-ttl-rght-div {
                            margin: 0px;
                            padding: 0px;
                            width: 25%;
                            float: left;
                        }


                        .footer-ttl-rght-p-dv {
                            margin: 3px 0px 3px 0px;
                            padding: 0px;
                            width: 80%;
                            float: right;
                            text-align: right;
                            font-size: 13px;
                            font-weight: 600;
                        }

                            .footer-ttl-rght-p-dv p {
                                margin: 0px;
                                padding: 0px;
                                width: 35px;
                                float: left;
                            }

                            .footer-ttl-rght-p-dv i {
                                margin: 0px;
                                padding: 0px 7px 0px 0px;
                                width: 12px;
                                float: left;
                                font-weight: 700;
                                font-style: inherit;
                            }

                            .footer-ttl-rght-p-dv span {
                                margin: 0px;
                                padding: 0px;
                                width: 68%;
                                float: left;
                            }
                    </style>

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



                            <div class="footer_middle" style="display: none">
                                <b>Adjust Amount :</b>
                            </div>
                            <div class="footer_right" style="display: none">
                                <asp:Label ID="lbl_Adjustamount" runat="server" Font-Bold="true"></asp:Label>
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



            <div class="mainautot">
                <div class="mainwith" style="margin: 70px 0px 5px 0px;">
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
                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; display: none">
                                Contact Details :<asp:Label ID="lbl_contact_details1" runat="server"></asp:Label>


                            </div>
                        </div>
                    </div>

                    <div class="slipno">
                        <div class="slipno_left">
                            Receipt No :
                        <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="slipno_middle">
                            <b>Fees Receipt (Student copy)</b>
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
                            <p>Roll</p>
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
    </form>
</body>
</html>
