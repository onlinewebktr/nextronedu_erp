<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="update-hostel-roll.aspx.cs" Inherits="school_web.Admin.update_hostel_roll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update Hostel Roll No.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <script src="../Grid_calender/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Grid_calender/Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Grid_calender/Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Calender").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                minDate: 0,
                startDate: new Date(),
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>
    <style type="text/css">
        table.dataTable td, table.dataTable th {
            position: relative;
        }

        .fxtblWpr {
            width: 100%;
            height: 450px;
            background-color: #ddd;
            overflow: auto;
            border: 1px solid #ccc;
        }

        table {
            width: 100% !important;
            overflow-x: scroll;
        }

        td, th {
            border: 1px solid #ccc;
        }

        th {
            font-weight: 600;
            text-align: left;
            background-color: #f1f4f7;
        }

        .fixed-td {
            position: sticky !important;
            z-index: 2;
            left: 0;
            color: #000;
            background-color: #efff00 !important;
        }

        .fixed-hd {
            position: sticky !important;
            top: 0;
            z-index: 1 !important;
        }

        .left-top-td {
            z-index: 3 !important;
        }

        .noline-break {
            white-space: nowrap;
            word-break: keep-all;
        }
    </style>
    <script type="text/javascript"> 
        $(function () {
            var sessionid = $("#<%=ddlsession.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'update-hostel-roll.aspx/GetRooPathAdmNo',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
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


        $(function () {
            var sessionid = $("#<%=ddlsession.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'bill-delete.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
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
                <div class="breadcrumb-title pe-3">Student Update</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a></li>
                            <li class="breadcrumb-item active" aria-current="page">Update Hostel Roll No.</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                            <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                            <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Section</label>
                                                            <asp:DropDownList ID="ddl_secion" runat="server" class="form-control find-dv-txtbx">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Button ID="btn_fnd" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_Click" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-8">
                                                            <label for="validationCustom01" class="find-dv-lbl">Enter Admission No.</label>
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" Style="margin-top: 20px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-8">
                                                            <label for="validationCustom01" class="find-dv-lbl">Enter Student Name</label>
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:Button ID="btn_find_with_std_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_with_std_name_Click" Style="margin-top: 20px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr" style="overflow: auto;">
                                            <div class="fxtblWpr">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th class="fixed-td fixed-hd left-top-td">#</th>
                                                            <th class="fixed-td fixed-hd left-top-td">Student</th>
                                                            <th class="scrollable-td fixed-hd">Class</th>
                                                            <th class="scrollable-td fixed-hd">Section</th>
                                                            <th class="scrollable-td fixed-hd">Roll No.</th>
                                                            <th class="scrollable-td fixed-hd">Hostel Roll No.</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fixed-td">
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td class="fixed-td noline-break">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        (<asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>) 
                                                                        <asp:Label ID="lbl_Section" Visible="false" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_roll_no" runat="server" Style="width: 70px;" onkeypress="return isNumberKey(event)" Text='<%#Bind("Hostel_roll_no")%>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-primary find-dv-btn" OnClick="btn_save_Click" Style="padding: 10px 14px 10px 14px; margin-bottom: 10px;" Visible="false" />
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
</asp:Content>
