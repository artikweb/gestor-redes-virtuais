Public Class GlobalVariables
    Public Shared networkIsUp As Int16 = 0
    Public Shared currentVersion As String = Application.ProductVersion.ToString
    Public Shared fakedate As String = "13-09-2014"
    Public Shared showPopup As Int16
    Public Shared changelog As String
    Public Shared updtQueue As Int16
    Public Shared updtCmd As String
    Public Shared item3State As Boolean = False
    Public Shared notificationSeen As Boolean = False
    Public Shared network As String = ""
    Public Shared password As String = ""
    Public Shared _lang = New Dictionary(Of String, String)

End Class
