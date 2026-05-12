<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="notice-board-details.aspx.cs" Inherits="school_web.Student_Profile.notice_board_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Notice Board
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="grd-wpr">

                                <asp:Repeater ID="RPDetails" runat="server" class="table table-striped table-bordered dataTable" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12" style="margin-top: 4px; border-bottom: 1px dashed #beb7b7; padding-right: 0px; padding-left: 0px;">
                                            <table class="table" style="width: 100%;">
                                                <tr>
                                                    <td style="padding: 2px 0px 2px 0px; border-bottom: 1px solid #beb7b7; font-weight: bold"><i class="fa fa-calendar" aria-hidden="true" style="color: #000"></i>&nbsp;<asp:Label ID="lbl_Posted_Date" Font-Bold="true" runat="server" Text='<%#Bind("Date1") %>'></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 2px 0px 2px 0px;">
                                                        <asp:Label ID="lbl_Details" runat="server" Text='<%#Bind("Notice") %>'></asp:Label>
                                                    </td>

                                                </tr>


                                                <tr>
                                                    <td style="padding: 2px 0px 2px 0px;">
                                                        <asp:Label ID="lbl_Attachments" runat="server" Text='<%#Bind("Attachment") %>' Visible="false"></asp:Label>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <a href='<%#Eval("Attachment") %>' download target="_blank" style="display: block; padding: 0px 0px 0px 0px; font-size: 22px; color: #0066CC; text-decoration: none; float: left;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 2px 0px 2px 0px; text-align: left;">
                                                        <asp:Button ID="btn_submit" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
