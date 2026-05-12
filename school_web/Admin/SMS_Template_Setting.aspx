<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="SMS_Template_Setting.aspx.cs" Inherits="school_web.Admin.SMS_Template_Setting" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    SMS Template Setting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .buttons-print {
            display: none;
        }

        #notification {
            z-index: 99999999999 !important;
        }

        .find-dv-lbl {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            color: #000;
        }

        .logo-icon {
            width: 54px;
            height: 55px;
        }

        img {
            height: 13px;
            width: 13px;
            margin-right: 12px;
        }

        .btn-group, .btn-group-vertical {
            position: relative;
            display: inline-flex;
            vertical-align: middle;
            display: none;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
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
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        @media print {
            .noPrint {
                display: none;
            }
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
            background: #00000052!important;
        }
         .buttons-print {
            display: none;
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
                <div class="breadcrumb-title pe-3">SMS</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">SMS Template Setting</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">



                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">

                                    <div class="row">

                                        <div class="find-dv">
                                            <div class="row">







                                                <div class="col-sm-12">
                                                    <asp:LinkButton ID="btn_excels" Visible="false" runat="server" Style="margin: 0px 1px 6px 15px !important; float: right;"
                                                        OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print11" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px !important; float: right;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                                    <a href="#" class="btn btn-success find-dv-btn" style="margin: 0px 7px 1px 0px !important; float: right; padding: 3px 6px 6px 11px; font-size: 14px;" title="Add Enquiry" data-toggle="modal" data-target="#myModal1"><i class="bx bx-plus-medical"></i></a>
                                                </div>
                                            </div>


                                        </div>

                                    </div>





                                    <div class="row">

                                        <div class="grd-wpr">

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
                                                                    <span style="font-size: 14px; font-weight: bold;">Message Template<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="example2" class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Message Type</th>
                                                                        <th>SMS Template</th>
                                                                        <th>Variable Name</th>
                                                                        <th>SMS Type</th>
                                                                        <%if (this.IsChecked)
                                                                            { %>
                                                                        <th class="noPrint">Action</th>
                                                                        <%}%>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Send_From" runat="server" Text='<%#Bind("Send_From")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_SMS_Tempate" runat="server" Style="word-break: break-all" Text='<%#Bind("SMS_Tempate")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_VariableName" runat="server" Style="word-break: break-all" Text='<%#Bind("VariableName")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_SMSType" runat="server" Text='<%#Bind("SMSType")%>'></asp:Label>
                                                                                </td>
                                                                                <%if (this.IsChecked)
                                                                                    { %>
                                                                                <td style="text-align: left;" class="noPrint">
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_Is_edit_delete" runat="server" Text='<%#Bind("Is_edit_delete")%>' Visible="false"></asp:Label>

                                                                                </td>
                                                                                <%} %>
                                                                            </tr>

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
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />





    <style>
        .mdl-frm-row {
            margin: 2px 0px 6px 0px !important;
        }

        .col-eq {
            flex: 1;
            background: #f4f4f4;
        }

        .taskside {
            padding: 10px;
        }

            .taskside h4 {
                margin-bottom: 0;
                font-size: 18px;
            }

        .taskseparator {
            border-top: 1px solid #e6e6e6;
            -webkit-box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
            box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
        }

        .taskside hr {
            margin-top: 9px;
            margin-bottom: 9px;
            border: 0;
            border-top: 1px solid #655f5f !important;
        }

        .task-info {
            float: left;
            width: 100%;
        }

            .task-info h5 {
                font-size: 13px;
            }

        .text-dark {
            font-weight: bold;
        }

        .fa.pull-left {
            margin-right: 0.3em;
        }

        .task-info h5 span {
        }
    </style>
    <div class="modal fade" id="myModal1" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 1050px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Add Message Template</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">

                            <div class="col-sm-2">
                                <label for="validationCustom01" class="form-label">
                                    Message Type<sup>*
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field."
                                            ControlToValidate="txt_type_module" ValidationGroup="A">
                                        </asp:RequiredFieldValidator>
                                    </sup>
                                </label>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_type_module" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <label for="validationCustom01" class="form-label">
                                    SMS Type<sup>* </sup>
                                </label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_type" runat="server" class="form-select find-dv-txtbx">
                                    <asp:ListItem>English</asp:ListItem>
                                    <asp:ListItem>Unicode</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>



                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-2">
                                <label for="validationCustom01" class="form-label">SMS Template<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_smstemplate"></asp:RequiredFieldValidator></sup></label>
                            </div>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txt_smstemplate" runat="server" class="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                            </div>

                        </div>
                    </div>



                    <div class="mdl-frm-row">
                        <div class="row">

                            <div class="col-sm-2">
                                <label for="validationCustom01" class="form-label">Variable Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_sms_variablename"></asp:RequiredFieldValidator></sup></label>

                            </div>
                            <div class="col-sm-10">(comma separated)
                                <asp:TextBox ID="txt_sms_variablename" runat="server" TextMode="MultiLine" class="form-control" Height="100px"></asp:TextBox>
                            </div>


                        </div>



                    </div>



                </div>
                <div class="mdl-frm-row">
                    <div class="row">
                        <div class="col-sm-4"></div>
                        <div class="col-sm-8">
                            <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal1() {

            $('#myModal1').modal('show');

        }

    </script>

</asp:Content>
