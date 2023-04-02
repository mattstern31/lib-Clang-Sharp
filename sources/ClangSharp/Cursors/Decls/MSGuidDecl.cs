// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using ClangSharp.Interop;
using static ClangSharp.Interop.CXCursorKind;
using static ClangSharp.Interop.CX_DeclKind;

namespace ClangSharp;

public sealed class MSGuidDecl : ValueDecl, IMergeable<MSGuidDecl>
{
    internal MSGuidDecl(CXCursor handle) : base(handle, CXCursor_UnexposedDecl, CX_DeclKind_MSGuid)
    {
    }

    public Guid Value => Handle.GuidValue;
}
