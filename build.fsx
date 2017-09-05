#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.Bing.Maps")
        .VersionFrom("WebSharper", versionSpec = "(,4.0)")
        .References(fun r -> [r.Assembly "System.Web"])
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun f -> f.Net40)

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
        .References(fun r ->
            [
                r.Project main
                r.Project rest
                r.NuGet("WebSharper.Html").Version("(,4.0)").ForceFoundVersion().Reference()
            ])

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
