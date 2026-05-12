<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="set-student-wise-discount.aspx.cs" Inherits="school_web.Admin.set_student_wise_discount" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set Student Wise Discount
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_student.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'set-student-wise-discount.aspx/GetRooPath',
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
                        url: 'set-student-wise-discount.aspx/GetRooPathAdmNo',
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


        function openModalSTD() {
            $('#myModalStd').modal('show');
        }
    </script>


    <style type="text/css">
        .modal.fade .modal-dialog {
            transform: translate(0, 0px);
        }

        .modal-open .modal {
            background: rgb(0 0 0 / 54%);
        }


        .stdinfo-lft {
            width: 87%;
        }

        .stdinfo-rght {
            width: 13%;
        }

        .stdinfo-rght-imgwpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            height: 131px;
            overflow: hidden;
            border: 2px dashed #FFBA5F;
            border-radius: 2px;
        }

            .stdinfo-rght-imgwpr img {
                width: 100%;
            }

        .fnd-box-wpr {
            margin: 0px 0px 5px 0px;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 19px;
            height: 19px;
            position: relative;
            top: 5.4px;
            left: 0px;
            margin: 0px 5px 0px 0px;
            z-index: 9;
        }

        label {
            font-size: 14px;
        }

        .fnd-box-row-wpr-h {
            background: #f5f3f3;
        }
         

        .disc-pop-save_disc {
            border: 1px solid #0808b4;
            background: #0d31e0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_fee_group" runat="server" />
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
                <div class="breadcrumb-title pe-3">Fees Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Discount Setup</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn">
                        <li><a href="set-discount-on-admission-fee.aspx">Admission Fees</a></li>
                        <li><a href="set-discount-on-annual-fee.aspx">Annual Fees</a></li>
                        <li><a href="set-discount-on-monthly-fee.aspx">Monthly Fees</a></li>
                        <li><a href="set-discount-on-bus-fees.aspx">Bus Fees</a></li>
                        <li><a href="Set_Student_Wise_Discount_type_head.aspx">Discount Mode</a></li>
                        <li><a href="set-student-wise-discount.aspx" class="sub-mnu-p-a-active">Student Wise</a></li>   
                        <li><a href="set-student-wise-discount-s-month.aspx">Month Wise</a></li>
                    </ul>
                </div>
                <hr />
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row" id="fndSectionDV" runat="server">
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
                                                            <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
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
                                                        <div class="col-xl-3 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:Button ID="btn_find_admission_no" OnClick="btn_find_admission_no_Click" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" />
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
                                                            <asp:DropDownList ID="ddl_session_student" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
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
                                                            <asp:Button ID="btn_find_name" OnClick="btn_find_name_Click" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">
                                    <asp:HiddenField ID="hd_admission_no" runat="server" />
                                    <div class="row">
                                        <div class="col-xl-12">

                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Student Details <a href="set-student-wise-discount.aspx" class="btn btn-primary form-fnd-btns" style="background: #f00; border: 1px solid #ddd; width: auto; margin: -3px 0px -2px 0px; padding: 3px 10px 2px; float: right;">Find New Student</a></h2>
                                                <div class="fnd-box-wpr-inr">

                                                    <div class="fnd-box-row-wpr stdinfo-lft">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Type : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="lbl_studentype" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
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
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lblclass_show" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                        <asp:Label ID="lblclass" Visible="false" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="txtroll_no" runat="server" Font-Bold="true" Text=" " class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transport : </label>
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
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="lblhostel" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Disc. Group : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_catogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Disc. Sub-Group : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_subcatogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="transport_data" runat="server" visible="false">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Start Month : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_start_month" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Boarding Point :  </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_boarding_point" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Seat No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="lbl_seatno" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-12 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transport Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-10 padd-lft-5">
                                                                        <asp:Label ID="lbl_transportname" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-12 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transport Route : </label>
                                                                    </div>
                                                                    <div class="col-xl-10 padd-lft-5">
                                                                        <asp:Label ID="lbl_transport_Route" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div> 
                                                    </div>
                                                    <div class="fnd-box-row-wpr stdinfo-rght">
                                                        <div class="stdinfo-rght-imgwpr">
                                                            <asp:Image ID="Image1" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Give Discount 
                                                    <asp:CheckBox ID="chk_is_admission_disc" onclick="ShowHideDivAdm(this)" Text="Discount on Admission" runat="server" /></h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div id="monthFeeDiscHead">
                                                        <div class="disc-month-checkbox-sec">
                                                            <asp:Repeater ID="rp_month_for_discount" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_month_discount" Text='<%#Eval("Month") %>' runat="server" />
                                                                    <asp:Label ID="lbl_Month_discount" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_month_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                        <div class="disc-pop-tbl-wpr">
                                                            <table class="table table-striped table-bordered dataTable" style="border-bottom: 1px solid #adadad; border-left: 1px solid #adadad; border-top: 1px solid #adadad; margin-bottom: 0px !important;">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Fees Head</th>
                                                                        <th>Fees Amount</th>
                                                                        <th>Disc. Amount</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_discount_fee_head" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td class="noclick">
                                                                                    <asp:TextBox ID="txt_head_fee" class="noclick" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_disc_fee" runat="server" Text='<%#Eval("discount") %>' Style="width: 80px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td style="text-align: right; font-weight: 700;">TOTAL</td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_head_fee_for_head" Style="font-weight: bold;" runat="server"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_disc" runat="server" Style="font-weight: bold;"></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>

                                                    <div id="admissionFeeDiscHead">
                                                        <div class="disc-pop-tbl-wpr">
                                                            <table class="table table-striped table-bordered dataTable" style="border-bottom: 1px solid #adadad; border-left: 1px solid #adadad; border-top: 1px solid #adadad; margin-bottom: 0px !important; margin-top: 0px !important;">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Fees Head</th>
                                                                        <th>Fees Amount</th>
                                                                        <th>Disc. Amount</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rp_fee_head_admission" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_adm_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_adm_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td class="noclick">
                                                                                    <asp:TextBox ID="txt_adm_head_fee" class="noclick" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_adm_disc_fee" runat="server" Text='<%#Eval("discount") %>' Style="width: 80px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td style="text-align: right; font-weight: 700;">TOTAL</td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_head_fee_for_head_adm" Style="font-weight: bold;" runat="server"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_disc_adm" runat="server" Style="font-weight: bold;"></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="disc-pop-txtbx-sec">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <p>Discount Mode</p>
                                                                <asp:DropDownList ID="ddl_discount_mode" runat="server" class="form-select disc-pop-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <p>Remark</p>
                                                                <asp:TextBox ID="txt_discount_Remarks" runat="server" class="form-control disc-pop-txtbx"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <asp:Button ID="btn_save_discount" runat="server" class="button-6161 disc-pop-save_disc" Text="Save" OnClick="btn_save_discount_Click" />
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="disc-pop-grid-sec" id="discgridDV" runat="server">
                                                        <h2>Discount History</h2>
                                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Discount on</th>
                                                                    <th>Month</th>
                                                                    <th>Fees Head</th>
                                                                    <th>Fees Amount</th>
                                                                    <th>Disc. Amount</th>
                                                                    <th>After Disc.</th>
                                                                    <th>Discount Mode</th>
                                                                    <th>Date</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_discount" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_discount_on" runat="server" Text='<%#Bind("Discount_on")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("month")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_disc_amount" runat="server" Text='<%#Bind("disc_amount")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_after_disc" runat="server" Text='<%#Bind("after_disc")%>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_lbl_Student_Discunt_Type" runat="server" Text='<%#Bind("Discunt_Type")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label10" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" OnClick="lnkDel_Click" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("Admission_no")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="myModalStd" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 900px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header" style="padding: 5px 15px;">
                    <h3 class="modal-title" style="font-size: 20px;">Student Details</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th></th>
                                <th>Session</th>
                                <th>Student Name</th>
                                <th>Father Name</th>
                                <th>Adm. No.</th>
                                <th>Class</th>
                                <th>Section</th>
                                <th>Roll No.</th>
                                <th>Mobile No</th>
                                <th>DOB</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rd_view" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnk_select" OnClick="lnk_select_Click" class="button-61 nowordbreak collect-feesss" runat="server">Select</asp:LinkButton>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_mobile2" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_dob" runat="server" Text='<%#Bind("dob")%>'></asp:Label>
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

    <script type="text/javascript">
        $(document).ready(function () {

            CalculateTotalFee();
            $("[Id*=txt_head_fee]").on("keyup", function () {
                CalculateTotalFee();
            });

            CalculateTotal();
            $("[Id*=txt_disc_fee]").on("keyup", function () {
                CalculateTotal();
            });

            CalculateTotalAdm();
            $("[Id*=txt_adm_disc_fee]").on("keyup", function () {
                CalculateTotalAdm();
            });

            CalculateTotalFeeAdm();
            $("[Id*=txt_adm_head_fee]").on("keyup", function () {
                CalculateTotalFeeAdm();
            });


            var chkPassport = $("#<%=chk_is_admission_disc.ClientID %>");
            ShowHideDivAdm(chkPassport);
        });


        function CalculateTotalFee() {
            var total = 0;
            $("[Id*=txt_head_fee").each(function () {
                total += parseFloat($(this).val());
            });
            $("#<%=lbl_total_head_fee_for_head.ClientID%>").text(total);
        }


        function ShowHideDivAdm(chkPassport) {
            if ($(chkPassport).prop('checked') == true) {
                $("#monthFeeDiscHead").hide();
                $("#admissionFeeDiscHead").show();
            }
            else {
                $("#monthFeeDiscHead").show();
                $("#admissionFeeDiscHead").hide();
            }
        }

        function CalculateTotalFeeAdm() {
            var total = 0;
            $("[Id*=txt_adm_head_fee").each(function () {
                total += parseFloat($(this).val());
            });
            $("#<%=lbl_total_head_fee_for_head_adm.ClientID%>").text(total);
        }

        function CalculateTotal() {
            var total = 0; var totalFee = 0;
            $("[Id*=txt_disc_fee").each(function () {
                total += parseFloat($(this).val());

                $("[Id*=txt_disc_fee").focus(function () { $(this).select(); });
            });

            $("[Id*=txt_head_fee").each(function () {
                totalFee = parseFloat($(this).val());
            });
            $("#<%=lbl_total_disc.ClientID%>").text(total);


            var ttlfeeheads = parseFloat($("#<%=lbl_total_head_fee_for_head.ClientID%>").text());
            var ttlfeedisc = parseFloat($("#<%=lbl_total_disc.ClientID%>").text());
            if (ttlfeedisc > ttlfeeheads) {
                $("#<%=lbl_total_disc.ClientID%>").text(ttlfeeheads);
            }
        }



        //==========
        function CalculateTotalAdm() {
            var total = 0; var totalFee = 0;
            $("[Id*=txt_adm_disc_fee").each(function () {
                total += parseFloat($(this).val());

                $("[Id*=txt_adm_disc_fee").focus(function () { $(this).select(); });
            });

            $("[Id*=txt_adm_head_fee").each(function () {
                totalFee = parseFloat($(this).val());
            });
            $("#<%=lbl_total_disc_adm.ClientID%>").text(total);


            var ttlfeeheads = parseFloat($("#<%=lbl_total_head_fee_for_head_adm.ClientID%>").text());
            var ttlfeedisc = parseFloat($("#<%=lbl_total_disc_adm.ClientID%>").text());

            if (ttlfeedisc > ttlfeeheads) {
                $("#<%=lbl_total_disc_adm.ClientID%>").text(ttlfeeheads);
            }
        }
    </script>
</asp:Content>
