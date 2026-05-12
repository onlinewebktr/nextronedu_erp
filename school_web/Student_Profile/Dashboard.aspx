<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/User.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Dashboard.aspx.cs" Inherits="school_web.Student_Profile.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="#">Dashboard</a></li>
    </ol>
    <asp:HiddenField ID="hd_id" runat="server" />
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
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8" style="margin: 0px auto; float: none; border: 1px solid #ccc;">
                        <h2 class="blue_bg">Notice Board</h2>

                        <div class="row">
                            <div style="width: 100%; max-height: 500px; overflow: auto;">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notice Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Heading" runat="server" Text='<%#Bind("Heading") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details" HeaderStyle-Width="400px">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Details" runat="server" Text='<%#Bind("Details") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Posted_Date" runat="server" Text='<%#Bind("Posted_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File"  >
                                            <ItemTemplate>
                                                <a href='<%#Eval("Attachments") %>' download >View Document</a>
                                                <asp:Label ID="lbl_Attachments" runat="server" Text='<%#Bind("Attachments") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#880300" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                </asp:GridView>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

