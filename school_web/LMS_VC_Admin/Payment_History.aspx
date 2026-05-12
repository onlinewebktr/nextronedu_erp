<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Payment_History.aspx.cs" Inherits="school_web.LMS_VC_Admin.Payment_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
   Verify Missing Payment Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
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

        calender-icon {
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
    <script>
        $(function () {
            $("#<%=txt_start_date_su.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_end_date_su.ClientID %>").datepicker({
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
                        <i class="pe-7s-display1 icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">  Verify Missing Payment Entry</asp:Literal>

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
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">

                        <div class="form-row" style="font-size: 16px;">
                            <div class="row" style="padding: 10px 0px 10px 0px; border: 1px solid #ccc; margin: 0px auto; background: #dbdbdb;">

                                <div class="col-md-12">
                                    <div class="form-group form-contro" style="text-align: center">
                                        <asp:RadioButton ID="rd_Twodate_wise" GroupName="ak" runat="server" Text="Search Between Two Dates" Checked="true" AutoPostBack="true" OnCheckedChanged="rd_Twodate_wise_CheckedChanged" />
                                        <asp:RadioButton ID="rd_class_and_section_wise" GroupName="ak" runat="server" Text="Class & Section Wise" OnCheckedChanged="rd_class_and_section_wise_CheckedChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="rd_Admission_no_wise" GroupName="ak" runat="server" Text="Admission No. Wise" OnCheckedChanged="rd_Admission_no_wise_CheckedChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="rd_Transaction_Id_Wise" GroupName="ak" runat="server" Text="Reference Id Wise" OnCheckedChanged="rd_Transaction_Id_Wise_CheckedChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="rd_Success_and_failure_wise" GroupName="ak" Visible="false" runat="server" Text="Success & Failure" OnCheckedChanged="rd_Success_and_failure_wise_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <hr style="width: 100%; margin: 0px 0px 0px 0px; border-top: 1px solid rgb(50 42 42 / 58%);" />
                                <asp:Panel ID="pnl_Twodate_wise" runat="server" Visible="true">
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


                                <asp:Panel ID="pnl_transaction_wise" runat="server" Visible="false">
                                    <div class="col-md-12" style="float: left;">
                                        <div class="form-group">
                                            <label style="float: left;">Reference Id</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:TextBox ID="txt_transaction_id_wise" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_Success_Failure" runat="server" Visible="false">
                                    <div class="col-md-4" style="float: left;">
                                        <div class="form-group">
                                            <label style="float: left;">Select Start Date</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:TextBox ID="txt_start_date_su" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4" style="float: left;">
                                        <div class="form-group">
                                            <label style="float: left;">Select End Date</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:TextBox ID="txt_end_date_su" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="float: left;">
                                        <div class="form-group">
                                            <label style="float: left;">Success &  Failure</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">

                                                <asp:DropDownList ID="ddl_status" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="success">Success</asp:ListItem>
                                                    <asp:ListItem Value="failure">Failure</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>




                                <div class="col-md-1" style="float: left;">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_find_Click" Style="margin: 10px 0px 0px 0px;" />

                                    </div>
                                </div>



                            </div>
                        </div>
                        <hr />

                        <asp:Panel ID="pnl_view" runat="server" Visible="false">
                            <h5 class="card-title">
                                <asp:Label ID="lbl_month_year" runat="server" Style="color: #f81b1b;"></asp:Label>&nbsp; | No. of Transaction:-<asp:Label ID="lbl_total" runat="server" Style="color: #f81b1b;"></asp:Label>

                            </h5>


                            <hr />
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>

                                    <tr>

                                        <th>Sl No.</th>
                                        <th>Student Name</th>
                                        <th>Admission No.</th>
                                        <th>Class</th>
                                        <th>Section</th>

                                        <th>Month Name</th>
                                        <th>Reference Id(Order Tracking ID)</th>
                                        <th>Software Transaction Id</th>
                                        <th>Transacion Date</th>
                                        <th>Entry Date</th>
                                        <th>Total Amount</th>
                                        <th>Dicount Amount</th>
                                        <th>Payable Amount</th>
                                          <th>Status</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RpDetailsStudent" runat="server"  OnItemDataBound="RpDetailsStudent_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>

                                                <td>
                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("Classname") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_month" runat="server" Style="word-break: break-all" Text='<%#Bind("months") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_OrderTrackingID" runat="server" Text='<%#Bind("OrderTrackingID") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_TransactionId" runat="server" Text='<%#Bind("TransactionId") %>'></asp:Label>
                                                </td>


                                                <td>
                                                    <asp:Label ID="lbl_date11" runat="server" Text='<%#Bind("date1") %>'></asp:Label>
                                                      <asp:Label ID="lbl_Transaction_date" runat="server" Text='<%#Bind("Transaction_date") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_tatal_amount" runat="server" Text='<%#Bind("tatal_amount") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_discount" runat="server" Text='<%#Bind("discount") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                    <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_disstatus" runat="server" ></asp:Label>

                                                </td>
                                                <td>
                                                    <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                        <div class="btn-group dropdown">
                                                            <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                                <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                            </button>
                                                            <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">


                                                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>

                                                                <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><i class="dropdown-icon lnr lnr-pencil"></i><span>Edit</span></asp:LinkButton>

                                                                 <asp:LinkButton ID="lnk_verify" runat="server" CssClass="dropdown-item" OnClick="lnk_verify_Click" OnClientClick='return confirm("Are you sure want to verify ?")'><i class="dropdown-icon lnr lnr-rocket"></i><span>Verify</span></asp:LinkButton>

                                                                <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                 <asp:Label ID="lbl_Istatus" runat="server" Font-Names="Arial" Text='<%#Bind("Istatus") %>' Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>



                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                       
                                        <td>Total </td>
                                        <td>
                                            <asp:Label ID="lbl_total_amount" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                        </td>
                                         <td></td>
                                    </tr>
                                </tfoot>
                            </table>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
