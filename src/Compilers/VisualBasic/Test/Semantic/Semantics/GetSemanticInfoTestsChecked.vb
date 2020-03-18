' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.
#If SupportCheckedStatement Then
Imports System.Collections.Immutable
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.SpecialType
Imports Microsoft.CodeAnalysis.Test.Utilities
Imports Microsoft.CodeAnalysis.Text
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.OverloadResolution
Imports Microsoft.CodeAnalysis.VisualBasic.Symbols
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

Imports Roslyn.Test.Utilities

Namespace Microsoft.CodeAnalysis.VisualBasic.UnitTests.Semantics
    Partial Public Class SemanticModelTests
        <Fact>
        Public Sub SpeculativelyBindChecked()
            Dim comp = CompilationUtils.CreateCompilationWithMscorlib40AndVBRuntimeAndReferences(
    <compilation name="CheckedUnchecked">
        <file name="a.vb">
Imports System

Module Program
    Sub Main(args As String())
        Dim ten As Byte = 10
        Checked Off
            Dim i2 As Integer = 2147483647 + ten
            System.Console.WriteLine(i2)
        End Checked
    End Sub
End Module
        </file>
    </compilation>, {SystemCoreRef})
            comp.AssertNoDiagnostics()

            Dim tree = comp.SyntaxTrees.Single()
            Dim model = comp.GetSemanticModel(tree)

            Dim originalSyntax = tree.GetCompilationUnitRoot().DescendantNodes.OfType(Of CheckedBlockSyntax).Single()
            ' GetLocalForDeclaration has been called and returns correct value
            Dim dimStatement As LocalDeclarationStatementSyntax = CType(originalSyntax.Statements(0), LocalDeclarationStatementSyntax)
            Assert.True(dimStatement.ToString().EndsWith("ten", StringComparison.Ordinal))

            Assert.True(dimStatement.Declarators.Count = 1)
            Assert.True(dimStatement.Declarators(0) IsNot Nothing)
            Dim var As ModifiedIdentifierSyntax = dimStatement.Declarators(0).Names(0)
            Dim expressionStatement As ExpressionStatementSyntax = CType(originalSyntax.Statements(1), ExpressionStatementSyntax)

            Dim expression As InvocationExpressionSyntax = CType(expressionStatement.Expression, InvocationExpressionSyntax)
            Dim info1 = model.GetSymbolInfo(expression.ArgumentList.Arguments(0).GetExpression)
            Stop
        End Sub

    End Class
End Namespace
#End If
