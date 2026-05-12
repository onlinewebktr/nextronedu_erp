<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Topic_Details.aspx.cs" Inherits="school_web.Student_Profile.Topic_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <link href="css/bootstrap.min.css" rel='stylesheet' type='text/css' />
    <link href="css/style.css" rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="css/morris.css" type="text/css" />
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <script src="js/jquery-2.1.4.min.js" type="text/javascript"></script>
    <link href='//fonts.googleapis.com/css?family=Roboto:700,500,300,100italic,100,400' rel='stylesheet' type='text/css' />
    <link href='//fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="css/icon-font.min.css" type='text/css' />
    <script src="../Autocomplete/jquery-ui.js" type="text/javascript"></script>
    <link href="../Autocomplete/jquery.autocomplete.css" rel="stylesheet" />
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <style type="text/css">
        .gridview th {
            padding: 1px 1px 1px 1px!important;
            text-align: center!important;
            font-size: 11px!important;
            font-weight: bold!important;
        }

        .gridview td {
            padding: 1px 1px 1px 1px!important;
            font-size: 11px!important;
            text-align: center!important;
            font-weight: normal!important;
        }

        html, body {
            font-family: 'Roboto', sans-serif;
            font-size: 100%;
            overflow-x: hidden;
            background-color: #fff!important;
        }

        .not-active {
            pointer-events: none;
            cursor: default;
            text-decoration: none;
            color: black;
        }

        td, th {
            padding: 1px!important;
        }

        .grid-form {
            margin: 0!important;
        }

        .border {
            /*border: 1px solid red;*/
            background-color: none;
        }

        .panel-body {
            padding: 0px!important;
        }

        .blogDetails .details-sulf ul {
            border-top: none;
            padding: 0px;
            margin-top: 14px;
        }

            .blogDetails .details-sulf ul li img {
                border-radius: 0%!important;
            }

        @media (max-width: 991.98px) {
            .blog-details-right {
                margin-top: 20px;
            }
        }

        td i {
            color: #000!important;
            padding: 5px;
            background-color: #34f06a!important;
            border-radius: 5px;
        }
    </style>



</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hdUserId" runat="server" />
        <asp:HiddenField ID="hd_CourseId" runat="server" />
        <asp:HiddenField ID="hd_TopicId" runat="server" />
        <asp:HiddenField ID="hd_SectionID" runat="server" />
        <asp:HiddenField ID="AllTopicIDs" runat="server" />
        <div class="grid-form blogDetails" id="blogDetails">
            <div class="grid-form1">
                <div class="panel-body">
                    <div class="row" style="padding: 0px 0px 0px 0px;">
                        <div style="margin: 0px 0px 10px 0px; padding: 0px; float: left; width: 100%; height: auto;">
                            <h2 class=" blue_bg">
                                <asp:Literal ID="LtCourseName" runat="server" Text='<%#Eval("TopicName") %>' />
                            </h2>
                            <div class="col-lg-12" style="padding: 0px 0px 0px 0px;">
                                <asp:Repeater ID="Rp_TopicDetails" runat="server" OnItemDataBound="Rp_TopicDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="blog-details-left">
                                            <div class="details-sulf">
                                                <asp:HiddenField ID="hd_Video" runat="server" Value='<%#Eval("VideoPostion") %>' />
                                                <asp:HiddenField ID="hd_TopicID" runat="server" Value='<%#Eval("TopicID") %>' />
                                                <asp:HiddenField ID="hd_SectionID" runat="server" Value='<%#Eval("SectionID") %>' />
                                                <asp:HiddenField ID="hd_CategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                                                <%--class id--%>
                                                <asp:HiddenField ID="hd_subjectid" runat="server" Value='<%#Eval("CourseID") %>' />
                                                <asp:HiddenField ID="hd_AudioFile" runat="server" Value='<%#Eval("AudioFile") %>' />
                                                <%--subjectid id--%>
                                                <div class="details-img" runat="server" id="topVideo">
                                                     <asp:Label ID="lbl_VideoLink" runat="server" Text='<%#Bind("VideoLink") %>' Visible="false"></asp:Label>
                                                    <iframe width="100%" height="250" src='<%#Eval("VideoLink") %>' frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                                </div>
                                                <div class="details-text">
                                                    <ul style="padding: 0px;">
                                                        <li>
                                                            <asp:Panel ID="Panel1" runat="server" Visible="false">

                                                                <audio style="width: 250px;"
                                                                    controls>
                                                                    <source src='<%#Eval("AudioFile") %>' type='audio/mp4'></audio>
                                                            </asp:Panel>

                                                        </li>
                                                        <li style="width: 100%">
                                                            <%--<p style="border-radius: 10%; background-color: #f1f3f4; padding: 5px 20px 5px 20px;  
    margin: 0px; font-size: 18px;">
                                                                <i class="fa fa-download" style="margin-right: 10px;"></i><a href='<%#Eval("Document") %>' download>Document File</a>
                                                            </p>--%>



                                                            <asp:GridView ID="grd_doclist" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="false" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" CssClass="gridview" ShowHeader="False">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr No." Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                
 

                                                                    <asp:TemplateField HeaderText="Download">
                                                                        <ItemTemplate>


                                                                            <a href='<%#Eval("Images") %>' download style="display: block; padding: 5px 0px 7px 30px; font-family: ebrima; font-size: 31px; color: #0066CC; text-decoration: none;"><i class="fa fa-download" aria-hidden="true"></i></a>



                                                                            <asp:Label ID="lbl_Images" runat="server" Text='<%#Bind("Images") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <SelectedRowStyle BackColor="#EFEFEF" Font-Bold="True" ForeColor="#CC0000" />
                                                                <HeaderStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#333333" Height="40px" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>


                                                        </li>
                                                    </ul>
                                                    <div class="det-text">
                                                        <i class="fa fa-quote-left"></i>
                                                        <p>
                                                            <asp:Literal ID="Literal9" runat="server" Text='<%#Eval("Details") %>' />
                                                        </p>
                                                    </div>
                                                    <div class="details-img" runat="server" id="bottomvideo">
                                                        <iframe width="100%" height="480" src='<%#Eval("VideoLink") %>' frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                                    </div>
                                                    <%--<asp:Button ID="btn_Next" runat="server" Text="Next" CssClass="btn-primary btn" OnClick="btn_Next_Click" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                            <%--<div class="col-lg-4">
                                <div class="blog-details-right">
                                    <div class="categories">
                                        <h3>Topics</h3>
                                        <asp:Repeater ID="RpTopicLesson" runat="server">
                                            <ItemTemplate>
                                                <p>
                                                    <b>
                                                        <asp:Literal ID="Ltslno" runat="server" Text='<%#Eval("slno") %>' />
                                                    </b>: 
                                                <asp:HyperLink ID="lessonlink" class="disable" NavigateUrl='TopicDetails.aspx?TopicId=<%#Eval("TopicID") %>&SectionID=<%#Eval("SectionID") %>' runat="server">
                                                     <%#Eval("TopicName") %></asp:HyperLink>

                                                </p>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- <script src="js/jquery.nicescroll.js" type="text/javascript"></script>--%>
        <%-- <script src="js/scripts.js" type="text/javascript"></script>--%>
        <script src="js/bootstrap.min.js" type="text/javascript"></script>


        <link href="../colorbox/colorbox.css" rel="stylesheet" />
        <script src="../colorbox/jquery.colorbox.js"></script>

        <script type="text/javascript">
            $(function () {
                //$(document).ready(function () {
                $(".login2").colorbox({ width: "20%", height: "30%", slideshow: false });




            });
        </script>
    </form>
</body>
</html>
