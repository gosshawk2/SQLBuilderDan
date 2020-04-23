Public Class myGlobals
    Private _WhereConditions As String
    Private _lstConditions As List(Of String)
    Private _lastOperator As String


    Sub New()
        _lstConditions = New List(Of String)

    End Sub

    Public Property GetMyWhereCondtions As String
        Get
            Return _WhereConditions
        End Get
        Set(value As String)
            _WhereConditions = value
        End Set
    End Property

    Public Property lbConditions As List(Of String)
        Get
            Return _lstConditions
        End Get
        Set(value As List(Of String))
            _lstConditions = value
        End Set
    End Property

    Public Property LastOperator As String
        Get
            Return _lastOperator
        End Get
        Set(value As String)
            _lastOperator = value
        End Set
    End Property

    Public Sub DeleteConditions()
        _WhereConditions = ""
    End Sub

    Public Sub ClearConditionsList()
        If lbConditions IsNot Nothing Then
            lbConditions.Clear()
        End If


    End Sub

    Public Function IsInList(strItem As String) As Boolean
        IsInList = False
        If lbConditions IsNot Nothing Then
            If lbConditions.Contains(strItem) Then
                Return True
            End If
        End If

    End Function

    Public Function CountConditions() As Integer
        CountConditions = 0
        If lbConditions IsNot Nothing Then
            CountConditions = lbConditions.Count
        End If
    End Function

End Class
