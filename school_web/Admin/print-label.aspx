<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="print-label.aspx.cs" Inherits="school_web.Admin.print_label" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Print Label
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
                <div class="breadcrumb-title pe-3">Admission Details</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Print Label</li>
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
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Student Type</label>
                                                        <asp:DropDownList ID="ddl_studenttype" runat="server" class="form-control find-dv-txtbx">
                                                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                            <asp:ListItem Value="New">New Admission</asp:ListItem>
                                                            <asp:ListItem Value="NT">Old Admission</asp:ListItem>
                                                            <asp:ListItem Value="Transferred">Transferred to Next Session</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />


                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Admission Type</label>
                                                        <asp:DropDownList ID="ddl_days_hostel" runat="server" class="form-control find-dv-txtbx">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <%--<asp:ListItem Value="Yes">Hostel</asp:ListItem>--%>
                                                            <%--<asp:ListItem Value="No">Day Scholar</asp:ListItem> --%>
                                                            <asp:ListItem Value="1">Hostel</asp:ListItem>
                                                            <asp:ListItem Value="2">Day Scholar</asp:ListItem>
                                                            <asp:ListItem Value="3">Day Boarding with Lunch</asp:ListItem>
                                                            <asp:ListItem Value="4">Bus</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_fnd_by_days" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_days_Click" />
                                                    </div>

                                                    <div class="col-sm-5">
                                                        <asp:LinkButton ID="lnk_print_label" OnClick="lnk_print_label_Click" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server" ToolTip="Print"><i class='bx bx-printer'></i> Print Label</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>


                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr printborder">

                                                    <div class="grd-wpr">
                                                        <table id="example24" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th></th>
                                                                    <th>Admission No.</th> 
                                                                    <%--<th>Current Session Date</th>--%>
                                                                    <%--<th>Admission Type</th>--%>
                                                                    <th>Class</th>
                                                                    <th>Session</th>
                                                                    <th>Section</th>
                                                                    <th>Roll No.</th>
                                                                    <%--<th>Pwd</th>--%>
                                                                    <th>Student Name</th>
                                                                    <th>Mobile No</th>
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
                                                                                <asp:CheckBox ID="rowChkBox" runat="server" />
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                            </td>

                                                                            <%--<td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Old_Admission_Date" runat="server" Text='<%#Bind("Old_Admission_Date")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                            </td>--%>

                                                                            <%--<td style="text-align: left;">
                                                                                <asp:Label ID="lbl_admissiontype" runat="server" Text='<%#Bind("Admission_Type")%>'></asp:Label>
                                                                            </td>--%>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                            </td>
                                                                            <%--<td style="text-align: left;">
                                                                                <asp:Label ID="lbl_pwd" runat="server" Text='<%#Bind("Pwd")%>'></asp:Label>
                                                                            </td>--%>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                            </td>


                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_mobile2" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
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
                                                                                        <%-- <li>
                                                                                            <a class="dropdown-item" href="student-full-details.aspx?admNo=<%#Eval("admissionserialnumber") %>&ssion=<%#Eval("Session_id") %>&clss=<%#Eval("Class_id") %>" target="_blank"><i class='bx bx-happy-heart-eyes'></i><span>View Student Details</span> </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a class="dropdown-item" href="student-details.aspx?admNo=<%#Eval("admissionserialnumber") %>&ssion=<%#Eval("Session_id") %>&clss=<%#Eval("Class_id") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Student Details</span></a>
                                                                                        </li>--%>
                                                                                        <li>
                                                                                            <a class="dropdown-item" href="slip/student-label.aspx?printfrom=0&type=1&session_id=<%#Eval("Session_id") %>&class_id=<%#Eval("Class_id") %>&adm_no=<%#Eval("admissionserialnumber") %>&section=<%#Eval("Section") %>&prinTtype=1" target="_blank"><i class='bx bx-printer'></i><span>Print Label</span></a>
                                                                                        </li>
                                                                                        
                                                                                    </ul>
                                                                                </div>

                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>' Visible="false"></asp:Label>
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
        </div>
        <!--end row-->
    </div>


    <style>
        table tr th input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 1.15em;
            height: 1.15em;
            border: 0.15em solid currentColor;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        table tr th label {
            width: 100%;
        }

        table tr td input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 25px;
            height: 25px;
            border: 1px solid #646464;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        .form-control + .form-control {
            margin-top: 1em;
        }

        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #0c6eff !important;
            text-align: center;
            font-size: 13px;
            color: #fff;
        }

        tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            text-align: center;
            font-size: 13px;
        }

        .wrapper.toggled:not(.sidebar-hovered) .sidebar-wrapper {
            width: 70px;
        }
    </style>
</asp:Content>
