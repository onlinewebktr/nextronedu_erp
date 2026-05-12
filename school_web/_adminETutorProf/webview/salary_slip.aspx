<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="salary_slip.aspx.cs" Inherits="school_web._adminETutorProf.webview.salary_slip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="fullinfo">

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
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Year</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control" >
                </asp:DropDownList>
            </p>
        </div>
        <div class="clearfix"></div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Month</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control"  >
                </asp:DropDownList>
            </p>
        </div>
        <div class="clearfix"></div>

      
         
        
      
      
        <div class="clearfix"></div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
            <asp:Button ID="btn_submit" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_submit_Click" />


        </div>
        <div class="clearfix"></div>
        <div class="texbox-border">

            <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Sl No.">
                        <ItemTemplate>
                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Month"  >
                        <ItemTemplate>
                            <asp:Label ID="lbl_monthname" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Year"  >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Year" runat="server" Text='<%#Bind("Year")%>'></asp:Label>

                             <asp:Label ID="lbl_Calculation_Id" runat="server" Text='<%#Bind("Calculation_Id")%>' Visible="false"></asp:Label>

                               <asp:Label ID="lbl_EmployeeCode" runat="server" Text='<%#Bind("EmployeeCode")%>' Visible="false"></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                    <asp:LinkButton ID="lnkview" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkview_Click" ToolTip="Slip View">  <span>View Slip</span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>



               
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
