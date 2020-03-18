' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports Microsoft.CodeAnalysis.VisualBasic.Symbols
Imports System.Diagnostics

Namespace Microsoft.CodeAnalysis.VisualBasic
    Partial Friend Class Binder

        Friend Function WithCheckedOrUncheckedRegion(CheckIntegerOverflow As Boolean) As Binder
            Debug.Assert(Not Me.Flags.Includes(BinderFlags.UncheckedRegion Or BinderFlags.CheckedRegion))

            Dim added As BinderFlags = If(CheckIntegerOverflow, BinderFlags.CheckedRegion, BinderFlags.UncheckedRegion)
            Dim removed As BinderFlags = If(CheckIntegerOverflow, BinderFlags.UncheckedRegion, BinderFlags.CheckedRegion)

            If Me.Flags.Includes(added) Then
                Return Me
            End If
            Me.WithFlags((Me.Flags And Not removed) Or added)
            Return Me
        End Function

        Friend Function WitharenthesizedExpression(Expression As BoundParenthesized) As Binder

            Return Me
        End Function

    End Class
End Namespace
