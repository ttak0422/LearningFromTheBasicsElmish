module Form

open Elmish
open Elmish.React
open Form.State
open Form.View

Program.mkProgram init update root
|> Program.withReactBatched "elmish-app"
|> Program.run