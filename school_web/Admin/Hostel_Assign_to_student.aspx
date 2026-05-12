<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Hostel_Assign_to_student.aspx.cs" Inherits="school_web.Admin.Hostel_Assign_to_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Hostel Assign
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var session = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Hostel_Assign_to_student.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "','sessioN': '" + session + "'}",
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
            var session = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Hostel_Assign_to_student.aspx/GetRooPathAdmNo',
                        data: "{ 'PathRooT': '" + request.term + "','sessioN': '" + session + "'}",
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

        .form-select {
            font-size: 14px;
        }
    </style>
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
                <div class="breadcrumb-title pe-3">Hostel</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Assign Hostel To Student</li>
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
                                    <div class="col-xl-3">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
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

                                    <div class="col-xl-5">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Roll No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"    ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Class</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                     
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Roll No.</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:TextBox ID="txtrollnumber" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5">
                                                                </div>
                                                                <div class="col-xl-7">
                                                                    <asp:Button ID="btnfind" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btnfind_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-4">
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
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Section/Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_section_roll_no" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_phone" runat="server" class="stdnt-info-fnds"></asp:Label>
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
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr" style="margin: 8px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h">Assign Hostel To Student</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                             
                                                            <div class="col-md-2">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label">From Month<sup>*</sup></label>
                                                                    <asp:DropDownList ID="ddl_month" runat="server" class="form-select"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label">Select Hostel<sup>*</sup></label>
                                                                    <asp:DropDownList ID="ddl_hostel" runat="server" class="form-select"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label">Room Category<sup>*</sup></label>
                                                                    <asp:DropDownList ID="ddl_room_cat" runat="server" class="form-select" OnSelectedIndexChanged="ddl_room_cat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label">Room<sup>*</sup></label>
                                                                    <asp:DropDownList ID="ddl_room" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_room_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label"></label>
                                                                    Total no of Bed  
                                                                <asp:Label ID="lbl_no_of_bed" runat="server" Text="" class="form-control" Style="height: 38px;"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label"></label>
                                                                    Total no of Empty Bed  
                                                                <asp:Label ID="lbl_ttl_empty_bed" runat="server" Text="" class="form-control" Style="height: 38px;"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label">Select Bed<sup>*</sup></label>
                                                                    <asp:DropDownList ID="ddl_bed" runat="server" class="form-select"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label"></label>
                                                                    Bed Charge/month : 
                                                                  <asp:Label ID="lbl_bed_charge" runat="server" Text="" class="form-control" Style="height: 38px;"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" runat="server" id="additionalService" visible="false">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Additional Services</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="grd-wpr">
                                                                    <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th></th>
                                                                                <th>Service Name</th>
                                                                                <th>Service Charge/month</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:CheckBox ID="rowChkBox" runat="server" />
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_service_name" runat="server" Text='<%#Bind("Service_name")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_service_amount" runat="server"   Text='<%# Getamount_comma_seperated(Eval("Service_amount").ToString())%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_service_id" Visible="false" runat="server" Text='<%#Bind("Service_id")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_is_assined" Visible="false" runat="server" Text='<%#Bind("Is_added")%>'></asp:Label>
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
                                        </div>
                                    </div>

                                    <div class="row">
                                        <%--<div class="col-8"></div>--%>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:Button ID="btn_calculate" runat="server" Text="Calculate Amount" CssClass="btn btn-primary" OnClick="btn_calculate_Click" Style="margin: 5px 10px 5px 0px; background-color: #a1a6ad; border-color: #86888b;" />

                                                    Total Amount/Month : 
                                                    <asp:Label ID="lbl_final_amt" runat="server" Text="00"></asp:Label>

                                                    <asp:Button ID="btn_save_student_dt" runat="server" Text="Final Submit" class="btn btn-dark" OnClick="btn_save_student_dt_Click" Style="margin: 5px 0px 5px 10px; background-color: #0d6efd; border-color: #0d6efd; padding: 6px 15px 6px 15px; font-size: 15px;" />
                                                </div>
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
    <asp:HiddenField ID="hd_id" runat="server" />
    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 750px;">
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
                                    <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                </td>
                                <td>
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
