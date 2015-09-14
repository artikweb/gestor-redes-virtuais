Imports System.IO
Imports System.Reflection

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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


        Label1.Text = "Gestor de Redes Virtuais - Versão " & Application.ProductVersion & vbNewLine & "Esta aplicação em VB é desenvolvida e mantida por Emanuel Alves." & vbNewLine & "Para mais informações: emanuel-alves@outlook.com"
    End Sub
End Class