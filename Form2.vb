Imports Microsoft.Win32
'Gestor de Redes Virtuais para Windows 8 e Windows 10'
'Desenvolvido por Emanuel Alves'
'Código fonte disponível em https://github.com/emannxx/Gestor-de-Redes-Virtuais'
Public Class defaultconfig

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ssiddefault.Text = "" Or pswdefault.Text = "" Then
            MsgBox("Nenhum dos campos 'Nome da Rede' ou 'Password' podem estar vazios. Introduza a configuração pretendida e tente novamente.", MessageBoxIcon.Warning)
        Else
            If Len(pswdefault.Text) < 8 Then
                MsgBox("A password tem de ter mais de 8 caracteres. Tente novamente.", MessageBoxIcon.Warning)

            Else
                GRVHelperFunctions.setValue("ssidPadrao", ssiddefault.Text)
                GRVHelperFunctions.setValue("pswPadrao", pswdefault.Text)
            End If
        End If

        If (CheckBox1.Checked = True) Then
            GRVHelperFunctions.setValue("defaultStartup", "yes")
        Else GRVHelperFunctions.setValue("defaultStartup", "no")
        End If
        If (CheckBox2.Checked = True) Then
            GRVHelperFunctions.setValue("autoNetwork", "yes")
        Else GRVHelperFunctions.setValue("autoNetwork", "no")
        End If
        If (CheckBox3.Checked = True) Then
            GRVHelperFunctions.setValue("autoUpdate", "yes")
        Else GRVHelperFunctions.setValue("autoUpdate", "no")
        End If
        If CheckBox4.Checked = True Then
            GRVHelperFunctions.setValue("showPopup", "yes")
        Else GRVHelperFunctions.setValue("showPopup", "no")
        End If
        Me.Close()
    End Sub

    Private Sub defaultconfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (GRVHelperFunctions.getValue("ssidPadrao") Is Nothing And GRVHelperFunctions.getValue("pswPadrao") Is Nothing) Then
        Else
            ssiddefault.Text = GRVHelperFunctions.getValue("ssidPadrao")
            pswdefault.Text = GRVHelperFunctions.getValue("pswPadrao")
        End If
        If (GRVHelperFunctions.getValue("defaultStartup") = "no") Or (GRVHelperFunctions.getValue("defaultStartup") = "") Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
        If (GRVHelperFunctions.getValue("autoNetwork") = "no") Or (GRVHelperFunctions.getValue("autoNetwork") = "") Then
            CheckBox2.Checked = False
        Else
            CheckBox2.Checked = True
        End If
        If (GRVHelperFunctions.getValue("autoUpdate") = "no") Or (GRVHelperFunctions.getValue("autoUpdate") = "") Then
            CheckBox3.Checked = False
        Else
            CheckBox3.Checked = True
        End If

        If GRVHelperFunctions.getValue("showPopup") = "no" Or GRVHelperFunctions.getValue("showPopup") = "" Then
            CheckBox4.Checked = False
        Else
            CheckBox4.Checked = True
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = True
            CheckBox1.Enabled = False
        ElseIf CheckBox2.Checked = False Then
            CheckBox1.Enabled = True
        End If
    End Sub

    Private Sub CheckBox4_Click(sender As Object, e As EventArgs) Handles CheckBox4.Click
        MsgBox("Esta alteração será refletida na próxima vez que iniciar a aplicação.", MessageBoxIcon.Information)
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Button1_Click(sender, e)
    End Sub
End Class