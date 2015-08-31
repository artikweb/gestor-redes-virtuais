Imports Microsoft.Win32
'Gestor de Redes Virtuais para Windows 8 e Windows 10'
'Desenvolvido por Emanuel Alves em Junho de 2015'
'Código fonte disponível em https://github.com/emannxx/Gestor-de-Redes-Virtuais'
Public Class defaultconfig
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Computer.Registry.CurrentUser.CreateSubKey("GestorRedesVirtuais")
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "ssidPadrao", ssiddefault.Text)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "pswPadrao", pswdefault.Text)
        If (CheckBox1.Checked = True) Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "defaultStartup", "yes")
        Else My.Computer.Registry.SetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "defaultStartup", "no")
        End If
        MsgBox("Configuração padrão atualizada com sucesso!", MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub defaultconfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "ssidPadrao", Nothing) Is Nothing And My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "pswPadrao", Nothing) Is Nothing) Then
        Else
            ssiddefault.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "ssidPadrao", Nothing)
            pswdefault.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "pswPadrao", Nothing)
        End If
        If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\GestorRedesVirtuais", "defaultStartup", Nothing) = "no" Then
        Else
            CheckBox1.Checked = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class