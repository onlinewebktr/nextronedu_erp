<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Live_Meeting_info.aspx.cs" Inherits="school_web.LMS_VC_Admin.Live_Meeting_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Live Class Schedule
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
    </style>
    <style>
        .multiselect-container {
            top: 0px !important;
            width: 300px;
            left: -18px!important;
        }

        .input-group > .form-control, .input-group > .form-control-plaintext, .input-group > .custom-select, .input-group > .custom-file {
            position: relative;
            flex: 1 1 auto;
            width: 1%;
            margin-bottom: 0;
            z-index: 99999!important;
        }

        .dropdown-menu.show {
            animation: fade-in2 0.2s cubic-bezier(0.39, 0.575, 0.565, 1) both;
            top: 10px!important;
            height: 408px!important;
            overflow-x: hidden!important;
            overflow-y: scroll!important;
        }
        .dt-button-collection {
           margin-top: -59.4px!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Live Class Schedule</asp:Literal>

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
                        <div class="col-md-12" id="tblPrintIQ" style="margin: 0px 0px 0px 0px;">
                            <asp:Panel ID="pnl_view" runat="server" Visible="true">
                                <h2 class="blue_bg" style="margin: 0px;">
                                    <asp:Label ID="lbl_month_year" runat="server"></asp:Label></h2>

                                <div class="row noPrint">
                                    <div class="col-md-2">
                                        <b>Search By Teacher : </b>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <asp:DataList ID="datl_teacherlist" runat="server" RepeatColumns="14" Style="border-collapse: collapse; font-size: 9px; display: none" border="1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_UserID" runat="server" Text='<%#Bind("UserID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="chk_subjectname" runat="server" Text='<%#Bind("Name")%>' CssClass="lbl_color" />
                                                </ItemTemplate>
                                            </asp:DataList>
                                            <asp:ListBox ID="lst_Teacher" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <b>Date</b>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
                                            <script src="../Autocomplete/jquery-ui.js"></script>
                                            <div class="input-group input-group-icon">
                                                <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
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
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <div class="input-group input-group-icon">
                                                <br />
                                                <asp:Button ID="btn_search" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_search_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="display: none">
                                        <br />
                                        <asp:LinkButton ID="lnk_excel_download" runat="server" class="btn-print noPrint" Style="float: right; font-size: 30px; color: #a94442;"><i class="fa fa-file-excel-o"> </i></asp:LinkButton>
                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel('tblPrintIQ')" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right; margin: 0px 10px; font-size: 30px; color: #a94442;"><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>

                                    </div>
                                </div>
                                <hr />
                                <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Sl No.</th>
                                            <th>Teacher</th>
                                            <th>Teacher ID</th>
                                            <th>Class</th>
                                            <th>Section</th>
                                            <th>Subject</th>
                                            <th>Date</th>
                                            <th>Start Time  </th>
                                            <th>End Time
                                            </th>
                                            <th>Duration</th>
                                            <th>Created Time</th>

                                            <th>Zoom User Id</th>
                                            <th>Status</th>

                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RpDetailsStudent" runat="server" OnItemDataBound="RpDetailsStudent_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_teacherName1" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_teacherid" runat="server" Text='<%#Bind("UserID") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_classname" runat="server" Text='<%#Bind("classname") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTopic" runat="server" Text='<%#Bind("CourseName") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Meeting_start_at" runat="server" Text='<%#Bind("Meeting_start_at","{0:hh:mm:ss tt}") %>'></asp:Label>


                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_end_time" runat="server" Text='<%#Bind("End_Time","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Duration" runat="server" Text='<%#Bind("Duration") %>'></asp:Label>
                                                        <asp:Label ID="lvl_vcid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_createdtime" runat="server" Text='<%#Bind("CreatedOn","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                    </td>







                                                    <td>

                                                        <asp:Label ID="lbl_Zoom_id" runat="server" Text='<%#Bind("Zoom_id") %>'></asp:Label>

                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status") %>' Font-Bold="true"></asp:Label>

                                                    </td>

                                                    <td>
                                                        <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                            <div class="btn-group dropdown">
                                                                <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                                    <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                                </button>
                                                                <div tabindex="-1" role="menu" style="height: 46px !important;overflow: hidden!important;" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                                    <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click" Style="display: none"><span>Edit</span></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><span>Delete</span></asp:LinkButton>

                                                                    <asp:LinkButton ID="lnkedit_zoom" Text="Update Zoom ID" CssClass="dropdown-item" runat="server" Style="display: none" OnClick="lnkedit_zoom_Click" Visible="false" />
                                                                    <asp:Label ID="lbl_zmid" runat="server" Text='<%#Bind("Zoom_Meeting_id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_zmpwd" runat="server" Text='<%#Bind("Zoom_Metting_Password") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Zoom_Api_Sl_No" runat="server" Text='<%#Bind("Zoom_Api_Sl_No") %>' Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <%--   <asp:Button ID="btnedit_zoom" BackColor="Red" Text='<%#Bind("Zoom_Api_Sl_No") %>' CssClass="btn btn-success btn-sm" runat="server" OnClick="btnedit_zoom_Click" />

                                                         

                                                        <asp:LinkButton ID="lnk_edit" runat="server" OnClick="lnk_edit_Click" CssClass="btn btn-success btn-sm" Style="background-color: Red;"><i class="fa fa-edit"></i></asp:LinkButton>--%>
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
            </div>
        </div>
    </div>
    <link href="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lst_Teacher]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative; float: left;">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px!important;">Update  ZSL</h3>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body" style="position: relative; padding: 0px 8px 8px 8px; float: left;">
                    <div role="form">
                        <div class="position-relative form-group">
                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                <table border="1" style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td style="padding: 3px">Teacher Name</td>
                                        <td style="padding: 3px">
                                            <asp:Label ID="lbl_teachername" runat="server" Text=""></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 3px">User ID</td>
                                        <td style="padding: 3px">
                                            <asp:Label ID="lbluserid" runat="server" Text=""></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 3px">Old Zoom Sl. No.</td>
                                        <td style="padding: 3px">
                                            <asp:Label ID="lbl_old_zoomid" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px">New Zoom Sl. No.</td>
                                        <td style="padding: 3px">

                                            <asp:DropDownList ID="ddl_zoomsl" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px; text-align: center" colspan="2">
                                            <asp:Button ID="btn_update_zoomid" runat="server" CssClass="btn btn-success btn-sm" Text="Update" OnClick="btn_update_zoomid_Click" />
                                            <asp:HiddenField ID="hd_teacheruserid" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>

    <div class="modal fade" id="div_teacher_edit_zmid" role="dialog">
        <div class="modal-dialog" style="width: 450px; background: #fff;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative; float: left;">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px!important;">Update user id and pwd</h3>
                    <asp:Button ID="Button1" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body" style="position: relative; padding: 0px 8px 8px 8px; float: left;">
                    <div role="form">
                        <div class="form-group">
                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                <table border="1" style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td style="padding: 3px">Teacher Name</td>
                                        <td style="padding: 3px">
                                            <asp:Label ID="lbl_teachername_edit_m" runat="server" Text=""></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 3px">User ID</td>
                                        <td style="padding: 3px">
                                            <asp:Label ID="lbl_userid_teacher_edit_m" runat="server" Text=""></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 3px">ZMId<sup>*</sup></td>
                                        <td style="padding: 3px">

                                            <asp:TextBox ID="txt_zmid_edit" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px">ZMPWD <sup>*</sup></td>
                                        <td style="padding: 3px">
                                            <asp:TextBox ID="txt_zmpwd_edit" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px">Status <sup>*</sup></td>
                                        <td style="padding: 3px">

                                            <asp:DropDownList ID="ddl_status" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px; text-align: center" colspan="2">
                                            <asp:Button ID="btn_change" runat="server" CssClass="btn btn-success btn-sm" Text="Update" OnClick="btn_change_Click" />
                                            <asp:HiddenField ID="hd_teacherid_edit" runat="server" />
                                            <asp:HiddenField ID="hd_tacher_cell_id_edit" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeup1"></div>


    <script type="text/javascript">
        function openModal1() {
            $("#div_teacher_edit_zmid").show();
            $('#div_teacher_edit_zmid').addClass('show');
            $('#fadeup1').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#div_teacher_edit_zmid").hide();
            $('#div_teacher_edit_zmid').removeClass('show');
            $('#fadeup1').removeClass('modal-backdrop fade show');
        }
    </script>
</asp:Content>
