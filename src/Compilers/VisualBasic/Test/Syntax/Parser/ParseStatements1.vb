' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.
#If SupportCheckedStatement Then
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Text
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Symbols
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.CodeAnalysis.VisualBasic.SyntaxFacts
Imports Roslyn.Test.Utilities

<CLSCompliant(False)>
Public Class ParseStatements1
    Inherits BasicTestBase
    <Fact>
    Public Sub ParseCheckedStatementOff()
        Dim tree = ParseAndVerify(<![CDATA[
Module M
    Sub Main()
        Checked Off
            Dim x As Byte = 258
        End Checked
    End Sub
End Module
        ]]>)
    End Sub

    <Fact>
    Public Sub ParseCheckedStatementOn()
        Dim tree = ParseAndVerify(<![CDATA[
Module M
    Sub Main()
        Checked On
            Dim x As Byte = 258
        End Checked
    End Sub
End Module
        ]]>
        )
    End Sub

    <Fact>
    Public Sub SyntaxFactoryParseCheckedStatement()
        Dim text = "Checked On
    Dim X as integer = 1
End Checked"
        Dim statement As StatementSyntax = SyntaxFactory.ParseExecutableStatement(text)

        Assert.NotNull(statement)
        Assert.Equal(SyntaxKind.CheckedBlock, statement.Kind())
        Assert.Equal(text, statement.ToString())
        Assert.Equal(0, statement.GetDiagnostics().Count)

        Dim cs As CheckedBlockSyntax = CType(statement, CheckedBlockSyntax)
        Assert.Equal(SyntaxKind.CheckedKeyword, cs.CheckedStatement.CheckedKeyword.Kind())
        Assert.Equal(SyntaxKind.OnKeyword, cs.CheckedStatement.ValueKeyword.Kind())
        Assert.Equal(cs.Statements.Count, 1)
        Assert.NotEqual(CType(Nothing, EndBlockStatementSyntax), cs.EndCheckedStatement)
    End Sub

End Class
#End If
