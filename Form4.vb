Imports System.IO
Imports System.Reflection
Imports System.Net



Public Class Form4
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim url As String = "http://qrcode.tec-it.com/API/QRCode?data=WIFI%3aT%3aWPA%3bS%3a" + Form1.GlobalVariables.network + "%3bP%3a" + Form1.GlobalVariables.password + "%3b%3b&size=small"
        Try
            Dim tClient As WebClient = New WebClient
            Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
            PictureBox1.Image = tImage
        Catch
            Console.WriteLine("an error occured!")
            Label1.Text = "Ocorreu um erro a obter o código QR."
            Label3.Text = "Por favor verifique a sua ligação à internet."
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class