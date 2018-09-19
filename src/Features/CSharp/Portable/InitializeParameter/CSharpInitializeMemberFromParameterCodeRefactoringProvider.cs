﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Composition;
using System.Threading;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.InitializeParameter;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis.CSharp.InitializeParameter
{
    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(CSharpInitializeMemberFromParameterCodeRefactoringProvider)), Shared]
    [ExtensionOrder(Before = nameof(CSharpAddParameterCheckCodeRefactoringProvider))]
    internal class CSharpInitializeMemberFromParameterCodeRefactoringProvider :
        AbstractInitializeMemberFromParameterCodeRefactoringProvider<
            ParameterSyntax,
            StatementSyntax,
            ExpressionSyntax>
    {
        protected override bool IsFunctionDeclaration(SyntaxNode node)
            => InitializeParameterHelpers.IsFunctionDeclaration(node);

        protected override SyntaxNode GetTypeBlock(SyntaxNode node)
            => node;

        protected override bool CanOfferRefactoring(SyntaxNode functionDeclaration, OperationKind operationKind)
            => InitializeParameterHelpers.CanOfferRefactoring(functionDeclaration, operationKind);

        protected override IBlockOperation GetBlockOperation(SyntaxNode functionDeclaration, SemanticModel semanticModel, IOperation operation, CancellationToken cancellationToken)
            => InitializeParameterHelpers.GetBlockOperation(functionDeclaration, semanticModel, operation, cancellationToken);

        protected override SyntaxNode TryGetLastStatement(IBlockOperation blockStatementOpt)
            => InitializeParameterHelpers.TryGetLastStatement(blockStatementOpt);

        protected override void InsertStatement(SyntaxEditor editor, SyntaxNode functionDeclaration, IMethodSymbol method, SyntaxNode statementToAddAfterOpt, StatementSyntax statement)
            => InitializeParameterHelpers.InsertStatement(editor, functionDeclaration, method, statementToAddAfterOpt, statement);

        protected override bool IsImplicitConversion(Compilation compilation, ITypeSymbol source, ITypeSymbol destination)
            => InitializeParameterHelpers.IsImplicitConversion(compilation, source, destination);
    }
}
