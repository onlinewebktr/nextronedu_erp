<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback_one.aspx.cs" Inherits="school_web.Feedback.Feedback_one" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feedback </title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet" />
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/feedback.css" rel="stylesheet" />
    <style>
        .pointer_event {
            pointer-events: none;
            background-color: #ababab;
        }

        .notificationpan {
            background-color: #d07e09 !important;
        }
        .workinghny-block-grid {
    border-radius: 5px;
    box-shadow: 2px 9px 49px -17px rgb(0 0 0 / 10%);
    height: auto;
    max-width: 444px;
    width: 100%;
    margin: 20px auto;
    background: #fff;
    padding: 40px 35px 80px 35px;
}
        

        .Parent input[type=radio] {
            height: 32px;
            position: relative;
            top: 2.6px;
            left: 0px;
            margin: 0px 8px 0px 0px;
            z-index: 9999;
            width: 30px;
        }

        .student input[type=radio] {
            height: 32px;
            position: relative;
            top: 2.6px;
            left: 0px;
            margin: 0px 8px 0px 0px;
            z-index: 9999;
            width: 30px;
        }

        .Parent label {
            margin-top: 8px;
            float: right;
            font-size: 15px;
        }

        /*.Parent .loginull_radio {
            height: auto !important;
            width: 180px!important;
            margin: 0px 0px 0px 0px !important;
            padding: 0px 0px 0px 0px !important;
            display: inline-flex;
            font-size: 15px;
            line-height: 20px;
            text-align: left;
            float: left;
        }*/
        /* .loginull_radio {
            height: auto !important;
            width: 185px;
        }*/
    </style>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
         <section class="intslogin-form">
            <div class="workinghny-form-grid">
                <div class="wrapper">
                    <div id="notification">
                        <div id="pan" class="notificationpan">
                            <div style="float: left; height: auto;">
                                <asp:Label ID="msg1" runat="server" ForeColor="White" Style="font-size: 21px;"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="workinghny-block-grid">
                        <div class="form-right-inf">
                            <div class="logoschool">
                                <img src="images/logo.jpg" id="schoollogo" runat="server" class="mainlogo" alt="Logo in school" title="Logo in school" />
                                <asp:Label class="schoolnamenn" ID="lbl_schoolname" runat="server"></asp:Label>
                                <br />
                                <asp:Label class="schoolnamenn" ID="Label1" runat="server">Feedback Form</asp:Label>
                            </div>


                            <div class="checkboxx">
                                <ul class="loginull">
                                    <li class="loginull_li">
                                        <asp:RadioButton ID="rd_student" runat="server" Checked="true" GroupName="ab" AutoPostBack="true" Text="Own Student" CssClass="loginull_radio" OnCheckedChanged="rd_student_CheckedChanged" />
                                    </li>
                                    <li class="loginull_li">
                                        <asp:RadioButton ID="rd_otherstudent" runat="server" GroupName="ab" AutoPostBack="true" Text="Other" CssClass="loginull_radio" OnCheckedChanged="rd_otherstudent_CheckedChanged" />
                                    </li>

                                </ul>
                            </div>
                            <asp:Panel ID="pnl_login_other" runat="server" Visible="false">
                                <div class="login-form-content">
                                    <div action="#" class="signin-form">

                                        <div class="one-frm">
                                            <asp:TextBox CssClass="input1" ID="txt_othername" type="text" runat="server" placeholder="Enter Name"></asp:TextBox>
                                        </div>

                                        <div class="one-frm">
                                            <asp:TextBox ID="txt_mobile_no" CssClass="input1" type="text" runat="server" placeholder="Enter Mobile No." onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                        </div>

                                        <%-- <label class="check-remaind">
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                            <span class="checkmark"></span>
                                            <p class="remember">Are You Parents</p>
                                        </label>--%>

                                        <div class="checkboxx">
                                            <ul class="loginull">

                                                <li class="loginull_li">
                                                    <asp:RadioButton ID="rd_parentsother" runat="server" GroupName="ab1" Text="Are you Parent?" CssClass="Parent" Checked="true" />
                                                </li>
                                                <li class="loginull_li">
                                                    <asp:RadioButton ID="rd_studentother" runat="server" Checked="true" GroupName="ab1" Text="Are you Student?" CssClass="Parent" />
                                                </li>

                                            </ul>
                                        </div>

                                        <asp:Button class="btn btn-style mt-3" ID="btn_other_studentfeedback" runat="server" Text="Your Feedback" OnClick="btn_other_studentfeedback_Click" />



                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pbl_own_student" runat="server" Visible="true">
                                <div class="login-form-content">
                                    <div action="#" class="signin-form">

                                        <div class="one-frm">
                                            <asp:TextBox ID="txt_admission_no" CssClass="input1" type="text" runat="server" placeholder="Enter Admission No."></asp:TextBox>
                                        </div>
                                        <asp:Button class="btn btn-style mt-3" ID="btn_find_admission_no" runat="server" Text="Find" OnClick="btn_find_admission_no_Click" />


                                        <asp:Panel ID="pnl_ownstudentdata" runat="server" Visible="false">
                                            <div class="one-frm">
                                                <asp:TextBox ID="txt_own_name" CssClass="input1" type="text" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="one-frm">
                                                <asp:TextBox ID="txt_mobile_own_mobile" CssClass="input1" type="text" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="checkboxx">
                                                <ul class="loginull">

                                                    <li class="loginull_li">
                                                        <asp:RadioButton ID="rd_parents_own" runat="server" GroupName="ab1" Text="Are you Parent?" CssClass="Parent" Checked="true" />
                                                    </li>
                                                    <li class="loginull_li">
                                                        <asp:RadioButton ID="rd_student_own" runat="server" Checked="true" GroupName="ab1" Text="Are you Student?" CssClass="Parent" />
                                                    </li>

                                                </ul>
                                            </div>

                                            <asp:Button class="btn btn-style mt-3" ID="btn_own_studentfeedback" runat="server" Text="Your Feedback" OnClick="btn_own_studentfeedback_Click" />
                                        </asp:Panel>




                                    </div>
                                </div>
                            </asp:Panel>
                            
                        </div>
                    </div>
                </div>

            </div>
            <!-- copyright-->
            <div class="copyright text-center">
                <div class="wrapper">
                    <p class="copy-footer-29">
                        ©2020 All rights reserved | Design by: &nbsp; <a href="#" target="_blank" id="copyright">
                            <asp:Label ID="copyright1" runat="server"></asp:Label>
                        </a>
                    </p>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
