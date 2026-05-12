<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="study-material-details.aspx.cs" Inherits="school_web.Student_Profile.study_material_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Study Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body {
            height: 100%;
        }

        .wrapper {
            width: 100%;
            height: 100%;
            margin: 0 auto;
            background: #CCC;
        }

        .h_iframe {
            position: relative;
        }

            .h_iframe .ratio {
                display: block;
                width: 100%;
                height: auto;
            }

            .h_iframe iframe {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }
    </style>
    <script src="assets/js/jquery-1.10.2.min.js"></script>
    <link href="assets/galleryNew/fancybox.min.css" rel="stylesheet" />
    <link href="assets/galleryNew/style.css" rel="stylesheet" />

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
                    <h4 class="card-title"><a href="study-material-details.aspx" class="back-btnss"><i class="fa fa-angle-left" aria-hidden="true"></i></a>Subject : <span id="SubjectName" runat="server"></span></h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="std-mt-dt-sec">
                                    <h2 class="std-mt-dt-topic-h">Topic : </h2>
                                    <p class="std-mt-dt-topic-txt-p" runat="server" id="topic_p"></p>

                                    <h2 class="std-mt-dt-topic-h">Description : </h2>
                                    <p class="std-mt-dt-topic-txt-p" runat="server" id="topic_desc"></p>


                                    <div class="std-mt-dt-grphview" runat="server" id="graphicS">
                                        <h2 class="std-mt-dt-topic-h">Graphic View : </h2>
                                        <div class="row"> 
                                            <asp:Repeater ID="rd_graphics" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 custom-col">
                                                        <div class="std-mt-dt-grphview-bx-wpr">
                                                            <div class="std-mt-dt-grphview-bx-imd-dv">

                                                                <div class="photos">
                                                                    <div class="fnder-zoom-slider-img-wpr">
                                                                        <%--<a href="<%#Eval("Images") %>"  class="d-block photo-item" data-fancybox="gallery">--%>
                                                                            <img src="<%#Eval("Images") %>" alt="Integer Foundation" />
                                                                            <span class="thumb-overlay"><span class="thumb-bg" style="background-color: rgba(181,181,181,0.85);"><span class="thumb-title fadeInLeft animated" runat="server" id="test8h"></span></span></span>
                                                                       <%-- </a>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="std-mt-dt-grphview-bx-cont">
                                                                <ul class="std-mt-dt-grphview-bx-cont-ul">
                                                                    <li><a href="<%#Eval("Images") %>" class="d-block photo-item" data-fancybox="gallery">View</a></li>
                                                                    <li><a href="<%#Eval("Images") %>" download="" target="_blank">Download</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>

                                    <div class="std-mt-dt-grphview" runat="server" id="pdfsDV" style="margin: 10px 0px 0px 0px;">
                                        <h2 class="std-mt-dt-topic-h">PDF : </h2>
                                        <div class="row">
                                            <asp:Repeater ID="rp_pdfs" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 custom-col">
                                                        <div class="std-mt-dt-grphview-bx-wpr">
                                                            <div class="std-mt-dt-grphview-bx-imd-dv" style="background: #F6F6F6;">
                                                                <img src="assets/images/icons/pdf-icon.jpg" />
                                                            </div>
                                                            <div class="std-mt-dt-grphview-bx-cont">
                                                                <ul class="std-mt-dt-grphview-bx-cont-ul">
                                                                    <li style="width: 100%; border-right: 0px;"><a href="<%#Eval("Images") %>" download="" target="_blank">Download</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>


                                    <div class="std-mt-dt-grphview" id="videoDV" runat="server" style="margin: 10px 0px 0px 0px;">
                                        <h2 class="std-mt-dt-topic-h">Video : </h2>

                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                                                <div class="vdos-wprs">
                                                    <div class="video-ifrm-wpr">
                                                        <div class="h_iframe">
                                                            <img src="assets/images/birthday-bg.png" />
                                                            <iframe runat="server" id="videoS" frameborder="0" allowfullscreen></iframe>
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
            </div>
        </div>
    </div>


    <script src="assets/galleryNew/jquery.fancybox.min.js" type="15819cd09bcb6d927ec3ae3d-text/javascript"></script>
    <script src="assets/galleryNew/rocket-loader.min.js" data-cf-settings="15819cd09bcb6d927ec3ae3d-|49" defer=""></script>
</asp:Content>
