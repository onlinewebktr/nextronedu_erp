<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="bulk-fee-update-adm-mnth.aspx.cs" Inherits="school_web.Admin.bulk_fee_update_adm_mnth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Fee via Excel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit final?")) {
                confirm_value.value = "Yes";
                if (!isSubmitted) {
                    $('#<%=btn_final_submit.ClientID %>').val('Submitting.. Please Wait..');
                    isSubmitted = true;

                }
                else {

                    alert("Please Wait.. due to process is running");
                    // return false;
                }
            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }
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
                <div class="breadcrumb-title pe-3">Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Upload Fee via Excel</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="row">
                        <div class="col-xl-8">
                            <div class="card">
                                <div class="card-body">
                                    <h6 class="mb-0 text-uppercase">Upload Fee via Excel</h6>
                                    <hr />
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Choose Excel(.csv file)<sup>*</sup></label>
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                            </div>

                                            <div class="col-4">
                                                <asp:Button ID="btn_Submit" OnClick="btn_Submit_Click" runat="server" Text="Upload" CssClass="btn btn-primary" ValidationGroup="a" Style="margin: 24px 0px 0px 0px; padding: 4px 10px;" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4">
                            <div class="card">
                                <div class="card-body">
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-md-12">
                                                <a href="doc/adm-monthly-fee-payment.csv" download="" class="btnbtn btn-1 btn-sep icon-cart">Download Excel</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div> 
                    </div>
                </div>
                <asp:TextBox ID="txt_adm_ann_fee" Visible="false" runat="server"></asp:TextBox>
                <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                    <div class="col-xl-12">
                        <h6 class="mb-0 text-uppercase">Uploaded Fee</h6>
                        <hr />
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbl_total1" runat="server"></asp:Label>
                                                <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>

                                                <asp:Panel ID="pnlPaymentS" Visible="false" runat="server">
                                                    <asp:Repeater ID="rd_months" runat="server">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_month" Text='<%#Eval("Month") %>' Checked="true" runat="server" />
                                                            <asp:Label ID="lbl_Month" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:TextBox ID="txttotal" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_paid_prev" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_discount" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txtfineamount" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_other_fee" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txttotalbill" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hd_paybaleamount" runat="server" />
                                                    <asp:HiddenField ID="hd_adjustamount" runat="server" />
                                                    <asp:HiddenField ID="hd_totalamount" runat="server" />
                                                    <asp:HiddenField ID="hd_total_discount" runat="server" />
                                                    <asp:HiddenField ID="hd_overall_bill" runat="server" />
                                                    <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="false">
                                                        <table style="width: 100%;" class="table table-hover table-bordered">
                                                            <tr>
                                                                <th></th>
                                                                <th>Month</th>
                                                                <th>Fees Head</th>
                                                                <th>Amount</th>
                                                                <th>Discount</th>
                                                                <th>Paid Prev.</th>
                                                                <th>Payable</th>
                                                            </tr>
                                                            <asp:Repeater ID="rp_fee_details" runat="server">
                                                                <ItemTemplate>
                                                                    <tr id="row" runat="server">
                                                                        <td>
                                                                            <asp:CheckBox class="box" Checked="true" ID="chk_get_fee" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                            <asp:Label ID="lblcontent_id" Visible="false" runat="server" Text='<%#Bind("content_id") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount","{0:n}") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                            <tr>
                                                                <th colspan="3">Total :
                                                                </th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_fee_amount" runat="server" Text=""></asp:Label></th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_discount" runat="server" Text=""></asp:Label></th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_paid_prev" runat="server" Text=""></asp:Label></th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></th>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </asp:Panel>

                                                <div class="col-4">
                                                    <asp:Button ID="btn_final_submit" OnClick="btn_final_submit_Click" OnClientClick="Confirm()" runat="server" Text="Final Submit" CssClass="btn btn-primary" Style="margin: 0px 0px 0px 0px; padding: 6px 10px;" />
                                                </div>
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
</asp:Content>
