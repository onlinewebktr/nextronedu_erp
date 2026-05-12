<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Upload_Principal_Signatur.aspx.cs" Inherits="school_web.LMS_VC_Admin.Upload_Principal_Signatur" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Upload Principal's Signatur
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-settings icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Upload Principal's Signatur</asp:Literal>

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

            <div class="col-lg-4">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-12">

                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Principal Principal's</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />

                                </div>




                            </div>

                            <asp:Button ID="btn_Submit" runat="server" Text="Add" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />


                        </div>
                    </div>
                </div>
            </div>
              <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Principal's Signatur</h5>


                          <img   width="150" height="50" id="imgsig" runat="server" style="padding:2px; border:1px solid #bdbdbd">
                        </div>
                    </div>
                  </div>


          

        </div>
    </div>
    <asp:HiddenField ID="hd_photo" runat="server" />
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
