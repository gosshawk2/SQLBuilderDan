'**********************************************************************************************************
' DAL (Data Access Layer) Code Template
'**********************************************************************************************************
Imports System.Data.Odbc
Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class SQLBuilderDAL

    Function GetHeaderList(ConnectString As String) As DataTable
        Dim cn As New OdbcConnection(ConnectString)
        Dim SQLStatement As String

        GetHeaderList = Nothing
        SQLStatement = "SELECT " &
            "trim(DatasetName) as ""DataSet Name"", " &
            "trim(DataSetHeaderText) as ""DataSet Header Text"", " &
            "trim(Tablename) as ""Tablename"", " &
            "trim(AuthorityFlag) as ""Authority Flag"", " &
            "trim(CRTuserID) as ""CRT userID"", " &
            "CRTTIMESTAMP as ""CRT Timestamp"", " &
            "UPDUserID as ""UPD UserID"", " &
            "UPDTimestamp as ""UPD Timestamp"", " &
            "DatasetID " &
            "FROM ebi7020t " &
            "ORDER BY DatasetID"
        Try
            cn.Open()
            Dim cm As OdbcCommand = cn.CreateCommand 'Create a command object via the connection
            cm.CommandTimeout = 0
            cm.CommandType = CommandType.Text
            cm.CommandText = SQLStatement
            Dim da As New OdbcDataAdapter(cm)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds.Tables(0)
        Catch ex As Exception
            MsgBox("DB ERROR: " & ex.Message)
        End Try

    End Function

    Function GetColumns(ConnectString As String, DatasetID As Integer) As DataTable
        Dim cn As New OdbcConnection(ConnectString)
        Dim SQLStatement As String

        GetColumns = Nothing
        SQLStatement = "SELECT " &
            "trim(ColumnName) as ""Column Name"", " &
            "trim(ColumnText) as ""Column Text"", " &
            "trim(ColumnType) as ""Column Type"", " &
            "ColumnLength as ""Column Length""," &
            "ColumnDecimals as ""Column Decimals"" " &
            "FROM ebi7023t " &
            "Where DatasetID= " & DatasetID & " " &
            "ORDER BY Sequence"
        Try
            cn.Open()
            Dim cm As OdbcCommand = cn.CreateCommand 'Create a command object via the connection
            cm.CommandTimeout = 0
            cm.CommandType = CommandType.Text
            cm.CommandText = SQLStatement
            Dim da As New OdbcDataAdapter(cm)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds.Tables(0)
        Catch ex As Exception
            MsgBox("DB ERROR: " & ex.Message)
        End Try

    End Function

    Function GetColumnsMYSQL(DatasetID As Integer) As DataTable
        Dim ConnString As String
        Dim myDR As MySqlDataReader = Nothing
        Dim SQLStatement As String
        Dim ZeroDatetime As Boolean = True
        Dim Server As String = "localhost"
        Dim DbaseName As String = "simplequerybuilder"
        Dim USERNAME As String = "root"
        Dim password As String = "root"
        Dim port As String = "3306"

        GetColumnsMYSQL = Nothing
        Try
            'ConnString = setupMySQLconnection("localhost", "simplequerybuilder", "root", "root", "3306", ErrMessage)
            ConnString = String.Format("server={0}; user id={1}; password={2}; database={3}; Convert Zero Datetime={4}; port={5}; pooling=false", Server, USERNAME, password, DbaseName, ZeroDatetime, port)
            Dim cn As New MySqlConnection(ConnString)
            cn.Open()
            SQLStatement = "SELECT " &
            "trim(ColumnName) as ""Column Name"", " &
            "trim(ColumnText) as ""Column Text"", " &
            "trim(ColumnType) as ""Column Type"", " &
            "ColumnLength as ""Column Length""," &
            "ColumnDecimals as ""Column Decimals"" " &
            "FROM ebi7023t " &
            "Where DatasetID= " & DatasetID & " " &
            "ORDER BY Sequence"

            Dim cmd As New MySqlCommand
            cmd.Connection = cn
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.Text
            cmd.CommandText = SQLStatement
            Dim da As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds.Tables(0)
        Catch ex As Exception
            MsgBox("DB ERROR: " & ex.Message)
        End Try

    End Function

    Function GetHeaderListMYSQL() As DataTable
        Dim ConnString As String
        Dim SQLStatement As String
        Dim ZeroDatetime As Boolean = True
        Dim Server As String = "localhost"
        Dim DbaseName As String = "simplequerybuilder"
        Dim USERNAME As String = "root"
        Dim password As String = "root"
        Dim port As String = "3306"

        GetHeaderListMYSQL = Nothing
        Try
            'ConnString = setupMySQLconnection("localhost", "simplequerybuilder", "root", "root", "3306", ErrMessage)
            ConnString = String.Format("server={0}; user id={1}; password={2}; database={3}; Convert Zero Datetime={4}; port={5}; pooling=false", Server, USERNAME, password, DbaseName, ZeroDatetime, port)
            Dim cn As New MySqlConnection(ConnString)
            cn.Open()
            SQLStatement = "SELECT " &
            "trim(DatasetName) as ""DataSet Name"", " &
            "trim(DataSetHeaderText) as ""DataSet Header Text"", " &
            "trim(Tablename) as ""Tablename"", " &
            "trim(AuthorityFlag) as ""Authority Flag"", " &
            "trim(CRTuserID) as ""CRT userID"", " &
            "trim(CRTTIMESTAMP) as ""CRT Timestamp"", " &
            "trim(UPDUserID) as ""UPD UserID"", " &
            "trim(UPDTimestamp) as ""UPD Timestamp"", " &
            "DatasetID " &
            "FROM ebi7020t " &
            "ORDER BY DatasetID"

            Dim cmd As New MySqlCommand
            cmd.Connection = cn
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.Text
            cmd.CommandText = SQLStatement
            Dim da As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds.Tables(0)
        Catch ex As Exception
            MsgBox("DB ERROR: " & ex.Message)
        End Try

    End Function

    Sub GetMySQLFieldsAndTypes(ByVal DBTable As String,
                         ByRef MyTypes As String,
                         ByRef Dic_Types As Object,
                         ByRef Fieldnames As String)
        Dim con As MySqlConnection
        Dim cmd As MySqlCommand
        Dim strSQL As String
        Dim NumFields As Integer
        Dim colIDX As Integer = 0
        Dim dr1 As MySqlDataReader
        Dim FieldType As String
        Dim Fieldname As String
        Dim NewFieldnames As String
        Dim ConnString As String
        Dim SQLStatement As String
        Dim ZeroDatetime As Boolean = True
        Dim Server As String = "localhost"
        Dim DbaseName As String = "simplequerybuilder"
        Dim USERNAME As String = "root"
        Dim password As String = "root"
        Dim port As String = "3306"

        NewFieldnames = ""
        Fieldnames = ""
        MyTypes = ""
        NumFields = 0
        strSQL = ""
        Dic_Types = CreateObject("Scripting.Dictionary")
        Dic_Types.comparemode = vbTextCompare

        If Len(DBTable) = 0 Then
            MsgBox("DBError: No Table Specified")
            Exit Sub
        End If
        Try
            ConnString = String.Format("server={0}; user id={1}; password={2}; database={3}; Convert Zero Datetime={4}; port={5}; pooling=false", Server, USERNAME, password, DbaseName, ZeroDatetime, port)
            con = New MySqlConnection(ConnString)
            con.Open()
            strSQL = "SELECT * FROM " & DBTable
            cmd = New MySqlCommand(strSQL, con)
            dr1 = cmd.ExecuteReader()

            NumFields = dr1.FieldCount - 1
            While colIDX <= NumFields
                Fieldname = dr1.GetName(colIDX)
                FieldType = dr1.GetDataTypeName(colIDX)
                If Not Dic_Types.exists(Fieldname) Then
                    Dic_Types(Fieldname) = FieldType
                End If
                If Len(NewFieldnames) = 0 Then
                    NewFieldnames = Fieldname
                    MyTypes = FieldType.ToString
                Else
                    NewFieldnames = NewFieldnames & "," & Fieldname
                    MyTypes = MyTypes & "," & FieldType.ToString
                End If
                colIDX = colIDX + 1
            End While
            Fieldnames = NewFieldnames
            con.Close()
            dr1.Close()

        Catch ex As Exception
            MsgBox("DB Error In GetMySQLFieldsAndTypes: " & ex.Message.ToString)
        End Try

    End Sub

    Function Get_Total_Rows_From_CSVFile(CSVFilename As String, Optional ByRef TotalFields As Long = 0) As Long
        Dim rownum As Long
        Dim TotalRows As Long
        Dim wholeRows As String()

        Using csvReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(CSVFilename)
            csvReader.TextFieldType = FileIO.FieldType.Delimited
            csvReader.SetDelimiters(",")
            TotalRows = 0
            rownum = 0
            While Not csvReader.EndOfData
                Try
                    wholeRows = csvReader.ReadFields()

                    TotalFields = wholeRows.Length
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is NOT valid and will be skipped.")
                End Try
                rownum = rownum + 1
            End While

        End Using
        TotalRows = rownum
        Get_Total_Rows_From_CSVFile = TotalRows
    End Function

    Function CSVFileToArray(ByRef NewstrArray As String(,), ByVal CSVFilename As String, Optional ByRef TotalFields As Long = 0,
                                Optional ByRef Message As String = "") As Long
        Dim wholeRows As String()
        Dim numRowsRead As Long
        Dim colnum As Long
        Dim RowNum As Long
        Dim currentFields As String
        Dim Percentage As Long
        Dim TotalRows As Long
        Dim RowMessage As String
        Dim messages As String

        ReDim NewstrArray(1, 1)
        CSVFileToArray = 0
        numRowsRead = 0
        colnum = 0
        RowNum = 0
        messages = ""
        ReDim wholeRows(1)
        wholeRows(0) = ""
        Try
            Using csvReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(CSVFilename)
                csvReader.TextFieldType = FileIO.FieldType.Delimited
                csvReader.SetDelimiters(",")
                TotalRows = Get_Total_Rows_From_CSVFile(CSVFilename, TotalFields)
                ReDim NewstrArray(TotalFields, TotalRows)
                While Not csvReader.EndOfData
                    Try
                        wholeRows = csvReader.ReadFields()
                        colnum = 0
                        For Each currentFields In wholeRows
                            If TotalRows > 0 Then
                                Percentage = CLng((RowNum / TotalRows) * 100)
                            Else
                                Percentage = 0
                            End If
                            RowMessage = "Read " & CStr(RowNum) & " ROW / " & CStr(TotalRows) & " ROWS"
                            NewstrArray(colnum, RowNum) = currentFields

                            Application.DoEvents()

                            colnum = colnum + 1
                        Next
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        If Len(messages) > 0 Then
                            messages = messages & ","
                        End If
                        messages = messages & " Line " & ex.Message & "is NOT valid and will be skipped."
                        Message = messages
                        'logERR.LogMessage("NMS_Error_" & Me.GetVersion & ".log", Application.StartupPath, Message, "CSVFileToArray()", GetComputerName() & "," & GetIPv4Address() & "," & GetIPv6Address() & ", OPERATOR Logged in:" & Me.GetUsername)
                    End Try
                    RowNum = RowNum + 1
                End While
            End Using
        Catch ex As Exception
            Message = "Error In CSVFileToArray: " & ex.Message.ToString
            'logERR.LogError("NMS_Error_" & Me.GetVersion & ".log", Application.StartupPath, Message, "CSVFileToArray()", GetComputerName() & "," & GetIPv4Address() & "," & GetIPv6Address() & ", OPERATOR Logged in:" & Me.GetUsername)
        End Try
        numRowsRead = RowNum
        CSVFileToArray = numRowsRead
    End Function

    Sub CreateMySQLTable(DBName As String, Fieldnames As String, FieldTypes As String, FieldValues As String)

    End Sub

    Sub BulkLoaderMySQL(csvFilename As String, DBTable As String)
        Dim ConnString As String
        Dim con As MySqlConnection
        Dim cmd As MySqlCommand
        Dim strSQL As String
        Dim NumFields As Integer
        Dim colIDX As Integer = 0
        Dim dr1 As MySqlDataReader
        Dim FieldType As String
        Dim Fieldname As String
        Dim NewFieldnames As String
        Dim SQLStatement As String
        Dim ZeroDatetime As Boolean = True
        Dim Server As String = "localhost"
        Dim DbaseName As String = "simplequerybuilder"
        Dim USERNAME As String = "root"
        Dim password As String = "root"
        Dim port As String = "3306"
        Dim bl As MySqlBulkLoader
        Dim intLineCount As Integer

        Try
            ConnString = String.Format("server={0}; user id={1}; password={2}; database={3}; Convert Zero Datetime={4}; port={5}; pooling=false", Server, USERNAME, password, DbaseName, ZeroDatetime, port)
            con = New MySqlConnection(ConnString)
            bl = New MySqlBulkLoader(con)
            bl.TableName = DBTable
            bl.FieldTerminator = "|"
            bl.LineTerminator = "\n"
            bl.FileName = csvFilename

            Try
                con.Open()
                intLineCount = bl.Load()

            Catch ex As Exception
                MsgBox("DB Error during IMPORT In BulkLoaderMySQL: " & ex.Message.ToString)
            End Try

            con.Close()

        Catch ex As Exception
            MsgBox("DB Error In BulkLoaderMySQL: " & ex.Message.ToString)
        End Try

        MsgBox("OK FINISHED: " & CStr(intLineCount) & " LINES IMPORTED")
    End Sub

End Class
