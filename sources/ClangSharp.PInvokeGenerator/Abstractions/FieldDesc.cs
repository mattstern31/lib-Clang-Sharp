// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using ClangSharp.Interop;

namespace ClangSharp.Abstractions
{
    internal struct FieldDesc
    {
        public AccessSpecifier AccessSpecifier { get; set; }
        public string NativeTypeName { get; set; }
        public string EscapedName { get; set; }
        public int? Offset { get; set; }
        public bool NeedsNewKeyword { get; set; }
        public string InheritedFrom { get; set; }
        public CXSourceLocation? Location { get; set; }
    }
}
