<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Class_Routine_Chart.aspx.cs" Inherits="school_web.LMS_VC_Admin.Class_Routine_Chart" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="app-main__inner">
                    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>
                                    <asp:Literal ID="ltUsertop" runat="server">Class Routine List</asp:Literal>
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
                    <asp:HiddenField ID="hd_regid" runat="server" />
                    <div class="row">

                        <div class="col-lg-12">
                            <div class="main-card mb-3 card">
                                <div class="card-body">
                                    <table class="tab-content table table-bordered">
                                        <tr>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold;">Session
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px;">
                                                <asp:DropDownList ID="ddl_session" Style="width: 118px!important;" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Class Name
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Section 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:DropDownList ID="ddl_section" runat="server" Style="width: 150px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:ImageButton ID="imgexcel2" runat="server" Visible="false" ImageUrl="~/images/excel_con.png" CssClass="excelbutton22" OnClick="imgexcel2_Click"
                                                    Style="height: 31px; width: 32px; margin-top: 1px; margin: 8px 0px 0px 13px;" />
                                            </td>


                                        </tr>



                                    </table>

                                    <div runat="server" visible="false" id="grid111">
                                        <%-- <asp:GridView ID="grd_class" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin: 2px 0px 2px 0px"
                                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows">
                                             
                                        </asp:GridView>--%>
                                        <div style="overflow-y: scroll; width: 1047px;">
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                        <asp:GridView ID="GrdView" runat="server" Width="100%" CssClass="mydatagrid">
                                        </asp:GridView>
                                </table>
                                            </div>

                                    </div>



                                </div>
                            </div>



                        </div>

                    </div>
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />
                <asp:HiddenField ID="hd_subjectid" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="imgexcel2" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress2"
            runat="server" AssociatedUpdatePanelID="UpdatePanel2"
            DynamicLayout="False">
            <ProgressTemplate>
                <p class="waiting">
                    &nbsp;&nbsp;&nbsp;
                                            <img src="../images/Processing.gif" />

                </p>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
