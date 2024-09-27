Imports System.Net.Sockets
Imports System.Text
Imports System.Net


Public Class Anasayfa


    Dim GelenDatalar As New ListBox
    Dim DinlenilecekipAdres As String = "255.255.255.255"
    Dim VeriGelenPort As Integer = 2017
    Dim Udp As UdpClient
    Dim Result As IAsyncResult = Nothing


    Private Sub Udp_Dinle()
        Result = Udp.BeginReceive(AddressOf Receive, New Object())
    End Sub

    Private Sub Receive(ByVal Result As IAsyncResult)
        Dim VerininGeldigiipAdres As IPEndPoint = New IPEndPoint(IPAddress.Parse(DinlenilecekipAdres), VeriGelenPort)
        Dim GelenByte As Byte() = udp.EndReceive(Result, VerininGeldigiipAdres)
        Dim GelenData As String = Encoding.GetEncoding("ISO-8859-9").GetString(GelenByte)

        If GelenDatalar.Items.Contains(GelenData) = False Then
            GelenDatalar.Items.Add(GelenData)
        End If
        Udp_Dinle()

    End Sub



    Private Sub Ayarları_Gonder_Buton_Click(sender As Object, e As EventArgs) Handles Ayarları_Gonder_Buton.Click
        Dim TabelaOlcusu() As String = Tabela_Olcusu_Text.Text.Split("x")

        Dim IPSplit() As String = Ip_Adres_Text.Text.Split(".")
        Dim MaskeSplit() As String = Alt_Ag_Maskesi.Text.Split(".")
        Dim GecitSplit() As String = Alt_Ag_Gecidi_Text.Text.Split(".")
        Dim DnsSunucuSplit() As String = Dns_Sunucu_Adres_Text.Text.Split(".")

        Dim AyarFormatı As String = "$PSS" ' Panel Setting
        Dim PanelMacID As String = Panel_Mac_ID_Text.Text
        Dim PanelID As String = Panel_Ayar_ID_Text.Text.PadLeft(3, "0")
        Dim Olcu1 As String = TabelaOlcusu(0).PadLeft(3, "0")
        Dim Olcu2 As String = TabelaOlcusu(1).PadLeft(3, "0")
        Dim OlcuSperator As Char = "x"
        Dim CihazAdı As String = Panel_Adı_Text.Text.PadRight(16, " ")

        Dim LogoFontu As String = Logo_Fontu_Text.SelectedIndex
        Dim AnlıkYazıFontu As String = Anlık_Yazı_Fontu_Text.SelectedIndex
        Dim AnlıkYazıSıgmazsaKaydır As Char = Math.Abs(CInt(Anlık_Yazı_Panele_Sıgdırsa_Kaydır_Check.Checked)).ToString
        Dim AnlıkYazıyıOrtala As Char = Math.Abs(CInt(Anlık_Yazıyı_Ortaya_Yaz_Check.Checked)).ToString
        Dim EkranParlaklık As String = Ekran_Parlaklık_Trackbar.Value.ToString("D3")


        Dim Logo1Yazısı As String = Logo_1_Yazısı_Text.Text.PadRight(35, " ")
        Dim Logo2Yazısı As String = Logo_2_Yazısı_Text.Text.PadRight(35, " ")
        Dim Logo3Yazısı As String = Logo_3_Yazısı_Text.Text.PadRight(35, " ")
        Dim Logo4Yazısı As String = Logo_4_Yazısı_Text.Text.PadRight(35, " ")

        Dim Logo1Kaydır As Char = Math.Abs(CInt(Logo_1_Kaydır_Check.Checked)).ToString
        Dim Logo1Ortala As Char = Math.Abs(CInt(Logo_1_Ortala_Check.Checked)).ToString

        Dim Logo2Kaydır As Char = Math.Abs(CInt(Logo_2_Kaydır_Check.Checked)).ToString
        Dim Logo2Ortala As Char = Math.Abs(CInt(Logo_2_Ortala_Check.Checked)).ToString

        Dim Logo3Kaydır As Char = Math.Abs(CInt(Logo_3_Kaydır_Check.Checked)).ToString
        Dim Logo3Ortala As Char = Math.Abs(CInt(Logo_3_Ortala_Check.Checked)).ToString

        Dim Logo4Kaydır As Char = Math.Abs(CInt(Logo_4_Kaydır_Check.Checked)).ToString
        Dim Logo4Ortala As Char = Math.Abs(CInt(Logo_4_Ortala_Check.Checked)).ToString

        Dim TarihSaatGoster As Char = Math.Abs(CInt(Tarih_Saat_Goster_Check.Checked)).ToString
        Dim BuPanelSaatleriAyarlasın As Char = Math.Abs(CInt(Bu_Panel_Saatleri_Esitlesin_Check.Checked)).ToString
        Dim LogoGosterimSuresi As String = Logo_Gosterim_Suresi_Text.Value.ToString.PadLeft(2, "0")
        Dim SaatGosterimSuresi As String = Saat_Gosterim_Suresi_Text.Value.ToString.PadLeft(2, "0")

        Dim IpAdres As String = IPSplit(0).PadLeft(3, "0") & IPSplit(1).PadLeft(3, "0") & IPSplit(2).PadLeft(3, "0") & IPSplit(3).PadLeft(3, "0") ' İp Adres
        Dim AltAgMaskesi As String = MaskeSplit(0).PadLeft(3, "0") & MaskeSplit(1).PadLeft(3, "0") & MaskeSplit(2).PadLeft(3, "0") & MaskeSplit(3).PadLeft(3, "0") ' Sub Mask
        Dim AltAgGecidi As String = GecitSplit(0).PadLeft(3, "0") & GecitSplit(1).PadLeft(3, "0") & GecitSplit(2).PadLeft(3, "0") & GecitSplit(3).PadLeft(3, "0") ' Gateway
        Dim DnsAdres As String = DnsSunucuSplit(0).PadLeft(3, "0") & DnsSunucuSplit(1).PadLeft(3, "0") & DnsSunucuSplit(2).PadLeft(3, "0") & DnsSunucuSplit(3).PadLeft(3, "0") ' Dns

        Dim RemoteipAdres As String = "192168001255"
        Dim RemoteipPort As String = "01520"
        Dim HaberlesmePortu As String = Port_Text.Text.PadLeft(5, "0")
        Dim intx As Integer
        If Integer.TryParse(HaberlesmePortu, intx) = False OrElse HaberlesmePortu.Length > 5 Then
            MessageBox.Show("Panel Haberleşme Portu Geçersiz !")
            Exit Sub
        End If


        Dim Role1Suresi As String = "01"
        Dim Role2Suresi As String = "01"

        Dim inputlarAktif As Char = Math.Abs(CInt(input_Giris_Aktif.Checked)).ToString
        Dim input1Font As String = input1_Font_Combobox.SelectedIndex
        Dim input2Font As String = input2_Font_Combobox.SelectedIndex

        Dim input1EkranlamaSuresi As String = input1_Ekranlama_Suresi.Value.ToString.PadLeft(3, "0")
        Dim input2EkranlamaSuresi As String = input2_Ekranlama_Suresi.Value.ToString.PadLeft(3, "0")

        Dim input1_1_Yazısı As String = input1_1_Yazısı_Text.Text.PadRight(10, " ")
        Dim input1_2_Yazısı As String = input1_2_Yazısı_Text.Text.PadRight(10, " ")
        Dim input1_3_Yazısı As String = input1_3_Yazısı_Text.Text.PadRight(10, " ")
        Dim input1_4_Yazısı As String = input1_4_Yazısı_Text.Text.PadRight(10, " ")


        Dim input2_1_Yazısı As String = input2_1_Yazısı_Text.Text.PadRight(10, " ")
        Dim input2_2_Yazısı As String = input2_2_Yazısı_Text.Text.PadRight(10, " ")
        Dim input2_3_Yazısı As String = input2_3_Yazısı_Text.Text.PadRight(10, " ")
        Dim input2_4_Yazısı As String = input2_4_Yazısı_Text.Text.PadRight(10, " ")

        If input_Giris_Aktif.Checked = False Then

            input1Font = "0"
            input2Font = "0"
            input1EkranlamaSuresi = "000"
            input2EkranlamaSuresi = "000"

            input1_1_Yazısı = "0000000000"
            input1_2_Yazısı = "0000000000"
            input1_3_Yazısı = "0000000000"
            input1_4_Yazısı = "0000000000"


            input2_1_Yazısı = "0000000000"
            input2_2_Yazısı = "0000000000"
            input2_3_Yazısı = "0000000000"
            input2_4_Yazısı = "0000000000"
        End If

        Dim Revize As String = "000000000000000000000000000**"

        Dim GidecekKomut As String = AyarFormatı & PanelMacID & PanelID & Olcu1 & OlcuSperator & Olcu2 & CihazAdı & LogoFontu & AnlıkYazıFontu & AnlıkYazıSıgmazsaKaydır & AnlıkYazıyıOrtala & EkranParlaklık
        GidecekKomut = GidecekKomut & Logo1Yazısı & Logo2Yazısı & Logo3Yazısı & Logo4Yazısı & Logo1Kaydır & Logo1Ortala & Logo2Kaydır & Logo2Ortala & Logo3Kaydır & Logo3Ortala & Logo4Kaydır & Logo4Ortala & TarihSaatGoster & BuPanelSaatleriAyarlasın & LogoGosterimSuresi & SaatGosterimSuresi & IpAdres & AltAgMaskesi & AltAgGecidi & DnsAdres & RemoteipAdres & RemoteipPort & HaberlesmePortu & Role1Suresi & Role2Suresi & inputlarAktif & input1Font & input2Font & input1EkranlamaSuresi & input2EkranlamaSuresi & input1_1_Yazısı & input1_2_Yazısı & input1_3_Yazısı & input1_4_Yazısı & input2_1_Yazısı & input2_2_Yazısı & input2_3_Yazısı & input2_4_Yazısı & Revize





        My.Computer.Clipboard.SetText(GidecekKomut)

        GelenDatalar.Items.Clear()


        Dim UdpBroadCast As New UdpClient
        UdpBroadCast.EnableBroadcast = True
        Dim Exencoding As Encoding = Encoding.GetEncoding("ISO-8859-9") ' Türkçe karakter problemi çözümü
        Dim Data() As Byte = Exencoding.GetBytes(GidecekKomut)
        UdpBroadCast.Connect("255.255.255.255", 2016) ' Dinleme Portuna Yazıyorumkii Zaten Hep Dinlemede Olucaz..
        UdpBroadCast.Send(Data, Data.Length)
        UdpBroadCast.Close()




        System.Threading.Thread.Sleep(1000)




        If GelenDatalar.Items.Count = 0 Then
            MsgBox("Panelden Beklenen Sürede Cevap Gelmedi !", 48, "Uyarı !")
        Else
            Dim GelenCevap As String = GelenDatalar.Items.Item(0).ToString
            If GelenCevap.Length = 24 Then

                If GelenCevap.Substring(1, 3) = "DTO" Then
                    MsgBox("Ayar Gönderme Başarılı !", 64, "Bilgi")
                ElseIf GelenCevap.Substring(1, 3) = "DTE" Then
                    MsgBox("Gönderilen Data Formatında Hata Var !", 16, "Hata")

                End If
            End If




        End If




    End Sub


    Private Sub Saati_Guncelle_Buton_Click(sender As Object, e As EventArgs) Handles Saati_Guncelle_Buton.Click


        Dim Komut As String = "$RTW"
        Dim Zaman As DateTime = Tarih_Text.Text & " " & Saat_Text.Text
        If Pc_Saat_Esitle_Check.Checked = True Then
            Zaman = Now
        End If
        Dim ZamanStr As String = Zaman.ToString("dd.MM.yyyy hh:mm:ss")

        MsgBox(ZamanStr)



    End Sub

    Private Sub Ekran_Parlaklık_Trackbar_Scroll(sender As Object, e As EventArgs) Handles Ekran_Parlaklık_Trackbar.Scroll

    End Sub

    Private Sub Ekran_Parlaklık_Trackbar_ValueChanged(sender As Object, e As EventArgs) Handles Ekran_Parlaklık_Trackbar.ValueChanged
        Parlaklık_Value_Label.Text = "%" & Ekran_Parlaklık_Trackbar.Value
    End Sub

    Private Sub Anasayfa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        Udp = New UdpClient(VeriGelenPort)
        Udp_Dinle()

    End Sub
    Function ip_Parse(ByVal ipAdres As String) As String
        Return CInt(ipAdres.Substring(0, 3)) & "." & CInt(ipAdres.Substring(3, 3)) & "." & CInt(ipAdres.Substring(6, 3)) & "." & CInt(ipAdres.Substring(9, 3))
    End Function


    Private Sub Panel_Ara_Buton_Click(sender As Object, e As EventArgs) Handles Panel_Ara_Buton.Click

        Panel_Sayısı_Label.Text = "0"
        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        System.Threading.Thread.Sleep(50)
        GelenDatalar.Items.Clear()
        For i = 1 To 2

            Dim UdpBroadCast As New UdpClient
            UdpBroadCast.EnableBroadcast = True
            Dim Exencoding As Encoding = Encoding.GetEncoding("ISO-8859-9") ' Türkçe karakter problemi çözümü
            Dim Data() As Byte = Exencoding.GetBytes("$LPA1989**")
            UdpBroadCast.Connect("255.255.255.255", 2016) ' Dinleme Portuna Yazıyorumkii Zaten Hep Dinlemede Olucaz..
            UdpBroadCast.Send(Data, Data.Length)
            UdpBroadCast.Close()
            System.Threading.Thread.Sleep(100)
        Next

        System.Threading.Thread.Sleep(1500)
        Me.Cursor = Cursors.Default

        Dim Datatablo As New DataTable
        Datatablo.Columns.Add("Panel Adı")
        Datatablo.Columns.Add("Versiyon")
        Datatablo.Columns.Add("Mac Adres")
        Datatablo.Columns.Add("Ip")
        Datatablo.Columns.Add("Ağ Maskesi")
        Datatablo.Columns.Add("Alt Ağ Geçidi")
        Datatablo.Columns.Add("Dns")
        Datatablo.Columns.Add("Port")
        Datatablo.Columns.Add("Panel ID")
        Datatablo.Columns.Add("Logo 1")
        Datatablo.Columns.Add("Logo 2")
        Datatablo.Columns.Add("Logo 3")
        Datatablo.Columns.Add("Logo 4")
        Datatablo.Columns.Add("Logo Fontu")
        Datatablo.Columns.Add("Anlık Yazı Fontu")
        Datatablo.Columns.Add("Anlık Yazı Kaydır")
        Datatablo.Columns.Add("Anlık Yazı Ortala")

        Datatablo.Columns.Add("Logo 1 Kaydır")
        Datatablo.Columns.Add("Logo 1 Ortala")
        Datatablo.Columns.Add("Logo 2 Kaydır")
        Datatablo.Columns.Add("Logo 2 Ortala")
        Datatablo.Columns.Add("Logo 3 Kaydır")
        Datatablo.Columns.Add("Logo 3 Ortala")
        Datatablo.Columns.Add("Logo 4 Kaydır")
        Datatablo.Columns.Add("Logo 4 Ortala")
        Datatablo.Columns.Add("inputlarAktif")



        If GelenDatalar.Items.Count > 0 Then
            For i = 0 To GelenDatalar.Items.Count - 1
                Dim GelenData As String = GelenDatalar.Items(i)
                Dim PanelPort As String = GelenData.Substring(284, 5).Replace(" ", "")

                Dim Satır As DataRow = Datatablo.NewRow()
                Satır("Panel Adı") = GelenData.Substring(42, 16).TrimEnd
                Satır("Versiyon") = GelenData.Substring(4, 3)
                Satır("Mac Adres") = GelenData.Substring(15, 17)
                Satır("Ip") = ip_Parse(GelenData.Substring(219, 12))
                Satır("Ağ Maskesi") = ip_Parse(GelenData.Substring(231, 12))
                Satır("Alt Ağ Geçidi") = ip_Parse(GelenData.Substring(243, 12))
                Satır("Dns") = ip_Parse(GelenData.Substring(255, 12))
                Satır("Port") = PanelPort
                Satır("Panel ID") = CInt(GelenData.Substring(32, 3))
                Satır("Logo 1") = GelenData.Substring(65, 35).TrimEnd
                Satır("Logo 2") = GelenData.Substring(100, 35).TrimEnd
                Satır("Logo 3") = GelenData.Substring(135, 35).TrimEnd
                Satır("Logo 4") = GelenData.Substring(170, 35).TrimEnd

                Satır("Logo 1 Kaydır") = GelenData.Substring(205, 1)
                Satır("Logo 1 Ortala") = GelenData.Substring(206, 1)
                Satır("Logo 2 Kaydır") = GelenData.Substring(207, 1)
                Satır("Logo 2 Ortala") = GelenData.Substring(208, 1)
                Satır("Logo 3 Kaydır") = GelenData.Substring(209, 1)
                Satır("Logo 3 Ortala") = GelenData.Substring(210, 1)
                Satır("Logo 4 Kaydır") = GelenData.Substring(211, 1)
                Satır("Logo 4 Ortala") = GelenData.Substring(212, 1)


                Satır("Logo Fontu") = GelenData.Substring(58, 1).TrimEnd
                Satır("Anlık Yazı Fontu") = GelenData.Substring(59, 1).TrimEnd
                Satır("Anlık Yazı Kaydır") = GelenData.Substring(60, 1).TrimEnd
                Satır("Anlık Yazı Ortala") = GelenData.Substring(61, 1).TrimEnd
                Satır("inputlarAktif") = GelenData.Substring(293, 1).TrimEnd
                Datatablo.Rows.Add(Satır)

              


            Next



            Gelen_Datalar_Gridview.DataSource = Datatablo
            Panel_Sayısı_Label.Text = Datatablo.Rows.Count

        End If




    End Sub




    Private Sub Anlık_Yazı_Gonder_Buton_Click(sender As Object, e As EventArgs) Handles Anlık_Yazı_Gonder_Buton.Click
        If CInt(Ekranlama_Suresi_Text.Value) > 99 Then
            Ekranlama_Suresi_Text.Value = 99
        End If

        Dim Ekranlamatxt As String = CInt(Ekranlama_Suresi_Text.Value).ToString("D2")
        Dim PanelIDText As String = Panel_ID_No_Text.Text.PadLeft(3, "0")



        Dim GidenData As String = "W" & PanelIDText & "&" & Satir_1_Text.Text & "&" & Satir_2_Text.Text & "&" & Satir_3_Text.Text & "&" & Satir_4_Text.Text & "&" & Ekranlamatxt
        My.Computer.Clipboard.SetText(GidenData)
        If Udp_Radio.Checked = True Then
            Dim UdpBroadCast As New UdpClient
            UdpBroadCast.EnableBroadcast = True
            Dim Exencoding As Encoding = Encoding.GetEncoding("ISO-8859-9") ' Türkçe karakter problemi çözümü
            Dim Data() As Byte = Exencoding.GetBytes(GidenData)
            UdpBroadCast.Connect("255.255.255.255", 55555) ' Dinleme Portuna Yazıyorumkii Zaten Hep Dinlemede Olucaz..
            UdpBroadCast.Send(Data, Data.Length)
            UdpBroadCast.Close()

        ElseIf Tcp_Radio.Checked = True Then

            Dim Tcp As New TcpClient
            If Tcp.ConnectAsync(Anlık_Ip_Adres_Text.Text, CInt(Anlık_Port_Text.Text)).Wait(500) = True Then
                Dim ServerStream As NetworkStream = Tcp.GetStream()
                Dim outStream As Byte() = Encoding.GetEncoding("ISO-8859-9").GetBytes(GidenData)
                ServerStream.Write(outStream, 0, outStream.Length)
                Threading.Thread.Sleep(50)
                ServerStream.Flush()
                ServerStream.Close()
            Else
                MsgBox(" Bağlantı Sağlanamadı !")
            End If

            Tcp.Close()
        End If






    End Sub

    Private Sub Gelen_Datalar_Gridview_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Gelen_Datalar_Gridview.CellClick
        Try


            Panel_Adı_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Panel Adı").Value.ToString()
            Panel_Ayar_ID_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Panel ID").Value.ToString()
            Panel_Mac_ID_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Mac Adres").Value.ToString()
            Ip_Adres_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Ip").Value.ToString()
            Alt_Ag_Maskesi.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Ağ Maskesi").Value.ToString()
            Alt_Ag_Gecidi_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Alt Ağ Geçidi").Value.ToString()
            Dns_Sunucu_Adres_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Dns").Value.ToString()
            Port_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Port").Value.ToString()
            Panel_ID_No_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Panel ID").Value.ToString()
            Logo_1_Yazısı_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 1").Value.ToString()
            Logo_2_Yazısı_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 2").Value.ToString()
            Logo_3_Yazısı_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 3").Value.ToString()
            Logo_4_Yazısı_Text.Text = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 4").Value.ToString()
            Logo_Fontu_Text.SelectedIndex = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo Fontu").Value.ToString()
            Anlık_Yazı_Fontu_Text.SelectedIndex = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Anlık Yazı Fontu").Value.ToString()
            Anlık_Yazı_Panele_Sıgdırsa_Kaydır_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Anlık Yazı Kaydır").Value.ToString()
            Anlık_Yazıyı_Ortaya_Yaz_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Anlık Yazı Ortala").Value.ToString()

            Logo_1_Kaydır_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 1 Kaydır").Value
            Logo_1_Ortala_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 1 Ortala").Value
            Logo_2_Kaydır_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 2 Kaydır").Value
            Logo_2_Ortala_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 2 Ortala").Value
            Logo_3_Kaydır_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 3 Kaydır").Value
            Logo_3_Ortala_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 3 Ortala").Value
            Logo_4_Kaydır_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 4 Kaydır").Value
            Logo_4_Ortala_Check.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("Logo 4 Ortala").Value

            input_Giris_Aktif.Checked = Gelen_Datalar_Gridview.Rows(e.RowIndex).Cells("inputlarAktif").Value




            Anlık_Ip_Adres_Text.Text = Ip_Adres_Text.Text
            Anlık_Port_Text.Text = Port_Text.Text


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try












    End Sub

   
    Private Sub Gelen_Datalar_Gridview_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Gelen_Datalar_Gridview.CellContentClick

    End Sub

    Private Sub Port_Text_TextChanged(sender As Object, e As EventArgs) Handles Port_Text.TextChanged

    End Sub
End Class
