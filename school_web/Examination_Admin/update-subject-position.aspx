<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="update-subject-position.aspx.cs" Inherits="school_web.Examination_Admin.update_subject_position" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Subject Position
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            overflow-x: inherit;
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
                <div class="breadcrumb-title pe-3">Subject Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Update  Subject Position</li>
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
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-3" id="storeDv" runat="server">
                                                <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                                <asp:DropDownList ID="ddl_course_search" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary" Style="margin: 21px 0px 0px 0px; padding: 4px 10px;" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="grd-wpr">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="example22" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Class Name</th>
                                                            <th>Subject Name</th>
                                                            <th>Short Name</th>
                                                            <th>Subject Code</th>
                                                            <th>Subject Type</th>
                                                            <th>Mandatory</th>

                                                            <th>Position</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Subject_Short_Name" runat="server" Text='<%#Bind("Subject_Short_Name")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Subject_Code" runat="server" Text='<%#Bind("Subject_Code")%>'></asp:Label>
                                                                        </td>



                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Subject_type" runat="server" Text='<%#Bind("Subject_Type_Scholastic_Co_Scholastic")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_midetry" runat="server"></asp:Label>

                                                                            <asp:Label ID="lbl_Is_mandatory" runat="server" Text='<%#Bind("Is_mandatory")%>' Visible="false"></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:TextBox ID="txt_position_number" class="form-control" Style="width: 60px;" Text='<%#Bind("Subject_position") %>' onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("course_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Subject_position" runat="server" Text='<%#Bind("Subject_position")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Subject_Type_Scholastic_Co_Scholastic" runat="server" Text='<%#Bind("Subject_Type_Scholastic_Co_Scholastic")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
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

        </div>
    </div>
</asp:Content>
