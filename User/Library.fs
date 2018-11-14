namespace User

type Bug1 = TpBug.TpBugProvider<"a">
type Bug2 = TpBug.TpBugProvider<"b">

module Say =
    let hello name =
        let x = Bug1()
        let y = Bug2()
        printfn "Hello %s" name
