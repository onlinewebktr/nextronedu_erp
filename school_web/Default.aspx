<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="school_web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="keywords" content="Welcome To Nextron School ERP" />
    <meta name="description" content="Welcome To Nextron School ERP" />
    <title>School ERP | Login</title>
    <link href="login/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="body">
            <div class="container">
                <!-- Left Info Section -->
                <section class="info-panel">
                    <div class="info-content">
                        <img src="login/logo.png" alt="School Logo" class="logo" style="border-radius: 4px" />
                        <h1>Empowering Your School with <span>School ERP</span></h1>
                        <p>
                            A unified platform to streamline academics, administration, and communication —
          designed to make your school smarter, faster, and more connected.
                        </p>
                        <div class="features">
                            <div class="feature">
                                <h3>📘 Academic Management</h3>
                                <p>Organize classes, grades, and schedules effortlessly.</p>
                            </div>
                            <div class="feature">
                                <h3>💰 Finance & Billing</h3>
                                <p>Track fees and generate reports in real time.</p>
                            </div>
                            <div class="feature">
                                <h3>📈 Smart Analytics</h3>
                                <p>Gain insights into student performance and operations.</p>
                            </div>
                        </div>



                    </div>
                </section>

                <!-- Right Login Section -->
                <section class="login-panel">
                    <div class="login-card">


                        <div style="float: left; width: 100%; text-align: center; margin: 0px 0px 20px 0px;">
                            <img src="login/logo2.png" alt="School Logo" style="width: 130px; margin: 0px auto;" id="school_logo" runat="server" />
                        </div>


                        <div class="multiple-branch-sec">
                            <div class="row">
                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" id="extraSpace" runat="server"></div>
                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="mltiple-txt-devide" runat="server" id="branchBxDV">
                                            <asp:Label ID="lbl_branch_dv" runat="server" class="branch-deactive">
                                                <a href="<%#Eval("Branch_link") %>">
                                                    <div class="multiple-branch-bx-sec">
                                                        <%--<div class="multiple-branch-bx-logo">
                                                            <img src="<%#Eval("Logo") %>" />
                                                        </div>--%>
                                                        <asp:Label ID="lbl_branch_name" class="multiple-branch-bx-branch-h" runat="server" Text='<%#Bind("Branch_nme")%>'></asp:Label>
                                                        <asp:Label ID="lbl_firm_id" Visible="false" runat="server" Text='<%#Bind("Firm_id")%>'></asp:Label>
                                                    </div>
                                                </a>
                                            </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <h2>Welcome Back</h2>
                        <p class="subtitle">Sign in to access your ERP dashboard</p>
                        <asp:Panel ID="pnl_login" runat="server">
                            <div>
                                <div class="form-group">
                                    <label for="username">Username</label>
                                    <asp:TextBox ID="txt_user_id" runat="server" placeholder="Enter username"></asp:TextBox>
                                </div>
                                <div class="form-group password-group">
                                    <label for="password">Password</label>
                                    <div class="password-wrapper">
                                        <asp:TextBox ID="txt_pwd" runat="server" TextMode="Password" placeholder="Enter password"></asp:TextBox>
                                        <button type="button" id="togglePassword" class="toggle-password">Show</button>
                                    </div>
                                </div>
                                <asp:Button ID="btn_login" runat="server" Text="Login" class="login-btn" OnClick="btn_login_Click" />
                                <asp:Label runat="server" CssClass="contmessage33" ID="lbl_error"></asp:Label>
                            </div>
                        </asp:Panel>

                        <asp:HiddenField ID="hd_whatsapp_msg_otp" runat="server" />
                        <asp:HiddenField ID="hd_text_msg_otp" runat="server" />
                        <asp:HiddenField ID="hd_mail_msg_otp" runat="server" />
                        <asp:Panel ID="pnl_otp" runat="server" Visible="false">
                            <div class="login-form-content">
                                <div action="#" class="signin-form">
                                    <div class="one-frm">
                                        <p style="font-size: 15px; margin: 20px 0px 5px 0px; float: left;">
                                            Please enter the OTP
                                        </p>
                                        <div>
                                            <asp:TextBox ID="txt_otp" class="useridsec-box" type="text" MaxLength="6" runat="server" placeholder="Enter OTP" Style="border: 1px solid #ddd; border-radius: 3px !important; padding: 0px 10px 0px 10px; height: 40px; font-size: 13px; width: 100%"></asp:TextBox>
                                            <div style="margin: 10px 0px 15px 0px; padding: 0px; width: 100%; float: left">
                                                <div class="otp-time-remaining-dv">
                                                    <p class="otp-time-remaining-dv-p">
                                                        Time Remaining :
                                                <asp:Label ID="timerLabel" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="otp-resnd-btn-dv">
                                                    <asp:LinkButton ID="lnk_resend_otp" runat="server" class="resend-otp-btn" OnClick="lnk_resend_otp_Click">Resend OTP</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <asp:Button class="login-btn" ID="btn_otp_login" runat="server" Text="Submit" OnClick="btn_otp_login_Click" Style="margin: 0px 0px 0px 0px;" />
                                    <asp:Label ID="lbl_otp_message" CssClass="loginclass" runat="server" Style="float: left; color: #f00; width: 100%;"></asp:Label>

                                    <ul class="forgotpass">
                                        <li><a href="Default.aspx">Back</a></li>
                                    </ul>
                                </div>
                            </div>
                        </asp:Panel>



                        <ul class="pptcrp" style="display: none">
                            <li style="padding-left: 0px"><a href="https://nnierp.nextronsoft.com/doc/privacy-policy.pdf" target="_blank">Privacy Policy</a></li>
                            <li><a href="https://nnierp.nextronsoft.com/doc/refund-polic.pdf" target="_blank">Refund Policy</a></li>
                            <li style="padding-right: 0px"><a href="https://nnierp.nextronsoft.com/doc/terms-and-conditions.pdf" target="_blank">Terms & Conditions</a></li>
                        </ul>
                    </div>
                    <footer>
                        <p>© 2025 NextronSoft. All rights reserved.</p>
                    </footer>
                </section>
            </div>
        </div>

        <style>
            .pptcrp {
                margin: 10px 0px 0px 0px;
                padding: 0px;
                width: 100%;
                float: left;
                text-align: center;
            }

                .pptcrp li {
                    margin: 0px;
                    padding: 0px 2px;
                    display: inline;
                    list-style-type: none;
                }

                    .pptcrp li a {
                        margin: 0px;
                        padding: 0px;
                        font-size: 14px;
                        text-decoration: none;
                        font-weight: 500;
                    }
        </style>
        <asp:HiddenField ID="hd_doule_authentication" runat="server" />
        <script type="text/javascript">
            // Show/Hide Password Toggle
            const togglePassword = document.getElementById("togglePassword");
            const passwordField = document.getElementById("txt_pwd");

            togglePassword.addEventListener("click", () => {
                const type = passwordField.getAttribute("type") === "password" ? "text" : "password";
                passwordField.setAttribute("type", type);
                togglePassword.textContent = type === "password" ? "Show" : "Hide";
            });


            function startCountdown(timeLeft) {
                var interval = setInterval(countdown, 1000);
                update();
                function countdown() {
                    if (--timeLeft > 0) {
                        update();
                        $("#<%=lnk_resend_otp.ClientID %>").addClass("desabledClass");
                    }
                    else {
                        clearInterval(interval);
                        update();
                        completed();
                    }
                }

                function update() {
                    hours = Math.floor(timeLeft / 3600);
                    minutes = Math.floor((timeLeft % 3600) / 60);
                    seconds = timeLeft % 60;
                    document.getElementById("<%=timerLabel.ClientID%>").innerText = seconds;
                }
                function completed() {
                    $("#<%=lnk_resend_otp.ClientID %>").removeClass("desabledClass");
                    $("#<%=lnk_resend_otp.ClientID %>").addClass("enabledClass");
                }
            }

            function showmessage() {
                document.getElementById("pan").style.display = "block";
            }
        </script>


        <div style="margin: 0px; padding: 0px; height: 0px; width: 100%; overflow: hidden; float: left;">
            <asp:Panel ID="pnl_otp_mail" runat="server">
                <div style="margin: 0px; padding: 50px 0px; width: 100%; float: left; background: #FAFAFA;">
                    <div style="margin: 0 auto; padding: 0; width: 600px; height: auto;">
                        <div style="margin: 0; padding: 25px 20px; width: 100%; height: auto; float: left; background: #ffffff; border-bottom: 2px solid #eaeaea;">
                            <div style="margin: 0px 0px 25px 0px; padding: 0px 0px 15px 0px; width: 100%; float: left; text-align: center; border-bottom: 1px solid #ddd;">
                                <img src="images/logo.jpg" id="Img1OTO" runat="server" style="height: 100px;" />
                                <asp:Label ID="lbl_school_name_otp" runat="server" Style="font-weight: 600; margin: 0px 0px 5px 0px; padding: 0px; width: 100%; float: left; font-family: 'Google Sans', Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; line-height: 25px;"></asp:Label>
                            </div>
                            <div style="margin: 0px; padding: 0px; width: 100%; float: left;">
                                <p style="font-weight: 600; margin: 0px 0px 5px 0px; padding: 0px; width: 100%; float: left; font-family: 'Google Sans', Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; line-height: 25px; color: #444b5d;">
                                    Hello!
                                </p>
                                <p style="margin: 0px 0px 5px 0px; padding: 0px; width: 100%; float: left; font-family: 'Google Sans', Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; line-height: 29px; color: #444b5d;">
                                    Your One-Time Password (OTP) for logging into your account is 
                                    <asp:Label ID="lbl_otp" Style="font-weight: 600" runat="server"></asp:Label>. Please do not share this OTP with anyone. 
                                     
                                     <br />
                                    <br />
                                    Regards  :  
                                    <asp:Label ID="lbl_firm_name" Style="font-weight: 600" runat="server"></asp:Label>
                                    <br />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
