module Counter

open Elmish
open Elmish.React
open Counter.State
open Counter.View

Program.mkProgram init update root
|> Program.withReact "elmish-app"
|> Program.run
