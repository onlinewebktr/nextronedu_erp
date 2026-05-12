<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="personality-traits.aspx.cs" Inherits="school_web.Examination_Admin.personality_traits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Personality Traits
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Personality Traits</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="hd_id" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Personality Traits"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Select Class<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Activity Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_activity_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Position<sup>*</sup></label>
                                        <asp:TextBox ID="txt_position" onkeypress="return isNumberKey(event)" runat="server" class="form-control"></asp:TextBox>
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
                    <h6 class="mb-0 text-uppercase">Added Personality Traits</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example2" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Class</th>
                                                        <th>Activity Name</th>
                                                        <th>Position</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_class_name" runat="server" Text='<%#Bind("Class_name")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_activity_name" runat="server" Text='<%#Bind("Activity_name")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_position_number" class="form-control" Style="width: 60px;" Text='<%#Bind("Position") %>' onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_activity_id" runat="server" Text='<%#Bind("Activity_Id")%>' Visible="false"></asp:Label>
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
</asp:Content>
