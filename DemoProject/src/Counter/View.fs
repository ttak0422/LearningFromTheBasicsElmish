module Counter.View


open Fable.Helpers.React.Props
module R = Fable.Helpers.React
open Types


let simpleButton txt action dispatch =
  R.div
    [ ClassName "column is-narrow" ]
    [ R.a
        [ ClassName "button"
          OnClick (fun _ -> action |> dispatch) ]
        [ R.str txt ] ]

let root model dispatch =
    let onClick msg = OnClick (fun _ -> dispatch msg)
    R.div []
        [ R.button [ onClick Decrement ] [ R.str "-"]
          R.div [] [ R.str (string model) ]
          R.button [ onClick Increment ] [ R.str "+"] ]
