<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="School_Calender_List.aspx.cs" Inherits="school_web.LMS_VC_Admin.School_Calender_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    School Calender List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">   School Calender List</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                    <a href="Add_School_Calender.aspx">
                        <button type="button" class="btn-shadow btn btn-info">
                            <span class="btn-icon-wrapper pr-2 opacity-7">
                                <i class="pe-7s-plus fa-w-20"></i>
                            </span>
                            Add School Calender
                        </button>
                    </a>
                </div>
            </div>
        </div>



        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="slid" runat="server" />
        <div class="row">

            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <asp:HiddenField ID="HdID" runat="server" />
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-4">
                                <div class="position-relative form-group">
                                    <label>Select Month</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control">
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>Mar</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>Jun</asp:ListItem>
                                            <asp:ListItem>Jul</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="position-relative form-group">
                                    <label>Select Year</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control">
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 30px;" />
                                <a id="a1" runat="server"   target="_blank">
                                    <img src="../images/printer.png" class="printBtn noPrint" style="height: 36px; width: 36px;margin: 23px 0px 0px 0px;" /></a>
                            </div>
                        </div>
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Date</th>
                                    <th>Day</th>
                                    <th>Type</th>
                                    <th>Details</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_Date" runat="server" Font-Names="Arial" Text='<%#Bind("Date") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Day" runat="server" Font-Names="Arial" Text='<%#Bind("Day") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_type" runat="server" Font-Names="Arial" Text='<%#Bind("Type") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Remarks" runat="server" Font-Names="Arial" Text='<%#Bind("Details") %>'></asp:Label>
                                            </td>





                                            <td>

                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">




                                                            <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><i class="dropdown-icon lnr lnr-pencil"></i><span>Edit</span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>

                                                            <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>

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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
