<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add-syllabus.aspx.cs" Inherits="school_web._adminETutorProf.webview.add_syllabus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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

    <script src="../../js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <style>
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
            position: relative;
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
            color: #fdb351;
            position: absolute;
            top: 9px;
            right: 5px;
            left: inherit;
        }

        .form-control {
            font-weight: 500;
        }

        td, th {
            padding: 5px 7px;
        }

        .form-row-wpr {
            margin: 5px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

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
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="fullinfo">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>


            <div class="container"> 
                    <div class="form-row-wpr">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Term</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:DropDownList ID="ddl_term" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Class</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Subject</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:DropDownList ID="ddl_subject" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Is Sub Subject</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:DropDownList ID="ddl_is_sub_subject" runat="server" class="form-control">
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr" id="subsubjest">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Sub-Subject</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:TextBox ID="txt_sub_subject" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Chapter Name</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:TextBox ID="txt_Chaptername" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Is Sub Chapter</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:DropDownList ID="ddl_sub_chapter" runat="server" class="form-control">
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr" id="subchapter">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                                <p class="textcont1 ">Enter Subchapter Name</p>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                                <div class="textcont3">
                                    <asp:TextBox ID="txt_subchapter" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row-wpr">
                        <div class="clearfix"></div>
                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px; padding-left: 0px;"></div>
                        <div class="col-lg-12 col-md-12 col-sm-8 col-xs-8" style="padding-right: 0px; padding-left: 0px;">
                            <asp:Button ID="btn_submit" Style="background-color: #fdb351 !important; border-color: #fdb351 !important; padding: 5px 12px 3px 12px; margin: 2px 0px 15px 0px;"
                                runat="server" Text="Add" class="mt-2 btn btn-primary my-btn my-btn" OnClick="btn_submit_Click" />

                            <asp:Button ID="btn_cncel" Visible="false" Style="padding: 5px 12px 3px 12px; margin: 2px 0px 15px 0px; background-color: #adadad; border-color: #8b8b8b;"
                                runat="server" Text="Cancel" class="mt-2 btn btn-primary my-btn my-btn" />
                        </div>
                    </div> 
            </div>
        </div>

        <asp:HiddenField ID="hd_id" runat="server" />


        <script type="text/javascript">  
            $(document).ready(function () {
                on_subj_selection();
                $("#<%=ddl_is_sub_subject.ClientID%>").on('change', function () {
                    on_subj_selection();
                })
            });

            function on_subj_selection() {
                if ($('#<%= ddl_is_sub_subject.ClientID %> option:selected').val() == "Yes") {
                    $("#subsubjest").show();
                    $("#<%=txt_sub_subject.ClientID %>").focus();
                }
                else {
                    $("#subsubjest").hide();
                }
            }

            //================
            $(document).ready(function () {
                on_chapter_selection();
                $("#<%=ddl_sub_chapter.ClientID%>").on('change', function () {
                    on_chapter_selection();
                })
            });

            function on_chapter_selection() {
                if ($('#<%= ddl_sub_chapter.ClientID %> option:selected').val() == "Yes") {
                    $("#subchapter").show();
                    $("#<%=txt_subchapter.ClientID %>").focus();
                }
                else {
                    $("#subchapter").hide();
                }
            }
        </script>
    </form>
</body>
</html>
