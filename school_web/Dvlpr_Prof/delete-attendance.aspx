<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="delete-attendance.aspx.cs" Inherits="school_web.Dvlpr_Prof.delete_attendance" %>

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
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Delete Student Attendance</asp:Literal>
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
            <div class="card-body">
                <div class="form-row">
                    <div class="col-md-2">
                        <div class="position-relative form-group">
                            <label>Session</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_Session" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="position-relative form-group">
                            <label>Class</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="position-relative form-group">
                            <label>Section</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="position-relative form-group">
                            <label>Date</label>
                            <div class="input-group input-group-icon">
                                <asp:TextBox ID="txt_date" runat="server" CssClass="form-control" placeholder="dd/MM/yyyy"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="position-relative form-group">
                            <div class="input-group input-group-icon">
                                <asp:Button ID="btn_find" runat="server" CssClass="btn btn-primary" Text="Find" OnClick="btn_find_Click" Style="float: left; width: auto; margin: 32px 0px 0px 13px;" />


                                <asp:Button ID="btn_dalete" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('Please check once before deleting it?');" Text="Delete" OnClick="btn_dalete_Click" Style="float: left; width: auto; margin: 32px 0px 0px 13px; height: 30px; padding: 0px 15px; background: #f00;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <table style="width: 100%;" id="example1" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Adm. No.</th>
                                    <th>Day</th>
                                    <th>Date</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr class="trclass" id="row" runat="server">
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_Admission_no" runat="server" Font-Names="Arial" Text='<%#Bind("Admission_no") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Day" runat="server" Font-Names="Arial" Text='<%#Bind("Day") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_attendance_Date" runat="server" Font-Names="Arial" Text='<%#Bind("Attendance_Date") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("Attendance_Status") %>'></asp:Label>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
