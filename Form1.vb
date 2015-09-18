Imports Microsoft.Win32
Imports System.IO
Imports System.Reflection
'Gestor de Redes Virtuais para Windows 8 e Windows 10'
'Desenvolvido por Emanuel Alves'
'Código fonte disponível em https://github.com/emannxx/Gestor-de-Redes-Virtuais'

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Computer.Registry.CurrentUser.CreateSubKey("GestorRedesVirtuais")
        If (getValue("ssidPadrao") Is Nothing And getValue("pswPadrao") Is Nothing) Then
            setValue("ssidPadrao", "RedeAdHocVirtual")
            setValue("pswPadrao", "Masterlock64")
            setValue("autoNetwork", "no")
            setValue("defaultStartup", "no")
            setValue("autoUpdate", "yes")
            setValue("changelogShown", "yes")
        End If

        If (getValue("autoUpdate") = "") Then
            setValue("autoUpdate", "yes")
            setValue("autoNetwork", "no")
        End If

        If getValue("defaultStartup") = "yes" Then
            ssid.Text = getValue("ssidPadrao")
            password.Text = getValue("pswPadrao")
        End If

        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Load
        If getValue("autoNetwork") = "yes" Then
            Button1_Click(sender, e)
        End If
        currentVersionLabel.Text = GlobalVariables.currentVersion
        If getValue("autoUpdate") = "yes" Then
            Dim latestCheckDate As String = getValue("lastUpdateCheck")
            If latestCheckDate = "" Then
                setValue("lastUpdateCheck", GlobalVariables.fakedate)
                latestCheckDate = GlobalVariables.fakedate
            End If
            Dim latestCheck As Date = Date.ParseExact(latestCheckDate, "dd-MM-yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            If DateDiff(DateInterval.Day, Now, latestCheck) < -5 Then
                Console.WriteLine("Running auto-update")
                checkforUpdates()
            Else
                Console.WriteLine("auto updater ran less than 5 days ago, skipping")
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim _imageStream As Stream
        Dim _assembly As [Assembly]
        _assembly = [Assembly].GetExecutingAssembly()
        _imageStream = _assembly.GetManifestResourceStream("gestor_de_redes_virtuais.wifi.ico")
        Try
            _assembly = [Assembly].GetExecutingAssembly()
            _imageStream = _assembly.GetManifestResourceStream("gestor_de_redes_virtuais.wifi.ico")
        Catch ex As Exception
            MessageBox.Show("Resource wasn't found!", "Error")
        End Try
        NotifyIcon3.Icon = New Icon(_imageStream)
        NotifyIcon3.Text = "Gestor de Redes Virtuais"
        NotifyIcon3.Visible = False

        If getValue("changelogShown") = "no" Or getValue("changelogShown") = "" Then
            changelogworker.RunWorkerAsync()
            setValue("changelogShown", "yes")
        End If
    End Sub

    Private Sub changelogworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles changelogworker.DoWork
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://emanuel-alves.com/GRV/changelog.txt")
        Dim response As System.Net.HttpWebResponse
        Dim sr As System.IO.StreamReader
        Dim changelog As String
        Try
            response = request.GetResponse()
            sr = New System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8, True)
            changelog = sr.ReadToEnd()
        Catch ex As Exception
            Console.WriteLine("couldnt get the changelog {0}", ex.ToString)
            changelog = ""
        End Try
        MsgBox("Novidades desta versão - " & Application.ProductVersion & vbNewLine & changelog, MessageBoxIcon.Information, "Changelog")
    End Sub




    Public Class GlobalVariables
        Public Shared networkIsUp As Int16 = 0
        Public Shared currentVersion As String = Application.ProductVersion.ToString
        Public Shared fakedate As String = "13-09-2014"

    End Class



    Function setValue(ByVal regname As String, ByVal regval As String) As Integer
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", regname, regval)
        Return vbNull
    End Function

    Function getValue(ByVal regname As String) As String
        Dim localvar As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", regname, Nothing)
        Return localvar
    End Function

    Function terminate()
        If GlobalVariables.networkIsUp = 1 Then
            If MsgBox("A rede virtual está inicilizada, se sair a mesma será desligada." & vbNewLine & "Deseja sair?", MsgBoxStyle.YesNo, "Atenção") = MsgBoxResult.Yes Then
                applyCommand("netsh wlan stop hostednetwork")
                Application.Exit()
            Else
                Return ""
            End If
        Else
            Application.Exit()
        End If
        Return ""
    End Function

    Function applyCommand(ByVal command As String) As Integer
        Dim commandDispatcherSettings As New ProcessStartInfo()
        Dim commandDispatcherProcess As New Process()
        commandDispatcherSettings.FileName = "cmd"
        commandDispatcherSettings.Verb = "runas"
        commandDispatcherSettings.WindowStyle = ProcessWindowStyle.Hidden
        commandDispatcherSettings.Arguments = "cmd /C " + command
        commandDispatcherProcess.StartInfo = commandDispatcherSettings
        commandDispatcherProcess.Start()
        commandDispatcherProcess.WaitForExit()
        Dim errorCode As Integer = commandDispatcherProcess.ExitCode
        Console.WriteLine("codigo de erro é {0}", errorCode)
        applyCommand = errorCode
    End Function


    Function checkforUpdates() As Integer
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://emanuel-alves.com/GRV/latestversion.txt")
        Dim response As System.Net.HttpWebResponse
        Dim sr As System.IO.StreamReader
        Dim newestversion As String
        Try
            response = request.GetResponse()
            sr = New System.IO.StreamReader(response.GetResponseStream())
            newestversion = sr.ReadToEnd()
        Catch ex As Exception
            Console.WriteLine("internet update crashed with {0}", ex.ToString)
            MsgBox("Não foi possível procurar por uma nova versão." & vbNewLine & "Certifique-se que está ligado à internet.", MessageBoxIcon.Error, "Ocorreu um erro")
            newestversion = Application.ProductVersion
        End Try
        Console.WriteLine("current version is {0} ", GlobalVariables.currentVersion)
        Console.WriteLine("version available online is {0} ", newestversion)
        If (newestversion <> GlobalVariables.currentVersion) Then
            If MsgBox("Está disponível uma nova versão do Gestor de Redes Virtuais" & vbNewLine & "Deseja transferir?", MsgBoxStyle.YesNo, "Nova versão disponível!") = MsgBoxResult.Yes Then
                Process.Start("http://emanuel-alves.com/GRV/download.html")
                setValue("lastUpdateCheck", GlobalVariables.fakedate)
                setValue("changelogShown", "no")
                Return 0
            Else
                Me.Height += 20
                updateWarningLabel.Visible = True
                setValue("lastUpdateCheck", GlobalVariables.fakedate)
                Return 0
            End If

        Else
            Dim todaysdate As String = String.Format("{0:dd-MM-yyyy}", DateTime.Now)
            Console.WriteLine("todaysdate is {0}", todaysdate.ToString)
            setValue("lastUpdateCheck", todaysdate)
            Return 1
        End If
    End Function



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim ssidVar As String = ssid.Text
        Dim paswVar As String = password.Text
        Dim networkSettingValsCMD As String = "netsh wlan set hostednetwork mode=allow ssid=" + ssidVar + " key=" + paswVar
        Dim settingErrorCheck As Integer = applyCommand(networkSettingValsCMD)
        If (settingErrorCheck <> 1) Then
            Dim startupErrorCheck As Integer = applyCommand("netsh wlan start hostednetwork")
            If (startupErrorCheck = 1) Then
                Console.WriteLine("that didnt work")
                MsgBox("Não foi possível inicializar a rede." & vbNewLine & "Certifique-se que a placa WiFi está activada e que não está ligado a nenhuma rede wireless. Verifique também se o seu sistema tem suporte para redes virtuais.", MessageBoxIcon.Error, "Ocorreu um erro")
            Else
                MsgBox("Rede virtual inicializada com sucesso!", MessageBoxIcon.Information)
                Dim green As Color
                green = Color.Lime
                statebox.BackColor = green
                statelabel.Text = "Rede Iniciada"
                GlobalVariables.networkIsUp = 1
            End If
        Else
            Console.WriteLine("that didnt work either")
            MsgBox("Os dados introduzidos não são válidos." & vbNewLine & "As passwords têm que ter mais de 8 caracteres, corrija a configuração e tente de novo.", MessageBoxIcon.Error, "Ocorreu um erro")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        applyCommand("netsh wlan stop hostednetwork")
        MsgBox("Rede virtual desligada com sucesso!", MessageBoxIcon.Information)
        Dim ctrl As Color
        ctrl = SystemColors.Control
        statebox.BackColor = ctrl
        statelabel.Text = "Rede Inativa"
        GlobalVariables.networkIsUp = 0
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ssid.Text = getValue("ssidPadrao")
        password.Text = getValue("pswPadrao")
    End Sub

    Private Sub SobreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SobreToolStripMenuItem.Click
        Form3.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        applyCommand("ncpa.cpl")
    End Sub

    Private Sub LimparTudoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimparTudoToolStripMenuItem.Click
        ssid.Text = ""
        password.Text = ""
    End Sub

    Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SairToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://emanuel-alves.com")
    End Sub

    Private Sub AlterarConfiguraçãoPadrãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlterarConfiguraçãoPadrãoToolStripMenuItem.Click
        defaultconfig.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        terminate()
    End Sub

    Private Sub ConfigurarPlacaDeRedeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigurarPlacaDeRedeToolStripMenuItem.Click
        MsgBox("Será aberta uma página no seu browser. Necessita de uma ligação à internet. ", MessageBoxIcon.Information)
        Process.Start("http://emanuel-alves.com/GRV/config1.html")
    End Sub

    Private Sub VerificarCompatibilidadeDoSistemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerificarCompatibilidadeDoSistemaToolStripMenuItem.Click
        MsgBox("Será aberta uma página no seu browser. Necessita de uma ligação à internet. ", MessageBoxIcon.Information)
        Process.Start("http://emanuel-alves.com/GRV/config2.html")
    End Sub

    Private Sub ContactoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ContactoToolStripMenuItem1.Click
        MsgBox("Será aberta uma página no seu browser. Necessita de uma ligação à internet. ", MessageBoxIcon.Information)
        Process.Start("http://emanuel-alves.com/contato.html")
    End Sub

    Private Sub ActualizaçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizaçãoToolStripMenuItem.Click
        If (checkforUpdates() = 1) Then
            MsgBox("Está a utilizar a versão mais recente do Gestor de Redes Virtuais.", MessageBoxIcon.Information, "Nenhuma Atualização Disponível")
        End If
    End Sub

    Private Sub NotifyIcon3_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon3.MouseDoubleClick
        Me.Visible = True
    End Sub

    Private Sub NotifyIcon3_rightClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon3.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim menu As New ContextMenu
            Dim menuItem1 As New MenuItem("Abrir GRV")
            Dim menuItem2 As New MenuItem("Sair")
            menu.MenuItems.Add(menuItem1)
            menu.MenuItems.Add(menuItem2)
            AddHandler menuItem1.Click, AddressOf Me.menuItem1_Click
            AddHandler menuItem2.Click, AddressOf Me.menuItem2_Click
            NotifyIcon3.ContextMenu = menu
        End If

    End Sub

    Private Sub menuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Visible = True
        NotifyIcon3.Visible = False
    End Sub
    Private Sub menuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        NotifyIcon3.Visible = False
        terminate()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        NotifyIcon3.Visible = True
        Me.Visible = False
        NotifyIcon3.ShowBalloonTip(10)
    End Sub

    Private Sub NovidadesDestaVersãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NovidadesDestaVersãoToolStripMenuItem.Click
        changelogworker.RunWorkerAsync()
    End Sub

End Class
