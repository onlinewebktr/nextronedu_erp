<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits="school_web.LMS_VC_Admin.chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Complaint & Feedback Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.4/angular.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/angular-sanitize/1.6.4/angular-sanitize.min.js"></script>

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
            background: #16aaff;
            color: #fff;
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

        .notificationpan {
            position: fixed;
            top: 150px!important;
            right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-headphones icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Label ID="lbl_req_id" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                        <asp:Label ID="lbl_user_name" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                        <asp:Label ID="lbl_admission_no" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                        <asp:Label ID="lbl_user_mobile_no" Style="display: none" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                        <asp:Label ID="lbl_order_id" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                        <asp:Label ID="lbl_complain_date" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                    </div>
                </div>
                <div class="page-title-actions">
                    <asp:LinkButton ID="lnk_back" OnClick="lnk_back_Click" class="btn-shadow btn btn-info" runat="server"></asp:LinkButton>
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


        <div class="main-card mb-3 card" data-ng-app="myApp" data-ng-controller="MyChatCtrl">
            <asp:HiddenField ID="hd_request_id" runat="server" />
            <asp:HiddenField ID="hd_studentid" runat="server" />
            <div class="card-body">

                <div class="chat-sec">
                    <div class="chat-msg-bx">
                        <asp:GridView ID="Grdattache" runat="server" class="mb-0 table table-bordered" AutoGenerateColumns="False" Width="18%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download">
                                    <ItemTemplate>
                                        <a href='<%#Eval("Attachment") %>' download target="_blank" style="margin: 0px; display: block; padding: 0px; font-family: ebrima; font-size: 19px; color: #0066CC; text-decoration: none;"><i class="fa fa-download" aria-hidden="true"></i></a>

                                    </ItemTemplate>

                                </asp:TemplateField>







                            </Columns>
                        </asp:GridView>
                        <div class="chat-msg-bxdfgdfg" data-ng-repeat="x in messages">
                            <div class="chat-msg-bx-wpr {{x.Message_by}}">
                                <p class="chat-msg-bx-wpr-p">{{x.Message}}</p>
                                <p class="chat-msg-bx-wpr-date-time-p">{{x.new_date}}-{{x.Time}}</p>
                                <p class="chat-msg-bx-wpr-sent-by">{{x.Message_by}}</p>
                            </div>
                        </div>




                    </div>


                    <div class="chat-send_msg-sec">
                        <div id="pnl_msg_sends" class=" {{reqStatus[0].ShowStatus}}" runat="server">
                            <div class="chat-send-msg-txtbx-wpr">
                                <asp:TextBox ID="txt_message" runat="server" CssClass="chat-send-msg-txtbx form-control" placeholder="Enter your message here..." TextMode="MultiLine" Style="height: 60px"></asp:TextBox>
                            </div>
                            <div class="chat-send-msg-ddl-wpr">
                                <asp:DropDownList ID="ddl_status" runat="server" CssClass="chat-send-msg-ddl form-control">
                                    <asp:ListItem Value="0">Running</asp:ListItem>
                                    <asp:ListItem Value="1">Close</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="chat-send-msg-btn-sec">
                                <ul class="chat-send-msg-btn-ul">
                                    <li><a href="javascript:" data-ng-click="ButtonClick()">Send</a></li>
                                </ul>
                            </div>
                        </div>
                        <div id="pnl_closes" class=" {{reqStatus[0].PnlClosed}}" runat="server">
                            <p class="closed-status">Closed</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript">
        var app = angular.module('myApp', ['ngSanitize']);
        app.controller('MyChatCtrl', function ($scope, $http) {
            var request_id = $("#<%=hd_request_id.ClientID%>").val();
            var studentid = $("#<%=hd_studentid.ClientID%>").val();

            $http.get("WebService1.asmx/fetch_chat_message", { params: { "Request_id": request_id } }).then(function (response) {
                $scope.messages = response.data;
            })
            $http.get("WebService1.asmx/fetch_reqest_status", { params: { "Request_id": request_id } }).then(function (response) {
                $scope.reqStatus = response.data;
            })
            $scope.ButtonClick = function () {

                var message = $("#<%=txt_message.ClientID %>").val();
                var status = $("#<%=ddl_status.ClientID %>").val();
                //alert(request_id),
                //alert(message),
                //alert(status)


                if (request_id == "") {

                    messge("Something went wrong please try again later.");
                }
                else if (message == "") {
                    messge("Please enter message.");
                }

                else {

                    //send-data  

                    $http.get("WebService1.asmx/send_message_to_user", { params: { "Request_id": request_id, "Message": message, "Status": status, "studentid": studentid } }).then(function (response) {
                        $scope.client_dt = response.data;
                        if ($scope.client_dt == "") {
                            $("#<%=txt_message.ClientID %>").val('');
                            messge("Your Message Send Successfully.");
                            $http.get("WebService1.asmx/fetch_chat_message", { params: { "Request_id": request_id } }).then(function (response) {
                                $scope.messages = response.data;
                            })

                            $http.get("WebService1.asmx/fetch_reqest_status", { params: { "Request_id": request_id } }).then(function (response) {
                                $scope.reqStatus = response.data;
                            })
                        }
                    })
                }
            }
        });

    function messge(msg) {
        $("#<%=lblmessage.ClientID%>").text(msg);
        $('.notificationpan').hide().slideDown(1000);
        $('.notificationpan').delay(4000).show().slideUp(1000);
    }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
