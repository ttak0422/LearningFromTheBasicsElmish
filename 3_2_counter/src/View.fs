module Counter.View

open Fable.Helpers.React.Props
open Types

module R = Fable.Helpers.React

let root model dispatch =
    let onClick msg = OnClick(fun _ -> dispatch msg)
    R.div [] [ R.button [ onClick Decrement ] [ R.str "-" ]
               R.div [] [ R.str (string model) ]
               R.button [ onClick Increment ] [ R.str "+" ] ]
