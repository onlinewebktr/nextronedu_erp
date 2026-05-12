<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="add-syllabus.aspx.cs" Inherits="school_web.InstructorProfile.add_syllabus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add Syllabus
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: 3.6px !important;
        }

        .form-group {
            padding: 0px !important;
            margin-bottom: 15px !important;
        }

        .card.mb-3 {
            margin-bottom: 30px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-network icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Add Syllabus</asp:Literal>

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
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-3" style="display: none">
                                <div class="position-relative form-group">
                                    <label>Session</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="position-relative form-group">
                                    <label>Term</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_term" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="position-relative form-group">
                                    <label>Subject</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_subject" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="position-relative form-group">
                                    <label>Is Sub Subject</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_is_sub_subject" runat="server" class="form-control">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-row"> 
                            <div class="col-md-3" id="subsubjest">
                                <div class="position-relative form-group">
                                    <label>Sub-Subject </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_sub_subject" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="position-relative form-group">
                                    <label>Chapter Name</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_Chaptername" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="position-relative form-group">
                                    <label>Is Sub Chapter</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_sub_chapter" runat="server" class="form-control">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3" id="subchapter">
                                <div class="position-relative form-group">
                                    <label>Enter Subchapter Name </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_subchapter" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-md-3">
                                <asp:Button ID="btn_submit" runat="server" Text="Add" Style="float: left;" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                <asp:Button ID="btn_cncel" runat="server" Text="Cancel" Style="float: right" CssClass="btn btn-dark" OnClick="btn_cncel_Click" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Last Top 10  Added Chapter & Subchapter</h5>

                        <table style="width: 100%;" id="examplecc" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Session</th>
                                    <th>Teram</th>
                                    <th>Class</th>
                                    <th>Subject</th>
                                    <th>Sub-Subject</th>
                                    <th>Chapter Name</th>
                                    <th>Subchapter Name</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Term_Name" runat="server" Text='<%#Bind("Term_Name") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_subsubject" runat="server" Text='<%#Bind("SubSubjName") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Chapter_Name" runat="server" Text='<%#Bind("Chapter_Name") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Subchapter_Name" runat="server" Text='<%#Bind("SubChapterName") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Term_id" runat="server" Text='<%#Bind("Term_id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id") %>' Visible="false"></asp:Label>

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


    <script type="text/javascript">  
        $(document).ready(function () {
            on_subj_selection();
            $("#<%=ddl_is_sub_subject.ClientID%>").on('change', function () {
                on_subj_selection();
            })
        });

        function on_subj_selection() {
            if ($('#<%= ddl_is_sub_subject.ClientID %> option:selected').val() == "Yes") {
                $("#subsubjest").show();
                $("#<%=txt_sub_subject.ClientID %>").focus();
            }
            else {
                $("#subsubjest").hide();
            }
        }

        //================
        $(document).ready(function () {
            on_chapter_selection();
            $("#<%=ddl_sub_chapter.ClientID%>").on('change', function () {
                on_chapter_selection();
            })
        });

        function on_chapter_selection() {
            if ($('#<%= ddl_sub_chapter.ClientID %> option:selected').val() == "Yes") {
                $("#subchapter").show();
                $("#<%=txt_subchapter.ClientID %>").focus();
            }
            else {
                $("#subchapter").hide();
            }
        }
    </script>

    <asp:HiddenField ID="hd_edit_from" runat="server" />
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
