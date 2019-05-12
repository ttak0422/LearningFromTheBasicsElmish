module SearchBox.Types

open GitHub

type UserState =
    | Init
    | Waiting
    | Loaded of User
    | Failed of exn

type Model =
    { Input : string
      UserState : UserState }

type Msg =
    | Input of string
    | Send
    | ReceiveUser of Result<User, string>
    | ReceiveUserErr of exn