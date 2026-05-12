<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="ENACH-Registration.aspx.cs" Inherits="school_web.Admin.ENACH_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Enach Registartion
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#<%=txt_date_new.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "2025:2100",
                minDate: 0 // 👈 This disables past dates
            }).attr("readonly", "true");
        });


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }


        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'bill-delete.aspx/GetRooPathAdmNo',
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
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Enach Registartion</li>
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
                                    <div class="col-xl-7">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Session</label>
                                                            <asp:DropDownList ID="ddl_session" runat="server" AutoPostBack="true" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xl-5">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Admission No.</label>
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xl-3">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" Style="margin: 22px 0px 0px 0px;" OnClick="btn_find_admission_no_Click" />
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
                                                                        <asp:Label ID="lbl_father_name" runat="server" class="stdnt-info-fnds " Font-Bold="true"></asp:Label>
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
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_old_roll_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
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
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Mobile No. : </label>
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


                                        <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnldate" runat="server" visible="false">
                                            <table style="padding: 0px; height: 43px; width: 90%; text-align: center; margin: 0px auto; float: none;">
                                                <tr>
                                                    <td>Valid Up To date
                                                    </td>
                                                    <td>
                                                        <div class="clndr-div">
                                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                            <asp:TextBox ID="txt_date_new" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td>Max Debit Amount
                                                    </td>
                                                    <td>
                                                        <div class="clndr-div">

                                                            <asp:TextBox ID="txt_Mandate_amount" onkeypress="return isNumberKey(event)" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_update_final" runat="server" Text="Register for NPCI eNACH" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_update_final_Click" />
                                                    </td>
                                                </tr>
                                            </table>
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




</asp:Content>
