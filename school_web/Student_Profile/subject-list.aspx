<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="subject-list.aspx.cs" Inherits="school_web.Student_Profile.subject_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Subject List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">My Subject</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <asp:Repeater ID="rp_subjects" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 custom-col">
                                        <div class="std-mt-dt-grphview-bx-wpr">
                                            <div class="std-mt-dt-grphview-bx-imd-dv" style="background: #F6F6F6;">
                                                <img src="assets/images/icons/subject-icon.png" />
                                            </div>
                                            <ul class="subjct-ul">
                                                <li style="width: 100%; border-right: 0px;"><%#Eval("Subject_name") %></li>
                                            </ul>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
