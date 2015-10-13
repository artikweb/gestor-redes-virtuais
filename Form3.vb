Imports System.IO
Imports System.Reflection
'Gestor de Redes Virtuais para Windows 8 e Windows 10'
'Desenvolvido por Emanuel Alves'
'Código fonte disponível em https://github.com/emannxx/Gestor-de-Redes-Virtuais'
Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        aboutImgWorker.RunWorkerAsync()
        Label1.Text = "Gestor de Redes Virtuais - Versão " & Application.ProductVersion & vbNewLine & "Esta aplicação em VB é desenvolvida e mantida por Emanuel Alves."
    End Sub

    Private Sub aboutImgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles aboutImgWorker.DoWork
        Dim _imageStream As Stream
        Dim _assembly As [Assembly]
        _assembly = [Assembly].GetExecutingAssembly()
        _imageStream = _assembly.GetManifestResourceStream("gestor_de_redes_virtuais.wifi.png")

        Try
            _assembly = [Assembly].GetExecutingAssembly()
            _imageStream = _assembly.GetManifestResourceStream("gestor_de_redes_virtuais.wifi.png")
        Catch ex As Exception
            MessageBox.Show("Resource wasn't found!", "Error")
        End Try

        Try
            PictureBox1.Image = New Bitmap(_imageStream)
        Catch ex As Exception
            MessageBox.Show("Image Couldn't be created !")
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://github.com/emannxx/Gestor-de-Redes-Virtuais")
    End Sub
End Class