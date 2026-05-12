<%@ Page Title="" Language="C#" MasterPageFile="~/Monthly_Payments/main.Master" AutoEventWireup="true" CodeBehind="Monthly_payment_student.aspx.cs" Inherits="school_web.Monthly_Payments.Monthly_payment_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Fees Collection Monthly Wise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            font-weight: 500;
        }
        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 30px;
            height: 30px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>

    <script type="text/javascript">


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
                <div class="breadcrumb-title pe-3">Fees Collections</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Monthly Fees Payment</li>
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
                                                <div class="fnd-box-row-wpr" style="display: block">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Father Mobile</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                              <asp:TextBox ID="txt_fathermobileno" runat="server" class="form-control find-dv-txtbx" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx" Visible="false"></asp:DropDownList>
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
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Catogery : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_catogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Sub Catogery : </label>
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
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Student Paymnet History</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-12 padd-rght-5">
                                                                <asp:Label ID="lbl_msg" runat="server" ForeColor="Maroon" Font-Bold="true"  ></asp:Label>
                                                                <asp:GridView ID="grid_payment_history" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;" ShowFooter="True" OnRowDataBound="grid_payment_history_RowDataBound">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>

                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Slip No.">
                                                                            <ItemTemplate>

                                                                                <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>

                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Decription">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description") %>'></asp:Label>

                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbl_totaldiscount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Payment Mode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_paymentmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>

                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>



                                                                    </Columns>
                                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                    <RowStyle ForeColor="#000066" />
                                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                                </asp:GridView>




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
                                                <h2 class="fnd-box-row-wpr-h">Select Month 
                                                    <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-primary form-fnd-btns" Style="background: #bfbfbf; border: 1px solid #ddd; width: auto; margin: -3px 0px -2px 0px; padding: 2px 10px; float: right;"
                                                        OnClick="btn_reset_Click" /></h2>
                                                <div class="fnd-box-wpr-inr">

                                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Month">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_month" OnCheckedChanged="chk_month_CheckedChanged" AutoPostBack="true" runat="server" Enabled="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-6 pdd-both0">
                                            <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h">MonthWise Fees Details</h2>


                                                <div class="fnd-box-wpr-inr">
                                                    <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="false">


                                                        <table style="width: 100%;" class="table table-hover table-bordered">

                                                            <tr>
                                                                <th>Month</th>
                                                                <th>Fees Head</th>
                                                                <th>Fees Amount</th>
                                                                <th>Discount</th>
                                                                <th>Paid Previously</th>
                                                                <th>Payable</th>
                                                            </tr>


                                                            <asp:Repeater ID="rp_fee_details" runat="server" OnItemDataBound="rp_fee_details_ItemDataBound">
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
                                                <h2 class="fnd-box-row-wpr-h">Billing Details   </h2>
                                                <div class="fnd-box-wpr-inr">


                                                    <div class="fnd-box-row-wpr" style="display: none">

                                                        <div class="col-md-12" style="background: #62f415; padding: 3px 4px 4px 6px;">
                                                            <asp:CheckBox ID="chk_latefineapplay" runat="server" Text="Is Late Fine Applied" Checked="true" AutoPostBack="true" OnCheckedChanged="chk_latefineapplay_CheckedChanged" />
                                                        </div>

                                                    </div>


                                                    <div class="fnd-box-row-wpr" style="display: none">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <asp:CheckBox ID="chk_delete_slip" runat="server" Text="Is Deleted Bill Re-entry" AutoPostBack="true" OnCheckedChanged="chk_delete_slip_CheckedChanged" />

                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="fnd-box-row-wpr" id="oldslip_no" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Slip No.</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_old_slip_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>






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
                                                                <asp:TextBox ID="txtfineamount" runat="server" class="form-control find-dv-txtbx" Text="0" ReadOnly="true" AutoPostBack="true" OnTextChanged="txtfineamount_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label for="validationCustom01" class="form-label-fnds">Other Fee</label>
                                                            </div>
                                                            <div class="col-md-6 padd-lft0">
                                                                <asp:TextBox ID="txt_other_fee" runat="server" class="form-control find-dv-txtbx" Text="0" OnTextChanged="txt_other_fee_TextChanged" ReadOnly="true"></asp:TextBox>
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
                                                                <asp:TextBox ID="txt_paid_amount" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_paid_amount_TextChanged" ReadOnly="true"></asp:TextBox>
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
                                                                <asp:TextBox ID="txt_payment_date" runat="server" class="form-control find-dv-txtbx" OnTextChanged="txt_payment_date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <script>
                                                                    $(function () {
                                                                        $("#<%=txt_payment_date.ClientID %>").datepicker({
                                                                            dateFormat: "dd/mm/yy",
                                                                            changeMonth: true,
                                                                            changeYear: true,
                                                                            yearRange: "2021:2023",
                                                                            minDate: "-150d",
                                                                            maxDate: '0',
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
                                                                <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select find-dv-txtbx">
                                                                    <asp:ListItem>Online</asp:ListItem>

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
                                                                <asp:Button ID="btn_save_payment" runat="server" Text="Pay" Visible="false" OnClientClick="return confirm('Are you sure you want to pay ?');" OnClick="btn_save_payment_Click" CssClass="btn btn-primary" Style="width: 100%;" />
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
