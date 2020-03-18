Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

Friend Module BinderExtensions
    <Extension>
    Friend Function CheckOverflowFromCompiler(_Binder As Binder) As Boolean
        If _Binder.Compilation Is Nothing Then
            Return True
        End If
        If _Binder.Compilation.Options Is Nothing Then
            Return True
        End If
        Return _Binder.Compilation.Options.CheckOverflow

    End Function

    <Extension>
    Friend Function RequireOverflowCheck(executableSyntax As SyntaxNode, Optional checkOverflowDefault As Boolean = True, Optional compilationState As TypeCompilationState = Nothing) As Boolean
        While executableSyntax IsNot Nothing
            Select Case executableSyntax.Kind
                Case SyntaxKind.CheckedExpression
                    Return True
                Case SyntaxKind.UncheckedExpression
                    Return False
#If SupportCheckedStatement Then
                Case SyntaxKind.CheckedStatement
                    Return CType(executableSyntax, CheckedStatementSyntax).ValueKeyword.Kind <> SyntaxKind.UncheckedKeyword
                Case SyntaxKind.CheckedBlock
                    Return CType(executableSyntax, CheckedBlockSyntax).CheckedStatement.ValueKeyword.Kind = SyntaxKind.OnKeyword
#End If
            End Select
            executableSyntax = executableSyntax.Parent
        End While
        If compilationState IsNot Nothing Then
            Return compilationState.CompilationCheckOverflow
        End If
        Return checkOverflowDefault
    End Function

End Module
