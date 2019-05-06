module Counter

open Elmish
open Elmish.React
open Counter.State
open Counter.View

Program.mkProgram init update root
|> Program.withReactBatched "elmish-app"
|> Program.run