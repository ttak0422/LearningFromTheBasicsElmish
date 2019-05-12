module Clock.View

open Fable.React
open Fable.React.Props
open Types

let root model dispatch =
    let hour = model.Time.Hour
    let minute = model.Time.Minute
    let second = model.Time.Second
    h1 [] [ str <| sprintf "%i:%i:%i" hour minute second ]