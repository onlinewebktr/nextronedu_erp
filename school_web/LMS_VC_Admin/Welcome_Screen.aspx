<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Welcome_Screen.aspx.cs" Inherits="school_web.LMS_VC_Admin.Welcome_Screen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Welcome Screen
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


        function CheckLimit() {
            var textField =
                document.getElementById("<%=txt_description.ClientID %>");
              var labelCount =
                  document.getElementById("<%=lblCountLimit.ClientID %>");

            if (textField.value.length > 200) {
                textField.value = textField.value.substring(0, 200);
            } else {
                labelCount.innerHTML = 200 - textField.value.length;
            }
        }
        function CheckLimit1() {
            var textField =
                document.getElementById("<%=txt_title.ClientID %>");
            var labelCount =
                document.getElementById("<%=lbl_title.ClientID %>");

              if (textField.value.length > 50) {
                  textField.value = textField.value.substring(0, 50);
              } else {
                  labelCount.innerHTML = 50 - textField.value.length;
              }
          }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }


    </script>

    <style>
        .highlightRow {
            background-color: #c5c5ff !important;
        }
    </style>

    <script>
        $(function () {
            $(".trclass").click(function () {
                $(this).addClass("highlightRow").siblings().removeClass("highlightRow");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-album icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Add App Welcome Screen</asp:Literal>

                    </div>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="slid" runat="server" />
        <div class="row">
            <div class="col-lg-3">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>Title</label>
                                    <asp:TextBox ID="txt_title" runat="server" CssClass="form-control" onKeyUp="CheckLimit1();" placeholder="Enter Title" TextMode="MultiLine" Height="50px" MaxLength="30"></asp:TextBox>
                                    You have&nbsp;<asp:Label ID="lbl_title" runat="server" ForeColor="Maroon" Text="50"></asp:Label>
                                    characters left 
                                </div>

                                <div class="position-relative form-group">
                                    <label>Description</label>
                                    <asp:TextBox ID="txt_description" runat="server" CssClass="form-control" onKeyUp="CheckLimit();" placeholder="Enter Description" TextMode="MultiLine" Height="50px" MaxLength="30"></asp:TextBox>
                                    You have&nbsp;<asp:Label ID="lblCountLimit" runat="server" ForeColor="Maroon" Text="200"></asp:Label>
                                    characters left 
                                </div>


                                 <div class="position-relative form-group">
                                    <label>Position</label>
                                    <asp:TextBox ID="txt_position" runat="server" CssClass="form-control"  placeholder="Position" onkeypress="return isNumberKey(event)"  ></asp:TextBox>
                                   
                                </div>

                                <div class="position-relative form-group">
                                    <label>Image</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" /><span>
                                        <br />
                                        e.g. .jpg,.png only(Max Size 500KB)</span>
                                </div>
                                <asp:Button ID="btn_Submit" runat="server" Text="Create" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="mt-2 btn btn-danger" OnClick="btn_cancel_Click" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-9">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <asp:HiddenField ID="HdID" runat="server" />
                        <h5 class="card-title">Added Welcome Screen </h5>









                        <asp:GridView ID="GrdView" runat="server" class="mb-0 table table-bordered" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_tittle" runat="server" Text='<%#Bind("Title")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Message")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Postion">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_position" runat="server" Text='<%#Bind("position")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Slider Image">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Image" runat="server" Text='<%#Bind("Image")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                          
                                        <a target="_blank" href='<%#Eval("Image") %>'>
                                            <asp:Image ID="myImg" runat="server" ImageUrl='<%# Bind("Image") %>' Style="height: 60px; width: 60px; margin: 0px; border: 2px solid #f93; padding: 2px" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="mb-2 mr-2 btn btn-warning" OnClick="lnkEdit_Click" CausesValidation="false">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDel" runat="server" CssClass="mb-2 mr-2 btn btn-danger" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete?');" CausesValidation="false">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
