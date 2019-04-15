module Http.View

open Fable.Helpers.React.Props
open Types

module R = Fable.Helpers.React

let root model dispatch =
    R.div [] [ R.button [ OnClick(fun _ -> dispatch Click) ] [ R.str "Get" ]
               R.p [] [ R.str model.Result ] ]
