<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_Session_Master.aspx.cs" Inherits="school_web.Admin.Add_Session_Master" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Session Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Session Master</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Session"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Session From<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_session_from"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_session_from" runat="server" onkeypress="return isNumberKey(event)" class="form-control" MaxLength="4"></asp:TextBox>

                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Session To<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txtsession_to"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txtsession_to" runat="server" onkeypress="return isNumberKey(event)" class="form-control" MaxLength="4"></asp:TextBox>
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


                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Added Session</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example211" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Session</th>
                                                        <th>Action</th>
                                                       
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_sessionid" runat="server" Text='<%#Bind("session_id")%>' Visible="false"></asp:Label>

                                                                           <asp:Button ID="btn_Create_Year" runat="server" Text="Create Year" Visible="false" CssClass="btn btn-primary" CausesValidation="false" OnClick="btn_Create_Year_Click" />
                                                                        <asp:Button ID="btn_create_Sem" runat="server" Text="Create Semester" Visible="false" CssClass="btn btn-primary" CausesValidation="false" OnClick="btn_create_Sem_Click" />
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
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 916px;
                margin: 1.75rem auto;
            }
        }
    </style>
    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Academic Session</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 50%">
                        <table class="table table-bordered" style="width: 100%;">
                            <tr>
                                <td style="font-size: 16px; font-weight: bold;">Session
                                </td>
                                <td style="font-size: 14px;">
                                    <asp:Label ID="lbl_session_view" runat="server" Style="font-size: 13px!important; font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 16px; font-weight: bold;">Year<sup>*</sup>
                                </td>
                                <td style="font-size: 14px;">
                                    <asp:TextBox ID="txt_year" runat="server" onkeypress="return isNumberKey(event)" class="form-control" MaxLength="4"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_year" runat="server" Display="Dynamic" ValidationGroup="ck" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Button ID="btn_add_academic_year" runat="server" Text="Add" ValidationGroup="ck" CssClass="btn btn-primary" OnClick="btn_add_academic_year_Click" />
                                    <asp:Button ID="btn_cancel_acadmic_year" runat="server" Visible="false" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" OnClick="btn_cancel_acadmic_year_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lbl_msg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 50%">


                        <asp:GridView ID="grid_session_year" runat="server" class="mb-0 table table-bordered" CssClass="table table-striped table-bordered gridcss" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Session">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Academic Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Academic_Year" runat="server" Text='<%#Bind("Academic_Year")%>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Academic_Year_Id" Visible="false" runat="server" Text='<%#Bind("Academic_Year_Id")%>'></asp:Label>
                                         <asp:Label ID="lbl_Session_Id" Visible="false" runat="server" Text='<%#Bind("Session_Id")%>'></asp:Label>
                                        <asp:LinkButton ID="lnkEdit_sessionyear" runat="server" CausesValidation="false" OnClick="lnkEdit_sessionyear_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDel_sessionyear" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_sessionyear_Click"><i class="lni lni-trash"> </i></asp:LinkButton>

                                    </ItemTemplate>

                                </asp:TemplateField>



                            </Columns>

                        </asp:GridView>
                    </div>



                </div>
            </div>
        </div>
    </div>


    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Academic Semester</h5>
                    <asp:Button ID="Button1" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 50%">
                        <table class="table table-bordered" style="width: 100%;">
                            <tr>
                                <td style="font-size: 16px; font-weight: bold;">Session
                                </td>
                                <td style="font-size: 14px;">
                                    <asp:Label ID="lbl_viewsession_sem" runat="server" Style="font-size: 13px!important; font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 16px; font-weight: bold;">Semester<sup>*</sup>
                                </td>
                                <td style="font-size: 14px;">
                                    <asp:TextBox ID="txt_academic_semester" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_academic_semester" runat="server" Display="Dynamic" ValidationGroup="dk" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Button ID="btn_add_semester" runat="server" Text="Add" ValidationGroup="dk" CssClass="btn btn-primary" OnClick="btn_add_semester_Click" />
                                    <asp:Button ID="btn_semester_cancel" runat="server" Visible="false" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" OnClick="btn_semester_cancel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lbl_msg1" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 50%">


                        <asp:GridView ID="grid_Semester" runat="server" class="mb-0 table table-bordered" CssClass="table table-striped table-bordered gridcss" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Session">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Academic Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Acamedic_Semester" runat="server" Text='<%#Bind("Acamedic_Semester")%>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                          <asp:Label ID="lbl_Acamedic_Semester_Id" Visible="false" runat="server" Text='<%#Bind("Acamedic_Semester_Id")%>'></asp:Label>
                                        <asp:Label ID="lbl_Session_Id_sem" Visible="false" runat="server" Text='<%#Bind("Session_Id")%>'></asp:Label>
                                        <asp:LinkButton ID="lnkEdit_Semester" runat="server" CausesValidation="false" OnClick="lnkEdit_Semester_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDel_Semesterr" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Semesterr_Click"><i class="lni lni-trash"> </i></asp:LinkButton>

                                    </ItemTemplate>

                                </asp:TemplateField>



                            </Columns>

                        </asp:GridView>
                    </div>



                </div>
            </div>
        </div>
    </div>

    <div id="fadeup"></div>
    <div id="fadeup1"></div>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>


     <script type="text/javascript">
         function openModal1() {
             $("#myModal1").show();
             $('#myModal1').addClass('show');
             $('#fadeup1').addClass('modal-backdrop fade show');
         }
         function close() {
             $("#myModal1").hide();
             $('#myModal1').removeClass('show');
             $('#fadeup1').removeClass('modal-backdrop fade show');
         }
    </script>

    <asp:HiddenField ID="hd_sessionid" runat="server" />
    <asp:HiddenField ID="hd_id_Session_Academic" runat="server" />

</asp:Content>
