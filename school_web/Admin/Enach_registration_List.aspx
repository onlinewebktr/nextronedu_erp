<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Enach_registration_List.aspx.cs" Inherits="school_web.Admin.Enach_registration_List"  EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Enach Registration List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Enach_registration_List.aspx/GetRooPathAdmNo',
                        data: "{ 'PathRooT': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });



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


    <style type="text/css">
        table tr th input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 22px;
            height: 22px;
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
            width: 20px;
            height: 20px;
            border: 1px solid #646464;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        .form-control + .form-control {
            margin-top: 1em;
        }

        .wrapper.toggled:not(.sidebar-hovered) .sidebar-wrapper {
            width: 70px;
        }

        table.dataTable > thead > tr > th:not(.sorting_disabled), table.dataTable > thead > tr > td:not(.sorting_disabled) {
            padding-right: inherit;
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
                <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" runat="server" id="backbtns" class="backlnk-css"><i class="bx bx-arrow-back"></i>Reports</a></div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Enach Registrtion List</li>
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
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx"  A ></asp:DropDownList>
                                                </div>
                                               
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                                    <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find_by_admission_no" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_admission_no_Click" />
                                                </div>

                                                <div class="col-sm-3">
                                                    <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="pgslry-head-div head" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
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
                                                            <span style="font-size: 14px; font-weight: bold;">Enach Registartion List</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <table id="datatable" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                              
                                                                <th>Session</th>
                                                                <th>Adm. No.</th>
                                                                <th>Class</th>
                                                                <th>Section</th>
                                                                <th>Roll No.</th>
                                                                <th>Student Name</th>
                                                                <th>Father Name</th>
                                                                 
                                                                <th>Mobile No.</th>
                                                                 <th>Father Email Id</th>
                                                                  <th>Status</th>
                                                                    
                                                                  <th>Max Debit Amount</th>
                                                                  <th>Start Date</th>
                                                                 <th>End Date</th>
                                                                <th>Txn Id</th>
                                                                <th>

                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr id="trR" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </td>


                                                                        <td>
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                                           
                                                                            <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                                                                        </td>
                                                                          <td>
                                                                            <asp:Label ID="Label8" runat="server" Text='<%#Bind("email_id")%>'></asp:Label>
                                                                           
                                                                           
                                                                        </td>
                                                                        
                                                                          <td>
                                                                            <asp:Label ID="lbl_Status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                           
                                                                           
                                                                        </td>
                                                                         
                                                                        
                                                                          <td>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("totalamount")%>'></asp:Label>
                                                                           
                                                                           
                                                                        </td>

                                                                           <td>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("debitStartDate")%>'></asp:Label>
                                                                           
                                                                           
                                                                        </td>
                                                                        
                                                                           <td>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("debitEndDate")%>'></asp:Label>
                                                                           
                                                                           
                                                                        </td>
                                                                          <td>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("txnId")%>'></asp:Label>
                                                                           
                                                                           
                                                                        </td>
                                                                        <td>
                                                                             <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn" OnClick="lnkEdit_Click" CausesValidation="false">Approved</asp:LinkButton>
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
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
