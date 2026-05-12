<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="birthday.aspx.cs" Inherits="school_web.Student_Profile.birthday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Birthday
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Lobster&display=swap" rel="stylesheet" />
    <style>
        .birthday-dv {
            margin: 40px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            text-align: center;
        }

        .birthday-dv-h {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-family: 'Lobster', cursive;
            font-size: 30px;
            color: #fb00b9 !important;
            letter-spacing: 1px;
        }

        .birthday-dv-p {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 15px;
        }

        .birthday-dv-img {
            margin: 0px;
            padding: 0px;
            width: 50%;
        }

        .birthday-na-dv {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            text-align: center;
            display: flex;
            align-items: center;
            height: 300px;
        }

        .birthday-dv-na-p {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 26px;
            color: #ed0000 !important;
            font-weight: 300;
            letter-spacing: .5px;
        }

        .card-body {
            background: url(assets/images/birthday-bg.png) no-repeat;
            background-position: center;
            background-size: 130%;
        }

        @media only screen and (max-width:900px) {
            .birthday-dv {
                margin: 80px 0px 0px 0px;
            }

            .birthday-dv-img {
                width: 70%;
            }

            .birthday-dv-na-p {
                font-size: 18px;
            }

            .card-body {
                background-repeat: repeat;
                background-size: 142%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">
            <div class="main-card mb-3 card">
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="birthday-dv" id="is_birthday_yes" runat="server" visible="false">
                                    <h2 class="birthday-dv-h">Happy Birthday</h2>
                                    <p class="birthday-dv-p">You were an exceptional student and i hope you also excel in this new chapter of your life..!</p>
                                    <img src="assets/images/birthday_balloon.png" class="birthday-dv-img" />
                                </div>
                                <div class="birthday-na-dv" id="is_birthday_no" runat="server" visible="false">
                                    <p class="birthday-dv-na-p">Today is not your birthday.</p>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>


</asp:Content>
