// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Diagnostics;
using ClangSharp.Interop;
using static ClangSharp.Interop.CXCursorKind;
using static ClangSharp.Interop.CX_StmtClass;

namespace ClangSharp;

public sealed class ObjCEncodeExpr : Expr
{
    private readonly Lazy<Type> _encodedType;

    internal ObjCEncodeExpr(CXCursor handle) : base(handle, CXCursor_ObjCEncodeExpr, CX_StmtClass_ObjCEncodeExpr)
    {
        Debug.Assert(NumChildren is 0);
        _encodedType = new Lazy<Type>(() => TranslationUnit.GetOrCreate<Type>(Handle.TypeOperand));
    }

    public Type EncodedType => _encodedType.Value;
}
