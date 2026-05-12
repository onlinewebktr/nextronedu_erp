<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback_form.aspx.cs" Inherits="school_web.Feedback.Feedback_form" %>

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
    </style>
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



                            
                                <div class="login-form-content">
                                    <div action="#" class="signin-form">

                                        <div class="one-frm">
                                            <p style="color:#000;text-align: center;"><sup>** (KINDLY PROVIDE RATING UNDER 0 TO 5, HERE “0” IS MINIMUM AND “5” IS MAXIMUM)</sup></p>
                                            <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" style="font-size:18px;">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Question">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_question" runat="server" Text='<%#Bind("Question")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Your Ratting">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_rating" runat="server" CssClass="form_control" style="width:100%">
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>







                                                </Columns>
                                            </asp:GridView>
                                        </div>



                                        <asp:Button class="btn btn-style mt-3" ID="btn_other_studentfeedback" runat="server" Text="Save" OnClick="btn_other_studentfeedback_Click" OnClientClick="return confirm('Are you sure you want to save?');" />



                                    </div>
                                </div>
                            
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
