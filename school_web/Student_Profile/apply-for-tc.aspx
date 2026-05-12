<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="apply-for-tc.aspx.cs" Inherits="school_web.Student_Profile.apply_for_tc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Apply For TC
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



            <div class="headingtablee">
                <div class="row">
                      <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <h4 class="card-title">Apply For Transfer Certificate</h4>
                            </div>
                            <div class="card-body">
                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="position-relative form-group">
                                            <label>Write Your Message </label>
                                            <asp:TextBox ID="txt_description" runat="server" CssClass="form-control" placeholder="Message" TextMode="MultiLine" Style="max-height: 150px; height: 150px"></asp:TextBox>
                                        </div>
                                        <div class="position-relative form-group">
                                            <label>Choose File (<sup> jpg,png,pdf  500kb file size </sup>)<sup>*</sup></label>

                                            <asp:FileUpload ID="fl_Photo" runat="server" CssClass="form-control"  />
                                            <asp:HiddenField ID="Hd_Photo" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                runat="server" ControlToValidate="fl_Photo"
                                                ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf|.PDF )$"
                                                ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                        </div>


                                        <asp:Button ID="btn_Submit" ValidationGroup="A" runat="server" Text="Apply For TC" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
