module Counter


open Elmish
open Elmish.React
open Fable.Helpers.React.Props
module R = Fable.Helpers.React


// MODEL
type Model = int

let init () : Model =
    0


// UPDATE
type Msg =
    | Increment
    | Decrement

let update msg model =
    match msg with
    | Increment -> model + 1
    | Decrement -> model - 1


// VIEW
let view model dispatch =
    let onClick msg =
        OnClick (fun _ -> dispatch msg)
    R.div []
        [ R.button [ onClick Decrement ] [ R.str "-"]
          R.div [] [ R.str (string model) ]
          R.button [ onClick Increment ] [ R.str "+"] ]


Program.mkSimple init update view
|> Program.withReact "elmish-app"
|> Program.run