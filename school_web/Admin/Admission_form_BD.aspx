<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Admission_form_BD.aspx.cs" Inherits="school_web.Admin.Admission_form_BD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Student Admission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-wprs {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .online_frm-bg {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .online_frm {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .online_frm-hdg {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .othr-pg-cont-wpr {
            margin: 15px 0px 20px 0px;
        }

        .online_frm-h {
            height: auto;
            width: 100%;
            margin: 0px 0px 5px 0px;
            padding: 3px 10px 3px 10px;
            float: left;
            font-size: 21px;
            line-height: 29px;
            background: #0e66b1;
            color: #fff;
            font-weight: 500;
            border-radius: 2px;
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

        .form_control {
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 5px 5px 5px 5px;
            float: left;
            border: 1px solid rgb(171 171 171);
            font-size: 14px;
            color: #000;
            height: 38px;
            text-align: left;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }

            .form_control:hover {
                border-color: rgb(18 18 19);
            }

        .online_frm-grp {
            margin: 5px 0px 8px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .steps-dv {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .steps-bx-dv {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            position: relative;
        }

        .steps-bx-number {
            margin: 0px;
            padding: 0px;
            float: left;
            position: relative;
            width: 24px;
            height: 24px;
            line-height: 24px;
            text-align: center;
            font-weight: 600;
            border-radius: 100%;
            background-color: rgb(234, 236, 239);
            font-family: 'IBM Plex Sans', sans-serif;
        }

        .steps-bx-txt-p {
            margin: 0px;
            padding: 0px 0px 0px 8px;
            width: auto;
            float: left;
            min-width: 0px;
            font-weight: 500;
            font-size: 16px;
            line-height: 24px;
            color: rgb(112, 122, 138);
            font-family: 'IBM Plex Sans', sans-serif;
        }

        .steps-root {
            box-sizing: border-box;
            margin: 7px 0px 7px 10px;
            min-width: 0px;
            width: 4px;
            left: 18px;
            background-color: rgb(234, 236, 239);
            float: left;
            height: 38px;
        }

        .steps-root-done {
            background-color: rgb(252, 213, 53);
        }

        .stps-success-name {
            color: #000;
        }

        .stps-success-num {
            color: #000;
            background-color: rgb(252, 213, 53);
        }

        .acc-dt-sub-btn {
            margin: 8px 0px 0px;
            appearance: none;
            user-select: none;
            cursor: pointer;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            display: inline-flex;
            -webkit-box-align: center;
            align-items: center;
            -webkit-box-pack: center;
            justify-content: center;
            box-sizing: border-box;
            font-size: 17px;
            font-family: inherit;
            font-weight: 500;
            text-align: center;
            text-decoration: none;
            outline: none;
            padding: 7px 24px;
            line-height: 24px;
            min-width: 80px;
            word-break: keep-all;
            color: rgb(24, 26, 32);
            border-radius: 4px;
            min-height: 24px;
            border: none;
            background-image: none;
            background-color: rgb(252, 213, 53);
            width: 100%;
            font-family: 'IBM Plex Sans', sans-serif;
        }

            .acc-dt-sub-btn:hover {
                box-shadow: none;
                background-image: none;
                background-color: rgb(252, 213, 53);
                opacity: 0.9;
            }
        /*================================================*/
        .chkstle {
            display: block;
            position: relative;
            padding: 6px 10px 6px 10px;
            margin: 6px 5px 0px 0px;
            cursor: pointer;
            font-size: 15px;
            float: left;
            border: 1px solid #ddd;
            background: #f9f8f8;
            border-radius: 2px;
        }

            /* Hide the default checkbox */
            .chkstle input {
                cursor: pointer;
                width: 15px;
                margin: 5px 0px 0px 0px;
                float: left;
            }

        /* Create a custom checkbox */
        .mark {
            position: absolute;
            top: 0;
            left: 0;
            height: 25px;
            width: 25px;
            background-color: lightgray;
        }

        .chkstle:hover input ~ .mark {
            background-color: gray;
        }

        .chkstle input:checked ~ .mark {
            background-color: blue;
        }

        /* Create the mark/indicator (hidden when not checked) */
        .mark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the mark when checked */
        .chkstle input:checked ~ .mark:after {
            display: block;
        }

        /* Style the mark/indicator */
        .chkstle .mark:after {
            left: 9px;
            top: 5px;
            width: 5px;
            height: 10px;
            border: solid white;
            border-width: 0 3px 3px 0;
            transform: rotate(45deg);
        }

        .chkstle label {
            font-weight: 400;
            padding: 0px 5px 0px 5px;
            margin-bottom: 0px;
        }

        .protected {
            font-family: 'Syne Mono', monospace;
            font-size: 22px;
            float: left;
            width: 130px;
            padding: 0px 0px 0px;
            margin: 0px 0px 0px 0px;
            font-weight: 500;
            font-style: oblique;
            line-height: 41px;
            letter-spacing: 3px;
            color: #000000;
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            text-align: center;
            background: #ffffff !important;
            background: linear-gradient(to left top, transparent -54.25%, currentColor 100.5%, currentColor 7.5%, transparent 128.25%);
        }

        .acc-dt-sub-back-btn {
            margin: 8px 0px 0px;
            appearance: none;
            user-select: none;
            cursor: pointer;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            display: inline-flex;
            -webkit-box-align: center;
            align-items: center;
            -webkit-box-pack: center;
            justify-content: center;
            box-sizing: border-box;
            font-size: 17px;
            font-family: inherit;
            font-weight: 500;
            text-align: center;
            text-decoration: none;
            outline: none;
            padding: 7px 24px;
            line-height: 24px;
            min-width: 80px;
            word-break: keep-all;
            color: rgb(255 255 255);
            border-radius: 4px;
            min-height: 24px;
            border: none;
            background-image: none;
            background-color: rgb(74 74 74);
            width: 100%;
            font-family: 'IBM Plex Sans', sans-serif;
        }

            .acc-dt-sub-back-btn:hover {
                box-shadow: none;
                background-image: none;
                background-color: rgb(74 74 74);
                opacity: 0.9;
            }

        sup {
            font-weight: 600;
            color: red;
            font-size: 14px;
            top: -0.2em;
        }

        .txtbxError {
            border: 1px solid #f00;
        }
        /*================================*/
        .aplication-sec {
            margin: 5px 0px 5px 0px;
            padding: 0px 0px 20px 0px;
            height: auto;
            width: 100%;
            float: left;
            border: 1px solid #fcd535;
            background: #ff4b0247;
        }

        .reg-content-sec {
            margin: 0px;
            padding: 8px 15px 0px 15px;
            width: 100%;
            height: auto;
            float: left;
        }

        .terms-ul {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
        }

            .terms-ul li {
                margin: 5px 0px;
                padding: 0px 0px 0px 23px;
                width: 100%;
                height: auto;
                float: left;
                list-style-type: none;
                line-height: 24px;
                position: relative;
                font-size: 16px;
                color: #4e4e4e;
                font-family: 'IBM Plex Sans', sans-serif;
            }

                .terms-ul li:before {
                    font-family: FontAwesome;
                    content: "\f00c";
                    font-size: 15px;
                    position: absolute;
                    top: 2px;
                    left: 0px;
                    color: #bba822;
                }

        .app-head-sec {
            margin: 0px;
            padding: 0px 0px 0px 0px;
            width: 100%;
            height: auto;
            float: left;
            border-bottom: 1px solid #fcd535;
        }

        .regi-title {
            margin: 0px;
            padding: 5px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 22px;
            font-weight: 600;
            text-align: center;
            font-family: 'IBM Plex Sans', sans-serif;
        }

        .textcheckbbbb {
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            font-size: 14px;
            line-height: 23px;
            font-weight: 500;
            color: #333;
            text-align: justify;
            float: left;
            font-family: 'IBM Plex Sans', sans-serif;
        }

            .textcheckbbbb span {
                font-weight: 600;
            }

        .add-same-as {
            margin: 0px;
            padding: 10px 0px;
            width: 100%;
            float: left;
        }

            .add-same-as input {
                margin: 0px;
                padding: 0px;
            }

            .add-same-as label {
                margin: 0px;
                padding: 0px 0px 0px 10px;
                font-size: 16px;
                font-weight: 600;
            }

        tbody, td, tfoot, th, thead, tr {
            padding: 5px 5px;
        }

        .clicktxt {
            position: absolute;
            left: 0;
            padding: 2px 0px 0px 35px;
            top: -1px;
        }

        @media only screen and (max-width:800px) {
            .steps-dv {
                display: none;
            }
        }
    </style>


    <script type="text/javascript">
        $(function () {
            $("#<%=txt_caste_jati.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'admission.aspx/GetRooPath',
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


        function openModalJati() {
            $('#myModalJati').modal('show');
        }
    </script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
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
                <div class="breadcrumb-title pe-3">Admission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Admission</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Student Admission"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row">
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <div class="online_frm-bg">
                                            <div class="online_frm">
                                                <asp:Panel ID="pnl_academic_details" runat="server">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Current Academic Details</h2>
                                                        <p class="online_frm-p">Enter student academic details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Student Type <sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_student_type" runat="server" CssClass="form_control">
                                                                        <asp:ListItem>NEW</asp:ListItem>
                                                                        <asp:ListItem>OLD</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Discount Group<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_category" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Discount Sub-Group<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_subcategory" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Form Sl.No.</h2>
                                                                    <asp:TextBox ID="txt_form_slno" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">UID/Board/APAAR No.</h2>
                                                                    <asp:TextBox ID="txt_uid" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Index No.</h2>
                                                                    <asp:TextBox ID="txt_index_no" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Admission Date<sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_admission_date" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Session<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddlsession" runat="server" CssClass="form_control" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Student's PEN No.</h2>
                                                                    <asp:TextBox ID="txt_student_pen_no" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="dayBoardingWithLunchDV">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="display: none">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Admission in<sup>*</sup></h2>
                                                                    <asp:RadioButton ID="rdb_dayscholar" runat="server" Text="Day Scholar" Checked="true" GroupName="ak" Enabled="false" AutoPostBack="false" class="chkstle" Style="margin: 3px 5px 0px 0px;" />
                                                                    <asp:RadioButton ID="rdb_hostel" runat="server" Text="Hosteler" GroupName="ak" AutoPostBack="false" Enabled="false" class="chkstle" Style="margin: 3px 5px 0px 0px;" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Boarding Type<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_day_boarding" runat="server" class="form_control">
                                                                        <asp:ListItem Value="0">DAY SCHOLAR</asp:ListItem>
                                                                        <%--<asp:ListItem Value="2">DAY BOARDING WITH LUNCH</asp:ListItem>--%>
                                                                        <asp:ListItem Value="3">HOSTEL</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="transportddlDV">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Transport<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_istransport_assign" runat="server" class="form_control">
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div id="HostelDV" style="margin: 0px 0px 5px 0px; float: left; width: 100%; border: 1px solid #ddd; padding: 1px 5px 0px; border-radius: 4px; background: rgb(209 255 204);">
                                                            <div class="row">
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">Hostel<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_hostel" runat="server" CssClass="form_control" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">Room Type<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_room_cat" runat="server" CssClass="form_control" OnSelectedIndexChanged="ddl_room_cat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">Room No.<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_room" runat="server" CssClass="form_control" AutoPostBack="true" OnSelectedIndexChanged="ddl_room_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">Bed No.<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_bed" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div id="TransportDV" style="margin: 0px 0px 5px 0px; float: left; width: 100%; border: 1px solid #ddd; padding: 1px 5px 0px; border-radius: 4px; background: #fff3b6;">
                                                            <div class="row">
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">From Month<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_trns_month" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">Vehicle<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_trns_vehicle" runat="server" CssClass="form_control" OnSelectedIndexChanged="ddl_trns_vehicle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">Route<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_trns_route" runat="server" CssClass="form_control" AutoPostBack="true" OnSelectedIndexChanged="ddl_trns_route_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                                    <div class="online_frm-grp">
                                                                        <h2 class="online_frm-grp-h">Boarding Point<sup>*</sup></h2>
                                                                        <asp:DropDownList ID="ddl_trns_boarding_point" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

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
                                                                    $("#transportddlDV").hide();
                                                                    $("#TransportDV").hide();
                                                                }
                                                                else {
                                                                    $("#HostelDV").hide();
                                                                    $("#transportddlDV").show();

                                                                    if ($('#<%= ddl_istransport_assign.ClientID %> option:selected').val() == "1") {
                                                                        $("#TransportDV").show();
                                                                    }
                                                                    else {
                                                                        $("#TransportDV").hide();
                                                                    }
                                                                }
                                                            }
                                                        </script>
                                                        <script type="text/javascript">
                                                            $(document).ready(function () {
                                                                on_transp_type_selection();
                                                                $("#<%=ddl_istransport_assign.ClientID%>").on('change', function () {
                                                                    on_transp_type_selection();
                                                                })
                                                            });

                                                            function on_transp_type_selection() {
                                                                if ($('#<%= ddl_istransport_assign.ClientID %> option:selected').val() == "1") {
                                                                    $("#TransportDV").show();
                                                                }
                                                                else {
                                                                    $("#TransportDV").hide();
                                                                }
                                                            }
                                                        </script>


                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Class<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddlclass" runat="server" CssClass="form_control" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Section</h2>
                                                                    <asp:DropDownList ID="ddl_section" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Admission no.<sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_admission_no" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Roll No.</h2>
                                                                    <asp:TextBox ID="txt_rollnumber" onkeypress="return isNumberKey(event)" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">House</h2>
                                                                    <asp:DropDownList ID="ddl_house" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="acc-dt-sub-back-btn" Visible="false" OnClick="btn_cancel_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_academic_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_academic_dt_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>



                                                <asp:Panel ID="pnl_student_details" runat="server" Visible="false">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Student Details</h2>
                                                        <p class="online_frm-p">Enter student  details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">First Name <sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_firstname" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Middle Name <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_middlename" runat="server" oninput="this.value = this.value.toUpperCase()" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Last Name <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_lastname" runat="server" oninput="this.value = this.value.toUpperCase()" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Date of birth<sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_dob" runat="server" CssClass="form_control" placeholder="dd/mm/yyyy" onkeyup="var v = this.value; if (v.match(/^\d{2}$/) !== null) {this.value = v + '/';} else if (v.match(/^\d{2}\/\d{2}$/) !== null) {this.value = v + '/';}" MaxLength="10"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Place of Birth</h2>
                                                                    <asp:TextBox ID="txt_place_of_birth" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Birth Certificate</h2>
                                                                    <asp:DropDownList ID="ddl_is_birth_certificate" runat="server" CssClass="form_control">
                                                                        <asp:ListItem>NO</asp:ListItem>
                                                                        <asp:ListItem>YES</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="brth_crtificate_no">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Birth Certificate no.<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_birth_certificate_no" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Gender <sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form_control">
                                                                        <asp:ListItem>MALE</asp:ListItem>
                                                                        <asp:ListItem>FEMALE</asp:ListItem>
                                                                        <asp:ListItem>TRANSGENDER</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Blood Group<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_blood_group" runat="server" CssClass="form_control">
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
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Nationality<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_nationality" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Religion<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_religion" runat="server" CssClass="form_control">
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

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Ration Type<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_ration_cards_types" runat="server" class="form_control">
                                                                        <asp:ListItem>N/A</asp:ListItem>
                                                                        <asp:ListItem>APL</asp:ListItem>
                                                                        <asp:ListItem>BPL</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>


                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Category<sup>*</sup></h2>
                                                                    <div class="select2-dv">
                                                                        <asp:DropDownList ID="ddl_cast_category" runat="server" class="single-select">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h" style="position: relative">Caste <a href="#!" data-toggle="modal" data-target="#myModalJati" class="addmore-btns"><span class="material-symbols-outlined" style="font-size: 17px;">add</span></a></h2>
                                                                    <div class="select2-dv">
                                                                        <asp:DropDownList ID="ddl_caste_jati" runat="server" class="single-select"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Caste Certificate</h2>
                                                                    <asp:DropDownList ID="ddl_is_cast_certificate" runat="server" class="form_control">
                                                                        <asp:ListItem>NO</asp:ListItem>
                                                                        <asp:ListItem>YES</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="cst_crtificte_no_dv">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Caste Certificate No.<sup></sup></h2>
                                                                    <asp:TextBox ID="txt_cast_certificate_no" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Aadhar card no</h2>
                                                                    <asp:TextBox ID="txt_aadharno_mark" MaxLength="16" onkeypress="return isNumberKey(event)" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Mother Tongue</h2>
                                                                    <asp:DropDownList ID="ddl_student_mother_tongue" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">If any illness</h2>
                                                                    <asp:DropDownList ID="ddl_illness" runat="server" class="form_control">
                                                                        <asp:ListItem>NO</asp:ListItem>
                                                                        <asp:ListItem>YES</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="illnes_remarkDv">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Illness Remark<sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_illness_remark" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">RTE Student</h2>
                                                                    <asp:DropDownList ID="ddl_rte" runat="server" class="form_control">
                                                                        <asp:ListItem>NO</asp:ListItem>
                                                                        <asp:ListItem>YES</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Staff Ward</h2>
                                                                    <asp:DropDownList ID="ddl_staff_ward" runat="server" class="form_control">
                                                                        <asp:ListItem>NO</asp:ListItem>
                                                                        <asp:ListItem>YES</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="staff_name">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Staff Name<sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_staff_name" runat="server" class="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Employee Code</h2>
                                                                    <asp:TextBox ID="txt_employee_code" runat="server" class="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Identification marks</h2>
                                                                    <asp:TextBox ID="txt_identification_marks" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="row">
                                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                        <div class="online_frm-grp">
                                                                            <h2 class="online_frm-grp-h">Height <i style="float: right; font-style: normal; font-size: 12px; font-weight: 500;">cms</i></h2>
                                                                            <asp:TextBox ID="txt_height" runat="server" CssClass="form_control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                        <div class="online_frm-grp">
                                                                            <h2 class="online_frm-grp-h">Weight <i style="float: right; font-style: normal; font-size: 12px; font-weight: 500;">kg</i></h2>
                                                                            <asp:TextBox ID="txt_weight" runat="server" CssClass="form_control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Hobbies/Field of interest</h2>
                                                                    <asp:TextBox ID="txt_hobbies" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="siblingdetailS">
                                                            <div class="online_frm-hdg">
                                                                <h2 class="online_frm-h">Sibling Details</h2>
                                                            </div>
                                                            <table class="table-bordered" style="width: 100%">
                                                                <tr>
                                                                    <th style="color: #fff; width: 50px;">S. No.</th>
                                                                    <th style="color: #fff;">Name of Sibling</th>
                                                                    <th style="color: #fff;">Age</th>
                                                                    <th style="color: #fff;">School/College</th>
                                                                    <th style="color: #fff;">Class</th>
                                                                </tr>
                                                                <tr>
                                                                    <td>1.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_name_of_sibling1" runat="server" class="form-control"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_age_sibling1" Style="width: 80px;" runat="server" class="form-control"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_school_sibling1" runat="server" class="form-control"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddl_class_sb1" class="form-select" runat="server"></asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>2.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_name_of_sibling2" runat="server" class="form-control"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_age_sibling2" Style="width: 80px;" runat="server" class="form-control"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_school_sibling2" runat="server" class="form-control"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddl_class_sb2" class="form-select" runat="server"></asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                        </div>





                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_cack_std" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_cack_std_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_std_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_std_dt_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pn_prev_info" runat="server" Visible="false">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Previous School Details</h2>
                                                        <p class="online_frm-p">Enter your previous school details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Name of last school attended<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_lastschool" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Admission Date </h2>
                                                                    <asp:TextBox ID="txt_admission_date_old" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Board Type<sup> </sup></h2>
                                                                    <asp:DropDownList ID="ddl_prev_board_type" runat="server" class="form_control" AutoPostBack="true" OnSelectedIndexChanged="ddl_prev_board_SelectedIndexChanged">
                                                                        <asp:ListItem>SELECT</asp:ListItem>
                                                                        <asp:ListItem>COUNCIL</asp:ListItem>
                                                                        <asp:ListItem>STATE</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Board<sup> </sup></h2>
                                                                    <asp:DropDownList ID="ddl_board_list" runat="server" class="form_control" oninput="this.value = this.value.toUpperCase()"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Last class attended</h2>
                                                                    <asp:DropDownList ID="ddl_old_class" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Percentage %</h2>
                                                                    <asp:TextBox ID="txt_percentage" onkeypress="return isNumberKey(event)" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Reason for shift</h2>
                                                                    <asp:TextBox ID="txt_reason_for_shift" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Year</h2>
                                                                    <asp:TextBox ID="txt_year" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>




                                                        <%--//=========================--%>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Last class attendance</h2>
                                                                    <asp:TextBox ID="txt_prev_last_class_attended" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Status</h2>
                                                                    <asp:DropDownList ID="ddl_prev_pass_fail_status" runat="server" CssClass="form_control">
                                                                        <asp:ListItem>Pass</asp:ListItem>
                                                                        <asp:ListItem>Failed</asp:ListItem>
                                                                        <asp:ListItem>Incomplete</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_back_pre_school" oninput="this.value = this.value.toUpperCase()" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_pre_school_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_prev_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_prev_dt_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnl_father_dt" runat="server" Visible="false">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Father’s Contact Details</h2>
                                                        <p class="online_frm-p">Enter your father’s contact details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">First Name <sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_father_first_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Middle Name <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_father_middle_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Last Name <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_father_last_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Occupation<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_occupation" runat="server" class="form_control">
                                                                        <asp:ListItem>NA</asp:ListItem>
                                                                        <asp:ListItem>OTHERS</asp:ListItem>
                                                                        <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                                                        <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                                                        <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                                        <asp:ListItem>BUSINESS</asp:ListItem>
                                                                        <asp:ListItem>FARMER</asp:ListItem>
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
																		 <asp:ListItem>Ex-SERVICE MANE</asp:ListItem>
																		
                                                                        <asp:ListItem>OTHER</asp:ListItem> 
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Qualification<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_father_qualification" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Nationality</h2>
                                                                    <asp:DropDownList ID="ddl_father_nationality" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Marital Status</h2>
                                                                    <asp:DropDownList ID="ddl_maritial_status" runat="server" class="form_control">
                                                                        <asp:ListItem>MARRIED</asp:ListItem>
                                                                        <asp:ListItem>UNMARRIED</asp:ListItem>
                                                                        <asp:ListItem>DIVORCE</asp:ListItem>
                                                                        <asp:ListItem>SINGLE PARENT</asp:ListItem>
                                                                        <asp:ListItem>N/A</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Mobile No.<sup>*</sup></h2>
                                                                    <div class="row">
                                                                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                            <asp:DropDownList ID="ddl_cunterycode1" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                            <asp:TextBox ID="txt_father_mobile" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">WhatsApp No.</h2>
                                                                    <div class="row">
                                                                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                            <asp:DropDownList ID="ddl_fthr_whatsapp_c_Code" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                            <asp:TextBox ID="txt_father_whatsapp_no" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Email</h2>
                                                                    <asp:TextBox ID="txt_guardian_email" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Annual Income<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_annual_income" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Aadhar No.</h2>
                                                                    <asp:TextBox ID="txt_father_aadhar_no" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Age<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_father_age" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="display: none">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_back_father" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_father_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_fther_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_fther_dt_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnl_mther_dt" runat="server" Visible="false">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Mother’s Contact Details</h2>
                                                        <p class="online_frm-p">Enter your mother’s contact details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">First Name <sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_mother_first_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Middle Name </h2>
                                                                    <asp:TextBox ID="txt_mother_middle_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Last Name <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_mother_last_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Occupation</h2>
                                                                    <asp:DropDownList ID="ddl_m_occupation" runat="server" CssClass="form_control">
                                                                        <asp:ListItem>OTHERS</asp:ListItem>
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
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Qualification<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_mother_qualification" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Nationality</h2>
                                                                    <asp:DropDownList ID="ddl_mother_nationality" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Marital Status</h2>
                                                                    <asp:DropDownList ID="ddl_m_maritial_status" runat="server" CssClass="form_control">
                                                                        <asp:ListItem>MARRIED</asp:ListItem>
                                                                        <asp:ListItem>UNMARRIED</asp:ListItem>
                                                                        <asp:ListItem>DIVORCE</asp:ListItem>
                                                                        <asp:ListItem>SINGLE PARENT</asp:ListItem>
                                                                        <asp:ListItem>N/A</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Mobile No.</h2>
                                                                    <div class="row">
                                                                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                            <asp:DropDownList ID="ddl_cunterycode2" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                            <asp:TextBox ID="txt_mother_mobile_no" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">WhatsApp No.</h2>
                                                                    <div class="row">
                                                                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                            <asp:DropDownList ID="ddl_mthr_whatsapp_c_Code" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                            <asp:TextBox ID="txt_mother_whatsapp_no" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Email Id</h2>
                                                                    <asp:TextBox ID="txt_mother_emailid" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Aadhar No.</h2>
                                                                    <asp:TextBox ID="txt_mother_aadhar_no" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Age</h2>
                                                                    <asp:TextBox ID="txt_mother_age" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Annual Income</h2>
                                                                    <asp:TextBox ID="txt_mother_annual_income" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Guardian’s Details</h2>
                                                        <p class="online_frm-p">Enter your guardian’s details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Guardian's Name<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_guardian_name" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Relation with student</h2>
                                                                    <asp:TextBox ID="txt_relation_with_student" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Occupation</h2>
                                                                    <asp:DropDownList ID="ddl_guardian_occupation" runat="server" CssClass="form_control">
                                                                        <asp:ListItem>OTHERS</asp:ListItem>
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
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Qualification<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_guardian_qualification" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Mobile No.</h2>
                                                                    <div class="row">
                                                                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                            <asp:DropDownList ID="ddl_guardian_contry_code" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                            <asp:TextBox ID="txt_guardian_mobile_no" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Aadhar No.</h2>
                                                                    <asp:TextBox ID="txt_guardian_aadhar_no" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Annual Income</h2>
                                                                    <asp:TextBox ID="txt_guardian_annual_income" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Address</h2>
                                                                    <asp:TextBox ID="txt_guardian_address" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_back_mother" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_mother_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_mther_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_mther_dt_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>


                                                <asp:Panel ID="pnl_address_dt" runat="server" Visible="false">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Address details</h2>
                                                        <p class="online_frm-p">Enter your address  details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Present Address<sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_adress" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Present P.O.<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_present_po" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Present P.S.<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_temp_ps" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Present District<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_present_district" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Present City<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_city" runat="server" oninput="this.value = this.value.toUpperCase()" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Present State<sup>* </sup></h2>
                                                                    <asp:TextBox ID="txt_c_state" runat="server" class="form_control" Visible="false"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddl_temp_state" runat="server" class="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Present Pin Code<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_pincode" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Country<sup>*</sup></h2>
                                                                    <asp:DropDownList ID="ddl_country_c" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Mobile No.<sup>*</sup></h2>
                                                                    <div class="row">
                                                                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                            <asp:DropDownList ID="ddl_cunterycode3" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                            <asp:TextBox ID="txt_temp_mobileno" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="add-same-as">
                                                            <asp:CheckBox ID="chkCopyHomeAddress" runat="server" Text=" Same as Present Address" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Permanent Address <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_pAddress" runat="server" oninput="this.value = this.value.toUpperCase()" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Permanent P.O. <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_perma_po" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Permanent P.S. <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_par_ps" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Permanent District <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_perma_disctrict" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Permanent City <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_pcity" runat="server" oninput="this.value = this.value.toUpperCase()" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Permanent State <sup></sup></h2>
                                                                    <asp:TextBox ID="txt_p_state" runat="server" class="form-control find-dv-txtbx" Visible="false"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddl_par_state" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Permanent Pin Code<sup> </sup></h2>
                                                                    <asp:TextBox ID="txt_Ppincod" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Country</h2>
                                                                    <asp:DropDownList ID="ddl_country_p" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Mobile No. </h2>
                                                                    <div class="row">
                                                                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                            <asp:DropDownList ID="ddl_cunterycode4" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                            <asp:TextBox ID="txt_p_mobile_no" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_back_add" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_add_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_add_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_add_dt_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>



                                                <asp:Panel ID="pnl_misc_dt" runat="server" Visible="false">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Bank Details</h2>
                                                        <p class="online_frm-p">Enter bank details to process your registration.</p>
                                                    </div>

                                                    <div class="online_frm-inr">
                                                        <%-- <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Is the student handicapped</h2>
                                                                    <asp:RadioButton ID="rd_handicp_yes" class="chkstle" runat="server" Text="Yes" />
                                                                    <asp:RadioButton ID="rd_handicp_no" runat="server" class="chkstle" Text="No" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Medical Remarks</h2>
                                                                    <asp:TextBox ID="txt_medicalremarks" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">From where did you hear about the school?</h2>
                                                                    <asp:TextBox ID="txt_about_theschool" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>

                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Account No.</h2>
                                                                    <asp:TextBox ID="txt_account_no" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Account Holder Name</h2>
                                                                    <asp:TextBox ID="txt_account_holder_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Bank Name</h2>
                                                                    <asp:DropDownList ID="ddl_bank" runat="server" CssClass="form_control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">IFSC Code</h2>
                                                                    <asp:TextBox ID="txt_ifsc_code" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Branch Name</h2>
                                                                    <asp:TextBox ID="txt_branch_name" oninput="this.value = this.value.toUpperCase()" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_back_misc" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_misc_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_misc_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_misc_dt_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnl_docs" runat="server" Visible="false">
                                                    <div class="online_frm-hdg">
                                                        <h2 class="online_frm-h">Upload Document<sup></sup></h2>
                                                        <p class="online_frm-p">Upload document to process your registration.(only jpg,png max size 200kb)</p>
                                                    </div>

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
                                                        <asp:HiddenField ID="hd_temp_reg_id" runat="server" />


                                                        <div class="row" style="display: none">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <table border="0" style="margin: 0px 0px 0px 0px; padding: 3px; height: 42px; width: 274px; font-size: 22px; border: 1px solid #000;">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="padding-left: 7px;">
                                                                                    <asp:Label ID="lbl_captch1" runat="server" Style="font-weight: bold;"></asp:Label>
                                                                                </td>
                                                                                <td style="padding: 0px 5px;">
                                                                                    <asp:Label ID="lbl_captch2" runat="server" Style="font-weight: bold;">+</asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_captch3" runat="server" Style="font-weight: bold;"> </asp:Label>
                                                                                </td>
                                                                                <td style="padding: 0px 5px;">=</td>
                                                                                <td style="padding: 5px 0px">
                                                                                    <asp:TextBox ID="txt_inputcatcha" runat="server" class="form-control" onkeypress="return isNumberKey(event)" Style="height: 27px; width: 91px;"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_back_doc" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_doc_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Button ID="btn_final_submit" runat="server" Text="Submit" ValidationGroup="ab" class="acc-dt-sub-btn" OnClick="btn_final_submit_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="steps-dv">
                                            <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS1">1</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt1">
                                                    <asp:LinkButton ID="lnk_step1" runat="server" OnClick="lnk_step1_Click" CssClass="clicktxt">Academic Details</asp:LinkButton>
                                                </p>
                                            </div>
                                            <div class="steps-root" runat="server" id="pro1"></div>

                                            <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS2">2</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt2">
                                                    <asp:LinkButton ID="lnk_step2" runat="server" OnClick="lnk_step2_Click" CssClass="clicktxt">Student Details</asp:LinkButton>
                                                </p>
                                            </div>
                                            <div class="steps-root" runat="server" id="pro2"></div>
                                            <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS3">3</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt3">
                                                    <asp:LinkButton ID="lnk_step3" runat="server" OnClick="lnk_step3_Click" CssClass="clicktxt">Previous School Details</asp:LinkButton>
                                                </p>
                                            </div>
                                            <div class="steps-root" runat="server" id="pro3"></div>
                                            <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS4">4</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt4">
                                                    <asp:LinkButton ID="lnk_step4" runat="server" OnClick="lnk_step4_Click" CssClass="clicktxt">Parent's Details</asp:LinkButton>
                                                </p>
                                            </div>
                                            <div class="steps-root" runat="server" id="pro4"></div>
                                            <%-- <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS5">5</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt5"><asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnk_step4_Click">Mother’s Contact Details</asp:LinkButton></p>
                                            </div>
                                            <div class="steps-root" runat="server" id="pro5"></div>--%>
                                            <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS6">5</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt6">
                                                    <asp:LinkButton ID="lnk_step5" runat="server" OnClick="lnk_step5_Click" CssClass="clicktxt">Address Details</asp:LinkButton>
                                                </p>
                                            </div>
                                            <div class="steps-root" runat="server" id="pro6"></div>
                                            <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS7">6</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt7">
                                                    <asp:LinkButton ID="lnk_step6" runat="server" OnClick="lnk_step6_Click" CssClass="clicktxt">Bank Details</asp:LinkButton>
                                                </p>
                                            </div>
                                            <div class="steps-root" runat="server" id="pro7"></div>
                                            <div class="steps-bx-dv">
                                                <p class="steps-bx-number" runat="server" id="pronumS8">7</p>
                                                <p class="steps-bx-txt-p" runat="server" id="prontxt8">
                                                    <asp:LinkButton ID="lnk_step7" runat="server" OnClick="lnk_step7_Click" CssClass="clicktxt">Upload Document</asp:LinkButton>
                                                </p>
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
        <!--end row-->
    </div>

    <script lang="Javascript">

        <%--$(function () {
            $("#<%=txt_firstname.ClientID %>").on('keypress', function (e) {
                if (e.which == 32) {
                    console.log('Space Detected');
                    return false;
                }
            });
        });--%>


        function IsCharacter(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if (!(charCode >= 65 && charCode <= 90) && !(charCode >= 97 && charCode <= 122) && (charCode != 32 && charCode != 8) && !(charCode == 9)) {
                return false;
            }
            //if (e.which == 32) {
            //    console.log('Space Detected');
            //    return false;
            //}
            return true;
        }

        //===============


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        $(function () {
            $("#<%=txt_admission_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
       <%-- $(function () {
            $("#<%=txt_dob.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });--%>
        $(function () {
            $("#<%=txt_admission_date_old.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });


        //===========================================
        $(document).ready(function () {
            on_is_illness_selection();
            $("#<%=ddl_illness.ClientID%>").on('change', function () {
                on_is_illness_selection();
            })
        });

        function on_is_illness_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_illness.ClientID %> option:selected').val() == "YES") {
                $("#illnes_remarkDv").show();
            }
            else {
                $("#illnes_remarkDv").hide();
            }
        }
        //===========================================
        $(document).ready(function () {
            on_is_cast_selection();
            $("#<%=ddl_is_cast_certificate.ClientID%>").on('change', function () {
                on_is_cast_selection();
            })
        });

        function on_is_cast_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_cast_certificate.ClientID %> option:selected').val() == "YES") {
                $("#cst_crtificte_no_dv").show();
            }
            else {
                $("#cst_crtificte_no_dv").hide();
            }
        }

        //===========================================
        $(document).ready(function () {
            on_is_birth_selection();
            $("#<%=ddl_is_birth_certificate.ClientID%>").on('change', function () {
                on_is_birth_selection();
            })
        });

        function on_is_birth_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_birth_certificate.ClientID %> option:selected').val() == "YES") {
                $("#brth_crtificate_no").show();
            }
            else {
                $("#brth_crtificate_no").hide();
            }
        }
        //===========================================
        $(document).ready(function () {
            on_staffward_selection();
            $("#<%=ddl_staff_ward.ClientID%>").on('change', function () {
                on_staffward_selection();
            })
        });

        function on_staffward_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_staff_ward.ClientID %> option:selected').val() == "YES") {
                $("#staff_name").show();
            }
            else {
                $("#staff_name").hide();
            }
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
                    $('#<%=ddl_par_state.ClientID %>').val($('#<%=ddl_temp_state.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
                }

                if ($(this).is(":checked")) {
                    $('#<%=txt_p_mobile_no.ClientID %>').val($('#<%=txt_temp_mobileno.ClientID %>').val());
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
                $('#<%=ddl_par_state.ClientID %>').val($('#<%=ddl_temp_state.ClientID %>').val());
            }
            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
            }
            if ($(this).is(":checked")) {
                $('#<%=txt_p_mobile_no.ClientID %>').val($('#<%=txt_temp_mobileno.ClientID %>').val());
            }

        });
    </script>

    <style>
        .td1 {
            padding: 3px 0px 4px 5px !important;
            background: #f00 !important;
            color: #fff !important;
            font-weight: bold !important;
            margin: 2px 0px 1px 0px;
            float: left;
            width: 100%;
        }

        .Llabel {
            margin: 11px 0px 6px 0px;
        }

        .modal-body {
            padding: 3px 1px 2px 1px !important;
        }

        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1050;
            display: none;
            width: 100%;
            height: 100%;
            overflow: hidden;
            outline: 0;
        }

        .modal-header {
            padding: 7px 15px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }
    </style>

    <script type="text/javascript">
        function openModal1() {

            $('#myModal1').modal('show');
        }

    </script>

    <div class="modal fade" id="myModal1" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Required Filed</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-12">
                                <label for="validationCustom01" class="find-dv-lbl" style="background: #faf52b; padding: 2px 0px 2px 5px;">
                                    <span style="border-bottom: 1px solid #000;">1. Academic Details</span>
                                </label>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <table style="margin: 0px; padding: 0px; float: left; height: auto" class="table table-bordered">

                                    <tr id="Categories" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Categories is required
                                        </td>

                                    </tr>
                                    <tr id="subCategories" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Sub Categories is required
                                        </td>
                                    </tr>

                                    <tr id="admissiondate" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Admission Date is required
                                        </td>
                                    </tr>
                                    <tr id="session" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Session is required
                                        </td>
                                    </tr>
                                    <tr id="Class_name" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Class name is required
                                        </td>
                                    </tr>
                                    <tr id="admission_no" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Admission No. is required
                                        </td>
                                    </tr>
                                    <tr id="admission_no_dublicate" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Admission No. is duplicate
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <label for="validationCustom01" class="find-dv-lbl" style="background: #faf52b; padding: 2px 0px 2px 5px;">
                                    <span style="border-bottom: 1px solid #000;">2. Student Details</span>
                                </label>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <table style="margin: 0px; padding: 0px; float: left; height: auto" class="table table-bordered">

                                    <tr id="studentname" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Student name is required
                                        </td>

                                    </tr>
                                    <tr id="dateofbirth" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Date of birth is required
                                        </td>
                                    </tr>

                                    <tr id="caste" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Category is required
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-sm-12">
                                <label for="validationCustom01" class="find-dv-lbl" style="background: #faf52b; padding: 2px 0px 2px 5px;">
                                    <span style="border-bottom: 1px solid #000;">3. Previous School Details</span>
                                </label>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <label for="validationCustom01" class="find-dv-lbl" style="background: #faf52b; padding: 2px 0px 2px 5px;">
                                    <span style="border-bottom: 1px solid #000;">4. Parent's Details</span>
                                </label>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <table style="margin: 0px; padding: 0px; float: left; height: auto" class="table table-bordered">

                                    <tr id="fathername" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Father’s Name is required
                                        </td>

                                    </tr>
                                    <tr id="fathermobileno" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Father’s mobile number is required
                                        </td>
                                    </tr>

                                    <tr id="mothername" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Mother’s Name is required
                                        </td>
                                    </tr>




                                </table>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-sm-12">
                                <label for="validationCustom01" class="find-dv-lbl" style="background: #faf52b; padding: 2px 0px 2px 5px;">
                                    <span style="border-bottom: 1px solid #000;">5. Address Details</span>
                                </label>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <table style="margin: 0px; padding: 0px; float: left; height: auto" class="table table-bordered">

                                    <tr id="address" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Address is required
                                        </td>

                                    </tr>

                                    <tr id="statename" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>State name is required
                                        </td>
                                    </tr>

                                    <tr id="mobilenotemp" runat="server" visible="false">
                                        <td class="td1"><i class="fa fa-times"></i>Mobile  no number is required
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


    <div class="modal fade" id="myModalJati" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header" style="padding: 3px 10px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Add Caste</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body" style="padding: 5px 5px;">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                        <div class="mdl-frm-row">
                            <label for="validationCustom01" class="find-dv-lbl">Caste Name</label>
                            <div class="row">
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txt_caste_jati" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btn_add_caste" OnClick="btn_add_caste_Click" runat="server" Text="Save" class="button-6161 disc-pop-save_disc" Style="margin: 0px 0px 0px 0px; height: 30px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
