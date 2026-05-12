<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="school_web.Library_Admin.images.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:0px;padding:0px; float:left;width:100%">
            <asp:Label ID="lbl_con" runat="server"  ></asp:Label>
        </div>

           <div style="margin:0px;padding:0px; float:left;width:100%">
               <div>
            <table style="width: 100%; height: 648px;">
                <tr style="width: 100%;">
                    <td rowspan="3" style="vertical-align: top" class="auto-style1">
                        <asp:Label ID="lbl_message" Style="color: red;" runat="server" Text="HNJHKLJ"></asp:Label>
                        <div style="margin: 0px; padding: 0px; float: left; overflow: auto; width: 291px; height: 582px">
                            <asp:GridView ID="grd_tables" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Candara" Font-Size="12px" ForeColor="Black" GridLines="None" Width="287px">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Table List">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_show_data" OnClick="btn_show_data_Click" Text="Show Data" runat="server"></asp:LinkButton>
                                            <br />
                                            <asp:LinkButton ID="btn_table_structure" OnClick="btn_table_structure_Click" runat="server" Text='Table Structure'></asp:LinkButton>
                                            <asp:Label ID="lbl_data" runat="server" Visible="false" Text='<%#Bind("TABLE_NAME") %>'></asp:Label>
                                            <asp:LinkButton ID="lnk_generatescript" OnClick="lnk_generatescript_Click" runat="server" Text='Generate script'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />

                            </asp:GridView>
                        </div>
                    </td>
                    <td style="width: 100%; height: 150px;">
                        <asp:TextBox ID="TextBox1" Style="width: 972px; height: 80px; vertical-align: top" runat="server" TextMode="MultiLine" placeholder="Rough..............."></asp:TextBox><br />
                        <asp:Label ID="lbl_table" runat="server"></asp:Label><br />
                        <asp:TextBox ID="txt_query" Style="width: 972px; height: 120px; vertical-align: top" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                        <asp:Button ID="btn_execute" runat="server" Text="Execute" OnClick="btn_execute_Click" />
                        &nbsp;<asp:Button ID="btn_excel" runat="server" OnClick="btn_excel_Click" Text="excel download" />
                        &nbsp;
                       <%-- <asp:Button ID="btn_selerate_script" runat="server" OnClick="btn_selerate_script_Click" Text="generate Script" />--%>
                    </td>

                </tr>

                <tr style="width: 100%; vertical-align: top">
                    <td class="auto-style2">
                        <div style="margin: 0px; padding: 0px; float: left; overflow: auto; width: 100%; height: 448px">
                            <asp:GridView ID="grd_data" Style="width: 100%;" runat="server" AllowPaging="True" OnPageIndexChanging="grd_data_PageIndexChanging" PageSize="100" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" Font-Names="Ebrima" Font-Size="12px" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                            <br />
                            <asp:TextBox ID="txt_code" runat="server" TextMode="MultiLine" Height="300px" Width="100%" Visible="false"></asp:TextBox>
                        </div>

                    </td>


                </tr>

                <tr>
                    <td style="padding-top: 30px">
                        
                       <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Forwading Data Date Arrange" OnClick="Button2_Click" />--%>
                    </td>
                </tr>
            </table>
        </div>
               </div>



    </form>
</body>
</html>
