<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Upload_Mothley_Fee_Using_CSV.aspx.cs" Inherits="school_web.Admin.Upload_Mothley_Fee_Using_CSV"  EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Bulk Monthly Fee taken Using CSV File
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
                            <li class="breadcrumb-item active" aria-current="page">Upload Monthly Fee Using CSV</li>
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
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation" novalidate="">

                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Browse Excel(.csv file)<sup>*</sup></label>
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                            </div>

                                            <div class="col-4">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Upload" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Style="margin: 24px 0px 0px 0px; padding: 4px 10px;" />
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
                                            <div class="col-sm-12">
                                                <label for="validationCustom01" class="find-dv-lbl">Download With Student</label>
                                                <asp:DropDownList ID="ddl_download_with_fee" runat="server" class="form-select find-dv-txtbx">
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                            <div class="col-sm-6" id="sessinDV">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddl_excel_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6" id="classDV">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:DropDownList ID="ddl_excel_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                        </div>


                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-md-12" id="exclempty" style="display: none">
                                                <a href="doc/Monthly-fee-payment-format.csv" download="" class="btnbtn btn-1 btn-sep icon-cart">Download Excel Format</a>
                                            </div>

                                            <div class="col-md-12" id="exclWithData" style="display: none">
                                                <asp:LinkButton ID="lnk_download_excel" OnClick="lnk_download_excel_Click" class="btnbtn btn-1 btn-sep icon-cart" runat="server">Download Excel Format</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                    <div class="col-xl-12">
                        <h6 class="mb-0 text-uppercase">Uploaded Monthly Fee</h6>
                        <hr />
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbl_total1" runat="server" Text="Label"></asp:Label>
                                                <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>
                                                <asp:Panel ID="pnl_paymentprocess" runat="server" Visible="false">
                                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Month">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_month" runat="server" Checked="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                    <asp:TextBox ID="txttotal" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_paid_prev" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_discount" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txtfineamount" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_other_fee" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txttotalbill" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_paid_amount" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txt_total_dues" runat="server"></asp:TextBox>

                                                    <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="true">
                                                        <table style="width: 100%;" class="table table-hover table-bordered">
                                                            <tr>
                                                                <th>Month</th>
                                                                <th>Fees Head</th>
                                                                <th>Fees Amount</th>
                                                                <th>Discount</th>
                                                                <th>Paid Previously</th>
                                                                <th>Payable</th>
                                                            </tr>
                                                            <asp:Repeater ID="rp_fee_details" runat="server">
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
                                                </asp:Panel>
                                                <div class="col-4">
                                                    <asp:CheckBox ID="chk_latefineapplay" Visible="false" runat="server" Text="Is Late Fine Applied" Checked="true" />
                                                    <asp:Button ID="btn_final_submit" OnClientClick="Confirm()" runat="server" Text="Final Submit" CssClass="btn btn-primary" OnClick="btn_final_submit_Click" Style="margin: 0px 0px 0px 0px; padding: 6px 10px;" />
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

    <script type="text/javascript">
        $(document).ready(function () {
            on_selection();
            $("#<%=ddl_download_with_fee.ClientID%>").on('change', function () {
                on_selection();
            })
        });

        function on_selection() {
            if ($('#<%= ddl_download_with_fee.ClientID %> option:selected').val() == "Yes") {
                $("#sessinDV").show();
                $("#classDV").show();
                $("#exclempty").hide();
                $("#exclWithData").show();
            }
            else {
                $("#sessinDV").hide();
                $("#classDV").hide();
                $("#exclempty").show();
                $("#exclWithData").hide();
            }
        }
    </script>
</asp:Content>
