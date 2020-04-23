Public Class WherePart
    Private _WhereField As String
    Dim WhereConditions As String
    Dim GlobalParms As New ESPOParms.Framework
    Dim GlobalSession As New ESPOParms.Session

    Public Sub GetParms(Session As ESPOParms.Session, Parms As ESPOParms.Framework)
        GlobalParms = Parms
        GlobalSession = Session
    End Sub

    Public Property WhereField As String
        Get
            Return _WhereField
        End Get
        Set(value As String)
            _WhereField = value
        End Set
    End Property

    Private Sub WherePart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        Me.MdiParent = FromHandle(GlobalSession.MDIParentHandle)
        BuildOperatorCombo()

    End Sub

    Sub PopulateForm(ByRef FieldList As String())
        Dim OrCondtions As String()
        Dim AndConditions As String()
        Dim AllConditions As String()
        'Populate Field List from previous form:
        'lbSelectedWHEREFields.Items.AddRange(FieldList)
        lbSelectedWHEREFields.Items.AddRange(FieldList)
        lblMessage.Text = ""
        lblMessage.Visible = False
        WhereConditions = ColumnSelect.myWhereConditions.GetMyWhereCondtions
        SetControls("SINGLE", "")
        If WhereConditions <> "" Then
            ReDim AllConditions(0)
            txtConditions.Text = WhereConditions
        End If


    End Sub

    Sub BuildOperatorCombo()
        Dim lstOperators As New List(Of Operators)

        lstOperators.Add(New Operators With {.OperatorSymbol = "=", .OperatorFull = "Equals"})
        lstOperators.Add(New Operators With {.OperatorSymbol = ">", .OperatorFull = "Greater Than"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "<", .OperatorFull = "Less Than"})
        lstOperators.Add(New Operators With {.OperatorSymbol = ">=", .OperatorFull = "Greater Than or Equal to"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "<=", .OperatorFull = "Less Than or Equal to"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "<>", .OperatorFull = "Does Not Equal"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "BEGINS", .OperatorFull = "Begins With"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "NOT BEGINS", .OperatorFull = "Does Not Begin With"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "LIKE", .OperatorFull = "Contains"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "NOT LIKE", .OperatorFull = "Does Not Contain"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "BETWEEN", .OperatorFull = "BETWEEN"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "NOT BETWEEN", .OperatorFull = "Is Not Between"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "IN", .OperatorFull = "In"})
        lstOperators.Add(New Operators With {.OperatorSymbol = "NOT IN", .OperatorFull = "Not In"})
        'lstOperators.Add(New Operators With {.OperatorSymbol = "LIKE", .OperatorFull = "LIKE"})

        cboOperators.DataSource = lstOperators
        cboOperators.ValueMember = "OperatorSymbol"
        cboOperators.DisplayMember = "OperatorFull"

    End Sub

    Sub SetControls(PosType As String, DBTable As String)
        Dim myDAL As New SQLBuilderDAL
        Dim myTypes As String
        Dim myFieldnames As String
        Dim dic_Types As Object

        dic_Types = CreateObject("Scripting.Dictionary")
        dic_Types.comparemode = vbTextCompare

        If DBTable <> "" Then
            myDAL.GetMySQLFieldsAndTypes(DBTable, myTypes, dic_Types, myFieldnames)
        End If
        'if type = datetime then make the datepickers visible.

        If UCase(PosType) = "SINGLE" Then
            dtp1.Visible = True
            dtp2.Visible = False
            dtp1.Value = Now()
            dtp2.Value = Now()
            lblAnd.Visible = False
            txtValue2.Visible = False
            lbValues.Visible = False
            btnAddValue.Visible = False
            GroupBox2.Left = 11
            GroupBox2.Top = 105
            GroupBox2.SendToBack()
            Me.Height = 358
        ElseIf UCase(PosType) = "BETWEEN" Then
            dtp1.Visible = True
            dtp2.Visible = True
            dtp1.Value = Now()
            dtp2.Value = Now()
            lblAnd.Visible = True
            txtValue2.Visible = True
            lbValues.Visible = False
            btnAddValue.Visible = False
            GroupBox2.Left = 11
            GroupBox2.Top = 105
            GroupBox2.SendToBack()
            Me.Height = 505
        ElseIf UCase(PosType) = "IN" Then
            dtp1.Visible = False
            dtp2.Visible = False
            lblAnd.Visible = False
            txtValue2.Visible = False
            lbValues.Visible = True
            btnAddValue.Visible = True
            GroupBox2.Left = 11
            GroupBox2.Top = 254
            GroupBox2.SendToBack()
            Me.Height = 505
        End If
    End Sub

    Function IsInList(lstBox As ListBox, CheckItem As String) As Boolean
        Dim ItemName As String

        IsInList = False
        For i As Integer = 0 To lstBox.Items.Count - 1
            ItemName = lstBox.Items(i)
            If UCase(CheckItem) = UCase(ItemName) Then
                Return True
                Exit For
            End If
        Next

    End Function

    Function AddCondition() As String
        '****************************************************** Add Condition to list: *****************************************************
        Dim strCondition As String
        Dim strWhereField1 As String
        Dim strWhereField2 As String
        Dim intSelectedFieldIDX As Integer
        Dim strOperator As String
        Dim strValue As String
        Dim strJoin As String
        Dim Position As Integer
        Dim Message As String
        Dim lstValues As String

        'Perhaps we need the db field type here to validate the value entered by the user ???
        strValue = ""
        AddCondition = ""
        strCondition = ""
        intSelectedFieldIDX = lbSelectedWHEREFields.SelectedIndex
        If intSelectedFieldIDX = -1 Then
            MsgBox("Please select a field first")
            Exit Function
        End If
        If lbSelectedWHEREFields.Items.Count > 0 Then
            'grab selected field:
            strWhereField1 = lbSelectedWHEREFields.Items(intSelectedFieldIDX)
        Else
            MsgBox("Dont forget to press the Add Field button.")
            Exit Function
        End If

        If lbSelectedWHEREFields.Items.Count > 1 Then
            'just an idea at the moment:
            strWhereField2 = lbSelectedWHEREFields.Items(1)
        End If
        strOperator = ""
        strJoin = ""
        If strWhereField1 <> "" Then
            If txtOperator.Text <> "" Then
                strOperator = txtOperator.Text
                strValue = txtValue.Text
                If cbIncludeSingleQuote.Checked Then
                    strValue = "'" & strValue & "'"
                End If
                If UCase(strOperator) = "BETWEEN" Or UCase(strOperator) = "NOT BETWEEN" Then
                    'two dates in both text boxes:
                    If txtValue.Text <> "" And txtValue2.Text <> "" Then
                        If IsDate(txtValue.Text) Then
                            If IsDate(txtValue2.Text) Then
                                strValue = "'" & CDate(txtValue.Text).ToString("yyyy-MM-dd") & "'" & " AND " & "'" & CDate(txtValue2.Text).ToString("yyyy-MM-dd") & "'"
                            End If
                        End If
                    End If
                End If
                If UCase(strOperator) = "IN" Or UCase(strOperator) = "NOT IN" Then
                    If lbValues.Items.Count > 0 Then
                        strValue = "("
                        For val As Integer = 0 To lbValues.Items.Count - 1
                            If lstValues = "" Then
                                lstValues = "'" & lbValues.Items(val) & "'"
                            Else
                                lstValues += "," & "'" & lbValues.Items(val) & "'"
                            End If
                        Next
                        strValue += lstValues & ")"
                    End If
                End If
                If UCase(strOperator) = "BEGINS" Then
                    strValue = "LIKE '" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
                    strOperator = ""
                End If
                If UCase(strOperator) = "NOT BEGINS" Then
                    strValue = "NOT LIKE '" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
                    strOperator = ""
                End If
                If UCase(strOperator) = "LIKE" Then
                    strValue = "LIKE '%" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
                    strOperator = ""
                End If
                If UCase(strOperator) = "NOT LIKE" Then
                    strValue = "NOT LIKE '%" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
                    strOperator = ""
                End If
                'Check if any other conditions exist:
                If ColumnSelect.myWhereConditions.CountConditions > 0 Then
                    If rbAND.Checked Then
                        strJoin = " AND "
                    End If
                    If rbOR.Checked Then
                        strJoin = " OR "
                    End If
                End If

                If strValue = "" Then
                    MsgBox("Please enter a value")
                    Exit Function
                End If

                strCondition += strWhereField1 & " " & strOperator & " " & strValue
                If Not ColumnSelect.myWhereConditions.IsInList(strCondition) Then
                    'lbConditions.Items.Add(strJoin & strCondition)
                    ColumnSelect.myWhereConditions.lbConditions.Add(strJoin & strCondition)
                Else
                    MsgBox("Already in condtions list")
                    Exit Function
                End If
                WhereConditions += strJoin & strCondition & vbCrLf
                txtConditions.Text = WhereConditions
                Return strCondition
            Else
                    MsgBox("No operator or value entered.")
                Exit Function
            End If
        End If

    End Function

    Private Sub lbSelectedWHEREFields_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbSelectedWHEREFields.SelectedIndexChanged
        Dim IDX As Integer

        IDX = lbSelectedWHEREFields.SelectedIndex
        Me.WhereField = lbSelectedWHEREFields.Items(IDX)

    End Sub

    Private Sub btnAddWhere_Click(sender As Object, e As EventArgs) Handles btnAddWhere.Click
        'pass list back
        If ColumnSelect.myWhereConditions.CountConditions > 0 Then
            'SQLBuilder.myWhereConditions.GetMyWhereCondtions = ""
            For i As Integer = 0 To ColumnSelect.myWhereConditions.lbConditions.Count - 1
                ColumnSelect.myWhereConditions.GetMyWhereCondtions += ColumnSelect.myWhereConditions.lbConditions.Item(i)
            Next
        Else
            MsgBox("No conditions were entered")
        End If
        'If lbConditions.Items.Count > 0 Then

        Close()

    End Sub

    Private Sub btnAddCondition_Click(sender As Object, e As EventArgs) Handles btnAddCondition.Click
        AddCondition()

    End Sub

    Private Sub btnRemoveCondition_Click(sender As Object, e As EventArgs) Handles btnRemoveCondition.Click
        Dim IDX As Integer

        'IDX = lbConditions.SelectedIndex
        'If IDX > -1 Then
        'lbConditions.Items.RemoveAt(IDX)
        'End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()

    End Sub

    Private Sub cboOperators_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOperators.SelectedIndexChanged
        Dim IDX As Integer
        Dim ItemName As String

        IDX = cboOperators.SelectedIndex
        If IDX > -1 Then
            ItemName = cboOperators.SelectedValue.ToString()

            If ItemName <> "" Then
                txtOperator.Text = ItemName
                SetControls("SINGLE", "")
            End If
            lblMessage.Text = ""
            lblMessage.Visible = False
            If ItemName = "LIKE" Or ItemName = "NOT LIKE" Then
                'lblMessage.Text = "use % for wildcard and _ for single character search"
                'lblMessage.Visible = True
                SetControls("SINGLE", "")
            End If
            If ItemName = "BETWEEN" Or ItemName = "NOT BETWEEN" Then
                lblMessage.Visible = False
                SetControls("BETWEEN", "")
            End If
            If ItemName = "IN" Or ItemName = "NOT IN" Then
                lblMessage.Visible = False
                SetControls("IN", "")
            End If
        End If

    End Sub

    Private Sub UndockChild()
        If Me.MdiParent Is Nothing Then
            Me.MdiParent = FromHandle(GlobalSession.MDIParentHandle)
        Else
            Me.MdiParent = Nothing
        End If
    End Sub

    Private Sub WherePart_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyValue = Keys.F5 Then
            'btnRefresh.PerformClick()
        ElseIf e.KeyValue = 27 Then 'ESC pressed
            'Clear certain fields
        ElseIf e.KeyValue = Keys.F7 Then
            UndockChild()
        End If
    End Sub

    Private Sub btnAddValue_Click(sender As Object, e As EventArgs) Handles btnAddValue.Click
        If txtValue.Text <> "" Then
            If lbValues.Visible = True Then
                lbValues.Items.Add(txtValue.Text)
                txtValue.Text = ""
            End If
        End If
    End Sub

    Private Sub dtp1_ValueChanged(sender As Object, e As EventArgs) Handles dtp1.ValueChanged
        'txtValue.Text = dtp1.Text
    End Sub

    Private Sub dtp2_ValueChanged(sender As Object, e As EventArgs)
        'txtValue2.Text = dtp2.Text
    End Sub

    Private Sub dtp1_Leave(sender As Object, e As EventArgs) Handles dtp1.Leave
        txtValue.Text = dtp1.Text
    End Sub

    Private Sub dtp2_Leave(sender As Object, e As EventArgs) Handles dtp2.Leave
        txtValue2.Text = dtp2.Text
    End Sub
End Class