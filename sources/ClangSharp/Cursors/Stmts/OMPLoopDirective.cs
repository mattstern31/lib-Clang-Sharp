// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using ClangSharp.Interop;

namespace ClangSharp;

public class OMPLoopDirective : OMPExecutableDirective
{
    private protected OMPLoopDirective(CXCursor handle, CXCursorKind expectedCursorKind, CX_StmtClass expectedStmtClass) : base(handle, expectedCursorKind, expectedStmtClass)
    {
        if (handle.StmtClass is > CX_StmtClass.CX_StmtClass_LastOMPLoopDirective or < CX_StmtClass.CX_StmtClass_FirstOMPLoopDirective)
        {
            throw new ArgumentOutOfRangeException(nameof(handle));
        }
    }
}
