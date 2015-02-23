#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    let bt = BuildTool().PackageId("WebSharper.Bing.Maps", "3.0-alpha").References(fun r -> [r.Assembly "System.Web"])
    bt.WithFramework(bt.Framework.Net40)

let main =
    bt.WebSharper.Extension("WebSharper.Bing.Maps")
        .SourcesFromProject()

let rest =
    bt.WebSharper.Library("WebSharper.Bing.Maps.Rest")
        .SourcesFromProject()
        .References(fun r -> [r.Project main])

let test =
    bt.WebSharper.HtmlWebsite("WebSharper.Bing.Maps.Tests")
        .SourcesFromProject()
        .References(fun r -> [r.Project main; r.Project rest])

bt.Solution [
    main
    rest
    test

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.Bing.Maps-v7"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/websharper.bing.maps"
                Description = "WebSharper Extensions for Bing Maps AJAX v7 and REST services"
                RequiresLicenseAcceptance = true })
        .Add(main)
        .Add(rest)

]
|> bt.Dispatch
