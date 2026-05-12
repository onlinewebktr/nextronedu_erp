<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/User.Master" AutoEventWireup="true" CodeBehind="View_Profile.aspx.cs" Inherits="school_web.Student_Profile.View_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Your Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a> <i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item">Your Profile</li>
    </ol>
    <div id="notification">
        <div id="pan" class="notificationpan">
            <div style="float: left; width: 235px; height: auto;">
                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
            </div>
            <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                class="closenotificationpan" alt="" />
        </div>
    </div>
    <div class="grid-form">
        <div class="grid-form1">
            <div class="panel-body">
                <div class="row" style="padding: 0px 0px 0px 0px;">
                    <div style="margin: 0px 0px 10px 0px; padding: 0px; float: left; width: 100%; height: auto;">
                        <h2 class=" blue_bg">Personal Details </h2>
                        <div class="row form-group">
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                Name :
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txt_Name" runat="server" class="textcolor" Style="width: 100%;"
                                    ReadOnly="true" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12" style="padding-bottom: 10px">
                                Father Name:
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12" style="padding-bottom: 10px">
                                <asp:TextBox ID="txt_fathername" runat="server" class="textcolor" Width="100%" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                Mother Name :
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txt_mothername" runat="server" class="textcolor" Width="100%" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                Mobile Number :
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txt_mobilenumber" runat="server" class="textcolor" Enabled="false" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                Gender :
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:DropDownList ID="ddl_Gender" runat="server" CssClass="form-control" Style="color: Green; font-weight: bold; width: 100%;">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3 col-sm-6 col-xs-12">
                                City :
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txt_City" runat="server" class="textcolor"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-md-3 col-sm-6 col-xs-12">Download Profile Photo :</div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:Image ID="Image1" runat="server" />
                                <br />
                                <a id="a1_img" runat="server">Download</a>
                            </div>

                            <div class="col-md-3 col-sm-6 col-xs-12">Upload Profile Photo :</div>
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:FileUpload ID="fl_photo" runat="server" class="form-control" />
                                <asp:HiddenField ID="hd_photo" runat="server" Value="Not Uploaded" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                    runat="server" ControlToValidate="fl_photo"
                                    ErrorMessage="Invalid File. Please upload a File with extension:  png, jpg, jpeg" ForeColor="Red"
                                    ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG)$"
                                    ValidationGroup="A" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:Button ID="btn_photo1" runat="server" Text="Upload" CssClass="btn-danger" OnClick="btn_photo1_Click" />
                            </div>
                        </div>
                    </div>
                    <%--<div class="row form-group">
                        <asp:Button ID="btn_update" CssClass="btn btn-success " Text="Update" runat="server" Style="width: 100px; height: 35px;"
                            ValidationGroup="A" />
                    </div>--%>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
