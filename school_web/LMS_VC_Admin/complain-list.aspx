<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="complain-list.aspx.cs" Inherits="school_web.LMS_VC_Admin.complain_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Complaint Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table td {
            padding: 5px 5px 5px 5px !important;
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

        .dt-button-collection {
            margin-top: -59.4px!important;
        }

        .input-group > .form-control, .input-group > .form-control-plaintext, .input-group > .custom-select, .input-group > .custom-file {
            position: relative;
            flex: 1 1 auto;
            width: 1%;
            margin-bottom: 0;
            font-weight: bold!important;
        }

        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 66px!important;
            right: 10px;
            padding: 10px 10px;
            width: 290px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgb(154 154 154 / 80%);
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
            top: -25px;
            left: 126px;
        }
    </style>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%=txt_startdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


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
                        <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        Complaint Details
                    </div>
                </div>
                <div class="page-title-actions" style="display: none">
                    <a href="feedback-list.aspx">
                        <button type="button" class="btn-shadow btn btn-info">
                            <span class="btn-icon-wrapper pr-2 opacity-7">
                                <i class="pe-7s-plus fa-w-20"></i>
                            </span>
                            Feedback
                        </button>
                    </a>
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
            <div class="card-body">

                <div class="form-row" style="font-size: 16px;">
                    <div class="row" style="padding: 10px 0px 10px 0px; border: 1px solid #ccc; margin: 0px auto; background: #dbdbdb;">

                        <div class="col-md-12">
                            <div class="form-group form-contro" style="text-align: center">
                                <asp:RadioButton ID="rd_all" GroupName="ak" runat="server" Text="ALL" OnCheckedChanged="rd_all_CheckedChanged" AutoPostBack="true" Checked="true" />
                                <asp:RadioButton ID="rd_Twodate_wise" GroupName="ak" runat="server" Text="Search Between Two Dates" AutoPostBack="true" OnCheckedChanged="rd_Twodate_wise_CheckedChanged" />
                                <asp:RadioButton ID="rd_class_and_section_wise" GroupName="ak" runat="server" Text="Class & Section Wise" OnCheckedChanged="rd_class_and_section_wise_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rd_Admission_no_wise" GroupName="ak" runat="server" Text="Admission No. Wise" OnCheckedChanged="rd_Admission_no_wise_CheckedChanged" AutoPostBack="true" />


                            </div>
                        </div>
                        <hr style="width: 100%; margin: 0px 0px 0px 0px; border-top: 1px solid rgb(50 42 42 / 58%);" />
                        <asp:Panel ID="pnl_Twodate_wise" runat="server" Visible="false">
                            <div class="col-md-6" style="float: left;">
                                <div class="form-group">
                                    <label style="float: left;">Select Start Date</label>
                                    <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                        <asp:TextBox ID="txt_startdate" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-6" style="float: left;">
                                <div class="form-group">
                                    <label style="float: left;">Select End Date</label>
                                    <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                        <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnl_class_and_section_wise" runat="server" Visible="false" Style="width: 86%">
                            <div class="col-md-3" style="float: left;">
                                <div class="form-group">
                                    <label style="float: left;">Select Start Date</label>
                                    <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                        <asp:TextBox ID="txt_start_date_cl" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-3" style="float: left;">
                                <div class="form-group">
                                    <label style="float: left;">Select End Date</label>
                                    <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                        <asp:TextBox ID="txt_end_date_cl" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    </div>
                                </div>
                            </div>



                            <div class="col-md-3" style="float: left;">
                                <div class="form-group">
                                    <label style="float: left;">Select Class</label>
                                    <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3" style="float: left;">
                                <div class="form-group">
                                    <label style="float: left;">Select Section</label>
                                    <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                        <asp:DropDownList ID="dd_section" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                        </asp:Panel>
                        <asp:Panel ID="pnl_admission_report_wise" runat="server" Visible="false">
                            <div class="col-md-12" style="float: left;">
                                <div class="form-group">
                                    <label style="float: left;">Admission No.  </label>
                                    <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                        <asp:TextBox ID="txt_admissiono" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-md-1" style="float: left;">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_find_Click" Visible="false" Style="margin: 10px 0px 0px 0px;" />

                            </div>
                        </div>



                    </div>
                </div>
                <hr />

                <asp:Panel ID="pnl_view" runat="server" Visible="false">

                    <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                        <thead>
                            <tr>
                                <th colspan="13">
                                    <p style="width: auto; float: left; background-color: #e50800; padding: 4px 9px 0px 0px; margin: 0px 5px 0px 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                        <asp:RadioButton ID="rd_pendin_req" runat="server" Checked="true" GroupName="qw" Text="Pending Complaint" OnCheckedChanged="rd_pendin_req_CheckedChanged1" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                    </p>
                                    <p style="width: auto; float: left; background-color: #29a700; padding: 4px 9px 0px 0px; margin: 0px; line-height: 10px; color: #fff; border-radius: 2px; font-weight: 500;">
                                        <asp:RadioButton ID="rd_closed_req" runat="server" GroupName="qw" Text="Closed Complaint" OnCheckedChanged="rd_closed_req_CheckedChanged1" AutoPostBack="true" Style="margin: 0px 0px 0px 10px" />
                                    </p>
                                    <span style="float: right">
                                        <p style="width: auto; float: left; background-color: #ffefef; padding: 3px; margin: 0px 3px; font-weight: 500;">
                                            Total Complaint : 
                                    <asp:Label ID="lbl_total_data" runat="server" Text="Label"></asp:Label>
                                        </p>
                                        <p style="width: auto; float: left; background-color: #ffefef; padding: 3px; margin: 0px 3px; font-weight: 500;">
                                            Total Pending Complaint : 
                                    <asp:Label ID="lbl_pending_comp" runat="server" Text="Label"></asp:Label>
                                        </p>
                                        <p style="width: auto; float: left; background-color: #ffefef; padding: 3px; margin: 0px 3px; font-weight: 500;">
                                            Total Closed Complaint : 
                                    <asp:Label ID="lbl_closed_comp" runat="server" Text="Label"></asp:Label>
                                        </p>
                                    </span>
                                </th>
                            </tr>
                            <tr>
                                <th>#</th>
                                <th>Request Id</th>
                                <th>Name</th>
                                <th>Admission No.</th>
                                <th>Class</th>
                                <th>Section</th>
                                <th>Roll No.</th>
                                <th>Mobile No.</th>

                                <th>Req. Date</th>
                                <th>Last Reply</th>
                                <th id="closedDate" runat="server">Closed Date</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:HiddenField ID="hdUserID" runat="server" />
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
                                        <td>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Bind("Full_name") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_mobile_no" runat="server" Text='<%#Bind("Mobile_no") %>'></asp:Label>
                                        </td>



                                        <td>
                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Created_date") %>'></asp:Label>
                                            -
                                        <asp:Label ID="lbl_time" runat="server" Text='<%#Bind("Created_time") %>'></asp:Label>
                                            <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_msg_count" runat="server" Text='<%#Bind("msgCount") %>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_last_reply_date" runat="server" Text='<%#Bind("Created_date") %>'></asp:Label>
                                            -
                                        <asp:Label ID="lbl_last_reply_date_timel2" runat="server" Text='<%#Bind("Created_time") %>'></asp:Label>
                                        </td>
                                        <td class="<%#Eval("HideShow") %>">
                                            <asp:Label ID="lbl_closed_date" runat="server" Text='<%#Bind("Closed_date") %>'></asp:Label>
                                            -
                                        <asp:Label ID="lbl_closed_idate" runat="server" Text='<%#Bind("Closed_time") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btn_go_to_chat" class="lnk-btn" runat="server" OnClick="btn_go_to_chat_Click">View Message</asp:LinkButton>


                                            <asp:Label ID="lbl_related_with_order" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_Is_related_with_order" Visible="false" runat="server" Text='<%#Bind("Is_related_with_order") %>'></asp:Label>

                                            <asp:Label ID="lbl_order_id" runat="server" Text='<%#Bind("Order_id") %>' Visible="false"></asp:Label>
                                             <asp:Label ID="lbl_User_id" runat="server" Text='<%#Bind("User_id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_show_order_id" runat="server" Text="NA" Visible="false" Style="display: none"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>

                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
