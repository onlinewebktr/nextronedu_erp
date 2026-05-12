<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Mapping_Transportation_with_Student_N.aspx.cs" Inherits="school_web.Admin.Mapping_Transportation_with_Student_N" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Transport Mapping
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            font-weight: 500;
        }

        .fnd-box-row-wpr-h {
            margin: 0px;
            padding: 5px 5px 4px 5px;
            width: 100%;
            float: left;
            font-size: 16px;
            border-bottom: 1px solid #ddd;
        }
    </style>
    <style>
        .fnd-box-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            border: 1px solid #ddd;
            border-radius: 2px;
        }

        .fnd-box-wpr-inr {
            margin: 0px;
            padding: 5px 5px;
            width: 100%;
            float: left;
        }

        .fnd-box-row-wpr-h {
            margin: 0px;
            padding: 5px 5px 4px 5px;
            width: 100%;
            float: left;
            font-size: 16px;
            border-bottom: 1px solid #ddd;
        }

        .fnd-box-row-wpr {
            margin: 3px 0px 3px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .form-label-fnds {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .padd-rght-5 {
            padding-right: 5px;
        }

        .padd-lft-5 {
            padding-left: 5px;
        }

        .padd-lft0 {
            padding-left: 0px;
        }

        .pdd-both0 {
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        .form-fnd-btns {
            margin: 0px;
            padding: 3px 10px;
        }

        .stdnt-info-fnds {
            margin: 3px 0px 3px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 13px;
        }

        .chkbx-all {
            margin: 0px 0px 0px 10px;
            padding: 0px 0px 0px 0px;
            font-weight: 500;
        }

        .hdrmodify {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .hdrmodify table tr th {
                background: #6ea9ff;
            }

        .pntsHeadS {
            margin: 0px 0px 2px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 17px;
            font-weight: 500;
            text-align: center;
            display: none;
        }

        .pntsDatesS {
            margin: 0px 0px 4px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            display: none;
        }

        .frms-row-wpr {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_student.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Mapping_Transportation-with-Student.aspx/GetRooPath',
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
            var sessionid = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Mapping_Transportation-with-Student.aspx/GetRooPathAdmNo',
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
    <!--start page wrapper -->
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
                <div class="breadcrumb-title pe-3">Transportation</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Transport Mapping</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row">
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-3 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-3 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Admission No.</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_session_student" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Student Name</label>
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:Button ID="btn_find_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_name_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Student Basic Information</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" Text="Md Wahab" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Section : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="txtsection" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_yesr_smester" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transportation : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbltransporttion" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_phone" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-2">
                                            <div class="frms-row-wpr" style="margin: 10px 0px 10px 0px;">
                                                <label for="validationCustom01" class="form-label">From Month<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_from_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-xl-4">
                                            <div class="frms-row-wpr" style="margin: 10px 0px 10px 0px;">
                                                <label for="validationCustom01" class="form-label">Vehicle Name<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_bus_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_bus_name_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-xl-3">
                                            <div class="frms-row-wpr" style="margin: 10px 0px 10px 0px;">
                                                <label for="validationCustom01" class="form-label">Transportation Route<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_path_root" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_path_root_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="frms-row-wpr" style="margin: 10px 0px 10px 0px;">
                                                <label for="validationCustom01" class="form-label">Boarding Point<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_boarding_point" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_boarding_point_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:DropDownList ID="ddl_seatname" runat="server" Visible="false" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-xl-5">
                                            <div class="frms-row-wpr" style="margin: 10px 0px 10px 0px;">
                                                <label for="validationCustom01" class="form-label">Transport Fee Details<sup>*</sup></label>
                                                <table class="table-bordered table " style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; font-size: 13px;">
                                                    <tr>
                                                        <th style="padding: 3px;">Boarding Point</th>
                                                        <th style="padding: 3px;">KM Coverd</th>
                                                        <th style="padding: 3px;">Transport Fee</th>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 3px;">
                                                            <asp:Label ID="lbl_boardingpoint" runat="server" Font-Bold="true">0</asp:Label></td>
                                                        <td style="padding: 3px;">
                                                            <asp:Label ID="lbl_kmcoverdby" runat="server" Font-Bold="true">0</asp:Label></td>
                                                        <td style="padding: 3px;">
                                                            <asp:Label ID="lbl_trasportfee" runat="server" Font-Bold="true">0</asp:Label></td>
                                                    </tr>

                                                </table>

                                                <table class="table-bordered table" id="selected_seletd" runat="server" visible="false" style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; font-size: 13px;">


                                                    <tr>
                                                        <th style="padding: 3px;" colspan="3">Seat Details</th>

                                                    </tr>

                                                    <tr>
                                                        <td style="padding: 3px;" colspan="3">
                                                            <span style="font-weight: bold">Your Selected Seat</span>
                                                            <br />

                                                            <asp:ImageButton ID="imgbuton_1" runat="server" Enabled="false" />
                                                            <br />
                                                            <asp:Label ID="lbl_seletedseat" runat="server" Font-Bold="true"></asp:Label>

                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 3px;" colspan="3">
                                                            <span style="font-weight: bold">Your beside seat</span>
                                                            <br />
                                                            <asp:ImageButton ID="imgbuton_2" runat="server" Enabled="false" />
                                                            <br />
                                                            <asp:Label ID="lbl_byside_seat" runat="server" Font-Bold="true"></asp:Label>


                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 3px; text-align: left" colspan="3">
                                                            <asp:Label ID="lbl_student_info" runat="server" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="3" style="padding: 0px; text-align: center;">
                                                            <asp:Button ID="btn_save_bording" runat="server" Text="Submit" class="btn btn-dark" OnClick="btn_save_bording_Click" OnClientClick="return confirm('Are you sure you want allocate transport?');" Style="margin: 5px 0px 5px 0px; background-color: #0d6efd; border-color: #0d6efd; padding: 5px 10px 5px 10px; font-size: 14px;" />
                                                        </td>
                                                    </tr>

                                                </table>


                                            </div>
                                        </div>
                                        <div class="col-xl-5">
                                            <div class="frms-row-wpr" style="margin: 10px 0px 10px 0px;" id="seatview" runat="server" visible="false">
                                                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                                    <label for="validationCustom01" class="form-label">Choose Seat</label>
                                                </div>

                                                <table class="table-bordered table " style="margin: 0px; padding: 0px; float: left; height: auto; width: 58%; font-size: 13px;">
                                                    <tr>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">Boy</td>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">Girl</td>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">Staf</td>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">Assigned</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">
                                                            <img src="../images/bus_icon/boy.png" title="Girl" /></td>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">
                                                            <img src="../images/bus_icon/girle.png" title="Girl" /></td>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">
                                                            <img src="../images/bus_icon/staff.png" title="Staf" /></td>
                                                        <td style="padding: 5px; text-align: center; font-weight: bold;">
                                                            <img src="../images/bus_icon/Booked.png" title="Staf" /></td>
                                                    </tr>
                                                </table>



                                                <table class="table-bordered table" style="margin: 0px; padding: 0px; float: left; height: auto; width: 58%; font-size: 13px;">
                                                    <tr>
                                                        <td style="padding: 5px; width: 50%; text-align: center; font-weight: bold;">Left Side</td>
                                                        <td style="padding: 5px; width: 50%; text-align: center; font-weight: bold;">Right Side</td>
                                                    </tr>

                                                    <tr>
                                                        <td style="padding: 5px; width: 50%; vertical-align: top">
                                                            <asp:DataList ID="dlbus_seatleft" Style="width: 100%;" runat="server" CellSpacing="3" RepeatColumns="2" OnItemDataBound="dlbus_seatleft_ItemDataBound" GridLines="Horizontal" RepeatDirection="Horizontal">
                                                                <ItemTemplate>

                                                                    <div class="row">
                                                                        <div class="col-md-12" style="text-align: center;">

                                                                            <asp:ImageButton ID="imgbuton_1_left" runat="server" OnClick="imgbuton_1_left_Click" />
                                                                            <br />
                                                                            <asp:Label ID="lbl_Sheet_No" runat="server" Font-Bold="true" Text='<%#Bind("Sheet_No") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Transportation_Id" Visible="false" runat="server" Text='<%#Bind("Transportation_Id") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_TransportationPath_id" Visible="false" runat="server" Text='<%#Bind("TransportationPath_id") %>'></asp:Label>

                                                                            <asp:Label ID="lbl_Sheet_Id" runat="server" Visible="false" Text='<%#Bind("Sheet_Id") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Seat_Type" runat="server" Visible="false" Text='<%#Bind("Seat_Type") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Row" runat="server" Visible="false" Text='<%#Bind("Row") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Sheet_Status" runat="server" Visible="false" Text='<%#Bind("Sheet_Status") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Sheet_position" runat="server" Visible="false" Text='<%#Bind("Sheet_position") %>'></asp:Label>

                                                                        </div>

                                                                    </div>

                                                                </ItemTemplate>
                                                            </asp:DataList>

                                                        </td>
                                                        <td style="padding: 5px; width: 50%; vertical-align: top">
                                                            <asp:DataList ID="dl_bus_seat_right" Style="width: 100%;" runat="server" CellSpacing="3" RepeatColumns="2" OnItemDataBound="dl_bus_seat_right_ItemDataBound" GridLines="Horizontal" RepeatDirection="Horizontal">
                                                                <ItemTemplate>

                                                                    <div class="row">
                                                                        <div class="col-md-12" style="text-align: center;">

                                                                            <asp:ImageButton ID="imgbuton_1_right" runat="server" OnClick="imgbuton_1_right_Click" />
                                                                            <br />
                                                                            <asp:Label ID="lbl_Sheet_No" runat="server" Font-Bold="true" Text='<%#Bind("Sheet_No") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Transportation_Id" Visible="false" runat="server" Text='<%#Bind("Transportation_Id") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_TransportationPath_id" Visible="false" runat="server" Text='<%#Bind("TransportationPath_id") %>'></asp:Label>

                                                                            <asp:Label ID="lbl_Sheet_Id" runat="server" Visible="false" Text='<%#Bind("Sheet_Id") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Seat_Type" runat="server" Visible="false" Text='<%#Bind("Seat_Type") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Row" runat="server" Visible="false" Text='<%#Bind("Row") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Sheet_Status" runat="server" Visible="false" Text='<%#Bind("Sheet_Status") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Sheet_position" runat="server" Visible="false" Text='<%#Bind("Sheet_position") %>'></asp:Label>
                                                                        </div>

                                                                    </div>

                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px; width: 100%; vertical-align: top" colspan="2">

                                                            <asp:DataList ID="dl_bus_seat_back" runat="server" Style="width: 100%;" CellSpacing="3" OnItemDataBound="dl_bus_seat_back_ItemDataBound" GridLines="Horizontal" RepeatDirection="Horizontal">
                                                                <ItemTemplate>

                                                                    <div class="row">
                                                                        <div class="col-md-12" style="text-align: center;">

                                                                            <asp:ImageButton ID="imgbuton_1_back" runat="server" OnClick="imgbuton_1_back_Click" />
                                                                            <br />
                                                                            <asp:Label ID="lbl_Sheet_No" runat="server" Font-Bold="true" Text='<%#Bind("Sheet_No") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Transportation_Id" Visible="false" runat="server" Text='<%#Bind("Transportation_Id") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_TransportationPath_id" Visible="false" runat="server" Text='<%#Bind("TransportationPath_id") %>'></asp:Label>

                                                                            <asp:Label ID="lbl_Sheet_Id" runat="server" Visible="false" Text='<%#Bind("Sheet_Id") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Seat_Type" runat="server" Visible="false" Text='<%#Bind("Seat_Type") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Row" runat="server" Visible="false" Text='<%#Bind("Row") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Sheet_Status" runat="server" Visible="false" Text='<%#Bind("Sheet_Status") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Sheet_position" runat="server" Visible="false" Text='<%#Bind("Sheet_position") %>'></asp:Label>
                                                                        </div>

                                                                    </div>

                                                                </ItemTemplate>
                                                            </asp:DataList>

                                                        </td>
                                                    </tr>
                                                </table>





                                            </div>
                                        </div>


                                    </div>



                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 994px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-6">
                        <h2 class="popup-dt-h">Student Details</h2>
                    </div>
                    <div class="col-md-6">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>

                <table style="width: 100%;" id="Table1" class="table table-hover table-bordered ">

                    <tr>
                        <th>Student Name</th>
                        <th>Admission No</th>
                        <th>Session</th>
                        <th>Class</th>

                        <th>Section</th>
                        <th>Roll</th>
                        <th>Father's Name</th>
                        <th>Action</th>
                    </tr>


                    <asp:Repeater ID="rp_std" runat="server">
                        <ItemTemplate>
                            <tr id="row" runat="server">
                                <td>
                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session") %>'></asp:Label>
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
                                    <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lbl_Academic_Sem_or_Year_id" runat="server" Text='<%#Bind("Academic_Sem_or_Year_id") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnk_select" runat="server" OnClick="lnk_select_Click">Select</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>


            </div>

        </div>
    </div>
    <style>
        .conf-btn-ul li a {
            margin: 0px 5px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff0000;
            color: #fff;
            width: 50px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }

        table tr th {
            padding: 10px 5px !important;
        }

        table tr td {
            padding: 10px 5px !important;
        }
    </style>


</asp:Content>
