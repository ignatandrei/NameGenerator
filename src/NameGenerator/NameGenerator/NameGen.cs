﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Text;

namespace NameGenerator;
[Generator]
internal class NameGen : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var gen = NameGeneratorData.Generate();

        context.RegisterSourceOutput(context.CompilationProvider, (spc, compilation) =>
        {
        var dtNow = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            var assemblyName = compilation.AssemblyName;
            
            var source = $@"
                // <auto-generated/>
                namespace Generated.{assemblyName}
                {{
                    public static class TheAssemblyInfo
                    {{
                        
                        public static readonly System.DateTime DateGeneratedUTC ;
                        public const string AssemblyName = ""{assemblyName}"";
                        public const string GeneratedNameNice = ""{gen.UniqueNameLong}"";
                        public const string GeneratedNameSmall = ""{gen.UniqueNameSmall}"";
                        public const string GeneratedName = ""{gen.UniqueName}"";
                        static TheAssemblyInfo(){{
                            DateGeneratedUTC = System.DateTime.ParseExact(""{dtNow}"", ""yyyy-MM-dd HH:mm:ss"", null);
                        }}
                    }}
                }}";

            spc.AddSource("TheAssemblyInfo.g.cs", SourceText.From(source, Encoding.UTF8));
        });
    }
}
