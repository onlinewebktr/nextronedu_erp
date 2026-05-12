<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_Add_Program_Parameter.aspx.cs" Inherits="school_web.Admin.Scholarship_Add_Program_Parameter" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Scholarship Program Parameter
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
 <%--   <script src="https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>--%>
        <asp:Literal ID="lt_meata" runat="server"></asp:Literal> 

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
        .form-label {
            margin-bottom: .5rem;
            font-weight: bold !important;
        }

        .form-select {
            padding: 5px 5px 4px 6px;
            font-size: 14px;
        }
    </style>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_start_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2022:2027",

            }).attr("readonly", "true");
        });
    </script>

    <script>
        $(function () {
            $("#<%=txt_end_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2022:2027",

            }).attr("readonly", "true");
        });



    </script>
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
                <div class="breadcrumb-title pe-3">Scholarship </div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Scholarship Program Parameter</li>
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
                                    <div class="col-md-15" style="margin: 10px 0px 0px 0px !important; padding: 0px 0px 0px 10px; float: left;">

                                        <label class="form-label Llabel">
                                            <asp:Label ID="lbl_edit_dis" Style="font-size: 12px; color: red" runat="server" Text="You can't change class name at the time of editable"></asp:Label></label>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label Llabel">Session</label>
                                        <asp:DropDownList ID="ddl_session" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label Llabel">Scholarship Name</label>
                                        <asp:DropDownList ID="ddl_Scholorship_Name" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label Llabel">Scholarship for </label>
                                        <asp:DropDownList ID="ddl_Scholorshipclass" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Fee <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_coursefee"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_coursefee" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">
                                            Max Application Form Allowed
 <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_no_application"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_no_application" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Start Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_start_date"></asp:RequiredFieldValidator></sup></label>
                                        <div class="clndr-div">
                                            <asp:TextBox ID="txt_start_date" runat="server" class="form-control"></asp:TextBox>
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">End Date<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_end_date"></asp:RequiredFieldValidator></sup></label>
                                        <div class="clndr-div">
                                            <asp:TextBox ID="txt_end_date" runat="server" class="form-control"></asp:TextBox>
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Payment Mode <sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_payment_mode" runat="server" class="form-select">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Offline</asp:ListItem>


                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <label for="validationCustom01" class="form-label">
                                                    Scholarship Guidelines

                                                </label>
                                                <p class="txtbx-name-p" id="title_textS" runat="server" style="color: #f00; font-weight: 700; font-size: 17px;"></p>
                                            </div>

                                        </div>



                                       <script>
                                           $(function () {
                                               tinymce.init({
                                                   selector: $('#<%=txt_info.ClientID%>').selector,
                                                    width: 600,
                                                    height: 300,
                                                    plugins: [
                                                        'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
                                                        'searchreplace', 'wordcount', 'visualblocks', 'code', 'fullscreen', 'insertdatetime', 'media',
                                                        'table', 'emoticons', 'help'
                                                    ],
                                                    toolbar: 'undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | ' +
                                                        'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
                                                        'forecolor backcolor emoticons | help',
                                                    menu: {
                                                        favs: { title: 'My Favorites', items: 'code visualaid | searchreplace | emoticons' }
                                                    },
                                                    menubar: 'favs file edit view insert format tools table help',
                                                    content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }'
                                                });
                                            });
                                       </script>
                                        <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 500px; width: 100%"></textarea>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <label for="validationCustom01" class="form-label">
                                                    Scholarship Benefits


                                                </label>
                                                <p class="txtbx-name-p" id="P1" runat="server" style="color: #f00; font-weight: 700; font-size: 17px;"></p>
                                            </div>

                                        </div>


                                           <script>
                                               $(function () {
                                                   tinymce.init({
                                                       selector: $('#<%=txt_Scholorship_Benefit.ClientID%>').selector,
                                                    width: 600,
                                                    height: 300,
                                                    plugins: [
                                                        'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
                                                        'searchreplace', 'wordcount', 'visualblocks', 'code', 'fullscreen', 'insertdatetime', 'media',
                                                        'table', 'emoticons', 'help'
                                                    ],
                                                    toolbar: 'undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | ' +
                                                        'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
                                                        'forecolor backcolor emoticons | help',
                                                    menu: {
                                                        favs: { title: 'My Favorites', items: 'code visualaid | searchreplace | emoticons' }
                                                    },
                                                    menubar: 'favs file edit view insert format tools table help',
                                                    content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }'
                                                });
                                            });
                                           </script>
                                       
                                        <textarea id="txt_Scholorship_Benefit" runat="server" name="area" class="form-control" style="min-height: 500px; width: 100%"></textarea>
                                    </div>





                                    <div class="col-12">
                                        <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_add_Click" />


                                        <asp:Button ID="btn_back" runat="server" Text="Back" CssClass="btn-dark" OnClick="btn_back_Click" />
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



</asp:Content>
