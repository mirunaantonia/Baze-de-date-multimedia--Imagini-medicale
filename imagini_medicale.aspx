<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imagini_medicale.aspx.cs" Inherits="proiect_imagini_medicale_vasilemiruna.imagini_medicale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" BackColor="#FF99CC" BorderColor="#FF0066" BorderStyle="Inset" ForeColor="White" Text="Gestionarea imaginilor medicale" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <p>
            <asp:Label ID="Ecou" runat="server" ForeColor="#FF6699" Text="Zona destinata erorilor "></asp:Label>
        </p>
        <asp:Label ID="Label2" runat="server" BackColor="#FF6699" Text="Incarcare imagini medicale" Font-Bold="True" Font-Italic="True" Font-Size="X-Large"></asp:Label>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Cod imagine :"></asp:Label>
            <asp:TextBox ID="tb_cod" runat="server" BackColor="#FF99CC" Width="157px"></asp:TextBox>
        </p>
        <asp:Label ID="Label4" runat="server" Text="Descrierea imaginii :"></asp:Label>
        <asp:TextBox ID="tb_descriere" runat="server" BackColor="#FF99CC" Height="48px" Width="935px"></asp:TextBox>
        <p>
            <asp:Label ID="Label5" runat="server" Text="Insereaza imaginea: "></asp:Label>
            <asp:FileUpload ID="incarcareimagine" runat="server" BackColor="#FF99CC" Height="45px" Width="429px" />
        </p>
        <asp:Label ID="Label6" runat="server" Text="Data incarcare :"></asp:Label>
        <asp:TextBox ID="tb_dataincarcare" runat="server" BackColor="#FF99CC" Height="31px" Width="206px"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" Text="(introduceti o data in formatul DD-MM-YYYY)"></asp:Label>
        <p>
            <asp:Label ID="Label8" runat="server" Text="Rezultate analiza : "></asp:Label>
            <asp:TextBox ID="tb_rezultateanaliza" runat="server" BackColor="#FF99CC" Height="42px" Width="954px"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="IncarcaImagineaInSistem" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Outset" ForeColor="White" Height="53px" OnClick="IncarcaImagineaInSistem_Click" Text="Incarca imaginea in sistem" Width="644px" Font-Bold="True" Font-Size="Large" />
        </p>
        <p>
            <asp:Label ID="Label9" runat="server" BackColor="#FF6699" Font-Bold="True" Font-Italic="True" Font-Size="X-Large" Text="Vizualizare imagini medicale incarcate in sistem"></asp:Label>
        </p>
        <asp:Label ID="Label10" runat="server" Text="Cod imagine: "></asp:Label>
        <asp:TextBox ID="codafisareimagine" runat="server" BackColor="#FF99CC"></asp:TextBox>
        <p>
            <asp:Button ID="afisareimagine" runat="server" BackColor="Black" Font-Bold="True" Font-Size="Large" ForeColor="White" Height="79px" OnClick="afisareimagine_Click" Text="Afisare Imagine" Width="272px" />
            <asp:Image ID="imagineaafisata" runat="server" BackColor="#FF99CC" Height="260px" Width="437px" />
        </p>
        <p>
            <asp:TextBox ID="afisaredescriere" runat="server" BackColor="#FF99CC" Width="824px"></asp:TextBox>
        </p>
        <asp:TextBox ID="afisaredataincarcare" runat="server" BackColor="#FF99CC" Width="824px"></asp:TextBox>
        <p>
            <asp:TextBox ID="afisarerezultate" runat="server" BackColor="#FF99CC" Width="822px"></asp:TextBox>
        </p>
        <asp:Label ID="Label11" runat="server" BackColor="#993366" Font-Bold="True" Font-Size="X-Large" Text="SCINTIGRAFIE (Imagini de medicina nucleara)"></asp:Label>
        <p>
            <asp:Label ID="Ecouu" runat="server" BackColor="#660033" ForeColor="White" Text="Sectiunea de erori"></asp:Label>
        </p>
        <asp:Label ID="substantaradioactiva" runat="server" BackColor="#993366" Text="Substanta radioactiva"></asp:Label>
        <asp:CheckBox ID="technetiu" runat="server" Text="Technetiu      " />
        <asp:CheckBox ID="iod" runat="server" Text="IOD           " />
        <asp:CheckBox ID="Cfluor" runat="server" Text="Fluor" />
        <p>
            <asp:Label ID="numepacient" runat="server" BackColor="#993366" Text="Nume pacient   :"></asp:Label>
            <asp:TextBox ID="numepacientintrodus" runat="server"></asp:TextBox>
            <asp:Label ID="Label12" runat="server" BackColor="#660033" Text="            ID:"></asp:Label>
            <asp:TextBox ID="idnuclear" runat="server"></asp:TextBox>
        </p>
        <asp:FileUpload ID="imaginenucleara" runat="server" />
        <br />
        <asp:Button ID="incarcanucleara" runat="server" OnClick="incarcanucleara_Click" Text="Incarca imaginea de medicina nucleara" />
        <p>
            <asp:Button ID="genereazasemnatura" runat="server" Height="57px" OnClick="genereazasemnatura_Click" Text="Genereaza semnatura" Width="812px" />
        </p>
        <asp:Label ID="Label13" runat="server" BackColor="#660033" ForeColor="White" Text="Coeficientul de importanta al culorii"></asp:Label>
        <asp:TextBox ID="culoare" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="Label14" runat="server" BackColor="#660033" ForeColor="White" Text="Coeficientul de textura: "></asp:Label>
            <asp:TextBox ID="textura" runat="server"></asp:TextBox>
        </p>
        <asp:Label ID="Label15" runat="server" BackColor="#660033" ForeColor="White" Text="Coeficientul forma: "></asp:Label>
        <asp:TextBox ID="forma" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="Label16" runat="server" BackColor="#660033" ForeColor="White" Text="Coeficientul locatie :"></asp:Label>
            <asp:TextBox ID="locatie" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="imaginearezultat" runat="server" BackColor="#FF6699" Text="ID-ul imaginii rezultat: "></asp:Label>
            <asp:TextBox ID="idrezultat" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="regasireimagine" runat="server" BackColor="#CC6699" Height="83px" OnClick="regasireimagine_Click" Text="Buton regasire imagine" Width="339px" />
            <asp:Image ID="imagineaa" runat="server" BackColor="#660033" Height="201px" Width="306px" />
            <asp:Button ID="butonafisid" runat="server" BackColor="#CC6699" OnClick="butonafisid_Click" style="margin-top: 0px" Text="Buton afisare imagine cu ID-ul rezultat" Width="513px" />
        </p>
        <br />
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
