<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Online_Terms_Condition.aspx.cs" Inherits="school_web.Admin.Online_Terms_Condition" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Online Registration Guideline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="lt_meata" runat="server"></asp:Literal> 
    <style>
        .tox-tinymce {
            height: 400px !important;
            width: 100% !important;
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
                <div class="breadcrumb-title pe-3">Online Registration</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Registration Guideline</li>
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
                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label Llabel">Session</label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label Llabel">Class</label>
                                        <span class="chkbx-all">
                                            <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" />
                                        </span>
                                        <br />
                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                                <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>


                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <label for="validationCustom01" class="form-label">Terms & Condition</label>
                                                <p class="txtbx-name-p" id="title_textS" runat="server" style="color: #f00; font-weight: 700; font-size: 17px;"></p>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label" style="float: left;">View Guideline</label>
                                                <asp:DropDownList ID="ddl_class" Style="width: 52%; float: left; margin: 0px 0px 0px 10px; height: 28px; padding: 0px 5px 0px 5px;"
                                                    runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                                </asp:DropDownList>
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
                                    <div class="col-xl-4">
                                        <div class="txtbx-wraper">
                                            <label for="validationCustom01" class="form-label">Attachment(pdf,jpg)</label>

                                            <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />

                                            <a id="file1" runat="server" visible="false" target="_blank">Download File</a>


                                            <p class="form-txtbx-sec-p" style="color: #f00; font-size: 13px; line-height: 19px; margin: 10px 0px 0px 0px;">
                                                <b>Note :-</b>
                                                <br />
                                                *Image upload max size 500 kb.
                                                                <br />
                                                *Only .pdf, .jpg
                                            </p>
                                        </div>
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
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
