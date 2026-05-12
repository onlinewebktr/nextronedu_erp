<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="update-admission-fee.aspx.cs" Inherits="school_web.Admin.update_admission_fee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update Admission Fee
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            font-weight: 500;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 25px;
            height: 25px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_student.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'update-admission-fee.aspx/GetRooPath',
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
                        url: 'update-admission-fee.aspx/GetRooPathAdmNo',
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
                <div class="breadcrumb-title pe-3">Student Update</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Update Admission Fee</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
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
                                                                    <label for="validationCustom01" class="form-label-fnds">Class</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Section</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:TextBox ID="txt_section" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
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
                                                                <div class="col-xl-7 padd-lft-5">
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
                                            <div class="fnd-box-wpr" style="margin: 0px 0px 10px 0px;">
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
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" Font-Bold="true" Text=" " class="stdnt-info-fnds"></asp:Label>
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
                                                                        <asp:Label ID="txtsection" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
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
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Type : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_studentype" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
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
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblhostel" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Category : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_catogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Sub-Category : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_subcatogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnl_fee_details" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card">
                                                    <div class="card-body">
                                                        <div class="p-4 border rounded">
                                                            <div class="row g-3 needs-validation" novalidate="">

                                                                <table class="table table-striped table-bordered" style="padding: 0px">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Fee Head</th>
                                                                            <th>Fees Amount</th>
                                                                            <th>Discount</th>
                                                                            <th>Paid Perviously</th>
                                                                            <th>Paybale</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rd_fees" runat="server" OnItemDataBound="rd_fees_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("feetype")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_content_id" Visible="false" runat="server" Text='<%#Bind("content_id")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txt_payble" runat="server" class="form-control" onkeypress="return isNumberKey(event)" Style="width: 120px;" Text='<%#Bind("payable")%>' AutoPostBack="true" OnTextChanged="lbl_payble_TextChanged"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txt_disc_amount" runat="server" class="form-control" onkeypress="return isNumberKey(event)" Style="width: 120px;" Text='<%#Bind("disc_amount")%>' AutoPostBack="true" OnTextChanged="lbl_payble_TextChanged"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_paid_amt" runat="server" Text='<%#Bind("paid")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_net_payble" runat="server" Text='<%#Bind("net_payable")%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                    <tr>
                                                                        <td colspan="2" style="text-align: right"><b>Total :</b></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_payble" runat="server"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_disc" runat="server"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_paid_amt" runat="server"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_total_net_payble" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                </table>

                                                                <div class="row">
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds" style="font-size: 14px;">Remark<sup>*</sup> : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:TextBox ID="txt_remarks" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-xl-2 padd-rght-5"></div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Button ID="btn_Submit" runat="server" OnClientClick="return confirm('Are you sure you want to update?');" CausesValidation="false" Style="width: auto; padding: 6px 15px; margin: 15px 0px 0px 0px;"
                                                                            Text="Update" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--end row-->

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
    </style>
    <asp:HiddenField ID="hd_user_Type" runat="server" />



</asp:Content>
