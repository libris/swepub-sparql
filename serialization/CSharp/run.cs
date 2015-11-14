using System;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using JsonLD.Core;
using JsonLD.Impl;



public class Program {
     public static void Main (string[] args)
    {

        String sparql = File.ReadAllText("../query.sparql");
        //Console.WriteLine(sparql);
        var parameters = new Dictionary<string, string>();
        parameters["query"] = sparql;
        parameters["format"] = "text/turtle";

        var client = new HttpClient();
        StringContent queryString = new StringContent(sparql);
        HttpResponseMessage resp = client.PostAsync("http://virhp07.libris.kb.se/sparql",new FormUrlEncodedContent(parameters)).Result;
        Console.WriteLine ("Hello Mono World" + resp.StatusCode);
        var turtle = resp.Content.ReadAsStringAsync().Result;
        Console.WriteLine(turtle);
        var options = new JsonLdOptions() {format = "text/turtle"};
        options.SetUseNativeTypes(true);
        var expanded = JsonLdProcessor.FromRDF(turtle, options);
        var context = File.ReadAllText("../context.jsonld");
        var compOptions = new JsonLdOptions();
        compOptions.SetEmbed(true);
        var compacted = JsonLdProcessor.Frame(expanded, context, compOptions);
        Console.WriteLine(turtle);
        Console.WriteLine(expanded);
        Console.WriteLine(compacted);



    }
  }
