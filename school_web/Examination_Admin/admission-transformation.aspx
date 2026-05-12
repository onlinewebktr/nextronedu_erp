<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="admission-transformation.aspx.cs" Inherits="school_web.Examination_Admin.admission_transformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Transfer Students in New Year
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 33px;
            right: 10px;
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
                <div class="breadcrumb-title pe-3">Admission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Transfer Students in New Year</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Old Student</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Session</label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Class</label>
                                        <asp:DropDownList ID="ddl_course" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Section</label>
                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary" Style="margin: 24px 0px 0px 0px; padding: 7px 10px;" />
                                    </div>


                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Session</label>
                                        <asp:DropDownList ID="ddl_session_adm" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Admission No.</label>
                                        <asp:TextBox ID="txt_Adm_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btn_find_by_adm_no" runat="server" Text="Find" class="btn btn-primary" OnClick="btn_find_by_adm_no_Click" Style="margin: 24px 0px 0px 0px; padding: 7px 10px;" />
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="grd_studentdetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Admission No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnk_position" runat="server" OnClick="lnk_position_Click" Style="background: #0277b1; width: 75px; color: #fff; padding: 4px 10px 2px 10px; border-radius: 2px;">Name <i class="bx bx-sort"></i></asp:LinkButton>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_course" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Roll No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                                        <asp:Label ID="lbl_status" runat="server" Visible="false" Text='<%#Bind("Transfer_Status") %>'></asp:Label>
                                                        <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%#Bind("id") %>'></asp:Label>
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
                                    </div> 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--end row-->

                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Transfer To New Year </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Session</label>
                                        <asp:DropDownList ID="ddl_session_tranfser2" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Class</label>
                                        <asp:DropDownList ID="ddl_course_transfer2" runat="server" class="form-select">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Section</label>
                                        <asp:DropDownList ID="ddl_section_transfer_2" runat="server" class="form-select">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-3" style="position: relative;">
                                        <label for="validationCustom01" class="form-label">Re-Admission Date</label>
                                        <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon" Style="width: 100%;" class="form-control"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
                                        <script src="../Autocomplete/jquery-ui.js"></script>
                                        <script>
                                            $(function () {
                                                $("#<%=txt_date.ClientID %>").datepicker({
                                                    dateFormat: "dd/mm/yy",
                                                    changeMonth: true,
                                                    changeYear: true,
                                                    yearRange: "1900:2100",


                                                });
                                            });
                                        </script>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Transfer" OnClientClick="return confirm('Are you sure you want to transfer ?');" OnClick="btn_Submit_Click" CssClass="btn btn-primary" Style="width: 137px; margin: 24px 0px 0px 0px; padding: 7px 0px 7px 0px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
