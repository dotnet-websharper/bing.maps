namespace IntelliFactory.WebSharper.BingExtension

module Main =
    open IntelliFactory.WebSharper.InterfaceGenerator

    do Compiler.Compile stdout Bing.Assembly
