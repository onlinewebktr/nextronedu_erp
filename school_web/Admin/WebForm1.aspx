<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="school_web.Admin.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
 
    <style>
        th {
            font-weight: 500;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPath',
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

        $(function () {
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPathAdmNo',
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="breadcrumb-title pe-3">Fee Collection</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Monthly Fee Payment</li>
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
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <div class="fnd-box-row-wpr">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <div class="fnd-box-row-wpr">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Button ID="btnfind" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns"  />
                                                                    </div>
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
                                                            <asp:Button ID="btn_find_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns"  />
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
                                                                        <asp:Label ID="lbl_name" runat="server" Text="Md Wahab" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" Text="Md Wahab" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" Text="Class II" class="stdnt-info-fnds"></asp:Label>
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
                                                                        <asp:Label ID="txtsection" runat="server" Text="A" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Text="55475" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblhostel" runat="server" Text="No" class="stdnt-info-fnds"></asp:Label>
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
                                                                        <asp:Label ID="lbltransporttion" runat="server" Text="No" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_phone" runat="server" Text="7250408680" class="stdnt-info-fnds"></asp:Label>
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
                                        <div class="col-xl-3">
                                            <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h">Select Month</h2>
                                                <div class="fnd-box-wpr-inr">

                                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Month">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_month"  AutoPostBack="true" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-6 pdd-both0">
                                            <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h">MonthWise Fee Details</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <asp:Panel ID="pnl_month_wise_fee_details" runat="server">


                                                        <table style="width: 100%;" id="example" class="table table-hover table-bordered">

                                                            <tr>
                                                                <th>Month</th>
                                                                <th>Fee Head</th>
                                                                <th>Fee Amount</th>
                                                                <th>Discount</th>
                                                                <th>Paid Previously</th>
                                                                <th>Payable</th>
                                                            </tr>


                                                            <asp:Repeater ID="rp_fee_details" runat="server" >
                                                                <ItemTemplate>
                                                                    <tr id="row" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                            <tr>
                                                                <th colspan="2">Total :
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lbl_fee_amount" runat="server" Text=""></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lbl_discount" runat="server" Text=""></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lbl_paid_prev" runat="server" Text=""></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></th>

                                                            </tr>
                                                        </table>

                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-xl-3">
                                            <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h">Billing Details</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Total Amount</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txttotal" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Paid Previously</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_paid_prev" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Discount</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_discount" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Late Fine</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txtfineamount" runat="server" class="form-control find-dv-txtbx" Text="0" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Payble Bill</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txttotalbill" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Paid Amount</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_paid_amount" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Total Dues</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_total_dues" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Remark</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_description" runat="server" TextMode="MultiLine" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Payment Date</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_payment_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                <script>
                                                                    $(function () {
                                                                        $("#<%=txt_payment_date.ClientID %>").datepicker({
                                                                            dateFormat: "dd/mm/yy",
                                                                            changeMonth: true,
                                                                            changeYear: true,
                                                                            yearRange: "1900:2100",
                                                                        }).attr("readonly", "true");
                                                                    });
                                                                </script>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Payment Mode</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:DropDownList ID="ddl_paymentmode" runat="server"  AutoPostBack="true" class="form-select find-dv-txtbx">
                                                                    <asp:ListItem>Cash</asp:ListItem>
                                                                    <asp:ListItem>Netbanking</asp:ListItem>
                                                                    <asp:ListItem>Deposited In Bank</asp:ListItem>
                                                                    <asp:ListItem>Sbdebit</asp:ListItem>
                                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                                    <asp:ListItem>NEFT</asp:ListItem>
                                                                    <asp:ListItem>Debitcard</asp:ListItem>
                                                                    <asp:ListItem>Creditcard</asp:ListItem>
                                                                    <asp:ListItem>Otherdcard</asp:ListItem>
                                                                    <asp:ListItem>UPI</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="fnd-box-row-wpr" id="pnl_mode_t_nS" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label-fnds" Text="Transaction No."></asp:Label>

                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_trans_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:Button ID="btn_save_payment" runat="server" Text="Pay" OnClientClick="return confirm('Are you sure you want to pay ?');"  CssClass="btn btn-primary" Style="width: 100%;" />
                                                            </div>
                                                        </div>
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
    <!--end row-->

     
     
</asp:Content>
