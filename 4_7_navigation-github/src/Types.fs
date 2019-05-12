module Nav.Types

open GitHub

type Page =
    | NotFound
    | ErrPage of exn
    | TopPage
    | UserPage of Result<Repo list, string>
    | RepoPage of Result<Issue list, string>

type Model =
    { Page : Page }

type Msg =
    | Loaded of Page
    | Loadfailed of exn
