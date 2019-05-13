module Table


open Elmish
open Elmish.React
open Table.State
open Table.View

Program.mkProgram init update root
|> Program.withReactBatched "elmish-app"
|> Program.run