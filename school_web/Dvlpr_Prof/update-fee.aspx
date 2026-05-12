<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="update-fee.aspx.cs" Inherits="school_web.Dvlpr_Prof.update_fee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Update Fee
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
                        Update Fee 
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-5">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Update Fee  </h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="form-group col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <label>
                                        Select Session</label>
                                    <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <label>Select Payment Type</label>
                                    <asp:DropDownList ID="ddl_payment_type" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">Admission</asp:ListItem>
                                        <asp:ListItem Value="2">Annual</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <asp:Button ID="btn_update_fee" runat="server" OnClick="btn_update_fee_Click" class="btn btn-sm btn-success" Text="Update Session" Style="margin: 10px 0px 0px 0px;" />
                                </div>

                                <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
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
