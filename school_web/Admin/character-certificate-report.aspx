<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="character-certificate-report.aspx.cs" Inherits="school_web.Admin.character_certificate_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Reprint Character Certificate 
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
                <div class="breadcrumb-title pe-3"><a href="certificate.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Certificate</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Reprint Character Certificate</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">

                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Reprint Character Certificate"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="table-responsive" style="margin: 10px 0px 0px 0px">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Student Name</th>
                                                            <th>Father's Name</th>
                                                            <th>Adm No.</th>
                                                            <th>Class</th>
                                                            <th>Section</th>
                                                            <th>Roll No.</th>
                                                            <th>Session</th>
                                                            
                                                            <th>Certificate No.</th>
                                                            <th>Create Date</th>
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
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Certificate_no" runat="server" Text='<%#Bind("Certificate_no")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Create_date")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <a style="background: #f00; padding: 2px 5px 1px 5px; display: inline-flex; color: #fff; border-radius: 2px;" href="slip/character-certificate.aspx?adm_no=<%#Eval("admissionserialnumber") %>&clssid=<%#Eval("Class_id") %>&sessnid=<%#Eval("Session_id") %>&crtificateno=<%#Eval("Certificate_no") %>" target="_blank"><i class='bx bx-printer'></i><span>Print</span> </a>
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
        <!--end row-->
    </div>
</asp:Content>
