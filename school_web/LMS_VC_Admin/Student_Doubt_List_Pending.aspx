<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Student_Doubt_List_Pending.aspx.cs" Inherits="school_web.LMS_VC_Admin.Student_Doubt_List_Pending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Doubt List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .gridcss th {
            font-size: 14px!important;
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
            left: 77px;
        }
    </style>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Student Doubt List </asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Section</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Subject</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Start Date</label>

                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>End Date</label>

                                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 30px;" />
                            </div>

                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th colspan="9">
                                        <p style="width: auto; float: left; background-color: #e50800; padding: 4px 9px 0px 0px; margin: 0px 5px 0px 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                            <asp:RadioButton ID="rd_pendin_req" runat="server" GroupName="qw" Text="Unanswered" OnCheckedChanged="rd_pendin_req_CheckedChanged" Checked="true" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                        </p>
                                        <p style="width: auto; float: left; background-color: #29a700; padding: 4px 9px 0px 0px; margin: 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                            <asp:RadioButton ID="rd_closed_req" runat="server" GroupName="qw" Text="Replied" OnCheckedChanged="rd_closed_req_CheckedChanged" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                        </p>
                                        <span style="float: right">
                                            <p style="width: auto; float: left; background-color: #c6c6c6; padding: 3px; margin: 0px 3px; font-weight: 500;">
                                                Total Asked Questions : 
                                    <asp:Label ID="lbl_total_data" runat="server" Text="0"></asp:Label>
                                            </p>
                                            <p style="width: auto; float: left; background-color: #e50800; color: #fff; padding: 3px; margin: 0px 3px; font-weight: 500;">
                                                Total Unanswered Questions : 
                                    <asp:Label ID="lbl_pending" runat="server" Text="0"></asp:Label>
                                            </p>
                                            <p style="width: auto; float: left; background-color: #23c210; color: #fff; padding: 3px; margin: 0px 3px; font-weight: 500;">
                                                Total Replied Questions : 
                                    <asp:Label ID="lbl_replied_Questions" runat="server" Text="0"></asp:Label>
                                            </p>
                                        </span>
                                    </th>
                                </tr>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Date</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Asked By</th>
                                    <th>Asked To</th>

                                    <th>Question </th>
                                    <th>Status </th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_Upload_Date" runat="server" Font-Names="Arial" Text='<%#Bind("Date") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CategoryName" runat="server" Font-Names="Arial" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section_Name" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CourseName" runat="server" Font-Names="Arial" Text='<%#Bind("Cource_name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("askedby") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_askedto" runat="server" Font-Names="Arial" Text='<%#Bind("teachername") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Student_question" runat="server" Font-Names="Arial" Style="word-break: break-all" Text='<%#Bind("Student_question") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_status" runat="server" Font-Names="Arial" Visible="false" Text='<%#Bind("Status") %>'></asp:Label>
                                                <asp:Label ID="lbl_status_disply" runat="server" Font-Names="Arial"></asp:Label>
                                            </td>

                                            <td>

                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">




                                                            <asp:LinkButton ID="lnk_reply" runat="server" CssClass="dropdown-item" OnClick="lnk_reply_Click"><i class="dropdown-icon lnr lnr-pencil"></i><span>Reply</span></asp:LinkButton>



                                                            <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Doubt_Id" runat="server" Font-Names="Arial" Text='<%#Bind("Doubt_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Question_Image" runat="server" Font-Names="Arial" Text='<%#Bind("Question_Image") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>


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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
