<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="admit-card-guidline.aspx.cs" Inherits="school_web.Examination_Admin.admit_card_guidline" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Admit Card Guidline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="lt_meata" runat="server"></asp:Literal> 
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
                <div class="breadcrumb-title pe-3">Admit Card</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Admit Card Guidline</li>
                        </ol>
                    </nav>
                </div>
            </div> 

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <%--<asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Admit Card Guidline"></asp:Label>
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
                                        <asp:DropDownList ID="ddl_CourseCat" class="form-select find-dv-txtbx" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                     
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Exam<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_exam" class="form-select find-dv-txtbx" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_exam_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-xl-12">
                                        <label for="validationCustom01" class="form-label">Enter Guideline<sup>*</sup></label>
                                        <script type="text/javascript">
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
                                        <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 500px; width: 100%" oncopy="return false" onpaste="return false"></textarea>
                                    </div> 
                                    <div class="col-12">
                                        <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_add_Click" />
                                    </div>
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .tox-tinymce {
            height: 400px !important;
            width: 100% !important;
        }
    </style>
</asp:Content>
