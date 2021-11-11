// Copyright (c) .NET Foundation and Contributors. All rights reserved. Licensed under the University of Illinois/NCSA Open Source License. See LICENSE.txt in the project root for license information.

using System.Collections.Generic;

namespace ClangSharp
{
    public interface IDeclContext
    {
        IReadOnlyList<Decl> Decls { get; }

        bool IsNamespace { get; }

        bool IsStdNamespace { get; }

        bool IsTranslationUnit { get; }

        IDeclContext LexicalParent { get; }

        IDeclContext Parent { get; }

        IDeclContext RedeclContext { get; }
    }
}
