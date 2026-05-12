<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="App_Version.aspx.cs" Inherits="school_web.LMS_VC_Admin.App_Version" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    App Version
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-network icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> App Version</asp:Literal>

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
            <div class="col-lg-4" style="display: block">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Update App Version</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>App Version:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_appversion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>App Version Details:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_versio_detals" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="card-footer pull-right">
                                    <asp:Button ID="btn_submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">App Version Deatils</h5>
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>App Version</th>
                                    <th>App Version Deatils</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_App_Version" runat="server" Text='<%#Bind("App_Version") %>'></asp:Label>




                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("App_Version_Details") %>'  ></asp:Label>
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
      <asp:HiddenField ID="hd_id1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
