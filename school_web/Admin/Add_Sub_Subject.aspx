<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_Sub_Subject.aspx.cs" Inherits="school_web.Admin.Add_Sub_Subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Sub-Subject
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 99999;
            /* float: left; */
            width: 100%;
        }

        .waiting {
            padding: 15px 15px 15px 14px;
            font-size: 16px;
            bottom: 0px;
            left: 1px;
            top: 300px;
            background: #fff0;
            color: #1a1313;
            height: 55px!important;
            z-index: 100000;
            font-size: 17px;
            text-align: center;
            width: 99.8%;
            position: fixed;
        }
          .head {
            display: none;
        }
    </style>
    <link href="../assets/css/Print.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/app.css" rel="stylesheet" type="text/css" />');
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

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
                                        <li class="breadcrumb-item active" aria-current="page">Add Sub-Subject</li>
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
                                                <div class="row">


                                                    <div class="find-dv">
                                                        <div class="row">
                                                            <div class="col-sm-3" id="storeDv" runat="server">
                                                                <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                                                <asp:DropDownList ID="ddl_course_search" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_course_search_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">

                                                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                                    ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                            </div>


                                                            <div class="col-sm-7">
                                                                <div style="margin: 0px; padding: 0px; float: left; height: 50px; width: 100%;">
                                                                    <a onclick="openModal()">
                                                                        <img src="../assets/images/add_subject.png" style="height: 50px; width: 50px; float: right;" /></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-12">

                                                        <div id="tblPrintIQ" runat="server">
                                                            <div class="prnt-dv-wpr printborder">
                                                                 <div class="pgslry-head-div head">

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
                                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                            Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                            &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                            Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                        </div>
                                                                    </div>


                                                                </div>
                                                                     </div>

                                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                    <thead>

                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Class Name</th>
                                                                            <th>Subject Name</th>
                                                                            <th>Sub Subject Name</th>
                                                                            <th>Sub Subject Position</th>
                                                                            <th>Action</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rd_view" runat="server">
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
                                                                                            <asp:Label ID="lbl_Sub_Subject_Master" runat="server" Text='<%#Bind("Sub_Subject_name")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Subject_position" runat="server" Text='<%#Bind("Sub_Subject_position")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                            <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id")%>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("course_id")%>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="lbl_Sub_Subject_id" runat="server" Text='<%#Bind("Sub_Subject_id")%>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </asp:Panel>
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
                    <%--------------------------------------------------------------%>

                    <style>
                        @media (min-width: 576px) {
                            .modal-dialog {
                                max-width: 916px;
                                margin: 1.75rem auto;
                            }
                        }
                    </style>
                    <!-------popupadd year----->
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Add Sub-Subject Details</h5>
                                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                                </div>
                                <div class="modal-body">

                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Class Name <sup>* </sup></label>

                                                <asp:DropDownList ID="ddl_course" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_course_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Subject Name <sup>* </sup></label>

                                                <asp:DropDownList ID="ddl_subject" runat="server" class="form-control">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Sub-Subject Name <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_subjectsubject"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_subjectsubject" runat="server" class="form-control"></asp:TextBox>

                                            </div>

                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Subject Position(1,2,3)<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_subjectposition"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_subjectposition" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>






                                            <div class="col-12">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return close()" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="fadeup"></div>

                    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
                    <script src="../Autocomplete/jquery-ui.js"></script>
                    <script type="text/javascript">
                        function openModal() {
                            $("#myModal").show();
                            $('#myModal').addClass('show');
                            $('#fadeup').addClass('modal-backdrop fade show');
                        }
                        function close() {
                            $("#myModal").hide();
                            $('#myModal').removeClass('show');
                            $('#fadeup').removeClass('modal-backdrop fade show');
                        }
                    </script>
                    <asp:HiddenField ID="hd_id" runat="server" />
                    <asp:HiddenField ID="hd_subjectid" runat="server" />
            </ContentTemplate>
            <Triggers>
                <%--  <asp:PostBackTrigger ControlID="imgexcel2" />--%>
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress2"
            runat="server" AssociatedUpdatePanelID="UpdatePanel2"
            DynamicLayout="False">
            <ProgressTemplate>
                <p class="waiting">
                    &nbsp;&nbsp;&nbsp;
                                            <img src="../images/Processing.gif" />

                </p>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>
