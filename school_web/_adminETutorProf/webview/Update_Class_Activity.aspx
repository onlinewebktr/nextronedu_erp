<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="Update_Class_Activity.aspx.cs" Inherits="school_web._adminETutorProf.webview.Update_Class_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Update Class Activity 
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
            position:absolute!important;
            top: 9px!important;
        }

        .notificationpan {
            top: 10px !important;
        }
        .form-control[disabled], .form-control[readonly], fieldset[disabled] .form-control {
    background-color: #ffffff!important;
    color: #070404!important;
    cursor: not-allowed!important;
    font-weight: bold!important;
}
        .textcont1 {
    height: auto;
    width: 100%;
    margin: 2px 0px 0px 4px!important;
    padding: 0px 0px 0px 0px;
    float: left;
    font-size: 13px;
    line-height: 18px;
    font-weight: bold!important;
    color: #4c5258;
    text-align: left;
}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullinfo">
        <div class="fullinfo">
            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
                <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                <div id="notification">
                    <div id="pan" class="notificationpan">
                        <div style="float: left; width: 100%; height: auto;">
                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                        </div>
                        <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(500);});"
                            class="closenotificationpan" alt="" />
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1 ">Class</p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </p>
                </div>
                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1 ">Section</p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </p>
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
                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1">Date</p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:TextBox ID="txt_date" runat="server" CssClass="classTarget form-control" Style="width: 100%" Enabled="false"></asp:TextBox> 
                    </p>
                </div>

                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1 ">Remarks</p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:TextBox ID="txt_remarks" runat="server" CssClass="form-control" Style="    height: 200px !important;
    border: 1px solid #999797;
    border-radius: 7px;" TextMode="MultiLine"></asp:TextBox>
                    </p>
                </div>


                <div class="clearfix"></div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont1 ">Attachment</p>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                    <p class="textcont3">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" style="padding: 4px 5px 4px 5px;
    height: 31px !important;" />
                    </p>
                </div>
                <div class="clearfix"></div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-right: 5px; padding-left: 5px;text-align: center;">
                    <asp:Button ID="btn_save" runat="server" Text="Save" class="mt-2 btn btn-primary my-btn" OnClick="btn_save_Click1" ValidationGroup="a" Style="float: none" />
                </div>


                <%--</ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress2"
                    runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                    DynamicLayout="False">
                    <ProgressTemplate>
                        <p class="waiting">
                            &nbsp;&nbsp;&nbsp; 
                                            <img src="../../images/Processing.gif" />

                        </p>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            </div>
        </div>
    </div>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();

        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();
                }
            });
        };
        function SetDatePicker() {
            $("[id$=txt_date]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'calendar.png',
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",
                dateFormat: "dd/mm/yy",
                maxDate: '0',
                /* minDate: 0,*/
            });
        }
    </script>
</asp:Content>
