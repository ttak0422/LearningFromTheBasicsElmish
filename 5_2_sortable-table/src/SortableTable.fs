module SortableTable


open Fable.Helpers.React.Props
module R = Fable.Helpers.React


type State =
    { SortedColumn : Option<string>
      Reversed : bool }

let init () =
    { SortedColumn = None
      Reversed = false }


type Msg =
    | Sort of string

let update msg state =
    match msg with
    | Sort column ->
        if state.SortedColumn = Some column then
            { state with Reversed = not state.Reversed }
        else
            { state with SortedColumn = Some column; Reversed = false }


type Config<'a> =
    { Columns : string list
      ToValue : string -> 'a -> string }

let sort config state items =
    match state.SortedColumn with
    | Some column ->
        List.sortBy (config.ToValue column) items
        |> if state.Reversed then List.rev else id
    | None ->
        items

(*
    呼び出し元からのdispatcherを各関数で引数としてもらうようにするのは大変，
    他で呼び出すこともないのでローカル関数とした．
*)
let view config state items dispatch =
    let onClick msg = OnClick (fun _ -> dispatch msg)

    let headerCell config state columnId =
        let label =
            columnId +
            match (state.SortedColumn = Some columnId, state.Reversed) with
            | (true, false) -> "(↓)"
            | (true, true) ->  "(↑)"
            | _ -> ""
        R.th [ onClick <| Sort columnId ] [ R.str label ]

    let bodyCell config item columnId  =
        R.td [] [ R.str <| config.ToValue columnId item ]

    let headerRow config state =
        R.tr [] (List.map (headerCell config state) config.Columns)

    let bodyRow config item =
        R.tr [] (List.map (bodyCell config item) config.Columns)

    R.table []
        [ R.thead [] [ headerRow config state ]
          R.tbody [] (List.map (bodyRow config) (sort config state items)) ]