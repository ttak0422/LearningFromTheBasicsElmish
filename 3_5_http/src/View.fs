module Http.View

open Fable.React
open Fable.React.Props
open Types

let root model dispatch =
    div [] [ button [ OnClick(fun _ -> dispatch Click) ] [ str "Get" ]
             p [] [ str model.Result ] ] 