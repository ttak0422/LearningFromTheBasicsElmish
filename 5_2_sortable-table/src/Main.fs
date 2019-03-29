module Counter


open Elmish
open Elmish.React
module R = Fable.Helpers.React
open SortableTable
open Elmish.ReactNative


// MODEL
type Person =
    { Name : string
      Mail : string }

type Model =
    { Items : Person list
      TableState : SortableTable.State }

let init () =
    let personList =
        [ { Name = "Taro"; Mail = "taro@example.com"}
          { Name = "Hanako"; Mail = "hanako@example.com"} ]
    { Items = personList
      TableState = SortableTable.init () }


// UPDATE
type Msg =
    | SortableTableMsg of SortableTable.Msg

let update msg model =
    match msg with
    | SortableTableMsg msg ->
        { model with TableState = SortableTable.update msg model.TableState }


// VIEW
let config : SortableTable.Config<Person> =
    { Columns = [ "Name"; "Mail"]
      ToValue =
        (fun id person ->
            match id with
            | "Name" -> person.Name
            | "Mail" -> person.Mail
            | _ -> failwith <| "unknown column: " + id) }

let view model (dispatch : Msg -> unit) =
    R.div []
        [ R.h1 [] [ R.str "People" ]
          SortableTable.view config model.TableState model.Items (SortableTableMsg >> dispatch) ]

Program.mkSimple init update view
|> Program.withReact "elmish-app"
|> Program.run