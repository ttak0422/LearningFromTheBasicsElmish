module Clock.View

open Fable.Helpers.React.Props
open Types

module R = Fable.Helpers.React

let root model dispatch =
    let hour = model.Time.Hour
    let minute = model.Time.Minute
    let second = model.Time.Second
    R.h1 [] [ R.str <| sprintf "%i:%i:%i" hour minute second ]
