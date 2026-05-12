<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="admission-new.aspx.cs" Inherits="school_web.Admin.admission_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    New Admission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/js/my_old.js"></script>
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <style>
        .form-ttleS {
            margin: 0px 0px 0px 0px;
            padding: 7px 10px 6px 10px;
            width: 100%;
            float: left;
            font-size: 18px;
            color: #ffffff;
            border-bottom: 1px solid #ddd;
            background: #0e66b1;
            border-radius: 4px 4px 0px 0px;
        }

        .form-label {
            margin-bottom: 2px;
        }

        th {
            font-weight: 500;
        }

        .form-control:disabled, .form-control[readonly] {
            background-color: #ffffff;
        }

        .txtbx-style {
            box-shadow: inset 10px 9px 14px #8888881a;
            -moz-appearance: none;
            appearance: none;
            border: 1px solid #aaa;
            transition: all 0.3s;
            height: 34px;
        }

        .card {
            margin-bottom: .59rem !important;
        }

        .online_frm-hss {
            height: auto;
            width: 100%;
            margin: 0px 0px 5px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 21px;
            line-height: 32px;
            font-weight: 600;
            color: #474d57;
            text-decoration: underline;
            font-family: 'IBM Plex Sans', sans-serif;
        }

        .online_frm-p {
            height: auto;
            width: 100%;
            margin: 0px 0px 15px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 17px;
            line-height: 30px;
            color: #000;
            font-weight: 500;
        }

        .online_frm-grp-h {
            height: auto;
            width: 100%;
            margin: 0px 0px 5px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            color: #000;
            font-weight: 500;
            font-size: 13px;
            line-height: 20px;
            text-align: left;
            text-transform: uppercase;
        }

        .g-3, .gy-3 {
            --bs-gutter-y: .6rem;
        }

        .wdth20 {
            width: 20%;
        }

        .add-same-as {
            margin: 10px 0px 0px 0px;
            padding: 11px 0px 10px 10px;
            width: 100%;
            float: left;
            border-top: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

            .add-same-as input {
                margin: 0px;
                padding: 0px;
            }

            .add-same-as label {
                margin: 0px;
                padding: 0px 0px 0px 5px;
                font-size: 16px;
                font-weight: 600;
            }
    </style>


    <style>
        /*==============================*/
        .modal.rights .modal-dialog {
            position: fixed;
            margin: auto;
            width: 65px;
            max-width: 65px;
            -webkit-transform: translate3d(0,0,0);
            -ms-transform: translate3d(0,0,0);
            -o-transform: translate3d(0,0,0);
            transform: translate3d(0,0,0);
        }

        .modal.rights .modal-content {
            height: 100%;
            overflow-y: auto;
        }

        .modal.rights .modal-body {
            padding: 15px 15px 80px;
        }

        .modal.rights.fade .modal-dialog {
            right: -250px;
            -webkit-transition: opacity .3s linear,left .3s ease-out;
            -moz-transition: opacity .3s linear,left .3s ease-out;
            -o-transition: opacity .3s linear,left .3s ease-out;
            transition: opacity .3s linear,left .3s ease-out;
        }

        .modal.rights.fade.in .modal-dialog {
            right: 0;
        }

        .contnt-cntr {
            width: 100%;
            float: left;
            align-items: center;
            display: flex;
            height: 100%;
            text-align: center;
        }

        .roll-dv {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .roll-dv p {
                padding: 5px 0px 5px 0px;
                margin: 1px 0px 1px 0px;
                width: 50px;
                float: left;
                text-align: center;
                border: 1px solid #29e1ff;
                background: #b1fff8;
                border-radius: 2px;
                font-weight: 700;
                color: #000;
            }

        .mandatory {
            background-color: #ffffc7;
            border-left: 5px solid #cbcb00;
            box-shadow: inset 10px 9px 14px #a39e0d1a;
            -moz-appearance: none;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#<%=txt_adress.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'admission-new.aspx/GetAddress',
                        data: "{ 'PathRooT': '" + request.term + "'}",
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
        $(function () {
            $("#<%=txt_present_po.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'admission-new.aspx/PoliceStation',
                        data: "{ 'PathRooT': '" + request.term + "'}",
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
        $(function () {
            $("#<%=txt_temp_ps.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'admission-new.aspx/PoliceStation',
                        data: "{ 'PathRooT': '" + request.term + "'}",
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
        $(function () {
            $("#<%=txt_present_district.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'admission-new.aspx/getdistrict',
                        data: "{ 'PathRooT': '" + request.term + "'}",
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
        $(function () {
            $("#<%=txt_city.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'admission-new.aspx/getcitY',
                        data: "{ 'PathRooT': '" + request.term + "'}",
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
    <asp:HiddenField ID="hd_temp_reg_id" runat="server" />
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
                <div class="breadcrumb-title pe-3">Admission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Registration</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Student Registration"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Admission Information</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label" style="width: 100%;">Student Type<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_student_type" runat="server" class="form-select txtbx-style mandatory">
                                                <asp:ListItem>New</asp:ListItem>
                                                <asp:ListItem>Old</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" >
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Category<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_category" runat="server" class="form-select txtbx-style" AutoPostBack="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3" >
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Sub Category<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_subcategory" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <%--<div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Id No.</label>
                                            <asp:TextBox ID="txt_form_slno" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Admission date</label>
                                            <div class="clndr-div">
                                                <asp:TextBox ID="txt_admission_date" runat="server" class="form-control txtbx-style mandatory" onkeyup="var v = this.value; if (v.match(/^\d{2}$/) !== null) {this.value = v + '/';} else if (v.match(/^\d{2}\/\d{2}$/) !== null) {this.value = v + '/';}" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select txtbx-style mandatory" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">House</label>
                                            <asp:DropDownList ID="ddl_house" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Admission No./Id<sup>*</sup></label>
                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-select txtbx-style mandatory"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Section<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-select txtbx-style mandatory"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label" style="width: 100%;">
                                                Roll No.  <a href="#" data-toggle="modal" data-target="#myModalRoll" style="float: right; position: relative;"><span class="material-symbols-outlined" style="color: #0005a9; font-size: 22px; position: absolute; right: 0px;">info</span></a>
                                            </label>
                                            <asp:TextBox ID="txt_rollnumber" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>

                                        </div>
                                    </div>








                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" id="HS_roll_updateDV" runat="server" visible="false">
                                        <div class="online_frm-grp">
                                            <h2 class="online_frm-grp-h">Hostel Roll No.</h2>
                                            <asp:TextBox ID="txt_hstl_roll_no_update" onkeypress="return isNumberKey(event)" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="col-md-3" runat="server" id="boarningTypeDV">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Boarding Type<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_day_boarding" runat="server" class="form-select txtbx-style">
                                                <asp:ListItem Value="0">DAY SCHOLAR</asp:ListItem>
                                                <%--<asp:ListItem Value="2">DAY BOARDING WITH LUNCH</asp:ListItem>--%>
                                                <asp:ListItem Value="3">RESIDENTIAL</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>


                                    <div class="col-md-3" runat="server" id="TransportDV">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Bus</label>
                                            <asp:DropDownList ID="ddl_route_path" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="admnoDateDV" runat="server" visible="false">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Admission No./Date<sup>*</sup></label>
                                            <asp:TextBox ID="txt_adm_no_date" runat="server" placeholder="9999/dd/mm/yyyy" class="form-control txtbx-style masked-input"></asp:TextBox>
                                            <script>
                                                $(document).ready(function () {
                                                    // Apply mask to the phone number textbox
                                                    $('#<%= txt_adm_no_date.ClientID %>').mask('9999/99/99/9999');
                                                });
                                            </script>
                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" id="stdTypeDV" visible="false">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Type</label>
                                            <asp:DropDownList ID="ddl_student_type_new" runat="server" class="form-select txtbx-style">
                                                <asp:ListItem>General</asp:ListItem>
                                                <asp:ListItem>Gyandeep</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div id="HostelDV" style="margin: 0px 0px 5px 0px; float: left; width: 100%; border: 1px solid #ddd; padding: 1px 5px 6px; border-radius: 4px; background: #fffdf3;">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 wdth20">
                                                    <div class="online_frm-grp">
                                                        <h2 class="online_frm-grp-h">Hostel<sup>*</sup></h2>
                                                        <asp:DropDownList ID="ddl_hostel" runat="server" CssClass="form-select txtbx-style" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 wdth20">
                                                    <div class="online_frm-grp">
                                                        <h2 class="online_frm-grp-h">Room Type<sup>*</sup></h2>
                                                        <asp:DropDownList ID="ddl_room_cat" runat="server" CssClass="form-select txtbx-style" OnSelectedIndexChanged="ddl_room_cat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 wdth20">
                                                    <div class="online_frm-grp">
                                                        <h2 class="online_frm-grp-h">Room No.<sup>*</sup></h2>
                                                        <asp:DropDownList ID="ddl_room" runat="server" CssClass="form-select txtbx-style" AutoPostBack="true" OnSelectedIndexChanged="ddl_room_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 wdth20">
                                                    <div class="online_frm-grp">
                                                        <h2 class="online_frm-grp-h">Bed No.<sup>*</sup></h2>
                                                        <asp:DropDownList ID="ddl_bed" runat="server" CssClass="form-select txtbx-style"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 wdth20">
                                                    <div class="online_frm-grp">
                                                        <h2 class="online_frm-grp-h">Hostel Roll No.</h2>
                                                        <asp:TextBox ID="txt_hostel_roll_no" onkeypress="return isNumberKey(event)" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="modal rights fade model-fltr" id="myModalRoll" role="dialog" style="z-index: 9999; background: rgb(0 0 0 / 17%);">
                                        <div class="modal-dialog model-dialog-fltr" style="background: rgb(255 255 255 / 62%);">
                                            <div class="modal-content menusidec" style="background: #dddddd00;">
                                                <div class="p-4 border contnt-cntr">
                                                    <div id="rollList" class="roll-dv"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <script type="text/javascript">
                                        $("#<%=ddlclass.ClientID%>,#<%=ddl_section.ClientID%>,#<%=ddlsession.ClientID%>").on('change', function () {
                                            var data = {};
                                            data.class = $('#<%=ddlclass.ClientID%>').val();
                                            data.section = $('#<%=ddl_section.ClientID%>').val();
                                            data.session_id = $('#<%=ddlsession.ClientID%>').val();
                                            gowithData('../../api/get-roll-no-list', 'post', JSON.stringify({ data: data }), function (resp) {
                                                $('#<%=txt_rollnumber.ClientID%>').val(resp.data[0].MissingRollNumber);

                                                $('#rollList').empty();
                                                $.each(resp.data, function (i, d) {
                                                    $('#rollList').append($('<p>').text(d.MissingRollNumber));
                                                })
                                            })
                                        })
                                    </script>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            on_type_selection();
                                            $("#<%=ddl_day_boarding.ClientID%>").on('change', function () {
                                                on_type_selection();
                                            })
                                        });

                                        function on_type_selection() {
                                            if ($('#<%= ddl_day_boarding.ClientID %> option:selected').val() == "3") {
                                                $("#HostelDV").show();
                                            }
                                            else {
                                                $("#HostelDV").hide();
                                            }
                                        }
                                    </script>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Student Information</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Student Name<sup>*</sup></label>
                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Date of Birth</label>
                                            <asp:TextBox ID="txt_dob" runat="server" class="form-control txtbx-style mandatory" placeholder="dd/mm/yyyy" onkeyup="var v = this.value; if (v.match(/^\d{2}$/) !== null) {this.value = v + '/';} else if (v.match(/^\d{2}\/\d{2}$/) !== null) {this.value = v + '/';}" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Gender<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_gender" runat="server" class="form-select txtbx-style mandatory">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>MALE</asp:ListItem>
                                                <asp:ListItem>FEMALE</asp:ListItem>
                                                <asp:ListItem>TRANSGENDER</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Blood Group<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_blood_group" runat="server" CssClass="form-select txtbx-style">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>A+</asp:ListItem>
                                                    <asp:ListItem>A-</asp:ListItem>
                                                    <asp:ListItem>B+</asp:ListItem>
                                                    <asp:ListItem>B-</asp:ListItem>
                                                    <asp:ListItem>O+</asp:ListItem>
                                                    <asp:ListItem>O-</asp:ListItem>
                                                    <asp:ListItem>AB+</asp:ListItem>
                                                    <asp:ListItem>AB-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Nationalty<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_nationality" runat="server" class="form-select txtbx-style mandatory"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Religion<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_religion" runat="server" CssClass="form-select txtbx-style  mandatory">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>HINDU</asp:ListItem>
                                                    <asp:ListItem>ISLAM</asp:ListItem>
                                                    <asp:ListItem>SIKH</asp:ListItem>
                                                    <asp:ListItem>CHRISTIAN</asp:ListItem>
                                                    <asp:ListItem>BUDDHISM</asp:ListItem>
                                                    <asp:ListItem>JAIN</asp:ListItem>
                                                    <asp:ListItem>N/A</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Category<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_cast" runat="server" class="form-select txtbx-style mandatory">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GENERAL</asp:ListItem>
                                                    <asp:ListItem>OBC</asp:ListItem>
                                                    <asp:ListItem>OBC-A</asp:ListItem>
                                                    <asp:ListItem>OBC-B</asp:ListItem>
                                                    <asp:ListItem>ST</asp:ListItem>
                                                    <asp:ListItem>SC</asp:ListItem>
                                                    <asp:ListItem>EBC</asp:ListItem>
                                                    <asp:ListItem>OTHERS</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Caste</label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_caste_jati" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Ration Type<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_ration_cards_types" runat="server" class="form-select txtbx-style">
                                                    <asp:ListItem>N/A</asp:ListItem>
                                                    <asp:ListItem>APL</asp:ListItem>
                                                    <asp:ListItem>BPL</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Aadhar Card No.</label>
                                            <asp:TextBox ID="txt_aadhar_no" runat="server" class="form-control txtbx-style" onkeypress="return isNumberKey(event)" MaxLength="16"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">HEIGHT</label>
                                            <asp:TextBox ID="txt_height" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">WEIGHT</label>
                                            <asp:TextBox ID="txt_weight" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Previous School Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Name  of the last school</label>
                                            <asp:TextBox ID="txt_lastschool" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Admission Date</label>
                                            <asp:TextBox ID="txt_admission_date_old" runat="server" class="form-control txtbx-style" onkeyup="var v = this.value; if (v.match(/^\d{2}$/) !== null) {this.value = v + '/';} else if (v.match(/^\d{2}\/\d{2}$/) !== null) {this.value = v + '/';}" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Board Type</label>
                                            <asp:DropDownList ID="ddl_prev_board_type" runat="server" class="form-select txtbx-style" AutoPostBack="true" OnSelectedIndexChanged="ddl_prev_board_SelectedIndexChanged">
                                                <asp:ListItem>SELECT</asp:ListItem>
                                                <asp:ListItem>COUNCIL</asp:ListItem>
                                                <asp:ListItem>STATE</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Board</label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_board_list" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Passour Class</label>
                                            <asp:DropDownList ID="ddl_old_class" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Percentage %</label>
                                            <asp:TextBox ID="txt_percentage" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Reason for shift</label>
                                            <asp:TextBox ID="txt_reason_for_shift" runat="server" CssClass="form-control txtbx-style" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Year</label>
                                            <asp:TextBox ID="txt_year" runat="server" onkeypress="return isNumberKey(event)" MaxLength="4" CssClass="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Parent's Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Father's Name<sup>*</sup></label>
                                            <asp:TextBox ID="txt_father_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Father's Occupation</label>
                                            <asp:DropDownList ID="ddl_occupation" runat="server" class="form-select txtbx-style">
                                                <asp:ListItem>NA</asp:ListItem>
                                                <asp:ListItem>OTHERS</asp:ListItem>
                                                <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                                <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                                <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                <asp:ListItem>BUSINESS</asp:ListItem>
                                                <asp:ListItem>FACTORY WORKER</asp:ListItem>
                                                <asp:ListItem>FARMER</asp:ListItem>
                                                 <asp:ListItem>ELECTRICIAN</asp:ListItem>
                                                <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                                                <asp:ListItem>ACCOUNTANT</asp:ListItem>
                                                <asp:ListItem>ADVOCATE</asp:ListItem>
                                                <asp:ListItem>AIR CRAFT ENG</asp:ListItem>
                                                <asp:ListItem>ARMY</asp:ListItem>
                                                <asp:ListItem>ASSISTAND PROPESSOR</asp:ListItem>
                                                <asp:ListItem>ASSISTANT TEACHER</asp:ListItem>
                                                <asp:ListItem>BANKING SERVICE</asp:ListItem>
                                                <asp:ListItem>CENTRAL GOVT</asp:ListItem>
                                                <asp:ListItem>DOCTOR</asp:ListItem>
                                                <asp:ListItem>ENGINEER</asp:ListItem>
                                                <asp:ListItem>MECHANIC</asp:ListItem>
                                                <asp:ListItem>EXPIRED</asp:ListItem>
                                                <asp:ListItem>GOVT JOB</asp:ListItem>
                                                <asp:ListItem>GOVT RETIRED</asp:ListItem>
                                                <asp:ListItem>IT PROFESSIONAL</asp:ListItem>
                                                <asp:ListItem>JOURNALIST</asp:ListItem>
                                                <asp:ListItem>LAWYER</asp:ListItem>
                                                <asp:ListItem>MANAGER</asp:ListItem>
                                                <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                <asp:ListItem>RETIRED</asp:ListItem>
                                                <asp:ListItem>POLICE OFFICER</asp:ListItem>
                                                <asp:ListItem>PROFESSOR</asp:ListItem>
                                                <asp:ListItem>SENIOR AME</asp:ListItem>
                                                <asp:ListItem>SHOPKEEPER</asp:ListItem>
                                                <asp:ListItem>TEACHER</asp:ListItem>
                                                <asp:ListItem>TAILOR</asp:ListItem>
                                                <asp:ListItem>OTHER</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Father's Qualification</label>
                                            <asp:DropDownList ID="ddl_father_qualification" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Father's Annual Income</label>
                                            <asp:TextBox ID="txt_annual_income" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Mother's Name<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:TextBox ID="txt_mother_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Mother's Occupation</label>
                                            <asp:DropDownList ID="ddl_m_occupation" runat="server" CssClass="form-select txtbx-style">
                                                <asp:ListItem>NA</asp:ListItem>
                                                <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                                <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                                <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                <asp:ListItem>BUSINESS</asp:ListItem>
                                                <asp:ListItem>FARMER</asp:ListItem>
                                                <asp:ListItem>HOUSE WIFE</asp:ListItem>
                                                <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                                                <asp:ListItem>ADVOCATE</asp:ListItem>
                                                <asp:ListItem>ASSISTANT PROFESSOR</asp:ListItem>
                                                <asp:ListItem>ASST TEACHER</asp:ListItem>
                                                <asp:ListItem>GOVT JOB</asp:ListItem>
                                                <asp:ListItem>JOURNALIST</asp:ListItem>
                                                <asp:ListItem>PENSION</asp:ListItem>

                                                <asp:ListItem>SERVICE</asp:ListItem>
                                                <asp:ListItem>OTHERS</asp:ListItem>

                                                <asp:ListItem>NA</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Mother's Qualification</label>
                                            <asp:DropDownList ID="ddl_mother_qualification" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Mother's Annual Income</label>
                                            <asp:TextBox ID="txt_m_annual_income" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Guardian's Name</label>
                                            <asp:TextBox ID="txt_guardian_name" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Guardian's Occupation</label>
                                            <asp:DropDownList ID="ddl_g_occupation" runat="server" CssClass="form-select txtbx-style">
                                                <asp:ListItem>NA</asp:ListItem>
                                                <asp:ListItem>OTHERS</asp:ListItem>
                                                <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                                <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                                <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                <asp:ListItem>BUSINESS</asp:ListItem>
                                                <asp:ListItem>FARMER</asp:ListItem>
                                                <asp:ListItem>HOUSE WIFE</asp:ListItem>
                                                <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                                                <asp:ListItem>ACCOUNTANT</asp:ListItem>
                                                <asp:ListItem>ADVOCATE</asp:ListItem>
                                                <asp:ListItem>AIR CRAFT ENG</asp:ListItem>
                                                <asp:ListItem>ARMY</asp:ListItem>
                                                <asp:ListItem>ASSISTAND PROPESSOR</asp:ListItem>
                                                <asp:ListItem>ASSISTANT TEACHER</asp:ListItem>
                                                <asp:ListItem>BANKING SERVICE</asp:ListItem>
                                                <asp:ListItem>CENTRAL GOVT</asp:ListItem>
                                                <asp:ListItem>DOCTOR</asp:ListItem>
                                                <asp:ListItem>ENGINEER</asp:ListItem>
                                                <asp:ListItem>EXPIRED</asp:ListItem>
                                                <asp:ListItem>GOVT JOB</asp:ListItem>
                                                <asp:ListItem>GOVT RETIRED</asp:ListItem>
                                                <asp:ListItem>IT PROFESSIONAL</asp:ListItem>
                                                <asp:ListItem>JOURNALIST</asp:ListItem>
                                                <asp:ListItem>LAWYER</asp:ListItem>
                                                <asp:ListItem>MANAGER</asp:ListItem>
                                                <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                <asp:ListItem>RETIRED</asp:ListItem>
                                                <asp:ListItem>SENIOR AME</asp:ListItem>
                                                <asp:ListItem>SERVICE</asp:ListItem>
                                                <asp:ListItem>OTHER</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Guardian's Qualification</label>
                                            <asp:DropDownList ID="ddl_g_qualification" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Guardian's Annual Income</label>
                                            <asp:TextBox ID="txt_g_annual_income" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Mobile No.<sup>*</sup></label>
                                            <asp:TextBox ID="txt_father_mobile" runat="server" class="form-control txtbx-style mandatory" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">WhatsApp No.</label>
                                            <asp:TextBox ID="txt_father_whatsapp_no" runat="server" class="form-control txtbx-style" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Email Id</label>
                                            <asp:TextBox ID="txt_guardian_email" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Address Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Address<sup>*</sup></label>
                                            <asp:TextBox ID="txt_adress" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style mandatory" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Post Office</label>
                                            <asp:TextBox ID="txt_present_po" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Police Station</label>
                                            <asp:TextBox ID="txt_temp_ps" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">District</label>
                                            <asp:TextBox ID="txt_present_district" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">City</label>
                                            <asp:TextBox ID="txt_city" runat="server" oninput="this.value = this.value.toUpperCase()" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Pincode</label>
                                            <asp:TextBox ID="txt_pincode" runat="server" class="form-control txtbx-style" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">State</label>
                                            <asp:DropDownList ID="ddl_state" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Location</label>
                                            <asp:DropDownList ID="ddl_location" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>




                                <div style="float: left; width: 100%;" id="extraAdd" runat="server" visible="false">
                                    <div class="add-same-as">
                                        <asp:CheckBox ID="chkCopyHomeAddress" runat="server" Text=" Same as Present Address" />
                                    </div>
                                    <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                        <div class="col-md-12">
                                            <div class="txtbx-wprs">
                                                <label for="validationCustom01" class="form-label">Address<sup>*</sup></label>
                                                <asp:TextBox ID="txt_pAddress" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="txtbx-wprs">
                                                <label for="validationCustom01" class="form-label">Post Office</label>
                                                <asp:TextBox ID="txt_perma_po" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="txtbx-wprs">
                                                <label for="validationCustom01" class="form-label">Police Station</label>
                                                <asp:TextBox ID="txt_par_ps" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="txtbx-wprs">
                                                <label for="validationCustom01" class="form-label">District</label>
                                                <asp:TextBox ID="txt_perma_disctrict" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="txtbx-wprs">
                                                <label for="validationCustom01" class="form-label">City</label>
                                                <asp:TextBox ID="txt_pcity" runat="server" oninput="this.value = this.value.toUpperCase()" class="form-control txtbx-style"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="txtbx-wprs">
                                                <label for="validationCustom01" class="form-label">Pincode</label>
                                                <asp:TextBox ID="txt_Ppincod" runat="server" class="form-control txtbx-style" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="txtbx-wprs">
                                                <label for="validationCustom01" class="form-label">State</label>
                                                <asp:DropDownList ID="ddl_p_state" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Bank Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Account No.</label>
                                            <asp:TextBox ID="txt_account_no" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Account Holder Name</label>
                                            <asp:TextBox ID="txt_account_holder_name" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Bank Name</label>
                                            <asp:DropDownList ID="ddl_bank" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">IFSC Code</label>
                                            <asp:TextBox ID="txt_ifsc_code" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Branch Name</label>
                                            <asp:TextBox ID="txt_branch_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card" id="documentss">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Upload Document</h2>
                                <asp:TextBox ID="txt_doc_focus" Style="display: none" runat="server"></asp:TextBox>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <div class="online_frm-inr">
                                            <table class="table table-striped table-bordered dataTable">
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Document_type")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                <asp:Label ID="lbl_column_name" runat="server" Text='<%#Bind("Column_name")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_column_name_for_online_reg" runat="server" Text='<%#Bind("Online_reg_column")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:LinkButton ID="btn_upload_image" OnClick="btn_upload_image_Click" runat="server" Style="background: #c203e5; padding: 3px 10px 6px 10px; color: #fff; font-weight: 600; font-size: 14px; border-radius: 3px;">Save</asp:LinkButton>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <img runat="server" id="stdimages" style="max-width: 100px; max-height: 80px;" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important; border: 0px !important;">
                                <asp:Button ID="btn_save" class="button-61" runat="server" Text="Save" OnClick="btn_save_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" Visible="false" Style="background-color: #ee3f00;" OnClick="btn_cancel_Click" class="button-61" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function openDocUpload() {
            window.location = "#documentss";
        }



        //================Address Copy
        $(document).ready(function () {
            //Function to copy from txtShiptoAddress to txtShiptoAddress when checkbox is checked
            $('#<%=chkCopyHomeAddress.ClientID%>').on("click", function () {
                if ($(this).is(":checked")) {
                    $('#<%=txt_pAddress.ClientID %>').val($('#<%=txt_adress.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_perma_po.ClientID %>').val($('#<%=txt_present_po.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_par_ps.ClientID %>').val($('#<%=txt_temp_ps.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_perma_disctrict.ClientID %>').val($('#<%=txt_present_district.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_pcity.ClientID %>').val($('#<%=txt_city.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=ddl_p_state.ClientID %>').val($('#<%=ddl_state.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
                }
            });

            //Function to copy from txtShiptoAddress to txtShiptoAddress when checkbox is checked and user modifies it

            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=txt_pAddress.ClientID %>').val($('#<%=txt_adress.ClientID %>').val());
            }
            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=txt_perma_po.ClientID %>').val($('#<%=txt_present_po.ClientID %>').val());
            }
            if ($(this).is(":checked")) {
                $('#<%=txt_par_ps.ClientID %>').val($('#<%=txt_temp_ps.ClientID %>').val());
            }
            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=txt_perma_disctrict.ClientID %>').val($('#<%=txt_present_district.ClientID %>').val());
            }
            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=txt_pcity.ClientID %>').val($('#<%=txt_city.ClientID %>').val());
            }
            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=ddl_p_state.ClientID %>').val($('#<%=ddl_state.ClientID %>').val());
            }
            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
            }
        });
    </script>
</asp:Content>
