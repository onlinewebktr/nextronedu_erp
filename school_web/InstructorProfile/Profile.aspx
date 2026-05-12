<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="school_web.InstructorProfile.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    My Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="css/profile.css" rel="stylesheet" />
    <style>
        .container {
            min-height:550px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="main-body">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 235px; height: auto;">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                    <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" alt="" />
                </div>
            </div>


            <div class="row gutters-sm">
                <div class="col-md-4 mb-3">
                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex flex-column align-items-center text-center">
                                <img src="https://bootdey.com/img/Content/avatar/avatar7.png" class="rounded-circle" width="150" id="img" runat="server">
                                <div class="mt-3">
                                    <h4> <asp:Label ID="lbl_name" runat="server"></asp:Label></h4>
                                   
                                    

                                    <asp:LinkButton ID="lnk_edit" runat="server" class="btn btn-primary" OnClick="lnk_edit_Click"><i class="pe-7s-note"></i>Edit</asp:LinkButton>

                                    <%-- <button class="btn btn-outline-primary">Message</button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card mt-3" style="display: none">
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-globe mr-2 icon-inline">
                                        <circle cx="12" cy="12" r="10"></circle><line x1="2" y1="12" x2="22" y2="12"></line><path d="M12 2a15.3 15.3 0 0 1 4 10 15.3 15.3 0 0 1-4 10 15.3 15.3 0 0 1-4-10 15.3 15.3 0 0 1 4-10z"></path></svg>Website</h6>
                                <span class="text-secondary">https://bootdey.com</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-github mr-2 icon-inline">
                                        <path d="M9 19c-5 1.5-5-2.5-7-3m14 6v-3.87a3.37 3.37 0 0 0-.94-2.61c3.14-.35 6.44-1.54 6.44-7A5.44 5.44 0 0 0 20 4.77 5.07 5.07 0 0 0 19.91 1S18.73.65 16 2.48a13.38 13.38 0 0 0-7 0C6.27.65 5.09 1 5.09 1A5.07 5.07 0 0 0 5 4.77a5.44 5.44 0 0 0-1.5 3.78c0 5.42 3.3 6.61 6.44 7A3.37 3.37 0 0 0 9 18.13V22"></path></svg>Github</h6>
                                <span class="text-secondary">bootdey</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-twitter mr-2 icon-inline text-info">
                                        <path d="M23 3a10.9 10.9 0 0 1-3.14 1.53 4.48 4.48 0 0 0-7.86 3v1A10.66 10.66 0 0 1 3 4s-4 9 5 13a11.64 11.64 0 0 1-7 2c9 5 20 0 20-11.5a4.5 4.5 0 0 0-.08-.83A7.72 7.72 0 0 0 23 3z"></path></svg>Twitter</h6>
                                <span class="text-secondary">@bootdey</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-instagram mr-2 icon-inline text-danger">
                                        <rect x="2" y="2" width="20" height="20" rx="5" ry="5"></rect><path d="M16 11.37A4 4 0 1 1 12.63 8 4 4 0 0 1 16 11.37z"></path><line x1="17.5" y1="6.5" x2="17.51" y2="6.5"></line></svg>Instagram</h6>
                                <span class="text-secondary">bootdey</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-facebook mr-2 icon-inline text-primary">
                                        <path d="M18 2h-3a5 5 0 0 0-5 5v3H7v4h3v8h4v-8h3l1-4h-4V7a1 1 0 0 1 1-1h3z"></path></svg>Facebook</h6>
                                <span class="text-secondary">bootdey</span>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-8" style="margin: 17px 0px 0px 0px;">
                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-4">
                                    <h6 class="mb-0">Full Name</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                    <asp:Label ID="lbl_name1" runat="server"></asp:Label>
                                    <asp:TextBox ID="txt_name" runat="server" Visible="false" style="width: 300px;"> </asp:TextBox>
                                </div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col-sm-4">
                                    <h6 class="mb-0">Email</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                    <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                    <asp:TextBox ID="txt_emailid" runat="server" type="email" Visible="false" style="width: 300px;"> </asp:TextBox>
                                </div>
                            </div>
                            <hr>


                            <div class="row">
                                <div class="col-sm-4">
                                    <h6 class="mb-0">Mobile</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                    <asp:Label ID="lbl_mobile" runat="server"></asp:Label>
                                    <asp:TextBox ID="txt_mobileno" runat="server" onkeypress="return isNumberKey(event)" Visible="false" MaxLength="10" style="width: 300px;"> </asp:TextBox>
                                </div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col-sm-4">
                                    <h6 class="mb-0">Address</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                    <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" Style="height: 100px;width: 300px;" Visible="false"> </asp:TextBox>
                                </div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col-sm-4">
                                    <h6 class="mb-0">About Your Self</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                    <asp:Label ID="lbl_yourself" runat="server"></asp:Label>
                                    <asp:TextBox ID="txt_yourself" runat="server" TextMode="MultiLine" Style="height: 100px;width: 300px;" Visible="false"> </asp:TextBox>
                                </div>

                            </div>
                             <hr>
                               <div class="row">
                                <div class="col-sm-4">
                                    <h6 class="mb-0">Your Signature</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                     <img   width="150" height="50" id="imgsig" runat="server" style="padding:2px; border:1px solid #bdbdbd">
                                </div>

                            </div>


                            <hr>
                            <div class="row" id="a1" runat="server" visible="false">
                                <div class="col-sm-4">

                                    <h6 class="mb-0">Upload Photo (Only jpg,png file size 500kb)</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                                <hr>
                                <div class="col-sm-4">

                                    <h6 class="mb-0">Upload Signatur (Only jpg,png file size 500kb)</h6>
                                </div>
                                <div class="col-sm-8 text-secondary">
                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Button ID="btn_submit" Visible="false" runat="server" Text="Submit" CssClass="mt-2 btn btn-primary pull-right" OnClick="btn_submit_Click" OnClientClick="return confirm('Are you sure want to update ?')" />
                                </div>


                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
</asp:Content>
