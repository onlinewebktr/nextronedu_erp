<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site1.Master" AutoEventWireup="true" CodeBehind="View_Class_Activity.aspx.cs" Inherits="school_web.Student_Profile.webview.View_Class_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 8px;
            left: 75px;
        }

        .clndr-icon {
            font-size: 11px !important;
            color: #ff2956;
            position: absolute;
            top: 10px;
            right: 3px;
            left: auto;
        }
    </style>


    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" /> 
    <script src="../../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>

    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
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
            <p class="textcont1 ">Subject</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </p>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Start Date  </p>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3" style="position: relative">
                <asp:TextBox ID="txt_date" runat="server" CssClass="calender-icon"></asp:TextBox>
                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
            </p>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">End Date </p>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3" style="position: relative">
                <asp:TextBox ID="txt_enddate" runat="server" CssClass="calender-icon"></asp:TextBox>
                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
            </p>
        </div>
        <div class="clearfix"></div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
            <asp:Button ID="btn_submit" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_submit_Click" />


        </div>
        <div class="clearfix"></div>
        <div class="texbox-border" style="padding: 0px 5px; overflow: auto">

            <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl No.">
                        <ItemTemplate>
                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Class" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Subject">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lbl_remarks" Style="word-break: break-all;" runat="server" Text='<%#Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Attachment">
                        <ItemTemplate>
                            <asp:Label ID="lbl_attachment" Visible="false" runat="server" Text='<%#Bind("Attachment") %>'></asp:Label>
                            <a href='<%#Eval("Attachment") %>' id="attachmentss" runat="server" download=""  style="background: #14bb00; padding: 5px 5px 5px 5px; border-radius: 2px; color: #fff; font-weight: 600;">Download</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
