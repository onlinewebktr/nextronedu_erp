<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="fine-master.aspx.cs" Inherits="school_web.Admin.fine_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Fine Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .chkstle {
            margin-bottom: 5px;
        } 
        .form-label {
            margin: 10px 0px 3px;
            font-weight: 500;
            float: left;
            width: 100%;
        }

        .form-control {
            padding: 3px 6px 3px 6px;
        }

        .form-select {
            height: 32px;
            padding: 2px 10px;
            font-size: 14px;
        }
        input[type="checkbox"] {
  margin-right: 5px;
}
    </style>

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
                <div class="breadcrumb-title pe-3">Fine</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Fine Master</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Fine Master"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">
                                    <div class="col-md-12">
                                        <asp:RadioButton ID="rd_day" runat="server" class="chkstle" GroupName="a1" Text="Day Wise" AutoPostBack="true" OnCheckedChanged="rd_type_CheckedChanged" />
                                        <asp:RadioButton ID="rd_month" runat="server" class="chkstle" GroupName="a1" Text="Month Wise" AutoPostBack="true" OnCheckedChanged="rd_type_CheckedChanged" />
                                        <asp:RadioButton ID="rd_quater" runat="server" class="chkstle" GroupName="a1" Text="Quarter Wise" AutoPostBack="true" OnCheckedChanged="rd_type_CheckedChanged" />
                                        <asp:RadioButton ID="rd_day_range" runat="server" class="chkstle" GroupName="a1" Text="Day Range Wise" AutoPostBack="true" OnCheckedChanged="rd_type_CheckedChanged" />
                                        <asp:RadioButton ID="rd_next_month" Style="display: none" runat="server" class="chkstle" GroupName="a1" Text="Next Month Wise" AutoPostBack="true" OnCheckedChanged="rd_type_CheckedChanged" />
                                    </div>


                                    <asp:Panel ID="pnl_day_and_month" runat="server" Visible="false" Style="margin: 0px">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Last day of month to deposit fees<sup>*</sup></label>
                                                <asp:TextBox ID="txt_last_day_to_deposit_fees" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label" runat="server" id="lbl_fine_day_mnth">Fine Amount Per Day<sup>*</sup></label>
                                                <asp:TextBox ID="txt_fine_amt" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Status<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_staus" runat="server" class="form-select">
                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                    <asp:ListItem Value="0">De-Active</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Applicable from month<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_applicable_from_month" runat="server" class="form-select"></asp:DropDownList>
                                            </div>


                                             <div class="col-md-4" style="margin-top: 20px;">
                                                
                                                 <asp:CheckBox ID="chk_isnextmonth_fine_apply" Visible="false" runat="server" Text="Will a fine be imposed next month?" />
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl_quarter" runat="server" Style="margin: 0px">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Select Quarter<sup>*</sup>  <a href="quater-master.aspx">(Add Quarter)</a></label>
                                                <asp:DropDownList ID="ddl_quarter" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_quarter_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Month From<sup>*</sup></label>
                                                <asp:TextBox ID="txt_month_from" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Month To<sup>*</sup></label>
                                                <asp:TextBox ID="txt_month_to" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-5">
                                                <label for="validationCustom01" class="form-label">Last day of month to deposit fees<sup>*</sup></label>
                                                <asp:TextBox ID="txt_q_last_day_to_deposit" runat="server" class="form-control"></asp:TextBox>
                                            </div>


                                            <div class="col-md-12">
                                                <label for="validationCustom01" class="form-label">Fine Apply<sup>*</sup></label>
                                                <asp:RadioButton ID="rd_q_pay_mode_dw" runat="server" class="chkstle" GroupName="ab1" Text="Day Wise" AutoPostBack="true" OnCheckedChanged="rd_q_pay_mode_dw_CheckedChanged" />
                                                <asp:RadioButton ID="rd_q_pay_mode_qw" runat="server" class="chkstle" GroupName="ab1" Text="Quarter Wise" AutoPostBack="true" OnCheckedChanged="rd_q_pay_mode_dw_CheckedChanged" />
                                            </div>

                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label" runat="server" id="Label1">
                                                    Fine Amount Per
                                                <asp:Label ID="lbl_fine_mode" runat="server" Text=""></asp:Label><sup>*</sup></label>
                                                <asp:TextBox ID="txt_q_fine_amt" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>


                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Is Applicable<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_q_staus" runat="server" class="form-select">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </asp:Panel>



                                    <asp:Panel ID="pnl_day_range_wise" runat="server" Visible="false" Style="margin: 0px">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Description<sup>*</sup></label>
                                                <asp:TextBox ID="txt_descriptions" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label" runat="server" id="Label3">No. of Day<sup>*</sup></label>
                                                <asp:TextBox ID="txt_no_of_daye" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label" runat="server" id="Label5">Fine Amount<sup>*</sup></label>
                                                <asp:TextBox ID="txt_fine_amount" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </asp:Panel>




                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Fine</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Panel ID="pnl_qtr_grid" runat="server" Style="margin: 0px">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Quarter No.</th>
                                                            <th>Quarter Start Month</th>
                                                            <th>Quarter End Month</th>
                                                            <th>Last day to deposit fees</th>
                                                            <th>Fine Amount</th>
                                                            <th>Is Applicable</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Quater_no" runat="server" Text='<%#Bind("Quater_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("Start_month")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("End_month")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_last_day" runat="server" Text='<%#Bind("Last_day_to_deposit_fees")%>'></asp:Label>,
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("Start_month")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_fine_amt" runat="server" Text='<%#Bind("Fine_amt_per_day_or_month")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_show_status" runat="server"></asp:Label>
                                                                        </td>
                                                                         <td style="text-align: left;">
                                                                             <asp:Label ID="lbl_dis_apply" runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_Is_next_month_fine_calculate" Visible="false"  Text='<%#Bind("Is_next_month_fine_calculate")%>' runat="server"></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_start_month_id" runat="server" Text='<%#Bind("Q_start_month")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_end_month_id" runat="server" Text='<%#Bind("Q_end_month")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_quarter_id" runat="server" Text='<%#Bind("Quater_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_q_payment_mode" runat="server" Text='<%#Bind("Q_payment_mode")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnl_day_mnth" runat="server" Style="margin: 0px">
                                                <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Last day to deposit fees</th>
                                                            <th>Fine Amount</th>
                                                            <th>Applicable From Month</th>
                                                            <th>Is Applicable</th>
                                                            <th>Is Next Month Fee Apply</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rp_dm" runat="server" OnItemDataBound="rp_dm_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_last_day_to_deposit_fees" runat="server" Text='<%#Bind("Last_day_to_deposit_fees")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_fine_amt_per_day_or_month" runat="server" Text='<%#Bind("Fine_amt_per_day_or_month")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_applicable_from_month_or_quater" runat="server" Text='<%#Bind("Applicable_from_month_or_quater")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_show_status" runat="server"></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                             <asp:Label ID="lbl_dis_apply" runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_Is_next_month_fine_calculate" Visible="false"  Text='<%#Bind("Is_next_month_fine_calculate")%>' runat="server"></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnkEdit_dm" runat="server" CausesValidation="false" OnClick="lnkEdit_dm_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel_dm" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_applicable_from_month_or_quater_id" runat="server" Text='<%#Bind("Applicable_from_month_or_quater_id")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>


                                            <asp:Panel ID="pnl_day_range" runat="server" Style="margin: 0px">
                                                <table id="Table2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Description</th>
                                                            <th>No. of Day</th>
                                                            <th>Fine Amount</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                            <ItemTemplate>
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_description" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_no_of_day" runat="server" Text='<%#Bind("No_of_day")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_fine_amount" runat="server" Text='<%#Bind("Fine_amount")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnkEdit_dy_r" runat="server" CausesValidation="false" OnClick="lnkEdit_dy_r_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel_dy_r" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_dy_r_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
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
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
