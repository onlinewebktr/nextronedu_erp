<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="annual-fee-for-hosteler.aspx.cs" Inherits="school_web.Admin.annual_fee_for_hosteler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Annual Fees for Hostel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server"> 
    <style>
        .Llabel {
            margin: 11px 0px 6px 0px;
        }

        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1050;
            display: none;
            width: 100%;
            height: 100%;
            overflow: hidden;
            outline: 0;
        }

        .modal-header {
            padding: 7px 15px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }
    </style>
    <script src="../assets/js/table2excel.js"></script>
    <script type="text/javascript">
        function Export() {
            var class_name = $('#<%= hd_class_name.ClientID %>').val();
            $("[id*=tabledata1]").table2excel({
                filename: class_name + "AnuulFee.xls",
                sheetName: class_name + "-"

            });
            return false;
        }

        function openModal() {
            $('#myModal').modal('show');
        }

        function openModal1() {
            $('#myModal1').modal('show');
        }
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Fees Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Fees for Hostel</li>
                        </ol>
                    </nav>
                </div>
                <a href="#" data-toggle="modal" data-target="#myModal1" class="button-29" style="float: right; position: absolute; right: 0px;"><i class="bx bx-cog"></i>Copy Fee</a>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="admission-fee-for-hosteler.aspx">Admission Fees</a></li>
                        <li><a href="annual-fee-for-hosteler.aspx" class="sub-mnu-p-a-active">Annual Fees</a></li>
                        <li><a href="monthly-fee-for-hostel.aspx">Monthly Fees</a></li>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-xl-12">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label Llabel">Session</label>
                                                <asp:DropDownList ID="ddl_session" runat="server" class="form-select">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label Llabel">Hostel </label>
                                                <asp:DropDownList ID="ddl_hostel_name" runat="server" class="form-select">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-12">
                                                <label for="validationCustom01" class="form-label Llabel">Class</label>
                                                <span class="chkbx-all">
                                                    <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" /></span>
                                                <br />
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                                        <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                        <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>

                                            <div class="col-md-6" style="margin: 0px 0px 10px 0px;">
                                                <label for="validationCustom01" class="form-label Llabel">Fees Detail</label>
                                                <br />
                                                <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="grd_fee_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center; margin: 0px 0px 0px 0px;">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Fees Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fees">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_fee" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
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
                                                <asp:Label ID="lbl_totalmrp" runat="server" Style="color: #00131c; width: 100%; float: right; text-align: right; padding: 1px 75px 0px 0px; background: #b9af9e; font-weight: 500; border: 1px solid #8d8d8d; border-top: 0px; font-size: 15px;"></asp:Label>
                                            </div>
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
                </div>

                <script type="text/javascript">  
                    //=============================
                    $(document).ready(function () {
                        CalculateTotalFee();
                        $("[Id*=txt_fee]").on("keyup", function () {
                            CalculateTotalFee();
                        });
                    });
                    function CalculateTotalFee() {
                        var total = 0; var totalFee = 0;
                        $("[Id*=txt_fee").each(function () {
                            total += parseFloat($(this).val());
                            $("[Id*=txt_fee").focus(function () { $(this).select(); });
                        });
                        $("#<%=lbl_totalmrp.ClientID%>").text("Total : " + total);
                    }
                </script>

                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Fees</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-3" id="Div1" runat="server">
                                            <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Session</label>
                                            <asp:DropDownList ID="ddl_session_serach" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" id="storeDv" runat="server">
                                            <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class</label>
                                            <asp:DropDownList ID="ddl_course_search" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_course_search_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-3" id="Div2" runat="server">
                                            <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Hostel</label>
                                            <asp:DropDownList ID="ddl_hostel_search" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_search_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click1" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                        </div> 
                                        <div class="col-sm-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Session</th>
                                                            <th>Class</th>
                                                            <th>Hostel Name</th>
                                                            <th>Parameter</th>
                                                            <th>Amount</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_viewaddedfee" runat="server">
                                                            <ItemTemplate>
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_hostel" runat="server" Text='<%#Bind("Hostel_name")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Parameter" runat="server" Text='<%#Bind("Parameter")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_parameter_id" runat="server" Text='<%#Bind("parameter_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Acamedic_Semester_Id" runat="server" Text='<%#Bind("Acamedic_Semester_Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("session_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("class_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type")%>' Visible="false"></asp:Label>
                                                                            <asp:LinkButton ID="lnk_view" runat="server" CausesValidation="false" OnClick="lnk_view_Click"><i class="lni lni-eye"> </i></asp:LinkButton>

                                                                            <asp:Label ID="lbl_Hostel_Id" runat="server" Text='<%#Bind("Hostel_Id")%>' Visible="false"></asp:Label>

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

    <!--end page wrapper -->
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 916px;
                margin: 1.75rem auto;
            }
        }

        .auto-style1 {
            height: 25px;
        }
    </style>

    <asp:HiddenField ID="hd_class_name" runat="server" />
    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Fees Details</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                        <asp:LinkButton ID="LinkButton_excel" runat="server" OnClientClick="return Export();" class="btn btn-primary find-dv-btn" Style="margin: -12px 7px 4px 0px !important;">  <i class='bx bx-download'></i> Excel</asp:LinkButton>

                        <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Session</th>
                                    <th>Class</th>
                                    <th>Hostel Name</th>

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
                                                    <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lbl_Class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lbl_Hostel_name" runat="server" Text='<%#Bind("Hostel_name")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>



                    </div>


                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%" runat="server" id="tabledata1">


                        <asp:GridView ID="grid_feedetails" runat="server" class="mb-0 table table-bordered" CssClass="table table-striped table-bordered gridcss" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_feedetails_RowDataBound" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fees Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("content")%>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fees">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount")%>'></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal1" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 18px;">Copy Hostel Annual Fee For Next Session</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">From Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_current_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">To Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_copy_to_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_copy_admissionfee_for_day" OnClick="btn_copy_admissionfee_for_day_Click" runat="server" Text="Copy" class="btn btn-success" OnClientClick="return confirm('Are you sure you want to copy  Annual fee?');" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
