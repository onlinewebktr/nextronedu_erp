<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="manual-attendance.aspx.cs" Inherits="school_web.Payroll.manual_attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Manual Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            overflow-x: inherit;
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
                <div class="breadcrumb-title pe-3">Attendance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Manual Attendance</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Manual Attendance</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Year</label>
                                                        <asp:DropDownList ID="ddl_year" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Employee Name</label>
                                                        <asp:DropDownList ID="ddl_employee_name" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="grd-wpr">
                                                <asp:GridView ID="grd_attendance" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" class="table table-striped table-bordered dataTable">
                                                    <RowStyle />
                                                    <Columns>
                                                        <%--<asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Day">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Day" runat="server" Text='<%#Bind("Day") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Working">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Working" runat="server" Text='<%#Bind("Working") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shift 1 in">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Shift_1_in" runat="server" Text='<%#Bind("Shift_1_in") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shift 1 out">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Shift_1_out" runat="server" Text='<%#Bind("Shift_1_out") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shift 2 in">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Shift_2_in" runat="server" Text='<%#Bind("Shift_2_in") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Shift 2 out">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Shift_2_out" runat="server" Text='<%#Bind("Shift_2_out") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Width="125px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_employee_id" runat="server" Visible="false" Text='<%#Bind("Employee_id") %>'></asp:Label>
                                                                <asp:Label ID="lbl_grade_id" runat="server" Visible="false" Text='<%#Bind("Grade_id") %>'></asp:Label>
                                                                <asp:Label ID="lbl_idate" runat="server" Visible="false" Text='<%#Bind("idate") %>'></asp:Label>

                                                                <asp:LinkButton ID="link_make_attendance" runat="server" class="lnk-btn" OnClick="link_make_attendance_Click">Make Attendance</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>



                                                <%--<table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Date</th>
                                                            <th>Day</th>
                                                            <th>Working</th>

                                                            <th>Shift 1 in</th>
                                                            <th>Day</th>
                                                            <th>Working</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_day" runat="server" Text='<%#Bind("Day")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Working")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Working")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Working")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("Working")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_description_id" runat="server" Text='<%#Bind("description_id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>--%>
                                            </div>
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



    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 500px">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-xl-8">
                        <h2 class="popup-dt-h">Make Attendance</h2>
                    </div>
                    <div class="col-xl-4">
                        <ul class="conf-btn-ul" style="margin: -5px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="lnk_close" runat="server" OnClick="lnk_close_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="mdl-frm-wpr" id="grp_shift_1" runat="server">
                    <asp:CheckBox ID="chk_first_shift" runat="server" class="mdl-wrkng-shift-p" Text="First Shift" />
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-4">
                            <label for="validationCustom01" class="find-dv-lbl">In Time</label>
                            <div class='input-group date' id='datetimepicker3'>
                                <asp:TextBox ID="txt_morning_in" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                    <span class="bx bx-timer"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label for="validationCustom01" class="find-dv-lbl">Out Time</label>
                            <div class='input-group date' id='datetimepicker1'>
                                <asp:TextBox ID="txt_morning_out" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                    <span class="bx bx-timer"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="mdl-frm-wpr" id="grp_shift_2" runat="server">
                    <asp:CheckBox ID="chk_sec_shift" runat="server" class="mdl-wrkng-shift-p" Text="Second Shift" />
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-4">
                            <label for="validationCustom01" class="find-dv-lbl">In Time</label>
                            <div class='input-group date' id='datetimepicker4'>
                                <asp:TextBox ID="txt_evening_in" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                    <span class="bx bx-timer"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label for="validationCustom01" class="find-dv-lbl">Out Time</label>
                            <div class='input-group date' id='datetimepicker5'>
                                <asp:TextBox ID="txt_evening_out" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                    <span class="bx bx-timer"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="mdl-frm-wpr" runat="server" visible="false" id="appc_leave">
                    <asp:CheckBox ID="chk_is_leave_taken" runat="server" class="mdl-wrkng-shift-p" Text="Is leave taken" OnCheckedChanged="chk_is_leave_taken_CheckedChanged" />
                    <div class="row" style="margin-top: 10px;" id="pnl_applicable_leave" runat="server">
                        <div class="col-sm-4">
                            <label for="validationCustom01" class="find-dv-lbl">Applicable leave</label>
                            <asp:DropDownList ID="ddl_apl_leave" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 10px;">
                    <div class="col-sm-4">
                        <asp:CheckBox ID="chk_is_absent" runat="server" Text=" Mark as absent" />
                    </div>
                </div>

                <div class="col-12">
                    <asp:Button ID="btn_save_att" OnClick="btn_save_att_Click" runat="server" Text="Save" CssClass="btn btn-primary" Style="margin: 13px 0px 0px 0px; padding: 3px 10px; font-size: 13px; border-radius: 3px;" />
                </div>
            </div>
        </div>
    </div>
    <!--end page wrapper -->

    <link href="../timePicker/glaphycon.css" rel="stylesheet" />
    <link href="../timePicker/bootstrap-datetimepicker.css" rel="stylesheet" />
    <script src="../timePicker/moment-with-locales.js"></script>
    <script src="../timePicker/bootstrap-datetimepicker.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#datetimepicker3').datetimepicker({
                format: 'LT'
            });
        });
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'LT'
            });
        });


        $(function () {
            $('#datetimepicker4').datetimepicker({
                format: 'LT'
            });
        });
        $(function () {
            $('#datetimepicker5').datetimepicker({
                format: 'LT'
            });
        });
    </script>
    <style>
        .popup-dt-h {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            font-size: 18px;
            color: #333;
            font-weight: 500;
            letter-spacing: .5px;
        }

        .conf-btn-ul li a {
            margin: 0px 0px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff3b00;
            color: #fff;
            width: 51px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }

        .mdl-frm-wpr {
            margin: 10px 0px 0px 0px;
            padding: 10px 10px;
            width: 100%;
            border: 1px solid #ddd;
            border-radius: 3px;
        }

        .mdl-wrkng-shift-p {
            margin: -21px 0px 0px -5px;
            padding: 0px 5px;
            width: auto;
            float: left;
            background: #fff;
            font-weight: 500;
        }
    </style>
</asp:Content>
