<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_Status.aspx.cs" Inherits="school_web.Admin.Scholarship_Status" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Scholarship Status (Published or Stopped)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .modal {
            background: rgb(0 0 0 / 52%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 600px;
            }
        }
    </style>
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
                <div class="breadcrumb-title pe-3">Scholarship</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Scholarship Status (Published or Stopped) </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">



                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase" style="position: relative; display:none">Parameter of Scholarship Program List 
                        <a href="Scholarship_Add_Program_Parameter.aspx" class="add-forpopup-btn"><i class="bx bx-upload"></i>Add Scholarship Program Parameter</a></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="find-dv-lbl">Scholarship Program Name</label>
                                            <asp:DropDownList ID="ddl_test_name" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_test_name_SelectedIndexChanged"></asp:DropDownList>
                                        </div>


                                        <div class="col-md-7" style="text-align: right">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" Visible="false" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                        </div>


                                        <div class="col-sm-12">

                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">


                                                    <div class="pgslry-head-div head" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">

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

                                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                <span style="font-size: 14px; font-weight: bold;">Scholarship Program List  <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                            </div>
                                                        </div>


                                                    </div>
                                                    <asp:Panel ID="Panel1" runat="server">



                                                        <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Session">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Scholarship Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Test_name" runat="server" Text='<%#Bind("Test_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Scholarship For">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                               

                                                                <asp:TemplateField HeaderText="Fee">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Course_Fee" runat="server" Text='<%#Bind("Fees")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Start Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Start_Date" runat="server" Text='<%#Bind("start_date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="End Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_end_date" runat="server" Text='<%#Bind("end_date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Maximu Application Form Allowed">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_no_application" runat="server" Text='<%#Bind("no_application")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Payment Mode ">
                                                                    <ItemTemplate>
                                                                       <asp:Label ID="lbl_Payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>




                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_isactivenew" runat="server" Text='<%#Bind("activestatus")%>' ></asp:Label>

                                                                        
                                                                        <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%#Bind("id")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_Isactive" runat="server" Visible="false" Text='<%#Bind("Isactive")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn" OnClick="lnkEdit_Click" CausesValidation="false">Inactive</asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="hdrChkBox" runat="server" onClick="checkAllRows(this);" Text="All" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="rowChkBox" runat="server" onClick="checkUncheckHeaderCheckBox(this);" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <script type="text/javascript">
                                                            function checkAllRows(obj) {

                                                                var objGridview = obj.parentNode.parentNode.parentNode;
                                                                var list = objGridview.getElementsByTagName("input");

                                                                for (var i = 0; i < list.length; i++) {
                                                                    var objRow = list[i].parentNode.parentNode;
                                                                    if (list[i].type == "checkbox" && obj != list[i]) {
                                                                        if (obj.checked) {

                                                                            //If the header checkbox is checked then check all 
                                                                            //checkboxes and highlight all rows.

                                                                            objRow.style.backgroundColor = "#0baf36";
                                                                            objRow.style.Color = "#fff";
                                                                            list[i].checked = true;
                                                                        }
                                                                        else {
                                                                            objRow.style.backgroundColor = "#FFFFFF";
                                                                            list[i].checked = false;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            function checkUncheckHeaderCheckBox(obj) {
                                                                var objRow = obj.parentNode.parentNode;

                                                                if (obj.checked) {
                                                                    objRow.style.backgroundColor = "#0baf36";
                                                                    objRow.style.Color = "#fff";
                                                                }
                                                                else {
                                                                    objRow.style.backgroundColor = "#FFFFFF";
                                                                }
                                                                var objGridView = objRow.parentNode;

                                                                //Get all input elements in Gridview
                                                                var list = objGridView.getElementsByTagName("input");
                                                                for (var i = 0; i < list.length; i++) {
                                                                    var objHeaderChkBox = list[0];

                                                                    //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                                                                    var checked = true;

                                                                    if (list[i].type == "checkbox" && list[i] != objHeaderChkBox) {
                                                                        if (!list[i].checked) {
                                                                            checked = false;
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                objHeaderChkBox.checked = checked;
                                                            }
                                                        </script>

                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            
                                                        <asp:Button ID="btn_inactive" runat="server" Text="Stop" class="btn btn-danger find-dv-btn" OnClick="btn_inactive_Click" OnClientClick="return confirm('Are you sure want to stop Scholarship?');" style="float: right;" />
                                                        <asp:Button ID="btn_active" runat="server" Text="Publish" class="btn btn-success find-dv-btn" Style="margin-left: 20px;float: right;" OnClick="btn_active_Click" OnClientClick="return confirm('Are you sure want to publish Scholarship?');" />

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
</asp:Content>
