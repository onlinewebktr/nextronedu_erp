<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Add_Subject_Master.aspx.cs" Inherits="school_web.LMS_VC_Admin.Add_Subject_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add Subject
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: 5.6px!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Subject List</asp:Literal>

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
            <div class="col-lg-4" style="display: block">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Add Subject</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>Select Class</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_CourseCat" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Select Section</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control">
                                            <asp:ListItem>ALL</asp:ListItem>
                                            <asp:ListItem>A</asp:ListItem>
                                            <asp:ListItem>B</asp:ListItem>
                                            <asp:ListItem>C</asp:ListItem>
                                            <asp:ListItem>D</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Subject Name:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_Course" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                        <asp:GridView ID="GrdSub" runat="server"
                                            CellPadding="4" ForeColor="Black"
                                            Style="text-align: center;"
                                            Width="100%" AutoGenerateColumns="False" ShowFooter="true" ShowHeader="true" CssClass="gridview">
                                            <RowStyle BackColor="#F7F7DE" ForeColor="#000000" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Slno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Course" runat="server" Text='<%#Bind("CourseName") %>' Style="width: 100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="img_btnFam_add" runat="server" ImageUrl="~/images/plus_icon.png"
                                                            Width="20px" CausesValidation="false" Style="float: right;" OnClick="img_btnFam_add_Click" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <SelectedRowStyle BackColor="#EFEFEF" Font-Bold="True" ForeColor="#CC0000" />
                                            <HeaderStyle ForeColor="White" BackColor="#364150" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlhide" runat="server" Visible="false">
                                    <div class="position-relative form-group">
                                        <label>Subject Details:</label>
                                        <div class="input-group">
                                            <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 300px; width: 100%"></textarea>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label>Upload Subject Image</label>
                                        <div class="input-group input-group-icon">
                                            <asp:FileUpload ID="fl_Photo" runat="server" />
                                            <asp:HiddenField ID="Hd_Photo" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="card-footer pull-right">
                                    <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Visible="false" OnClick="btn_Cancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">All Added Subject</h5>

                        <div class="form-row">
                            <div class="col-md-1">
                                <label>Class</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddl_SearchCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_SearchCategory_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Section</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddl_search_section" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btn_Find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_Find_Click" Style="margin-top: 3px" />
                            </div>
                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>


                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CourseName" runat="server" Text='<%#Bind("CourseName") %>'></asp:Label>
                                                <asp:HiddenField ID="hdCategoryID" runat="server" Value='<%#Bind("CategoryID") %>' />

                                                <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description") %>' Visible="false"></asp:Label>

                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Bind("Image") %>' Width="80px" Visible="false"></asp:Image>

                                            </td>
                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" Visible="false" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkBtnEdit" OnClick="LinkBtnEdit_Click" runat="server" Visible="false">View</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

    </div>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Subject Description</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>
    <asp:HiddenField ID="hd_id" runat="server" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
