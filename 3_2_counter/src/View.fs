module Counter.View

open Fable.React
open Fable.React.Props
open Types

let root model dispatch =
    let onClick msg = OnClick(fun _ -> dispatch msg)
    div [] [ button [ onClick Decr ] [ str "-"] 
             div [] [ str <| string model.Count ]
             button [ onClick Incr ] [ str "+"] ]
