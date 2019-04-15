module Http.Types

type Model =
    { Result : string }

type Msg =
    | Click
    | GotRepo of string
    | GotRepoErr of exn
