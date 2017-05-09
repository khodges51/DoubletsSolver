Imports System.Collections

'This class creates node used for A* search in doublets. The class stores information about the node, such as the word contained,
'and allows for the node to be expanded by finding all neighbouring nodes. 
Public Class Node

    'The word this node contains
    Dim word As String
    'The dictionary of possible words of the same size as this word
    Dim possibleWords As Hashtable
    'Used to help generate linked words
    Dim alphabet As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

    'The words that differ from this word by 1 letter
    Dim linkedNodes As List(Of Node)
    'The parent node during an A* search
    Dim parent As Node
    'The score to get to this node during an A* search
    Dim gScore As Integer = 0

    'Create a new node
    Sub New(ByVal theWord As String, ByRef wordTable As Hashtable)
        word = theWord
        possibleWords = wordTable
        linkedNodes = New List(Of Node)
    End Sub

    'Find all nodes that differ from this word by 1 letter 
    Public Function generateLinkedNodes() As List(Of Node)
        'If they have already been generated
        If linkedNodes.Count > 0 Then
            Return linkedNodes
        End If

        'Loop for each letter in the word
        For position As Integer = 1 To word.Length

            'Loop for each letter in the alphabet
            For letter As Integer = 1 To alphabet.Length

                'Create a word that is 1 letter different than the original
                Dim linkedWord = word
                Mid(linkedWord, position, 1) = Mid(alphabet, letter, 1)

                'If this is a word contained within the dictionary
                If possibleWords.Contains(linkedWord) Then
                    If Not linkedWord = word Then
                        'Create a new node
                        Dim newNode As Node = New Node(linkedWord, possibleWords)
                        'Store a reference of it
                        linkedNodes.Add(newNode)
                    End If
                End If

            Next

        Next

        'Return the linked nodes
        Return linkedNodes
    End Function

    'Return the word this node contains
    Public Function getWord() As String
        Return word
    End Function

    'Return the score so far to reach this node
    Public Function getGScore() As Integer
        Return gScore
    End Function

    'Set the score so far to reach this node
    Public Sub setGScore(ByVal newGScore As Integer)
        gScore = newGScore
    End Sub

    'Return the parent node
    Public Function getParent() As Node
        Return parent
    End Function

    'Set the parent node
    Public Sub setParent(ByVal newParent As Node)
        parent = newParent
    End Sub

End Class
