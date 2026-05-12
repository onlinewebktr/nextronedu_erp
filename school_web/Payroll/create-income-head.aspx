<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="create-income-head.aspx.cs" Inherits="school_web.Payroll.create_income_head" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Income Head
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Setup Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Income Head</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Income Head"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Select Grade<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_grade" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Income Head<sup>*</sup></label>
                                        <asp:TextBox ID="txt_income_head" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Income Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_income_type" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_income_type_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-5">
                                        <asp:Panel ID="pnl_is_basic" runat="server">
                                            <div class="row">
                                                <div class="col-md-5">
                                                    <label for="validationCustom01" class="form-label">Is Variable Head<sup>*</sup></label>
                                                    <br />
                                                    <asp:CheckBox ID="chk_variable_head" AutoPostBack="true" OnCheckedChanged="chk_variable_head_CheckedChanged" runat="server" />
                                                </div>


                                                <div class="col-md-7">
                                                    <asp:Panel ID="pnl_is_veriable" runat="server">
                                                        <div class="row">
                                                            <div class="col-xl-7">
                                                                <label for="validationCustom01" class="form-label">Income Formula<sup>*</sup></label>
                                                                <asp:DropDownList ID="ddl_based_on" runat="server" class="form-select"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-xl-5">
                                                                <label for="validationCustom01" class="form-label">Value<sup>*</sup></label>
                                                                <asp:TextBox ID="txt_formula" runat="server" class="form-control" onkeypress="return isNumberKey(event)" Style="float: left;"></asp:TextBox><span>%</span>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Applicable From<sup>*</sup></label>
                                        <asp:TextBox ID="txt_applicable_from" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">Calculation of following will be based on these income type<sup>*</sup></label>
                                        <asp:CheckBox ID="chk_pf" runat="server" Text="P.F." Style="margin: 0px 5px 0px 0px;" />
                                        <asp:CheckBox ID="chk_esi" runat="server" Text="ESI" Style="margin: 0px 5px 0px 0px;" />
                                        <asp:CheckBox ID="chk_ptax" runat="server" Text="P.Tax" Style="margin: 0px 5px 0px 0px;" />
                                        <asp:CheckBox ID="chk_ot" runat="server" Text="O.T." Style="margin: 0px 5px 0px 0px;" />
                                        <asp:CheckBox ID="chk_leave_encash" Visible="false" runat="server" Text="Leave Encash" Style="margin: 0px 5px 0px 0px;" />
                                        <asp:CheckBox ID="chk_lwf" runat="server" Visible="false" Text="LWF" Style="margin: 0px 5px 0px 0px;" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Income Head</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Grade</th>
                                                        <th>Income Head</th>
                                                        <th>Income Type</th>
                                                        <th>% Value</th>
                                                        <th>Applicable From</th>
                                                        <th>P.F.</th>
                                                        <th>ESI</th>
                                                        <th>P. Tax</th>
                                                        <th>O.T.</th>
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
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("grade_name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Income_head")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Income_Type_Name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("Formula")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label5" runat="server" Text='<%#Bind("applicable_from")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:CheckBox ID="chk_pf" runat="server" Style="pointer-events: none;" />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:CheckBox ID="chk_esi" runat="server" Style="pointer-events: none;" />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:CheckBox ID="chk_ptax" runat="server" Style="pointer-events: none;" />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:CheckBox ID="chk_otax" runat="server" Style="pointer-events: none;" />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i>e</asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i>d</asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                        <asp:Label ID="lbl_pf" runat="server" Text='<%#Bind("PF")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_esi" runat="server" Text='<%#Bind("ESI")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_ptac" runat="server" Text='<%#Bind("P_Tax")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_otax" runat="server" Text='<%#Bind("OT")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
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

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />


</asp:Content>
