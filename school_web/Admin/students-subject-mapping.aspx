<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="students-subject-mapping.aspx.cs" Inherits="school_web.Admin.students_subject_mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Subject Allocation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .find-dv {
            margin: 10px 0px 15px 0px;
            padding: 0px 5px;
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Subject Allocation</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-7">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Subject Allocation"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                                <asp:TextBox ID="txt_admission_no" runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox>

                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btn_find_by_admission" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_admission_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="grd-wpr">
                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th class="allCheckbox">
                                                        <asp:CheckBox ID="hdrChkBox" runat="server" />
                                                    </th>
                                                    <th>Admission No</th>
                                                    <th>Roll No.</th>
                                                    <th>Student Name</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td class="singleCheckbox">
                                                                <asp:CheckBox ID="rowChkBox" runat="server" />
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="Label7" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                            </td>


                                                            <td style="text-align: left;">
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("class")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                <asp:Label ID="lbl_adm_no" runat="server" Visible="false" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                <asp:Label ID="lbl_section" runat="server" Visible="false" Text='<%#Bind("Section")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Session_id" runat="server" Visible="false" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Class_id" runat="server" Visible="false" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                            </td>
                                                        </tr>
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

                <div class="col-xl-5">
                    <asp:Label ID="lbl_subject_name" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Subject List"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row" style="margin-bottom: 20px;">
                                    <div class="col-sm-4">
                                        <label for="validationCustom01" class="find-dv-lbl">Subject Type</label>
                                        <asp:DropDownList ID="ddl_subjecttyep" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_subjecttyep_SelectedIndexChanged">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Scholastic</asp:ListItem>
                                            <asp:ListItem>Co-Scholastic</asp:ListItem>
                                        </asp:DropDownList> 
                                    </div> 
                                </div>

                                <div class="row g-3 needs-validation">
                                    <div class="grd-wpr"> 
                                        <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th class="allCheckbox1">
                                                        <asp:CheckBox ID="hdrChkBox1" runat="server" />
                                                    </th>
                                                    <th>Subject Name</th>
                                                    <th>Mandatory</th>
                                                    <th>Subject Type</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rp_subJ_list" runat="server" OnItemDataBound="rp_subJ_list_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td class="singleCheckbox1">
                                                                <asp:CheckBox ID="rowChkBox1" runat="server" />
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                <asp:Label ID="lbl_subject_id" runat="server" Visible="false" Text='<%#Bind("Subject_id")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_midetry" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">

                                                                <asp:Label ID="lbl_Subject_Type_Scholastic_Co_Scholastic_dis" runat="server"    ></asp:Label>
                                                                <asp:Label ID="lbl_midetry2" runat="server" Text='<%#Bind("Is_mandatory")%>' Visible="false"></asp:Label>

                                                                  <asp:Label ID="lbl_Subject_Type_Scholastic_Co_Scholastic" runat="server"   Text='<%#Bind("Subject_Type_Scholastic_Co_Scholastic")%>' Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                        <div class="col-sm-2">
                                            <asp:Button ID="btn_map" runat="server" Text="Map Subject" class="btn btn-primary find-dv-btn" OnClick="btn_map_Click" />
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

    <script type="text/javascript">
        $(function () {
            var $allCheckbox = $('.allCheckbox :checkbox');
            var $checkboxes = $('.singleCheckbox :checkbox');
            $allCheckbox.change(function () {
                if ($allCheckbox.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox.removeAttr('checked');
                }
                else {
                    $allCheckbox.attr('checked', 'checked');
                }
            });
        });



        //====================
        $(function () {
            var $allCheckbox1 = $('.allCheckbox1 :checkbox');
            var $checkboxes = $('.singleCheckbox1 :checkbox');
            $allCheckbox1.change(function () {
                if ($allCheckbox1.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox1.removeAttr('checked');
                }
                else {
                    $allCheckbox1.attr('checked', 'checked');
                }
            });
        });
    </script>
</asp:Content>
