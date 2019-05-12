module Http.Types

type Model = 
    { Result : string }

type Msg = 
    | Click
    | GotRepo of Result<string, string> 
    | GotRepoErr of exn