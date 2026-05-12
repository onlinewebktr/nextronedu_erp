<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Rank_Configuration.aspx.cs" Inherits="school_web.Examination_Admin.Rank_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Rank Configuration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            border-style: none;
            width: 25px;
            height: 25px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 8px;
            z-index: 9999;
            background-color: green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--start page wrapper -->
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Rank Configuration</li>
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
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label" style="font-weight: bold">Select Class<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_class" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-md-12">


                                        <asp:RadioButton ID="rd_yearwise" Checked="true" runat="server" Text="Yearly Ranking" GroupName="ab" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rd_yearwise_CheckedChanged" />
                                        <asp:RadioButton ID="rd_termwise" runat="server" Text="Term Wise Ranking" GroupName="ab" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rd_termwise_CheckedChanged" />
                                        <asp:RadioButton ID="rd_Assessmentwise" runat="server" Text="Assessment Wise Ranking" GroupName="ab" AutoPostBack="true" Font-Bold="true" OnCheckedChanged="rd_Assessmentwise_CheckedChanged" />
                                    </div>
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: bold; margin-left: 9px; font-size: 16px;">
                                                Rank based on
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 0px;">

                                        <asp:RadioButton ID="rd_marks" Checked="true" runat="server" Text="Marks" GroupName="abc" Font-Bold="true" />
                                        <asp:RadioButton ID="rd_Percentage" Checked="true" runat="server" Text="Percentage" GroupName="abc" Font-Bold="true" />
                                    </div>

                                    <div class="col-md-12">

                                        <label class="switch" style="margin: 0px 0px 0px 0px;">
                                            <asp:CheckBox ID="chk_considaer_failed_student" runat="server" Checked="false" Text="Consider Failed Students" />
                                            <span class="slider round"></span>
                                        </label>

                                        <label class="switch" style="margin: 0px 0px 0px 0px;">
                                            <asp:CheckBox ID="chk_rank_pass_student" runat="server" Checked="false" Text="Ranks Passed Students First" />
                                            <span class="slider round"></span>
                                        </label>


                                    </div>
                                    <div class="col-md-12">

                                        <label class="switch" style="margin: 0px 0px 0px 0px;">
                                            <asp:CheckBox ID="chk_skip_ranks" runat="server" Checked="false" Text="Skip ranks (Eg. 1st, 2nd, 3nd, 4th)" />
                                            <span class="slider round"></span>
                                        </label>




                                    </div>

                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-12">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
        <!--end row-->
    </div>

    <!--end page wrapper -->
</asp:Content>
