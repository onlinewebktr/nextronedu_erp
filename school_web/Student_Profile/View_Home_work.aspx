<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/User.Master" AutoEventWireup="true" CodeBehind="View_Home_work.aspx.cs" ValidateRequest="false" Inherits="school_web.Student_Profile.View_Home_work" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    View Added Home Work  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @media screen and (max-width: 480px) {
            .page-content {
                overflow-x: scroll;
            }
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $("#modalConfirm").modal('show');
        }

        function confirmalert() {
            alert("Are You Sure To delete this.")
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a> <i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item"><a href="#">Home Work</a></li>
        <li class="breadcrumb-item">View Added Home Work  </li>
    </ol>
    <div id="notification">
        <div id="pan" class="notificationpan">
            <div style="float: left; width: 235px; height: auto;">
                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
            </div>
            <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                class="closenotificationpan" alt="" />
        </div>
    </div>
    <asp:HiddenField ID="hdDIO" runat="server" />
    <div class="grid-form">
        <div class="grid-form1">
            <div class="panel-body">
                <div class="row" style="padding: 0px 0px 0px 0px;">
                    <div class="col-xs-12 col-md-12">
                        <div class="row">
                            <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                <label>Class</label>
                                <asp:Label ID="lbl_class" class="form-control" runat="server" Style="width: 98%"></asp:Label>
                                <asp:HiddenField ID="hd_classid" runat="server" />
                            </div>

                            <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                <label>Section </label>
                                <asp:Label ID="lbl_section" class="form-control" runat="server" Style="width: 98%"></asp:Label>
                                <asp:HiddenField ID="hd_section_id" runat="server" />
                            </div>

                            <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                <label>Lesson Name</label>
                                <asp:DropDownList ID="ddl_subject" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                            </div>
                            <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-sm btn-success" Text="Find" Style="margin-top: 24px;" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                            <br />
                            <asp:GridView ID="grd_view" runat="server" AutoGenerateColumns="false" CssClass="table">
                                <Columns>

                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CategoryName" runat="server" Font-Names="Arial" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Section_Name" runat="server" Font-Names="Arial" Text='<%#Bind("Section_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Subject">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CourseName" runat="server" Font-Names="Arial" Text='<%#Bind("CourseName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Topic">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Topic" runat="server" Font-Names="Arial" Text='<%#Bind("Topic") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                              <asp:LinkButton ID="lnk_view" OnClick="lnk_view_Click" runat="server">View</asp:LinkButton>

                                            <asp:Label ID="lbl_Description" runat="server" Font-Names="Arial" Text='<%#Bind("Description") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Upload_Date" runat="server" Font-Names="Arial" Text='<%#Bind("Upload_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href='<%#Eval ("Attachments")%>' target="_blank">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval ("Attachments")%>' Width="100px" />
                                            </a>
                                            <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#880300" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalConfirm" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                  <h5 class="modal-title">Home Work Details</h5>
                </div>
                <div class="modal-body">
                    <asp:Literal ID="txt_info" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

