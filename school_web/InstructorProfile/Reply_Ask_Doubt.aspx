<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Reply_Ask_Doubt.aspx.cs" Inherits="school_web.InstructorProfile.Reply_Ask_Doubt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Replay Ask Doubt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .Admin {
            float: right;
            background: #d0ebff;
            border: 1px solid #b3daf7;
        }

        .User {
            float: left;
            background: #ecffda;
            border: 1px solid #cbedad;
        }

        .comp-dt-in-pg {
            margin: -3px 5px 0px 0px;
            padding: 4px 3px;
            font-size: 14px;
            border: 1px solid #ddd;
            float: left;
            border-radius: 2px;
            background: #ffd78f;
            color: #000;
        }

        .app-page-title .page-title-icon {
            margin: 0 5px 0 0;
        }

        .closed-status {
            margin: 0px 5px 0px 0px;
            padding: 4px 23px;
            font-size: 16px;
            border: 1px solid #ddd;
            float: left;
            border-radius: 2px;
            background: #04fff3;
            color: #000;
        }

        .hidden {
            display: none;
        }

        .table td, .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            text-align: center;
        }

        .chat-send-msg-txtbx-wpr {
            margin: 0px 3px 0px 0px;
            padding: 0px;
            width: 87%;
            height: auto;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>

                </div>
                <div class="page-title-actions">
                    <asp:LinkButton ID="lnk_back" OnClick="lnk_back_Click" class="btn-shadow btn btn-info" runat="server">Back</asp:LinkButton>
                    <%--<button type="button" >
                            <span class="btn-icon-wrapper pr-2 opacity-7">
                                <i class="pe-7s-plus fa-w-20"></i>
                            </span>
                            Feedback
                        </button>--%>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>


        <div class="main-card mb-3 card">
            <asp:HiddenField ID="hd_request_id" runat="server" />
            <div class="card-body">

                <div class="chat-sec">
                    <div class="chat-msg-bx">
                        <div>

                            <table style="width: 100%;" class="table table-hover table-striped table-bordered">
                                <thead>

                                    <tr>

                                        <th>Date</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Subject</th>
                                        <th>Asked By</th>





                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RPDetails" runat="server">
                                        <ItemTemplate>
                                            <tr>

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





                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>


                        </div>
                        <div class="chat-msg-bxdfgdfg">
                            <div class="chat-msg-bx-wpr">
                                <p class="chat-msg-bx-wpr-p">
                                    <asp:Label ID="lbl_question" runat="server"></asp:Label>
                                </p>
                                <p class="chat-msg-bx-wpr-date-time-p">
                                    <a id="a1" runat="server" download target="_blank" style="display: block; padding: 0px 0px 0px 0px; font-size: 22px; color: #0066CC; text-decoration: none; float: left;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                </p>

                            </div>
                        </div>

                        <div class="chat-msg-bxdfgdfg ng-scope"  id="admin"  runat="server" visible="false">
                            <div class="chat-msg-bx-wpr Admin">
                                <p class="chat-msg-bx-wpr-p ng-binding">
                                    <asp:Label ID="lbl_answer" runat="server"></asp:Label>
                                </p>
                                <p class="chat-msg-bx-wpr-date-time-p ng-binding">
                                    <asp:Label ID="lbl_replydate" runat="server"></asp:Label>
                                </p>
                                <p class="chat-msg-bx-wpr-sent-by ng-binding">
                                  Teacher:  <asp:Label ID="lbl_teachername" runat="server"></asp:Label>
                                </p>
                                <p class="chat-msg-bx-wpr-date-time-p">
                                    <a id="a3" runat="server" download target="_blank" style="display: block; padding: 0px 0px 0px 0px; font-size: 22px; color: #0066CC; text-decoration: none; float: left;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                </p>
                            </div>
                        </div>


                    </div>


                    <div class="chat-send_msg-sec">
                        <div id="pnl_msg_sends" runat="server">
                            <div class="chat-send-msg-txtbx-wpr">
                                <asp:TextBox ID="txt_message" runat="server" CssClass="chat-send-msg-txtbx form-control" placeholder="Enter your reply here..." TextMode="MultiLine" Style="height: 70px"></asp:TextBox>


                                <div class="form-group">
                                    <label style="color:white;">Choose File ( jpg,png,pdf 500 kb )</label>
                                    <div class="input-group input-group-icon">
                                        <asp:FileUpload ID="fl_Photo" runat="server" />

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                            runat="server" ControlToValidate="fl_Photo"
                                            ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf|.PDF )$"
                                            ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="chat-send-msg-btn-sec" style="margin: 0px 0px 0px 0px;">
                                <ul class="chat-send-msg-btn-ul" style="margin: 0px 0px 0px 0px;">

                                    <li style="margin: 0px 0px 0px 0px;">
                                        <asp:Button ID="bntn_send_reply" runat="server" Style="margin: 0px; padding: 23px 15px 20px; background-color: #16aaff; border-color: #16aaff; color: #fff; font-size: 15px; border-radius: 2px;"
                                            Text="Reply" OnClick="bntn_send_reply_Click" OnClientClick='return confirm("Are you sure want to reply ?")' />

                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div id="pnl_closes" runat="server" visible="false">
                            <p class="closed-status">Replied</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
