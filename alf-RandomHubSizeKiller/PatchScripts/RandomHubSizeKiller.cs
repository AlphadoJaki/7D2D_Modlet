using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using SDX.Core;
using SDX.Compiler;
using SDX.Payload;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;

public class RandomHubSizeKiller : IPatcherMod
{
    public bool Patch(ModuleDefinition module)

    {
        Console.WriteLine(" == Random Hub Size Killer == ");

        CustomEntityClasses(module);

        return true;

    }

    public bool Link(ModuleDefinition gameModule, ModuleDefinition modModule)
    {
        return true;
    }
    private void CustomEntityClasses(ModuleDefinition module)
    {
        var method = module.GetType("WorldGenerationEngine.GenerationManager").Methods.First(d => d.Name == "GenerateTowns");
        var first = method.Body.Instructions.First(d => d.OpCode == OpCodes.Stloc_0).Next;
        var numOp = method.Body.Instructions.First(d => d.OpCode == OpCodes.Ldc_I4).Next;
        var last = numOp.Next;
        var il = method.Body.GetILProcessor();
  
        il.Remove(last);
        il.Remove(numOp.Previous);
        il.Remove(numOp.Previous);
        il.Remove(numOp.Previous);

    }
}
