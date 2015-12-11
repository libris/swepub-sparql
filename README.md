# SwePubs SPARQL-bibliotek
Här kommer SwePub-projektet - och kanske även du - att lägga upp SPARQL-frågor som kan vara användbara för andra.   
Källkoden för SwePubs webbgränssnitt hittar du på <https://github.com/libris/swepubanalys>.

## Hur ställer jag SPARQL-frågor mot SwePub?
För att ställa frågor mot SwePub kan man antingen använda gränssnittet på <http://info.swepub.kb.se> eller gå direkt mot SwePubs Sparql-endpoint på <http://virhp07.libris.kb.se/sparql/>. 

Endpointen fungerar även som ett API. Man kan ställa frågor genom både POST och GET-anrop till samma url. 
Nedan följer ett exempel på anrop genom Groovy med paketet WsLite som returnerar svaret i json-format:    

       def client = new RESTClient(http://virhp07.libris.kb.se/sparql)
        def response = client.post(
            accept: contentType = "application/json",
            path: '/',
            query: [query: sparql, format: contentType])
        assert 200 == response.statusCode
        return response.json   

För mer information, läs i [Wikin](https://github.com/libris/swepub-sparql/wiki) eller på [SwePubs sidor på kb.se](http://www.kb.se/libris/swepub).

