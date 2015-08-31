Imports Microsoft.Win32
'Gestor de Redes Virtuais para Windows 8 e Windows 10'
'Desenvolvido por Emanuel Alves em Junho de 2015'
'Código fonte disponível em https://github.com/emannxx/Gestor-de-Redes-Virtuais'

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "ssidPadrao", Nothing) Is Nothing And My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "pswPadrao", Nothing) Is Nothing) Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "ssidPadrao", "RedeAdHocVirtual")
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "pswPadrao", "Masterlock64")
        End If

        If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "defaultStartup", Nothing) = "yes" Then
            ssid.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "ssidPadrao", Nothing)
            password.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "pswPadrao", Nothing)
        End If
    End Sub

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
            End If
        Else
            Console.WriteLine("that didnt work either")
            MsgBox("Os dados introduzidos não são válidos." & vbNewLine & "As passwords têm que ter mais de 8 caracteres, corrija a configuração e tente de novo.", MessageBoxIcon.Error, "Ocorreu um erro")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        applyCommand("netsh wlan stop hostednetwork")
        MsgBox("Rede virtual desligada com sucesso!", MessageBoxIcon.Information)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ssid.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "ssidPadrao", Nothing)
        password.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "pswPadrao", Nothing)
    End Sub

    Private Sub SobreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SobreToolStripMenuItem.Click
        MsgBox("Gestor de Redes Virtuais - Versão 0.9.1-Beta" & vbNewLine & "Esta aplicação em VB foi desenvolvida por Emanuel Alves em Junho de 2015." & vbNewLine & "Para mais informações: emanuel-alves@outlook.com", 0, "Sobre")
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
        Application.Exit()
    End Sub

    Private Sub ConfigurarPlacaDeRedeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigurarPlacaDeRedeToolStripMenuItem.Click
        MsgBox("Será aberta uma página no seu browser. Necessita de uma ligação à internet. ", MessageBoxIcon.Information)
        Process.Start("http://emanuel-alves.com/GRV/config1.html")
    End Sub

    Private Sub VerificarCompatibilidadeDoSistemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerificarCompatibilidadeDoSistemaToolStripMenuItem.Click
        MsgBox("Será aberta uma página no seu browser. Necessita de uma ligação à internet. ", MessageBoxIcon.Information)
        Process.Start("http://emanuel-alves.com/GRV/config2.html")
    End Sub
End Class
