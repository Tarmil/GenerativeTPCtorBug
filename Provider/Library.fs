namespace Provider

open FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes

[<TypeProvider>]
type MyProvider (cfg: TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces(cfg)

    let asm = ProvidedAssembly()
    let rootNamespace = "TpBug"

    do
        let templateTy = ProvidedTypeDefinition(asm, rootNamespace, "TpBugProvider", None, isErased = false)
        templateTy.DefineStaticParameters([ProvidedStaticParameter("param", typeof<string>)], fun typename pars ->
            let n = pars.[0] :?> string
            let ty = ProvidedTypeDefinition(asm, rootNamespace, typename, Some typeof<obj>, isErased = false)
            ProvidedConstructor([], fun _ -> <@@ () @@>) |> ty.AddMember
            asm.AddTypes([ty])
            ty
        )
        this.AddNamespace(rootNamespace, [templateTy])

[<assembly:TypeProviderAssembly>]
do ()