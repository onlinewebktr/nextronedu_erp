<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Set_Attendance_Student_Term_Wise.aspx.cs" Inherits="school_web.Examination_Admin.Set_Attendance_Student_Term_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Term Wise Make Attendance
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

            var TTLClass = $("input[id*='txt_no_of_class']");
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


                TTLClass[arr[i]].value = i + 1;
                TTLClass[arr[i]].disabled = false;
            }

            for (vari = 0; i < chk.length; i++) {
                if (!chk[i].checked) {
                    var valueess = parseFloat(txt[i].value);
                    var max_value = parseFloat(TTLClass[i].value);
                    //alert(max_value);
                    //alert(valueess);


                    if (valueess <= max_value) {
                        //alert("true");
                        $(txt[i]).removeClass("txtbxError");
                    }
                    else {
                        //alert("false");
                        txt[i].value = '';
                        $(txt[i]).addClass("txtbxError");
                    }
                }
            }
        }

        function openModal() {
            $('#myModal').modal('show'); 
        }
    </script>


    <style>
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .mdl-close-btn {
            margin: 0px;
            padding: 0px 5px 0px 5px;
            border: 0px;
            background: #ed0000;
            font-size: 18px;
            color: #fff;
            line-height: 25px;
            border-radius: 2px;
        }

        .modal-header {
            padding: 7px 15px;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position:relative">
                <div class="breadcrumb-title pe-3">Marks Entry</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Term Wise Make Attendance</li>
                        </ol>
                    </nav>
                </div>

                <a href="#" data-toggle="modal" data-target="#myModal" style="float: right; position: absolute; right: 0px; font-size: 23px; top: 2px;"><i class="bx bx-cog"></i></a>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <%--<asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Marks Entry"></asp:Label>
                    <hr />--%>
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_CourseCat" class="form-select find-dv-txtbx" runat="server" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Section<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_section" class="form-select find-dv-txtbx" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Term<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_term" class="form-select find-dv-txtbx" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" Style="margin: 22px 0px 0px 0px; padding: 4px 10px 4px;"
                                            OnClick="btn_find_Click" />
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
                                                    <th>Total No. of class</th>

                                                    <th>Enter No. of class attend
                                                         <asp:Label ID="lbl_max_marks" runat="server" Style="display: none"></asp:Label>
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
                                                                <asp:Label ID="lbl_no_of_class" runat="server" Font-Names="Arial" Text='<%#Bind("No_of_Class") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:TextBox ID="txt_no_of_class" onblur="Calculation(this.value)" runat="server" Text='<%#Bind("No_of_Class") %>' class="grd-txtbx-clas" AutoPostBack="false" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </td>

                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" Style="display: none"
                                                                    runat="server" CssClass="chkbox m-0" Text=" " />
                                                                <asp:TextBox ID="txt_marks" onblur="Calculation(this.value)" runat="server" class="grd-txtbx-clas" OnTextChanged="txt_marks_TextChanged" AutoPostBack="false" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

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
        </div>
        <!--end row-->
    </div>


    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Fetch Attendance</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_session_p" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_CourseCat_p" runat="server" class="form-select" OnSelectedIndexChanged="ddl_CourseCat_p_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_section_p" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Term</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_term_p" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>



                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_from_date" runat="server" class="form-control"></asp:TextBox>


                                <script>
                                    $(function () {
                                        $("#<%=txt_from_date.ClientID %>").datepicker({
                                            dateFormat: "dd/mm/yy",
                                            changeMonth: true,
                                            changeYear: true,
                                            yearRange: "2021:2034",
                                            maxDate: '0',
                                        }).attr("readonly", "true");
                                    });
                                </script>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_to_date" runat="server" class="form-control"></asp:TextBox>
                                <script>
                                    $(function () {
                                        $("#<%=txt_to_date.ClientID %>").datepicker({
                                            dateFormat: "dd/mm/yy",
                                            changeMonth: true,
                                            changeYear: true,
                                            yearRange: "2021:2034",
                                            maxDate: '0',
                                        }).attr("readonly", "true");
                                    });
                                </script>
                            </div>
                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_fetch_attendance" runat="server" Text="Submit" class="btn btn-primary" OnClick="btn_fetch_attendance_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
