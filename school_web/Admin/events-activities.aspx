<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="events-activities.aspx.cs" Inherits="school_web.Admin.events_activities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Events
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            border-style: none;
            width: 17px;
            height: 17px;
            position: relative;
            top: 2.6px;
            left: 0px;
            margin: 0px 10px 0px 8px;
            z-index: 9;
            background-color: green;
        }

            input[type=checkbox], input[type=radio]:checked {
                background-color: #17a00e;
            }

        input[type="checkbox"]#der1_chk_per:checked + span {
            border-color: red;
            background-color: red;
        }

        tbody, td, tfoot, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            padding: 2px 0px 3px 6px;
        }

        .tbe1 {
            background: var(--secondary-bg-color) none repeat scroll 0 0;
            border: 1px solid #e1e1e1;
            padding: 15px 20px;
            margin-bottom: 0;
            width: 100%;
            min-width: 150px;
        }

        .form-label {
            margin-bottom: 4px !important;
            margin-top: 9px !important;
        }

        div.dd_chk_select {
            border-color: #CCCCCC;
            border-style: solid;
            border-width: 1px;
            height: 30px !important;
            padding: 0px 0px 0px 0px;
            text-align: left;
            vertical-align: middle;
            font-size: 14px;
            text-decoration: none;
            overflow: visible;
            color: Black;
            background-color: white;
            background-image: url(WebResource.axd?d=8mNM2oY0_MTbltHc0o1ItZnaj3eMWaRkOCe1ndy_hR9R3AdIBzoSPv_Ywene2l7sUuedivBnkluzSF6dczdfxuEsclUrEQ8FJ8fADG8qa5ySz0dBH1K8nfZq1zjp5ClriQLuWH6nrYavQri7uKTGSCpI2KI1&t=637959154290547877);
            background-position: right center;
            background-repeat: no-repeat;
            width: 99%;
        }

        .chkstle {
            display: block;
            position: relative;
            padding: 2px 5px 2px 0px;
            margin-bottom: 5px;
            cursor: pointer;
            font-size: 14px;
            float: left;
            border: 1px solid #ddd;
            background: #f5f5f5;
        }
    </style>

    <script>
        $(function () {
            $("#<%=txt_startdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="breadcrumb-title pe-3">School</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Events</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-12" style="margin: 0px;" id="classDV" runat="server">
                                        <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                        <span class="chkbx-all">
                                            <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" /></span>
                                        <br />
                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                                <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-md-12" style="margin: 0px;" id="edtClassDV" runat="server" visible="false">
                                        <label for="validationCustom01" class="form-label">Class</label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-4" style="margin: 0px;">
                                        <label for="validationCustom01" id="lblstartdate" runat="server" class="form-label">Date<sup>*</sup></label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_startdate" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="margin: 0px;">
                                        <label for="validationCustom01" class="form-label">Is Festival</label>
                                        <asp:DropDownList ID="ddl_is_festival" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4" id="saturdaySetting1" style="margin: 0px;">
                                        <label for="validationCustom01" class="form-label">Festival Name</label>
                                        <asp:TextBox ID="txt_festival_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12" style="margin: 0px;">
                                        <label for="validationCustom01" class="form-label">Activity Details</label>
                                        <asp:TextBox ID="txt_details" runat="server" class="form-control find-dv-txtbx" TextMode="MultiLine" Style="height: 50px"></asp:TextBox>
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Events Details</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="find-dv">
                                <div class="row  g-3 needs-validation">
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddl_session_search" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:DropDownList ID="ddl_class_search" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find" />

                                        <%--<div id="excel" runat="server" visible="false">
                                                <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                            </div>

                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>--%>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-sm-12">

                                                <asp:Panel ID="Panel1" runat="server">
                                                    <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Class</th>
                                                                <th>Date</th>
                                                                <th>Day</th>
                                                                <th>Festival</th>
                                                                <th>Activity</th>
                                                                <th>Action</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="Repeater1" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_class_name" runat="server" Text='<%#Bind("Class_name")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Event_date")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("event_day")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_festival" runat="server" Text='<%#Bind("Festival")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_event_details" runat="server" Text='<%#Bind("Event_details")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_is_festeval" runat="server" Text='<%#Bind("Is_festeval")%>' Visible="false"></asp:Label>
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
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //============================
        $(document).ready(function () {
            is_festivals();
            $("#<%=ddl_is_festival.ClientID%>").on('change', function () {
                is_festivals();
            })
        });
        function is_festivals() {
            if ($('#<%= ddl_is_festival.ClientID %> option:selected').val() == "Yes") {
                $("#saturdaySetting1").show();
            }
            else {
                $("#saturdaySetting1").hide();
            }
        }
    </script>
</asp:Content>
