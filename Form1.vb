Imports Microsoft.Win32
Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Net
'Gestor de Redes Virtuais para Windows 8 e Windows 10'
'Desenvolvido por Emanuel Alves'
'Código fonte disponível em https://github.com/emannxx/Gestor-de-Redes-Virtuais'

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GRVHelperFunctions.GRVInit(False)
        AddHandler Microsoft.Win32.SystemEvents.PowerModeChanged, AddressOf SystemEvents_PowerModeChanged
        initBgWorker.RunWorkerAsync()

    End Sub

    Public Sub SystemEvents_PowerModeChanged(ByVal sender As Object, ByVal e As PowerModeChangedEventArgs)

        Select Case e.Mode
            Case PowerModes.Resume
                If GlobalVariables.networkIsUp = 1 Then
                    GRVHelperFunctions.applyCommand("netsh wlan stop hostednetwork")
                    System.Threading.Thread.CurrentThread.Sleep(10000)
                    GRVHelperFunctions.applyCommand("netsh wlan start hostednetwork")
                End If
        End Select

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
        Dim autoNetwork = GRVHelperFunctions.getValue("autoNetwork")
        If autoNetwork = "yes" Then
            Button1_Click(sender, e)
        ElseIf autoNetwork IsNot "no" Or autoNetwork Is Nothing Then
            GRVHelperFunctions.GRVInit(True)
        End If
        currentVersionLabel.Text = "v" + GlobalVariables.currentVersion
        Dim yourToolTip = New ToolTip()
        yourToolTip.IsBalloon = True
        yourToolTip.ShowAlways = True
        yourToolTip.SetToolTip(currentVersionLabel, "Gestor de Redes Virtuais - Versão " + GlobalVariables.currentVersion + "." + vbNewLine + "Clique para procurar atualizações.")
    End Sub

    'Minimize on Form Minimize
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        Dim min As Boolean = False

        'if minimized button was pressed
        If Me.WindowState = FormWindowState.Minimized = True Then
            min = True

            'undo minimize
            Me.WindowState = FormWindowState.Normal
        End If

        If min Then
            minimize()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles initBgWorker.DoWork
        Dim _imageStream As Stream
        Dim _assembly As [Assembly]
        _assembly = [Assembly].GetExecutingAssembly()
        _imageStream = _assembly.GetManifestResourceStream("gestor_de_redes_virtuais.wifi.ico")
        Try
            _assembly = [Assembly].GetExecutingAssembly()
            _imageStream = _assembly.GetManifestResourceStream("gestor_de_redes_virtuais.wifi.ico")
        Catch ex As Exception
            Console.WriteLine("Resource wasn't found!", "Error")
        End Try
        NotifyIcon3.Icon = New Icon(_imageStream)
        NotifyIcon3.Text = "Gestor de Redes Virtuais"
        NotifyIcon3.Visible = False
        Dim menu As New ContextMenu
        Dim menuItem1 As New MenuItem("Abrir GRV")
        Dim menuItem2 As New MenuItem("Sobre o GRV")
        Dim menuItem3 As New MenuItem("Reinicar Rede Virtual")
        Dim menuItem4 As New MenuItem("Sair")
        menu.MenuItems.Add(menuItem1)
        menu.MenuItems.Add(menuItem2)
        menu.MenuItems.Add(menuItem3)
        menu.MenuItems.Add(menuItem4)
        AddHandler menuItem1.Click, AddressOf Me.menuItem1_Click
        AddHandler menuItem4.Click, AddressOf Me.menuItem2_Click
        AddHandler menuItem3.Click, AddressOf Me.menuItem3_Click
        AddHandler menuItem2.Click, AddressOf Me.menuItem4_Click
        NotifyIcon3.ContextMenu = menu

        If GRVHelperFunctions.getValue("autoUpdate") = "yes" Then
            Dim latestCheckDate As String = GRVHelperFunctions.getValue("lastUpdateCheck")
            If latestCheckDate = "" Then
                GRVHelperFunctions.setValue("lastUpdateCheck", GlobalVariables.fakedate)
                latestCheckDate = GlobalVariables.fakedate
            End If
            Dim latestCheck As Date = Date.ParseExact(latestCheckDate, "dd-MM-yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            If DateDiff(DateInterval.Day, Now, latestCheck) < -5 Then
                Console.WriteLine("Running auto-update")
                Dim path As String = Directory.GetCurrentDirectory()
                Dim args = New String() {"0", path}
                updaterWorker.RunWorkerAsync(args)
            Else
                Console.WriteLine("auto updater ran less than 5 days ago, skipping")
            End If
        End If


        If Debugger.IsAttached Then
        Else
            Dim currDate As String = String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now)
            Dim osVer As Version = Environment.OSVersion.Version
            Dim pcName = My.Computer.Name
            Dim url As String = "http://emanuel-alves.com/GRV/grvtelemetry.php?variable=>>%20" + currDate + "%20System:%20" + pcName + "%20is%20running%20GRV%20version%20" + GlobalVariables.currentVersion + " %0d%0a"
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
            Dim response As System.Net.HttpWebResponse
            Try
                Console.WriteLine("sending telemetry")
                response = request.GetResponse()
            Catch ex As Exception
                Console.WriteLine("telemetry broadcast failed")
            End Try
        End If
    End Sub

    Private Sub changelogworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles changelogworker.DoWork
        Dim args As Object() = DirectCast(e.Argument, Object())
        Dim show As Int32 = CInt(args(0))
        Console.WriteLine("Show is {0}", show)
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://emanuel-alves.com/GRV/changelog-p2.txt")
        Dim response As System.Net.HttpWebResponse
        Dim sr As System.IO.StreamReader
        Dim changelog As String
        Try
            response = request.GetResponse()
            sr = New System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8, True)
            changelog = sr.ReadToEnd()
        Catch ex As Exception
            Console.WriteLine("couldnt get the changelog {0}", ex.ToString)
            changelog = "Ocorreu um erro ao obter o changelog."
        End Try
        If show = 1 Then
            MsgBox("Novidades desta versão - " & Application.ProductVersion & vbNewLine & changelog, MessageBoxIcon.Information, "Changelog")
        Else
            GlobalVariables.changelog = changelog
        End If
    End Sub

    Function minimize() As Integer
        NotifyIcon3.ContextMenu.MenuItems.Item(index:=2).Enabled = GlobalVariables.item3State
        NotifyIcon3.Visible = True
        Me.Visible = False
        If GlobalVariables.notificationSeen = False Then
            NotifyIcon3.ShowBalloonTip(5)
            GlobalVariables.notificationSeen = True
        End If
        Return vbNull
    End Function

    Public Delegate Sub raiseWarn()
    Dim updateCancelled As raiseWarn = AddressOf raiseWarning

    Public Delegate Sub replacetheapp(newPath As String, swtch As Int16)
    Dim appReplace As replacetheapp = AddressOf GRVHelperFunctions.appReplaceD

    Function raiseWarning()
        Me.Height += 20
        updateWarningLabel.Visible = True
        Return ""
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ssid.Text = "" Or password.Text = "" Then
            MsgBox("Nenhum dos campos 'Nome da Rede' ou 'Password' podem estar vazios. Introduza a configuração pretendida e tente novamente.", MessageBoxIcon.Warning)
        Else
            If Len(password.Text) < 8 Then
                MsgBox("A password tem de ter mais de 8 caracteres. Corrija e tente novamente.", MessageBoxIcon.Warning)
            Else
                Dim ssidVar As String = ssid.Text
                Dim paswVar As String = password.Text
                Dim networkSettingValsCMD As String = "netsh wlan set hostednetwork mode=allow ssid=" + ssidVar + " key=" + paswVar
                Dim settingErrorCheck As Integer = GRVHelperFunctions.applyCommand(networkSettingValsCMD)
                If (settingErrorCheck <> 1) Then
                    Dim startupErrorCheck As Integer = GRVHelperFunctions.applyCommand("netsh wlan start hostednetwork")
                    If (startupErrorCheck = 1) Then
                        Console.WriteLine("that didnt work")
                        MsgBox("Não foi possível inicializar a rede." & vbNewLine & "Certifique-se que a placa WiFi está activada e que o seu sistema tem suporte para redes virtuais.", MessageBoxIcon.Error, "Ocorreu um erro")
                    Else
                        If GlobalVariables.showPopup = 1 Then
                            MsgBox("Rede virtual inicializada com sucesso!", MessageBoxIcon.Information)
                        End If
                        updateNetworkState(1)

                    End If
                Else
                    Console.WriteLine("that didnt work either")
                    MsgBox("Ocorreu um erro não especificado." + vbNewLine + "Por favor contacte o suporte em Ajuda -> Contacto.", MessageBoxIcon.Error, "Erro")
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GRVHelperFunctions.applyCommand("netsh wlan stop hostednetwork")
        If GlobalVariables.showPopup = 1 Then
            MsgBox("Rede virtual desligada com sucesso!", MessageBoxIcon.Information)
        End If
        updateNetworkState(0)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ssid.Text = GRVHelperFunctions.getValue("ssidPadrao")
        password.Text = GRVHelperFunctions.getValue("pswPadrao")
    End Sub

    Private Sub SobreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SobreToolStripMenuItem.Click
        Form3.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        GRVHelperFunctions.applyCommand("ncpa.cpl")
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
        defaultconfig.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        GRVHelperFunctions.terminate()
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
        Process.Start("http://emannxx.github.io/gestor-redes-virtuais/#suporte-para-quem-precisa")
    End Sub

    Private Sub ActualizaçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizaçãoToolStripMenuItem.Click
        Dim path As String = Directory.GetCurrentDirectory()
        Console.WriteLine("current path is {0}", path)
        Dim args = New String() {"1", path}
        Try
            updaterWorker.RunWorkerAsync(args)
        Catch ex As Exception
            Console.WriteLine("updateworker is already running!")
        End Try
    End Sub

    Private Sub NotifyIcon3_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon3.MouseDoubleClick
        Me.Visible = True
    End Sub

    Private Sub menuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Visible = True
        NotifyIcon3.Visible = False
        Try
            checkState.RunWorkerAsync()
        Catch ex As Exception
            Console.WriteLine("checkstate worker is already running!")
        End Try
    End Sub
    Private Sub menuItem4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Form3.ShowDialog()
    End Sub
    Private Sub menuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Visible = True
        GRVHelperFunctions.terminate()
    End Sub
    Private Sub menuItem3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        GRVHelperFunctions.applyCommand("netsh wlan stop hostednetwork")
        updateNetworkState(0)
        System.Threading.Thread.CurrentThread.Sleep(5000)
        GRVHelperFunctions.applyCommand("netsh wlan start hostednetwork")
        updateNetworkState(1)
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        minimize()
    End Sub

    Private Sub NovidadesDestaVersãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NovidadesDestaVersãoToolStripMenuItem.Click
        Dim args = New String() {"1"}
        Try
            changelogworker.RunWorkerAsync(args)
        Catch ex As Exception
            Console.WriteLine("changelogworker is already running!")
        End Try
    End Sub

    Private Sub updaterWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles updaterWorker.DoWork
        Dim args As Object() = DirectCast(e.Argument, Object())
        Dim alert As String = CInt(args(0))
        Dim path As String = CStr(args(1))
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://emanuel-alves.com/GRV/latestversion-p2.txt")
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
            Dim chgArgs = New String() {"0"}
            changelogworker.RunWorkerAsync(chgArgs)
            System.Threading.Thread.CurrentThread.Sleep(3000)
            Dim i As String = path + "\Gestor De Redes Virtuais_" + newestversion + ".exe"
            Dim success As Int16
            Try
                Dim fileReader As New WebClient()
                Dim fileAddress = "http://emanuel-alves.com/GRV/grv-latest-2.exe"
                fileReader.DownloadFile(fileAddress, i)
                success = 1
            Catch ex As Exception
                Console.WriteLine("Update failed!")
                success = 0
            Finally
                If success = 1 Then
                    If MsgBox("Foi transferida uma nova versão do Gestor de Redes Virtuais." & vbNewLine & vbNewLine & "Novidades da nova versão: " & vbNewLine & GlobalVariables.changelog & vbNewLine & vbNewLine & "Deseja reiniciar a app para atualizar?", MsgBoxStyle.YesNo, "Nova versão " + newestversion + " transferida com sucesso!") = MsgBoxResult.Yes Then
                        Me.Invoke(GRVHelperFunctions.appReplaceD(i, 0))
                    Else
                        Me.Invoke(updateCancelled)
                        GRVHelperFunctions.setValue("lastUpdateCheck", GlobalVariables.fakedate)
                        GlobalVariables.updtQueue = 1
                        GlobalVariables.updtCmd = i
                    End If
                Else
                    If MsgBox("Não foi possível transferir a nova versão automaticamente." & vbNewLine & "Deseja aceder ao site oficial para transferir manualmente?", MsgBoxStyle.YesNo, "Ocorreu um erro") = MsgBoxResult.Yes Then
                        Process.Start("http://emannxx.github.io/gestor-redes-virtuais")
                    End If
                End If
            End Try
        Else
            Dim todaysdate As String = String.Format("{0:dd-MM-yyyy}", DateTime.Now)
            Console.WriteLine("todaysdate is {0}", todaysdate.ToString)
            GRVHelperFunctions.setValue("lastUpdateCheck", todaysdate)
            If alert = "1" Then
                MsgBox("Está a utilizar a versão mais recente do Gestor de Redes Virtuais.", MessageBoxIcon.Information, "Nenhuma Atualização Disponível")
            End If

        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub EstadoAtualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EstadoAtualToolStripMenuItem.Click
        Dim statusProcess As New Process()
        Dim statusSettings As New ProcessStartInfo()
        statusSettings.FileName = "cmd"
        statusSettings.Verb = "runas"
        statusSettings.WindowStyle = ProcessWindowStyle.Hidden
        statusSettings.Arguments = "cmd /C netsh wlan show hostednetwork"
        statusSettings.RedirectStandardOutput = True
        statusSettings.UseShellExecute = False
        statusProcess.StartInfo = statusSettings
        statusProcess.Start()
        Dim sOutput As String
        Dim oStreamReader As System.IO.StreamReader = statusProcess.StandardOutput
        sOutput = oStreamReader.ReadToEnd()
        MsgBox("Em baixo encontra informações sobre o estado atual da rede e o número de dispositivos ligados à mesma." + vbNewLine + sOutput, 0, "Estado atual da rede")
    End Sub

    Private Sub currentVersionLabel_Click(sender As Object, e As EventArgs) Handles currentVersionLabel.Click
        Dim path As String = Directory.GetCurrentDirectory()
        Console.WriteLine("current path is {0}", path)
        Dim args = New String() {"1", path}
        Try
            updaterWorker.RunWorkerAsync(args)
        Catch ex As Exception
            Console.WriteLine("updateworker is already running!")
        End Try
    End Sub

    Public Delegate Sub cgState(status As Int16)
    Dim chgState As cgState = AddressOf updateNetworkState

    Function updateNetworkState(state As Int16)
        If state = 1 Then
            Console.WriteLine("changing state")
            GlobalVariables.item3State = True
            statebox.BackColor = Color.Lime
            statelabel.Text = "Rede Iniciada"
            GlobalVariables.networkIsUp = 1
        Else
            Console.WriteLine("changing state")
            GlobalVariables.item3State = False
            statebox.BackColor = SystemColors.Control
            statelabel.Text = "Rede Inativa"
            GlobalVariables.networkIsUp = 0
        End If
    End Function

    Private Sub checkState_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles checkState.DoWork
        Dim statusProcess As New Process()
        Dim statusSettings As New ProcessStartInfo()
        statusSettings.FileName = "cmd"
        statusSettings.Verb = "runas"
        statusSettings.WindowStyle = ProcessWindowStyle.Hidden
        statusSettings.Arguments = "cmd /C netsh wlan show hostednetwork"
        statusSettings.RedirectStandardOutput = True
        statusSettings.UseShellExecute = False
        statusProcess.StartInfo = statusSettings
        statusProcess.Start()
        Dim sOutput As String
        Dim oStreamReader As System.IO.StreamReader = statusProcess.StandardOutput
        sOutput = oStreamReader.ReadToEnd()

        Dim regex As Regex = New Regex("(Started)")
        Dim match As Match = regex.Match(sOutput)
        If match.Success Then
            Console.WriteLine("found")
            Me.Invoke(updateNetworkState(1))
        Else
            Console.WriteLine("not found")
            Me.Invoke(updateNetworkState(0))
        End If
        Return
    End Sub

    Private Sub CódigoQRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CódigoQRToolStripMenuItem.Click
        If ssid.Text IsNot String.Empty And password.Text IsNot String.Empty Then
            If GlobalVariables.networkIsUp = 1 Then
                GlobalVariables.network = ssid.Text
                GlobalVariables.password = password.Text
                Form4.Show()
            Else
                MsgBox("A rede virtual não está inicializada.")
            End If
        Else
                MsgBox("Por favor preencha os campos 'Nome da Rede' e 'Password' ou utilize os dados padrão da aplicação")
        End If


    End Sub
End Class
