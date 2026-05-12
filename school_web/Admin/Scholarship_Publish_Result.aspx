<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_Publish_Result.aspx.cs" Inherits="school_web.Admin.Scholarship_Publish_Result" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Scholarship Publish Result  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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

        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #1c77fd !important;
            font-size: 12px !important;
            font-weight: bold;
        }

        td {
            font-size: 12px !important;
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
                            <li class="breadcrumb-item active" aria-current="page">Publish Result  </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">

                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                            Note:- If result has been published that you can't change result
                                        </label>
                                    </div>
                                </div>

                                <div class="row">



                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Scholarship <sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_Scholarship_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Scholarship_name_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Scholarship For<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_find_Click" Style="margin: 24px 0px 0px 0px; padding: 3px 10px; width: 60px!important; height: 31px!important;" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 24px 0px 6px 0px !important;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn" Visible="false"><i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 24px 0px 6px 9px !important;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                    ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                            </div>

                                        </div>




                                    </div>

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
                                                            <span style="font-size: 14px; font-weight: bold;">Scholarship Result
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                        </div>
                                                    </div>


                                                </div>

                                                <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Reg. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_testid" runat="server" Text='<%#Bind("Test_id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                  <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("resultid")%>' Visible="false"></asp:Label>
                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scholarship for">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DOB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_DOB" runat="server" Text='<%#Bind("DOB")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Admission Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Admission_date" runat="server" Text='<%#Bind("Admission_date1")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Time">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Admission_time" runat="server" Text='<%#Bind("Admission_time1")%>'></asp:Label>







                                                            </ItemTemplate>
                                                            <ItemStyle Width="160px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Attendance Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_attendance_status" runat="server" Text='<%#Bind("Attendance_Status")%>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Exam Result">
                                                            <ItemTemplate>



                                                                <asp:Label ID="lbl_Exam_Result" runat="server" Text='<%#Bind("Exam_Result")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Full Marks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Full_Marks" runat="server" Text='<%#Bind("Full_Marks")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Obtain Marks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Obtain_Marks" runat="server" Text='<%#Bind("Obtain_Marks")%>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Obtain(%)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Obtain_percentage" runat="server" Text='<%#Bind("Obtain_percentage")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rank">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Obtain_rank" runat="server" Text='<%#Bind("Obtain_rank")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Result Publish Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_activestatus" runat="server" Text='<%#Bind("activestatus")%>'></asp:Label>

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
                                                
                                                        <asp:Button ID="btn_inactive" runat="server" Text="Unpublish" class="btn btn-danger find-dv-btn" OnClick="btn_inactive_Click" OnClientClick="return confirm('Are you sure want to Unpublished result?');" Visible="false" style="float: right;" />
                                                        <asp:Button ID="btn_active" runat="server" Text="Publish" class="btn btn-success find-dv-btn" Style="margin-left: 20px;float: right;" OnClick="btn_active_Click" Visible="false" OnClientClick="return confirm('Are you sure want to publish result?');" />
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

</asp:Content>
