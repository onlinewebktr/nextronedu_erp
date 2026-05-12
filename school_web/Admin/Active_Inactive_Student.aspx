<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Active_Inactive_Student.aspx.cs" Inherits="school_web.Admin.Active_Inactive_Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Active/Inactive Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Student Update</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Active/Inactive Student</li>
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
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_Section" runat="server" class="form-control find-dv-txtbx"  ></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="grd-wpr">


                                                <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Session">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Admission No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Section">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Roll No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Student Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                                <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%#Bind("id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_istatus" runat="server" Visible="false" Text='<%#Bind("Status")%>'></asp:Label>
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

                                                <asp:Button ID="btn_inactive" runat="server" Text="Inactive" class="btn btn-danger find-dv-btn" OnClick="btn_inactive_Click" OnClientClick="return confirm('Are you sure want to inactive student?');" />
                                                <asp:Button ID="btn_active" runat="server" Text="Active" class="btn btn-success find-dv-btn" style="margin-left: 20px;" OnClick="btn_active_Click" OnClientClick="return confirm('Are you sure want to active Student?');" />

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
</asp:Content>
