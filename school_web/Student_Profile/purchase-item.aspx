<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="purchase-item.aspx.cs" Inherits="school_web.Student_Profile.purchase_item" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Purchase Item
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <style>
        .form-control[disabled], .form-control[readonly], fieldset[disabled] .form-control {
            background-color: #fdfdfd;
            color: #344675;
            cursor: not-allowed;
            border-color: #cdcdcd;
        }

        .fnd-box-row-wpr {
            margin: 5px 0px 5px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .table {
            width: 100%;
            margin-bottom: 0rem;
        }

        .form-label-fnds {
            margin: 5px 0px 5px 0px;
            padding: 0px;
            width: 50%;
            float: right;
            text-align: right;
            font-weight: 400;
        }

        .fnd-btnmrgn {
            margin: 2px 0px 0px 0px !important;
            float: left;
            padding: 6px 40px;
            border-radius: 6px;
            font-weight: 600;
            font-size: 13px;
        }

        .monthgrd-wprs {
            margin: 10px 0px 0px 0px;
            padding: 0px 10px 0px 0px;
            width: 22%;
            float: left;
        }


        .feesgrds-wprs {
            margin: 10px 0px 0px 0px;
            padding: 5px;
            width: 78%;
            float: left;
            border: 1px solid #ddd;
        }

        @media only screen and (max-width:900px) {
            .form-label-fnds {
                margin: 5px 0px 5px 0px;
                padding: 0px;
                width: 100%;
                float: right;
                text-align: left;
            }

            .monthgrd-wprs {
                margin: 10px 0px 0px 0px;
                padding: 0px 10px 0px 0px;
                width: 100%;
                float: left;
            }


            .feesgrds-wprs {
                margin: 10px 0px 0px 0px;
                padding: 5px;
                width: 100%;
                float: left;
                border: 1px solid #ddd;
            }
        }
    </style>
     <script language="javascript">
        var popupWindow = null;
        function positionedPopup(url, winName, w, h, t, l, scroll) {
            settings =
                'height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + ',scrollbars=' + scroll + ',resizable'
            popupWindow = window.open(url, winName, settings)
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


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">Purchase Item <a href="home.aspx" class="pasgetitle-link">Back</a></h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">

                        <div class="monthgrd-wprs" style="width: 100%;
    text-align: center;">
                            <a   id="a1" runat="server" onclick="positionedPopup(this.href,'myWindow','950','650','200','200','yes');return false" style=" width: 10%;
        text-decoration: dashed;
        color: red">

                                <img src="Purchase_item.jpg" style="height: 225px;" />
                                <br />
                                <p style="margin: 0px;
    float: left;
    width: 100%;
    font-size: 22px;"><b style="text-decoration:underline">Click on the cart icon</b></p>

                            </a>

                  
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
