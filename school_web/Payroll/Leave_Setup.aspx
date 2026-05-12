<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="Leave_Setup.aspx.cs" Inherits="school_web.Payroll.Leave_Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Leave Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-ttleS {
            margin: 0px 0px 0px 0px;
            padding: 8px 10px 5px 10px;
            width: 100%;
            float: left;
            font-size: 18px;
            color: #0296bd;
            border-bottom: 1px solid #ddd;
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
    </style>
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
                            <li class="breadcrumb-item active" aria-current="page">Leave Setup</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Leave Setup"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Grade Name<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_gradename" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_gradename_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Periad <sup>*</sup></label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_start_period" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Periad To <sup>*</sup></label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_end_period" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">

                                    <table class="table table-bordered " style="width: 50%">
                                        <tr>
                                            <td style="padding: 5px;" colspan="4">Leave Description
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px;">Levae Name</td>
                                            <td style="padding: 5px;">Leave Type
                                            </td>
                                            <td style="padding: 5px;">Max No of Applied
                                            </td>
                                            <td style="padding: 5px;">Leave Applied
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px;">
                                                <asp:DropDownList ID="txt_leave_id" runat="server" class="form-select find-dv-txtbx">
                                                </asp:DropDownList>

                                            </td>
                                            <td style="padding: 5px;">
                                                <asp:DropDownList ID="ddl_leave_type" runat="server" class="form-select find-dv-txtbx" AutoPostBack="True" OnSelectedIndexChanged="ddl_leave_type_SelectedIndexChanged">
                                                    <asp:ListItem>Fixed</asp:ListItem>
                                                    <asp:ListItem>Calculated</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                            <td style="padding: 5px;">
                                                <asp:TextBox ID="txt_leave_applied" runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                            </td>
                                            <td style="padding: 5px;">
                                                <asp:DropDownList ID="ddl_leave_unit" runat="server" class="form-select find-dv-txtbx">
                                                    <asp:ListItem>Per Month</asp:ListItem>
                                                    <asp:ListItem>Per Year</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>


                                        </tr>

                                    </table>

                                    <table class="table table-bordered " style="width: 50%">
                                        <tr>
                                            <td style="padding: 5px;" colspan="2">Leave Taken Formula
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px;">Days Worked In Month</td>
                                            <td style="padding: 5px;">Earned Leave
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="padding: 5px;">
                                                <asp:TextBox ID="txt_days_worked_in_month" runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                            </td>
                                            <td style="padding: 5px;">

                                                <asp:TextBox ID="txt_earned_leave" runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>


                                            </td>



                                        </tr>

                                    </table>

                                    <table class="table table-bordered " style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px;" colspan="3">Treatment of balance leave to next year
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px;">Is C/F</td>
                                            <td style="padding: 5px;">Treatement of leave
                                            </td>
                                            <td style="padding: 5px;">Max Leave C/F
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px;">

                                                <asp:CheckBox ID="chk_cf_n_year_Checked_1" runat="server" OnCheckedChanged="chk_cf_n_year_Checked_1_CheckedChanged" />
                                            </td>
                                            <td style="padding: 5px;">

                                                <asp:DropDownList ID="ddl_treatement_of_leave_to_next_year" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_treatement_of_leave_to_next_year_SelectedIndexChanged">
                                                    <asp:ListItem>Lapse</asp:ListItem>
                                                    <asp:ListItem>Carry Forward All</asp:ListItem>
                                                    <asp:ListItem>Lapse & Carry Forward</asp:ListItem>
                                                </asp:DropDownList>


                                            </td>

                                            <td style="padding: 5px;">
                                                <asp:TextBox ID="txt_max_leave_cf" onkeypress="return isNumberKey(event)" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                            </td>

                                        </tr>

                                    </table>


                                    <asp:Button ID="btn_add" runat="server" Text="Add" class="btn btn-primary find-dv-btn" OnClick="btn_add_Click" Style="padding: 0px 15px; height: 34px; float: left; margin: 0px 0px 0px 0px;" />
                                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark find-dv-btn" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" Style="padding: 0px 15px; height: 34px; float: left; margin: 0px 0px 0px 10px;" />



                                </div>
                                <div class="needs-validation">
                                    <div style="overflow: auto;">
                                        <asp:GridView ID="GrdView" runat="server" class="mb-0 table table-bordered" AutoGenerateColumns="False" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grade Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_grade_name" runat="server" Text='<%#Bind("grade_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Period">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Start_Period" runat="server" Text='<%#Bind("Start_Period1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="End Period">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_End_Period" runat="server" Text='<%#Bind("End_Period1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Leave_Name" runat="server" Text='<%#Bind("Leave_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Leave_Type" runat="server" Text='<%#Bind("Leave_Type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No Of Leave">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_No_of_Leave" runat="server" Text='<%#Bind("No_of_Leave")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Applied">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_leave_applied" runat="server" Text='<%#Bind("leave_applied")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Days Worked In Month">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Days_Worked_in_month" runat="server" Text='<%#Bind("Days_Worked_in_month")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Earned Leave">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Earned_Leave" runat="server" Text='<%#Bind("Earned_Leave")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Leave CF In Next Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Leave_CF_in_next_year" runat="server" Text='<%#Bind("Leave_CF_in_next_year")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Treatement Of Leave To Next Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Treatement_of_Leave_to_Next_year" runat="server" Text='<%#Bind("Treatement_of_Leave_to_Next_year")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Max Leave CF">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Max_Leave_CF" runat="server" Text='<%#Bind("Max_Leave_CF")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Availed In Same Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Availed_in_same_year" runat="server" Text='<%#Bind("Availed_in_same_year")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                         <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" CssClass="mb-2 mr-2 btn btn-warning" OnClick="lnkEdit_Click" CausesValidation="false">Edit</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDel" runat="server" CssClass="mb-2 mr-2 btn btn-danger" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete?');" CausesValidation="false">Delete</asp:LinkButton>--%>
                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
    <script>
        $(function () {
            $("#<%=txt_start_period.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_end_period.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
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
