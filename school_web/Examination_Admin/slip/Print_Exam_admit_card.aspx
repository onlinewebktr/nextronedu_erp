<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Exam_admit_card.aspx.cs" Inherits="school_web.Examination_Admin.slip.Print_Exam_admit_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Admit Card</title>
    <%-- <script src="../../js/jquery-1.10.2.min.js"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="assets/css/print_page_admitcard.css" rel="stylesheet" />

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
            padding: 2px 10px 2px 10px;
            height: auto;
            font-size: 13px;
            color: #222;
            text-align: left;
            vertical-align: middle;
            font-family: 'K2D';
            border-collapse: collapse;
        }

            table td {
                margin: 0px 0px 0px 0px;
                padding: 2px 10px 2px 10px;
                height: auto;
                font-size: 12px;
                line-height: 17px;
                color: #222;
                text-align: left;
                vertical-align: middle;
                font-family: 'K2D';
            }

            table, tr td span {
                font-weight: 500 !important;
            }

        .linehight1 {
            height: 30px !important;
        }

        @media print {
            .noPrint {
                display: none;
            }
        }

        .printpage-sec-main {
            margin: 0px 0px 0px 0px !important;
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
                    <asp:ImageButton ID="ImageButton_print" CssClass="pimages33 noPrint" OnClick="ImageButton_print_Click" runat="server" Style="background: #139e1c;" ImageUrl="~/Examination_Admin/slip/print.png" />
                    <a id="A1" href="~/Examination_Admin/Exam_Time_Table_List.aspx" runat="server" class="pimages44 noPrint" title="Back">
                        <img class="backpring3" src="backbtn.png" /></a>
                </div>



                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                    <ItemTemplate>
                        <div style="width: 100%; padding: 0px; margin: 0px; height: 1049px; float: left;">
                            <div class="printpage-sec-mainadmtcrd">
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
                                            <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll" Text='<%#Bind("studentname")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">DATE OF BIRTH <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_dob" runat="server" CssClass="nameheadll" Text='<%#Bind("dob")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">MOTHER'S NAME <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_mother_name" runat="server" CssClass="nameheadll" Text='<%#Bind("mothername")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">FATHER'S NAME <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll" Text='<%#Bind("fathername")%>'></asp:Label>
                                        </div>
                                        <div class="namehead">
                                            <p class="nameheadp">MOBILE [FATHER] <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_mob_father" runat="server" CssClass="nameheadll" Text='<%#Bind("father_mob")%>'></asp:Label>
                                        </div>
                                        <div class="namehead">
                                            <p class="nameheadp">MOBILE [MOTHER] <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_mob_mother" runat="server" CssClass="nameheadll" Text='<%#Bind("mother_mob")%>'></asp:Label>
                                        </div>



                                    </div>

                                    <div class="name1sec">
                                        <div class="namehead">
                                            <p class="nameheadp">ADMISSION NO. <span class="spdottt">:</span> </p>
                                            <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">CLASS & SEC. <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("Class_id")%>'></asp:Label>
                                            <asp:Label ID="lbl_class_sec" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text='<%#Bind("class")%>'></asp:Label>

                                            <asp:Label ID="lbl_section" runat="server" CssClass="nameheadll" Style="float: none; padding: 0px 0px 0px 5px;"
                                                Text='<%#Bind("Section")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">ROLL NO. <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_roll_no" runat="server" CssClass="nameheadll" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">HOUSE <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_house" runat="server" CssClass="nameheadll" Text='<%#Bind("house_name")%>'></asp:Label>
                                        </div>
                                        <div class="namehead">
                                            <p class="nameheadp">MOBILE [SELF] <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_mob_self" runat="server" CssClass="nameheadll" Text='<%#Bind("selfmobileno")%>'></asp:Label>
                                        </div>
                                        <div class="namehead">
                                            <p class="nameheadp" style="width: 100%">PARENT'S SIGNATURE:</p>
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
                                            <th colspan="4" style="text-align: center; background-color: #f7f7f7">EXAMINATION STARTING DATE & TIME  </th>
                                        </tr>

                                        <tr>
                                            <asp:GridView ID="grid_grade" runat="server" AutoGenerateColumns="False" Style="width: 100%">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_lbl_dateofexam" runat="server" Text='<%#Bind("Exam_datetime1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TIME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_exam_time1" runat="server" Text='<%#Bind("Exam_time1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DAY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_day" runat="server" Text='<%#Bind("Day") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SUBJECT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_subject" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SIGNATURE OF THE INVIGILATOR">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="linehight1" />
                                                    </asp:TemplateField>




                                                </Columns>

                                            </asp:GridView>

                                        </tr>
                                    </table>

                                    <div class="courses-sec33">
                                        <table>
                                            <tr>
                                                <td colspan="2"><b>IMPORTANT NOTE:</b> Instructions to be strictly complied with;</td>
                                            </tr> 
                                            <tr>
                                                <td style="width: 70px;">01</td>
                                                <td>Please check all the particulars carefully and inform the Class Teacher within a week from the date of issue of this card, if correction is required.</td>
                                            </tr> 
                                            <tr>
                                                <td>02</td>
                                                <td>This card is validated for the school 
                                                    <asp:Label ID="lbl_examtermname" runat="server"></asp:Label>/Annual Examination-
                                                    <asp:Label ID="lbl_exam_years1" runat="server">2023</asp:Label>
                                                    and subject to the clearance of dues up to August-2023/March-
                                                    <asp:Label ID="lbl_exam_years" runat="server"></asp:Label>
                                                    (IF ANY) 
                                                </td>
                                            </tr> 
                                            <%--<tr>
                                                <td style="width: 70px;">03</td>
                                                <td>Student needs to put their subject name and self signature against the examination  in the presence of the Invigilator.</td>
                                            </tr>--%> 
                                            <tr>
                                                <td>03</td>
                                                <td>Keep this Admit Card in safe custody for future references.</td>
                                            </tr> 
                                        </table>
                                    </div>


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

                                    <%--<div class="signatseccc">
                                        <p class="signatpp">CLASS TEACHER</p>
                                    </div>

                                    <div class="signatseccc">
                                        <p class="signatpp">EXAMINATION INCHARGE</p>
                                    </div>

                                    <div class="signatseccc">
                                        <p class="signatpp">PRINCIPAL</p>
                                    </div>--%>
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
