# DoubletsSolver
This program finds solutions for the doublets game by Lewis Carroll
Visual basic implimentation of a doulbets solver. Doublets are word puzzles in which you must trasform the start word to the end word by changing one letter at a time, only ever using real words. 

For example: TALE to COOL would  have a solution [TALE, TALL, TOOL, COOL]

This solution uses a dictionary text file to check for real words. The program uses A* search to find the answer, creating graph of words that are connected to words that differ by only one letter. The heuristic used is the number of incorrect letters compared to the goal word, e.g TOOL compared to COOL is only different by 1 letter. 

DoubletsSolver.sln is the visual studio project. 

/DoubletsSolver contains all the project files, including the source files "AStar", "Main" and "Node"

/DoubletsSolver/bin/Debug contains the dictionary file

/DoubletsSolver/publish contains a published version of the project which I am having trouble running
