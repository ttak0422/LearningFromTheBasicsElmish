module Http

open Elmish
open Elmish.React
open Http.State
open Http.View

Program.mkProgram init update root
|> Program.withReactBatched "elmish-app"
|> Program.run