<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="other-fee-setup.aspx.cs" Inherits="school_web.Admin.other_fee_setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Special Fee Setup
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

        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>




    <script src="../assets/js/table2excel.js"></script>
    <%--<script type="text/javascript">
        function Export() {
            var class_name = $('#<%= hd_class_name.ClientID %>').val();
            $("[id*=tabledata1]").table2excel({
                filename: class_name + ".xls",
                sheetName: class_name + "-" 
            });
            return false;
        }
    </script>--%>
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
                            <li class="breadcrumb-item active" aria-current="page">Special Fee Setup</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="Admission_Fee_Master.aspx">Admission Fees</a></li>
                        <li><a href="annual-fee-master.aspx">Annual Fees</a></li>
                        <li><a href="monthly-fee-master.aspx">Monthly Fees</a></li>
                        <li><a href="other-fee-setup.aspx" class="sub-mnu-p-a-active">Special Fee Setup</a></li>
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

                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label Llabel" style="position: relative; width: 100%;">
                                                    Fees Type<a href="#!" data-toggle="modal" data-target="#myModalHead" class="addmore-btns"><span class="material-symbols-outlined" style="font-size: 17px;">add</span></a></label>
                                                <asp:DropDownList ID="ddl_fee_type" runat="server" class="form-select"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label Llabel">Amount</label>
                                                <asp:TextBox ID="txt_fee_amount" runat="server" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-12">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Style="margin: 15px 0px 0px 0px;" />
                                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" Style="margin: 15px 0px 0px 0px;" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Admission Fees </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-3" id="Div1" runat="server">
                                            <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Session</label>
                                            <asp:DropDownList ID="ddl_session_serach" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" id="storeDv" runat="server">
                                            <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                            <asp:DropDownList ID="ddl_course_search" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-3">
                                            <asp:Button ID="btn_find" OnClick="btn_find_Click" runat="server" Style="margin: 20px 0px 6px 0px;" class="btn btn-primary find-dv-btn" Text="Find" />
                                        </div>

                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click1" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-12">
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">
                                                    <div id="content">
                                                        <div class="pgslry-head-div head">
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                                </h1>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>
                                                                    &nbsp;&nbsp; 
                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">Admission Fee Master
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Session</th>
                                                                        <th>Class</th>
                                                                        <th>Fee Head</th>
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
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Class" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Parameter" runat="server" Text='<%#Bind("content")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("session_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("class_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id")%>' Visible="false"></asp:Label>
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
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModalHead" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header" style="padding: 3px 10px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Add Fee Head</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body" style="padding: 5px 5px;">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                        <div class="mdl-frm-row">
                            <label for="validationCustom01" class="find-dv-lbl">Fee Head Name</label>
                            <div class="row">
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txt_fee_head" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btn_add_fee_head" OnClick="btn_add_fee_head_Click" runat="server" Text="Save" class="button-6161 disc-pop-save_disc" Style="margin: 0px 0px 0px 0px; height: 30px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!--end page wrapper -->
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 916px;
                margin: 1.75rem auto;
            }
        }
    </style>

    <script type="text/javascript">
        function openModalHead() {
            $('#myModalHead').modal('show');
        }
    </script>

</asp:Content>
