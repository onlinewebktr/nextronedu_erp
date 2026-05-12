<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Bulk_sheet_create_hostel.aspx.cs" Inherits="school_web.Dvlpr_Prof.Bulk_sheet_create_hostel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Bulk sheet create hostel
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
                        Bulk sheet create hostel
                    </div>
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


        <div class="row">
            <div class="col-lg-10">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-12">

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                    <tr>


                                        <td>Select Hoste Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_hostel" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>

                                        <td>Select Hoste Catogery
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_hostel_catogery" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_catogery_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                           <td>Select room
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_room" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                         <td>No of Sheet
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_no_sheet" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>


                                        <td>

                                            <asp:Button ID="btn_create" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_create_Click" />
                                        </td>






                                    </tr>

                                </table>

                         

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
