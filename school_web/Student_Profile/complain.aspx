<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="complain.aspx.cs" Inherits="school_web.Student_Profile.complain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Complain
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <style>
        .hidden {
            display: none;
        }

        .modal-dialog {
            max-width: 400px;
            margin: 0.5rem auto;
            padding: 0px 5px;
        }

        .modal-body {
            position: relative;
            flex: 1 1 auto;
            padding: 0px;
            background: #fff;
            border-radius: 4px;
        }

        .modal {
            background: rgb(0 0 0 / 46%);
        }


        .mdl-title-h {
            margin: 0px;
            padding: 8px 15px 2px 15px;
            border-bottom: 1px solid #ddd;
            font-size: 18px;
            width: 100%;
            float: left;
            font-weight: 400;
        }

            .mdl-title-h a {
                margin: 2px 0px 1px 0px;
                padding: 0px;
                float: right;
                color: #000;
                font-size: 19px;
            }

        .mdl-cntnt-wpr {
            margin: 0px;
            padding: 15px 15px 15px 15px;
        }

        .modal.show .modal-dialog {
            transform: translateY(5%);
        }



        .answr-dv-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
        }

        .answr-rply-date-p {
            margin: 0px 0px 0px 0px;
            padding: 5px 0px 7px 0px;
            width: 100%;
            float: left;
        }

            .answr-rply-date-p span {
                font-weight: 700;
            }

        .answr-imd-dv-wpr {
            margin: 15px 0px;
            padding: 0px;
            width: 100%;
            position: relative;
            overflow: hidden;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 170px;
        }

            .answr-imd-dv-wpr img {
                margin: 0px;
                padding: 0px;
                max-width: 100%;
                height: 100%;
            }

        .answr-pss {
            margin: 0px;
            padding: 0px;
            width: 100%;
            font-size: 14px;
            line-height: 23px;
        }

        /*////////////////////////////////*/

        .coml-lst-ul {
            margin: -7px 0px 0px 0px;
            padding: 0px;
            float: right;
        }

            .coml-lst-ul li {
                margin: 0px;
                padding: 0px;
                list-style-type: none;
            }

                .coml-lst-ul li a {
                    margin: 0px;
                    padding: 3px 6px;
                    background: #0d6efd;
                    color: #fff;
                    border-radius: 2px;
                }

        .hidden {
            display: none;
        }

        .lnk-btn {
            background: #002899;
            color: #fff;
            padding: 3px 5px 3px 5px;
            min-width: 50px;
            float: left;
            font-size: 13px;
            text-align: center;
            border-radius: 3px;
            transition: .3s ease-out;
            text-decoration: none;
            box-shadow: 0 3px 3px 0 rgba(0, 0, 0, 0.14), 0 1px 7px 0 rgba(0, 0, 0, 0.12), 0 3px 1px -1px rgba(0, 0, 0, 0.2);
        }

            .lnk-btn:hover {
                background: #00994b;
                box-shadow: 0 12px 20px -10px rgba(156, 39, 176, 0.28), 0 4px 20px 0px rgba(0, 0, 0, 0.12), 0 7px 8px -5px rgba(156, 39, 176, 0.2);
                color: #fff;
            }

        .lnk-red {
            background: #e50800;
            color: #fff;
            padding: 2px 3px 2px 3px;
            min-width: 50px;
            float: left;
            font-size: 13px;
            text-align: center;
            border-radius: 3px;
            transition: .3s ease-out;
            text-decoration: none;
            box-shadow: 0 3px 3px 0 rgba(0, 0, 0, 0.14), 0 1px 7px 0 rgba(0, 0, 0, 0.12), 0 3px 1px -1px rgba(0, 0, 0, 0.2);
        }

        .lnk-green {
            background: #00a30b;
            color: #fff;
            padding: 2px 3px 2px 3px;
            min-width: 50px;
            float: left;
            font-size: 13px;
            text-align: center;
            border-radius: 3px;
            transition: .3s ease-out;
            text-decoration: none;
            box-shadow: 0 3px 3px 0 rgba(0, 0, 0, 0.14), 0 1px 7px 0 rgba(0, 0, 0, 0.12), 0 3px 1px -1px rgba(0, 0, 0, 0.2);
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

        .chat-sec {
            margin: 0px;
            padding: 5px 10px;
            width: 100%;
            min-height: 250px;
            float: left;
            position: relative;
        }

        .chat-send_msg-sec {
            margin: 0px;
            padding: 5px 5px;
            width: 100%;
            height: auto;
            float: left;
            background: #3f6ad8;
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
        }

        .chat-send-msg-txtbx {
            margin: 0px;
            padding: 0px 5px;
            width: 100%;
            height: 40px;
            float: left;
            border-radius: 2px;
        }

        .chat-send-msg-ddl-wpr {
            margin: 0px 3px 0px 0px;
            padding: 0px;
            width: 13%;
            height: auto;
            float: left;
        }

        .chat-send-msg-ddl {
            margin: 0px;
            padding: 0px 5px;
            width: 100%;
            height: 40px;
            float: left;
            border-radius: 2px;
        }

        .chat-send-msg-btn-sec {
            margin: 0px;
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
                    padding: 10px 15px 10px;
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
            height: 450px;
            float: left;
            overflow: auto;
        }

            .chat-msg-bx::-webkit-scrollbar {
                width: 3px;
            }

            .chat-msg-bx::-webkit-scrollbar-track {
                box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
            }

            .chat-msg-bx::-webkit-scrollbar-thumb {
                background-color: darkgrey;
                outline: 1px solid slategrey;
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
            max-width: 70%;
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
            font-size: 14px;
            line-height: 24px;
            color: #000;
        }

        .chat-msg-bx-wpr-date-time-p {
            margin: 2px 0px 1px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 12px;
            color: #000859;
        }

        .chat-msg-bx-wpr-sent-by {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 12px;
            font-weight: 600;
            color: #930000;
        }

        .You {
            float: right;
            background: #d0ebff;
            border: 1px solid #b3daf7;
        }

        .School {
            float: left;
            background: #ecffda;
            border: 1px solid #cbedad;
        }

        .attchmnt-ul {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .attchmnt-ul li {
                margin: 5px 10px 0px 0px;
                padding: 0px 0px 0px 0px;
                list-style-type: none;
                display: inline;
            }

                .attchmnt-ul li a {
                    margin: 0px;
                    padding: 0px 3px;
                    text-decoration: none;
                    font-size: 17px;
                    background: #00b762;
                    border-radius: 2px;
                    color: #fff;
                    line-height: 25px;
                }

        .count-p {
            width: auto;
            float: left;
            background-color: #c6fd1cd6;
            padding: 2px 3px;
            margin: 0px 1px 0px 0px;
            font-weight: 400;
            color: #002152;
            border-radius: 2px;
            border: 1px solid #a1d306;
        }

            .count-p span {
                font-weight: 600;
            }

        .topmdl-popups {
            margin: 0px 5px 3px 0px;
            padding: 2px 5px;
            width: auto;
            float: left;
            background: #fff2d5;
            border: 1px solid #f1d28c;
            border-radius: 2px;
            font-weight: 600;
        }

            .topmdl-popups span {
                font-weight: 500;
            }

        .name-name-p {
            margin: 10px 0px 2px 0px;
        }

        .compln-counts-p {
            margin: 5px 0px 5px 0px;
            padding: 0px 5px;
            width: 20%;
            float: left;
            font-weight: 600;
        }

            .compln-counts-p span {
            }

        .grids-wprs {
            border: 1px solid #ddd;
        }

        .mdl-chats {
            max-width: 500px;
            transform: translateY(0%) !important;
        }

        .chat-bx-wprss {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .chat-bx-txtbx-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            position: relative;
        }

        .chat-bx-txtbx {
            margin: 0px;
            padding: 4px 60px 4px 5px;
            width: 100%;
            float: left;
            height: 36px;
            border: 2px solid #82bbb5;
            border-radius: 3px;
        }

        .chat-bx-btns {
            margin: 0px;
            padding: 2px 10px;
            position: absolute;
            right: 3px;
            top: 3px;
            background: #14cdbc;
            font-weight: 500;
            border-radius: 2px;
            color: #fff;
        }
    </style>


    <script type="text/javascript">
        function openModalDoubt() {
            $('#myModalFltr').modal('show');
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh" data-ng-app="ComplainApp" data-ng-controller="ComplainAppCtrl">
        <div class="container">

            <div id="notification">



                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 235px; height: auto; background: #f00; padding: 4px; border-radius: 4px; color: #fff; position: absolute; right: 0px;">
                        <asp:Label ID="lblmessage" runat="server" class="notif-txt"></asp:Label>
                    </div>

                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>



            <div class="headingtablee">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <h4 class="card-title">Complain List  <a data-toggle="modal" href="#myModalFltr" class="pasgetitle-link">Make Complain</a></h4>
                            </div>
                            <div class="card-body" style="padding-top: 0px;">
                                <div class="doubt-qs-wpr">
                                    <div class="row">
                                        <asp:Repeater ID="rp_doubt" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-4">
                                                    <a data-ng-click="btnFndByComplainNo('<%# Eval("Request_id") %>')" href="#!" data-toggle="modal" data-target="#myModal">
                                                        <div class="doubt-qs-bx-wpr1">
                                                            <div class="doubt-qs-bx-wpr-inrs">
                                                                <div class="doubt-qs-bx-std-contnt1">
                                                                    <asp:Label ID="lbl_std_name" runat="server" class="doubt-qs-bx-std-contnt-name-p1" Text='<%#Bind("message") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_subject" runat="server" Text='<%#Bind("Status_name") %>' class="doubt-qs-bx-std-contnt-subs-p1"></asp:Label>
                                                                    <asp:Label ID="lbl_class" runat="server" Text="Complain" class="doubt-qs-bx-std-contnt-class-p1"></asp:Label>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("New_date") %>' class="doubt-qs-bx-std-contnt-date-p1"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>



                            <div class="modal left fade model-fltr right" id="myModalFltr" role="dialog" style="z-index: 9999" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog model-dialog-fltr">
                                    <div class="modal-body">
                                        <h2 class="mdl-title-h">Make Complain <a href="#!" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></a></h2>
                                        <div class="mdl-cntnt-wpr">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="txtbx-dv-wpr">
                                                        <p class="form-txtbx-p">Upload image (if any)</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8" style="padding-right: 0px; width: 75%">
                                                    <div class="txtbx-dv-wpr">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control pontr-non" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4" style="width: 25%">
                                                    <div class="txtbx-dv-wpr">
                                                        <asp:Button ID="btn_save_image" runat="server" Text="Save" class="mt-2 btn btn-primary" OnClick="btn_save_image_Click" Style="margin: 0px !important; padding: 6px 10px 6px 10px; font-size: 13px; background: #d800ff; border: 1px solid #8200c5;" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="std-mt-dt-grphview" runat="server" id="pdfsDV" visible="false" style="margin: 10px 0px 0px 0px;">
                                                    <div class="row">
                                                        <label for="validationCustom01" class="lebelheadpp" style="margin-bottom: 0px;">Images uploaded by you</label>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Repeater ID="rp_pdfs" runat="server">
                                                            <ItemTemplate>
                                                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 custom-col" style="padding: 2px; width: 25%">
                                                                    <div class="std-mt-dt-grphview-bx-wpr">
                                                                        <div class="std-mt-dt-grphview-bx-imd-dv" style="height: 60px;">
                                                                            <img src="<%#Eval("Attachment") %>" />
                                                                            <asp:Label ID="lbl_attchmnt_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        </div>
                                                                        <div class="std-mt-dt-grphview-bx-cont">
                                                                            <ul class="std-mt-dt-grphview-bx-cont-ul">
                                                                                <li style="width: 100%; border-right: 0px solid #ddd; padding: 1px 0px;">
                                                                                    <asp:LinkButton ID="lnk_delete_image" runat="server" OnClick="lnk_delete_image_Click">Delete</asp:LinkButton></li>
                                                                            </ul>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="txtbx-dv-wpr">
                                                        <p class="form-txtbx-p" style="margin: 15px 0px 0px 0px;">Write Complain</p>
                                                        <asp:TextBox ID="txt_complain" Style="padding: 5px; max-height: 120px; min-height: 120px;"
                                                            placeholder="Write Complain" runat="server" CssClass="form-control pontr-non" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>



                                                <div class="col-md-12 col-xs-12">
                                                    <div class="btns-dv-wpr">
                                                        <asp:Button ID="btn_Submit" runat="server" Text="Send Complain" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div id="myModal" class="modal fade" role="dialog" style="padding-left: 0px !important">
                                <div class="modal-dialog mdl-chats">
                                    <div class="modal-content">
                                        <h2 class="mdl-title-h">Message Details <a href="#!" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></a></h2> 
                                        <div class="modal-body">
                                            <div class="chat-sec">
                                                <div class="chat-msg-bx">
                                                    <div class="chat-msg-bxdfgdfg" data-ng-repeat="x in messages">
                                                        <div class="chat-msg-bx-wpr {{x.Message_by}}">
                                                            <p class="chat-msg-bx-wpr-p">{{x.Message}}</p>
                                                            <p class="chat-msg-bx-wpr-date-time-p">{{x.new_date}}</p>
                                                            <p class="chat-msg-bx-wpr-sent-by">{{x.Message_by}}</p>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="chat-bx-wprss {{messages[0].is_Closed}}">
                                                    <div class="chat-bx-txtbx-wpr">
                                                        <asp:TextBox ID="txt_msgs" class="chat-bx-txtbx" placeholder="Enter your message.." runat="server"></asp:TextBox>
                                                        <ul>
                                                            <li style="list-style-type: none"><a href="#!" data-ng-click="ButtonClick()" class="chat-bx-btns">Send</a></li>
                                                        </ul>
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
            </div>
        </div>
    </div>



    <script type="text/javascript">
        var app = angular.module('ComplainApp', []);
        app.controller('ComplainAppCtrl', function ($scope, $http) {

            var req_id = "";
            $scope.btnFndByComplainNo = function (Request_id) {
                req_id = Request_id;
                $http.get("WebService1.asmx/fetch_chat_messages", { params: { "Request_id": Request_id } }).then(function (response) {
                    $scope.messages = response.data;
                })
            }



            $scope.ButtonClick = function () {
                // alert(req_id);

                var message = $("#<%=txt_msgs.ClientID %>").val();
                if (message == "") {
                    messge("Please Enter Your Message.");
                }
                else {
                    $http.get("WebService1.asmx/send_complain_data", { params: { "Message": message, "Request_id": req_id } }).then(function (response) {
                        $scope.client_dt = response.data;
                        if ($scope.client_dt == "") {

                            $("#<%=txt_msgs.ClientID %>").val('');
                            messge("Your Message Send Successfully.");

                            $http.get("WebService1.asmx/fetch_chat_messages", { params: { "Request_id": req_id } }).then(function (response) {
                                $scope.messages = response.data;
                            })
                        }
                    })
                }
                $("#intsLoader").addClass("hidden");
            }
        });


        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.notificationpan').hide().slideDown(1000);
            $('.notificationpan').delay(4000).show().slideUp(1000);
        }
    </script>
</asp:Content>
