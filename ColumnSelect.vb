Public Class ColumnSelect
    Private _DataSetID As Integer
    Private _WhereConditions As String
    Private _WhereField As String
    Dim GlobalParms As New ESPOParms.Framework
    Dim GlobalSession As New ESPOParms.Session
    Public Shared myWhereConditions As New myGlobals
    Public Shared FieldAttributes As New clsAttributes

    Public Sub GetParms(Session As ESPOParms.Session, Parms As ESPOParms.Framework)
        GlobalParms = Parms
        GlobalSession = Session
    End Sub

    Private Sub SQLBuilder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        Me.KeyPreview = True
        Me.MdiParent = FromHandle(GlobalSession.MDIParentHandle)
        dgvFieldSelection.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvFieldSelection.AllowUserToOrderColumns = True
        dgvFieldSelection.AllowUserToResizeColumns = True
        dgvFieldSelection.AllowUserToAddRows = False
        dgvFieldSelection.AllowUserToDeleteRows = False
        dgvFieldSelection.MultiSelect = True
        dgvFieldSelection.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        myWhereConditions.DeleteConditions()
        myWhereConditions.ClearConditionsList()
        myWhereConditions.LastOperator = ""
        FieldAttributes.ClearSelectedAttributesList()

        For Each c As Control In Controls
            AddHandler c.MouseClick, AddressOf ClickHandler
        Next
        'Me.Height = 584

    End Sub

    Private Sub ClickHandler(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        'Label24.Text = String.Format("Clicked ""{0}"" with the {1} mouse button.", sender.name, e.Button.ToString.ToLower)
        Select Case e.Button
            Case MouseButtons.Left
                Me.BringToFront()
        End Select
    End Sub

    Public Property TheDataSetID As Integer
        Get
            Return _DataSetID
        End Get
        Set(value As Integer)
            _DataSetID = value
        End Set
    End Property

    Public Property WhereConditions As String
        Get
            Return _WhereConditions
        End Get
        Set(value As String)
            _WhereConditions = value
        End Set
    End Property

    Public Property WhereField As String
        Get
            Return _WhereField
        End Get
        Set(value As String)
            _WhereField = value
        End Set
    End Property

    Sub PopulateForm(DataSetID As Integer)
        Cursor = Cursors.WaitCursor
        Dim myDAL As New SQLBuilderDAL
        Dim dt As DataTable
        Dim IndexCol As DataColumn

        Try
            Me.Text = "SQL Builder"
            Me.TheDataSetID = DataSetID
            dgvFieldSelection.Columns.Clear()
            'Dim dgvCheck As New DataGridViewCheckBoxColumn()
            'dgvCheck.HeaderText = "Select Field"
            'dgvCheck.Name = "SelectField"

            Dim dgvCheckSum As New DataGridViewCheckBoxColumn()
            dgvCheckSum.HeaderText = "SUM()"
            dgvCheckSum.Name = "SUM"
            Dim dgvCheckMIN As New DataGridViewCheckBoxColumn()
            dgvCheckMIN.HeaderText = "MIN()"
            dgvCheckMIN.Name = "MIN"
            Dim dgvCheckMAX As New DataGridViewCheckBoxColumn()
            dgvCheckMAX.HeaderText = "MAX()"
            dgvCheckMAX.Name = "MAX"
            'IndexCol = dgvFieldSelection.Columns.Add
            'IndexCol.ColumnName = "#"
            'IndexCol.Namespace = "#"
            'IndexCol.SetOrdinal(0)

            If DataSetHeaderList.DBVersion = "IBM" Then
                dt = myDAL.GetColumns(GlobalSession.ConnectString, DataSetID)
            Else
                dt = myDAL.GetColumnsMYSQL(DataSetID)
            End If
            cbAudioClick.Checked = False
            dgvFieldSelection.DataSource = Nothing
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    dgvFieldSelection.DataSource = dt
                    PopulateAttributes(dt)
                    'Add fields to where combo here:
                    cboWhereFields.ValueMember = "Column Name"
                    cboWhereFields.DisplayMember = "Column Text"
                    cboWhereFields.DataSource = dt
                    cboWhereFields.Text = ""
                    cboOperators.ValueMember = "="
                    BuildOperatorCombo()
                    'dgvFieldSelection.Columns.Add(dgvCheck)
                    dgvFieldSelection.Columns.Add(dgvCheckSum)
                    dgvFieldSelection.Columns.Add(dgvCheckMIN)
                    dgvFieldSelection.Columns.Add(dgvCheckMAX)

                    dgvFieldSelection.Columns("Column Name").Visible = False
                    dgvFieldSelection.Columns("Column Name").HeaderText = "Field"
                    dgvFieldSelection.Columns("Column Name").ReadOnly = True
                    'dgvFieldSelection.Columns("Column Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvFieldSelection.Columns("Column Text").HeaderText = "Text"
                    dgvFieldSelection.Columns("Column Text").ReadOnly = True
                    'dgvFieldSelection.Columns("Column Text").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvFieldSelection.Columns("Column Type").HeaderText = "Type"
                    dgvFieldSelection.Columns("Column Type").ReadOnly = True
                    'dgvFieldSelection.Columns("Column Type").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvFieldSelection.Columns("Column Length").HeaderText = "Length"
                    dgvFieldSelection.Columns("Column Length").ReadOnly = True
                    'dgvFieldSelection.Columns("Column Length").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvFieldSelection.Columns("Column Decimals").HeaderText = "Decimals"
                    dgvFieldSelection.Columns("Column Decimals").ReadOnly = True
                    'dgvFieldSelection.Columns("Column Decimals").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Populate Error: " & ex.Message)
        End Try
        Cursor = Cursors.Default
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

    Function GetSelectedGridRows() As List(Of Integer)
        Dim tempRowsSelected As New List(Of Integer)
        Dim RowsSelected As New List(Of Integer)

        Try
            For i As Integer = 0 To dgvFieldSelection.SelectedCells.Count - 1
                If Not tempRowsSelected.Contains(CInt(dgvFieldSelection.SelectedCells(i).RowIndex)) Then
                    tempRowsSelected.Add(dgvFieldSelection.SelectedCells(i).RowIndex)
                End If
            Next
            For i As Integer = tempRowsSelected.Count - 1 To 0 Step -1
                RowsSelected.Add(tempRowsSelected.Item(i))
            Next

        Catch ex As Exception
            MsgBox("Error during get selection of rows: " & ex.Message)
        End Try

        GetSelectedGridRows = RowsSelected
    End Function

    Sub PopulateAttributes(dt As DataTable)
        Dim strFieldname As String
        Dim strFieldText As String
        Dim strFieldType As String
        Dim strFieldLength As String
        Dim strDecimals As String
        Dim strAttributes As String
        Dim intSUM As Integer
        Dim intMIN As Integer
        Dim intMAX As Integer
        Dim tempAttribute As clsAttributeProperties

        'Applies to ALL rows in the grid:
        For i As Integer = 0 To dt.Rows.Count - 1
            strFieldname = dt.Rows(i)("Column Name")
            strFieldText = dt.Rows(i)("Column Text")
            strFieldType = dt.Rows(i)("Column Type")
            If Not IsDBNull(dt.Rows(i)("Column Length")) Then
                strFieldLength = dt.Rows(i)("Column Length")
            Else
                strFieldLength = "0"
            End If
            If Not IsDBNull(dt.Rows(i)("Column Decimals")) Then
                strDecimals = dt.Rows(i)("Column Decimals")
            Else
                strDecimals = "0"
            End If

            intSUM = 0
            tempAttribute = New clsAttributeProperties
            tempAttribute.FieldPos = i + 1
            tempAttribute.SelectedFieldPos = 0
            tempAttribute.SelectedFieldname = strFieldname
            tempAttribute.SelectedFieldText = strFieldText
            tempAttribute.SelectedFieldType = strFieldType
            tempAttribute.SelectedFieldLength = CInt(strFieldLength)
            tempAttribute.SelectedDecimals = CInt(strDecimals)
            tempAttribute.IsSUM = False
            tempAttribute.IsMIN = False
            tempAttribute.IsMAX = False
            tempAttribute.IsSelected = False
            tempAttribute.Attributes = strFieldType & ";" & strFieldLength & ";" & strDecimals & ";" & CStr(intSUM) & ";" & CStr(intMIN) & ";" & CStr(intMAX)

            'If Not FieldAttributes.FindAttributeFieldName(strFieldname) Then
            If Not FieldAttributes.Dic_Attributes.exists(strFieldname) Then
                FieldAttributes.Dic_Attributes(strFieldname) = tempAttribute
            End If

            If Not FieldAttributes.Dic_FieldAlias.exists(strFieldname) Then
                FieldAttributes.Dic_FieldAlias(strFieldname) = strFieldText
            End If
            If Not FieldAttributes.Dic_Types.exists(strFieldname) Then
                FieldAttributes.Dic_Types(strFieldname) = strFieldType
                FieldAttributes.Dic_Types(strFieldText) = strFieldType
            End If
        Next

    End Sub

    Sub TestGetAttributes()
        Dim FieldPos As Integer
        Dim Attributes As String
        Dim Fieldname As String
        Dim FieldText As String
        Dim FieldType As String
        Dim FieldLength As String
        Dim strFieldDecimals As String
        Dim IsSUM As String
        Dim IsMIN As String
        Dim IsMAX As String
        Dim Output As String
        Dim lstSelected As New List(Of String)

        If FieldAttributes.CountSelectedFields > 0 Then
            Output = ""
            lstSelected = FieldAttributes.GetALLSelectedFields()
            For i As Integer = 0 To lstSelected.Count - 1
                Fieldname = lstSelected.Item(i)
                FieldText = ColumnSelect.FieldAttributes.GetSelectedFieldText(Fieldname)
                FieldPos = ColumnSelect.FieldAttributes.GetSelectedFieldPosition(Fieldname)
                Attributes = ColumnSelect.FieldAttributes.GetSelectedAttributes(Fieldname)
                FieldLength = CStr(ColumnSelect.FieldAttributes.GetSelectedFieldLength(Fieldname))
                FieldType = ColumnSelect.FieldAttributes.GetSelectedFieldType(Fieldname)
                strFieldDecimals = CStr(ColumnSelect.FieldAttributes.GetSelectedFieldDecimals(Fieldname))
                If ColumnSelect.FieldAttributes.GetSelectedFieldSUM(Fieldname) Then
                    IsSUM = "SUM"
                Else
                    IsSUM = "NO SUM"
                End If
                If ColumnSelect.FieldAttributes.GetSelectedFieldSUM(Fieldname) Then
                    IsMIN = "MIN"
                Else
                    IsMIN = "NO MIN"
                End If
                If ColumnSelect.FieldAttributes.GetSelectedFieldSUM(Fieldname) Then
                    IsMAX = "MAX"
                Else
                    IsMAX = "NO MAX"
                End If
                Output += CStr(FieldPos) & ") " & Fieldname & " " & Attributes & ": " & FieldText & " : " & IsSUM & ", " & IsMIN & ", " & IsMAX
                Output += vbCrLf
            Next
            If FieldAttributes.ErrMessage <> "" Then
                MsgBox(FieldAttributes.ErrMessage)
            Else
                MsgBox(Output)
            End If

        Else
            MsgBox("No Fields Selected")
        End If

    End Sub

    Sub SetControls(PosType As String, Fieldname As String)
        Dim myDAL As New SQLBuilderDAL
        Dim myTypes As String
        Dim myFieldnames As String

        If Fieldname <> "" Then
            'myDAL.GetMySQLFieldsAndTypes(DBTable, myTypes, dic_Types, myFieldnames)
        End If
        'if type = datetime then make the datepickers visible.
        stsQueryBuilderLabel1.Text = "FIELD SELECTED: " & Fieldname & " TYPE: " & FieldAttributes.Dic_Types(Fieldname)
        If UCase(PosType) = "SINGLE" Then
            lblValue.Visible = True
            txtValue.Visible = True
            If FieldAttributes.Dic_Types(Fieldname) = "L" Then
                dtp1.Visible = True
                dtp2.Visible = False
            Else
                dtp1.Visible = False
                dtp2.Visible = False
            End If

            lblValue2.Visible = False
            txtValue2.Visible = False
            txtINvalues.Visible = False

        ElseIf UCase(PosType) = "IN" Then
            lblValue.Visible = True
            txtValue.Visible = True
            If FieldAttributes.Dic_Types(Fieldname) = "L" Then
                dtp1.Visible = True
                dtp2.Visible = False
            Else
                dtp1.Visible = False
                dtp2.Visible = False
            End If

            lblValue2.Visible = False
            txtValue2.Visible = False
            txtINvalues.Visible = True

        ElseIf UCase(PosType) = "BETWEEN" Then
            lblValue.Visible = True
            txtValue.Visible = True
            If FieldAttributes.Dic_Types(Fieldname) = "L" Then
                dtp1.Visible = True
                dtp2.Visible = True
            Else
                dtp1.Visible = False
                dtp2.Visible = False
            End If

            lblValue2.Visible = True
            txtValue2.Visible = True
            txtINvalues.Visible = False
        End If
    End Sub

    Private Sub cboOperators_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOperators.SelectedIndexChanged
        Dim IDX As Integer
        Dim ItemName As String

        IDX = cboOperators.SelectedIndex
        If IDX > -1 Then
            ItemName = cboOperators.SelectedValue.ToString()

            If ItemName <> "" Then
                txtOperator.Text = ItemName
                SetControls("SINGLE", cboWhereFields.Text)
            End If
            lblMessage.Text = ""
            lblMessage.Visible = False
            If ItemName = "LIKE" Or ItemName = "NOT LIKE" Then
                SetControls("SINGLE", cboWhereFields.Text)
            End If
            If ItemName = "BETWEEN" Or ItemName = "NOT BETWEEN" Then
                SetControls("BETWEEN", cboWhereFields.Text)
            End If
            If ItemName = "IN" Or ItemName = "NOT IN" Then
                SetControls("IN", cboWhereFields.Text)
            End If
            txtValue.Text = ""
            txtValue2.Text = ""
        End If
    End Sub

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
        Dim FieldType As String
        Dim Quote As String
        Dim ExcludeUPPER As Boolean

        strValue = ""
        AddCondition = ""
        strCondition = ""
        intSelectedFieldIDX = cboWhereFields.SelectedIndex
        If intSelectedFieldIDX = -1 Then
            MsgBox("Please select a field first")
            Exit Function
        End If
        If cboWhereFields.Items.Count > 0 Then
            'grab selected field:
            strWhereField1 = cboWhereFields.SelectedValue
        Else
            MsgBox("Dont forget to press the Add Field button.")
            Exit Function
        End If
        ExcludeUPPER = False
        strOperator = ""
        strJoin = ""
        If strWhereField1 <> "" Then
            FieldType = FieldAttributes.Dic_Types(strWhereField1)
            If FieldType = "A" Or FieldType = "L" Then
                Quote = "'"
            Else
                Quote = ""
            End If
            If FieldType = "N" Or FieldType = "L" Then
                ExcludeUPPER = True
            End If
            If txtOperator.Text <> "" Then
                strOperator = txtOperator.Text
                strValue = Quote & txtValue.Text & Quote
                If UCase(strOperator) = "BETWEEN" Or UCase(strOperator) = "NOT BETWEEN" Then
                    'two dates in both text boxes:
                    ExcludeUPPER = True
                    If txtValue.Text <> "" And txtValue2.Text <> "" Then
                        If IsDate(txtValue.Text) Then
                            If IsDate(txtValue2.Text) Then
                                strValue = Quote & CDate(txtValue.Text).ToString("yyyy-MM-dd") & Quote & " AND " & Quote & CDate(txtValue2.Text).ToString("yyyy-MM-dd") & Quote
                            End If
                        Else
                            strValue = Quote & txtValue.Text & Quote & " AND " & Quote & txtValue2.Text & Quote
                        End If
                    Else
                        MsgBox("Please enter values in both boxes")
                        Exit Function
                    End If
                End If
                If UCase(strOperator) = "IN" Or UCase(strOperator) = "NOT IN" Then
                    ExcludeUPPER = True
                    If txtINvalues.Text <> "" Then
                        strValue = "("
                        For l As Integer = 0 To txtINvalues.Lines.Count - 1
                            If l = 0 Then
                                strValue += Quote & txtINvalues.Lines(l) & Quote
                            ElseIf txtINvalues.Lines(l) <> "" Then
                                strValue += "," & Quote & txtINvalues.Lines(l) & Quote
                            End If
                        Next
                        strValue += ")"
                    End If
                End If
                If UCase(strOperator) = "BEGINS" Then
                    strOperator = "LIKE "
                    strValue = "'" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
                End If
                If UCase(strOperator) = "NOT BEGINS" Then
                    strOperator = "NOT LIKE "
                    strValue = "'" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
                End If
                If UCase(strOperator) = "LIKE" Then
                    strOperator = "LIKE "
                    strValue = "'%" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
                End If
                If UCase(strOperator) = "NOT LIKE" Then
                    strOperator = "NOT LIKE "
                    strValue = "'%" & txtValue.Text & "%'"
                    ColumnSelect.myWhereConditions.LastOperator = strOperator
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

                If txtValue.Text = "" And strValue = "" Then
                    MsgBox("Please enter a value")
                    Exit Function
                End If
                If cbIgnoreCase.Checked And ExcludeUPPER = False Then
                    strCondition += "UPPER(" & (strWhereField1) & ") " & strOperator & " UPPER(" & strValue & ")"
                Else
                    strCondition += strWhereField1 & " " & strOperator & " " & strValue
                End If

                If Not ColumnSelect.myWhereConditions.IsInList(strCondition) Then
                    ColumnSelect.myWhereConditions.lbConditions.Add(strJoin & strCondition)
                Else
                    MsgBox("Condition Already in List")
                    Exit Function
                End If
                UpdateInternalConditionList()
                WhereConditions += strJoin & strCondition & vbCrLf

                Return strCondition
            Else
                MsgBox("No operator or value entered.")
                Exit Function
            End If
        End If
    End Function

    Sub UpdateInternalConditionList()
        Dim Condition As String
        Dim AndPos As Integer
        Dim OrPos As Integer
        Dim FirstPart As String

        If ColumnSelect.myWhereConditions.CountConditions > 0 Then
            ColumnSelect.myWhereConditions.DeleteConditions()
            lstConditions.Items.Clear()
            'what if the first condition is deleted - this leaves the others with AND in front.
            For i As Integer = 0 To ColumnSelect.myWhereConditions.lbConditions.Count - 1
                Condition = ColumnSelect.myWhereConditions.lbConditions.Item(i)
                If Len(Condition) > 5 And i = 0 Then
                    FirstPart = Mid(Condition, 1, 5)
                    If InStr(UCase(FirstPart), "AND ") > 0 Then
                        Condition = Replace(Condition, "AND", "", 1, 1, CompareMethod.Text)
                    End If
                    If InStr(UCase(FirstPart), "OR ") > 0 Then
                        Condition = Replace(Condition, "OR", "", 1, 1, CompareMethod.Text)
                    End If
                    ColumnSelect.myWhereConditions.lbConditions.Item(i) = Condition
                End If
                ColumnSelect.myWhereConditions.GetMyWhereCondtions += Condition
                lstConditions.Items.Add(Condition)
            Next
        Else
            'MsgBox("No conditions are entered")
        End If
    End Sub

    Private Sub btnRemoveCondition_Click(sender As Object, e As EventArgs)
        Dim lstIDX As Integer
        Dim ItemName As String

        lstIDX = lstConditions.SelectedIndex
        If lstIDX > -1 Then
            ItemName = lstConditions.Items(lstIDX)
            lstConditions.Items.RemoveAt(lstIDX)
            ColumnSelect.myWhereConditions.lbConditions.Remove(ItemName)
            UpdateInternalConditionList()
        End If
    End Sub

    Private Sub UndockChild()
        If Me.MdiParent Is Nothing Then
            Me.MdiParent = FromHandle(GlobalSession.MDIParentHandle)
        Else
            Me.MdiParent = Nothing
        End If
    End Sub

    Private Sub SQLBuilder_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyValue = Keys.F5 Then
            'btnRefresh.PerformClick()
        ElseIf e.KeyValue = 27 Then 'ESC pressed
            'Clear certain fields
        ElseIf e.KeyValue = Keys.F7 Then
            UndockChild()
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        'NEed to reset alll the fields in object FieldAttributes .Selected and .SUM back to false.
        lstFields.Items.Clear()
        FieldAttributes.ClearSelectedAttributesList()
        FieldAttributes.ResetAllSelectedFields()
        chklstOrderBY.Items.Clear()
        myWhereConditions.DeleteConditions()
        ColumnSelect.myWhereConditions.lbConditions.Clear()
        'cboWhereFields.Items.Clear()
        cboWhereFields.Text = ""
        txtValue.Text = ""
        txtValue2.Text = ""
        cboOperators.SelectedIndex = cboOperators.FindString("Equals")
        lstConditions.Items.Clear()
        btnSelectAll.Text = "Select All"
        stsQueryBuilderLabel1.Text = ""
        stsQueryBuilderLabel2.Text = ""
        'Me.Height = 584

    End Sub

    Function IsInList(lstBox As ListBox, CheckItem As String, ByRef ReturnIDX As Integer) As Boolean
        Dim ItemName As String

        IsInList = False
        ReturnIDX = -1
        For i As Integer = 0 To lstBox.Items.Count - 1
            ItemName = lstBox.Items(i)
            If UCase(CheckItem) = UCase(ItemName) Then
                ReturnIDX = i
                Return True
                Exit For
            End If
        Next

    End Function

    Function IsInCombo(cbo As ComboBox, CheckItem As String, ByRef ReturnIDX As Integer) As Boolean
        Dim ItemName As String

        IsInCombo = False
        ReturnIDX = -1
        If cbo.Items.Contains(CheckItem) Then
            ReturnIDX = cbo.SelectedIndex
            Return True
        End If

    End Function

    Function IsInCHKList(lstBox As CheckedListBox, CheckItem As String, ByRef ReturnIDX As Integer) As Boolean
        Dim ItemName As String

        IsInCHKList = False
        ReturnIDX = -1
        For i As Integer = 0 To lstBox.Items.Count - 1
            ItemName = lstBox.Items(i)
            If UCase(CheckItem) = UCase(ItemName) Then
                ReturnIDX = i
                Return True
                Exit For
            End If
        Next

    End Function

    Sub TestCheckboxCondition(ColumnName As String, ItemIDX As Integer, CheckboxType As String, strItem As String, ItemIsTicked As Boolean, ByRef ItemSelected As Integer)

        If InStr(CheckboxType, "SUM") > 0 Then
            FieldAttributes.ChangeFieldnameAttribute_IsSUM(ColumnName, ItemIsTicked)
        End If
        If InStr(CheckboxType, "MIN") > 0 Then
            FieldAttributes.ChangeFieldnameAttribute_IsMIN(ColumnName, ItemIsTicked)
        End If
        If InStr(CheckboxType, "MAX") > 0 Then
            FieldAttributes.ChangeFieldnameAttribute_IsMAX(ColumnName, ItemIsTicked)
        End If
        If ItemIsTicked = True Then 'ITEM SELECTED, if ticked - insert function in list, remove normal field and replace with function
            ItemSelected = 1

            If IsInList(lstFields, ColumnName, ItemIDX) Then
                lstFields.Items.RemoveAt(ItemIDX)
                lstFields.Items.Insert(ItemIDX, strItem)
                FieldAttributes.ChangeSelectedFieldnameAttribute_Position(ColumnName, ItemIDX + 1)
            End If
            'Insert fieldname with relevant function() around it:

            If Not IsInList(lstFields, strItem, ItemIDX) Then
                lstFields.Items.Add(strItem)
                FieldAttributes.ChangeSelectedFieldnameAttribute_Position(ColumnName, ItemIDX + 1)
            End If
        ElseIf ItemIsTicked = False Then 'ITEM NOT SELECTED, if un-ticked - remove function from list and replace with normal field:
            ItemSelected = 0
            If IsInList(lstFields, strItem, ItemIDX) Then
                lstFields.Items.RemoveAt(ItemIDX)
                If FieldAttributes.GetSelectedFieldSUM(ColumnName) = False And
                    FieldAttributes.GetSelectedFieldMIN(ColumnName) = False And
                    FieldAttributes.GetSelectedFieldMAX(ColumnName) = False Then
                    lstFields.Items.Insert(ItemIDX, ColumnName)
                    FieldAttributes.ChangeSelectedFieldnameAttribute_Position(ColumnName, ItemIDX + 1)
                End If
            End If
        End If
    End Sub

    Sub SelectFields2(SelectedList As String)
        Dim ColumnName As String
        Dim ColumnText As String
        Dim ColumnType As String
        Dim strColumnLength As String
        Dim strDecimals As String
        Dim SumSelected As Integer
        Dim MinSelected As Integer
        Dim MaxSelected As Integer
        Dim SumItem As String
        Dim MINItem As String
        Dim MAXItem As String
        Dim ItemIDX As Integer
        Dim IsSUM As Boolean
        Dim IsMIN As Boolean
        Dim IsMAX As Boolean
        Dim RowsSelected As New List(Of Integer)

        Try
            RowsSelected = GetSelectedGridRows() 'Grab NEW rows selected in grid...and puts into correct order.
            For i As Integer = 0 To RowsSelected.Count - 1
                ColumnName = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("Column Name").Value
                ColumnText = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("Column Text").Value
                ColumnType = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("Column Type").Value
                If Not IsDBNull(dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("Column Length").Value) Then
                    strColumnLength = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("Column Length").Value
                Else
                    strColumnLength = "0"
                End If
                If Not IsDBNull(dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("Column Decimals").Value) Then
                    strDecimals = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("Column Decimals").Value
                Else
                    strDecimals = "0"
                End If
                IsSUM = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("SUM").Value
                IsMIN = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("MIN").Value
                IsMAX = dgvFieldSelection.Rows(RowsSelected.Item(i)).Cells("MAX").Value
                SumSelected = 0
                MinSelected = 0
                MaxSelected = 0
                SumItem = "SUM(" & ColumnName & ")"
                MINItem = "MIN(" & ColumnName & ")"
                MAXItem = "MAX(" & ColumnName & ")"
                Select Case UCase(SelectedList)
                    Case "SELECT FIELDS"
                        If Not IsInList(lstFields, ColumnName, ItemIDX) And Not IsInList(lstFields, SumItem, ItemIDX) _
                            And Not IsInList(lstFields, MINItem, ItemIDX) And Not IsInList(lstFields, MAXItem, ItemIDX) Then
                            lstFields.Items.Add(ColumnName)
                            'If Not FieldAttributes.IsInList(ColumnName) Then
                            'FieldAttributes.SelectedFields.Add(ColumnName)
                            'End If
                            FieldAttributes.ChangeSelectedFieldnameAttribute_Position(ColumnName, i + 1)
                            FieldAttributes.ChangeFieldnameAttribute_IsSelected(ColumnName, True)
                        End If
                    Case "WHERE FIELDS"
                        If Not IsInCombo(cboWhereFields, ColumnText, ItemIDX) Then
                            cboWhereFields.Items.Add(ColumnText)
                            BuildOperatorCombo()
                        End If
                    Case "GROUPBY FIELDS"

                    Case "ORDERBY FIELDS"
                        If IsInList(lstFields, ColumnName, ItemIDX) Then
                            If Not IsInCHKList(chklstOrderBY, ColumnName, ItemIDX) And
                                FieldAttributes.GetSelectedFieldSUM(ColumnName) = False And
                                FieldAttributes.GetSelectedFieldMIN(ColumnName) = False And
                                FieldAttributes.GetSelectedFieldMAX(ColumnName) = False Then
                                chklstOrderBY.Items.Add(ColumnName)
                            End If
                        Else
                            If FieldAttributes.GetSelectedFieldSUM(ColumnName) = True Or
                                FieldAttributes.GetSelectedFieldMIN(ColumnName) = True Or
                                FieldAttributes.GetSelectedFieldMAX(ColumnName) = True Then
                                MsgBox("Cannot insert aggregate functions")
                                Exit For
                            End If
                            MsgBox("Field must be in the Display List first")
                        End If

                    Case Else
                        MsgBox("List not recognised")
                        Exit Sub
                End Select
                TestCheckboxCondition(ColumnName, ItemIDX, "SUM", SumItem, IsSUM, SumSelected)
                TestCheckboxCondition(ColumnName, ItemIDX, "MIN", MINItem, IsMIN, MinSelected)
                TestCheckboxCondition(ColumnName, ItemIDX, "MAX", MAXItem, IsMAX, MaxSelected)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSelectFields_Click(sender As Object, e As EventArgs) Handles btnSelectFields.Click
        SelectFields2("SELECT FIELDS")
    End Sub

    Private Sub btnSelectOrderBy_Click(sender As Object, e As EventArgs) Handles btnSelectOrderBy.Click
        SelectFields2("ORDERBY FIELDS")
    End Sub

    Sub MoveItemUp(ByRef lstBox As ListBox)
        Dim CurrentIDX As Integer
        Dim strCurrentItem As String
        Dim Second As String
        Dim First As String
        Dim NewIDX As Integer
        Dim ColumnText As String
        Dim NewPos As Integer
        Dim OldPos As Integer

        CurrentIDX = lstBox.SelectedIndex

        If CurrentIDX > 0 Then
            strCurrentItem = lstBox.Items(CurrentIDX)
            First = lstBox.Items(CurrentIDX)
            OldPos = CurrentIDX
            NewIDX = CurrentIDX - 1
            NewPos = NewIDX
            Second = lstBox.Items(NewIDX)
            FieldAttributes.ChangeSelectedFieldnameAttribute_Position(First, NewPos + 1)
            FieldAttributes.ChangeSelectedFieldnameAttribute_Position(Second, OldPos + 1)
            lstBox.Items.RemoveAt(CurrentIDX)
            lstBox.Items.Insert(NewIDX, strCurrentItem)
            lstBox.SelectedIndex = NewIDX
            'OldPos = FieldAttributes.GetSelectedFieldPosition(strCurrentItem)

        End If

    End Sub

    Sub MoveItemDown(ByRef lstBox As ListBox)
        Dim CurrentIDX As Integer
        Dim strCurrentItem As String
        Dim NewIDX As Integer
        Dim ColumnText As String
        Dim NewPos As Integer
        Dim OldPos As Integer
        Dim First As String
        Dim Second As String
        Dim temp As String

        CurrentIDX = lstBox.SelectedIndex
        OldPos = lstBox.SelectedIndex
        'Dont forget -> zero-based indexes.
        If CurrentIDX < lstBox.Items.Count - 1 And CurrentIDX > -1 Then
            strCurrentItem = lstBox.Items(CurrentIDX)
            First = strCurrentItem
            NewIDX = CurrentIDX + 1
            NewPos = NewIDX
            Second = lstBox.Items(NewIDX)
            lstBox.Items.RemoveAt(CurrentIDX)
            lstBox.Items.Insert(NewIDX, strCurrentItem)
            lstBox.SelectedIndex = NewIDX

            FieldAttributes.ChangeSelectedFieldnameAttribute_Position(First, NewPos + 1)
            FieldAttributes.ChangeSelectedFieldnameAttribute_Position(Second, OldPos + 1)
        End If
    End Sub

    Sub MoveCheckedListItemUp(chklstBox As CheckedListBox)
        Dim CurrentIDX As Integer
        Dim strCurrentItem As String
        Dim NewIDX As Integer
        Dim IsSelected As Boolean

        CurrentIDX = chklstBox.SelectedIndex

        If CurrentIDX > 0 Then
            IsSelected = chklstBox.GetItemChecked(CurrentIDX)
            strCurrentItem = chklstBox.Items(CurrentIDX)
            NewIDX = CurrentIDX - 1
            chklstBox.Items.RemoveAt(CurrentIDX)
            chklstBox.Items.Insert(NewIDX, strCurrentItem)
            chklstBox.SelectedIndex = NewIDX
            chklstBox.SetItemChecked(NewIDX, IsSelected)
        End If

    End Sub

    Sub MoveCheckedListItemDown(chklstBox As CheckedListBox)
        Dim CurrentIDX As Integer
        Dim strCurrentItem As String
        Dim NewIDX As Integer
        Dim IsSelected As Boolean

        CurrentIDX = chklstBox.SelectedIndex

        If CurrentIDX < chklstBox.Items.Count - 1 And CurrentIDX > -1 Then
            IsSelected = chklstBox.GetItemChecked(CurrentIDX)
            strCurrentItem = chklstBox.Items(CurrentIDX)
            NewIDX = CurrentIDX + 1
            chklstBox.Items.RemoveAt(CurrentIDX)
            chklstBox.Items.Insert(NewIDX, strCurrentItem)
            chklstBox.SelectedIndex = NewIDX
            chklstBox.SetItemChecked(NewIDX, IsSelected)
        End If
    End Sub

    Function BuildQueryFromSelection() As String
        Dim ColumnName As String
        Dim NewColumnName As String
        Dim ColumnText As String
        Dim FieldAlias As String
        Dim TableName As String
        Dim Sequence As String
        Dim FieldID As String
        Dim FieldsSelected As String
        Dim SelectPart As String
        Dim GroupByFields As String
        Dim OrderByFields As String
        Dim IsChecked As Boolean
        Dim FinalQuery As String
        Dim IncludeGroupBy As Boolean = False
        Dim tempAttribute As clsAttributeProperties
        Dim lstSelected As New List(Of String)

        BuildQueryFromSelection = ""
        SelectPart = "SELECT "
        FieldsSelected = ""
        OrderByFields = ""
        TableName = txtTablename.Text

        If lstFields.Items.Count = 0 Then
            'MsgBox("No Fields were selected")
            Exit Function
        End If

        lstSelected = FieldAttributes.GetALLSelectedFields 'SORT THE FIELD IN POSITION ORDER
        For i As Integer = 0 To lstSelected.Count - 1
            tempAttribute = New clsAttributeProperties 'THis is VERY important !
            ColumnName = lstSelected.Item(i) 'THIS CHOOSES THE SELECTED FIELD IN THE LIST.
            NewColumnName = ""
            ColumnText = ""
            FieldAlias = ""
            tempAttribute = FieldAttributes.Dic_Attributes(ColumnName) 'EXTRACT THE MATCHING SELECTED FIELD FROM OBJECT.
            If Not IsNothing(tempAttribute) Then
                ColumnText = tempAttribute.SelectedFieldText
                FieldAlias = " AS """ & ColumnText & """"

                If tempAttribute.IsSUM Then
                    NewColumnName += " SUM(" & ColumnName & ")"
                    NewColumnName += " AS """ & "SUM(" & ColumnText & ")" & """"
                    IncludeGroupBy = True
                End If
                If tempAttribute.IsMIN Then
                    If NewColumnName <> "" Then
                        NewColumnName += ","
                    End If
                    NewColumnName += " MIN(" & ColumnName & ")"
                    NewColumnName += " AS """ & "MIN(" & ColumnText & ")" & """"
                    IncludeGroupBy = True
                End If
                If tempAttribute.IsMAX Then
                    If NewColumnName <> "" Then
                        NewColumnName += ","
                    End If
                    NewColumnName += " MAX(" & ColumnName & ")"
                    NewColumnName += " AS """ & "MAX(" & ColumnText & ")" & """"
                    IncludeGroupBy = True
                End If
            End If
            If NewColumnName = "" Then
                NewColumnName = ColumnName & FieldAlias
            End If
            If FieldsSelected = "" Then
                FieldsSelected += NewColumnName
            Else
                FieldsSelected += "," & NewColumnName
            End If
        Next
        If IncludeGroupBy Then
            GroupByFields = ""
            For i As Integer = 0 To lstSelected.Count - 1
                ColumnName = lstSelected.Item(i)
                If Not FieldAttributes.GetSelectedFieldSUM(ColumnName) And
                   Not FieldAttributes.GetSelectedFieldMIN(ColumnName) And
                   Not FieldAttributes.GetSelectedFieldMAX(ColumnName) Then
                    If GroupByFields = "" Then
                        GroupByFields += ColumnName
                    Else
                        GroupByFields += "," & ColumnName
                    End If
                End If
            Next
        End If
        If chklstOrderBY.Items.Count > 0 Then
            For i As Integer = 0 To chklstOrderBY.Items.Count - 1
                ColumnName = chklstOrderBY.Items(i)
                IsChecked = chklstOrderBY.GetItemChecked(i)
                If OrderByFields = "" Then
                    OrderByFields += Trim(ColumnName)
                Else
                    OrderByFields += "," & Trim(ColumnName)
                End If
                If IsChecked Then
                    OrderByFields += " DESC"
                End If
            Next
        End If

        SelectPart += FieldsSelected & " FROM " & TableName
        If myWhereConditions.GetMyWhereCondtions <> "" Then
            SelectPart += " WHERE " & myWhereConditions.GetMyWhereCondtions
        End If
        'GROUPBY:
        If GroupByFields <> "" Then
            SelectPart += " GROUP BY " & GroupByFields
        End If
        'ORDERBY:
        If OrderByFields <> "" Then
            SelectPart += " ORDER BY " & OrderByFields
        End If
        FinalQuery = SelectPart
        Cursor = Cursors.Default
        Return FinalQuery

    End Function

    Private Sub btnSelectionToQuery_Click(sender As Object, e As EventArgs)
        '
        BuildQueryFromSelection()

    End Sub

    Private Sub SQLBuilder_ClientSizeChanged(sender As Object, e As EventArgs) Handles MyBase.ClientSizeChanged

    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        Dim Selected As Boolean = False

        If btnSelectAll.Text = "Select All" Then
            btnSelectAll.Text = "Unselect All"
            btnSelectAll.Refresh()
            Selected = True
        Else
            'If btnSelectAll.Text = "Unselect All" Then
            btnSelectAll.Text = "Select All"
            btnSelectAll.Refresh()

            Selected = False
        End If
        For i As Integer = 0 To dgvFieldSelection.Rows.Count - 1
            dgvFieldSelection.Rows(i).Cells("SelectField").Value = Selected
        Next
    End Sub

    Sub ShowQueryForm()
        Dim FinalQuery As String
        Dim App As New ViewSQL

        Cursor = Cursors.Default
        FinalQuery = BuildQueryFromSelection()
        If FinalQuery = "" Then
            Exit Sub
        End If
        stsQueryBuilderLabel1.Text = "Building Query ..."
        Cursor = Cursors.WaitCursor
        Refresh()

        App.Visible = False
        'App.GetParms(GlobalSession, GlobalParms)
        App.PopulateForm(FinalQuery)
        App.Show()
        'App.Visible = True
        stsQueryBuilderLabel1.Text = ""
        Cursor = Cursors.Default
    End Sub

    Private Sub SQLQueryToolStripMenuItem_Click(sender As Object, e As EventArgs)
        ShowQueryForm()

    End Sub

    Private Sub btnMoveOrderByFieldsDown_Click(sender As Object, e As EventArgs) Handles btnMoveOrderByFieldsDown.Click
        MoveCheckedListItemDown(chklstOrderBY)
    End Sub

    Private Sub btnMoveOrderByFieldsUp_Click(sender As Object, e As EventArgs) Handles btnMoveOrderByFieldsUp.Click
        MoveCheckedListItemUp(chklstOrderBY)
    End Sub

    Private Sub btnMoveSelectFieldsUP_Click(sender As Object, e As EventArgs) Handles btnMoveSelectFieldsUP.Click
        MoveItemUp(lstFields)
    End Sub

    Private Sub btnMoveSelectFieldsDOWN_Click(sender As Object, e As EventArgs) Handles btnMoveSelectFieldsDOWN.Click
        MoveItemDown(lstFields)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Close()

    End Sub

    Private Sub btnShowQuery_Click(sender As Object, e As EventArgs) Handles btnShowQuery.Click
        Dim Answer As Integer
        Dim recs As Long

        recs = GetRecordsGenerated()
        stsQueryBuilderLabel2.Text = "Total Records Generated: " & CStr(recs)
        If lstFields.Items.Count = 0 Then
            MsgBox("No Fields Selected")
            Exit Sub
        End If
        If myWhereConditions.CountConditions = 0 Then
            Answer = MsgBox("No where Conditions defined, this may Generate a large number of records. Are You Sure ?", vbYesNo)
            If Answer = vbNo Then
                Exit Sub
            End If
        End If
        ShowQueryForm()

    End Sub

    Function GetRecordsGenerated() As Long
        Dim recs As Long
        Dim dt As DataTable
        Dim FinalQuery As String

        GetRecordsGenerated = 0
        FinalQuery = BuildQueryFromSelection()
        If FinalQuery = "" Then
            Exit Function
        End If
        If DataSetHeaderList.DBVersion = "IBM" Then
            dt = ViewSQL.ExecuteSQLQuery(FinalQuery)
        Else
            dt = ViewSQL.ExecuteMySQLQuery(FinalQuery)
        End If
        recs = dt.Rows.Count
        Return recs
    End Function

    Private Sub lstFields_MouseDown(sender As Object, e As MouseEventArgs) Handles lstFields.MouseDown
        Dim IDX As Integer
        Dim src As String

        If e.Button = MouseButtons.Left Then
            IDX = lstFields.IndexFromPoint(e.X, e.Y)
            If IDX > -1 Then
                src = lstFields.Items(IDX).ToString()
            End If
            If src <> "" Then
                Dim objDragDropEff As DragDropEffects = DoDragDrop(src, DragDropEffects.Copy)
                If objDragDropEff = DragDropEffects.Copy Then
                    'perform action.
                End If
            End If

        End If
    End Sub

    Private Sub btnRemoveSelectedFields_Click(sender As Object, e As EventArgs) Handles btnRemoveSelectedFields.Click
        Dim IDX As Integer

        IDX = lstFields.SelectedIndex
        If IDX > -1 Then
            lstFields.Items.RemoveAt(IDX)
            'FieldAttributes.SelectedFields.RemoveAt(IDX)
        End If
    End Sub

    Private Sub btnRemoveOrderByFields_Click(sender As Object, e As EventArgs) Handles btnRemoveOrderByFields.Click
        Dim IDX As Integer

        IDX = chklstOrderBY.SelectedIndex
        If IDX > -1 Then
            chklstOrderBY.Items.RemoveAt(IDX)
        End If
    End Sub

    Private Sub dgvFieldSelection_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFieldSelection.CellClick

    End Sub

    Private Sub dgvFieldSelection_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFieldSelection.CellContentClick
        Dim RowIDX As Integer = e.RowIndex
        Dim ColIDX As Integer = e.ColumnIndex

        'This event is not as responsive but e.columnindex does return a value !
    End Sub

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()

    End Sub

    Private Sub lbSelectedWHEREFields_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim IDX As Integer

        IDX = cboWhereFields.SelectedIndex
        Me.WhereField = cboWhereFields.Items(IDX)
    End Sub

    Private Sub btnAddCondition_Click(sender As Object, e As EventArgs) Handles btnAddCondition.Click
        AddCondition()
    End Sub

    Private Sub dtp1_Leave(sender As Object, e As EventArgs) Handles dtp1.Leave
        txtValue.Text = dtp1.Text
    End Sub

    Private Sub dtp2_Leave(sender As Object, e As EventArgs) Handles dtp2.Leave
        txtValue2.Text = dtp2.Text
    End Sub

    Private Sub btnRemoveCondition_Click_1(sender As Object, e As EventArgs) Handles btnRemoveCondition.Click
        Dim lstIDX As Integer
        Dim ItemName As String

        lstIDX = lstConditions.SelectedIndex
        If lstIDX > -1 Then
            ItemName = lstConditions.Items(lstIDX)
            lstConditions.Items.RemoveAt(lstIDX)
            ColumnSelect.myWhereConditions.lbConditions.Remove(ItemName)
            UpdateInternalConditionList()
        End If

    End Sub

    Private Sub btnTestGetAttributes_Click(sender As Object, e As EventArgs) Handles btnTestGetAttributes.Click
        TestGetAttributes()

    End Sub

    Private Sub dgvFieldSelection_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvFieldSelection.MouseClick
        Dim hit As DataGridView.HitTestInfo = dgvFieldSelection.HitTest(e.X, e.Y)

        'dgvFieldSelection.ClearSelection()
        'Me.dgvFieldSelection.Rows(hit.RowIndex).Selected = True
    End Sub

    Private Sub cboWhereFields_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWhereFields.SelectedIndexChanged
        If cboWhereFields.Text <> "" Then
            If cboOperators.ValueMember = "=" Or
                cboOperators.ValueMember = ">" Or
                cboOperators.ValueMember = "<" Or
                cboOperators.ValueMember = ">=" Or
                cboOperators.ValueMember = "<=" Or
                cboOperators.ValueMember = "<>" Then
                SetControls("SINGLE", cboWhereFields.Text)
            End If
            If cboWhereFields.Text <> "" And cboOperators.Text = "BETWEEN" Then
                SetControls("BETWEEN", cboWhereFields.Text)
            End If
            If cboWhereFields.Text <> "" And cboOperators.Text = "IN" Then
                SetControls("IN", cboWhereFields.Text)
            End If
        End If

    End Sub

    Private Sub chklstOrderBY_DragEnter(sender As Object, e As DragEventArgs) Handles chklstOrderBY.DragEnter
        e.Effect = DragDropEffects.Copy
        'My.Computer.Audio.Play("C:\Users\PC\Documents\ESPO stuff\SQLBuilder31Dan\SQLBuilder\Media\suction.wav")
    End Sub

    Private Sub chklstOrderBY_DragDrop(sender As Object, e As DragEventArgs) Handles chklstOrderBY.DragDrop
        Dim str As String
        Dim ReturnIDX As Integer

        If Not IsNothing(e.Data.GetData(DataFormats.StringFormat)) Then
            str = CStr(e.Data.GetData(DataFormats.StringFormat)) 'defines type of data to get
            If Not IsInCHKList(chklstOrderBY, str, ReturnIDX) And
                FieldAttributes.GetSelectedFieldSUM(str) = False And
                FieldAttributes.GetSelectedFieldMIN(str) = False And
                FieldAttributes.GetSelectedFieldMAX(str) = False Then
                chklstOrderBY.Items.Add(str)
                If cbAudioClick.Checked = True Then
                    My.Computer.Audio.Play("C:\Users\PC\Documents\ESPO stuff\SQLBuilder36Dan\SQLBuilder\Media\click.wav")
                End If

            End If
        End If


    End Sub

    Private Sub cboWhereFields_TextChanged(sender As Object, e As EventArgs) Handles cboWhereFields.TextChanged
        cboWhereFields.SelectedIndex = cboWhereFields.FindString(cboWhereFields.Text)

    End Sub
End Class
