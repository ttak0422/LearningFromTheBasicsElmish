module Table.View

open Fable.React
open Fable.React.Props
open Types

let root model dispatch =

    let config : SortableTable.Config<Person> =
        { Columns = [ "Name"; "Mail"]
          ToValue =
            (fun id person ->
                match id with
                | "Name" -> person.Name
                | "Mail" -> person.Mail
                | _ -> failwith <| "unknown: " + id) }

    div []
        [ h1 [] [ str "people" ]
          SortableTable.view config model.TableState model.Items (SortableTableMsg >> dispatch) ]
