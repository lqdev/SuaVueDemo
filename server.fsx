#r "./packages/Suave/lib/net461/Suave.dll"

open Suave
open Suave.Files
open Suave.SuaveConfig
open Suave.Successful
open Suave.Filters
open Suave.Operators
open System.IO

//Configuration
let serverConfig:SuaveConfig = {
    defaultConfig with
        homeFolder = Some (Path.GetFullPath "./public")
        bindings = [HttpBinding.createSimple Protocol.HTTP "127.0.0.1" 8001]
}

let app = 
    choose [ 
        GET >=> choose
            [ 
                path "/" >=> Files.sendFile "./public/index.html" false
                Files.browseHome 
            ]
        RequestErrors.NOT_FOUND "Page not found"
    ]

startWebServer serverConfig app