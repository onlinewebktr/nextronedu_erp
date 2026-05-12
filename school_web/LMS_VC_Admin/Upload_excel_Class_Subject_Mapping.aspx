<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Upload_excel_Class_Subject_Mapping.aspx.cs" Inherits="school_web.LMS_VC_Admin.Upload_excel_Class_Subject_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Upload Excel For Class & Subject Mapping
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script>
        function openModal() {

            $("#modalConfirm").modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Upload Excel For Class & Subject Mapping</asp:Literal>

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
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Upload Excel For Class & Subject Mapping <a href="InstructorCourseMapping.aspx" style="float: right;">View Mapped List</a></h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>Choose Excel(.csv) File</label>
                                    <div class="input-group input-group-icon">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <div class="col-md-4">
                                        <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                    </div>
                                    <div class="col-md-8">
                                        <a href="../images/Class_Subject_Mapping.csv" style="float: right" download>Download Format </a>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <asp:Panel ID="pnl_grd" runat="server" Visible="false">
                            <div class="col-md-12">
                                <h5 class="card-title">Class & Subject Mapping List
                            <asp:Label ID="lbl_total_student" runat="server" Text="0" Style="float: right;"></asp:Label>
                                </h5>
                                <asp:Label ID="lbl_smg" runat="server" Text="0" Style="float: right;"></asp:Label>

                                <div class="row" style="overflow: overlay; padding: 0px; max-height: 350px;">

                                    <asp:GridView ID="grvExcelData" runat="server" CssClass="table table-bordered table-striped" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                                <asp:Button ID="btn_final_butmit" runat="server" CssClass="btn btn-primary" Text="Final Submit" OnClick="btn_final_butmit_Click" />

                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div></div>
</asp:Content>
