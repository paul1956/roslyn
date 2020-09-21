' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.PooledObjects
Imports Microsoft.CodeAnalysis.Simplification
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

Namespace Microsoft.CodeAnalysis.VisualBasic.Extensions
    Friend Module CastExpressionSyntaxExtensions
        <Extension>
        Public Function Uncast(cast As CastExpressionSyntax) As ExpressionSyntax
            Return Uncast(cast, cast.OpenParenToken, cast.Expression, cast.CommaToken, cast.Type, cast.CloseParenToken)
        End Function

        <Extension>
        Public Function Uncast(cast As PredefinedCastExpressionSyntax) As ExpressionSyntax
            Return Uncast(cast, cast.OpenParenToken, cast.Expression, commaToken:=Nothing, typeNode:=Nothing, cast.CloseParenToken)
        End Function

        Private Function Uncast(castNode As ExpressionSyntax, openParen As SyntaxToken, innerNode As ExpressionSyntax, commaToken As SyntaxToken, typeNode As TypeSyntax, closeParen As SyntaxToken) As ExpressionSyntax

            Dim leadingTrivia As SyntaxTriviaList = castNode.GetLeadingTrivia
            leadingTrivia = leadingTrivia.AppendAdditionalTriviaWithCommentsOrLineContinuation(openParen.LeadingTrivia)
            leadingTrivia = leadingTrivia.AppendAdditionalTriviaWithCommentsOrLineContinuation(openParen.TrailingTrivia)
#Disable Warning CA1826 ' Do not use Enumerable methods on indexable collections. Instead use the collection directly
            If leadingTrivia.FirstOrDefault.IsKind(SyntaxKind.WhitespaceTrivia) AndAlso Not castNode.GetLeadingTrivia.Any Then
#Enable Warning CA1826 ' Do not use Enumerable methods on indexable collections. Instead use the collection directly
                leadingTrivia.RemoveAt(0)
            End If
            leadingTrivia.AddRange(innerNode.GetLeadingTrivia)

            Dim trailingTrivia As SyntaxTriviaList = castNode.GetTrailingTrivia
            trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(closeParen.LeadingTrivia)
            trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(closeParen.TrailingTrivia)
            trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(openParen.LeadingTrivia)

            ' Nothing for Predefined Cast
            If typeNode IsNot Nothing Then
                trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(typeNode.GetTrailingTrivia)
                trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(typeNode.GetLeadingTrivia)
            End If

            ' Kind None for Predefined Cast
            If commaToken.IsKind(SyntaxKind.CommaToken) Then
                trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(commaToken.TrailingTrivia)
                trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(commaToken.LeadingTrivia)
            End If

            trailingTrivia = trailingTrivia.InsertAdditionalTriviaWithCommentsOrLineContinuation(innerNode.GetTrailingTrivia)

            If trailingTrivia.Count > 0 Then
                Dim newTrailingTrivia As ArrayBuilder(Of SyntaxTrivia) = ArrayBuilder(Of SyntaxTrivia).GetInstance()
                Dim foundEOL As Boolean = False
                For i As Integer = 0 To trailingTrivia.Count - 1
                    Dim trivia As SyntaxTrivia = trailingTrivia(i)
                    Dim nextTrivia As SyntaxTrivia = GetForwardTriviaOrDefault(trailingTrivia, i, lookaheadCount:=1)
                    Select Case trivia.Kind
                        Case SyntaxKind.WhitespaceTrivia
                            Select Case nextTrivia.Kind
                                Case SyntaxKind.WhitespaceTrivia
                                    trailingTrivia = trailingTrivia.Replace(trailingTrivia(i + 1), AdjustWhitespace(trivia, nextTrivia, GetForwardTriviaOrDefault(trailingTrivia, i, lookaheadCount:=2).IsKind(SyntaxKind.LineContinuationTrivia)))
                                Case SyntaxKind.EndOfLineTrivia
                                    ' skip Whitespace before EOL
                                Case SyntaxKind.LineContinuationTrivia
                                    If GetForwardTriviaOrDefault(trailingTrivia, i, lookaheadCount:=2).IsKind(SyntaxKind.WhitespaceTrivia) Then
                                        trailingTrivia = trailingTrivia.Replace(trailingTrivia(i + 2), trivia)
                                        trivia = SyntaxFactory.Space
                                    End If
                                    newTrailingTrivia.Add(trivia)
                                Case SyntaxKind.CommentTrivia
                                    newTrailingTrivia.Add(trivia)
                                Case Else
                                    newTrailingTrivia.Add(trivia)
                            End Select
                        Case SyntaxKind.EndOfLineTrivia
                            If Not foundEOL Then
                                newTrailingTrivia.Add(trivia)
                                foundEOL = True
                            End If
                        Case SyntaxKind.LineContinuationTrivia
                            newTrailingTrivia.Add(trivia)
                        Case SyntaxKind.CommentTrivia
                            newTrailingTrivia.Add(trivia)
                            foundEOL = False
                        Case Else
                            foundEOL = False
                            newTrailingTrivia.Add(trivia)
                    End Select
                Next
                trailingTrivia = trailingTrivia.DefaultIfEmpty.ToSyntaxTriviaList
            End If
            Dim resultNode = innerNode.With(leadingTrivia, trailingTrivia)

            resultNode = SimplificationHelpers.CopyAnnotations(castNode, resultNode)

            Return resultNode

        End Function

        ''' <summary>
        ''' If additional trivia contains a comment or _
        ''' it is added it to end of triviaList
        ''' </summary>
        ''' <param name="triviaList"></param>
        ''' <param name="additionalTrivia"></param>
        ''' <returns>Merged Trivia List</returns>
        <Extension>
        Private Function AppendAdditionalTriviaWithCommentsOrLineContinuation(triviaList As SyntaxTriviaList, additionalTrivia As SyntaxTriviaList) As SyntaxTriviaList
            If additionalTrivia.ContainsAnyCommentOrLineContinuation() Then
                Return triviaList.AddRange(additionalTrivia)
            End If
            Return triviaList
        End Function

        ''' <summary>
        ''' If additional trivia contains a comment or _
        ''' insert it at start of triviaList
        ''' </summary>
        ''' <param name="triviaList"></param>
        ''' <param name="additionalTrivia"></param>
        ''' <returns>Merged Trivia List</returns>
        <Extension>
        Private Function InsertAdditionalTriviaWithCommentsOrLineContinuation(triviaList As SyntaxTriviaList, additionalTrivia As SyntaxTriviaList) As SyntaxTriviaList
            If additionalTrivia.ContainsAnyCommentOrLineContinuation() Then
                Return triviaList.InsertRange(0, additionalTrivia)
            End If
            Return triviaList
        End Function

        Private Function GetForwardTriviaOrDefault(triviaList As IEnumerable(Of SyntaxTrivia), index As Integer, lookaheadCount As Integer) As SyntaxTrivia
            Dim charIndex As Integer = index + lookaheadCount
            Return If(charIndex < triviaList.Count, triviaList(charIndex), New SyntaxTrivia)
        End Function

        ''' <summary>
        ''' Adjust whitespace trivia when comment are preserved using _ and you end up
        ''' with "        _ " and because of poor VB formatting around _ you end up
        ''' with " _ "
        ''' and whet you want is " _        "
        ''' </summary>
        ''' <param name="trivia"></param>
        ''' <param name="nextTrivia"></param>
        ''' <param name="afterLineContinue"></param>
        ''' <returns></returns>
        Private Function AdjustWhitespace(trivia As SyntaxTrivia, nextTrivia As SyntaxTrivia, afterLineContinue As Boolean) As SyntaxTrivia
            ' Trivia before and after _ is the same so just return it
            If trivia.Span.Length = nextTrivia.Span.Length Then
                Return trivia
            End If
            Dim lineContinueOffset As Integer = If(afterLineContinue, 2, 0)
            If trivia.Span.Length > nextTrivia.Span.Length Then
                Return SyntaxFactory.Whitespace(New String(" "c, Math.Max(trivia.FullWidth - lineContinueOffset, 1)))
            End If
            Return SyntaxFactory.Whitespace(New String(" "c, Math.Max(nextTrivia.FullWidth - lineContinueOffset, 1)))
        End Function
    End Module
End Namespace
