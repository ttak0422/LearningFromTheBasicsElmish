module Counter


open Elmish
open Elmish.React
open Fable.Import
open System
open Elmish.ReactNative
module R = Fable.Helpers.React


// MODEL
type Model =
    { Time : DateTime }

let init () =
    ( { Time = DateTime.Now }
    , Cmd.none )


// UPDATE
type Msg =
    | Tick of DateTime

let update msg model =
    match msg with
    | Tick time ->
        ( { model with Time = time }
        , Cmd.none )


// VIEW
let view model dispatch =
    let hour = model.Time.Hour
    let minute = model.Time.Minute
    let second = model.Time.Second

    R.h1 [] [ R.str <| sprintf "%i:%i:%i" hour minute second ]


// SUBSCRIPTION
let subscription model =
    let tickTime dispatch =
        Browser.window.setInterval (fun _ ->
            dispatch <| Tick DateTime.Now
        , 1000 ) |> ignore
    Cmd.ofSub tickTime


Program.mkProgram init update view
|> Program.withSubscription subscription
|> Program.withReact "elmish-app"
|> Program.run