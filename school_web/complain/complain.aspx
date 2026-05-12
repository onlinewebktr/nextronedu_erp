<%@ Page Title="" Language="C#" MasterPageFile="~/complain/main.Master" AutoEventWireup="true" CodeBehind="complain.aspx.cs" Inherits="school_web.complain.complain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Complain List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">


    <script src="../assets/Angular/angular.min.js"></script>
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <%--<script src="../assets/js/jquery-1.10.2.min.js"></script>--%>
    <style>
        .clndr-bx-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .ui-datepicker {
            border: 1px solid #ddd !important;
            background: #303030!important;
            color: #2d2d2d!important;
        }

        .ui-widget-header {
            border: 1px solid #00b7bd;
            background: #0ecad1 url(images/ui-bg_gloss-wave_35_f6a828_500x100.png) 50% 50% repeat-x;
        }

        .ui-datepicker select.ui-datepicker-month, .ui-datepicker select.ui-datepicker-year {
            width: 45%;
            background: #00a2a7;
            border: 1px solid #02989d;
        }
    </style>




    <style>
        .fade:not(.show) {
            opacity: 1;
        }

        .modal-content {
            margin: 25px 0px 0px 0px;
            float: left;
        }

        .modal-body, .modal-footer, .modal-header {
            padding: 10px;
        }

        .modal-dialog {
            max-width: 750px;
        }

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
    </style>
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {



            $("#<%=txt_school_name.ClientID%>").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: 'complain.aspx/Getcusmob',
                        data: "{ 'search_area': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });
    </script>



    <script type="text/javascript">
        function openModal() {
            $('#mdl_update_status').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="container-small">
            <div class="page-title-container">
                <div class="row">
                    <div class="col-auto mb-3 mb-md-0 me-auto">
                        <div class="w-auto sw-md-30">
                            <a href="#!" class="muted-link pb-1 d-inline-block breadcrumb-back">
                                <i data-acorn-icon="chevron-left" data-acorn-size="13"></i>
                                <span class="text-small align-middle">Complain</span>
                            </a>
                            <h1 class="mb-0 pb-0 display-4" id="H1">Report</h1>
                        </div>
                    </div>
                    <div id="notification">
                        <div id="pan" class="notificationpan">
                            <div style="float: left; width: 235px; height: auto;">
                                <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <h2 class="small-title">Complain List</h2>
            <div class="row g-2 mb-5">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row g-3">
                                <div class="col-xl-2">
                                    <div class="txtbx-dv">
                                        <p class="name-name-p">By Status</p>
                                        <asp:DropDownList ID="ddl_complain_status" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_complain_status_SelectedIndexChanged">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Process</asp:ListItem>
                                            <asp:ListItem Value="3">Hold</asp:ListItem>
                                            <asp:ListItem Value="4">Closed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-xl-2">
                                    <div class="txtbx-dv">
                                        <p class="name-name-p">Date From</p>
                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("#<%=txt_s_date.ClientID %>").datepicker({
                                                    dateFormat: "dd/mm/yy",
                                                    changeMonth: true,
                                                    changeYear: true,
                                                    readOnly: true,
                                                    yearRange: "1900:2100",
                                                }).attr("readonly", "true");
                                            });
                                        </script>
                                    </div>
                                </div>
                                <div class="col-xl-2">
                                    <div class="txtbx-dv">
                                        <p class="name-name-p">Date To</p>
                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control"></asp:TextBox>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("#<%=txt_e_date.ClientID %>").datepicker({
                                                    dateFormat: "dd/mm/yy",
                                                    changeMonth: true,
                                                    changeYear: true,
                                                    readOnly: true,
                                                    yearRange: "1900:2100",
                                                }).attr("readonly", "true");
                                            });
                                        </script>
                                    </div>
                                </div>
                                <div class="col-xl-1">
                                    <div class="txtbx-dv">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="save-btns" Style="margin: 33px 0px 0px 0px;"></asp:Button>
                                    </div>
                                </div>

                                <div class="col-xl-1"></div>
                                <div class="col-xl-3">
                                    <div class="txtbx-dv">
                                        <p class="name-name-p">School Name</p>
                                        <asp:TextBox ID="txt_school_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-1">
                                    <div class="txtbx-dv">
                                        <asp:Button ID="btn_find_by_school" runat="server" Text="Find" class="save-btns" OnClick="btn_find_by_school_Click"></asp:Button>
                                    </div>
                                </div>

                                <div data-ng-app="ComplainApp" data-ng-controller="ComplainAppCtrl">
                                    <div class="col-xl-12">
                                        <div class="grids-wprs">
                                            <p class="compln-counts-p">
                                                Total Complain :
                                            <asp:Label ID="lbl_all_count" runat="server"></asp:Label>
                                            </p>
                                            <p class="compln-counts-p">
                                                Pending Complain :
                                            <asp:Label ID="lbl_pending_count" runat="server"></asp:Label>
                                            </p>
                                            <p class="compln-counts-p">
                                                Running Complain :
                                            <asp:Label ID="lbl_runing_count" runat="server"></asp:Label>
                                            </p>
                                            <p class="compln-counts-p">
                                                Hold Complain :
                                            <asp:Label ID="lbl_hold_count" runat="server"></asp:Label>
                                            </p>
                                            <p class="compln-counts-p">
                                                Closed Complain :
                                            <asp:Label ID="lbl_closed_count" runat="server"></asp:Label>
                                            </p>
                                            <table style="width: 100%; margin: 0px;" id="example" class="table table-hover table-striped table-bordered">
                                                <tr>
                                                    <th>#</th>
                                                    <th>School Name</th>
                                                    <th>Complain Id</th>
                                                    <th>Complain Date</th>
                                                    <th>Last Update</th>
                                                    <th>Status</th>
                                                    <th></th>
                                                </tr>

                                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_school_name" runat="server" Text='<%#Bind("School_name") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_req_id" runat="server" Text='<%#Bind("Request_id") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_remarks" Visible="false" runat="server" Text='<%#Bind("Remarks") %>'></asp:Label>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("New_Request_date") %>'></asp:Label>
                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_msg_count" Visible="false" runat="server" Text='<%#Bind("msgCount") %>'></asp:Label>
                                                            </td>


                                                            <td>
                                                                <asp:Label ID="lbl_closed_date" runat="server" Text='<%#Bind("Last_rep_date") %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:LinkButton ID="lnk_update_status" runat="server" Style="margin: 0px 3px 0px 0px;" OnClick="lnk_update_status_Click" class="lnk-btn">Update Status</asp:LinkButton>
                                                                <a data-ng-click="btnFndByComplainNo('<%# Eval("Request_id") %>')" style="background: #00994b;" href="#!" data-toggle="modal" data-target="#myModal" class="lnk-btn"><i class="bx bx-message-rounded-dots"></i><span>View Status</span></a>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
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
                                                                    <p class="topmdl-popups">School Name : <span>{{messages[0].School_name}}</span> </p>
                                                                    <p class="topmdl-popups">Complain Id : <span>{{messages[0].Request_id}}</span> </p>
                                                                    <p class="topmdl-popups">Complain Date : <span>{{messages[0].Complain_date}}</span> </p>
                                                                    <div class="chat-msg-bxdfgdfg" data-ng-repeat="x in messages track by $index">
                                                                        <div class="chat-msg-bx-wpr {{x.Message_by}}">
                                                                            <p class="chat-msg-bx-wpr-p">{{x.Message}}</p>
                                                                            <p class="chat-msg-bx-wpr-date-time-p">{{x.new_date}}</p>
                                                                            <p class="chat-msg-bx-wpr-sent-by">{{x.Message_by}}</p>


                                                                            <ul class="attchmnt-ul">
                                                                                <li data-ng-repeat="item in x.MyMessagesItem track by $index"><a href="{{item.Documents}}" download="" title="Download"><i class="fa fa-download" aria-hidden="true"></i></a></li>
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
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div id="mdl_update_status" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title">Update Complain</h5>
                    <a href="#!" data-dismiss="modal" class="btn btn-secondary">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation">
                            <div class="chat-sec">
                                <div class="chat-msg-bx">
                                    <p class="topmdl-popups">
                                        School Name :
                                        <asp:Label ID="lbl_school_names" runat="server" Text="Label"></asp:Label>
                                    </p>
                                    <p class="topmdl-popups">
                                        Complain Id :
                                        <asp:Label ID="lbl_complain_id" runat="server" Text="Label"></asp:Label>
                                    </p>
                                    <p class="topmdl-popups">
                                        Complain Date :
                                        <asp:Label ID="lbl_complain_date" runat="server" Text="Label"></asp:Label>
                                    </p>


                                    <div class="col-xl-12">
                                        <p class="name-name-p">Status</p>
                                        <asp:DropDownList ID="ddl_status" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Process</asp:ListItem>
                                            <asp:ListItem Value="3">Hold</asp:ListItem>
                                            <asp:ListItem Value="4">Closed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xl-12">
                                        <p class="name-name-p">Remark<sup>*</sup></p>
                                        <asp:TextBox ID="txt_remark" runat="server" class="form-control" TextMode="MultiLine" Style="height: 80px"></asp:TextBox>
                                    </div>
                                    <div class="col-xl-6" style="float: left; display: inherit;">
                                        <p class="name-name-p">Attachment(if any)</p>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                    </div>
                                    <div class="col-xl-6" style="float: left; display: inherit;">
                                        <asp:Button ID="btn_save_attechment" runat="server" Text="Save" CssClass="save-btns" OnClick="btn_save_attechment_Click" Style="margin: 34px 0px 0px 15px;" />
                                    </div>
                                    <div class="col-xl-12" id="documentS" runat="server" visible="false">
                                        <p class="name-name-p"><b>Uploaded Attachment</b></p>

                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Attachment</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>

                                                                <td style="text-align: left;">
                                                                    <a href='<%# Eval("Documents") %>' download>View</a>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="fa fa-trash-o"> </i></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-xl-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="save-btns" OnClick="btn_Submit_Click" />
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
            $scope.btnFndByComplainNo = function (Request_id) {
                $http.get("chat.asmx/fetch_chat_messages", { params: { "Request_id": Request_id } }).then(function (response) {
                    $scope.messages = response.data;
                })
            }
        });
    </script>






</asp:Content>
