<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Add_Feeback_Question.aspx.cs" Inherits="school_web.Dvlpr_Prof.Add_Feeback_Question" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Feedback
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-id icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Add Feedback Question</asp:Literal>

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
        <div class="clearfix"></div>
        <div class="main-card mb-3 card">

            <div class="row">
                <div class="col-lg-4" style="display: block">
                    <div class="main-card card">
                        <div class="card-body">
                            <h5 class="card-title"></h5>
                            <div class="form-row">
                                <div class="col-md-12">
                                    <h5 class="card-title"></h5>
                                    <div class="position-relative form-group">
                                        <label>Question<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_question" runat="server" CssClass="form-control" TextMode="MultiLine"> </asp:TextBox>
                                        </div>
                                    </div>

                                     

                                     

                                    <div class="row">
                                        <div class="card-footer col-md-12">
                                            
                                            <div class="col-md-4">
                                                <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-8">
                    <div class="main-card card">
                        <div class="card-body">
                            <%--<h5 class="card-title">All Added Teacher </h5>--%>
                            <asp:HiddenField ID="hdid" runat="server" />
                            <asp:HiddenField ID="hdpwd" runat="server" />
                            <asp:HiddenField ID="hd_UserID" runat="server" />



                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Sl. No.</th>
                                        <th>Questions</th>
                                         
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RPDetails" runat="server" >
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_Question" runat="server" style="word-break:break-all;" Text='<%#Bind("Question") %>'></asp:Label>
                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                </td>
                                               
                                                


                                                <td>
                                                    <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                        <div class="btn-group dropdown">
                                                            <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                                <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                            </button>
                                                            <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                                <asp:LinkButton ID="lnkDel" runat="server" CssClass="dropdown-item" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete this?');" CausesValidation="false">Delete</asp:LinkButton>
                                                                <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><span>Edit</span></asp:LinkButton>



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




    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
