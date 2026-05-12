<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="ebook.aspx.cs" Inherits="school_web.Student_Profile.ebook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    E-Book
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
                    <h4 class="card-title">E-Book</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">Subject Name</label>
                                <asp:DropDownList ID="ddl_subject" runat="server" class="form-control form-control-custom"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary fnd-btnmrgn" OnClick="btn_find_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rp_ebboks" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 custom-col">
                                        <div class="ebook-sec">
                                            <div class="ebook-cover">
                                                <img src="<%#Eval("Cover_Photo") %>" />
                                            </div>
                                            <p class="ebook-cover-name-p"><%#Eval("Book_Name") %></p>
                                            <ul class="ebook-read-ul">
                                                <li><a href="read-ebbok.aspx?ebookid=<%#Eval("Book_id") %>">Read</a></li>
                                                <li style="border-right: 0px solid #ddd;"><a href="<%#Eval("Path_of_ebook") %>" download="">Download</a></li>
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
