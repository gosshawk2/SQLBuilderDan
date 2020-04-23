Public Class DataSetHeaderList
    Dim GlobalParms As New ESPOParms.Framework
    Dim GlobalSession As New ESPOParms.Session
    Public Shared DBVersion As String
    Public Sub GetParms(Session As ESPOParms.Session, Parms As ESPOParms.Framework)
        GlobalParms = Parms
        GlobalSession = Session
    End Sub
    Private Sub DataSetHeaderList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        Me.KeyPreview = True
        Me.MdiParent = FromHandle(GlobalSession.MDIParentHandle)
        dgvHeaderList.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromWin32(RGB(245, 255, 245))
        dgvHeaderList.DefaultCellStyle.BackColor = ColorTranslator.FromWin32(RGB(235, 255, 235)) 'LIGHT GREEN
        dgvHeaderList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvHeaderList.AllowUserToOrderColumns = True
        dgvHeaderList.AllowUserToResizeColumns = True
        dgvHeaderList.AllowUserToAddRows = False
        dgvHeaderList.AllowUserToDeleteRows = False
        Me.Top = 1
        Me.Left = 1

        For Each c As Control In Controls
            AddHandler c.MouseClick, AddressOf ClickHandler
        Next

    End Sub

    Private Sub ClickHandler(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        'Label24.Text = String.Format("Clicked ""{0}"" with the {1} mouse button.", sender.name, e.Button.ToString.ToLower)
        Select Case e.Button
            Case MouseButtons.Left
                Me.BringToFront()
        End Select
    End Sub

    Sub PopulateForm()
        Cursor = Cursors.WaitCursor
        Dim myDAL As New SQLBuilderDAL
        Dim dt As DataTable
        Dim Tablename As String

        Me.Text = "SQL Builder Header List"
        Try
            dgvHeaderList.Columns.Clear()
            dgvHeaderList.DataSource = Nothing
            DBVersion = "MYSQL"
            'DBVersion = "IBM"
            If DBVersion = "IBM" Then
                dt = myDAL.GetHeaderList(GlobalSession.ConnectString)
            Else
                dt = myDAL.GetHeaderListMYSQL()
            End If

            If dt.Rows.Count > 0 Then
                dgvHeaderList.DataSource = dt
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Populate Error: " & ex.Message)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub dgvHeaderList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHeaderList.CellContentClick
        '
    End Sub

    Private Sub UndockChild()
        If Me.MdiParent Is Nothing Then
            Me.MdiParent = FromHandle(GlobalSession.MDIParentHandle)
        Else
            Me.MdiParent = Nothing
        End If
        'MsgBox("hey")
    End Sub

    Private Sub DataSetHeaderList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyValue = Keys.F5 Then
            'btnSearch.PerformClick()
        ElseIf e.KeyValue = 27 Then 'ESC key
            'PopulateForm()
        ElseIf e.KeyValue = Keys.F7 Then
            UndockChild()
        End If
    End Sub

    Private Sub SelectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectToolStripMenuItem.Click
        'Select next form and fill grid depending on row selected:
        Dim DataSetID As Integer
        Dim Tablename As String
        Dim DataSetName As String
        Dim App As New SQLBuilder.ColumnSelect

        Cursor = Cursors.Default

        'stsFW100Label1.Text = "Loading List......"
        Cursor = Cursors.WaitCursor
        Refresh()
        DataSetID = dgvHeaderList.CurrentRow.Cells("DataSetID").Value
        Tablename = dgvHeaderList.CurrentRow.Cells("Tablename").Value
        DataSetName = dgvHeaderList.CurrentRow.Cells("DataSet Name").Value

        App.Visible = False
        App.GetParms(GlobalSession, GlobalParms)
        App.PopulateForm(DataSetID)
        App.txtTablename.Text = Tablename
        App.txtDatasetName.Text = DataSetName
        App.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub dgvHeaderList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHeaderList.CellClick
        '
    End Sub

    Private Sub dgvHeaderList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHeaderList.CellDoubleClick
        'GET DataSetID and call SQLBuilder form:

    End Sub

    Private Sub dgvHeaderList_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvHeaderList.CellMouseClick
        Dim rowClicked As DataGridView.HitTestInfo = dgvHeaderList.HitTest(e.X, e.Y)
        'Select Right Clicked Row if its not the header row
        If e.Button = Windows.Forms.MouseButtons.Right AndAlso e.RowIndex > -1 Then
            'Clear any currently sellected rows
            'dgvHeaderList.ClearSelection()
            'Me.dgvHeaderList.Rows(e.RowIndex).Selected = True
            'If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            'Me.dgvHeaderList.CurrentCell = Me.dgvHeaderList.Rows(e.RowIndex).Cells(e.ColumnIndex)
            'End If
            'HeaderListCRUD.Show(Cursor.Position)
        End If
    End Sub

    Private Sub dgvHeaderList_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvHeaderList.MouseClick
        Dim hit As DataGridView.HitTestInfo = dgvHeaderList.HitTest(e.X, e.Y)

        If e.Button = Windows.Forms.MouseButtons.Right Then
            'Clear any currently sellected rows ?
            If hit.Type = DataGridViewHitTestType.Cell Then
                'dgvHeaderList.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                dgvHeaderList.ClearSelection()
                Me.dgvHeaderList.Rows(hit.RowIndex).Selected = True
                If hit.ColumnIndex >= 0 And hit.RowIndex >= 0 Then
                    Me.dgvHeaderList.CurrentCell = Me.dgvHeaderList.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                End If
            End If
            HeaderListCRUD.Show(Cursor.Position)
        End If
    End Sub
End Class