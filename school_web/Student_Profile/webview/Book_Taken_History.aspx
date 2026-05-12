<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site1.Master" AutoEventWireup="true" CodeBehind="Book_Taken_History.aspx.cs" Inherits="school_web.Student_Profile.webview.Book_Taken_History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullinfo">
        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                    <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" alt="" />
                </div>
            </div>




            <div class="clearfix"></div>
            <div class="texbox-border">


                <div style="margin: 0px; padding: 0px; float: left; width: 100%; overflow: scroll">
                    <asp:GridView ID="GrdViewdata" runat="server" class="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdViewdata_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Book Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NameOfBook" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Book Issue No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_transaction_no" runat="server" Text='<%#Bind("transaction_no")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_book_issue_date" runat="server" Text='<%#Bind("issue_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_book_due_date" runat="server" Text='<%#Bind("due_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Book Return No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Book_reurn_slip_id" runat="server" Text='<%#Bind("Book_reurn_slip_id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_returned_date" runat="server" Text='<%#Bind("returned_date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Book Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Book_status" runat="server" Text='<%#Bind("Book_status")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>




            </div>
            <asp:HiddenField ID="hd_id" runat="server" />

        </div>
    </div>
</asp:Content>
