module Counter

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Elmish.React
open Nav.Route
open Nav.State
open Nav.View

Program.mkProgram init update root
|> Program.toNavigable (parseHash parser) urlUpdate
|> Program.withReactBatched "elmish-app"
|> Program.run