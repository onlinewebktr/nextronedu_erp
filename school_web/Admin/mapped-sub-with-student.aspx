<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="mapped-sub-with-student.aspx.cs" Inherits="school_web.Admin.mapped_sub_with_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Allocated Subject List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/Print.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
    <style>
        .head {
            display: none;
        }

        .pgslry-head-div {
            margin-top: 10px;
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
                <div class="breadcrumb-title pe-3">Subject</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Allocated Subject List</li>
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
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">

                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-3" id="Div1" runat="server">
                                                        <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Session</label>
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-control find-dv-txtbx" AutoPostBack="false" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3" id="storeDv" runat="server">
                                                        <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                                        <asp:DropDownList ID="ddl_course_search" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_course_search_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3" id="Div2" runat="server">
                                                        <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Section </label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>


                                                    </div>
                                                    <div class="col-sm-3" id="Div3" runat="server">
                                                        <div class="noPrint">
                                                            <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" Style="background-color: #009f25; border: 5px solid #009f25; cursor: pointer; float: none; display: inline-block; width: 33px; height: 32px; margin: 21px 0px 0px 11px;"
                                                                ToolTip="Print"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">
                                                    <div class="pgslry-head-div head">

                                                        <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                            <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                        </div>
                                                        <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                            <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                            </h1>

                                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                            </div>
                                                            <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>




                                                            </div>
                                                            <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                            </div>
                                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                <span style="font-size: 14px; font-weight: bold;">Allocated Subject List-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                            </div>
                                                        </div>


                                                    </div>
                                                    <table id="example21" class="table table-bordered dataTable" role="grid" aria-describedby="example2_info" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Student Name</th>
                                                                <th>Admission No</th>
                                                                <th>Session</th>
                                                                <th>Class</th>
                                                                <th>Section</th>
                                                                <th>Roll No.</th>

                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="background-color: #d0c4c4!important;">
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; background-color: #d0c4c4!important;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; background-color: #d0c4c4!important;">

                                                                            <asp:Label ID="lbl_adm_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; background-color: #d0c4c4!important;">
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; background-color: #d0c4c4!important;">
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; background-color: #d0c4c4!important;">
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; background-color: #d0c4c4!important;">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="background-color: #d0c4c4!important;">
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>

                                                                        </td>
                                                                    </tr>


                                                                    <tr>


                                                                        <td colspan="8" style="padding: 3px 0px 0px 51px; border-top: 1px solid #000; border-bottom: 1px solid #000;">
                                                                            <asp:Repeater ID="rd_subject_view" runat="server">
                                                                                <ItemTemplate>
                                                                                    <span style="padding: 1px 8px 0px 1px;">
                                                                                        <asp:Label ID="Label1" Style="font-weight: 600" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>. 
                                                                                <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                                    </span>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
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
</asp:Content>
