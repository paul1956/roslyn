' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

'-----------------------------------------------------------------------------
' Contains the definition of the BlockContext
'-----------------------------------------------------------------------------
#If SupportCheckedStatement Then
Imports Microsoft.CodeAnalysis.Syntax.InternalSyntax
Imports InternalSyntaxFactory = Microsoft.CodeAnalysis.VisualBasic.Syntax.InternalSyntax.SyntaxFactory

Namespace Microsoft.CodeAnalysis.VisualBasic.Syntax.InternalSyntax

    Friend NotInheritable Class CheckedBlockContext
        Inherits ExecutableStatementContext

        Friend Sub New(statement As StatementSyntax, prevContext As BlockContext)
            MyBase.New(SyntaxKind.CheckedBlock, statement, prevContext)

            Debug.Assert(statement.Kind = SyntaxKind.CheckedStatement)

        End Sub

        Friend Overrides Function ProcessSyntax(node As VisualBasicSyntaxNode) As BlockContext

            Return MyBase.ProcessSyntax(node)
        End Function

        Friend Overrides Function TryLinkSyntax(node As VisualBasicSyntaxNode, ByRef newContext As BlockContext) As LinkResult
            newContext = Nothing
            Select Case node.Kind

                Case _
                    SyntaxKind.CheckedStatement
                    Return UseSyntax(node, newContext)

                Case _
                    SyntaxKind.CheckedBlock
                    ' Skip terminator because these are not statements
                    Return UseSyntax(node, newContext) Or LinkResult.SkipTerminator

                Case Else
                    Return MyBase.TryLinkSyntax(node, newContext)
            End Select
        End Function

        Friend Overrides Function CreateBlockSyntax(endStmt As StatementSyntax) As VisualBasicSyntaxNode

            Debug.Assert(BeginStatement IsNot Nothing)
            Dim beginStmt As CheckedStatementSyntax = DirectCast(BeginStatement, CheckedStatementSyntax)

            If endStmt Is Nothing Then
                beginStmt = Parser.ReportSyntaxError(beginStmt, ERRID.ERR_EndCheckedExpected)
                endStmt = SyntaxFactory.EndCheckedStatement(InternalSyntaxFactory.MissingKeyword(SyntaxKind.EndKeyword), InternalSyntaxFactory.MissingKeyword(SyntaxKind.CheckedKeyword))
            End If

            Dim result = SyntaxFactory.CheckedBlock(beginStmt, Body(), DirectCast(endStmt, EndBlockStatementSyntax))
            FreeStatements()

            Return result
        End Function

        Friend Overrides Function EndBlock(statement As StatementSyntax) As BlockContext
            Dim blockSyntax = CreateBlockSyntax(statement)
            Return PrevBlock.ProcessSyntax(blockSyntax)
        End Function
    End Class
End Namespace
#End If
