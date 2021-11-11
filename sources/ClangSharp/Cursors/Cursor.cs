// Copyright (c) .NET Foundation and Contributors. All rights reserved. Licensed under the University of Illinois/NCSA Open Source License. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ClangSharp.Interop;

namespace ClangSharp
{
    [DebuggerDisplay("{Handle.DebuggerDisplayString,nq}")]
    public unsafe class Cursor : IEquatable<Cursor>
    {
        private readonly Lazy<string> _kindSpelling;
        private readonly Lazy<Cursor> _lexicalParentCursor;
        private readonly Lazy<Cursor> _semanticParentCursor;
        private readonly Lazy<string> _spelling;
        private readonly Lazy<TranslationUnit> _translationUnit;
        private List<Cursor> _cursorChildren;

        private protected Cursor(CXCursor handle, CXCursorKind expectedCursorKind)
        {
            if (handle.kind != expectedCursorKind)
            {
                throw new ArgumentOutOfRangeException(nameof(handle));
            }
            Handle = handle;

            _kindSpelling = new Lazy<string>(() => Handle.KindSpelling.ToString());
            _lexicalParentCursor = new Lazy<Cursor>(() => TranslationUnit.GetOrCreate<Cursor>(Handle.LexicalParent));
            _semanticParentCursor = new Lazy<Cursor>(() => TranslationUnit.GetOrCreate<Cursor>(Handle.SemanticParent));
            _spelling = new Lazy<string>(() => Handle.Spelling.ToString());
            _translationUnit = new Lazy<TranslationUnit>(() => TranslationUnit.GetOrCreate(Handle.TranslationUnit));
        }

        public IReadOnlyList<Cursor> CursorChildren
        {
            get
            {
                if (_cursorChildren is null)
                {
                    var cursorChildren = GCHandle.Alloc(new List<Cursor>());

#if !NET5_0_OR_GREATER
                    var visitor = (CXCursorVisitor)Visitor;
                    var pVisitor = Marshal.GetFunctionPointerForDelegate(visitor);
#else
                    var pVisitor = (delegate* unmanaged[Cdecl]<CXCursor, CXCursor, nint*, CXChildVisitResult>)&Visitor;
#endif

                    var client_data = stackalloc nint[2] {
                        GCHandle.ToIntPtr(cursorChildren),
                        TranslationUnit.Handle.Handle
                    };

                    _ = clang.visitChildren(Handle, (IntPtr)pVisitor, client_data);

                    _cursorChildren = (List<Cursor>)cursorChildren.Target;
                    cursorChildren.Free();

#if !NET5_0_OR_GREATER
                    GC.KeepAlive(visitor);
#else
                    [UnmanagedCallersOnly(CallConvs = new System.Type[] { typeof(CallConvCdecl) })]
#endif
                    static CXChildVisitResult Visitor(CXCursor cursor, CXCursor parent, void* client_data)
                    {
                        var cursorChildren = (List<Cursor>)GCHandle.FromIntPtr(((nint*)client_data)[0]).Target;
                        var translationUnit = TranslationUnit.GetOrCreate((CXTranslationUnitImpl*)((nint*)client_data)[1]);

                        var cursorChild = translationUnit.GetOrCreate<Cursor>(cursor);
                        cursorChildren.Add(cursorChild);
                        return CXChildVisitResult.CXChildVisit_Continue;
                    }
                }
                return _cursorChildren;
            }
        }

        public CXCursorKind CursorKind => Handle.kind;

        public string CursorKindSpelling => _kindSpelling.Value;

        public CXSourceRange Extent => Handle.Extent;

        public CXCursor Handle { get; }

        public Cursor LexicalParentCursor => _lexicalParentCursor.Value;

        public CXSourceLocation Location => Handle.Location;

        public Cursor SemanticParentCursor => _semanticParentCursor.Value;

        public string Spelling => _spelling.Value;

        public TranslationUnit TranslationUnit => _translationUnit.Value;

        public static bool operator ==(Cursor left, Cursor right) => (left is object) ? ((right is object) && (left.Handle == right.Handle)) : (right is null);

        public static bool operator !=(Cursor left, Cursor right) => (left is object) ? ((right is null) || (left.Handle != right.Handle)) : (right is object);

        internal static Cursor Create(CXCursor handle)
        {
            Cursor result;

            if (handle.IsDeclaration || handle.IsTranslationUnit)
            {
                result = Decl.Create(handle);
            }
            else if (handle.IsReference)
            {
                result = Ref.Create(handle);
            }
            else if (handle.IsExpression || handle.IsStatement)
            {
                result = Stmt.Create(handle);
            }
            else if (handle.IsAttribute)
            {
                result = Attr.Create(handle);
            }
            else if (handle.IsPreprocessing)
            {
                result = PreprocessedEntity.Create(handle);
            }
            else
            {
                Debug.WriteLine($"Unhandled cursor kind: {handle.KindSpelling}.");
                result = new Cursor(handle, handle.Kind);
            }

            return result;
        }

        public override bool Equals(object obj) => (obj is Cursor other) && Equals(other);

        public bool Equals(Cursor other) => this == other;

        public override int GetHashCode() => Handle.GetHashCode();

        public override string ToString() => Spelling;
    }
}
