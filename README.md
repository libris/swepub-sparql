# SwePubs SPARQL-bibliotek
Här kommer SwePub-projektet - och kanske även du - att lägga upp SPARQL-frågor som kan vara användbara för andra.

# Ställa SPARQL-frågor
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


##TODO
* how-to och länkar till informationssidor
* fler querys

