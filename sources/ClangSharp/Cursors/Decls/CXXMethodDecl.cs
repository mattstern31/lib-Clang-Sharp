// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Collections.Generic;
using ClangSharp.Interop;
using static ClangSharp.Interop.CXCursorKind;
using static ClangSharp.Interop.CX_DeclKind;

namespace ClangSharp;

public class CXXMethodDecl : FunctionDecl
{
    private readonly Lazy<IReadOnlyList<CXXMethodDecl>> _overriddenMethods;
    private readonly Lazy<Type> _thisType;
    private readonly Lazy<Type> _thisObjectType;

    internal CXXMethodDecl(CXCursor handle) : this(handle, CXCursor_CXXMethod, CX_DeclKind_CXXMethod)
    {
    }

    private protected CXXMethodDecl(CXCursor handle, CXCursorKind expectedCursorKind, CX_DeclKind expectedDeclKind) : base(handle, expectedCursorKind, expectedDeclKind)
    {
        if (handle.DeclKind is > CX_DeclKind_LastCXXMethod or < CX_DeclKind_FirstCXXMethod)
        {
            throw new ArgumentOutOfRangeException(nameof(handle));
        }

        _overriddenMethods = new Lazy<IReadOnlyList<CXXMethodDecl>>(() => {
            var numOverriddenMethods = Handle.NumMethods;
            var overriddenMethods = new List<CXXMethodDecl>(numOverriddenMethods);

            for (var i = 0; i < numOverriddenMethods; i++)
            {
                var overriddenMethod = TranslationUnit.GetOrCreate<CXXMethodDecl>(Handle.GetMethod(unchecked((uint)i)));
                overriddenMethods.Add(overriddenMethod);
            }

            return overriddenMethods;
        });

        _thisType = new Lazy<Type>(() => TranslationUnit.GetOrCreate<Type>(Handle.ThisType));
        _thisObjectType = new Lazy<Type>(() => TranslationUnit.GetOrCreate<Type>(Handle.ThisObjectType));
    }

    public new CXXMethodDecl CanonicalDecl => (CXXMethodDecl)base.CanonicalDecl;

    public bool IsConst => Handle.CXXMethod_IsConst;

    public bool IsVirtual => Handle.CXXMethod_IsVirtual;

    public new CXXMethodDecl MostRecentDecl => (CXXMethodDecl)base.MostRecentDecl;

    public IReadOnlyList<CXXMethodDecl> OverriddenMethods => _overriddenMethods.Value;

    public new CXXRecordDecl? Parent => (CXXRecordDecl?)(base.Parent ?? ThisObjectType.AsCXXRecordDecl);

    public uint SizeOverriddenMethods => unchecked((uint)Handle.NumMethods);

    public Type ThisType => _thisType.Value;

    public Type ThisObjectType => _thisObjectType.Value;

    public long VtblIndex => IsVirtual ? Handle.VtblIdx : -1;
}
