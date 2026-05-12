<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Upload_Home_Work.aspx.cs" Inherits="school_web.InstructorProfile.Upload_Home_Work" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Upload Homework
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="lt_meata" runat="server"></asp:Literal>
    <style>
        .form-group {
            /* padding: 0px!important; */
            margin-bottom: 0px !important;
            padding: 10px !important;
        }

        .modal-backdrop, .blockOverlay {
            position: fixed;
            top: 134px;
            left: 0;
            z-index: 1040;
            width: 100vw;
            height: 100vh;
            background-color: #ffffff03;
        }

        .notificationpan {
            top: 69px !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 11px;
            left: 209px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-network icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">HomeWork Upload </asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">

                        <div class="form-row">

                            <div class="row">
                                <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg3">
                                    <label>Class<sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg3">
                                    <label>Section<sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_section" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg3">
                                    <label>Subject Name<sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_subject" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                                </div>





                                <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                    <label>Homework Completion Date<sup>*</sup>  </label>
                                    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
                                    <script src="../Autocomplete/jquery-ui.js"></script>
                                    <asp:TextBox ID="txt_date" class="form-control" runat="server" Style="width: 98%" CssClass="form-control calender-icon"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    <script>
                                        $(function () {

                                            $("#<%=txt_date.ClientID %>").datepicker({
                                                dateFormat: "dd/mm/yy",
                                                changeMonth: true,
                                                changeYear: true,
                                                yearRange: "1900:2100",

                                            }).attr("readonly", "true");
                                        });
                                    </script>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-12 col-md-12 col-lg-12">
                                    <label>Homework Topic<sup>*</sup>  </label>
                                    <asp:TextBox ID="txtTopic" class="form-control" runat="server" Style="width: 23%"></asp:TextBox>
                                </div>


                                <div class="clearfix"></div>

                                <div class="form-group col-xs-10 col-sm-12 col-md-12 col-lg-12">
                                    <label>Homework Details<sup>*</sup></label>
                                   
                                    <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 500px; width: 100%" oncopy="return false" onpaste="return false"></textarea>



                                </div>

                                <div class="clearfix"></div>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <table class="tab-content table table-bordered" style="margin: 0px; padding: 0px; float: left; width: 50%;">
                                        <tr>
                                            <td colspan="3" style="padding: 5px 5px 5px 5px!important; font-size: 14px;">Upload Multiple Attachment(jpg,png,Pdf only -file size max 10 MB)</td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px 5px 5px 5px!important; font-size: 14px;">Choose file<sup></sup></td>
                                            <td style="padding: 5px 5px 5px 5px!important">
                                                <asp:FileUpload ID="fl_Document" runat="server" class="form-control" />


                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    runat="server" ControlToValidate="fl_Document"
                                                    ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                    ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf)$"
                                                    ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                                <asp:HiddenField ID="Hd_Document" runat="server" />
                                            </td>
                                            <td style="padding: 5px 5px 5px 5px!important">
                                                <asp:Button ID="btn_uploadimg" runat="server" OnClick="btn_uploadimg_Click" class="btn btn-sm btn-success" Text="Add" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="padding: 5px;">Uploaded File List
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">

                                                <asp:GridView ID="GrdViewimg" runat="server" class="mb-0 table table-bordered" CssClass="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Img" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_imp" runat="server" Text='<%#Bind("Images")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_Homework_Id" runat="server" Text='<%#Bind("Homework_Id")%>' Visible="false"></asp:Label>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("Images") %>' Style="margin: 0px; height: 50px; width: 50px; border: 2px solid #f93; padding: 1px" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Topic Name" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_topicname" runat="server" Text='<%#Bind("Topicname")%>'></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Download and view">
                                                            <ItemTemplate>
                                                                <a href='<%#Eval("Images") %>' download style="display: block; padding: 5px 0px 7px 30px; font-family: ebrima; font-size: 31px; color: #0066CC; text-decoration: none;"><i class="fa fa-cloud-download" aria-hidden="true"></i></a>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText=" ">
                                                            <ItemTemplate>
                                                                <asp:Button ID="BtnDeleteimg" OnClientClick="return confirm('Are you sure want to delete ?')" runat="server" OnClick="BtnDeleteimg_Click" class="btn btn-primary" Text="Delete" />

                                                            </ItemTemplate>

                                                        </asp:TemplateField>



                                                    </Columns>

                                                </asp:GridView>
                                            </td>
                                        </tr>

                                    </table>
                                </div>




                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-12 col-md-12 col-lg-12" style="text-align: center; border-top: 1px solid #c9c6c6;">
                                    <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" class="btn btn-sm btn-primary" Text="Submit" />
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-11 col-md-11 col-lg-11">
                                    <asp:HiddenField ID="hddId" runat="server" /> 
                                </div>
                                <div class="clearfix"></div> 
                            </div> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hdDIO" runat="server" />
    <asp:HiddenField ID="hd_homeworkid" runat="server" />


    <style>
        .tox-tinymce {
            height: 400px !important;
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
