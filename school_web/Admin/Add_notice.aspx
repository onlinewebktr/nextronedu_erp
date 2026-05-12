<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_notice.aspx.cs" Inherits="school_web.Admin.Add_notice" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Notice
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="lt_meata" runat="server"></asp:Literal>
    <style>
        input, textarea {
            margin: 0;
            width: 26px !important;
            height: 40px !important;
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
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
                <div class="breadcrumb-title pe-3">Online Reg.</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Notice</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase" style="position: relative;">Add Notice<a href="Online_Notic_List.aspx" class="add-forpopup-btn">Notice List</a></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Date<sup>*</sup></label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_date" runat="server" class="form-control find-dv-txtbx" Style="width: 100%!important;"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Notice Heading<sup>*</sup></label>
                                        <asp:TextBox ID="txt_heading" runat="server" class="form-control" Style="width: 100%!important;"></asp:TextBox>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Notice Details<sup>*</sup></label>
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


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">File(<sup> jpg,png,pdf-FIle Size-300KB </sup>)  </label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" Style="width: 100%!important;" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                            runat="server" ControlToValidate="FileUpload1"
                                            ErrorMessage="Invalid File. Please upload a File with extension: png, jpg" ForeColor="Red"
                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf)$"
                                            ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="col-3">

                                    <asp:Button ID="btn_find" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_find_Click" Style="margin: 19px 0px 0px 0px; padding: 6px 10px; width: 70px!important; height: 37px!important;" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });


    </script>

    <style>
        .tox-tinymce {
            height: 400px !important;
            width: 100% !important;
        }
    </style>

</asp:Content>
