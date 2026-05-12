<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Principal_Desk.aspx.cs" Inherits="school_web.LMS_VC_Admin.Principal_Desk" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
   Principal's Desk
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        ul.sidebar-menu li a.pagedatA {
            background-color: #003878;
            box-shadow: 0 4px 20px 0 rgba(0,0,0,.14), 0 7px 10px -5px rgb(0 56 120);
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="row">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 235px; height: auto;">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </div>
                    <asp:ImageButton ID="ImageButton1" ImageUrl="images/close.png" runat="server" OnClientClick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" Style="background: none" />
                </div>
            </div>
        </div>
        <!--main content start-->
        <section id="main-content">
            <div class="wrapper">
                <div class="row">
                    <div class="col-lg-12 colmd-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-body">
                                <h5 class="card-title">Principal's Desk</h5>
                                <div class="txtbx-sec">



                                    <asp:Panel ID="pnl_content_sec" runat="server" Visible="true">
                                        <div class="txtbx-wraper" style="position: relative;">
                                            <p class="txtbx-name-p" id="title_textS" runat="server" style="color: #f00; font-weight: 700; font-size: 17px;">
                                                <sup>*</sup>
                                            </p>
                                            <div class="mce-txt-bx">

                                                <%--   <script>
                                                    $(function () {
                                                        tinymce.init({
                                                            selector: $('#<%=Textarea1.ClientID%>').selector,
                                                            plugins: [
                                                         "advlist autolink autosave link image lists charmap print preview hr anchor pagebreak spellchecker",
                                                         "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                                                         "table contextmenu directionality emoticons template textcolor paste fullpage textcolor colorpicker textpattern", "imagetools"
                                                            ],

                                                            toolbar1: "bold italic underline strikethrough | fontselect fontsizeselect | alignleft aligncenter alignright alignjustify",
                                                            toolbar2: "| bullist numlist | outdent indent blockquote | undo redo | link unlink image media | forecolor backcolor | table",
                                                            //  toolbar3: "table | hr removeformat | subscript superscript | charmap emoticons | print fullscreen | ltr rtl | spellchecker | visualchars visualblocks nonbreaking template pagebreak restoredraft",

                                                            menubar: true,
                                                            toolbar_items_size: 'small',

                                                            link_list: [
                                                                     { title: 'Terms & Condition', value: '' }
                                                            ],

                                                            paste_data_images: true,



                                                            //style_formats: [
                                                            //        { title: 'Bold text', inline: 'b' },
                                                            //        { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
                                                            //        { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
                                                            //        { title: 'Example 1', inline: 'span', classes: 'example1' },
                                                            //        { title: 'Example 2', inline: 'span', classes: 'example2' },
                                                            //        { title: 'Table styles' },
                                                            //        { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
                                                            //],

                                                            templates: [
                                                                    { title: 'Test template 1', content: 'Test 1' },
                                                                    { title: 'Test template 2', content: 'Test 2' }
                                                            ]
                                                        });
                                                    });
                                                </script>--%>
                                                <script src="https://cdn.tiny.cloud/1/fh64n7ge121qyewkj79zzyj1nivy0u7hvwnxmrqwg96ov4g3/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>
                                                <script type="text/javascript">
                                                    tinymce.init({ selector: 'textarea', width: '100%' });
                                                </script>
                                                <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 300px; width: 100%"></textarea>
                                            </div>
                                        </div>

                                        <asp:Panel ID="pnl_btn_new_add" runat="server">
                                            <div class="form-btn-sec" style="padding: 10px 0px;">
                                                <asp:Button ID="btn_add" runat="server" Text="Update" ValidationGroup="D" class="btn btn-sm btn-primary" OnClick="btn_add_Click" />



                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnl_img_sec" runat="server">
                                            <div style="width: 100%; float: left; margin: 20px 0px 0px 0px; padding: 5px 0px 0px 0px; border-top: 2px solid #e3dcb9;">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <div class="txtbx-wraper">
                                                            <p class="txtbx-name-p">Image<sup>*</sup></p>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                                            <asp:Label ID="lbl_image_path" runat="server" Visible="false"></asp:Label>

                                                            <div class="form-btn-sec" style="padding: 10px 0px;">
                                                                <asp:Button ID="btn_update_image" runat="server" Text="Update" class="btn btn-sm btn-primary" OnClick="btn_update_image_Click" />
                                                                <asp:Button ID="btn_delete_phot" runat="server" Text="Delete Photo" class="btn btn-sm btn-primary" OnClick="btn_delete_phot_Click" />
                                                            </div>

                                                            <p class="form-txtbx-sec-p" style="color: #f00; font-size: 13px; line-height: 19px; margin: 10px 0px 0px 0px;">
                                                                <b>Note :-</b>
                                                                <br />
                                                                *Image upload max size 500 kb.
                                                                <br />
                                                                *Only .jpg, .png
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12"></div>
                                                    <div class="col-lg-7 colmd-7 col-sm-12 col-xs-12">
                                                        <div class="txtbx-wraper">
                                                            <asp:Image ID="Image1" runat="server" Style="width: 290px; height: 290px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
