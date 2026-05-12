<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="mark-entry.aspx.cs" Inherits="school_web.Examination_Admin.mark_entry1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Marks Entry
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
    <style>
        .table td, .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px !important;
        }

        .modal-backdrop.fade, .fade.blockOverlay {
            opacity: 0;
            display: none;
        }

        .txtbxError {
            border-bottom: 2px solid #ef0000 !important;
            padding: 3px 7px 2px !important;
        }
    </style>
    <script type="text/javascript">
        var arr = new Array();
        function Calculation(ele) {
            var max_mark = parseInt($("#<%=txt_mm.ClientID %>").val()); 
            var txt = $("input[id*='txt_marks']");
            var chk = $("input[id*='chkSelect']");
            var checkedIndex = $(ele).closest('tr').index();


            if ($(ele).closest('tr').find($("input[id*='chkSelect']")).is(':checked')) {
                arr.push(checkedIndex);
            }
            else {
                arr = arr.filter(function (item) {
                    return item !== checkedIndex
                });
            }

            for (var i = 0; i < arr.length; i++) {
                txt[arr[i]].value = i + 1;
                txt[arr[i]].disabled = false;
            }

            for (vari = 0; i < chk.length; i++) {
                if (!chk[i].checked) {
                    var valueess = txt[i].value;

                    if (valueess <= max_mark) {
                        $(txt[i]).removeClass("txtbxError");
                    }
                    else {
                        if (valueess.match(/^-?\d+$/)) {
                            txt[i].value = '';
                            $(txt[i]).addClass("txtbxError");
                        }
                        else {
                            //txt[i].value = '';
                            //$(txt[i]).addClass("txtbxError");
                        }
                    }
                }
            }
        }
    </script>

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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative;">
                <div class="breadcrumb-title pe-3">Marks Entry</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Marks Entry
                                <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print" Style="position: absolute; right: 0px; top: 6px;"><span class="material-symbols-outlined">print</span></asp:LinkButton>
                            </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <%--                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Marks Entry"></asp:Label>
                    <hr />--%>
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-3">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_CourseCat" class="form-select find-dv-txtbx" runat="server" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Section<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_section" class="form-select find-dv-txtbx" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Term<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_term" class="form-select find-dv-txtbx" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Assessment<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_assesment" class="form-select find-dv-txtbx" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_assesment_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-5">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="validationCustom01" class="form-label">Subject<sup>*</sup></label>
                                                        <asp:DropDownList ID="ddl_subject" class="form-select find-dv-txtbx" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="validationCustom01" class="form-label">Subject Activity<sup>*</sup></label>
                                                        <asp:DropDownList ID="ddl_exam_level" class="form-select find-dv-txtbx" runat="server" Style="width: 98%"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-1" style="padding-left: 0px">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" Style="margin: 22px 0px 0px 0px; padding: 4px 10px 4px;"
                                                    OnClick="btn_find_Click" /> 
                                            </div>
                                        </div>
                                    </div>
                                </div>

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
                                                <span style="font-size: 14px; font-weight: bold;">
                                                    <asp:Label ID="lbl_subject" runat="server"></asp:Label></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <hr />
                                            <table style="width: 100%;" id="example1" class="table table-hover table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Student</th>
                                                        <th>Roll No.</th>
                                                        <th>
                                                            <asp:Label ID="lbl_activity_type" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl_max_marks" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txt_mm" Style="display: none" runat="server"></asp:TextBox>
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                                                    (<asp:Label ID="lbl_adm_no" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>)
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkSelect" Style="display: none"
                                                                        runat="server" CssClass="chkbox m-0" Text=" " />

                                                                    <asp:TextBox ID="txt_marks" onblur="Calculation(this.value)" runat="server" class="grd-txtbx-clas"></asp:TextBox>
                                                                    <asp:Label ID="lbl_mark_ids" runat="server" Visible="false" Font-Names="Arial" Text="0"></asp:Label>
                                                                    <asp:LinkButton ID="lnk_remarks" runat="server" class="grd-remks" OnClick="lnk_remarks_Click"><i class="fa fa-ellipsis-v fa-w-6"></i></asp:LinkButton>

                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-xs-10 col-sm-3 col-md-12 col-lg-12">
                                        <asp:Button ID="btn_save" runat="server" class="btn btn-sm btn-success" Text="Save" Style="margin: 0px 0px 0px 0px; padding: 6px 10px 8px; float: right;"
                                            OnClick="btn_save_Click" />
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


    <div class="modal fade show" id="myModalSS" tabindex="-1" visible="false" runat="server" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: block; padding-right: 5px;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Remark Type</h5>
                    <asp:LinkButton ID="lnk_close_popup" OnClick="lnk_close_popup_Click" class="close" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        
                    </button>--%>
                </div>
                <div class="modal-body">
                    <asp:DropDownList ID="ddl_remarks" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_save_rmrks" runat="server" class="btn btn-primary" Text="Save" OnClick="btn_save_rmrks_Click" />
                </div>
            </div>
        </div>
    </div>

    <style>
        .grd-txtbx-clas {
            margin: 0px 10px 0px 0px;
            padding: 3px 7px;
            border: 0px;
            border-bottom: 1px solid #6208d5;
            background: rgb(255 255 255 / 0%);
            width: 100px;
            float: left;
        }

        .grd-remks {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            border: 0px;
            font-size: 16px;
            color: #6208d5;
        }

        .modal {
            background: rgb(0 0 0 / 39%);
        }
    </style>
</asp:Content>
