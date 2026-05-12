<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Student_Library.aspx.cs" Inherits="school_web.Library_Admin.print.Print_Student_Library" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="print.css" rel="stylesheet" />
    <link href="../css/barcode-print.css" rel="stylesheet" />
    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }

    </script>
    <style>
        @media print {
            .noPrint {
                display: none;
            }
        }

        th {
            padding: 2px;
            background-color: #dbdbdb;
            color: #000;
            font-weight: bold;
            font-size: 12px;
        }

        td {
            padding: 19px 2px 19px 2px;
            color: #000;
            border-spacing: 1px;
        }

        .barcode-img-wpr {
            margin: 0px -15px 0px 0px;
            padding: 3px 0px;
            width: 50%;
            float: left;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div class="main_div">
                <div class="main_div_iner">

                    <div class="printheadse333">

                        <div class="pimages22" style="float: left;">
                            <asp:ImageButton ID="ImageButton_print" CssClass="pimages33 noPrint" OnClick="ImageButton_print_Click" runat="server" Style="background: #139e1c;" ImageUrl="print.png" />
                            <a id="A1" runat="server" class="pimages44 noPrint" title="Back">
                                <img class="backpring3" src="backbtn.png" /></a>
                        </div>
                    </div>
                    <div style="width: 100%; padding: 0px; margin: 0px; height: 1165px; float: left;">
                        <div class="printpage-sec-main">

                            <div class="headdivv">
                                <div class="printlogo4455">
                                    <asp:Image ID="schoollogo" runat="server" ImageUrl="~/Library_Admin/print/logo_school.png" class="printlogo" />
                                </div>

                                <div class="schoolnameheadin">

                                    <asp:Label ID="lbl_school_name" CssClass="informatchild22-lab" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_under" CssClass="informatchild22-pp33" runat="server" Visible="false" Text="Under the aegis of Delhi Public School Society, New Delhi "></asp:Label>

                                    <asp:Label ID="lbl_address" CssClass="informatchild22-pp33 pp33italic" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_mobileno_emailid" CssClass="informatchild22-pp33 pp33italic" runat="server"></asp:Label>

                                    <div class="acardd">
                                        <asp:Label ID="lbl_termname" CssClass="admitcarterm1" runat="server">LIBRARY CARD -</asp:Label>
                                        <asp:Label ID="lbl_session" CssClass="admitcarterm2" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="printlogo4455">
                                 
                            <asp:Label ID="lbl_affilation_no" CssClass="informatchild22-pp33 pp33italic" runat="server" Style="float: none; display:none"></asp:Label>

                                    <asp:Image ID="student_image" runat="server" ImageUrl="~/Library_Admin/print/dummy-student.jpg" class="printlogo_student" />
                                </div>




                            </div>
                            <div class="nammarggg">
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp" style="width: 46px;">ID No. <span class="spdottt">:</span> </p>
                                        <div class="barcode-img-wpr">
                                            <asp:Image ID="img_barcode" Style="height: 83px; width: 129%; margin: -13px 0px 0px 0px;"
                                                runat="server" class="rght-sig-img" />
                                        </div>
                                        <asp:Label ID="lbl_Id_no" Visible="false" runat="server" CssClass="nameheadll"></asp:Label>

                                    </div>




                                </div>

                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp">ISSUE DATE <span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_issue_date" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>

                                </div>
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp">VALID UP TO<span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_validupto" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>
                                </div>


                            </div>
                            <div class="nammarggg">
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp" style="width: 106px;">STUDENT'S NAME <span class="spdottt">:</span> </p>
                                        <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>




                                </div>

                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp" style="width: 90px;">ADMISSION NO. <span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>

                                </div>
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp">ROLL NO. <span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_roll_no" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="nammarggg">
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp" style="width: 106px;">SECTION <span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_section" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="nammarggg">
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp" style="width: 106px">FATHER'S NAME <span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll"></asp:Label>

                                    </div>
                                </div>
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp" style="width: 106px">MOTHER'S NAME <span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_mother_name" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>
                                </div>
                                <div class="name1sec">
                                    <div class="namehead">
                                        <p class="nameheadp">CONTACT NO. <span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_contact_no" runat="server" CssClass="nameheadll"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="nammarggg">
                                <div class="name1sec" style="width: 100%;">
                                    <div class="namehead">
                                        <p class="nameheadp" style="width: 44px;">NOTE<span class="spdottt">:</span></p>
                                        <asp:Label ID="lbl_note1" runat="server" CssClass="nameheadll">Note1</asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="sig-dv">
                                <div class="sig-left">
                                    <div class="lft-sig-img-dv">
                                    </div>
                                    <p class="sig-ps">Student Signature</p>
                                </div>
                                <div class="sig-left">
                                    <div class="cntr-sig-img-dv">
                                    </div>
                                    <p class="sig-ps">Library Incharge Signature</p>
                                </div>
                                <div class="sig-left">
                                    <div class="rght-sig-img-dv">
                                        <asp:Image ID="Image3" runat="server" class="rght-sig-img" />
                                    </div>
                                    <p class="sig-ps">Principal Signature</p>
                                </div>
                            </div>


                        </div>

                        <div class="printpage-sec-main" style="margin: 13px 0px 0px 0px;">
                            <div style="margin: 0px 0px 0px 0px; padding: 0px; height: auto; text-align: center;  font-weight: bold">
                                <asp:Label ID="lbl_school_name21" CssClass="informatchild22-lab" style="font-size: 15px;" runat="server">Book Assined</asp:Label>
                            </div>

                            <div style="margin: 0px; padding: 0px; height: auto; width: 100%; float: left;">
                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%" border="1" cellspacing="0">
                                    <tr>
                                        <th>Sr.</th>
                                        <th>Issue Date</th>
                                        <th>Book ID</th>
                                        <th>Issue ID</th>
                                        <th>Bar code</th>
                                        <th>Signature</th>
                                        
                                        
                                    </tr>
                                    <tr>
                                        <td>1.</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        
                                    </tr>
                                      <tr>
                                        <td>2.</td>
                                       <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        
                                    </tr>
                                      <tr>
                                        <td>3.</td>
                                         <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>3.</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>5.</td>
                                       <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>6.</td>
                                         <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>7.</td>
                                         <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>8.</td>
                                         <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>9.</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>10</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                   

                                </table>
                                    <div class="sig-dv">
                                    <div class="sig-left">
                                        <div class="lft-sig-img-dv" style="display:none">
                                        </div>
                                        <p class="sig-ps" style="display:none"></p>
                                    </div>
                                    <div class="sig-left">
                                        <div class="cntr-sig-img-dv">
                                        </div>
                                        <p class="sig-ps">Library incharge Signature</p>
                                    </div>
                                    <div class="sig-left">
                                        <div class="rght-sig-img-dv">
                                            <asp:Image ID="Image1_princip" runat="server" class="rght-sig-img" />
                                        </div>
                                        <p class="sig-ps">Principal Signature</p>
                                    </div>
                                </div>
                            </div>

                        </div>



                    </div>
                </div>
            </div>



        </section>
    </form>
</body>
</html>
