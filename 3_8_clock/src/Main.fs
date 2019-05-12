module Clock

open Elmish
open Elmish.React
open Clock.State
open Clock.View

Program.mkProgram init update root
|> Program.withReactBatched "elmish-app"
|> Program.withSubscription (fun _ -> Cmd.ofSub tickTime)
|> Program.run