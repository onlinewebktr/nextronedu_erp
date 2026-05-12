<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="update-tinymc-link.aspx.cs" Inherits="school_web.Dvlpr_Prof.update_tinymc_link" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Update TinyMc Link
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        Update Session 
                    </div>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div> 
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Update TinyMc Link</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label>
                                        Enter Link</label>
                                    <asp:TextBox ID="txt_link" class="form-control" runat="server" placeholder="Ex-https://cdn.tiny.cloud/1/klxtoz3pw1dv3s8wd2jv7xcfaptx89e2n56axzkv4882j6f6/tinymce/7/tinymce.min.js "></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" class="btn btn-sm btn-success" Text="Update" Style="margin: 10px 0px 0px 0px;" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
