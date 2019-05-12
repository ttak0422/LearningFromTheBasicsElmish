module SearchBox

open Elmish
open Elmish.React
open SearchBox.State
open SearchBox.View

Program.mkProgram init update root
|> Program.withReactBatched "elmish-app"
|> Program.run