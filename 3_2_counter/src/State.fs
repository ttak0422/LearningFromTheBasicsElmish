module Counter.State

open Elmish
open Types

let init () =
    { Count = 0 }, Cmd.none

let update msg model =
    match msg with
    | Incr -> { model with Count = model.Count + 1 }, Cmd.none
    | Decr -> { model with Count = model.Count - 1 }, Cmd.none