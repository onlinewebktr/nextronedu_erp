<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="print-SLC-certificate.aspx.cs" Inherits="school_web.Admin.print_SLC_certificate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Print School Leaving Certificate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);


        
        function myMdlCertificateP_8() {
            $('#myMdlCertificate_8').modal('show');
         }

         function openModalTEN() {
             $('#ModalTCTEN').modal('show');
         }
     </script>
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }

        .rowdevider {
            margin: 0px;
            padding: 5px 0px 5px 0px;
            width: 100%;
            float: left;
            border-bottom: 1px solid #ddd;
        }

        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, -40px);
            }
    </style>
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
                <div class="breadcrumb-title pe-3"><a href="certificate-master.aspx" runat="server" id="backbtns" class="backlnk-css"><i class="bx bx-arrow-back"></i>Certificate</a></div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Reprint School Leaving Certificate</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="hd_tc_type" runat="server" />
            <div class="row">
                <div class="col-xl-12">

                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Reprint School Leaving Certificate"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="table-responsive" style="margin: 10px 0px 0px 0px">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                            </div> 
                                            <div class="col-sm-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                                <asp:TextBox ID="txt_admission_no" runat="server" class="form-control txtbx-ddl-style"></asp:TextBox>
                                            </div> 
                                            <div class="col-sm-1">
                                                <asp:Button ID="btn_find_by_admission_no" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_admission_no_Click" />
                                            </div> 
                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="float: right" class="btn btn-primary find-dv-btn" Visible="false">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" class="btn btn-primary find-dv-btn" style="float: right;"  Visible="false" ><i class="bx bx-printer" style="padding:0px 0px 0px 0px"></i>  Print</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div id="content">

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
                                                                        &nbsp;&nbsp; 
                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label> 
                                                                    </div>
                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <span style="font-size: 14px; font-weight: bold;">SLC Certificate 
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span> 
                                                                    </div>
                                                                </div> 
                                                            </div>

                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th class="noPrint">Action</th>
                                                                            <th>Student Name</th>
                                                                            <th>Father's Name</th>
                                                                            <th>Adm No.</th>
                                                                            <th>Class</th>
                                                                            <th>Section</th>
                                                                            <th>Roll No.</th>
                                                                            <th>Session</th>
                                                                            <th>Certificate No.</th>
                                                                            <th>Create Date</th>
                                                                            <th></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="noPrint">
                                                                                        <asp:LinkButton ID="lnk_edit" runat="server" OnClick="lnk_edit_Click"><span class="material-symbols-outlined">edit_square</span></asp:LinkButton>
                                                                                        <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_certificate_nos" Visible="false" runat="server" Text='<%#Bind("Certificate_no")%>'></asp:Label>
                                                                                        <a id="tcc_link" runat="server" target="_blank"><span class="material-symbols-outlined">print</span></a>
                                                                                        <%--<a style="background: #f00; padding: 2px 5px 1px 5px; display: inline-flex; color: #fff; border-radius: 2px;" href="slip/transfer-certificate.aspx?adm_no=<%#Eval("admissionserialnumber") %>&clssid=<%#Eval("Class_id") %>&sessnid=<%#Eval("Session_id") %>&crtificateno=<%#Eval("Certificate_no") %>" target="_blank"><i class='bx bx-printer'></i><span>Print</span> </a>--%>
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
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Certificate_no" runat="server" Text='<%#Bind("Certificate_no")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Create_date")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnk_delete" runat="server" OnClick="lnk_delete_Click" OnClientClick="return confirm('Are you sure you want to delete this?');" Style="background-color: #f7f100; min-width: 30px; color: #000;"
                                                                                            class="button-61 nowordbreak collect-feesss"><span class="material-symbols-outlined">delete</span></asp:LinkButton>
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
      

   <script>





       $(function () {
           $("#<%=txt_date_of_application_8.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
            });
        });


       $(function () {
           $("#<%=txt_date_of_issue_certificate_8.ClientID %>").datepicker({
                  dateFormat: "dd/mm/yy",
                  changeMonth: true,
                  changeYear: true,
                  yearRange: "2020:2030",
              });
        });
        $(function () {
            $("#<%=txt_date_of_admission_8.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
            });
        }); 
   </script>
     <div id="myMdlCertificate_8" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 961px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Create Certificate</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <div class="ttpp-wprs" style="height: auto; overflow: inherit;">
                            <table style="width: 100%;" id="Table211" class="table table-hover table-bordered ">
                                <tr>
                                    <th>Student Name</th>
                                    <th>Gender</th>
                                    <th>Admission No.</th>
                                    <th>Dob</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Roll</th>
                                    <th>Session</th>
                                    <th>Father's Name</th>
                                    <th>Mother's Name</th>
                                    <th>Address</th>
                                </tr>

                                <asp:Repeater ID="Repeater_8" runat="server">
                                    <ItemTemplate>
                                        <tr id="row" runat="server">
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_gender" runat="server" Text='<%#Bind("gender") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_don" runat="server" Text='<%#Bind("dob") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                                <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_mothername" runat="server" Text='<%#Bind("mothername")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_careof" runat="server" Text='<%#Bind("careof")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>

                             <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Dob</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_dob_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">PEN No.</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_pen_no_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether the candidate belongs to schedule caste or schedule trible or OBC</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_belong_to_sc_st_obc_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of first admission in the school with Class</label>
                                    </div>
                                    <div class="col-xl-4 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_admission_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-xl-2 padd-lft-5">
                                        <asp:DropDownList ID="ddl_class_c_8" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Class in which the pupil last studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        
                                         <asp:TextBox ID="txt_class_in_last_studied_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">School/Board Annual examination last taken with result</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_annual_exam_taken_with_result_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether failed, if so once/twice in the same class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_whether_failed_in_same_class_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Subject studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_subject_studied_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether qualified for promotion to the higher class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_whether_qualified_for_promotion_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">if so, to which class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        
                                         <asp:TextBox ID="txt_permote_class" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Month Upto which the pupil has paid school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_monthuptopaid_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                               <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any fee concession availed of. if so, the nature of such concession</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_concession" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total working days in the academic session</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ttl_working_days_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div> 
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total no. of days pupil present in the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_present_days_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                             <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether NCC Cadet/Boy Scout/Girl Guide </label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ncc_cadet_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Games played or extra curricular activites in which the pupil usually took part </label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_game_played_of_extra_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">General conduct</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_general_conduct_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                           

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of application for certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_application_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of issue for certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_issue_certificate_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Reason for leaving the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_reason_for_leaving_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%-- ==================================== --%>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any other remarks</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_any_other_remarks_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xl-12 padd-lft-5" style="text-align:center">
                                <asp:Button ID="btn_create_certificate_8" runat="server" Text="Update" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_create_certificate_8_Click" Style="margin: 8px 0px 10px -4px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalTCTEN" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Create Certificate</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <div class="ttpp-wprs" style="height: auto; overflow: inherit;">
                            <table style="width: 100%;" class="table table-hover table-bordered ">
                                <tr>
                                    <th>Session</th>
                                    <th>Student Name</th>
                                    <th>Admission No</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Roll</th>
                                    <th>Father's Name</th>
                                </tr>
                                <asp:Repeater ID="rp_trnsf_six" runat="server">
                                    <ItemTemplate>
                                        <tr id="row" runat="server">
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                                <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>


                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of Birth (in Christian Era) according to Admission & Withdrawal Register (in figures)</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_dob_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of Birth (in Word)</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_dob_in_word" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Nationality</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_nationality_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether the candidate belongs to SC/ST/OBC</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_belongs_to_scst_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of first admission in the school with class</label>
                                    </div>
                                    <div class="col-xl-4 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_adm_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-xl-2 padd-lft-5">
                                        <asp:TextBox ID="txt_adm_class_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Class in which the pupil last studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_class_last_studies_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">School/Board Annual Examination last taken with result</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_school_board_exam_taken_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether failed, if so once/twice in the same class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_failed_once_twice_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Subject Studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_subj_studies_ten" runat="server" TextMode="MultiLine" class="form-control find-dv-txtbx" Style="height: 150px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether qualified for promotion to the higher class if so, to witch class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_qualified_higher_class_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Month upto which the pupil has paid school dues</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_paid_fee_till_month_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any fee concession availed of. If so, the nature of such concession</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_any_fee_concession_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total no. of working days in the academic session</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ttl_no_of_workind_days_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total no. of working days pupil present in the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ttl_persent_days" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether NCC Cadet/Boy Scout/Girl Guide</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ncc_cadet_by_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Games played or extra curricular activites in which the pupil usually took apart</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_games_played" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">General conduct</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_general_conduct_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of application for certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_application_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of issue of certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_issue_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Reason for leaving the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_reason_for_leaving_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any other remarks</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_any_other_remarks_ten" runat="server" TextMode="MultiLine" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-6 padd-lft-5">
                                <asp:Button ID="btn_make_tcTen_update" runat="server" Text="Update" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_make_tcTen_update_Click" Style="margin: 8px 0px 10px -4px;" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
