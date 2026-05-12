<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="add-fee-in-mid-session.aspx.cs" Inherits="school_web.Dvlpr_Prof.add_fee_in_mid_session" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
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
                    <div><asp:Literal ID="ltUsertop" runat="server">Delete Student Attendance</asp:Literal></div>
                </div>
                <div class="page-title-actions"></div>
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
            <div class="card-body">
                <div class="form-row">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="position-relative form-group">
                                <label>Session</label>
                                <div class="input-group input-group-icon">
                                    <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2" style="display: none">
                            <div class="position-relative form-group">
                                <label>Fees For</label>
                                <div class="input-group input-group-icon">
                                    <asp:DropDownList ID="ddl_fees_for" runat="server" class="form-control">
                                        <asp:ListItem Value="4">Day Scholar</asp:ListItem>
                                        <asp:ListItem Value="44">Day Boarding with Lunch</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="position-relative form-group">
                                <label>Fees For</label>
                                <div class="input-group input-group-icon">
                                    <asp:DropDownList ID="ddl_days_hostel" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_days_hostel_SelectedIndexChanged">
                                        <asp:ListItem>School</asp:ListItem>
                                        <asp:ListItem>Hostel</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="position-relative form-group">
                                <label>Type</label>
                                <div class="input-group input-group-icon">
                                    <asp:DropDownList ID="ddl_fee_group" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Admission</asp:ListItem>
                                        <asp:ListItem Value="2">Annual</asp:ListItem>
                                        <asp:ListItem Value="3">Monthly</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="position-relative form-group">
                                <label>Fee Head</label>
                                <div class="input-group input-group-icon">
                                    <asp:DropDownList ID="ddl_fee_head" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="position-relative form-group">
                                <label>Fee Head Amount</label>
                                <div class="input-group input-group-icon">
                                    <asp:TextBox ID="txt_amount" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" runat="server" id="monthDV">
                            <label for="validationCustom01" class="form-label" style="margin: 10px 0px 5px 0px;">Choose Month<sup>*</sup></label>
                            <span class="chkbx-all">
                                <asp:CheckBox ID="chk_all_month" runat="server" Text="Select All" OnCheckedChanged="chk_all_month_CheckedChanged" AutoPostBack="true" /></span>
                            <br />
                            <asp:Repeater ID="rp_month" runat="server">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_month_name" class="chkstle" runat="server" Text='<%#Eval("Month") %>' />
                                    <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Value")%>'></asp:Label>
                                    <asp:Label ID="lbl_month_name" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                    <asp:Label ID="lbl_month_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <div class="col-md-12">
                            <label for="validationCustom01" class="form-label Llabel">Class</label>
                            <span class="chkbx-all">
                                <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" /></span>
                            <br />
                            <asp:Repeater ID="rd_view" runat="server">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                    <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                    <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="col-md-4">
                            <div class="position-relative form-group">
                                <div class="input-group input-group-icon">
                                    <asp:Button ID="btn_submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btn_submit_Click" Style="float: left; width: auto; margin: 32px 0px 0px 13px;" />
                                </div>
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
