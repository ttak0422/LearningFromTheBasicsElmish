module Navbar.View


open Fable.Helpers.React.Props
module R =  Fable.Helpers.React


let navButton classy href faClass txt =
    R.p
        [ ClassName "control" ]
        [ R.a
            [ ClassName (sprintf "button %s" classy)
              Href href ]
            [ R.span
                [ ClassName "icon" ]
                [ R.i
                    [ ClassName (sprintf "fab %s" faClass) ]
                    [ ] ]
              R.span
                [ ]
                [ R.str txt ] ] ]

let navButtons =
    R.span
        [ ClassName "navbar-item" ]
        [ R.div
            [ ClassName "field is-grouped" ]
            [ navButton "twitter" "https://twitter.com/bgdaewalkman" "fa-twitter" "Twitter"
              navButton "github" "https://github.com/ttak0422/LearningFromTheBasicsElmish" "fa-github" "Fork me" ] ]

let root =
    R.nav
        [ ClassName "navbar is-dark" ]
        [ R.div
            [ ClassName "container" ]
            [ R.div
                [ ClassName "navbar-brand" ]
                [ R.h1
                    [ ClassName "navbar-item title is-4" ]
                    [ R.str "LearningFromTheBasicsElmish" ] ]
              R.div
                [ ClassName "navbar-end" ]
                [ navButtons ] ] ]
