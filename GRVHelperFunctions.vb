Public Class GRVHelperFunctions
    Public Shared Function setValue(ByVal regname As String, ByVal regval As String) As Integer
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", regname, regval)
        Return vbNull
    End Function

    Public Shared Function getValue(ByVal regname As String) As String
        Dim localvar As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", regname, Nothing)
        Return localvar
    End Function

    Public Shared Function GRVInit(ByVal forced As Boolean) As Boolean
        If (getValue("ssidPadrao") Is Nothing Or forced) Then
            My.Computer.Registry.CurrentUser.CreateSubKey("GestorRedesVirtuais")
            setValue("ssidPadrao", "RedeAdHocVirtual")
            setValue("pswPadrao", "Masterlock64")
            setValue("autoNetwork", "no")
            setValue("defaultStartup", "no")
            setValue("autoUpdate", "yes")
            setValue("changelogShown", "yes")
            setValue("showPopup", "yes")
            If (Not forced) Then
                MsgBox("Olá! Esta é a primeira vez que executa o Gestor de Redes Virtuais." + vbNewLine + "Por esse motivo é necessário configurar as placas de rede do seu computador para poder partilhar a ligação à internet. Ao clicar em OK será aberta uma página web com instruções passo-a-passo para configurar tudo correctamente." + vbNewLine + "Poderá abrir esta página mais tarde ao clicar em:" + vbNewLine + "Ajuda -> Configuração Extra -> Configurar placa de rede", MessageBoxIcon.Information, "Primeira utilização")
                Process.Start("http://emanuel-alves.com/GRV/config1.html")
            End If
        End If

        If getValue("defaultStartup") = "yes" Then
            Form1.ssid.Text = getValue("ssidPadrao")
            Form1.password.Text = getValue("pswPadrao")
        End If

        If getValue("showPopup") = "yes" Then
            GlobalVariables.showPopup = 1
        Else
            GlobalVariables.showPopup = 0
        End If

        Return True
    End Function


    Public Shared Function terminate() As String
        If GlobalVariables.networkIsUp = 1 Then
            If MsgBox("A rede virtual está inicializada, ao sair esta será desligada." & vbNewLine & "Deseja sair?", MsgBoxStyle.YesNo, "Atenção") = MsgBoxResult.Yes Then
                applyCommand("netsh wlan stop hostednetwork")
                RemoveHandler Microsoft.Win32.SystemEvents.PowerModeChanged, AddressOf Form1.SystemEvents_PowerModeChanged
                If GlobalVariables.updtQueue = 1 Then
                    appReplaceD(GlobalVariables.updtCmd, 1)
                Else
                    Application.Exit()
                End If
            Else
                Return ""
            End If
        Else
            RemoveHandler Microsoft.Win32.SystemEvents.PowerModeChanged, AddressOf Form1.SystemEvents_PowerModeChanged
            If GlobalVariables.updtQueue = 1 Then
                appReplaceD(GlobalVariables.updtCmd, 1)
            Else
                Application.Exit()
            End If
        End If
        Return ""
    End Function

    Public Shared Function appReplaceD(newfilePath As String, switcher As Int16)
        Dim pId As String = CStr(Process.GetCurrentProcess().Id)
        Dim path As String = Application.ExecutablePath()
        Console.WriteLine("newfilepath is {0}", newfilePath)
        Console.WriteLine("actual path is {0}", path)
        Dim commands As String
        If switcher = 1 Then
            commands = "taskkill /f /PID " + pId + " && timeout 3 && DEL /F /S /Q /A """ + path + """ && exit"
        Else
            commands = "taskkill /f /PID " + pId + " && timeout 3 && DEL /F /S /Q /A """ + path + """ && """ + newfilePath + """ && exit"
        End If
        Console.WriteLine("current command is {0}", commands)
        applyCommand(commands)
    End Function

    Public Shared Function applyCommand(ByVal command As String) As Integer
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
End Class
