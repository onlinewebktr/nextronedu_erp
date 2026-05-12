<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Create_Live_Class_Credentials.aspx.cs" Inherits="school_web.LMS_VC_Admin.Create_Live_Class_Credentials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Create Live Class Credentials
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        label {
            display: inline-block;
            margin-bottom: 0.5rem;
            margin: 9px 0px 3px 0px !important;
        }

        a {
            color: #3f6ad8;
            text-decoration: none;
            background-color: transparent;
            padding: 3px 7px 5px 7px;
            text-decoration: none;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Create Live Class Credentials</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Create Live Class Credentials</h5>
                        <div class="form-row">
                            <div class="col-md-12">


                                 <a href="doc/100MS_Online_Meeting_Help_Documents.pdf" target="_blank">How to create account</a>
                                <div class="position-relative form-group">
                                    <label>
                                        Templates Name<sup>*</sup>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_templates_name"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_templates_name" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>
                                        Templates Id<sup>*</sup>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Templates_id"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_Templates_id" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="position-relative form-group">
                                    <label>
                                        Room Id<sup>*</sup>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_rommid"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_rommid" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>
                                        Email Id<sup>*</sup>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_emailid"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_emailid" class="form-control" runat="server" type="email" required placeholder="Enter a valid email address"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="position-relative form-group">
                                    <label>
                                        Password <sup>*</sup>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Password"></asp:RequiredFieldValidator></label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_Password" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Access Key<sup>*</sup><asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Password"></asp:RequiredFieldValidator></label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_accesskey" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>App Secret Key<sup>*</sup><asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_app_secretkey"></asp:RequiredFieldValidator></label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_app_secretkey" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="card-footer pull-right">
                                    <asp:Button ID="btn_submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_submit_Click" ValidationGroup="a" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">All Added Live Class Credentials  </h5>

                       
                    
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Template Name</th>
                                    <th>Template Id</th>
                                    <th>Room Id</th>
                                    <th>Email Id</th>
                                    <th>Password </th>
                                    <th>Is Status </th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_Template_Name" runat="server" Text='<%#Bind("Template_Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_TemplateID" runat="server" Text='<%#Bind("TemplateID") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_RoomId" runat="server" Text='<%#Bind("RoomId") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_EmailId" runat="server" Text='<%#Bind("EmailId") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Password100ms" runat="server" Text='<%#Bind("Password100ms") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnk_status" runat="server" OnClick="lnk_status_Click" OnClientClick="return confirm('Are you sure you want to update?');" CausesValidation="false"></asp:LinkButton>

                                                <asp:Label ID="lbl_iStatus" runat="server" Visible="false" Text='<%#Bind("Status")%>'></asp:Label>
                                                <asp:Label ID="lbl_AccessKey" runat="server" Visible="false" Text='<%#Bind("AccessKey")%>'></asp:Label>
                                                <asp:Label ID="lbl_AppSecret" runat="server" Visible="false" Text='<%#Bind("AppSecret")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>



                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
