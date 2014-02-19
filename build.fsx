#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    let bt = BuildTool().PackageId("WebSharper.Bing.Maps", "2.5").References(fun r -> [r.Assembly "System.Web"])
    bt.WithFramework(bt.Framework.Net40)

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
