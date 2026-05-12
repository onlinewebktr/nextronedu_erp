<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="ask-doubt.aspx.cs" Inherits="school_web.Student_Profile.ask_doubt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Ask Doubt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal-dialog {
            max-width: 400px;
            margin: 0.5rem auto;
            padding: 0px 5px;
        }

        .modal-body {
            position: relative;
            flex: 1 1 auto;
            padding: 0px;
            background: #fff;
            border-radius: 4px;
        }

        .modal {
            background: rgb(0 0 0 / 46%);
        }


        .mdl-title-h {
            margin: 0px;
            padding: 8px 15px 8px 15px;
            border-bottom: 1px solid #ddd;
            font-size: 21px;
            width: 100%;
            float: left;
            font-weight: 400;
        }

            .mdl-title-h a {
                margin: 3px 0px 0px 0px;
                padding: 0px;
                float: right;
                color: #000;
                font-size: 19px;
            }

        .mdl-cntnt-wpr {
            margin: 0px;
            padding: 15px 15px 15px 15px;
        }

        .modal.show .modal-dialog {
            transform: translateY(5%);
        }

        .form-txtbx-selcted-c {
            margin: 13px 0px 0px 0px;
            padding: 5px 5px;
            width: 100%;
            float: left;
            color: #000;
            font-size: 14px;
            border: 1px solid #2b3553;
            border-radius: 4px;
        }

            .form-txtbx-selcted-c span {
                font-weight: 600;
            }

        .tcher-dv-wpr {
            margin: 4px 0px;
            padding: 6px 5px 4px;
            width: 100%;
            float: left;
            font-weight: 500;
            background: #ffffff;
            border-radius: 3px;
            box-shadow: rgb(60 64 67 / 30%) 0px 1px 2px 0px, rgb(60 64 67 / 15%) 0px 2px 6px 2px;
        }



        .tcher-dv-wpr-p {
            margin: 0px 0px 0px 0px;
            padding: 0px 8px 0px 0px;
            width: 100%;
            float: left;
        }

            .tcher-dv-wpr-p span {
                margin: 0px;
                padding: 0px;
            }

        .tcher-dv-wpr-chkbx {
            margin: 3px 0px 0px 0px !important;
            padding: 0px;
            width: auto;
            float: right;
        }

        .tcher-dv-wpr-ss {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }
    </style>


    <script type="text/javascript">
        function openModalDoubt() {
            $('#myModalFltr').modal('show');
        }
    </script>

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
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <h4 class="card-title">Ask Doubt <a href="doubt-question.aspx" class="pasgetitle-link">Question?</a></h4>
                            </div>
                            <div class="card-body" style="padding-top: 0px;">
                                <h2 class="std-name-doubt-h">Hey
                                    <asp:Label ID="lbl_student_name" runat="server" Text="Label"></asp:Label>,</h2>

                                <p class="std-name-doubt-h1">Select your subject to ask your question.</p>
                                <h2 class="std-name-doubt-h3">Subject List</h2>
                                <div class="row">
                                    <asp:Repeater ID="rp_subjects" runat="server">
                                        <ItemTemplate>
                                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 custom-col">
                                                <asp:LinkButton ID="lnk_open_mdl" runat="server" OnClick="lnk_open_mdl_Click">
                                                    <div class="std-mt-dt-grphview-bx-wpr">
                                                        <div class="std-mt-dt-grphview-bx-imd-dv" style="background: #F6F6F6;">
                                                            <img src="assets/images/icons/subject-icon.png" />
                                                        </div>
                                                        <ul class="subjct-ul">
                                                            <li style="width: 100%; border-right: 0px;"><%#Eval("Subject_name") %></li>
                                                            <asp:Label ID="lbl_subject_id" Visible="false" runat="server" Text='<%#Bind("Subject_id")%>'></asp:Label>
                                                            <asp:Label ID="lbl_subject_name" Visible="false" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                        </ul>
                                                    </div>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <div class="modal left fade model-fltr right" id="myModalFltr" role="dialog" style="z-index: 9999" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog model-dialog-fltr">
                                        <div class="modal-body">
                                            <h2 class="mdl-title-h">Ask your question <a href="#!" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></a></h2>
                                            <div class="mdl-cntnt-wpr">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="txtbx-dv-wpr">
                                                            <p class="form-txtbx-p">Upload a picture of your question</p>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control pontr-non" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="txtbx-dv-wpr">
                                                            <p class="form-txtbx-p">Write question Name</p>
                                                            <asp:TextBox ID="txt_question" Style="padding: 5px" runat="server" CssClass="form-control pontr-non" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="txtbx-dv-wpr">
                                                            <p class="form-txtbx-selcted-c">
                                                                Selected Course :
                                                            <asp:Label ID="lbl_selected_course" runat="server" Text="ENGLISH LANGUAGE"></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12 col-xs-12">
                                                        <div class="txtbx-dv-wpr">
                                                            <p class="form-txtbx-p">Available Teacher</p>
                                                            <div class="tcher-dv-wpr-ss">
                                                                <asp:Repeater ID="rp_teachers" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="tcher-dv-wpr">
                                                                            <p class="tcher-dv-wpr-p">
                                                                                <asp:Label ID="lbl_tcher_name" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_teacher_id" Visible="false" runat="server" Text='<%#Bind("UserID")%>'></asp:Label>
                                                                                <asp:CheckBox ID="chk_teacher" runat="server" class="tcher-dv-wpr-chkbx" />
                                                                            </p>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 col-xs-12">
                                                        <div class="btns-dv-wpr">
                                                            <asp:Button ID="btn_Submit" runat="server" Text="Submit Question" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <%--<div class="row">
                                    <div class="col-md-3">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Write question Name</p>
                                            <asp:TextBox ID="txt_name" runat="server" CssClass="form-control pontr-non" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Your Class Name</p>
                                            <asp:TextBox ID="txt_class_name" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-xs-4  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Your Section</p>
                                            <asp:TextBox ID="txt_section" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-xs-4  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Admission No.</p>
                                            <asp:TextBox ID="txt_admission_no" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-xs-4  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Roll No.</p>
                                            <asp:TextBox ID="txt_roll_no" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">No. of Days</p>
                                            <asp:TextBox ID="txt_ttl_leave" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Subject</p>
                                            <asp:TextBox ID="txt_leave_subject" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Leave Detail</p>
                                            <asp:TextBox ID="txt_leave_details" runat="server" CssClass="form-control" TextMode="MultiLine" Style="border: rgba(29,37,59,.5)1px solid; padding: 5px 5px 0 5px; height: 70px; border-radius: 4px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Add Attachment (if any)</p>
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="btns-dv-wpr">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Apply For Leave" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
