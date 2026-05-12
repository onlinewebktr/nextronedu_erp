<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reprint-payment-voucher-all.aspx.cs" Inherits="school_web.Admin.slip.reprint_payment_voucher_all" %>

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
            width: 1100px;
        }

        .mainwith {
            margin: 0px 0px 0px 0px;
            padding: 5px;
            height: 794.5px;
            width: 510px;
            float: left;
            border: 1px solid #000;
        }

        .mainwithsss {
            margin: 0px 0px 33.1px 15px;
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
            margin: 10px 0px 0px 0px;
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
            padding: 12px 0px 6px 0px;
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
            width: 40%;
            float: left;
            text-align: center;
        }

        .slipno_right {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 30%;
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
            margin: 0px 0px 0px 0px;
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
            margin: 40px 0px 15px 0px;
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

        .admissionno-p {
            margin: 12px 0px 12px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            line-height: 15px;
        }

            .admissionno-p span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 80%;
                display: inline-block;
                border-bottom: 1px dashed #323232;
                font-weight: 600;
            }

        .class-p {
            margin: 12px 0px 12px 0px;
            padding: 0px;
            width: 50%;
            float: left;
            font-size: 14px;
            line-height: 15px;
        }

            .class-p span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 77%;
                display: inline-block;
                border-bottom: 1px dashed #323232;
                font-weight: 600;
            }

        .section-p {
            margin: 12px 0px 12px 0px;
            padding: 0px;
            width: 24%;
            float: left;
            font-size: 14px;
            line-height: 15px;
        }

            .section-p span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 52%;
                display: inline-block;
                border-bottom: 1px dashed #323232;
                font-weight: 600;
            }

        .namesss-p {
            margin: 12px 0px 12px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            line-height: 15px;
        }

            .namesss-p span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 90%;
                display: inline-block;
                border-bottom: 1px dashed #323232;
                font-weight: 600;
            }

        .fthers-namesss-p {
            margin: 12px 0px 12px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            line-height: 15px;
        }

            .fthers-namesss-p span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 78%;
                display: inline-block;
                border-bottom: 1px dashed #323232;
                font-weight: 600;
            }

        .phones-no-p {
            margin: 12px 0px 12px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            line-height: 15px;
        }

            .phones-no-p span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 84%;
                display: inline-block;
                border-bottom: 1px dashed #323232;
                font-weight: 600;
            }

        .roll-no-p {
            margin: 12px 0px 12px 10px;
            padding: 0px;
            width: 24%;
            float: left;
            font-size: 14px;
            line-height: 15px;
        }

            .roll-no-p span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                width: 47%;
                display: inline-block;
                border-bottom: 1px dashed #323232;
                font-weight: 600;
            }

        @media print {
            .noPrint {
                display: none;
            }
        }

        .fees-amt-dv {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 50px;
            width: 40%;
            float: left;
        }

        .fees-amt-dv-p {
            margin: 0px 0px 0px 0px;
            padding: 13px 0px 3px 0px;
            width: 100%;
            float: left;
            font-weight: 600;
            font-size: 14px;
            border-bottom: 1px dashed #323232;
            text-align: center;
        }

        .fees-amt-dvp {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            width: 50%;
            float: left;
        }

        .fee-months-name {
            margin: 0px 0px 0px 0px;
            padding: 7px 0px 10px 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            font-weight: 600;
            text-align: right;
        }

            .fee-months-name span {
                margin: 0px 0px 0px 0px;
                padding: 0px 0px 0px 0px;
            }

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

        .prevous-duess {
            margin: 4px 0px 0px 0px;
            height: 20px;
        }

        .notedvs {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 5px;
            width: 100%;
            float: left;
        }

        .notedvs-note {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            width: 100%;
            float: left;
            font-weight: 600;
        }

        .notedvs-note-p {
            margin: 5px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            width: 100%;
            float: left;
            font-size: 13px;
            color: #000;
            line-height: 19px;
        }

        /*.notedvs-note-p span {
                                margin: 0px 0px 0px 0px;
                                padding: 0px 0px 0px 0px;
                            }*/

        .notedvs-note-p-no {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            width: auto;
            float: left;
        }

        .notedvs-note-p-txt {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 10px;
            width: 92%;
            float: left;
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

            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                <ItemTemplate>
                    <div class="mainautot">
                        <div class="mainwith">
                            <asp:Label ID="lbl_Header_images" runat="server" Text='<%#Eval("Header_images") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lbl_Is_slip_header" runat="server" Text='<%#Eval("Is_slip_header") %>' Visible="false"></asp:Label>

                            <div class="top" style="display: none">
                                <div class="topcell_left">
                                    Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true" Text='<%#Eval("Affiliation") %>'></asp:Label>
                                </div>
                                <div class="topcell_right" style="display: none">
                                    School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true" Text='<%#Eval("school_no") %>'></asp:Label>
                                </div>
                            </div>
                            <asp:Panel ID="textheader" runat="server">
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

                            
                            <asp:Label ID="lbl_website" runat="server" Style="display: none"></asp:Label>
                                        </div>
                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; display: none">
                                            Contact Details :<asp:Label ID="lbl_contact_details" runat="server" Text='<%#Eval("contact_no") %>'></asp:Label>
                                        </div>

                                        <div style="margin: 10px 0px 10px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; font-weight: 600;">
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="printheader" runat="server">
                                <div class="heading" style="text-align: center">

                                    <asp:Image ID="img_header" runat="server" Style="width: 500px; height: 96px;" />

                                </div>
                            </asp:Panel>


                            <div class="slipno">
                                <div class="slipno_left">
                                    No :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true" Text='<%#Eval("Slip_id") %>'></asp:Label>
                                </div>
                                <div class="slipno_middle">
                                    <b>Payment Receipt (Office copy)</b>
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
                            <asp:Label ID="lbl_phone_no" runat="server" Text='<%#Eval("father_mob") %>'></asp:Label>
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
                                        Tuition Fee 
                            (<asp:Label ID="lbl_fee_months" runat="server" Text='<%#Eval("Months") %>'></asp:Label>)
                                    </p>
                                    <p class="fee-months-name">
                                        Late Fine till Date
                                    </p>
                                    <p class="fee-months-name">
                                        Previous Due
                                    </p>

                                    <p class="fee-months-name">
                                        Total
                                    </p>


                                </div>

                                <div class="fees-amt-dv">
                                    <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232;">
                                        ₹
                                <asp:Label ID="lbl_fee_rupee" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                    </p>
                                    <p class="fees-amt-dv-p prevous-duess" style="border-bottom: 0px dashed #323232;">
                                        <%--₹--%>
                                        <asp:Label ID="Label2" runat="server" Style="display: none"></asp:Label>
                                    </p>
                                    <p class="fees-amt-dv-p prevous-duess">
                                        <%--₹--%>
                                        <asp:Label ID="lbl_prev_dues" runat="server" Style="display: none" Text='<%#Eval("Previous_dues") %>'></asp:Label>
                                    </p>
                                    <p class="fees-amt-dv-p prevous-duess" style="border-bottom: 0px dashed #323232;">
                                        <%-- ₹--%>
                                        <asp:Label ID="lbl_ttl_amts" runat="server" Style="display: none" Text='<%#Eval("Total_amount") %>'></asp:Label>
                                    </p>


                                </div>

                            </div>

                            <div style="margin: 0px auto; padding: 6px 0px 6px 0px; float: left; height: auto; width: 70%; font-weight: bold; font-size: 21px; border: 1px solid; text-align: center; background-color: #c4c2c2;">
                                Payment to be made within
                       <asp:Label ID="lbl_pay_date" runat="server" Text='<%#Eval("Last_date_of_payment") %>'></asp:Label>

                            </div>

                            <div class="footer_auth_sig">
                                <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Signature of Depositor</b>
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




                        <div class="mainautot">
                            <div class="mainwith mainwithsss">
                                <div class="top" style="display: none">
                                    <div class="topcell_left">
                                        Affiliation No :
                        <asp:Label ID="lbl_affiliation_no1" runat="server" Font-Bold="true" Text='<%#Eval("Affiliation") %>'></asp:Label>
                                    </div>
                                    <div class="topcell_right" style="display: none">
                                        School No. :
                        <asp:Label ID="lbl_schoolno1" runat="server" Font-Bold="true" Text='<%#Eval("school_no") %>'></asp:Label>
                                    </div>
                                </div>
                                <asp:Panel ID="textheader1" runat="server">

                                    <div class="heading">
                                        <div class="leftlogoheading">
                                            <img src="<%#Eval("logo") %>" style="height: 100px; width: 100px" />
                                        </div>
                                        <div class="righttextheading">
                                            <h1 style="margin: 23px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 16px; line-height: 27px; text-decoration: underline;">
                                                <asp:Label ID="lbl_heading1" runat="server" Text='<%#Eval("firm_name") %>'></asp:Label>


                                            </h1>

                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                <asp:Label ID="lbl_address1" runat="server" Text='<%#Eval("address1") %>'></asp:Label>


                                            </div>
                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                Email Id. :<asp:Label ID="lbl_emaiid1" runat="server" Text='<%#Eval("email") %>'></asp:Label>

                                                &nbsp;&nbsp;

                            
                            <asp:Label ID="lbl_website1" Style="display: none" runat="server" Text='<%#Eval("website") %>'></asp:Label>
                                            </div>
                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; display: none">
                                                Contact Details :<asp:Label ID="lbl_contact_details1" runat="server" Text='<%#Eval("contact_no") %>'></asp:Label>
                                            </div>

                                            <div style="margin: 10px 0px 10px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; font-weight: 600;">
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="printheader1" runat="server">
                                    <div class="heading" style="text-align: center">

                                        <asp:Image ID="img_header1" runat="server" Style="width: 500px; height: 96px;" />

                                    </div>
                                </asp:Panel>
                                <div class="slipno">
                                    <div class="slipno_left">
                                        No :
                        <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true" Text='<%#Eval("Slip_id") %>'></asp:Label>
                                    </div>
                                    <div class="slipno_middle">
                                        <b>Payment Receipt (Student copy)</b>
                                    </div>
                                    <div class="slipno_right">
                                        Date :
                        <asp:Label ID="lbl_paymentdate1" runat="server" Font-Bold="true" Text='<%#Eval("Created_date") %>'></asp:Label>
                                    </div>
                                </div>


                                <div class="studentdetails">
                                    <div class="studentdetails">
                                        <p class="admissionno-p">
                                            Admission No. :
                            <asp:Label ID="lbl_aadmissionno1" runat="server" Text='<%#Eval("Admission_no") %>'></asp:Label>
                                        </p>
                                        <p class="class-p">
                                            Class  :
                            <asp:Label ID="lbl_class1" runat="server" Text='<%#Eval("class") %>'></asp:Label>
                                        </p>
                                        <p class="section-p">
                                            Section  :
                            <asp:Label ID="lbl_section1" runat="server" Text='<%#Eval("Section") %>'></asp:Label>
                                        </p>
                                        <p class="roll-no-p">
                                            Roll No.  :
                            <asp:Label ID="lbl_rollno1" runat="server" Text='<%#Eval("rollnumber") %>'></asp:Label>
                                        </p>
                                        <p class="namesss-p">
                                            Name  :
                            <asp:Label ID="lbl_studentname1" runat="server" Text='<%#Eval("studentname") %>'></asp:Label>
                                        </p>
                                        <p class="fthers-namesss-p">
                                            Father's Name  :
                            <asp:Label ID="lbl_fathername1" runat="server" Text='<%#Eval("fathername") %>'></asp:Label>
                                        </p>
                                        <p class="phones-no-p">
                                            Phone No.  :
                            <asp:Label ID="lbl_phone_no1" runat="server" Text='<%#Eval("mobilenumber") %>'></asp:Label>
                                        </p>
                                        <p class="phones-no-p">
                                            Session  :
                             <asp:Label ID="Label3" runat="server" Text='<%#Eval("session") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <div class="pay_particular">
                                    <div class="fees-amt-dvp">
                                        <p class="fee-months-name">
                                            Tuition Fee 
                            (<asp:Label ID="lbl_fee_months1" runat="server" Text='<%#Eval("Months") %>'></asp:Label>)
                                        </p>

                                        <p class="fee-months-name">
                                            Late Fine till Date
                                        </p>
                                        <p class="fee-months-name">
                                            Previous Due
                                        </p>

                                        <p class="fee-months-name">
                                            Total
                                        </p>


                                    </div>

                                    <div class="fees-amt-dv">
                                        <p class="fees-amt-dv-p" style="border-bottom: 0px dashed #323232;">
                                            ₹
                                <asp:Label ID="lbl_fee_rupee1" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                        </p>
                                        <p class="fees-amt-dv-p prevous-duess" style="border-bottom: 0px dashed #323232;">
                                            <%--₹--%>
                                            <asp:Label ID="Label4" runat="server" Style="display: none"></asp:Label>
                                        </p>
                                        <p class="fees-amt-dv-p prevous-duess">
                                            <%--₹--%>
                                            <asp:Label ID="lbl_prev_dues1" runat="server" Style="display: none" Text='<%#Eval("Previous_dues") %>'></asp:Label>
                                        </p>
                                        <p class="fees-amt-dv-p prevous-duess" style="border-bottom: 0px dashed #323232;">
                                            <%--₹--%>
                                            <asp:Label ID="lbl_ttl_amts1" runat="server" Style="display: none" Text='<%#Eval("Total_amount") %>'></asp:Label>
                                        </p>


                                    </div>
                                </div>
                                <div style="margin: 0px auto; padding: 6px 0px 6px 0px; float: left; height: auto; width: 70%; font-weight: bold; font-size: 21px; border: 1px solid; text-align: center; background-color: #c4c2c2;">
                                    Payment to be made within

                       <asp:Label ID="Label1" runat="server" Text='<%#Eval("Last_date_of_payment") %>'></asp:Label>

                                </div>
                                <div class="footer_auth_sig">
                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%--Checked By--%></b>


                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Sign of Cashier</b>
                                </div>


                                <asp:Label ID="lbl_rd_notes" runat="server">
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
                                </asp:Label>
                            </div>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
