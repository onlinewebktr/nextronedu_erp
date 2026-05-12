<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="My_Attendance_From_Device.aspx.cs" Inherits="school_web._adminETutorProf.webview.My_Attendance_From_Device" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    My Attendance  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .daySunday {
            background: #ff7373 !important;
        }

        .tdwdth {
            width: 65px !important;
            display: inline-block;
            font-weight: 600;
            font-style: inherit;
        }

        .daypresenT {
            background: #5afb3d !important;
        }

        .dayabsenT {
            background: #fff84b !important;
        }

        .dayleavE {
            background: #ffb100 !important;
        }

        .txtcenter {
            text-align: center;
        }

        .notattendances {
            background: #ef91ff !important;
        }

        tfoot, th, thead {
            color: #fff;
        }

        .notesp {
            margin: 0px 0px 5px 0px;
            padding: 2px 5px 2px 5px;
        }

            .notesp span {
                font-weight: 600;
            }

        .headgroup1 {
            background: #c541c7 !important;
        }

        .headgroup2 {
            background: #58aac9 !important;
        }

        .tdgroup1 {
            background: #c5ef96 !important;
        }

        .txtnoWrap {
            white-space: nowrap;
        }

        .wdth100 {
            width: 100%;
            float: left;
        }

        th {
            background: #e1dddd !important;
            color: #000;
        }
    </style>
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
        <div class="col-lg-12">
            <div class="main-card mb-3 card">


                <div class="card-body" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                    <div class="form-row">
                        <table style="margin: 0px; padding: 0px; float: left; width: 100%">
                            <tr>
                                <td>Year
                                </td>
                                <td>Month
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlyear" runat="server" class="form-select"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_month" runat="server" class="form-select"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btn_submit" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                </td>
                            </tr>



                        </table>
                    </div>
                    <hr />


                    <div style="width: 300px; float: left; height: auto; margin: 0px; overflow: scroll">

                        <div class="grd-wpr">
                            <div id="tblPrintIQ" runat="server">

                                <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_In_Time" Text='<%#Bind("In_Time")%>' runat="server"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Out_Time" Text='<%#Bind("Out_Time")%>' runat="server">


                                                </asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AttendanceStatus" Style="background: #77ea03; padding: 5px; font-weight: bold;"
                                                    Text='<%#Bind("AttendanceStatus")%>' runat="server">


                                                </asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>


    </div>
</asp:Content>
