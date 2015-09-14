<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ssid = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.password = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FicheiroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlterarConfiguraçãoPadrãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LimparTudoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SairToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AjudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SobreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContactoToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActualizaçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContactoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurarPlacaDeRedeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerificarCompatibilidadeDoSistemaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.currentVersionLabel = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.statebox = New System.Windows.Forms.GroupBox()
        Me.statelabel = New System.Windows.Forms.Label()
        Me.updateWarningLabel = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.statebox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 260)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(152, 56)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Iniciar Rede"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(171, 260)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(152, 56)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Desligar Rede"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ssid
        '
        Me.ssid.Location = New System.Drawing.Point(21, 39)
        Me.ssid.Name = "ssid"
        Me.ssid.Size = New System.Drawing.Size(272, 20)
        Me.ssid.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nome da Rede (SSID)"
        '
        'password
        '
        Me.password.Location = New System.Drawing.Point(21, 89)
        Me.password.Name = "password"
        Me.password.Size = New System.Drawing.Size(272, 20)
        Me.password.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(137, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Password da Rede (WPA2)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(284, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "1 - Introduza a configuração da rede nos campos em baixo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "2 - Clique em Iniciar Rede"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ssid)
        Me.GroupBox1.Controls.Add(Me.password)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 83)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(311, 123)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dados da Rede"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FicheiroToolStripMenuItem, Me.AjudaToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(337, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FicheiroToolStripMenuItem
        '
        Me.FicheiroToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlterarConfiguraçãoPadrãoToolStripMenuItem, Me.LimparTudoToolStripMenuItem, Me.SairToolStripMenuItem})
        Me.FicheiroToolStripMenuItem.Name = "FicheiroToolStripMenuItem"
        Me.FicheiroToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.FicheiroToolStripMenuItem.Text = "Aplicação"
        '
        'AlterarConfiguraçãoPadrãoToolStripMenuItem
        '
        Me.AlterarConfiguraçãoPadrãoToolStripMenuItem.Name = "AlterarConfiguraçãoPadrãoToolStripMenuItem"
        Me.AlterarConfiguraçãoPadrãoToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.AlterarConfiguraçãoPadrãoToolStripMenuItem.Text = "Definições"
        '
        'LimparTudoToolStripMenuItem
        '
        Me.LimparTudoToolStripMenuItem.Name = "LimparTudoToolStripMenuItem"
        Me.LimparTudoToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.LimparTudoToolStripMenuItem.Text = "Limpar tudo"
        '
        'SairToolStripMenuItem
        '
        Me.SairToolStripMenuItem.Name = "SairToolStripMenuItem"
        Me.SairToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.SairToolStripMenuItem.Text = "Sair"
        '
        'AjudaToolStripMenuItem
        '
        Me.AjudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SobreToolStripMenuItem, Me.ContactoToolStripMenuItem1, Me.ActualizaçãoToolStripMenuItem, Me.ContactoToolStripMenuItem})
        Me.AjudaToolStripMenuItem.Name = "AjudaToolStripMenuItem"
        Me.AjudaToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.AjudaToolStripMenuItem.Text = "Ajuda"
        '
        'SobreToolStripMenuItem
        '
        Me.SobreToolStripMenuItem.Name = "SobreToolStripMenuItem"
        Me.SobreToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.SobreToolStripMenuItem.Text = "Sobre"
        '
        'ContactoToolStripMenuItem1
        '
        Me.ContactoToolStripMenuItem1.Name = "ContactoToolStripMenuItem1"
        Me.ContactoToolStripMenuItem1.Size = New System.Drawing.Size(173, 22)
        Me.ContactoToolStripMenuItem1.Text = "Contacto"
        '
        'ActualizaçãoToolStripMenuItem
        '
        Me.ActualizaçãoToolStripMenuItem.Name = "ActualizaçãoToolStripMenuItem"
        Me.ActualizaçãoToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ActualizaçãoToolStripMenuItem.Text = "Procurar Atualização"
        '
        'ContactoToolStripMenuItem
        '
        Me.ContactoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigurarPlacaDeRedeToolStripMenuItem, Me.VerificarCompatibilidadeDoSistemaToolStripMenuItem})
        Me.ContactoToolStripMenuItem.Name = "ContactoToolStripMenuItem"
        Me.ContactoToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ContactoToolStripMenuItem.Text = "Configuração Extra"
        '
        'ConfigurarPlacaDeRedeToolStripMenuItem
        '
        Me.ConfigurarPlacaDeRedeToolStripMenuItem.Name = "ConfigurarPlacaDeRedeToolStripMenuItem"
        Me.ConfigurarPlacaDeRedeToolStripMenuItem.Size = New System.Drawing.Size(243, 22)
        Me.ConfigurarPlacaDeRedeToolStripMenuItem.Text = "Configurar placa de rede"
        '
        'VerificarCompatibilidadeDoSistemaToolStripMenuItem
        '
        Me.VerificarCompatibilidadeDoSistemaToolStripMenuItem.Name = "VerificarCompatibilidadeDoSistemaToolStripMenuItem"
        Me.VerificarCompatibilidadeDoSistemaToolStripMenuItem.Size = New System.Drawing.Size(243, 22)
        Me.VerificarCompatibilidadeDoSistemaToolStripMenuItem.Text = "Verificar compatibilidade do sistema"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(12, 323)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(311, 23)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "Utilizar Configuração Padrão da Aplicação"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(12, 352)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(311, 23)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "Abrir Placas de Rede"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(246, 410)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(77, 13)
        Me.LinkLabel1.TabIndex = 14
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Emanuel Alves"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(160, 410)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Desenvolvido por"
        '
        'currentVersionLabel
        '
        Me.currentVersionLabel.AutoSize = True
        Me.currentVersionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentVersionLabel.Location = New System.Drawing.Point(10, 410)
        Me.currentVersionLabel.Name = "currentVersionLabel"
        Me.currentVersionLabel.Size = New System.Drawing.Size(36, 12)
        Me.currentVersionLabel.TabIndex = 6
        Me.currentVersionLabel.Text = "version"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(12, 381)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(311, 23)
        Me.Button4.TabIndex = 15
        Me.Button4.Text = "Sair"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'statebox
        '
        Me.statebox.Controls.Add(Me.statelabel)
        Me.statebox.Location = New System.Drawing.Point(12, 214)
        Me.statebox.Name = "statebox"
        Me.statebox.Size = New System.Drawing.Size(311, 34)
        Me.statebox.TabIndex = 16
        Me.statebox.TabStop = False
        '
        'statelabel
        '
        Me.statelabel.AutoSize = True
        Me.statelabel.Location = New System.Drawing.Point(122, 13)
        Me.statelabel.Name = "statelabel"
        Me.statelabel.Size = New System.Drawing.Size(68, 13)
        Me.statelabel.TabIndex = 0
        Me.statelabel.Text = "Rede Inativa"
        '
        'updateWarningLabel
        '
        Me.updateWarningLabel.AutoSize = True
        Me.updateWarningLabel.BackColor = System.Drawing.Color.IndianRed
        Me.updateWarningLabel.Location = New System.Drawing.Point(62, 429)
        Me.updateWarningLabel.Name = "updateWarningLabel"
        Me.updateWarningLabel.Size = New System.Drawing.Size(198, 13)
        Me.updateWarningLabel.TabIndex = 18
        Me.updateWarningLabel.Text = "Está a utilizar uma versão desatualizada!"
        Me.updateWarningLabel.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 430)
        Me.Controls.Add(Me.updateWarningLabel)
        Me.Controls.Add(Me.statebox)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.currentVersionLabel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Gestor de Redes Virtuais"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.statebox.ResumeLayout(False)
        Me.statebox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ssid As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents password As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FicheiroToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LimparTudoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SairToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AjudaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SobreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContactoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button3 As Button
    Friend WithEvents ConfigurarPlacaDeRedeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VerificarCompatibilidadeDoSistemaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContactoToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Button5 As Button
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label5 As Label
    Friend WithEvents currentVersionLabel As Label
    Friend WithEvents AlterarConfiguraçãoPadrãoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button4 As Button
    Friend WithEvents statebox As GroupBox
    Friend WithEvents statelabel As Label
    Friend WithEvents ActualizaçãoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents updateWarningLabel As Label
End Class
