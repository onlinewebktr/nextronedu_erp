<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Updated_Syllabus_Chapter.aspx.cs" Inherits="school_web._adminETutorProf.webview.Updated_Syllabus_Chapter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Syllabus Chapter</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />

    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />

    <style>
        .messbox-sec-h2 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #fff;
            background-color: #109be1;
        }

        .fullinfo {
            margin: 0px 0px 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .textcont1 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 13px;
            line-height: 20px;
            color: #000;
            text-align: left;
        }

        .textcont3 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 12px;
            line-height: 20px;
            color: #000;
            text-align: left;
            font-weight: bold;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: -23px;
        }

        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }

        .btn {
            padding: 2px 17px 2px 17px !important;
            margin: 5px 0px 0px 0px;
        }
        /******************Notification**********************/
        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 133px !important;
            right: 10px;
            padding: 10px 10px;
            width: 290px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgba(154, 154, 154, 0.8);
        }


        .closenotificationpan {
            position: absolute;
            margin: 0px 0px 0px 0px;
            top: 6px;
            right: 6px;
            cursor: pointer;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999;
        }

        table {
            /*box-shadow: 0 1px 1px 0 rgb(0 0 0 / 14%), 0 7px 10px -5px rgb(244 67 54 / 40%);*/
            /*background: linear-gradient( 60deg,#f7807e,#e53935);*/
            border-radius: 0px;
            /*border: 1px dashed #d1c5c5!important;*/
            background: #fff !important;
            border-bottom: 0px solid #c8c5c5;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 5px;
            vertical-align: middle;
            border-color: #ddd0;
            font-size: 12px;
            color: #000;
            padding: 6px 0px 5px 7px !important;
            text-align: center;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > .table-bordered > tbody > tr > .table-bordered > tfoot > tr > th {
            background: #e1dddd !important;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #a29b9b;
            text-align: center;
            padding: 3px 4px 3px 5px;
            font-size: 11px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 25px !important;
            font-size: 14px !important;
            padding: 2px 10px;
            font-weight: 500;
        }

        .table {
            margin-bottom: 9px !important;
        }

        label {
            display: inline !important;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
        }

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server" EnableCdn="true"></asp:ScriptManager>
        <div class="fullinfo">
            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="notification">
                            <div id="pan" class="notificationpan">
                                <div style="float: left; width: 100%; height: auto;">
                                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                </div>
                                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                                    class="closenotificationpan" alt="" />
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Term  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:DropDownList ID="ddl_term" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged">
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Class  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Section  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged">
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="clearfix"></div>

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Subject  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnl_is_subsubject" runat="server" Visible="false">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Sub-Subject</p>
                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont3">
                                    <asp:DropDownList ID="ddl_subsubject" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </p>
                            </div>
                        </asp:Panel>


                        <div class="clearfix"></div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Chapter  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:DropDownList ID="ddl_chapter" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_chapter_SelectedIndexChanged"></asp:DropDownList>
                            </p>
                        </div>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnl_is_subchapter" runat="server" Visible="false">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Subchapter  </p>
                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont3">
                                    <asp:DropDownList ID="ddl_subchapter" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </p>
                            </div>
                        </asp:Panel>
                        <div class="clearfix"></div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Date  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:Label ID="lbl_date" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Status  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:DropDownList ID="ddl_status" runat="server" CssClass="form-control">
                                    <asp:ListItem>Running</asp:ListItem>
                                    <asp:ListItem>Incomplete</asp:ListItem>
                                    <asp:ListItem>Completed</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont1 ">Remarks  </p>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
                            <p class="textcont3">
                                <asp:TextBox ID="txt_remarks" runat="server" CssClass="form-control" Style="height: 200px!important;" TextMode="MultiLine"></asp:TextBox>

                            </p>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-right: 5px; padding-left: 5px;">
                            <asp:Button ID="btn_save" runat="server" Text="Save" class="mt-2 btn btn-primary my-btn" OnClick="btn_save_Click" ValidationGroup="a" Style="float: right" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>


                    </ContentTemplate>
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
                </asp:UpdateProgress>
            </div>
        </div>
    </form>
</body>
</html>
