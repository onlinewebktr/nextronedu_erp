<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Subject_mapping_with_teacher.aspx.cs" Inherits="school_web.Admin.Subject_mapping_with_subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Subject Maping with Teacher
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .find-dv {
            margin: 10px 0px 15px 0px;
            padding: 0px 5px;
        }

        table.dataTable td, table.dataTable th {
            font-size: 13px;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative;">
                <div class="breadcrumb-title pe-3">Teacher</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Map Subject with Teacher</li>
                        </ol>
                    </nav>
                </div>
                <asp:LinkButton ID="lnk_auto_assign_subject" Visible="false" OnClick="lnk_auto_assign_subject_Click" Style="float: right; position: absolute; right: 0px; font-size: 16px; top: 8px; padding: 1px 5px 0px 25px; border: 1px solid #0d6efd; border-radius: 2px;"
                    runat="server">
                    <i class="bx bx-refresh" style="font-size: 23px; line-height: 20px; margin: 0px 0px 0px 0px; position: absolute; left: 0px; top: 2px;"></i>Auto Assign
                </asp:LinkButton>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-6">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Subject Details"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="grd-wpr">
                                        <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th class="allCheckbox1">
                                                        <asp:CheckBox ID="hdrChkBox1" runat="server" />
                                                    </th>
                                                    <th>Subject</th>
                                                    <th>Subject Id</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rp_subJ_list" runat="server">
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
                                                                <asp:Label ID="lbl_course_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Subject_id")%>'></asp:Label>
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

                <div class="col-xl-6" id="rightpnl" runat="server" visible="false">
                    <asp:Label ID="Label3" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Map Subject with Teacher"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="grd-wpr" style="padding: 20px 10px 5px 10px;">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Select Section</label>
                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Select Teacher</label>
                                                <asp:DropDownList ID="ddl_teacher" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_teacher_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-sm-4">
                                                <asp:Button ID="btn_map" runat="server" Text="Map" class="btn btn-primary find-dv-btn" OnClick="btn_map_Click" />
                                            </div>
                                        </div>


                                        <asp:Panel ID="pnl_mapped_subj" runat="server" Visible="false">
                                            <div class="breadcrumb-title pe-3" style="border: 0px; font-size: 18px; margin: 15px 0px 2px 0px;">
                                                Mapped Subject
                                            </div>
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Class</th>
                                                        <th>Section</th>
                                                        <th>Assign Subject</th>
                                                        <th>Teacher Name</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lbl_CategoryID" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_course_id" runat="server" Visible="false" Text='<%#Bind("course_id") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label>
                                                                    (<asp:Label ID="lbl_subjectid" runat="server" Text='<%#Bind("AssignCourseID") %>'></asp:Label>)
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Username" runat="server" Text='<%#Bind("UserName") %>'></asp:Label>
                                                                </td>
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

    <asp:HiddenField ID="hd_type" runat="server" />
</asp:Content>
