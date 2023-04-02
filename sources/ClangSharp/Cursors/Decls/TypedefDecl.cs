// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using ClangSharp.Interop;
using static ClangSharp.Interop.CXCursorKind;
using static ClangSharp.Interop.CX_DeclKind;

namespace ClangSharp;

public sealed class TypedefDecl : TypedefNameDecl
{
    internal TypedefDecl(CXCursor handle) : base(handle, CXCursor_TypedefDecl, CX_DeclKind_Typedef)
    {
    }
}
