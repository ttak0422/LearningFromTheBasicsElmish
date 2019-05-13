module SPA

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Elmish.React
open SPA.Route
open SPA.State
open SPA.View

Program.mkProgram init update root
|> Program.toNavigable (parseHash parser) urlUpdate
|> Program.withReactBatched "elmish-app"
|> Program.run