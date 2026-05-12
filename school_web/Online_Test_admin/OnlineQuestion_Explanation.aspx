<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineQuestion_Explanation.aspx.cs" Inherits="school_web.Online_Test_admin.OnlineQuestion_Explanation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Explanation</title>
    
    <script type="text/javascript" async src="https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js?config=MML_HTMLorMML-full">
    </script>
    <style type="text/css">
        .table {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .grd th {
            padding: 10px;
        }

        .grd td {
            padding: 2px;
            text-align: center;
        }

        .row {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        @media print {
            .noPrint {
                display: none;
            }
        }


        @media (min-width:260px) and (max-width:768px) {
            .prof-dt-sec {
                margin: 20px 0px 0px 0px;
            }

            .card {
                margin: 25px 0px;
                padding: 15px 5px;
                background: #fff;
            }

            .other-pg-sec {
                margin: 0px;
                padding: 20px 1px 50px 0px;
            }

            .a11 {
                display: none;
            }
        }

        @media (min-width:260px) and (max-width:768px) {

            h1, h2, h3, h4, h5, h6 {
                font-weight: bold;
                font-size: 9px !important;
            }

            .a1 {
                margin: 0px !important;
                padding: 0px !important;
                float: left !important;
                height: auto !important;
                width: 100% !important;
                overflow-x: scroll !important;
            }

            .table td {
                text-align: left;
                font-weight: normal;
                font-size: 10px;
            }

            .grd td {
                padding: 2px;
                text-align: center;
                font-size: 10px !important;
            }

            .table {
                margin: 0px;
                padding: 0px;
                height: auto;
                width: 100% !important;
                float: left !important;
            }
        }
        /*---------------tab---------*/
        @media (min-width:769px) and (max-width:992px) {
            .a1 {
                margin: 0px !important;
                padding: 0px !important;
                float: left !important;
                height: auto !important;
                width: 100% !important;
                overflow-x: scroll !important;
            }

            .table td {
                text-align: left;
                font-weight: normal;
                font-size: 10px;
            }

            .grd td {
                padding: 2px;
                text-align: center;
                font-size: 10px !important;
            }

            .table {
                margin: 0px;
                padding: 0px;
                height: auto;
                width: 100% !important;
                float: left !important;
            }

            .a1 {
                margin: 0px;
                padding: 0px;
                float: left;
                height: auto;
                width: 100%;
            }
        }

        div {
            width: 100%;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
        <div class="fullinfo">
        



        <div class="clearfix"></div>
        <div class="texbox-border" style="padding: 0px 5px; overflow: auto">
            <div class="card wow rotateInDownLeft" data-wow-delay=".50s" style="background: #fff;">
                <h2 class="other-pg-title-h" style="font-size: 20px; text-align: center; border-bottom: 1px solid #e1dadaf5;">Explanation View</h2>
                
                <asp:LinkButton ID="print1" OnClick="print1_Click" runat="server" CssClass="a11">
                    <img src="Doc/backbtn.png" height="25" alt="" width="25" border="0" style="text-align: left; float: left; margin: 7px 0px 0px 10px;background: #000;
    padding: 5px;" class="noPrint" />
                </asp:LinkButton>








                <br />
                <div class="a1">

                    <div style="margin: 0px auto; width: 1000px; padding: 0px;">
                        <div style="margin: 0px; width: 100%; padding: 0px; float: left;">

                            <div class="row" style="width: 100%; float: none; margin: 0px auto; background-color: white;" id="tblPrintIQ111" runat="server">


                                <div class="row">

                                    <table class="table" style="margin: 0px auto; float: none; width: 59%;">
                                        <tr>
                                            <td style="padding: 5px; font-weight:bold;">Test Name :- <asp:Label ID="lbl_testname" runat="server"></asp:Label></td>
                                             
                                                                 

                                            
                                        </tr>






                                    </table>



                                </div>
                                <div class="row">



                                    <asp:Panel ID="panel_english" runat="server">
                                        <asp:GridView ID="grd_view_english" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="">
                                            <Columns>



                                                <asp:TemplateField HeaderText="Question & Explanation">
                                                    <ItemTemplate>
                                                        <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">

                                                            <tr>
                                                                <td style="padding: 1px 0px 1px 0px;">
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text='<%#Bind("Question_no") %>'></asp:Label>)  
                                                            <asp:Label ID="lbl_Question_name" runat="server" Text='<%#Bind("Question_name") %>' Style="color: red;"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lbl_Question_name_HN" runat="server" Text='<%#Bind("Question_name_HN") %>' Style="color: blue;"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 1px 0px 1px 0px; color: green">Ans 
                                                    <asp:Label ID="lbl_inputen1" runat="server" Text='<%#Bind("Explanation_en") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                </div>




                            </div>
                        </div>
                    </div>

                </div>
            </div>



        </div>
    </div>
    </form>
</body>
</html>
