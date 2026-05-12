<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/User.Master" AutoEventWireup="true" CodeBehind="TopicDetails.aspx.cs" Inherits="school_web.Student_Profile.TopicDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Topic Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .disable {
            pointer-events: none;
            cursor: default;
            text-decoration: none;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a> <i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Course Details</asp:LinkButton>
            <i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item">Topic Details</li>
    </ol>
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
                        <div class="col-lg-8 pt-40">
                            <asp:Repeater ID="Rp_TopicDetails" runat="server" OnItemDataBound="Rp_TopicDetails_ItemDataBound">
                                <ItemTemplate>
                                    <div class="blog-details-left">
                                        <div class="details-sulf">
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("VideoPostion") %>' />
                                            <asp:HiddenField ID="hd_TopicID" runat="server" Value='<%#Eval("TopicID") %>' />
                                            <asp:HiddenField ID="hd_SectionID" runat="server" Value='<%#Eval("SectionID") %>' />
                                            <asp:HiddenField ID="hd_CategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                                            <%--class id--%>
                                            <asp:HiddenField ID="hd_subjectid" runat="server" Value='<%#Eval("CourseID") %>' />
                                            <asp:HiddenField ID="hd_AudioFile" runat="server" Value='<%#Eval("AudioFile") %>' />
                                            <asp:HiddenField ID="hd_Video" runat="server" Value='<%#Eval("VideoPostion") %>' />
                                            <div class="details-img" runat="server" id="topVideo">


                                                <asp:Label ID="lbl_VideoLink" runat="server" Text='<%#Eval("VideoLink") %>' Visible="false"></asp:Label>
                                                <iframe width="100%" height="480" src='<%#Eval("VideoLink") %>' frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                            </div>
                                            <div class="details-text">
                                                <ul>
                                                    <li>
                                                        <audio controls><source src='<%#Eval("AudioFile") %>' type='audio/mp4'></audio>
                                                        <li>|</li>
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

                                                <div>

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
                                                </div>
                                                <asp:Button ID="btn_Next" runat="server" Text="Next" CssClass="btn-primary btn" OnClick="btn_Next_Click" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                        <div class="col-lg-4">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
