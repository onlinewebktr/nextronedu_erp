<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="upload-log-book.aspx.cs" Inherits="school_web._adminETutorProf.webview.upload_log_book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .my-btn {
            color: #fff;
            background-color: #fdb351 !important;
            border-color: #fdb351 !important;
            font-size: 15px !important;
            line-height: 23px !important;
            font-weight: 400 !important;
            /*-webkit-transition: all 0.9s;
            -o-transition: all 0.9s;
            -moz-transition: all 0.9s;
            transition: all 0.9s;*/
        }

            .my-btn:hover {
                color: #fff;
                background-color: #fdb351 !important;
            }

        .form-control {
            display: block;
            width: 100%;
            height: 25px !important;
            font-size: 13px !important;
            padding: 2px 10px;
            font-weight: 500;
        }

        img {
            right: 12px !important;
            left: auto !important;
        }

        .notificationpan {
            top: 10px !important;
        }
    </style>

    <%--<link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100", 
                maxDate: '0',
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullinfo">
        <div class="fullinfo">
            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
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
                    <p class="textcont1 ">Class<sup>*</sup></p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </p>
                </div>
                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1 ">Section<sup>*</sup></p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </p>
                </div>

                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1">Date<sup>*</sup></p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:TextBox ID="txt_date" runat="server" CssClass="classTarget form-control" Style="width: 100%" ReadOnly="true"></asp:TextBox>
                    </p>
                </div>

                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1 ">Remarks<sup>*</sup></p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:TextBox ID="txt_remarks" runat="server" CssClass="form-control" Style="height: 200px!important;" TextMode="MultiLine"></asp:TextBox>
                    </p>
                </div>

                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1 ">Attachment <sup> </sup></p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                    </p>
                </div>

                <div class="clearfix"></div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-right: 5px; padding-left: 5px;">
                    <asp:Button ID="btn_save" runat="server" Text="Save" class="mt-2 btn btn-primary my-btn" OnClick="btn_save_Click1" ValidationGroup="a" Style="float: right" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
