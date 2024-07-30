<%@ Page Title="Juego de Ahorcado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AhorcadoAspNet.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container">
            <h1>Juego de Ahorcado</h1>
            <asp:Literal ID="wordContainer" runat="server"></asp:Literal>
            <asp:Image ID="hangmanImage" runat="server" ImageUrl="~/img/Ahorcado0.jpg" />
            <div class="alphabet-buttons">
                <asp:Repeater ID="AlphabetRepeater" runat="server">
                    <ItemTemplate>
                        <asp:Button ID="btnLetter" runat="server" Text='<%# Eval("Letter") %>' OnClick="LetterButton_Click" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
            <asp:Button ID="restartButton" runat="server" Text="Reiniciar" OnClick="RestartButton_Click" Visible="false" />
            <asp:Literal ID="wrongLettersContainer" runat="server"></asp:Literal>
        </div>
    </main>
</asp:Content>
