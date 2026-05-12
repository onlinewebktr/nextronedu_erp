<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-self-added-syllabus.aspx.cs" Inherits="school_web._adminETutorProf.webview.view_self_added_syllabus" %>

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
            font-size: 12px;
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
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont1 ">Term</p>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                        <div class="textcont3">
                            <asp:DropDownList ID="ddl_term" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont1 ">Class</p>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                        <div class="textcont3">
                            <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;">
                        <p class="textcont1 ">Subject</p>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                        <div class="textcont3">
                            <asp:DropDownList ID="ddl_subject" class="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 5px; padding-left: 5px;"></div>
                    <div class="col-lg-12 col-md-12 col-sm-8 col-xs-8" style="padding-right: 5px; padding-left: 5px;">
                        <asp:Button ID="btn_submit" Style="padding: 5px 12px 3px 12px; margin: 2px 0px 15px 0px;"
                            runat="server" Text="Find" class="mt-2 btn btn-primary my-btn my-btn" OnClick="btn_submit_Click" />
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-right: 5px; padding-left: 5px;">
                        <div class="texbox-border" style="overflow: auto">
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Sl No.</th>
                                        <th>Session</th>
                                        <th>Term</th>
                                        <th>Class</th>
                                        <th>Subject</th>
                                        <th>Sub-Subject</th>
                                        <th>Chapter Name</th>
                                        <th>Subchapter Name</th>
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
                                                    <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session") %>'></asp:Label>


                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_Term_Name" runat="server" Text='<%#Bind("Term_Name") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Sub_Subject_name" runat="server" Text='<%#Bind("SubSubjName") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Chapter_Name" runat="server" Text='<%#Bind("Chapter_Name") %>'></asp:Label>

                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_Subchapter_Name" runat="server" Text='<%#Bind("SubChapterName") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkEdit" Style="background: #3f8500; padding: 3px 8px 2px 8px; border-radius: 3px; color: #fff; font-size: 12px;"
                                                        runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                    <asp:LinkButton ID="lnk_Delete" Style="background: #df0000; padding: 3px 8px 2px 8px; border-radius: 3px; color: #fff; font-size: 12px; margin: 4px 0px 0px 0px; float: left;"
                                                        runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                    <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
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
    </form>
</body>
</html>
