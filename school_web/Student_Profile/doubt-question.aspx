<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="doubt-question.aspx.cs" Inherits="school_web.Student_Profile.doubt_question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Question
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .hidden {
            display: none;
        }
    </style>


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



        .answr-dv-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
        }

        .answr-rply-date-p {
            margin: 0px 0px 0px 0px;
            padding: 5px 0px 7px 0px;
            width: 100%;
            float: left;
        }

            .answr-rply-date-p span {
                font-weight: 700;
            }

        .answr-imd-dv-wpr {
            margin: 15px 0px;
            padding: 0px;
            width: 100%;
            position: relative;
            overflow: hidden;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 170px;
        }

            .answr-imd-dv-wpr img {
                margin: 0px;
                padding: 0px;
                max-width: 100%;
                height: 100%;
            }

        .answr-pss {
            margin: 0px;
            padding: 0px;
            width: 100%;
            font-size: 14px;
            line-height: 23px;
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
                                <h4 class="card-title">Question List  <a href="ask-doubt.aspx" class="pasgetitle-link">Ask Doubt</a></h4>
                            </div>
                            <div class="card-body" style="padding-top: 0px;">
                                <div class="doubt-qs-wpr">
                                    <div class="row">
                                        <asp:Repeater ID="rp_doubt" runat="server" OnItemDataBound="rp_doubt_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-md-4">
                                                    <div class="doubt-qs-bx-wpr">
                                                        <div class="doubt-qs-bx-wpr-inrs">
                                                            <div class="doubt-qs-bx-std-img">
                                                                <img src="<%#Eval("Student_img") %>" />
                                                            </div>
                                                            <div class="doubt-qs-bx-std-contnt">
                                                                <asp:Label ID="lbl_std_name" runat="server" class="doubt-qs-bx-std-contnt-name-p" Text='<%#Bind("studentname") %>'></asp:Label>
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>' class="doubt-qs-bx-std-contnt-class-p"></asp:Label>
                                                                <asp:Label ID="lbl_subject" runat="server" Text='<%#Bind("Cource_name") %>' class="doubt-qs-bx-std-contnt-subs-p"></asp:Label>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>' class="doubt-qs-bx-std-contnt-date-p"></asp:Label>
                                                                 <asp:Label ID="lbl_teacher" runat="server" Text='<%#Bind("tecahername") %>' class="doubt-qs-bx-std-contnt-subs-p"></asp:Label>

                                                            </div>
                                                            <asp:Label ID="lbl_question" class="doubt-qs-bx-std-quest-p" runat="server" Text='<%#Bind("Student_question") %>'></asp:Label>
                                                            
                                                             <asp:Label ID="lbl_Attachment" Visible="false" runat="server" Text='<%#Bind("Question_Image")%>'></asp:Label>
                                                                <a id="a1" runat="server" href='<%#Eval("Question_Image") %>' download target="_blank" style="display: block; padding: 0px 0px 0px 0px; font-size: 22px; color: #0066CC; text-decoration: none; float: left;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                                            
                                                            <asp:Label ID="lbl_doubt_id" Visible="false" class="doubt-qs-bx-std-quest-p" runat="server" Text='<%#Bind("Doubt_Id") %>'></asp:Label>

                                                            <asp:Label ID="lbl_answer" class="doubt-qs-bx-std-quest-p" Visible="false" runat="server" Text='<%#Bind("Answer") %>'></asp:Label>
                                                            <asp:Label ID="lbl_answerDate" class="doubt-qs-bx-std-quest-p" runat="server" Visible="false" Text='<%#Bind("AnswerDate") %>'></asp:Label>
                                                            <asp:Label ID="lbl_answer_image" class="doubt-qs-bx-std-quest-p" runat="server" Visible="false" Text='<%#Bind("Answer_Image") %>'></asp:Label>
                                                        </div>

                                                        <div class="doubt-qs-ans-sec">
                                                            <asp:LinkButton ID="lnk_answer" OnClick="lnk_answer_Click" runat="server" Style="margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: auto; float: left; font-weight: 600; font-size: 14px;"
                                                                class='<%#Eval("Reply_status") %>'>Answer</asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_ask_again" runat="server" class="doubt-qs-again-btns" OnClick="lnk_ask_again_Click">Ask again</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>



                            <div class="modal left fade model-fltr right" id="myModalFltr" role="dialog" style="z-index: 9999" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog model-dialog-fltr">
                                    <div class="modal-body">
                                        <h2 class="mdl-title-h">Answer Details <a href="#!" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></a></h2>
                                        <div class="mdl-cntnt-wpr">
                                            <div class="answr-dv-wpr">
                                                <p class="answr-rply-date-p">
                                                    Reply Date :
                                                    <asp:Label ID="lbl_reply_date" runat="server" Text=""></asp:Label>
                                                </p>
                                                <div class="answr-imd-dv-wpr">
                                                    <asp:Image ID="Image1" runat="server" />
                                                </div>
                                                <asp:Label ID="lbl_answerss" runat="server" Text="" class="answr-pss"></asp:Label>
                                            </div>
                                        </div>
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
