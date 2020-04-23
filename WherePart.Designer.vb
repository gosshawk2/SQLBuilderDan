<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WherePart
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbOR = New System.Windows.Forms.RadioButton()
        Me.rbAND = New System.Windows.Forms.RadioButton()
        Me.lblConditions = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lbSelectedWHEREFields = New System.Windows.Forms.ListBox()
        Me.txtOperator = New System.Windows.Forms.TextBox()
        Me.btnRemoveCondition = New System.Windows.Forms.Button()
        Me.btnAddCondition = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cboOperators = New System.Windows.Forms.ComboBox()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.cbIncludeSingleQuote = New System.Windows.Forms.CheckBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnAddWhere = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.txtValue2 = New System.Windows.Forms.TextBox()
        Me.lblAnd = New System.Windows.Forms.Label()
        Me.dtp1 = New System.Windows.Forms.DateTimePicker()
        Me.dtp2 = New System.Windows.Forms.DateTimePicker()
        Me.lbValues = New System.Windows.Forms.ListBox()
        Me.btnAddValue = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtConditions = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbOR)
        Me.GroupBox1.Controls.Add(Me.rbAND)
        Me.GroupBox1.Location = New System.Drawing.Point(763, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(125, 54)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Join Condition with:"
        '
        'rbOR
        '
        Me.rbOR.AutoSize = True
        Me.rbOR.Location = New System.Drawing.Point(68, 19)
        Me.rbOR.Name = "rbOR"
        Me.rbOR.Size = New System.Drawing.Size(41, 17)
        Me.rbOR.TabIndex = 1
        Me.rbOR.Text = "OR"
        Me.rbOR.UseVisualStyleBackColor = True
        '
        'rbAND
        '
        Me.rbAND.AutoSize = True
        Me.rbAND.Checked = True
        Me.rbAND.Location = New System.Drawing.Point(14, 19)
        Me.rbAND.Name = "rbAND"
        Me.rbAND.Size = New System.Drawing.Size(48, 17)
        Me.rbAND.TabIndex = 0
        Me.rbAND.TabStop = True
        Me.rbAND.Text = "AND"
        Me.rbAND.UseVisualStyleBackColor = True
        '
        'lblConditions
        '
        Me.lblConditions.AutoSize = True
        Me.lblConditions.Location = New System.Drawing.Point(221, 58)
        Me.lblConditions.Name = "lblConditions"
        Me.lblConditions.Size = New System.Drawing.Size(77, 13)
        Me.lblConditions.TabIndex = 49
        Me.lblConditions.Text = "CONDITIONS:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(14, 29)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(150, 13)
        Me.Label11.TabIndex = 48
        Me.Label11.Text = "SELECTED WHERE FIELDS:"
        '
        'lbSelectedWHEREFields
        '
        Me.lbSelectedWHEREFields.FormattingEnabled = True
        Me.lbSelectedWHEREFields.Location = New System.Drawing.Point(11, 52)
        Me.lbSelectedWHEREFields.Name = "lbSelectedWHEREFields"
        Me.lbSelectedWHEREFields.Size = New System.Drawing.Size(201, 212)
        Me.lbSelectedWHEREFields.TabIndex = 46
        '
        'txtOperator
        '
        Me.txtOperator.Location = New System.Drawing.Point(234, 79)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.Size = New System.Drawing.Size(114, 20)
        Me.txtOperator.TabIndex = 45
        '
        'btnRemoveCondition
        '
        Me.btnRemoveCondition.Location = New System.Drawing.Point(566, 47)
        Me.btnRemoveCondition.Name = "btnRemoveCondition"
        Me.btnRemoveCondition.Size = New System.Drawing.Size(120, 23)
        Me.btnRemoveCondition.TabIndex = 44
        Me.btnRemoveCondition.Text = "Remove Condition"
        Me.btnRemoveCondition.UseVisualStyleBackColor = True
        '
        'btnAddCondition
        '
        Me.btnAddCondition.Location = New System.Drawing.Point(377, 47)
        Me.btnAddCondition.Name = "btnAddCondition"
        Me.btnAddCondition.Size = New System.Drawing.Size(120, 23)
        Me.btnAddCondition.TabIndex = 43
        Me.btnAddCondition.Text = "Add Condition"
        Me.btnAddCondition.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(231, 29)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(117, 13)
        Me.Label14.TabIndex = 42
        Me.Label14.Text = "SELECT OPERATION:"
        '
        'cboOperators
        '
        Me.cboOperators.FormattingEnabled = True
        Me.cboOperators.Location = New System.Drawing.Point(234, 52)
        Me.cboOperators.Name = "cboOperators"
        Me.cboOperators.Size = New System.Drawing.Size(114, 21)
        Me.cboOperators.TabIndex = 41
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(389, 79)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(388, 20)
        Me.txtValue.TabIndex = 52
        '
        'cbIncludeSingleQuote
        '
        Me.cbIncludeSingleQuote.AutoSize = True
        Me.cbIncludeSingleQuote.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbIncludeSingleQuote.Checked = True
        Me.cbIncludeSingleQuote.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbIncludeSingleQuote.Location = New System.Drawing.Point(389, 59)
        Me.cbIncludeSingleQuote.Name = "cbIncludeSingleQuote"
        Me.cbIncludeSingleQuote.Size = New System.Drawing.Size(128, 17)
        Me.cbIncludeSingleQuote.TabIndex = 54
        Me.cbIncludeSingleQuote.Text = "Include Single Quote:"
        Me.cbIncludeSingleQuote.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(385, 29)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(85, 13)
        Me.lblTitle.TabIndex = 55
        Me.lblTitle.Text = "ENTER VALUE:"
        '
        'btnAddWhere
        '
        Me.btnAddWhere.Location = New System.Drawing.Point(1, 174)
        Me.btnAddWhere.Name = "btnAddWhere"
        Me.btnAddWhere.Size = New System.Drawing.Size(120, 23)
        Me.btnAddWhere.TabIndex = 56
        Me.btnAddWhere.Text = "Add Where"
        Me.btnAddWhere.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(160, 174)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 23)
        Me.btnCancel.TabIndex = 57
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(566, 61)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(0, 13)
        Me.lblMessage.TabIndex = 58
        '
        'txtValue2
        '
        Me.txtValue2.Location = New System.Drawing.Point(389, 129)
        Me.txtValue2.Name = "txtValue2"
        Me.txtValue2.Size = New System.Drawing.Size(388, 20)
        Me.txtValue2.TabIndex = 59
        Me.txtValue2.Visible = False
        '
        'lblAnd
        '
        Me.lblAnd.AutoSize = True
        Me.lblAnd.Location = New System.Drawing.Point(386, 108)
        Me.lblAnd.Name = "lblAnd"
        Me.lblAnd.Size = New System.Drawing.Size(30, 13)
        Me.lblAnd.TabIndex = 60
        Me.lblAnd.Text = "AND"
        Me.lblAnd.Visible = False
        '
        'dtp1
        '
        Me.dtp1.CustomFormat = "yyyy-MM-dd"
        Me.dtp1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp1.Location = New System.Drawing.Point(577, 26)
        Me.dtp1.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtp1.Name = "dtp1"
        Me.dtp1.Size = New System.Drawing.Size(200, 20)
        Me.dtp1.TabIndex = 61
        Me.dtp1.Value = New Date(2020, 4, 7, 0, 0, 0, 0)
        '
        'dtp2
        '
        Me.dtp2.CustomFormat = "yyyy-MM-dd"
        Me.dtp2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp2.Location = New System.Drawing.Point(577, 108)
        Me.dtp2.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtp2.Name = "dtp2"
        Me.dtp2.Size = New System.Drawing.Size(200, 20)
        Me.dtp2.TabIndex = 62
        Me.dtp2.Visible = False
        '
        'lbValues
        '
        Me.lbValues.FormattingEnabled = True
        Me.lbValues.Location = New System.Drawing.Point(577, 104)
        Me.lbValues.Name = "lbValues"
        Me.lbValues.Size = New System.Drawing.Size(200, 147)
        Me.lbValues.TabIndex = 63
        Me.lbValues.Visible = False
        '
        'btnAddValue
        '
        Me.btnAddValue.Location = New System.Drawing.Point(450, 104)
        Me.btnAddValue.Name = "btnAddValue"
        Me.btnAddValue.Size = New System.Drawing.Size(95, 23)
        Me.btnAddValue.TabIndex = 64
        Me.btnAddValue.Text = "Add Value"
        Me.btnAddValue.UseVisualStyleBackColor = True
        Me.btnAddValue.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.GroupBox2.Controls.Add(Me.txtConditions)
        Me.GroupBox2.Controls.Add(Me.btnAddCondition)
        Me.GroupBox2.Controls.Add(Me.btnRemoveCondition)
        Me.GroupBox2.Controls.Add(Me.lblConditions)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.btnAddWhere)
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(11, 105)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(893, 208)
        Me.GroupBox2.TabIndex = 65
        Me.GroupBox2.TabStop = False
        '
        'txtConditions
        '
        Me.txtConditions.Location = New System.Drawing.Point(223, 76)
        Me.txtConditions.Multiline = True
        Me.txtConditions.Name = "txtConditions"
        Me.txtConditions.Size = New System.Drawing.Size(665, 83)
        Me.txtConditions.TabIndex = 58
        '
        'WherePart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 381)
        Me.Controls.Add(Me.lbValues)
        Me.Controls.Add(Me.lbSelectedWHEREFields)
        Me.Controls.Add(Me.btnAddValue)
        Me.Controls.Add(Me.dtp2)
        Me.Controls.Add(Me.dtp1)
        Me.Controls.Add(Me.lblAnd)
        Me.Controls.Add(Me.txtValue2)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.cbIncludeSingleQuote)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtOperator)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cboOperators)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "WherePart"
        Me.Text = "Enter Where Clause"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbOR As RadioButton
    Friend WithEvents rbAND As RadioButton
    Friend WithEvents lblConditions As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents lbSelectedWHEREFields As ListBox
    Friend WithEvents txtOperator As TextBox
    Friend WithEvents btnRemoveCondition As Button
    Friend WithEvents btnAddCondition As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents cboOperators As ComboBox
    Friend WithEvents txtValue As TextBox
    Friend WithEvents cbIncludeSingleQuote As CheckBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents btnAddWhere As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblMessage As Label
    Friend WithEvents txtValue2 As TextBox
    Friend WithEvents lblAnd As Label
    Friend WithEvents dtp1 As DateTimePicker
    Friend WithEvents dtp2 As DateTimePicker
    Friend WithEvents lbValues As ListBox
    Friend WithEvents btnAddValue As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtConditions As TextBox
End Class
