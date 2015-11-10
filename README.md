# SwePubs SPARQL-bibliotek
Här kommer SwePub-projektet - och kanske även du - att lägga upp SPARQL-frågor som kan vara användbara för andra.

## Hur ställer jag SPARQL-frågor mot SwePub?
För att ställa frågor mot SwePub kan man antingen använda gränssnittet på http://info.swepub.kb.se eller gå direkt mot SwePubs Sparql-endpoint på http://virhp07.libris.kb.se/sparql/. 

Endpointen fungerar även som ett API. Man kan ställa frågor genom både POST och GET-anrop till samma url. 
Nedan ett exempel på anrop genom i Groovy med paketet WsLite som returnerar svaret i json-format:    

       def client = new RESTClient(http://virhp07.libris.kb.se/sparql)
        def response = client.post(
            accept: contentType = "application/json",
            path: '/',
            query: [query: sparql, format: contentType])
        assert 200 == response.statusCode
        return response.json   

## Hur formulerar jag mina SPARQL-frågor?
En bra indroduktion och steg-för-steg-genomgång i hur du använder den visuella [Query-generatorn](http://hp07.libris.kb.se/ExploreAndQuery/) finner du på [LIBRIS-bloggen](http://librisbloggen.kb.se/2014/06/18/swepub-analysis-you-can-sparql/).

##TODO
* how-to och länkar till informationssidor
* fler querys

