namespace IntelliFactory.WebSharper.BingExtension

module Main =
    
    open IntelliFactory.WebSharper.InterfaceGenerator
    

    do Compiler.Compile stdout Bing.Assembly

//    [<JavaScript>]
//    let Snippet1b =
//        Formlet.Yield (fun name -> name)
//        <*> (Controls.Input ""
//        |> Validator.IsNotEmpty "Enter a valid name"
//        |> Enhance.WithFormContainer)
//
//let tabs =
//[
//    "1b", ClientHtml.Div [(new Snippet1b()).Body]
//]
//ClientHtml.Div [Tabs.New(tabs, new TabsConfiguration())] :> _
//
//
//type FormletSnippets() =
//    inherit IntelliFactory.WebSharper.Web.Control()
//
//    [<JavaScript>]
//    override this.Body =
//        let tabs =
//            [
//            "1b", ClientHtml.Div [(new Snippet1b()).Body]
//            ]
//            ClientHtml.Div [Tabs.New(tabs, new TabsConfiguration())] :> _
//            then the pagE:
//            let FormletSnippetsPage =
//            Template "Formlet snippets"
//            <| fun ctx ->
//            [
//            H1 [Text "Formlet snippets"]
//            Div [new FormletSnippets()]
//            ]
