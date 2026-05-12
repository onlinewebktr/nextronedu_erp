<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="fee-settlement-admission-monthly.aspx.cs" Inherits="school_web.Dvlpr_Prof.fee_settlement_admission_monthly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <asp:TextBox ID="txt_adm_ann_fee" runat="server"></asp:TextBox>


    <asp:HiddenField ID="hd_paybaleamount" runat="server" />
    <asp:HiddenField ID="hd_adjustamount" runat="server" />
    <asp:HiddenField ID="hd_totalamount" runat="server" />
    <asp:HiddenField ID="hd_total_discount" runat="server" />
    <asp:Label ID="lbl_previous_year_dues" runat="server"></asp:Label>
    <asp:Label ID="lbl_class" runat="server"></asp:Label>
    <asp:Label ID="lbl_session" runat="server"></asp:Label>
    <asp:TextBox ID="txt_payment_date" runat="server"></asp:TextBox>
    <asp:Label ID="lbl_admissionno" runat="server"></asp:Label>

    <asp:TextBox ID="lbl_name" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_admission_no" runat="server"></asp:TextBox>
    <asp:Label ID="lblhostel" runat="server"></asp:Label>
    <asp:Label ID="lbl_payment_mode" runat="server"></asp:Label>

    <asp:Label ID="lbl_fee_amount" runat="server"></asp:Label>
    <asp:Label ID="lbl_discount" runat="server"></asp:Label>
    <asp:Label ID="lbl_paid_prev" runat="server"></asp:Label>
    <asp:Label ID="lbl_total" runat="server"></asp:Label>
    <asp:Label ID="txttotal" runat="server"></asp:Label>
    <asp:Label ID="txt_paid_prev" runat="server"></asp:Label>
    <asp:Label ID="txt_discount" runat="server"></asp:Label>
    <asp:TextBox ID="txt_monthlyFee" runat="server"></asp:TextBox>

    <asp:TextBox ID="txt_paid_amount" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_total_dues" runat="server"></asp:TextBox>

     <asp:TextBox ID="txttotalbill" runat="server"></asp:TextBox>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
