module SearchBox.State

open Elmish
open GitHub
open Types

let init() : Model * Cmd<Msg> =
    { Input = ""
      UserState = Init }, Cmd.none

let update msg model : Model * Cmd<Msg> =
    match msg with
    | Input input -> { model with Input = input }, Cmd.none
    | Send ->
        { model with Input = ""
                     UserState = Waiting },
        getUser ReceiveUser ReceiveUserErr model.Input
    | ReceiveUser user -> { model with UserState = Loaded user }, Cmd.none
    | ReceiveUserErr e -> { model with UserState = Failed e }, Cmd.none
