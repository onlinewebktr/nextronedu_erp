<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voice_Principal.aspx.cs" Inherits="school_web.About_School.Voice_Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300;0,400;1,300&display=swap" rel="stylesheet" />
    <style type="text/css">
        .body, form1 {
            margin: 0px;
            padding: 0px;
            float: left;
            font-family: 'Open Sans', sans-serif;
        }

        .fullinfo {
            margin: 0px 0px 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .hm-abot-img {
         margin: 11px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            width: 100%;
            height: auto;
            float: left;
        }

        @media (min-width: 260px) and (max-width: 768px) .thumbnail {
            .thumbnail {
                width: 100%!important;
            }
        }

        .hm-abot-title-sec {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
        }

        .othr-pg-cont-wpr-p {
            margin: 0px;
            padding: 0px 0px 21px 0px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 15px;
            line-height: 27px;
            color: #666;
            font-weight: 300;
            text-align: justify;
            letter-spacing: 0.2px;
        }

        .directormess-main {
            height: auto;
            width: 100%;
            margin: 15px 0px 20px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
        }

        .directormess-main-h1 {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 12px 0px;
            width: 100%;
            height: auto;
            font-family: 'Libre Franklin', sans-serif;
            float: left;
            text-align: left;
            font-size: 20px;
            font-weight: 600;
            line-height: 22px;
            color: #00cfd6;
            border-bottom: 1px dashed;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="fullinfo">
            <div class="container">

                <div class="container">
                    <div class="row">

                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="hm-abot-img">
                                <img class="img-responsive thumbnail" src="images/vc_img.jpg" id="img1" runat="server" />
                            </div>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                            <div class="hm-abot-title-sec">

                                <p class="othr-pg-cont-wpr-p">


                                    <asp:Label ID="lbl_abotschool" runat="server"></asp:Label>

                                </p>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
