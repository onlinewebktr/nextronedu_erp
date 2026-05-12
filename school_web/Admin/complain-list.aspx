<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="complain-list.aspx.cs" Inherits="school_web.Admin.complain_list" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Complain List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <style>
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
            padding: 2px 5px 1px 5px;
            min-width: 30px;
            float: left;
            font-size: 16px;
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

        .modal-dialog {
            max-width: 700px;
            top: 30px;
        }

        .modal {
            background: rgb(0 0 0 / 31%);
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
            padding: 0px;
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
            /*height: 400px;*/
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
            margin: 8px 0px 5px 0px;
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

        .Admin {
            float: right;
            background: #d0ebff;
            border: 1px solid #b3daf7;
        }

        .You {
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

        .modal.fade .modal-dialog {
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 55%);
        }
    </style>
    <link href="../assets/css/steps.css" rel="stylesheet" />
    <link href="../assets/css/odrHistry.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_branch" runat="server" />
    <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-white">Success Alerts</h6>
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-dark">Warning Alerts</h6>
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                </div>
            </div>
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Complain</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Complain</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row" data-ng-app="ComplainApp" data-ng-controller="ComplainAppCtrl">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Complain List</h6>
                    <ul class="coml-lst-ul">
                        <li>
                            <a href="need-help.aspx">Create Complain</a>
                        </li>
                    </ul>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">By Status</label>
                                                        <asp:DropDownList ID="ddl_complain_status" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_complain_status_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                                            <asp:ListItem Value="2">Process</asp:ListItem>
                                                            <asp:ListItem Value="3">Hold</asp:ListItem>
                                                            <asp:ListItem Value="4">Closed</asp:ListItem>
                                                            <asp:ListItem Value="5">Forward</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>


                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>

                                                    <%--<div class="col-sm-4">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>--%>
                                                </div>
                                            </div>

                                            <table>
                                                <tr>
                                                    <td>
                                                        <span style="float: right">
                                                            <p class="count-p">
                                                                Total Complain : 
                                    <asp:Label ID="lbl_total_data" runat="server" Text="0"></asp:Label>
                                                            </p>

                                                            <p class="count-p">
                                                                Pending Complain : 
                                    <asp:Label ID="lbl_pending_comp" runat="server" Text="0"></asp:Label>
                                                            </p>
                                                            <p class="count-p">
                                                                Process Complain : 
                                    <asp:Label ID="lbl_runing_comp" runat="server" Text="0"></asp:Label>
                                                            </p>
                                                            <p class="count-p">
                                                                Forward Complain : 
                                    <asp:Label ID="lbl_forward" runat="server" Text="0"></asp:Label>
                                                            </p>
                                                            <p class="count-p">
                                                                Hold Complain : 
                                    <asp:Label ID="lbl_ttl_hold_comp" runat="server" Text="0"></asp:Label>
                                                            </p>
                                                            <p class="count-p">
                                                                Closed Complain : 
                                    <asp:Label ID="lbl_closed_comp" runat="server" Text="0"></asp:Label>
                                                            </p>

                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="example2" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Complain Id</th>
                                                        <%--<th>Remark</th>--%>
                                                        <th>Complain Date</th>
                                                        <th>Last Update</th>

                                                        <%-- <th>Closed By</th>
                                                        <th id="closedDate" runat="server">Closed Date</th>--%>
                                                        <th>Complain By User id</th>
                                                        <th>Complain By</th>
                                                        <th>User Mobile No.</th>
                                                        <th>Call Back Time</th>
                                                        <th>Status</th>
                                                        <th style="min-width: 105px"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_req_id" runat="server" Text='<%#Bind("Request_id") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_unread_message" runat="server" Style="padding: 4px; background: #2153d2; height: 10px; width: 10px; display: inline-block; border-radius: 50px;"></asp:Label>
                                                                </td>

                                                                <%--<td>
                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks") %>'></asp:Label>
                                                                </td>--%>

                                                                <td>
                                                                    <asp:Label ID="lbl_remarks" Visible="false" runat="server" Text='<%#Bind("Remarks") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("New_Request_date") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>

                                                                    <asp:Label ID="lbl_msg_count" Visible="false" runat="server" Text='<%#Bind("msgCount") %>'></asp:Label>
                                                                </td>

                                                                <%--<td>
                                                                    <asp:Label ID="lbl_close_date" runat="server" Text='<%#Bind("Close_date") %>'></asp:Label>
                                                                </td>--%>


                                                                <%-- <td>
                                                                    <asp:Label ID="lbl_closed_date" runat="server" Text='<%#Bind("Solve_by") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" Visible="false" runat="server" Text='<%#Bind("Close_date") %>'></asp:Label>
                                                                </td>--%>

                                                                <td>
                                                                    <asp:Label ID="lbl_closed_date" runat="server" Text='<%#Bind("Last_rep_date") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_User_id" runat="server" Text='<%#Bind("User_id") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_Request_by_Name" runat="server" Text='<%#Bind("Request_by_Name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("Request_User_Mobile_No") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Request_Call_Back_time" runat="server" Text='<%#Bind("Request_Call_Back_time") %>'></asp:Label>
                                                                </td>



                                                                <td>
                                                                    <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                                </td>

                                                                <td>





                                                                    <a data-ng-click="btnFndByComplainNo('<%# Eval("Request_id") %>')" href="#!" data-toggle="modal" data-target="#myModal" class="lnk-btn" style="width: 30px;" title="Message"><i class="bx bx-message-rounded-dots"></i><span></span></a>

                                                                    <a href="javascript:" data-toggle="modal" data-target="#myModalProgress" style="margin: 0px 0px 0px 5px; text-align: left; background: #e3ae00; width: 30px; font-size: 16px;"
                                                                        title="Track Status"
                                                                        class="lnk-btn" data-ng-click="btnFndByOrderNo('<%# Eval("Request_id") %>~<%#Eval("Status") %>')"><span><i class="bx bx-line-chart"></i><span></span></a>



                                                                    <asp:LinkButton ID="lnk_view_Work_Progress" runat="server" Style="margin: 5px 0px 0px 0px; display: none; text-align: left; background: #e3ae00; min-width: 100px;"
                                                                        class="lnk-btn" OnClick="lnk_view_Work_Progress_Click"><i class="bx bx-line-chart"></i><span>Track Status</asp:LinkButton>



                                                                    <asp:Label ID="lbl_linksdV" runat="server" Visible="false">
                                                                     <a style="background: #03A9F4; text-align: left;  width: 30px; margin: 0px 0px 0px 5px; font-size: 16px;" target="_blank" title="Meeting"
                                                                         href="<%#Eval("Complain_Onlinemeeting") %>" class="lnk-btn"><i class="bx bxl-zoom"></i><span></span></a>
                                                                    </asp:Label>


                                                                    <asp:Label ID="lbl_feedbackDV" runat="server" Visible="false">


                                                                     <a style="background: #09ad07; text-align: left;  width: 30px; margin: 0px 0px 0px 5px; font-size: 17px; padding: 2px 5px 1px 5px;" target="_blank" title="Feedback" id="url" runat="server" class="lnk-btn"><i class="bx bx-message-alt-edit"></i><span></span></a>

                                                                    </asp:Label>

                                                                    <asp:Label ID="lbl_links" runat="server" Visible="false" Text='<%#Bind("Complain_Onlinemeeting") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_feedback_gien" runat="server" Visible="false" Text='<%#Bind("Feedback") %>'></asp:Label>
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
                    </div>


                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 3px 17px;">
                                    <h5 class="modal-title">Message Details</h5>
                                    <a href="#!" data-dismiss="modal" class="btn btn-secondary">Close</a>
                                </div>
                                <div class="modal-body">
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation">
                                            <div class="chat-sec">
                                                <div class="chat-msg-bx">
                                                    <div class="chat-msg-bxdfgdfg" data-ng-repeat="x in messages track by $index">
                                                        <div class="chat-msg-bx-wpr {{x.Message_by}}">
                                                            <p class="chat-msg-bx-wpr-p">{{x.Message}}</p>
                                                            <p class="chat-msg-bx-wpr-date-time-p">{{x.new_date}}</p>
                                                            <p class="chat-msg-bx-wpr-sent-by">{{x.Message_by_Name}}</p>


                                                            <ul class="attchmnt-ul">
                                                                <li data-ng-repeat="item in x.MyMessagesItem track by $index"><a href="{{item.Documents}}" download="" title="Download"><i class="bx bx-download"></i></a></li>
                                                            </ul>
                                                        </div>
                                                    </div>







                                                    <%--<div data-ng-repeat="x in messages track by $index">
                                                        <div class="profile-odr-id-h-sec">
                                                            <h2>{{x.Message}}</h2>
                                                        </div>


                                                        <div class="profile-tbl-wpr">
                                                            <table class="table-bordered">
                                                                <tr data-ng-repeat="item in x.MyMessagesItem track by $index">
                                                                    <td>{{item.Documents}}</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div id="myModal1" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 3px 17px;">
                                    <h5 class="modal-title">Progress Details</h5>
                                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                                </div>
                                <div class="modal-body">
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation">
                                            <div class="stepper-wrapper" style="display: flex; justify-content: space-between; margin-bottom: 0px; margin-top: 15px;">
                                                <div class="stepper-item" id="a1" runat="server">
                                                    <div class="step-counter">1</div>
                                                    <div class="step-name">
                                                        <asp:LinkButton ID="lnk_Pending" runat="server" OnClick="lnk_Pending_Click">Pending</asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="stepper-item" id="a2" runat="server">
                                                    <div class="step-counter">2</div>
                                                    <div class="step-name">
                                                        <asp:LinkButton ID="lnk_running" runat="server" OnClick="lnk_running_Click">Process</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="stepper-item" id="a3" runat="server">
                                                    <div class="step-counter">3</div>
                                                    <div class="step-name">
                                                        <asp:LinkButton ID="lnk_closed" runat="server" OnClick="lnk_closed_Click">Closed</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div style="margin: 0px; padding: 0px; float: left; height: auto">
                                                <asp:Panel ID="pnl_admission_type" runat="server">
                                                    <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; border: 1px solid #cec5c5;">
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lbl_date" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lbl_lastquery" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div id="myModalProgress" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog" style="max-width: 900px;">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 3px 17px;">
                                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Complain Progress</h5>
                                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                                        class="btn btn-primary find-dv-btn">Close</a>
                                </div>
                                <div class="modal-body">
                                    <div class="grid-wraper" id="orderHistoryTable">
                                        <div class="odrhistry-bx">
                                            <p style="margin: 0px 0px 5px 0px; padding: 0px 0px 0px 0px; width: 100%; float: left; font-weight: 500;">
                                                Complain Id : {{odrHistry[0].Complain_id}}
                                            </p>
                                            <div class="order-tracing-ico-sec">
                                                <div data-ng-repeat="x in odrHistry" class="order-tracing-ico-wpr trace-bdr-right {{x.first_pendingshowStatus}} {{x.ClosedLast}} {{x.process_status}}" style="width: {{x.dvWidth}}">
                                                    <div class="order-tracing-ico-wpr-inr">
                                                        <i class="bx bx-list-check order-tracing-ico {{x.first_pending}}"></i>
                                                        <p class="order-tracing-ico-wpr-p">
                                                            {{x.Status}}
                                                        </p>
                                                        <div class="more-info {{x.MoreInfo}}">
                                                            <p class="more-info-p">Date : {{x.dates}}</p>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--<div class="order-tracing-ico-wpr trace-bdr-right {{x.second_confirmshowStatus}}">
                                                <div class="order-tracing-ico-wpr-inr">
                                                    <i class="bx bx-user-check order-tracing-ico middl-ico {{x.second_confirm}}" style="font-size: 27px; padding: 0px 0px 0px 6px;"></i>
                                                    <p class="order-tracing-ico-wpr-p">
                                                        Confirm Order
                                                    </p>
                                                    <div class="more-info {{x.MoreInfo2}}">
                                                        <p class="more-info-p">Date : {{x.shipping_date}}</p>
                                                        <p class="more-info-p">Delivery Boy : {{x.DeliveryBoy}}</p> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-tracing-ico-wpr trace-bdr-right {{x.third_pickedshowStatus}}">
                                                <div class="order-tracing-ico-wpr-inr">
                                                    <i class="bx bx-shopping-bag order-tracing-ico middl-ico {{x.third_picked}}" style="font-size: 23px; padding: 0px 0px 0px 2px;"></i>
                                                    <p class="order-tracing-ico-wpr-p">
                                                        Picked Order
                                                    </p>
                                                    <div class="more-info {{x.MoreInfo3}}">
                                                        <p class="more-info-p">Date : {{x.PickedDate}}</p>
                                                        <p class="more-info-p">Customer OTP : {{x.CusTOTP}}</p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="order-tracing-ico-wpr {{x.fourth_delivershowStatus}}">
                                                <div class="order-tracing-ico-wpr-inr">
                                                    <i class="bx bx-home-heart order-tracing-ico {{x.fourth_deliver}}"></i>
                                                    <p class="order-tracing-ico-wpr-p">
                                                        Delivered Order
                                                    </p>
                                                    <div class="more-info {{x.MoreInfo4}}">
                                                        <p class="more-info-p">Date : {{x.DeliveredDate}}</p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="order-tracing-ico-wpr {{x.fifth_cancelshow_Status}}">
                                                <div class="order-tracing-ico-wpr-inr">
                                                    <i class="bx bx-block order-tracing-ico {{x.fifth_cancel}}"></i>
                                                    <p class="order-tracing-ico-wpr-p">
                                                        Cancel Order
                                                    </p>
                                                    <div class="more-info">
                                                        <p class="more-info-p">Date : {{x.CancelDate}}</p>
                                                        <p class="more-info-p">Canceled By : {{x.CanceledBy}}</p>
                                                        <p class="more-info-p">Reason : {{x.CanceledReason}}</p>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="grid-wraper">
                                        <div class="job-not-fount-div hidden" id="not_found">
                                            <p class="job-not-fount-p">Data not found!</p>
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
        var app = angular.module('ComplainApp', []);
        app.controller('ComplainAppCtrl', function ($scope, $http) {


            $scope.btnFndByOrderNo = function (orderid, status) {
                var complain_id = orderid;
                $http.get("chat.asmx/fetch_request_progress", { params: { "Paramiter1": complain_id } }).then(function (response) {
                    $scope.odrHistry = response.data;
                    if ($scope.odrHistry == "") {
                        $("#orderHistoryTable").addClass("hidden");
                        $("#not_found").removeClass("hidden");
                        //$("#modalOrderTraceinG").removeClass("hidden");
                    }
                    else {
                        //$("#modalOrderTraceinG").removeClass("hidden");
                        $("#orderHistoryTable").removeClass("hidden");
                        $("#not_found").addClass("hidden");
                    }
                })
            }







            //FindByOrderId 
            $scope.btnFndByComplainNo = function (Request_id) {
                $http.get("chat.asmx/fetch_chat_messages", { params: { "Request_id": Request_id } }).then(function (response) {
                    $scope.messages = response.data;
                })
            }
        });
    </script>

    <script type="text/javascript">
        function openModal() {
            $("#myModal1").show();
            $('#myModal1').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal1").hide();
            $('#myModal1').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>
</asp:Content>
