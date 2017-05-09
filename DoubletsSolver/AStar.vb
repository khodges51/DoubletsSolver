'This class allows an A* search to be performed in the domain of doublets. The search space is all words found in the dictionary as the same
'length as the target words. The search space is generated on the fly during search. 
Public Class AStar

    'Searches for the minimum number of changes needed to transform the start word to the end word. Returns the list of steps needed
    Public Function doubletSearch(ByVal startWord As String, ByVal endWord As String, ByRef possibleWords As Hashtable) As List(Of String)
        'Holds the search frontier
        Dim open As List(Of Node) = New List(Of Node)
        'Holds the visited nodes
        Dim closed As List(Of Node) = New List(Of Node)

        'Add the first node to the open list
        open.Add(New Node(startWord, possibleWords))

        'While the open list is not empty
        While open.Count > 0
            'Find the best scoring node on the frontier
            Dim currentNode As Node = popBestNode(open, endWord)

            'If we have reached our goal
            If String.Compare(currentNode.getWord, endWord) = 0 Then
                Console.WriteLine("SUCCESS")
                'Return a the list of steps needed to get here, including the start and end word
                Return traverseSolution(currentNode, startWord)
            End If

            'Add this node to the visited list and expand it to find its neighbours
            closed.Add(currentNode)
            Dim neighbours As List(Of Node) = currentNode.generateLinkedNodes()

            For Each neighbour As Node In neighbours
                'If the neighbour has already been visited
                If closed.Contains(neighbour) Then
                    Continue For
                End If

                'Incirment the g score, the cost of traversing to a neighbour is 1
                Dim gScore = currentNode.getGScore() + 1

                'If the open list does not contain this neighbour
                If open.Contains(neighbour) <> True Then
                    open.Add(neighbour)
                ElseIf gScore < neighbour.getGScore Then
                    'Move on if we have already found a better route to this node
                    Continue For
                End If

                neighbour.setParent(currentNode)
                neighbour.setGScore(gScore)
            Next

        End While

        'If we have reached the end without success, we have failed
        Console.WriteLine("FAILURE")
        Return New List(Of String)
    End Function

    'Calculate a nodes cost f(n) = g(n) + h(n) by calculating its heuristic value
    Private Function calculateNodeCost(ByVal node As Node, ByVal endWord As String) As Integer
        Dim word As String = node.getWord()
        'The heuristic value is the number of differing letter between this word and the end word
        Dim difference As Integer = 0

        'Count the number of differing letters
        For position As Integer = 0 To word.Length - 1
            If word(position) <> endWord(position) Then
                difference += 1
            End If
        Next

        'Return the total cost g + h
        Return node.getGScore() + difference
    End Function

    'Return and remove the best scoring node from the given list
    'I wanted to use a priorityQueue instead of a simple list but ran out of time
    Private Function popBestNode(ByVal nodeList As List(Of Node), ByVal endWord As String)
        'Initialise the best cost
        Dim bestNode As Node = nodeList(0)
        Dim bestCost As Integer = calculateNodeCost(bestNode, endWord)

        'Do a simple linear search for the best node
        For Each node As Node In nodeList
            Dim nodeCost As Integer = calculateNodeCost(node, endWord)
            If nodeCost < bestCost Then
                bestNode = node
                bestCost = nodeCost
            End If
        Next

        'Remove the node from the original list and return it
        nodeList.Remove(bestNode)
        Return bestNode
    End Function

    'Traverse the graph from the solution node to the start node, adding each word to a list
    Private Function traverseSolution(ByVal startNode As Node, ByVal startWord As String) As List(Of String)
        'The list of answers
        Dim answers As List(Of String) = New List(Of String)
        'The current node of traversal
        Dim currentNode As Node = startNode

        'Loop until we reach the start node
        While String.Compare(currentNode.getWord, startWord) <> 0
            Console.WriteLine(currentNode.getWord())
            answers.Add(currentNode.getWord())
            currentNode = currentNode.getParent()
        End While
        'Add the start word too
        Console.WriteLine(currentNode.getWord())
        answers.Add(currentNode.getWord())

        Return answers
    End Function

End Class
