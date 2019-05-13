module Table.SortableTable

open Fable.React
open Fable.React.Props


// Types

type State =
    { SortedColumn : Option<string>
      Reversed : bool }

type Msg =
    | Sort of string

type Config<'a> =
    { Columns : string list
      ToValue : string -> 'a -> string }

// State

let init () =
    { SortedColumn = None
      Reversed = false }

let update msg state =
    match msg with
    | Sort column ->
        if state.SortedColumn = Some column then
            { state with Reversed = not state.Reversed }
        else
            { state with
                SortedColumn = Some column
                Reversed = false }

let sort config state items =
    match state.SortedColumn with
    | Some column ->
        List.sortBy (config.ToValue column) items
        |> if state.Reversed then List.rev else id
    | None ->
        items

let view config state items dispatch =
    let onClick msg = OnClick(fun _ -> dispatch msg)

    let headerCell config state columnId =
        let label =
            columnId +
            match (state.SortedColumn = Some columnId, state.Reversed) with
            | (true, false) -> "(↓)"
            | (true, true) -> "(↑)"
            | _ -> ""
        th [ onClick <| Sort columnId ] [ str label ]
    let bodyCell config item columnId =
        td [] [ str <| config.ToValue columnId item ]
    let headerRow config state =
        tr [] (List.map (headerCell config state) config.Columns)
    let bodyRow config item =
        tr [] (List.map (bodyCell config item) config.Columns)
    table []
        [ thead [] [ headerRow config state ]
          tbody [] (List.map (bodyRow config) (sort config state items)) ]