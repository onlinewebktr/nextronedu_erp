<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Online_Apply_Result.aspx.cs" Inherits="school_web.Admin.slip.Print_Online_Apply_Result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Result Card</title>
    <link href="../../Examination_Admin/slip/assets/css/print_page_admitcard.css" rel="stylesheet" />
    <style>
        body, #form1 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            color: #000 !important;
        }

        table {
            width: 100%;
        }

        table, tr, td {
            border: 1px solid #000;
        }

        table, tr, th {
            border: 1px solid #000;
            margin: 0px 0px 0px 0px;
            padding: 5px 10px 5px 10px;
            height: auto;
            font-size: 14px;
            color: #222;
            text-align: left;
            vertical-align: middle;
            font-family: 'Open Sans', sans-serif;
            border-collapse: collapse;
        }

            table td {
                margin: 0px 0px 0px 0px;
                padding: 3px 10px 3px 10px;
                height: auto;
                font-size: 12px;
                line-height: 17px;
                color: #222;
                text-align: left;
                vertical-align: middle;
                font-family: 'Open Sans', sans-serif;
            }

        .linehight1 {
            height: 50px !important;
        }

        @media print {
            .noPrint {
                display: none;
            }
        }

        .printpage-sec-main {
            margin: 0px 0px 0px 0px !important;
        }

        .sig-dv {
            margin: 0;
            padding: 74px 0px 0px 0px;
            width: 100%;
            float: left;
        }
    </style>

    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <section>

            <div class="printheadse333">

                <div class="pimages22" style="float: left;">
                    <asp:ImageButton ID="ImageButton_print" CssClass="pimages33 noPrint" OnClick="ImageButton_print_Click" runat="server" Style="background: #139e1c;" ImageUrl="../../Examination_Admin/slip/assets/images/print.png" />
                    <a id="A1" runat="server" class="pimages44 noPrint" title="Back">
                        <img class="backpring3" src="../../Examination_Admin/slip/assets/images/backbtn.png" /></a>
                </div>



                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                    <ItemTemplate>

                        <div style="width: 100%; padding: 0px; margin: 0px; height: 1165px; float: left;">



                            <div class="printpage-sec-main">

                                <div class="headdivv">
                                    <div class="printlogo4455">
                                        <asp:Image ID="schoollogo" runat="server" ImageUrl="printi_mg/logo.png" class="printlogo" />
                                    </div>

                                    <div class="schoolnameheadin">

                                        <asp:Label ID="lbl_school_name" CssClass="informatchild22-lab" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_under" CssClass="informatchild22-pp33" runat="server" Visible="false" Text="Under the aegis of Delhi Public School Society, New Delhi "></asp:Label>
                                        <asp:Label ID="lbl_affilation_no" CssClass="informatchild22-pp33 pp33italic" runat="server" Text="2430043"></asp:Label>
                                        <asp:Label ID="lbl_address" CssClass="informatchild22-pp33 pp33italic" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_mobileno_emailid" CssClass="informatchild22-pp33 pp33italic" runat="server"></asp:Label>

                                        <div class="acardd">
                                            <asp:Label ID="lbl_termname" CssClass="admitcarterm1" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_session" CssClass="admitcarterm2" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="printlogo4455">
                                    </div>

                                </div>

                                <div class="nammarggg">

                                    <div class="name1sec">
                                        <div class="namehead">
                                            <p class="nameheadp">STUDENT'S NAME <span class="spdottt">:</span> </p>
                                            <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll" Text='<%#Bind("Name")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">DATE OF BIRTH <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_dob" runat="server" CssClass="nameheadll" Text='<%#Bind("DOB")%>'></asp:Label>
                                        </div>
                                        <div class="namehead">
                                            <p class="nameheadp">FATHER'S NAME <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll" Text='<%#Bind("Father_name")%>'></asp:Label>

                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">MOBILE NO. <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_mother_name" runat="server" CssClass="nameheadll" Text='<%#Bind("Student_mob_no")%>'></asp:Label>
                                        </div>


                                    </div>

                                    <div class="name1sec">
                                        <div class="namehead">
                                            <p class="nameheadp">Apply ID <span class="spdottt">:</span> </p>
                                            <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll" Text='<%#Bind("Registration_id")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">CLASS <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("Class_id")%>'></asp:Label>
                                            <asp:Label ID="lbl_class_sec" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text='<%#Bind("Course_Name")%>'></asp:Label>

                                            <asp:Label ID="lbl_Session_name" runat="server" Visible="false" Text='<%#Bind("Session_name")%>'></asp:Label>
                                        </div>




                                    </div>

                                    <div class="studimggr">
                                        <asp:Image ID="studentlogo" runat="server" ImageUrl='<%#Bind("Student_img")%>' class="studimggrt" Style="height: 110px; width: 98px;" />
                                        <%--  <img src="images/dummy-student.jpg" class="studimggrt" />--%>
                                    </div>

                                </div>



                                <div class="printlogo4455" style="display: none">
                                    <img src="printi_mg/logo.png" class="img-responsive printlogo" />

                                    <asp:Image ID="studentlogo11" runat="server" ImageUrl='<%#Bind("Student_img")%>' class="img-responsive printlogo" Style="height: 133px; width: 120px;" />
                                </div>

                                <div class="administration-paragraph">

                                    <table>
                                        <tr>
                                            <th colspan="4" style="text-align: center; background-color: #f7f7f7">EXAMINATION Result  </th>
                                        </tr>

                                        <tr>
                                            <asp:GridView ID="grid_grade" runat="server" AutoGenerateColumns="False" Style="width: 100%">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATTENDANCE STATUS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Attendance_Status" runat="server" Text='<%#Bind("Attendance_Status") %>' Style="font-weight: bold;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="EXAM RESULT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Exam_Result" runat="server" Text='<%#Bind("Exam_Result") %>' Style="font-weight: bold;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>





                                                </Columns>

                                            </asp:GridView>

                                        </tr>
                                    </table>




                                    <div class="sig-dv">
                                        <div class="sig-left">
                                            <div class="lft-sig-img-dv">
                                                <asp:Image ID="Image1" runat="server" class="lft-sig-img" />
                                            </div>
                                            <p class="sig-ps">CLASS TEACHER</p>
                                        </div>
                                        <div class="sig-left">
                                            <div class="cntr-sig-img-dv">
                                                <asp:Image ID="Image2" runat="server" class="cntr-sig-img" />
                                            </div>
                                            <p class="sig-ps">EXAMINATION INCHARGE</p>
                                        </div>
                                        <div class="sig-left">
                                            <div class="rght-sig-img-dv">
                                                <asp:Image ID="Image3" runat="server" class="rght-sig-img" />
                                            </div>
                                            <p class="sig-ps">PRINCIPAL</p>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>


            </div>

        </section>
    </form>
</body>
</html>
