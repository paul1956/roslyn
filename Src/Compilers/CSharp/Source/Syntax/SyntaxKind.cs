﻿// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace Microsoft.CodeAnalysis.CSharp
{
    public enum SyntaxKind : ushort
    {
        None,
        List = GreenNode.ListKind,

        // punctuation
        TildeToken = 8193,
        ExclamationToken,
        DollarToken,
        PercentToken,
        CaretToken,
        AmpersandToken,
        AsteriskToken,
        OpenParenToken,
        CloseParenToken,
        MinusToken,
        PlusToken,
        EqualsToken,
        OpenBraceToken,
        CloseBraceToken,
        OpenBracketToken,
        CloseBracketToken,
        BarToken,
        BackslashToken,
        ColonToken,
        SemicolonToken,
        DoubleQuoteToken,
        SingleQuoteToken,
        LessThanToken,
        CommaToken,
        GreaterThanToken,
        DotToken,
        QuestionToken,
        HashToken,
        SlashToken,

        // additional xml tokens
        SlashGreaterThanToken, // xml empty element end
        LessThanSlashToken, // element end tag start token
        XmlCommentStartToken, // <!--
        XmlCommentEndToken, // -->
        XmlCDataStartToken, // <![CDATA[
        XmlCDataEndToken, // ]]>
        XmlProcessingInstructionStartToken, // <?
        XmlProcessingInstructionEndToken, // ?>

        // compound punctuation
        BarBarToken,
        AmpersandAmpersandToken,
        MinusMinusToken,
        PlusPlusToken,
        ColonColonToken,
        QuestionQuestionToken,
        MinusGreaterThanToken,
        ExclamationEqualsToken,
        EqualsEqualsToken,
        EqualsGreaterThanToken,
        LessThanEqualsToken,
        LessThanLessThanToken,
        LessThanLessThanEqualsToken,
        GreaterThanEqualsToken,
        GreaterThanGreaterThanToken,
        GreaterThanGreaterThanEqualsToken,
        SlashEqualsToken,
        AsteriskEqualsToken,
        BarEqualsToken,
        AmpersandEqualsToken,
        PlusEqualsToken,
        MinusEqualsToken,
        CaretEqualsToken,
        PercentEqualsToken,

        // Keywords
        BoolKeyword,
        ByteKeyword,
        SByteKeyword,
        ShortKeyword,
        UShortKeyword,
        IntKeyword,
        UIntKeyword,
        LongKeyword,
        ULongKeyword,
        DoubleKeyword,
        FloatKeyword,
        DecimalKeyword,
        StringKeyword,
        CharKeyword,
        VoidKeyword,
        ObjectKeyword,
        TypeOfKeyword,
        SizeOfKeyword,
        NullKeyword,
        TrueKeyword,
        FalseKeyword,
        IfKeyword,
        ElseKeyword,
        WhileKeyword,
        ForKeyword,
        ForEachKeyword,
        DoKeyword,
        SwitchKeyword,
        CaseKeyword,
        DefaultKeyword,
        TryKeyword,
        CatchKeyword,
        FinallyKeyword,
        LockKeyword,
        GotoKeyword,
        BreakKeyword,
        ContinueKeyword,
        ReturnKeyword,
        ThrowKeyword,
        PublicKeyword,
        PrivateKeyword,
        InternalKeyword,
        ProtectedKeyword,
        StaticKeyword,
        ReadOnlyKeyword,
        SealedKeyword,
        ConstKeyword,
        FixedKeyword,
        StackAllocKeyword,
        VolatileKeyword,
        NewKeyword,
        OverrideKeyword,
        AbstractKeyword,
        VirtualKeyword,
        EventKeyword,
        ExternKeyword,
        RefKeyword,
        OutKeyword,
        InKeyword,
        IsKeyword,
        AsKeyword,
        ParamsKeyword,
        ArgListKeyword,
        MakeRefKeyword,
        RefTypeKeyword,
        RefValueKeyword,
        ThisKeyword,
        BaseKeyword,
        NamespaceKeyword,
        UsingKeyword,
        ClassKeyword,
        StructKeyword,
        InterfaceKeyword,
        EnumKeyword,
        DelegateKeyword,
        CheckedKeyword,
        UncheckedKeyword,
        UnsafeKeyword,
        OperatorKeyword,
        ExplicitKeyword,
        ImplicitKeyword,

        // contextual keywords
        YieldKeyword,
        PartialKeyword,
        AliasKeyword,
        GlobalKeyword,
        AssemblyKeyword,
        ModuleKeyword,
        TypeKeyword,
        FieldKeyword,
        MethodKeyword,
        ParamKeyword,
        PropertyKeyword,
        TypeVarKeyword,
        GetKeyword,
        SetKeyword,
        AddKeyword,
        RemoveKeyword,
        WhereKeyword,
        FromKeyword,
        GroupKeyword,
        JoinKeyword,
        IntoKeyword,
        LetKeyword,
        ByKeyword,
        SelectKeyword,
        OrderByKeyword,
        OnKeyword,
        EqualsKeyword,
        AscendingKeyword,
        DescendingKeyword,
        AsyncKeyword,
        AwaitKeyword,

        // additional preprocessor keywords
        ElifKeyword,
        EndIfKeyword,
        RegionKeyword,
        EndRegionKeyword,
        DefineKeyword,
        UndefKeyword,
        WarningKeyword,
        ErrorKeyword,
        LineKeyword,
        PragmaKeyword,
        HiddenKeyword,
        ChecksumKeyword,
        DisableKeyword,
        RestoreKeyword,
        ReferenceKeyword,

        // Other
        OmittedTypeArgumentToken,
        OmittedArraySizeExpressionToken,
        EndOfDirectiveToken,
        EndOfDocumentationCommentToken,
        EndOfFileToken, //NB: this is assumed to be the last textless token

        // tokens with text
        BadToken,
        IdentifierToken,
        NumericLiteralToken,
        CharacterLiteralToken,
        StringLiteralToken,
        XmlEntityLiteralToken,  // &lt; &gt; &quot; &amp; &apos; or &name; or &#nnnn; or &#xhhhh;
        XmlTextLiteralToken,    // xml text node text
        XmlTextLiteralNewLineToken,

        // trivia
        EndOfLineTrivia,
        WhitespaceTrivia,
        SingleLineCommentTrivia,
        MultiLineCommentTrivia,
        DocumentationCommentExteriorTrivia,
        SingleLineDocumentationCommentTrivia,
        MultiLineDocumentationCommentTrivia,
        DisabledTextTrivia,
        PreprocessingMessageTrivia,
        IfDirectiveTrivia,
        ElifDirectiveTrivia,
        ElseDirectiveTrivia,
        EndIfDirectiveTrivia,
        RegionDirectiveTrivia,
        EndRegionDirectiveTrivia,
        DefineDirectiveTrivia,
        UndefDirectiveTrivia,
        ErrorDirectiveTrivia,
        WarningDirectiveTrivia,
        LineDirectiveTrivia,
        PragmaWarningDirectiveTrivia,
        PragmaChecksumDirectiveTrivia,
        ReferenceDirectiveTrivia,
        BadDirectiveTrivia,
        SkippedTokensTrivia,

        // xml nodes (for xml doc comment structure)
        XmlElement,
        XmlElementStartTag,
        XmlElementEndTag,
        XmlEmptyElement,
        XmlTextAttribute,
        XmlCrefAttribute,
        XmlNameAttribute,
        XmlName,
        XmlPrefix,
        XmlText,
        XmlCDataSection,
        XmlComment,
        XmlProcessingInstruction,

        // documentation comment nodes (structure inside DocumentationCommentTrivia)
        TypeCref,
        QualifiedCref,
        NameMemberCref,
        IndexerMemberCref,
        OperatorMemberCref,
        ConversionOperatorMemberCref,
        CrefParameterList,
        CrefBracketedParameterList,
        CrefParameter,

        // names & type-names
        IdentifierName,
        QualifiedName,
        GenericName,
        TypeArgumentList,
        AliasQualifiedName,
        PredefinedType,
        ArrayType,
        ArrayRankSpecifier,
        PointerType,
        NullableType,
        OmittedTypeArgument,

        // expressions
        ParenthesizedExpression,
        ConditionalExpression,
        InvocationExpression,
        ElementAccessExpression,
        ArgumentList,
        BracketedArgumentList,
        Argument,
        NameColon,
        CastExpression,
        AnonymousMethodExpression,
        SimpleLambdaExpression,
        ParenthesizedLambdaExpression,
        ObjectInitializerExpression,
        CollectionInitializerExpression,
        ArrayInitializerExpression,
        AnonymousObjectMemberDeclarator,
        ComplexElementInitializerExpression,
        ObjectCreationExpression,
        AnonymousObjectCreationExpression,
        ArrayCreationExpression,
        ImplicitArrayCreationExpression,
        StackAllocArrayCreationExpression,
        OmittedArraySizeExpression,

        // binary expressions
        AddExpression,
        SubtractExpression,
        MultiplyExpression,
        DivideExpression,
        ModuloExpression,
        LeftShiftExpression,
        RightShiftExpression,
        LogicalOrExpression,
        LogicalAndExpression,
        BitwiseOrExpression,
        BitwiseAndExpression,
        ExclusiveOrExpression,
        EqualsExpression,
        NotEqualsExpression,
        LessThanExpression,
        LessThanOrEqualExpression,
        GreaterThanExpression,
        GreaterThanOrEqualExpression,
        IsExpression,
        AsExpression,
        CoalesceExpression,
        SimpleMemberAccessExpression,  // dot access:   a.b
        PointerMemberAccessExpression,  // arrow access:   a->b
        ConditionalAccessExpression,    // question mark access:   a?.b , a?[1]

        // binding expressions
        MemberBindingExpression,
        ElementBindingExpression,

        // binary assignment expressions
        SimpleAssignmentExpression,
        AddAssignmentExpression,
        SubtractAssignmentExpression,
        MultiplyAssignmentExpression,
        DivideAssignmentExpression,
        ModuloAssignmentExpression,
        AndAssignmentExpression,
        ExclusiveOrAssignmentExpression,
        OrAssignmentExpression,
        LeftShiftAssignmentExpression,
        RightShiftAssignmentExpression,

        // unary expressions
        UnaryPlusExpression,
        UnaryMinusExpression,
        BitwiseNotExpression,
        LogicalNotExpression,
        PreIncrementExpression,
        PreDecrementExpression,
        PointerIndirectionExpression,
        AddressOfExpression,
        PostIncrementExpression,
        PostDecrementExpression,
        AwaitExpression,

        // primary expression
        ThisExpression,
        BaseExpression,
        ArgListExpression,
        NumericLiteralExpression,
        StringLiteralExpression,
        CharacterLiteralExpression,
        TrueLiteralExpression,
        FalseLiteralExpression,
        NullLiteralExpression,

        // primary function expressions
        TypeOfExpression,
        SizeOfExpression,
        CheckedExpression,
        UncheckedExpression,
        DefaultExpression,
        MakeRefExpression,
        RefValueExpression,
        RefTypeExpression,

        // query expressions
        QueryExpression,
        QueryBody,
        FromClause,
        LetClause,
        JoinClause,
        JoinIntoClause,
        WhereClause,
        OrderByClause,
        AscendingOrdering,
        DescendingOrdering,
        SelectClause,
        GroupClause,
        QueryContinuation,

        // statements
        Block,
        LocalDeclarationStatement,
        VariableDeclaration,
        VariableDeclarator,
        EqualsValueClause,
        ExpressionStatement,
        EmptyStatement,
        LabeledStatement,

        // jump statements
        GotoStatement,
        GotoCaseStatement,
        GotoDefaultStatement,
        BreakStatement,
        ContinueStatement,
        ReturnStatement,
        YieldReturnStatement,
        YieldBreakStatement,
        ThrowStatement,

        WhileStatement,
        DoStatement,
        ForStatement,
        ForEachStatement,
        UsingStatement,
        FixedStatement,

        // checked statements
        CheckedStatement,
        UncheckedStatement,

        UnsafeStatement,
        LockStatement,
        IfStatement,
        ElseClause,
        SwitchStatement,
        SwitchSection,
        CaseSwitchLabel,
        DefaultSwitchLabel,
        TryStatement,
        CatchClause,
        CatchDeclaration,
        CatchFilterClause,
        FinallyClause,

        // declarations
        CompilationUnit,
        GlobalStatement,
        NamespaceDeclaration,
        UsingDirective,
        ExternAliasDirective,

        // attributes
        AttributeList,
        AttributeTargetSpecifier,
        Attribute,
        AttributeArgumentList,
        AttributeArgument,
        NameEquals,

        // type declarations
        ClassDeclaration,
        StructDeclaration,
        InterfaceDeclaration,
        EnumDeclaration,
        DelegateDeclaration,

        BaseList,
        TypeParameterConstraintClause,
        ConstructorConstraint,
        ClassConstraint,
        StructConstraint,
        TypeConstraint,
        ExplicitInterfaceSpecifier,
        EnumMemberDeclaration,
        FieldDeclaration,
        EventFieldDeclaration,
        MethodDeclaration,
        OperatorDeclaration,
        ConversionOperatorDeclaration,
        ConstructorDeclaration,
        BaseConstructorInitializer,
        ThisConstructorInitializer,
        DestructorDeclaration,
        PropertyDeclaration,
        EventDeclaration,
        IndexerDeclaration,
        AccessorList,
        GetAccessorDeclaration,
        SetAccessorDeclaration,
        AddAccessorDeclaration,
        RemoveAccessorDeclaration,
        UnknownAccessorDeclaration,
        ParameterList,
        BracketedParameterList,
        Parameter,
        TypeParameterList,
        TypeParameter,
        IncompleteMember
    }
}
