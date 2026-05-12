<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="read-ebbok.aspx.cs" Inherits="school_web.Student_Profile.read_ebbok" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Terms & Conditions</title>

    <!-- Bootstrap Core CSS -->
  
    <link href="css/bootstrap.min.css" rel="stylesheet" /> 
    <!-- Custom Fonts -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic,900,900italic' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="font-awesome/css/font-awesome.min.css" type="text/css" />

    <style>
        .close-btn {
            margin: 0px;
            padding: 0px;
            position: fixed;
            top: 0px;
            right: 0px;
            font-size: 25px;
            z-index: 999999;
            color: #222;
            width: 40px;
            height: 40px;
            text-align: center;
            line-height: 40px;
            text-decoration: none !important;
        }

            .close-btn:hover {
                background: #444 !important;
                color: white !important;
                border-radius: 0px 0px 0px 3px;
                text-decoration: none !important;
            }

        .wowbook-close {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_pdf_path" runat="server" />
        <a href="ebook.aspx" class="close-btn">✕</a>
        <div id='book2'></div>
        <style>
            .hidden-FIXME {
                display: none;
            }

            #portfolio {
                display: none;
            }

            header {
                display: none;
            }

            #contact {
                display: none;
            }

            #about {
                display: none;
            }

            .navbar-header {
                width: 100% !important;
            }

            .navbar-brand {
                padding-right: 0;
                font-size: 14px !important;
            }

            #nav-buy-now {
                color: black;
                float: right;
                font-size: 14px;
                font-weight: 700;
                margin-right: -10px;
            }

            @media (min-width: 768px) {
                #nav-buy-now {
                    margin-right: 0px;
                }
            }

            #bs-example-navbar-collapse-1 {
                display: none;
            }

            .wowbook {
                font-family: "Open Sans","Helvetica Neue",Arial,sans-serif;
            }

            .wowbook-page-content {
                padding: 1.5em;
            }

            .wowbook ul {
                padding-left: 1em;
            }

            .book-thumb {
                height: 150px;
                box-shadow: 0 0 3px rgba(0, 0, 0, 0.5);
            }

            #book1-trigger, #book2-trigger, #book3-trigger {
                cursor: pointer;
            }

                #book1-trigger:hover, #book2-trigger:hover, #book3-trigger:hover {
                    background: #f8f8f8;
                }

            .wowbook-lightbox > .wowbook-close {
                background: transparent !important;
                border: none !important;
                color: #222 !important;
                font-size: 2.5em;
            }

                .wowbook-lightbox > .wowbook-close:hover {
                    background: #444 !important;
                    color: white !important;
                    border-radius: 3px;
                }


            .lightbox-images1 .wowbook-book-container {
                background: #6d6b92; /* Old browsers */
                background: -moz-radial-gradient(center, ellipse cover, #ffffff 0%, #6d6b92 100%); /* FF3.6-15 */
                background: -webkit-radial-gradient(center, ellipse cover, #ffffff 0%,#6d6b92 100%); /* Chrome10-25,Safari5.1-6 */
                background: radial-gradient(ellipse at center, #ffffff 0%,#6d6b92 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            }

            .lightbox-images1 > .wowbook-close,
            .lightbox-images2 > .wowbook-close {
                color: #ccc !important;
            }

            .lightbox-images2 .wowbook-book-container {
                background: #1E2831; /* Old browsers */
                background: -moz-radial-gradient(center, ellipse cover, #ffffff 0%, #1E2831 100%); /* FF3.6-15 */
                background: -webkit-radial-gradient(center, ellipse cover, #ffffff 0%,#1E2831 100%); /* Chrome10-25,Safari5.1-6 */
                background: radial-gradient(ellipse at center, #ffffff 0%,#1E2831 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            }



            .lightbox-pdf .wowbook-book-container {
                background: #e5e5e5 url(./img/bg-lightbox-pdf.png); /* Old browsers */
                background: #e5e5e5 -moz-radial-gradient(center, ellipse cover, #ffffff 20%, #bbbbbb 100%); /* FF3.6-15 */
                background: #e5e5e5 -webkit-radial-gradient(center, ellipse cover, #ffffff 20%,#bbbbbb 100%); /* Chrome10-25,Safari5.1-6 */
                background: #e5e5e5 radial-gradient(ellipse at center, #ffffff 20%,#bbbbbb 100%); /* W3C, IE10+, FF16+, Chrome26+,Opera12+, Safari7+*/
            }


            .lightbox-html .wowbook-book-container {
                background: url(img/book_html/wood.jpg);
            }

            .lightbox-html .wowbook-toolbar {
                margin-top: 1em; /* FIXME */
                box-sizing: content-box !important;
            }

            .lightbox-html .wowbook-controls {
                border-radius: 6px;
                width: auto;
            }

            .lightbox-html.wowbook-mobile .wowbook-toolbar {
                margin: 0;
            }

            .lightbox-html.wowbook-mobile .wowbook-controls {
                border-radius: 0;
                width: 100%;
            }

            hr {
                max-width: 450px;
            }
        </style>

        <!-- jQuery -->
        <script src="assets/pdfBookView/jquery.js"></script> 
        <script>
            imageBook = ["1", "8"][Math.floor(Math.random() * 2)];
            imageBookPath = "./img/magazine_template_0" + imageBook;
            $("#book1-trigger .book-thumb").attr("src", imageBookPath + "/image_000.jpg")
        </script>
        <link href="assets/pdfBookView/wow_book.css" rel="stylesheet" />
        <style>
            .wowbook-right .wowbook-gutter-shadow {
                background-image: url("assets/pdfBookView/images/page_right_background.png");
                background-position: 0 0;
                width: 75px;
            }

            .wowbook-left .wowbook-gutter-shadow {
                background-image: url("assets/pdfBookView/images/page_left_background.png");
                opacity: 0.5;
                width: 60px;
            }

            .wowbook-control-currentPage {
                font-family: "Segoe UI",Helvetica,Arial,sans-serif;
            }
        </style>
        <script src="assets/pdfBookView/pdf.combined.min.js"></script>
        <script src="assets/pdfBookView/wow_book.min.js"></script>
        <script type="text/javascript">
            var pdfPath = $("#<%=hd_pdf_path.ClientID%>").val();
            function fullscreenErrorHandler() {
                if (self != top) return "The frame is blocking full screen mode. Click on 'remove frame' button above and try to go full screen again."
            }
            var optionsBook2 = {
                height: 1024
                , width: 725 * 2
                // ,maxWidth : 800
                // ,maxHeight : 400
                , pageNumbers: false

                , pdf: pdfPath
                , pdfFind: true
                , pdfTextSelectable: true

                , lightbox: "#book2-trigger"
                , lightboxClass: "lightbox-pdf"
                , centeredWhenClosed: true
                , hardcovers: true
                , curl: false
                //, toolbar: "lastLeft, left, currentPage, right, lastRight, find, toc, zoomin, zoomout, download, flipsound, fullscreen, thumbnails"
                , toolbar: "lastLeft, left, currentPage, right, lastRight, find, zoomin, zoomout, download, flipsound, fullscreen, thumbnails"
                , thumbnailsPosition: 'bottom'
                , responsiveHandleWidth: 50
                , onFullscreenError: fullscreenErrorHandler
            };


            var books = {
                "#book2": optionsBook2,
            };
            function openModalProduct() {
                buildBook("#book2");
            }

            function buildBook(elem) {
                var book = $.wowBook(elem);
                if (!book) {
                    $(elem).wowBook(books[elem]);
                    book = $.wowBook(elem);
                }
                book.showLightbox();
            }

        </script>
    </form>
</body>
</html>
