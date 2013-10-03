#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("IntelliFactory.WebSharper.Bing.Maps", "2.5")
        .References(fun r -> [r.Assembly "System.Web"])

let main =
    bt.WebSharper.Extension("IntelliFactory.WebSharper.Bing.Maps")
        .SourcesFromProject()

let rest =
    bt.WebSharper.Library("IntelliFactory.WebSharper.Bing.Maps.Rest")
        .SourcesFromProject()
        .References(fun r -> [r.Project main])

let test =
    bt.WebSharper.HtmlWebsite("IntelliFactory.WebSharper.Bing.Maps.Tests")
        .SourcesFromProject()
        .References(fun r -> [r.Project main; r.Project rest])

bt.Solution [
    main
    rest
    test

    bt.NuGet.CreatePackage()
        .Description("WebSharper Extensions for Bing Maps AJAX v7 and REST services")
        .ProjectUrl("https://github.com/intellifactory/websharper.bing.maps")
        .Add(main)

]
|> bt.Dispatch
