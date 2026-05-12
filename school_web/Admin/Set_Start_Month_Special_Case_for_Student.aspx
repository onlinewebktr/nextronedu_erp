<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Set_Start_Month_Special_Case_for_Student.aspx.cs" Inherits="school_web.Admin.Set_Start_Month_Special_Case_for_Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set Start Month Special Case for Student
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
                            <li class="breadcrumb-item active" aria-current="page">Set Start Monthly Billing</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row">
                                    <div class="col-xl-7">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">

                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-2 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-4 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_session_name" runat="server" class="form-select find-dv-txtbx">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-xl-2 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Adm. No.</label>
                                                        </div>
                                                        <div class="col-xl-4 padd-lft-5">
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-7" style="display:none">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Class</h2>
                                            <div class="fnd-box-wpr-inr">

                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-2 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-2 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_session_class" runat="server" class="form-select find-dv-txtbx">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-xl-2 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Class</label>
                                                        </div>
                                                        <div class="col-xl-2 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_class" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" runat="server" class="form-select find-dv-txtbx">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-xl-2 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Section</label>
                                                        </div>
                                                        <div class="col-xl-2 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx">
                                                            </asp:DropDownList>
                                                        </div> 
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:Button ID="btn_find_classwise" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_classwise_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Student Basic Information</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" T class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" Text="" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Section : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="txtsection" runat="server" Text=" " class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_old_roll_no" runat="server" Text=" " class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Text=" " class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lblhostel" runat="server" Text="No" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transportation : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbltransporttion" runat="server" Text="No" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_phone" runat="server" Text="7250408680" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row"> 
                                                            <div class="row" style="display: none">
                                                                <asp:GridView ID="GridView_month" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Month">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chk_month" AutoPostBack="true" runat="server" Enabled="false" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView> 
                                                            </div>
                                                            <div class="col-xl-10 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Month Name </label>
                                                                    </div>
                                                                    <div class="col-xl-4 padd-lft-5">
                                                                        <asp:DropDownList ID="ddl_monthname" runat="server" class="form-select find-dv-txtbx">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                     <div class="col-xl-2 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Remarks </label>
                                                                    </div>
                                                                    <div class="col-xl-4 padd-lft-5">
                                                                         <asp:TextBox id="txt_remarks" runat="server" class="form-control" TextMode="MultiLine" style="height:100px;"></asp:TextBox>
                                                                    </div>


                                                                    <div class="col-xl-10 padd-lft-5" style="text-align:center;">
                                                                        <asp:Button ID="btn_update_date" runat="server" Text="Update" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_update_date_Click" />
                                                                    </div> 
                                                                </div>
                                                            </div> 
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div> 
                                </asp:Panel>

                                <asp:Panel ID="pnl_class_wise_student" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-xl-12">

                                            <asp:GridView ID="grid_data_student_list" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Session">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                            <asp:Label ID="lbl_session_id" runat="server" Visible="false" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                            <asp:Label ID="lbl_hosteltaken" runat="server" Visible="false" Text='<%#Bind("hosteltaken")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admission No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                            <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
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
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-4 padd-rght-5">
                                            <label for="validationCustom01" class="stdnt-info-fnds">Select Month Name(Which month you start student month fee) </label>
                                        </div>
                                        <div class="col-xl-4 padd-lft-5">
                                            <asp:DropDownList ID="ddl_month_name_class_wise" runat="server" class="form-select find-dv-txtbx">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-xl-4 padd-lft-5">
                                            <asp:Button ID="btn_class_wise_month_skip" runat="server" Text="Update" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_class_wise_month_skip_Click" />
                                        </div>

                                    </div>
                                </asp:Panel>



                            </div>
                        </div>
                    </div>
                </div>


            </div>



        </div>
    </div>
</asp:Content>
