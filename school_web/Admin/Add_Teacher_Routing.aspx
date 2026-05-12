<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_Teacher_Routing.aspx.cs" Inherits="school_web.Admin.Add_Teacher_Routing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Teacher Routing 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            padding: 5px 20px;
            font-size: 15px;
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
                <div class="breadcrumb-title pe-3">Routine</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Teacher Routine </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=""></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="col-lg-3">
                                        <div class="position-relative form-group">
                                            <label>Class</label>

                                            <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="position-relative form-group">
                                            <label>Section</label>
                                            <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="position-relative form-group">
                                            <label>Day</label>
                                            <asp:DropDownList ID="ddl_day" runat="server" CssClass="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_day_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>






                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="position-relative form-group">
                                                    <label>Period</label>
                                                    <asp:DropDownList ID="ddl_period" runat="server" CssClass="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_period_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-lg-3">
                                                <div class="position-relative form-group">
                                                    <label>Subject</label>
                                                    <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="position-relative form-group">
                                                    <label>Teacher</label>
                                                    <asp:DropDownList ID="ddl_teacher" runat="server" CssClass="form-select find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="position-relative form-group">

                                                    <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure want to submit?');" OnClick="btn_Submit_Click" Style="padding: 3px 40px; margin-top: 20px;" on />

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="row">

                                            <div runat="server" visible="false" id="grid111">
                                                <table style="width: 100%;" class="table table-hover table-striped table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>Sl. No.</th>
                                                            <th>Class</th>
                                                            <th>Section</th>
                                                            <th>Subject</th>
                                                            <th>Class Period</th>
                                                            <th>Day</th>
                                                            <th>Teacher Name</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RPDetails" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>


                                                                    <td>
                                                                        <asp:Label ID="lbl_classname" runat="server" Font-Names="Arial" Text='<%#Bind("Classname") %>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lbl_section" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_subject" runat="server" Font-Names="Arial" Text='<%#Bind("subjectname") %>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                         <asp:Label ID="lbl_Class_period_name"  runat="server" Font-Names="Arial" Text='<%#Bind("Period_Name") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_Class_period" Visible="false" runat="server" Font-Names="Arial" Text='<%#Bind("Class_period") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Day" runat="server" Font-Names="Arial" Text='<%#Bind("Day") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_teachername" runat="server" Font-Names="Arial" Text='<%#Bind("teachername") %>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                            <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                    <i class="bx bx-grid-horizontal"></i>
                                                                                </div>
                                                                            </a>
                                                                            <ul class="dropdown-menu dropdown-menu-end">




                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Teacher_id" runat="server" Text='<%#Bind("Teacher_id") %>' Visible="false"></asp:Label>


                                                                                <li>


                                                                                    <asp:LinkButton ID="lnkDel" runat="server" CssClass="dropdown-item" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i> <span>Delete</span></asp:LinkButton>
                                                                                </li>

                                                                                <li>

                                                                                    <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click" CausesValidation="false" ToolTip="Edit"><i class="lni lni-pencil-alt"></i><span>Edit</span></asp:LinkButton>

                                                                                </li>
                                                                            </ul>
                                                                        </div>








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
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_subjectid" runat="server" />
</asp:Content>
