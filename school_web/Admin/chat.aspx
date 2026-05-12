<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits="school_web.Admin.chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Complaint  Details
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
            top: 150px !important;
            right: 10px;
        }

        .chat-box-wrapper {
            display: flex;
            clear: both;
            padding: .75rem;
        }

            .chat-box-wrapper + .chat-box-wrapper {
                padding-top: 0;
            }

            .chat-box-wrapper .chat-box {
                box-shadow: 0 0 0 transparent;
                position: relative;
                opacity: 1;
                background: #e0f3ff;
                border: 0;
                padding: 4px 0.5rem;
                border-radius: 30px;
                border-top-left-radius: .25rem;
                flex: 1;
                display: flex;
                max-width: 50%;
                min-width: 100%;
                text-align: left;
            }

                .chat-box-wrapper .chat-box + small {
                    text-align: left;
                    padding: .5rem 0 0;
                    margin-left: 1.5rem;
                    display: block;
                }

            .chat-box-wrapper.chat-box-wrapper-right {
                text-align: right;
            }

                .chat-box-wrapper.chat-box-wrapper-right .chat-box {
                    border-radius: 30px;
                    border-top-left-radius: 30px;
                    border-top-right-radius: .25rem;
                    margin-left: auto;
                }

                    .chat-box-wrapper.chat-box-wrapper-right .chat-box + small {
                        text-align: right;
                        margin-right: 1.5rem;
                        margin-left: 0;
                    }

        /*----------------chat admin------------*/
        .chat-sec {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: 450px;
            float: left;
            position: relative;
        }

        .chat-send_msg-sec {
            margin: 0px;
            padding: 5px 5px;
            width: 100%;
            height: auto;
            float: left;
            background: #fff;
            border-radius: 2px;
            position: absolute;
            bottom: 0px;
        }

        .chat-send-msg-txtbx-wpr {
            margin: 0px 3px 0px 0px;
            padding: 0px;
            width: 78%;
            height: auto;
            float: left;
            border: 1px solid #000;
        }

        .chat-send-msg-txtbx {
            margin: 0px;
            padding: 0px 5px;
            width: 100%;
            height: 40px;
            float: left;
            border-radius: 2px;
            border: 1px solid #16aaff;
        }

        .chat-send-msg-ddl-wpr {
            margin: 0px 3px 0px 0px;
            padding: 0px;
            width: 13%;
            height: auto;
            float: left;
            border: 1px solid #16aaff;
        }

        .chat-send-msg-ddl {
            margin: 0px;
            padding: 0px 5px;
            width: 100%;
            height: 60px;
            float: left;
            border-radius: 2px;
        }

        .chat-send-msg-btn-sec {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 7%;
            height: auto;
            float: left;
        }

        .chat-send-msg-btn-ul {
            margin: 9px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
        }

            .chat-send-msg-btn-ul li {
                margin: 0px;
                padding: 0px;
                list-style-type: none;
            }

                .chat-send-msg-btn-ul li a {
                    margin: 0px;
                    padding: 20px 15px 20px;
                    background-color: #16aaff;
                    border-color: #16aaff;
                    color: #fff;
                    font-size: 15px;
                    border-radius: 2px;
                }



        .chat-msg-bx {
            margin: 0px;
            padding: 0px 5px 0px 0px;
            width: 100%;
            height: 400px;
            float: left;
            overflow: auto;
        }

        .chat-msg-bxdfgdfg {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
        }

        .chat-msg-bx-wpr {
            margin: 0px 0px 5px 0px;
            padding: 1px 7px 2px;
            max-width: 60%;
            height: auto;
            float: left;
            background: #ddd;
            border-radius: 2px;
        }

        .chat-msg-bx-wpr-p {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 13px;
            line-height: 22px;
            color: #000;
        }

        .chat-msg-bx-wpr-date-time-p {
            margin: 2px 0px 1px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 11px;
            color: #000859;
        }

        .chat-msg-bx-wpr-sent-by {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 11px;
            color: #930000;
        }

        .User {
            float: left;
            background: #ecffda;
            border: 1px solid #cbedad;
        }

        .Admin {
            float: right;
            background: #d0ebff;
            border: 1px solid #b3daf7;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999;
        }

        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 70px;
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 235px; height: auto;">
                        <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">
                    <asp:LinkButton ID="lnk_back" OnClick="lnk_back_Click" class="backlnk-css" runat="server"><i class="bx bx-arrow-back"></i> </asp:LinkButton>
                </div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Complaint  Details</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">


                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=""></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <asp:Label ID="lbl_req_id" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                                <asp:Label ID="lbl_user_name" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                                <asp:Label ID="lbl_admission_no" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                                <asp:Label ID="lbl_user_mobile_no" Style="display: none" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                                <asp:Label ID="lbl_order_id" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>
                                <asp:Label ID="lbl_complain_date" runat="server" Text="" class="comp-dt-in-pg"></asp:Label>

                            </div>

                            <div class="row" data-ng-app="myApp" data-ng-controller="MyChatCtrl">
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
                    </div>
                </div>



            </div>
        </div>
        <!--end row-->
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
               // alert(message)
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
