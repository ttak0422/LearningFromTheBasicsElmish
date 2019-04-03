module Form.Types


type Model =
    { Input : string
      Memos : string list }

type Msg =
    | Input of string
    | Submit