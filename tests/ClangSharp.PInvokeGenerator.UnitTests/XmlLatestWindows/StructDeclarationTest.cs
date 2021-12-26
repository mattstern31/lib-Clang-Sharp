// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ClangSharp.Abstractions;

namespace ClangSharp.UnitTests
{
    public sealed class XmlLatestWindows_StructDeclarationTest : StructDeclarationTest
    {
        protected override Task BasicTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} r;
    {nativeType} g;
    {nativeType} b;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task BasicTestInCModeImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"typedef struct
{{
    {nativeType} r;
    {nativeType} g;
    {nativeType} b;
}}  MyStruct;
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";
            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents, commandlineArgs: Array.Empty<string>());
        }

        protected override Task BasicWithNativeTypeNameTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} r;
    {nativeType} g;
    {nativeType} b;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task BitfieldTestImpl()
        {
            var inputContents = @"struct MyStruct1
{
    unsigned int o0_b0_24 : 24;
    unsigned int o4_b0_16 : 16;
    unsigned int o4_b16_3 : 3;
    int o4_b19_3 : 3;
    unsigned char o8_b0_1 : 1;
    int o12_b0_1 : 1;
    int o12_b1_1 : 1;
};

struct MyStruct2
{
    unsigned int o0_b0_1 : 1;
    int x;
    unsigned int o8_b0_1 : 1;
};

struct MyStruct3
{
    unsigned int o0_b0_1 : 1;
    unsigned int o0_b1_1 : 1;
};
";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct1"" access=""public"">
      <field name=""_bitfield1"" access=""public"">
        <type>uint</type>
      </field>
      <field name=""o0_b0_24"" access=""public"">
        <type native=""unsigned int : 24"">uint</type>
        <get>
          <code>return <bitfieldName>_bitfield1</bitfieldName> &amp; 0x<bitwidthHexStringBacking>FFFFFFu</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield1</bitfieldName> = (_bitfield1 &amp; ~0x<bitwidthHexStringBacking>FFFFFFu</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>FFFFFFu</bitwidthHexString>);</code>
        </set>
      </field>
      <field name=""_bitfield2"" access=""public"">
        <type>uint</type>
      </field>
      <field name=""o4_b0_16"" access=""public"">
        <type native=""unsigned int : 16"">uint</type>
        <get>
          <code>return <bitfieldName>_bitfield2</bitfieldName> &amp; 0x<bitwidthHexStringBacking>FFFFu</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield2</bitfieldName> = (_bitfield2 &amp; ~0x<bitwidthHexStringBacking>FFFFu</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>FFFFu</bitwidthHexString>);</code>
        </set>
      </field>
      <field name=""o4_b16_3"" access=""public"">
        <type native=""unsigned int : 3"">uint</type>
        <get>
          <code>return (<bitfieldName>_bitfield2</bitfieldName> &gt;&gt; <bitfieldOffset>16</bitfieldOffset>) &amp; 0x<bitwidthHexStringBacking>7u</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield2</bitfieldName> = (_bitfield2 &amp; ~(0x<bitwidthHexStringBacking>7u</bitwidthHexStringBacking> &lt;&lt; <bitfieldOffset>16</bitfieldOffset>)) | ((value &amp; 0x<bitwidthHexString>7u</bitwidthHexString>) &lt;&lt; 16);</code>
        </set>
      </field>
      <field name=""o4_b19_3"" access=""public"">
        <type native=""int : 3"">int</type>
        <get>
          <code>return (<typeName>int</typeName>)((<bitfieldName>_bitfield2</bitfieldName> &gt;&gt; <bitfieldOffset>19</bitfieldOffset>) &amp; 0x<bitwidthHexStringBacking>7u</bitwidthHexStringBacking>);</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield2</bitfieldName> = (_bitfield2 &amp; ~(0x<bitwidthHexStringBacking>7u</bitwidthHexStringBacking> &lt;&lt; <bitfieldOffset>19</bitfieldOffset>)) | (uint)((value &amp; 0x<bitwidthHexString>7</bitwidthHexString>) &lt;&lt; 19);</code>
        </set>
      </field>
      <field name=""_bitfield3"" access=""public"">
        <type>byte</type>
      </field>
      <field name=""o8_b0_1"" access=""public"">
        <type native=""unsigned char : 1"">byte</type>
        <get>
          <code>return (<typeName>byte</typeName>)(<bitfieldName>_bitfield3</bitfieldName> &amp; 0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>);</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield3</bitfieldName> = (<typeNameBacking>byte</typeNameBacking>)((_bitfield3 &amp; ~0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>1u</bitwidthHexString>));</code>
        </set>
      </field>
      <field name=""_bitfield4"" access=""public"">
        <type>int</type>
      </field>
      <field name=""o12_b0_1"" access=""public"">
        <type native=""int : 1"">int</type>
        <get>
          <code>return <bitfieldName>_bitfield4</bitfieldName> &amp; 0x<bitwidthHexStringBacking>1</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield4</bitfieldName> = (_bitfield4 &amp; ~0x<bitwidthHexStringBacking>1</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>1</bitwidthHexString>);</code>
        </set>
      </field>
      <field name=""o12_b1_1"" access=""public"">
        <type native=""int : 1"">int</type>
        <get>
          <code>return (<bitfieldName>_bitfield4</bitfieldName> &gt;&gt; <bitfieldOffset>1</bitfieldOffset>) &amp; 0x<bitwidthHexStringBacking>1</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield4</bitfieldName> = (_bitfield4 &amp; ~(0x<bitwidthHexStringBacking>1</bitwidthHexStringBacking> &lt;&lt; <bitfieldOffset>1</bitfieldOffset>)) | ((value &amp; 0x<bitwidthHexString>1</bitwidthHexString>) &lt;&lt; 1);</code>
        </set>
      </field>
    </struct>
    <struct name=""MyStruct2"" access=""public"">
      <field name=""_bitfield1"" access=""public"">
        <type>uint</type>
      </field>
      <field name=""o0_b0_1"" access=""public"">
        <type native=""unsigned int : 1"">uint</type>
        <get>
          <code>return <bitfieldName>_bitfield1</bitfieldName> &amp; 0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield1</bitfieldName> = (_bitfield1 &amp; ~0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>1u</bitwidthHexString>);</code>
        </set>
      </field>
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
      <field name=""_bitfield2"" access=""public"">
        <type>uint</type>
      </field>
      <field name=""o8_b0_1"" access=""public"">
        <type native=""unsigned int : 1"">uint</type>
        <get>
          <code>return <bitfieldName>_bitfield2</bitfieldName> &amp; 0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield2</bitfieldName> = (_bitfield2 &amp; ~0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>1u</bitwidthHexString>);</code>
        </set>
      </field>
    </struct>
    <struct name=""MyStruct3"" access=""public"">
      <field name=""_bitfield"" access=""public"">
        <type>uint</type>
      </field>
      <field name=""o0_b0_1"" access=""public"">
        <type native=""unsigned int : 1"">uint</type>
        <get>
          <code>return <bitfieldName>_bitfield</bitfieldName> &amp; 0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield</bitfieldName> = (_bitfield &amp; ~0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>1u</bitwidthHexString>);</code>
        </set>
      </field>
      <field name=""o0_b1_1"" access=""public"">
        <type native=""unsigned int : 1"">uint</type>
        <get>
          <code>return (<bitfieldName>_bitfield</bitfieldName> &gt;&gt; <bitfieldOffset>1</bitfieldOffset>) &amp; 0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking>;</code>
        </get>
        <set>
          <code>
            <bitfieldName>_bitfield</bitfieldName> = (_bitfield &amp; ~(0x<bitwidthHexStringBacking>1u</bitwidthHexStringBacking> &lt;&lt; <bitfieldOffset>1</bitfieldOffset>)) | ((value &amp; 0x<bitwidthHexString>1u</bitwidthHexString>) &lt;&lt; 1);</code>
        </set>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task ExcludeTestImpl()
        {
            var inputContents = "typedef struct MyStruct MyStruct;";
            var expectedOutputContents = string.Empty;

            var excludedNames = new string[] { "MyStruct" };
            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents, excludedNames: excludedNames);
        }

        protected override Task FixedSizedBufferNonPrimitiveTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} value;
}};

struct MyOtherStruct
{{
    MyStruct c[3];
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""value"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
    </struct>
    <struct name=""MyOtherStruct"" access=""public"">
      <field name=""c"" access=""public"">
        <type native=""MyStruct [3]"" count=""3"" fixed=""_c_e__FixedBuffer"">MyStruct</type>
      </field>
      <struct name=""_c_e__FixedBuffer"" access=""public"">
        <field name=""e0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <indexer access=""public"">
          <type>ref MyStruct</type>
          <param name=""index"">
            <type>int</type>
          </param>
          <get>
            <code>return ref AsSpan()[index];</code>
          </get>
        </indexer>
        <function name=""AsSpan"" access=""public"">
          <type>Span&lt;MyStruct&gt;</type>
          <code>MemoryMarshal.CreateSpan(ref e0, 3);</code>
        </function>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task FixedSizedBufferNonPrimitiveMultidimensionalTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} value;
}};

struct MyOtherStruct
{{
    MyStruct c[2][1][3][4];
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""value"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
    </struct>
    <struct name=""MyOtherStruct"" access=""public"">
      <field name=""c"" access=""public"">
        <type native=""MyStruct [2][1][3][4]"" count=""2 * 1 * 3 * 4"" fixed=""_c_e__FixedBuffer"">MyStruct</type>
      </field>
      <struct name=""_c_e__FixedBuffer"" access=""public"">
        <field name=""e0_0_0_0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_0_0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_1_0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_1_0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_2_0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_2_0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_0_1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_0_1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_1_1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_1_1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_2_1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_2_1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_0_2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_0_2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_1_2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_1_2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_2_2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_2_2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_0_3"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_0_3"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_1_3"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_1_3"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e0_0_2_3"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1_0_2_3"" access=""public"">
          <type>MyStruct</type>
        </field>
        <indexer access=""public"">
          <type>ref MyStruct</type>
          <param name=""index"">
            <type>int</type>
          </param>
          <get>
            <code>return ref AsSpan()[index];</code>
          </get>
        </indexer>
        <function name=""AsSpan"" access=""public"">
          <type>Span&lt;MyStruct&gt;</type>
          <code>MemoryMarshal.CreateSpan(ref e0_0_0_0, 24);</code>
        </function>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task FixedSizedBufferNonPrimitiveTypedefTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} value;
}};

typedef MyStruct MyBuffer[3];

struct MyOtherStruct
{{
    MyBuffer c;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""value"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
    </struct>
    <struct name=""MyOtherStruct"" access=""public"">
      <field name=""c"" access=""public"">
        <type native=""MyBuffer"" count=""3"" fixed=""_c_e__FixedBuffer"">MyStruct</type>
      </field>
      <struct name=""_c_e__FixedBuffer"" access=""public"">
        <field name=""e0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <indexer access=""public"">
          <type>ref MyStruct</type>
          <param name=""index"">
            <type>int</type>
          </param>
          <get>
            <code>return ref AsSpan()[index];</code>
          </get>
        </indexer>
        <function name=""AsSpan"" access=""public"">
          <type>Span&lt;MyStruct&gt;</type>
          <code>MemoryMarshal.CreateSpan(ref e0, 3);</code>
        </function>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task FixedSizedBufferNonPrimitiveWithNativeTypeNameTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} value;
}};

struct MyOtherStruct
{{
    MyStruct c[3];
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""value"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
    </struct>
    <struct name=""MyOtherStruct"" access=""public"">
      <field name=""c"" access=""public"">
        <type native=""MyStruct [3]"" count=""3"" fixed=""_c_e__FixedBuffer"">MyStruct</type>
      </field>
      <struct name=""_c_e__FixedBuffer"" access=""public"">
        <field name=""e0"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e1"" access=""public"">
          <type>MyStruct</type>
        </field>
        <field name=""e2"" access=""public"">
          <type>MyStruct</type>
        </field>
        <indexer access=""public"">
          <type>ref MyStruct</type>
          <param name=""index"">
            <type>int</type>
          </param>
          <get>
            <code>return ref AsSpan()[index];</code>
          </get>
        </indexer>
        <function name=""AsSpan"" access=""public"">
          <type>Span&lt;MyStruct&gt;</type>
          <code>MemoryMarshal.CreateSpan(ref e0, 3);</code>
        </function>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task FixedSizedBufferPointerTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} c[3];
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""c"" access=""public"">
        <type native=""{nativeType}[3]"" count=""3"" fixed=""_c_e__FixedBuffer"">{expectedManagedType}</type>
      </field>
      <struct name=""_c_e__FixedBuffer"" access=""public"" unsafe=""true"">
        <field name=""e0"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
        <field name=""e1"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
        <field name=""e2"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
        <indexer access=""public"">
          <type>ref {expectedManagedType}</type>
          <param name=""index"">
            <type>int</type>
          </param>
          <get>
            <code>fixed ({expectedManagedType}* pThis = &amp;e0)
    {{
        return ref pThis[index];
    }}</code>
          </get>
        </indexer>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task FixedSizedBufferPrimitiveTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} c[3];
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"" unsafe=""true"">
      <field name=""c"" access=""public"">
        <type native=""{nativeType} [3]"" count=""3"" fixed=""_c_e__FixedBuffer"">{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task FixedSizedBufferPrimitiveMultidimensionalTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} c[2][1][3][4];
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"" unsafe=""true"">
      <field name=""c"" access=""public"">
        <type native=""{nativeType} [2][1][3][4]"" count=""2 * 1 * 3 * 4"" fixed=""_c_e__FixedBuffer"">{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task FixedSizedBufferPrimitiveTypedefTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"typedef {nativeType} MyBuffer[3];

struct MyStruct
{{
    MyBuffer c;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"" unsafe=""true"">
      <field name=""c"" access=""public"">
        <type native=""MyBuffer"" count=""3"" fixed=""_c_e__FixedBuffer"">{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task GuidTestImpl()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Non-Windows doesn't support __declspec(uuid(""))
                return Task.CompletedTask;
            }

            var inputContents = $@"#define DECLSPEC_UUID(x) __declspec(uuid(x))

struct __declspec(uuid(""00000000-0000-0000-C000-000000000046"")) MyStruct1
{{
    int x;
}};

struct DECLSPEC_UUID(""00000000-0000-0000-C000-000000000047"") MyStruct2
{{
    int x;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct1"" access=""public"" uuid=""00000000-0000-0000-c000-000000000046"">
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <struct name=""MyStruct2"" access=""public"" uuid=""00000000-0000-0000-c000-000000000047"">
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <class name=""Methods"" access=""public"" static=""true"">
      <iid name=""IID_MyStruct1"" value=""0x00000000, 0x0000, 0x0000, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46"" />
      <iid name=""IID_MyStruct2"" value=""0x00000000, 0x0000, 0x0000, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x47"" />
    </class>
  </namespace>
</bindings>
";

            var excludedNames = new string[] { "DECLSPEC_UUID" };
            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents, excludedNames: excludedNames);
        }

        protected override Task InheritanceTestImpl()
        {
            var inputContents = @"struct MyStruct1A
{
    int x;
    int y;
};

struct MyStruct1B
{
    int x;
    int y;
};

struct MyStruct2 : MyStruct1A, MyStruct1B
{
    int z;
    int w;
};
";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct1A"" access=""public"">
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
      <field name=""y"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <struct name=""MyStruct1B"" access=""public"">
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
      <field name=""y"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <struct name=""MyStruct2"" access=""public"" native=""struct MyStruct2 : MyStruct1A, MyStruct1B"">
      <field name=""Base"" access=""public"" inherited=""MyStruct1A"">
        <type>MyStruct1A</type>
      </field>
      <field name=""Base"" access=""public"" inherited=""MyStruct1B"">
        <type>MyStruct1B</type>
      </field>
      <field name=""z"" access=""public"">
        <type>int</type>
      </field>
      <field name=""w"" access=""public"">
        <type>int</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task InheritanceWithNativeInheritanceAttributeTestImpl()
        {
            var inputContents = @"struct MyStruct1A
{
    int x;
    int y;
};

struct MyStruct1B
{
    int x;
    int y;
};

struct MyStruct2 : MyStruct1A, MyStruct1B
{
    int z;
    int w;
};
";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct1A"" access=""public"">
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
      <field name=""y"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <struct name=""MyStruct1B"" access=""public"">
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
      <field name=""y"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <struct name=""MyStruct2"" access=""public"" native=""struct MyStruct2 : MyStruct1A, MyStruct1B"" parent=""MyStruct1B"">
      <field name=""Base"" access=""public"" inherited=""MyStruct1A"">
        <type>MyStruct1A</type>
      </field>
      <field name=""Base"" access=""public"" inherited=""MyStruct1B"">
        <type>MyStruct1B</type>
      </field>
      <field name=""z"" access=""public"">
        <type>int</type>
      </field>
      <field name=""w"" access=""public"">
        <type>int</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents, PInvokeGeneratorConfigurationOptions.GenerateNativeInheritanceAttribute);
        }

        protected override Task NestedAnonymousTestImpl(string nativeType, string expectedManagedType, int line, int column)
        {
            var inputContents = $@"typedef union {{
    {nativeType} value;
}} MyUnion;

struct MyStruct
{{
    {nativeType} x;
    {nativeType} y;

    struct
    {{
        {nativeType} z;

        struct
        {{
            {nativeType} value;
        }} w;

        MyUnion u;
        {nativeType} buffer1[4];
        MyUnion buffer2[4];
    }};
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyUnion"" access=""public"" layout=""Explicit"">
      <field name=""value"" access=""public"" offset=""0"">
        <type>{expectedManagedType}</type>
      </field>
    </struct>
    <struct name=""MyStruct"" access=""public"" unsafe=""true"">
      <field name=""x"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""y"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""Anonymous"" access=""public"">
        <type native=""MyStruct::(anonymous struct at ClangUnsavedFile.h:10:5)"">_Anonymous_e__Struct</type>
      </field>
      <field name=""z"" access=""public"">
        <type>ref {expectedManagedType}</type>
        <get>
          <code>return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.z, 1));</code>
        </get>
      </field>
      <field name=""w"" access=""public"">
        <type>ref _Anonymous_e__Struct._w_e__Struct</type>
        <get>
          <code>return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.w, 1));</code>
        </get>
      </field>
      <field name=""u"" access=""public"">
        <type>ref MyUnion</type>
        <get>
          <code>return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.u, 1));</code>
        </get>
      </field>
      <field name=""buffer1"" access=""public"">
        <type>Span&lt;{expectedManagedType}&gt;</type>
        <get>
          <code>return MemoryMarshal.CreateSpan(ref Anonymous.buffer1[0], 4);</code>
        </get>
      </field>
      <field name=""buffer2"" access=""public"">
        <type>Span&lt;MyUnion&gt;</type>
        <get>
          <code>return Anonymous.buffer2.AsSpan();</code>
        </get>
      </field>
      <struct name=""_Anonymous_e__Struct"" access=""public"" unsafe=""true"">
        <field name=""z"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
        <field name=""w"" access=""public"">
          <type native=""struct (anonymous struct at ClangUnsavedFile.h:14:9)"">_w_e__Struct</type>
        </field>
        <field name=""u"" access=""public"">
          <type>MyUnion</type>
        </field>
        <field name=""buffer1"" access=""public"">
          <type native=""{nativeType} [4]"" count=""4"" fixed=""_buffer1_e__FixedBuffer"">{expectedManagedType}</type>
        </field>
        <field name=""buffer2"" access=""public"">
          <type native=""MyUnion [4]"" count=""4"" fixed=""_buffer2_e__FixedBuffer"">MyUnion</type>
        </field>
        <struct name=""_w_e__Struct"" access=""public"">
          <field name=""value"" access=""public"">
            <type>{expectedManagedType}</type>
          </field>
        </struct>
        <struct name=""_buffer2_e__FixedBuffer"" access=""public"">
          <field name=""e0"" access=""public"">
            <type>MyUnion</type>
          </field>
          <field name=""e1"" access=""public"">
            <type>MyUnion</type>
          </field>
          <field name=""e2"" access=""public"">
            <type>MyUnion</type>
          </field>
          <field name=""e3"" access=""public"">
            <type>MyUnion</type>
          </field>
          <indexer access=""public"">
            <type>ref MyUnion</type>
            <param name=""index"">
              <type>int</type>
            </param>
            <get>
              <code>return ref AsSpan()[index];</code>
            </get>
          </indexer>
          <function name=""AsSpan"" access=""public"">
            <type>Span&lt;MyUnion&gt;</type>
            <code>MemoryMarshal.CreateSpan(ref e0, 4);</code>
          </function>
        </struct>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task NestedAnonymousWithBitfieldTestImpl()
        {
            var inputContents = @"struct MyStruct
{
    int x;
    int y;

    struct
    {
        int z;

        struct
        {
            int w;
            int o0_b0_16 : 16;
            int o0_b16_4 : 4;
        };
    };
};
";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""x"" access=""public"">
        <type>int</type>
      </field>
      <field name=""y"" access=""public"">
        <type>int</type>
      </field>
      <field name=""Anonymous"" access=""public"">
        <type native=""MyStruct::(anonymous struct at ClangUnsavedFile.h:6:5)"">_Anonymous_e__Struct</type>
      </field>
      <field name=""z"" access=""public"">
        <type>ref int</type>
        <get>
          <code>return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.z, 1));</code>
        </get>
      </field>
      <field name=""w"" access=""public"">
        <type>ref int</type>
        <get>
          <code>return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.w, 1));</code>
        </get>
      </field>
      <field name=""o0_b0_16"" access=""public"">
        <type>int</type>
        <get>
          <code>return Anonymous.Anonymous.o0_b0_16;</code>
        </get>
        <set>
          <code>Anonymous.Anonymous.o0_b0_16 = value;</code>
        </set>
      </field>
      <field name=""o0_b16_4"" access=""public"">
        <type>int</type>
        <get>
          <code>return Anonymous.Anonymous.o0_b16_4;</code>
        </get>
        <set>
          <code>Anonymous.Anonymous.o0_b16_4 = value;</code>
        </set>
      </field>
      <struct name=""_Anonymous_e__Struct"" access=""public"">
        <field name=""z"" access=""public"">
          <type>int</type>
        </field>
        <field name=""Anonymous"" access=""public"">
          <type native=""MyStruct::(anonymous struct at ClangUnsavedFile.h:10:9)"">_Anonymous_e__Struct</type>
        </field>
        <struct name=""_Anonymous_e__Struct"" access=""public"">
          <field name=""w"" access=""public"">
            <type>int</type>
          </field>
          <field name=""_bitfield"" access=""public"">
            <type>int</type>
          </field>
          <field name=""o0_b0_16"" access=""public"">
            <type native=""int : 16"">int</type>
            <get>
              <code>return <bitfieldName>_bitfield</bitfieldName> &amp; 0x<bitwidthHexStringBacking>FFFF</bitwidthHexStringBacking>;</code>
            </get>
            <set>
              <code>
                <bitfieldName>_bitfield</bitfieldName> = (_bitfield &amp; ~0x<bitwidthHexStringBacking>FFFF</bitwidthHexStringBacking>) | (value &amp; 0x<bitwidthHexString>FFFF</bitwidthHexString>);</code>
            </set>
          </field>
          <field name=""o0_b16_4"" access=""public"">
            <type native=""int : 4"">int</type>
            <get>
              <code>return (<bitfieldName>_bitfield</bitfieldName> &gt;&gt; <bitfieldOffset>16</bitfieldOffset>) &amp; 0x<bitwidthHexStringBacking>F</bitwidthHexStringBacking>;</code>
            </get>
            <set>
              <code>
                <bitfieldName>_bitfield</bitfieldName> = (_bitfield &amp; ~(0x<bitwidthHexStringBacking>F</bitwidthHexStringBacking> &lt;&lt; <bitfieldOffset>16</bitfieldOffset>)) | ((value &amp; 0x<bitwidthHexString>F</bitwidthHexString>) &lt;&lt; 16);</code>
            </set>
          </field>
        </struct>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task NestedTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} r;
    {nativeType} g;
    {nativeType} b;

    struct MyNestedStruct
    {{
        {nativeType} r;
        {nativeType} g;
        {nativeType} b;
        {nativeType} a;
    }};
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <struct name=""MyNestedStruct"" access=""public"">
        <field name=""r"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
        <field name=""g"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
        <field name=""b"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
        <field name=""a"" access=""public"">
          <type>{expectedManagedType}</type>
        </field>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task NestedWithNativeTypeNameTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"struct MyStruct
{{
    {nativeType} r;
    {nativeType} g;
    {nativeType} b;

    struct MyNestedStruct
    {{
        {nativeType} r;
        {nativeType} g;
        {nativeType} b;
        {nativeType} a;
    }};
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
      <struct name=""MyNestedStruct"" access=""public"">
        <field name=""r"" access=""public"">
          <type native=""{nativeType}"">{expectedManagedType}</type>
        </field>
        <field name=""g"" access=""public"">
          <type native=""{nativeType}"">{expectedManagedType}</type>
        </field>
        <field name=""b"" access=""public"">
          <type native=""{nativeType}"">{expectedManagedType}</type>
        </field>
        <field name=""a"" access=""public"">
          <type native=""{nativeType}"">{expectedManagedType}</type>
        </field>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task NewKeywordTestImpl()
        {
            var inputContents = @"struct MyStruct
{
    int Equals;
    int Dispose;
    int GetHashCode;
    int GetType;
    int MemberwiseClone;
    int ReferenceEquals;
    int ToString;
};";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""Equals"" access=""public"">
        <type>int</type>
      </field>
      <field name=""Dispose"" access=""public"">
        <type>int</type>
      </field>
      <field name=""GetHashCode"" access=""public"">
        <type>int</type>
      </field>
      <field name=""GetType"" access=""public"">
        <type>int</type>
      </field>
      <field name=""MemberwiseClone"" access=""public"">
        <type>int</type>
      </field>
      <field name=""ReferenceEquals"" access=""public"">
        <type>int</type>
      </field>
      <field name=""ToString"" access=""public"">
        <type>int</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task NoDefinitionTestImpl()
        {
            var inputContents = "typedef struct MyStruct MyStruct;";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public""></struct>
  </namespace>
</bindings>
";
            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }
        protected override Task PackTestImpl()
        {
            const string InputContents = @"struct MyStruct1 {
    unsigned Field1;

    void* Field2;

    unsigned Field3;
};

#pragma pack(4)

struct MyStruct2 {
    unsigned Field1;

    void* Field2;

    unsigned Field3;
};
";

            var packing = Environment.Is64BitProcess ? " layout=\"Sequential\" pack=\"4\"" : string.Empty;

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct1"" access=""public"" unsafe=""true"">
      <field name=""Field1"" access=""public"">
        <type native=""unsigned int"">uint</type>
      </field>
      <field name=""Field2"" access=""public"">
        <type>void*</type>
      </field>
      <field name=""Field3"" access=""public"">
        <type native=""unsigned int"">uint</type>
      </field>
    </struct>
    <struct name=""MyStruct2"" access=""public"" unsafe=""true""{packing}>
      <field name=""Field1"" access=""public"">
        <type native=""unsigned int"">uint</type>
      </field>
      <field name=""Field2"" access=""public"">
        <type>void*</type>
      </field>
      <field name=""Field3"" access=""public"">
        <type native=""unsigned int"">uint</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(InputContents, expectedOutputContents);
        }

        protected override Task PointerToSelfTestImpl()
        {
            var inputContents = @"struct example_s {
   example_s* next;
   void* data;
};";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""example_s"" access=""public"" unsafe=""true"">
      <field name=""next"" access=""public"">
        <type>example_s*</type>
      </field>
      <field name=""data"" access=""public"">
        <type>void*</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task PointerToSelfViaTypedefTestImpl()
        {
            var inputContents = @"typedef struct example_s example_t;

struct example_s {
   example_t* next;
   void* data;
};";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""example_s"" access=""public"" unsafe=""true"">
      <field name=""next"" access=""public"">
        <type native=""example_t *"">example_s*</type>
      </field>
      <field name=""data"" access=""public"">
        <type>void*</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task RemapTestImpl()
        {
            var inputContents = "typedef struct _MyStruct MyStruct;";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public""></struct>
  </namespace>
</bindings>
";

            var remappedNames = new Dictionary<string, string> { ["_MyStruct"] = "MyStruct" };
            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents, remappedNames: remappedNames);
        }

        protected override Task RemapNestedAnonymousTestImpl()
        {
            var inputContents = @"struct MyStruct
{
    double r;
    double g;
    double b;

    struct
    {
        double a;
    };
};";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type>double</type>
      </field>
      <field name=""g"" access=""public"">
        <type>double</type>
      </field>
      <field name=""b"" access=""public"">
        <type>double</type>
      </field>
      <field name=""Anonymous"" access=""public"">
        <type native=""MyStruct::(anonymous struct at ClangUnsavedFile.h:7:5)"">_Anonymous_e__Struct</type>
      </field>
      <field name=""a"" access=""public"">
        <type>ref double</type>
        <get>
          <code>return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.a, 1));</code>
        </get>
      </field>
      <struct name=""_Anonymous_e__Struct"" access=""public"">
        <field name=""a"" access=""public"">
          <type>double</type>
        </field>
      </struct>
    </struct>
  </namespace>
</bindings>
";

            var remappedNames = new Dictionary<string, string> {
                ["__AnonymousField_ClangUnsavedFile_L7_C5"] = "Anonymous",
                ["__AnonymousRecord_ClangUnsavedFile_L7_C5"] = "_Anonymous_e__Struct"
            };
            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents, remappedNames: remappedNames);
        }

        protected override Task SkipNonDefinitionTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"typedef struct MyStruct MyStruct;

struct MyStruct
{{
    {nativeType} r;
    {nativeType} g;
    {nativeType} b;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type>{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task SkipNonDefinitionPointerTestImpl()
        {
            var inputContents = @"typedef struct MyStruct* MyStructPtr;
typedef struct MyStruct& MyStructRef;
";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public""></struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task SkipNonDefinitionWithNativeTypeNameTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"typedef struct MyStruct MyStruct;

struct MyStruct
{{
    {nativeType} r;
    {nativeType} g;
    {nativeType} b;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type native=""{nativeType}"">{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task TypedefTestImpl(string nativeType, string expectedManagedType)
        {
            var inputContents = $@"typedef {nativeType} MyTypedefAlias;

struct MyStruct
{{
    MyTypedefAlias r;
    MyTypedefAlias g;
    MyTypedefAlias b;
}};
";

            var expectedOutputContents = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type native=""MyTypedefAlias"">{expectedManagedType}</type>
      </field>
      <field name=""g"" access=""public"">
        <type native=""MyTypedefAlias"">{expectedManagedType}</type>
      </field>
      <field name=""b"" access=""public"">
        <type native=""MyTypedefAlias"">{expectedManagedType}</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task UsingDeclarationTestImpl()
        {
            var inputContents = @"struct MyStruct1A
{
    void MyMethod() { }
};

struct MyStruct1B : MyStruct1A
{
    using MyStruct1A::MyMethod;
};
";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct1A"" access=""public"">
      <function name=""MyMethod"" access=""public"">
        <type>void</type>
        <code></code>
      </function>
    </struct>
    <struct name=""MyStruct1B"" access=""public"" native=""struct MyStruct1B : MyStruct1A"">
      <function name=""MyMethod"" access=""public"">
        <type>void</type>
        <code></code>
      </function>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents);
        }

        protected override Task WithAccessSpecifierTestImpl()
        {
            var inputContents = @"struct MyStruct1
{
    int Field1;
    int Field2;
};

struct MyStruct2
{
    int Field1;
    int Field2;
};

struct MyStruct3
{
    int Field1;
    int Field2;
};
";

            var expectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct1"" access=""private"">
      <field name=""Field1"" access=""private"">
        <type>int</type>
      </field>
      <field name=""Field2"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <struct name=""MyStruct2"" access=""internal"">
      <field name=""Field1"" access=""private"">
        <type>int</type>
      </field>
      <field name=""Field2"" access=""public"">
        <type>int</type>
      </field>
    </struct>
    <struct name=""MyStruct3"" access=""public"">
      <field name=""Field1"" access=""private"">
        <type>int</type>
      </field>
      <field name=""Field2"" access=""internal"">
        <type>int</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            var withAccessSpecifiers = new Dictionary<string, AccessSpecifier> {
                ["MyStruct1"] = AccessSpecifier.Private,
                ["MyStruct2"] = AccessSpecifier.Internal,
                ["Field1"] = AccessSpecifier.Private,
                ["MyStruct3.Field2"] = AccessSpecifier.Internal,
            };
            return ValidateGeneratedXmlLatestWindowsBindingsAsync(inputContents, expectedOutputContents, withAccessSpecifiers: withAccessSpecifiers);
        }

        protected override Task SourceLocationAttributeTestImpl()
        {
            const string InputContents = @"struct MyStruct
{
    int r;
    int g;
    int b;
};
";

            const string ExpectedOutputContents = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<bindings>
  <namespace name=""ClangSharp.Test"">
    <struct name=""MyStruct"" access=""public"">
      <field name=""r"" access=""public"">
        <type>int</type>
      </field>
      <field name=""g"" access=""public"">
        <type>int</type>
      </field>
      <field name=""b"" access=""public"">
        <type>int</type>
      </field>
    </struct>
  </namespace>
</bindings>
";

            return ValidateGeneratedXmlLatestWindowsBindingsAsync(InputContents, ExpectedOutputContents, PInvokeGeneratorConfigurationOptions.GenerateSourceLocationAttribute);
        }
    }
}
