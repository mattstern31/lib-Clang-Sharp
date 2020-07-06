// Copyright (c) Microsoft and Contributors. All rights reserved. Licensed under the University of Illinois/NCSA Open Source License. See LICENSE.txt in the project root for license information.

using ClangSharp.Interop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClangSharp
{
    public sealed class BlockDecl : Decl, IDeclContext
    {
        private readonly Lazy<Decl> _blockManglingContextDecl;
        private readonly Lazy<IReadOnlyList<Capture>> _captures;
        private readonly Lazy<IReadOnlyList<Decl>> _decls;
        private readonly Lazy<IReadOnlyList<ParmVarDecl>> _parameters;

        internal BlockDecl(CXCursor handle) : base(handle, CXCursorKind.CXCursor_UnexposedDecl, CX_DeclKind.CX_DeclKind_Block)
        {
            _blockManglingContextDecl = new Lazy<Decl>(() => TranslationUnit.GetOrCreate<Decl>(Handle.BlockManglingContextDecl));
            _captures = new Lazy<IReadOnlyList<Capture>>(() => {
                var captureCount = Handle.NumCaptures;
                var captures = new List<Capture>(captureCount);

                for (int i = 0; i < captureCount; i++)
                {
                    var capture = new Capture(this, unchecked((uint)i));
                    captures.Add(capture);
                }

                return captures;
            });
            _decls = new Lazy<IReadOnlyList<Decl>>(() => CursorChildren.OfType<Decl>().ToList());
            _parameters = new Lazy<IReadOnlyList<ParmVarDecl>>(() => {
                var parameterCount = Handle.NumArguments;
                var parameters = new List<ParmVarDecl>(parameterCount);

                for (int i = 0; i < parameterCount; i++)
                {
                    var parameter = TranslationUnit.GetOrCreate<ParmVarDecl>(Handle.GetArgument(unchecked((uint)i)));
                    parameters.Add(parameter);
                }

                return parameters;
            });
        }

        public Decl BlockManglingContextDecl => _blockManglingContextDecl.Value;

        public uint BlockManglingNumber => unchecked((uint)Handle.BlockManglingNumber);

        public bool BlockMissingReturnType => Handle.BlockMissingReturnType;

        public IReadOnlyList<Capture> Captures => _captures.Value;

        public bool CanAvoidCopyToHeap() => Handle.CanAvoidCopyToHeap;

        public bool CapturesCXXThis => Handle.CapturesCXXThis;

        public CompoundStmt CompoundBody => (CompoundStmt)Body;

        public IReadOnlyList<Decl> Decls => _decls.Value;

        public bool DoesNotEscape => Handle.DoesNotEscape;

        public bool IsConversionFromLambda => Handle.IsConversionFromLambda;

        public bool IsVariadic => Handle.IsVariadic;

        public IDeclContext LexicalParent => LexicalDeclContext;

        public IReadOnlyList<ParmVarDecl> Parameters => _parameters.Value;

        public IDeclContext Parent => DeclContext;

        public bool CapturesVariable(VarDecl var) => Handle.CapturesVariable(var.Handle);
    }
}
