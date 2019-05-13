module Table.Types

type Person =
    { Name : string
      Mail : string }

type Model =
    { Items : Person list
      TableState : SortableTable.State }

type Msg =
    | SortableTableMsg of SortableTable.Msg