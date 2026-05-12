<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Update_data_Local_to_online_Purnank.aspx.cs" Inherits="school_web.Dvlpr_Prof.Update_data_Local_to_online_Purnank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Update_data_Local_to_online_Purnank
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
                        Update data Local to online Purnank
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

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 65%;">
                                    <tr>
                                        <td>Select Session
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td> 
                                        <td> 
                                            <asp:Button ID="btn_update_student_data" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_update_student_data_Click" />
                                       
                                            <asp:Button ID="btn_insert_date" OnClick="btn_insert_date_Click" runat="server" Text="insert" />
                                        
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
