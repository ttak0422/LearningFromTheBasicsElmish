module Form.State

open Elmish
open Types

let init() : Model * Cmd<Msg> =
    { Input = ""
      Memos = [] }, Cmd.none

let update msg model : Model * Cmd<Msg> =
    match msg with
    | Input input -> { model with Input = input }, Cmd.none
    | Submit ->
        { model with Input = ""
                     Memos = model.Input :: model.Memos }, Cmd.none
