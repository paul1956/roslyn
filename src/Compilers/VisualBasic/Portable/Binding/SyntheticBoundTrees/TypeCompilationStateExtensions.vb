Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic

Friend Module TypeCompilationStateExtensions
    <Extension>
    Friend Function CompilationCheckOverflow(compilationState As TypeCompilationState) As Boolean
        If compilationState Is Nothing Then
            Return True
        End If
        If compilationState.Compilation Is Nothing Then
            Return True
        End If
        If compilationState.Compilation.Options Is Nothing Then
            Return True
        End If
        Return compilationState.Compilation.Options.CheckOverflow
    End Function
End Module
