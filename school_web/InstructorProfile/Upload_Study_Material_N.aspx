<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Upload_Study_Material_N.aspx.cs" Inherits="school_web.InstructorProfile.Upload_Study_Material_N" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Upload Study Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-group {
            /* padding: 0px!important; */
            margin-bottom: 0px!important;
            padding: 10px!important;
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
                        <asp:Literal ID="ltUsertop" runat="server">Upload Study Material</asp:Literal>

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
                        <h5 class="card-title"></h5>
                        <div class="form-row">

                            <div class="row">

                                <div class="form-group col-xs-10 col-sm-6 col-md-3 col-lg-3">
                                    <label>Class<sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-xs-10 col-sm-6 col-md-3 col-lg-3">
                                    <label>Section<sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_section" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-xs-10 col-sm-6 col-md-3 col-lg-3">
                                    <label>Subject Name<sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_subject" class="form-control" runat="server"></asp:DropDownList>
                                </div>


                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-12 col-md-12 col-lg-12">
                                    <label>Topic Name<sup>*</sup></label>
                                    <asp:TextBox ID="txtTopic" class="form-control" runat="server" Style="width: 23%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-12 col-md-12 col-lg-12">
                                    <label>Description<sup>*</sup></label>

                                    <script src="js/jquery-1.10.2.min.js"></script>

                                  
                                    <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 300px; width: 100%"></textarea>


                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                                    <label style="margin-right: 20px;">
                                        Add Video Link</label>
                                    <a href="Javascript:" data-toggle="modal" data-target="#myModal" style="font-size: 12px;">How to copy YouTube Video Link</a>
                                    <asp:TextBox ID="txt_VideoLink" class="form-control" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hdVideoLink" runat="server" />
                                </div>
                                <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                                </div>




                                <div class="clearfix"></div>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                    <table class="tab-content table table-bordered" style="margin: 0px; padding: 0px; float: left; width: 50%;">
                                        <tr>
                                            <td colspan="3" style="padding: 5px 5px 5px 5px!important; font-size: 14px;">Upload Multiple Graphic (jpg,png only -file size 10 MB)</td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px 5px 5px 5px!important; font-size: 14px;">Choose file<sup></sup></td>
                                            <td style="padding: 5px 5px 5px 5px!important">
                                                <asp:FileUpload ID="fl_img" runat="server" class="form-control" />


                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    runat="server" ControlToValidate="fl_img"
                                                    ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                    ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG)$"
                                                    ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </td>
                                            <td style="padding: 5px 5px 5px 5px!important">
                                                <asp:Button ID="btn_uploadimg" runat="server" OnClick="btn_uploadimg_Click" class="btn btn-sm btn-success" Text="Add" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="3" style="padding: 5px 5px 5px 5px!important; font-size: 14px;">Upload Multiple Attachment(pdf only -file size 10 MB)</td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px 5px 5px 5px!important; font-size: 14px;">Choose file<sup></sup></td>
                                            <td style="padding: 5px 5px 5px 5px!important">
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />


                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    runat="server" ControlToValidate="FileUpload1"
                                                    ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                    ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                    ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </td>
                                            <td style="padding: 5px 5px 5px 5px!important">
                                                <asp:Button ID="btn_upload_attacment" runat="server" OnClick="btn_upload_attacment_Click" class="btn btn-sm btn-success" Text="Add" />
                                            </td>
                                        </tr>



                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="GrdViewimg" runat="server" class="mb-0 table table-bordered" CssClass="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdViewimg_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <a id="a2" runat="server" href='<%#Eval("Images") %>' download target="_blank" style="display: block; padding: 2px; font-family: ebrima; font-size: 31px; color: #0066CC; text-decoration: none;"><i class="fa fa-cloud-download" aria-hidden="true"></i></a>

                                                                <a id="a1" runat="server" href='<%#Eval("Images") %>'  target="_blank" style="display: block; padding: 2px; font-size: 31px; color: #0066CC; text-decoration: none;">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("Images") %>' Style="margin: 0px; height: 50px; width: 50px; border: 2px solid #f93; padding: 1px" />

                                                                </a>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Type">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" ">
                                                            <ItemTemplate>
                                                                <asp:Button ID="BtnDeleteimg" OnClientClick="return confirm('Are you sure want to delete ?')" runat="server" OnClick="BtnDeleteimg_Click" class="btn btn-primary" Text="Delete" />
                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
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
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="Hd_Document" runat="server" />
    </div>

    <div id="myModal" class="modal fade" role="dialog">


        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">How to copy YouTube Video Link</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <ul>
                        <li>Go to YouTube Channel.</li>
                        <li>Search your video which you want to link.</li>
                        <li>Play your chosen video.</li>
                        <li>After play video you right click on video then the <b>"Copy Video URL"</b> option will appear. Here you copy video link.</li>
                        <li>After copy video link you paste video link in to <b>"Add Video Link"</b></li>

                    </ul>
                    <p>
                        <a href="../images/video url copy instruction.png" target="_blank">
                            <img src="../images/video%20url%20copy%20instruction.png" width="100%" class="img-responsive" /></a>
                    </p>
                </div>
            </div>
        </div>
    </div>

    <div id="fadeup"></div>
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hdDIO" runat="server" />
    <asp:HiddenField ID="hd_topicid" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script>
        function confirmalert() {
            alert("Are You Sure To delete this.")
        }
    </script>
</asp:Content>
