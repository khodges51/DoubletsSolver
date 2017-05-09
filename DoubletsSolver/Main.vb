Imports System.IO
Imports System.Collections

'This program finds solutions to the game Doublets. Through a console interface you can input two words of the same length.
'The program will output the minimum number of steps to transform the first word into the second by only changing one letter
'at a time. 'Each step must be a real word, this is checked through the dictionary text file provided in the program.
Module Main

    Sub Main()

        'Ask for user input
        Dim dictionaryName As String = "dictionary.txt"
        Console.WriteLine("Please input the start word")
        Dim startWord As String = Console.ReadLine
        Console.WriteLine("Please input the end word (Must be the same number of letters as the start word)")
        Dim endWord As String = Console.ReadLine
        Dim resultsName As String = "results.txt"

        'Find the number of transformations needed
        transformWord(dictionaryName, startWord, endWord, resultsName)

    End Sub

    'Creates a reults file with the given name that shows the transformation of the start word to the end word, using only words found within the dictionary file
    Public Sub transformWord(ByVal dictionaryFileName As String, ByVal startWord As String, ByVal endWord As String, ByVal resultFileName As String)

        'Assumes that the start and end word are the same length
        Dim wordLength As Integer = startWord.Length
        'Read from file to create a hash table of words that are the same length as the start word
        Dim wordTable As Hashtable = generateHashTable(dictionaryFileName, wordLength)

        'Run a new A* search
        Dim aStar As AStar = New AStar()
        Dim answers As List(Of String) = aStar.doubletSearch(startWord, endWord, wordTable)

        'Write the list of steps to the results file
        writeListToFile(resultFileName, answers)

        Console.WriteLine("Transformation Complete. A file has been created that shows the transformation results: " + resultFileName)
        Console.ReadLine()
    End Sub

    'Reads the dictionary file at the given url to generate a hash table containing all of the words of length "wordLength"
    'This is checking if a word is in the dictionary can have O(1) time
    Private Function generateHashTable(ByVal dictionaryFileName As String, ByVal wordLength As Integer) As Hashtable
        Dim wordTable As New Hashtable()

        If System.IO.File.Exists(dictionaryFileName) = True Then
            Dim objReader As New System.IO.StreamReader(dictionaryFileName)
            Dim nextWord As String

            'Loop for each word
            Do While objReader.Peek() <> -1

                'Read the word
                nextWord = objReader.ReadLine()

                'Add any words of the correct length to the hash table
                If nextWord.Length = wordLength Then
                    wordTable.Add(nextWord, nextWord)
                End If

            Loop

        Else
            Console.WriteLine("Dictionary file does Not Exist")
        End If

        Return wordTable
    End Function

    'Write the list of words to a the file
    Private Sub writeListToFile(ByVal fileName As String, ByVal wordList As List(Of String))
        Dim resultsFile As System.IO.StreamWriter
        resultsFile = My.Computer.FileSystem.OpenTextFileWriter(fileName, False)

        If wordList.Count > 0 Then
            'Works through backwards. Not very generalised but needed for my program as the answers list is backwards
            For i As Integer = wordList.Count - 1 To 0 Step -1
                resultsFile.WriteLine(wordList(i))
            Next
        End If

        resultsFile.Close()
    End Sub

End Module
