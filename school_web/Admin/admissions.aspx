<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="admissions.aspx.cs" Inherits="school_web.Admin.admissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Admission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/js/my_old.js"></script>
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <style type="text/css">
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
                font-size: 14px;
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


        .add-bx-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .add-bx-wpr-h {
            margin: 0px;
            padding: 4px 5px 4px 5px;
            width: 100%;
            float: left;
            border-bottom: 1px solid #dee2e6;
            font-size: 20px;
        }

        .acdbackground {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .acdbackground table {
                margin: 0px;
                padding: 0px;
            }

                .acdbackground table tr th {
                    margin: 0px;
                    border: 1px solid #ddd;
                    padding: 5px 5px 5px 5px;
                }

                .acdbackground table tr td {
                    margin: 0px;
                    padding: 5px 5px 5px 5px;
                    border: 1px solid #ddd;
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
                                    <div class="col-md-3" style="display: none">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Category<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_category" runat="server" class="form-select txtbx-style" AutoPostBack="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3" style="display: none">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Sub Category<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_subcategory" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>

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
                                            <label for="validationCustom01" class="form-label">Admission No.<sup>*</sup></label>
                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-select txtbx-style mandatory" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Information on Child</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">First Name<sup>*</sup></label>
                                            <asp:TextBox ID="txt_std_first_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Middle Name</label>
                                            <asp:TextBox ID="txt_std_middle_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Last Name (Surname)</label>
                                            <asp:TextBox ID="txt_student_last_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
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
                                            <label for="validationCustom01" class="form-label">Mother Tongue</label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_student_mother_tongue" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Second Language the child will study</label>
                                            <div class="clndr-div">
                                                <asp:DropDownList ID="ddl_second_language" runat="server" class="form-select txtbx-style">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Hindi</asp:ListItem>
                                                    <asp:ListItem>Bengali</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Place of Birth</label>
                                            <asp:TextBox ID="txt_place_of_birth" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Are there any medical conditions of your ward which the school should be aware of :</label>
                                            <asp:TextBox ID="txt_illness_remark" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="add-bx-wpr">
                                            <h2 class="add-bx-wpr-h">Residential Address</h2>
                                            <div style="width: 100%; padding: 5px 7px 7px; float: left;">
                                                <div class="row g-3 needs-validation">
                                                    <div class="col-md-12">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Address :</label>
                                                            <asp:TextBox ID="txt_adress" runat="server" class="form-control txtbx-style mandatory" TextMode="MultiLine" Style="height: 60px;"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">P.O. :</label>
                                                            <asp:TextBox ID="txt_present_po" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">P.S. :</label>
                                                            <asp:TextBox ID="txt_temp_ps" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">District :</label>
                                                            <asp:TextBox ID="txt_present_district" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">State :</label>
                                                            <asp:TextBox ID="txt_c_state" runat="server" class="form_control" Visible="false"></asp:TextBox>
                                                            <asp:DropDownList ID="ddl_temp_state" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Pin Code :</label>
                                                            <asp:TextBox ID="txt_pincode" runat="server" CssClass="form-control txtbx-style" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Tel :</label>
                                                            <asp:TextBox ID="txt_temp_mobileno" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">E-mail :</label>
                                                            <asp:TextBox ID="txt_temp_email_id" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Emergency Contact No. :</label>
                                                            <asp:TextBox ID="txt_temp_emergency_contact_no" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="add-same-as">
                                                    <asp:CheckBox ID="chkCopyHomeAddress" runat="server" Text="Residential Address Copy to Correspondence Address" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="add-bx-wpr">
                                            <h2 class="add-bx-wpr-h">Correspondence Address</h2>
                                            <div style="width: 100%; padding: 5px 7px 7px; float: left;">
                                                <div class="row g-3 needs-validation">
                                                    <div class="col-md-12">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Address :</label>
                                                            <asp:TextBox ID="txt_pAddress" runat="server" class="form-control txtbx-style" TextMode="MultiLine" Style="height: 60px;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">P.O. :</label>
                                                            <asp:TextBox ID="txt_perma_po" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">P.S. :</label>
                                                            <asp:TextBox ID="txt_par_ps" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">District :</label>
                                                            <asp:TextBox ID="txt_perma_disctrict" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">State :</label>
                                                            <asp:TextBox ID="txt_p_state" runat="server" class="form-control find-dv-txtbx" Visible="false"></asp:TextBox>
                                                            <asp:DropDownList ID="ddl_par_state" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Pin Code :</label>
                                                            <asp:TextBox ID="txt_Ppincod" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Tel :</label>
                                                            <asp:TextBox ID="txt_p_mobile_no" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">E-mail :</label>
                                                            <asp:TextBox ID="txt_p_email_id" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="txtbx-wprs">
                                                            <label for="validationCustom01" class="form-label">Emergency Contact No. :</label>
                                                            <asp:TextBox ID="txt_p_emergancy_contact_no" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Hobbies/Field of Interest:</label>
                                            <asp:TextBox ID="txt_hobbies" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Academic background of child</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-6">
                                        <div class="row g-3 needs-validation">
                                            <div class="col-md-12">
                                                <div class="txtbx-wprs">
                                                    <label for="validationCustom01" class="form-label">Previous School</label>
                                                    <asp:TextBox ID="txt_lastschool" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="txtbx-wprs">
                                                    <label for="validationCustom01" class="form-label">Last Class Attended</label>
                                                    <asp:DropDownList ID="ddl_old_class" runat="server" class="form-control txtbx-style"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="txtbx-wprs">
                                                    <label for="validationCustom01" class="form-label">Any Outstanding Achievement</label>
                                                    <asp:TextBox ID="txt_prev_any_achievement" runat="server" class="form-control txtbx-style" TextMode="MultiLine" Style="height: 124px;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="acdbackground">
                                            <table>
                                                <tr>
                                                    <th>Final Mark of Previous Year</th>
                                                    <th>Full Mark</th>
                                                    <th>Mark Obtained</th>
                                                </tr>
                                                <tr>
                                                    <td>English</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_eng_fm" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_eng_mo" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Hindi</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_hin_fm" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_hin_mo" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Math</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_math_fm" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_math_mo" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Social Science(Hist, Geog)</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_sc_fm" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_sc_mo" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Science</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_sci_fm" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_prev_sci_mo" runat="server" class="form-control txtbx-style"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>






                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <%--<h2 class="form-ttleS">Family Information</h2>--%>
                                <h2 class="form-ttleS">Father/Guardian:</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Name<sup>*</sup></label>
                                            <asp:TextBox ID="txt_father_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Age<sup>*</sup></label>
                                            <asp:TextBox ID="txt_father_age" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Nationality<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_f_nationality" runat="server" class="form-select txtbx-style mandatory"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Educational Qualification</label>
                                            <asp:DropDownList ID="ddl_father_qualification" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Institution</label>
                                            <asp:TextBox ID="txt_Father_institution" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Organisation</label>
                                            <asp:TextBox ID="txt_father_organization" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Working For</label>
                                            <asp:TextBox ID="txt_father_working_for" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Office Address</label>
                                            <asp:TextBox ID="txt_father_office_address" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Designation</label>
                                            <asp:DropDownList ID="ddl_occupation" runat="server" class="form-control txtbx-style">
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
                                                <asp:ListItem>OTHER</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Annual Income<sup>*</sup></label>
                                            <asp:TextBox ID="txt_annual_income" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Tel:<sup>*</sup></label>
                                            <asp:TextBox ID="txt_father_mobile" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">No. of Hours of Interaction with the child per week<sup>*</sup></label>
                                            <asp:TextBox ID="txt_father_no_of_hours_intrest_child" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>



                                <h2 class="form-ttleS">Mother/Guardian:</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Name<sup>*</sup></label>
                                            <asp:TextBox ID="txt_mother_name" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Age<sup>*</sup></label>
                                            <asp:TextBox ID="txt_mother_age" runat="server" class="form-control txtbx-style mandatory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Nationality<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_m_nationality" runat="server" class="form-select txtbx-style mandatory"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Educational Qualification</label>
                                            <asp:DropDownList ID="ddl_mother_qualification" runat="server" class="form-select txtbx-style"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Institution</label>
                                            <asp:TextBox ID="txt_mother_institution" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Organisation</label>
                                            <asp:TextBox ID="txt_mother_organization" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Working For</label>
                                            <asp:TextBox ID="txt_mother_working_for" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Office Address</label>
                                            <asp:TextBox ID="txt_mother_office_address" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Designation</label>
                                            <asp:DropDownList ID="ddl_m_occupation" runat="server" class="form-control txtbx-style">
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
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Annual Income</label>
                                            <asp:TextBox ID="txt_mother_annual_income" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">Tel:</label>
                                            <asp:TextBox ID="txt_mother_mobile_no" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">No. of Hours of Interaction with the child per week</label>
                                            <asp:TextBox ID="txt_mother_no_of_hours_intrest_child" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">If parents are devorced, living saperately or widowed, with whom is the child living?</label>
                                            <asp:TextBox ID="txt_if_parents_are_devorced" runat="server" class="form-control txtbx-style"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Questions : 
                                </h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">
                                                What are your reasons for choosing
                                                <asp:Label ID="lbl_school_name" runat="server"></asp:Label></label>
                                            <asp:TextBox ID="txt_what_reason_for_choosing" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="txtbx-wprs">
                                            <label for="validationCustom01" class="form-label">
                                                How did you learn about :
                                                <asp:Label ID="lbl_school_name1" runat="server"></asp:Label></label>
                                            <asp:TextBox ID="txt_how_did_learn_about_school" oninput="this.value = this.value.toUpperCase()" runat="server" class="form-control txtbx-style" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card" id="subjectsDV" runat="server" visible="false">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Subject Offered :</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <div class="txtbx-wprs">
                                            <table class="table table-striped table-bordered dataTable">
                                                <tr>
                                                    <th>#</th>
                                                    <th>Subject Name</th>
                                                    <th>Code No.</th>
                                                    <th></th>
                                                </tr>
                                                <asp:Repeater ID="rp_subjects" runat="server" OnItemDataBound="rp_subjects_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_subj_code" runat="server" Text='<%#Bind("Subject_Code")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_subj_id" runat="server" Visible="false" Text='<%#Bind("Subject_id")%>'></asp:Label>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
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
                                <asp:Button ID="btn_save" class="button-61" runat="server" Text="Submit" OnClick="btn_save_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" Visible="false" Style="background-color: #ee3f00;" class="button-61" />
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
                    $('#<%=ddl_par_state.ClientID %>').val($('#<%=ddl_temp_state.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_p_mobile_no.ClientID %>').val($('#<%=txt_temp_mobileno.ClientID %>').val());
                }

                if ($(this).is(":checked")) {
                    $('#<%=txt_p_email_id.ClientID %>').val($('#<%=txt_temp_email_id.ClientID %>').val());
                }
                if ($(this).is(":checked")) {
                    $('#<%=txt_p_emergancy_contact_no.ClientID %>').val($('#<%=txt_temp_emergency_contact_no.ClientID %>').val());
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
                $('#<%=ddl_par_state.ClientID %>').val($('#<%=ddl_temp_state.ClientID %>').val());
            }
            if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
            }
            if ($(this).is(":checked")) {
                $('#<%=txt_p_mobile_no.ClientID %>').val($('#<%=txt_temp_mobileno.ClientID %>').val());
            }

            if ($(this).is(":checked")) {
                $('#<%=txt_p_email_id.ClientID %>').val($('#<%=txt_temp_email_id.ClientID %>').val());
            }
            if ($(this).is(":checked")) {
                $('#<%=txt_p_emergancy_contact_no.ClientID %>').val($('#<%=txt_temp_emergency_contact_no.ClientID %>').val());
            }
        });
    </script>
</asp:Content>
