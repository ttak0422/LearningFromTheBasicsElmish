module Global


type Page =
    | Home
    | Counter
    | Form
    | About

let toHash page =
    match page with
    | About -> "#about"
    | Counter -> "#counter"
    | Form -> "#form"
    | Home -> "#home"
