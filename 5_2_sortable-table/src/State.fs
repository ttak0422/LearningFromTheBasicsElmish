module Table.State

open Elmish
open SortableTable
open Types

let init () =
    let personList =
        [ { Name = "Taro"; Mail = "taro@example.com"}
          { Name = "Hanako"; Mail = "hanako@example.com"} ]
    { Items = personList
      TableState = SortableTable.init() }, Cmd.none

let update msg model =
    match msg with
    | SortableTableMsg msg ->
        { model with TableState = SortableTable.update msg model.TableState },
        Cmd.none
