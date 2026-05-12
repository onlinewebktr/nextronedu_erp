<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="course-master.aspx.cs" Inherits="school_web.Admin.course_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Class Master
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
                            <li class="breadcrumb-item active" aria-current="page">Class Master</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Class Master"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Class Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_course_name"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_course_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Position<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_position"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_position" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12" style="display: none">
                                        <label for="validationCustom01" class="form-label">Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_type" runat="server" class="form-select">
                                            <asp:ListItem>Yearwise</asp:ListItem>
                                            <asp:ListItem>Semesterwise</asp:ListItem>
                                        </asp:DropDownList>
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
                    <h6 class="mb-0 text-uppercase">Added Class</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Class Name</th>
                                                        <th>Position</th>

                                                        <th>Action</th>
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
                                                                    <asp:Label ID="lbl_course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_position_number" class="form-control" Style="width: 60px;" Text='<%#Bind("Position") %>' onkeypress="return isNumberKey(event)" runat="server" OnTextChanged="txt_position_number_TextChanged" AutoPostBack="true"  ></asp:TextBox>
                                                                </td>

                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_type" runat="server" Text='<%#Bind("Type")%>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                    <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_course_id" runat="server" Text='<%#Bind("course_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_is_sync" runat="server" Text='<%#Bind("is_sync")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_position" runat="server" Text='<%#Bind("Position")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_show_is_synk" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>

                                            <asp:Button ID="btn_update_sl" OnClick="btn_update_sl_Click" class="mt-2 btn btn-primary" runat="server" Style="padding: 7px 15px 6px; font-size: 12px; max-width: 150px; float: right; margin: 10px 0px 0px 0px;"
                                                Text="Update Position" />
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
</asp:Content>
